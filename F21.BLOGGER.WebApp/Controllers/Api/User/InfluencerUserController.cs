#region Using

using System;
using System.Collections.Generic;
using System.Web.Http;
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
    public class InfluencerUserController : BaseApiController
    {
        #region 사용자정보 조회
        [HttpGet]
        [Route("api/user/{UserId}")]
        public InfUser GetUser(string UserId)
        {
            InfUser userIden = null;
            try
            {
                UserBiz biz = new UserBiz();
                userIden = biz.GetUserInfoByUserId(UserId);
            }
            catch (Exception ex)
            {
                userIden = null;
            }

            return userIden;
        }
        #endregion

        #region 사용자 대표 사진 변경
        // { "USER_EMAIL": "tiffany.k@forever21.com", "ACTION_EMAIL": "tiffany.k@forever21.com", "PHOTO_URL" : "https://www.forever21.com/images/default_330/00226118-02.jpg" }
        [Route("api/user/updatephoto")]
        public int UpdatePhoto([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_EMAIL = item.USER_EMAIL,
                ACTION_EMAIL = item.ACTION_EMAIL,
                PHOTO_URL = item.PHOTO_URL
            };

            UserBiz biz = new UserBiz();
            return biz.UpdatePhoto(user);
        }
        #endregion

        #region 사용자 수정
       
        [HttpPost]
        [Route("api/user/updateuser")]
        public int Update([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_SEQ = item.USER_SEQ,
                USER_EMAIL = item.USER_EMAIL,
                USER_PASS = Settings.SHA256Hash(item.USER_PASS),
                FIRST_NM = item.FIRST_NM,
                LAST_NM = item.LAST_NM,
                PHOTO_URL = item.PHOTO_URL,
                PHONE_NO = item.PHONE_NO,
                INSTAGRAM_ID = item.INSTAGRAM_ID,
                ABOUT_ME = item.ABOUT_ME
            };

            UserBiz biz = new UserBiz();
            return biz.UpdateUser(user);
        }
        #endregion
    }
}
