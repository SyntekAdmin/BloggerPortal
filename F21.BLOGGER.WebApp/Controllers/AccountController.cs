#region Using

using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using F21.BLOGGER.WebApp.Models;
using F21.BLOGGER.Business;
using F21.BLOGGER.WebApp.Common;
using F21.BLOGGER.Model;
using System.Web;

#endregion

namespace F21.BLOGGER.WebApp.Controllers
{
    [Authorize]
    public class AccountController : BaseExController 
    {

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            EnsureLoggedOut();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            EnsureLoggedOut();
            var viewModel = new AccountLoginModel { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
       
        public async Task<ActionResult> Login(AccountLoginModel viewModel)
        {
             //[ValidateAntiForgeryToken]
            LogUtil log = new LogUtil();

            // ModelState 가 false 이면 로그인 화면으로 이동 시킴
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                UserBiz biz = new UserBiz();

                // 이메일이 등록 된 계정인지 확인
                int user_cnt = -1;

                user_cnt = biz.GetUserCountByUserId(viewModel.Email);

                if (user_cnt > 0)
                {
                    string encryt_password = Settings.SHA256Hash(viewModel.Password);
                    InfUser identity = biz.LoginUser(viewModel.Email, encryt_password);
                    if (identity != null)
                    {
                        identity.SetTicket();
                        return RedirectToLocal(viewModel.ReturnUrl);
                    }

                    // log.LogWrite("login", ModelState.);

                    //log.LogWrite("login", GetSsoErrorMessage(-2));
                    ModelState.AddModelError("", GetSsoErrorMessage(-2));
                    viewModel.ErrorMessege = GetSsoErrorMessage(-2);
                    return View(viewModel);
                }
                else
                {
                    //log.LogWrite("login", GetSsoErrorMessage(-1));
                    ModelState.AddModelError("", GetSsoErrorMessage(-1));
                    viewModel.ErrorMessege = GetSsoErrorMessage(-1);
                    return View(viewModel);
                }
            }
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            EnsureLoggedOut();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            EnsureLoggedOut();
            return View(new AccountRegistrationModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountRegistrationModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (viewModel.Password.Equals( viewModel.PasswordConfirm, StringComparison.OrdinalIgnoreCase ) )
                return View(viewModel);

            try
            {
                var now = DateTime.Now;

                return RedirectToLocal();
            }
            catch (DbEntityValidationException ex)
            {
                AddErrors(ex);

                return View(viewModel);
            }
        }

        [Route("account/logout")]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            return RedirectToLocal();
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            if (!returnUrl.IsNullOrWhiteSpace() && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("index", "home");
        }

        private void AddErrors(DbEntityValidationException exc)
        {
            foreach (var error in exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors.Select(validationError => validationError.ErrorMessage)))
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            // Add all errors that were returned to the page error collection
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void EnsureLoggedOut()
        {
            if (Request.IsAuthenticated)
                Logout();
        }


        private string GetSsoErrorMessage(int errorNo)
        {
            switch (errorNo)
            {
                case -1:
                    return "Couldn't find your Global Forever 21 Account. Please create new account.";
                case -2:
                    return "Your email or password is incorrect. ";
                case -3:
                    return "인증 요청 실패 횟수를 초과하여, 사용자 계정이 잠겨있습니다.";
                case -21:
                    return "사용자의 인증 유후시간이 경과하였습니다. 다시 로그인 해주세요.";
                case -24:
                    return "복호화 과정에서 에러가 발생하였습니다. 다시 로그인 해주세요.";
                case -26:
                    return "인증 서버와 연결할 수 없습니다. 다시 로그인 해주세요.";
                case -27:
                    return "인증 내부 에러. 다시 로그인 해주세요.";
                case -35:
                    return "인증 만료 시간이 지났습니다. 다시 로그인 해주세요.";
                case -36:
                    return "접근 권한이 없습니다. 문의사항은 정보시스템개발부로 연락주시기 바랍니다.";
                case -37:
                    return "패스워드가 만료되었습니다. 문의사항은 정보시스템개발부로 연락주시기 바랍니다.";
                case -39:
                    return "승인 대기중입니다. 문의사항은 정보시스템개발부로 연락주시기 바랍니다.";
                case -40:
                    return "계정이 잠겼습니다. 문의사항은 정보시스템개발부로 연락주시기 바랍니다.";
                case -41:
                    return "퇴직자 계정입니다. 문의사항은 정보시스템개발부로 연락주시기 바랍니다.";
                case -42:
                    return "계정이 만료 되었습니다. 문의사항은 정보시스템개발부로 연락주시기 바랍니다.";

                case -43:
                    return "인증이 실패하였습니다. 다시 로그인 해주세요.";
                default:
                    return "Login error (errorNo:" + errorNo + ") retry login."; 
            }
        }


        public ActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Terms()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Thankyou()
        {
            return View();
        }

    }
}