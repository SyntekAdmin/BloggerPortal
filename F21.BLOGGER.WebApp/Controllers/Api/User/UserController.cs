#region Using

using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using F21.BLOGGER.WebApp.Models;
using F21.BLOGGER.Business;
using F21.BLOGGER.WebApp.Common;
using F21.BLOGGER.Model;

#endregion

namespace F21.BLOGGER.WebApp.Controllers.Api
{
    // UserController : 인증 되지 않은 사용자 API
    // InfluencerUserController : Influencer 사용자 API
    // AdminUserController : Admin 사용자 API

    public class UserController : ApiController
    {
        private NetworkFunctions nf = new NetworkFunctions();

        #region 사용자정보 등록
        [HttpPost]
        [Route("api/account/regist")]
        public Int64 Regist([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_SEQ = 0,
                USER_EMAIL = item.USER_EMAIL,
                USER_PASS = Settings.SHA256Hash(item.USER_PASS),
                ROLE = "User",
                STATUS_CD = "Regist",
                USE_CD = "Inactive",
                FIRST_NM = item.FIRST_NM,
                LAST_NM = item.LAST_NM,
                PHONE_NO = item.PHONE_NO,
                INSTAGRAM_ID = item.INSTAGRAM_ID,
                FOREVER21_ID = item.FOREVER21_ID,
                PHOTO_URL = item.PHOTO_URL,
                ABOUT_ME = item.ABOUT_ME,
                // GENDER = null,
                // BIRTHDAY = null,
                // PREFERENCE = null,
                // JOIN_DATE = now,
                // APPLIED_DATE = null,
                // ADMIN_SEQ = null,
                // LAST_SIGN_IN = now,
                // CREATE_DATE = now,
                CREATE_USER = -1
            };

            UserBiz biz = new UserBiz();
            return  biz.InsertUser(user);
        }
        #endregion

        #region 사용자의 비밀번호 변경 요청
        [HttpPost]
        [Route("api/account/forgotpassword")]
        public int ForgotPassword([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_EMAIL = item.USER_EMAIL,
                ACTION_EMAIL = item.USER_EMAIL,
                STATUS_CD = "Inactivate"
            };

            UserBiz biz = new UserBiz();
            return biz.UpdateStatus(user);
        }
        #endregion

        #region 사용자 비밀번호 변경
        // { "USER_EMAIL": "tiffany.k@forever21.com", "USER_PASS" : "12345678" }
        [Route("api/account/resetpassword")]
        public int ResetPassword([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_EMAIL = item.USER_EMAIL,
                ACTION_EMAIL = item.USER_EMAIL,    
                USER_PASS = Settings.SHA256Hash(item.USER_PASS)
            };

            UserBiz biz = new UserBiz();
            return biz.UpdatePasswordAndStatus(user);
        }
        #endregion

        #region 이미 회원 DB에 있는 사용자인지 확인 후 forever21 아이디 가져오기
        [HttpGet]
        [Route("api/account/getforever21id")]
        public InfForever21User GetForever21Id(string email)
        {
            int user_cnt = -1;
            string msg = "";
            string returnCode = "";

            InfForever21User list = null;
            InfForever21User forever21id = new InfForever21User() ;

            UserBiz biz = new UserBiz();
            user_cnt = biz.GetUserCountByUserId(email);

            // email이 시스템에 존재
            if (user_cnt > 0)
            {
                forever21id.ReturnMessage = "Id exist in Influencer Blog System";
                forever21id.ReturnCode = "01";
            }
            else {

                string url = "/f21api/UserService.svc/{0}/blog/userprofile/?email={1}";
                url = string.Format(url, F21.BLOGGER.WebApp.Common.Settings.API_Version, email);

                msg = nf.GetWebResponseData(url, NetworkFunctions.CallType.GET);

                list = JsonConvert.DeserializeObject<InfForever21User>(msg);

                returnCode = (list.ReturnCode);

                if (returnCode == "00")
                {
                    forever21id.UserId = list.UserId;
                    forever21id.ReturnCode = "00";
                 }
                else
                {
                    forever21id.ReturnMessage = "Couldn't find your Global Forever 21 Account. Please create new account.";
                    forever21id.ReturnCode = "02";
                }
            }
            return forever21id;
        }
        #endregion

    }
}
