using System;
using System.Web;
using System.Web.Mvc;
using Stis.Core;
using System.Web.Security;
using System.Linq;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using Stis.Web.Mvc;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using F21.BLOGGER.Web.Models;
using F21.BLOGGER.Web.Common;
using F21.BLOGGER.Model;
using F21.BLOGGER.Business;

namespace F21.BLOGGER.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseExController
    {

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        public ActionResult RegistPolicyCheck()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegistInfluencer()
        {
            UserIdentity registForm = new UserIdentity();
            return View(registForm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //UserIdentity identity = null;
            //try
            //{
            //    CommonBiz biz = new CommonBiz();
            //    string encryt_password = Settings.SHA256Hash(model.Password);
            //    identity = biz.LoginUser(model.Id, encryt_password);
            //    if (identity != null)
            //    {
            //        identity.SetTicket();
            //        return RedirectToAction("Index", "Home");
            //    }
            //}catch(Exception ex)
            //{
            //    Console.Write(ex.ToString());
            //}
            //return new JsonResult() { Data = new CallbackData { IsSuccess = false, ResultCode = -1, ExceptioMessage = GetSsoErrorMessage(-1) } };
            //return new JsonResult() { Data = new CallbackData { IsSuccess = true, ResultCode = resultCode } };
            return Content("");
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            string url = Url.Action("Index", "Home");

            return new RedirectResult(url);
        }
        public ActionResult InsertUser(UserIdentity user)
        {
            bool result = false;
            Int64 USER_SEQ = 0;
            //try
            //{
            //    CommonBiz biz = new CommonBiz();
            //    USER_SEQ = biz.InsertUser(user);
            //    result = true;
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex.ToString());

            //}
            return new JsonResult() { Data = new CallbackData(result) };
        }



        private string GetSsoErrorMessage(int errorNo)
        {
            switch (errorNo)
            {
                case -1:
                    return "아이디가 존재하지 않습니다. 다시 로그인 해주세요.";
                case -2:
                    return "비밀번호가 일치하지 않습니다. 다시 로그인 해주세요.";
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
                default:
                    return "인증이 실패하였습니다.(에러번호:" + errorNo + ") 다시 로그인 해주세요.";
            }
        }
    }
}