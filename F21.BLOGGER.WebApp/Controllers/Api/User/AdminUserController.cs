#region Using

using System;
using System.Globalization;
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
    public class AdminUserController : BaseApiController
    {
        #region Admin 전체 리스트
        [HttpGet]
        [Route("api/admin/adminscnt")]
        public int GetAdminAllCnt()
        {
            AdminUserBiz biz = new AdminUserBiz();
            return biz.GetAdminAllCnt();
        }

        [HttpGet]
        [Route("api/admin/admins/{sort}/{pageNum}/{pageSize}")]
        public IList<InfAdmin> GetAdminAll(string sort, int pageNum, int pageSize)
        {
            IList<InfAdmin> adminIden = null;
            try
            {
                AdminUserBiz biz = new AdminUserBiz();
                adminIden = biz.GetAdminAll(sort, pageNum, pageSize);
            }
            catch (Exception ex)
            {
                adminIden = null;
            }

            return adminIden;
        }

        #region 활동중인 Admin 리스트
        [HttpGet]
        [Route("api/admin/activeadmins")]
        public IList<InfAdmin> GetAdminAllActive()
        {
            IList<InfAdmin> adminIden = null;
            try
            {
                AdminUserBiz biz = new AdminUserBiz();
                adminIden = biz.GetAdminAllActive();
            }
            catch (Exception ex)
            {
                adminIden = null;
            }

            return adminIden;
        }
        #endregion

        #endregion

        #region Influencer 전체 리스트
        [HttpGet]
        [Route("api/admin/influencerscnt/{adminSeq}")]
        public int GetInfluencerAllCnt(int adminSeq)
        {
            AdminUserBiz biz = new AdminUserBiz();
            return biz.GetInfluencerAllCnt(adminSeq);
        }

        [HttpGet]
        [Route("api/admin/influencers/{sort}/{adminSeq}/{pageNum}/{pageSize}")]
        public IList<InfUser> GetInfluencerAll(string sort, int adminSeq, int pageNum, int pageSize)
        {
            
            IList<InfUser> userIden = null;
            IList<InfAdmin> adminIden = null;
            try
            {
                AdminUserBiz biz = new AdminUserBiz();
                userIden = biz.GetInfluencerAll(sort, adminSeq, pageNum, pageSize);

                // 모든 Admin 가져오기
                adminIden = biz.GetAdminAllActive();

                foreach (var t in userIden)
                {
                    t.ADMINS = adminIden;
                }

            }
            catch (Exception ex)
            {
                userIden = null;
            }

            return userIden;
        }

        [HttpGet]
        [Route("api/admin/nomatchInfluencers")]
        public IList<InfUser> GetNomatchInfluencerAll()
        {

            IList<InfUser> userIden = null;
            try
            {
                AdminUserBiz biz = new AdminUserBiz();
                userIden = biz.GetNomatchInfluencerAll();
            }
            catch (Exception ex)
            {
                userIden = null;
            }

            return userIden;
        }
        
        #endregion

        #region Influencer 상세
        [HttpGet]
        [Route("api/admin/influencer/{UserId}")]
        public InfInfluencer GetInfluencer(string UserId)
        {
            InfInfluencer influencerIden = null;
            try
            {
                AdminUserBiz biz = new AdminUserBiz();
                influencerIden = biz.GetInfluencerInfoByUserId(UserId);
            }
            catch (Exception ex)
            {
                influencerIden = null;
            }

            return influencerIden;
        }

        #region Influencer 삭제
        
        [HttpPost]
        [Route("api/admin/deleteinfluencer")]
        public int DeleteInfluencer([FromBody]InfUser item)
        {
            string actionEmail = Identity.USER_EMAIL;
            InfUser user = new InfUser
            {
                USER_EMAIL = item.USER_EMAIL,
                ACTION_EMAIL = actionEmail,
                STATUS_CD = "Delete"
            };

            UserBiz biz = new UserBiz();
            return biz.UpdateStatus(user);
        }
        #endregion

        #region Influencer Reject

        [HttpPost]
        [Route("api/admin/updatestatusinfluencer")]
        public int RejectInfluencer([FromBody]InfUser item)
        {
            string actionEmail = Identity.USER_EMAIL;
            InfUser user = new InfUser
            {
                USER_EMAIL = item.USER_EMAIL,
                ACTION_EMAIL = actionEmail,
                STATUS_CD = item.STATUS_CD,
                REJECT = item.REJECT
            };

            UserBiz biz = new UserBiz();
            return biz.UpdateStatus(user);
        }
        #endregion

        #endregion

        #region Influencer 등록
        [HttpPost]
        [Route("api/admin/regist")]
        public Int64 Regist([FromBody]InfUser item)
        {
            Int64 returnValue = -1;

            string userCD = "Inactive";
            if (item.STATUS_CD == "Accept")
            {userCD = "Active"; }
            else if (item.STATUS_CD == "Regist")
            { userCD = "Inactive"; }

            InfUser user = new InfUser
            {
                USER_SEQ = 0,
                USER_EMAIL = item.USER_EMAIL,
                USER_PASS = Settings.SHA256Hash("ssdfds923423sdfd"),
                ROLE = "User",
                STATUS_CD = item.STATUS_CD,
                USE_CD = userCD,
                FIRST_NM = item.FIRST_NM,
                LAST_NM = item.LAST_NM,
                PHONE_NO = item.PHONE_NO,
                INSTAGRAM_ID = item.INSTAGRAM_ID,
                FOREVER21_ID = item.FOREVER21_ID,
                PHOTO_URL = "/resource/images/profile_smiley.png",
                ADMIN_SEQ = item.ADMIN_SEQ,
                CREATE_USER = item.CREATE_USER
            };

            UserBiz biz = new UserBiz();
            returnValue = biz.InsertUser(user);
            if (returnValue > 0)
            {
                // 사용자 등록이 성공 하면 회원가입 성공 및 비밀번호 설정 하라는 메일을 전송 해야함
            }
            return returnValue;
        }
        #endregion
        
        #region Admin 상세
        [HttpGet]
        [Route("api/admin/admins/{userSeq}")]
        public InfAdmin GetAdmin(string userSeq)
        {
            InfAdmin adminIden = null;
            try
            {
                AdminUserBiz biz = new AdminUserBiz();
                adminIden = biz.GetAdmin(Int64.Parse(userSeq));
            }
            catch (Exception ex)
            {
                adminIden = null;
            }

            return adminIden;
        }

        #region Influencer에 Admin 할당 및 해제
        // [ {"USER_EMAIL": "lillian.l@forever21.com", "ADMIN_SEQ" : 2} ,  {"USER_EMAIL": "kyle.m@forever21.com", "ADMIN_SEQ" : 2}] 
        [Route("api/admin/updateinfluenceradmin")]
        public int UpdateInfluencerAdmin([FromBody]IList<InfUser> item)
        {
            string ACTION_EMAIL = Identity.USER_EMAIL;
            AdminUserBiz biz = new AdminUserBiz();
            return biz.UpdateInfluencerAdmin(item, ACTION_EMAIL);
        }

        [Route("api/admin/releaseinfluenceradmin")]
        public int ReleaseInfluencerAdmin([FromBody]InfUser item)
        {
            AdminUserBiz biz = new AdminUserBiz();
            item.ACTION_EMAIL = Identity.USER_EMAIL;
            item.ADMIN_SEQ = -1;
            return biz.ReleaseInfluencerAdmin(item);
        }
        #endregion

        #endregion

        #region Influencer Assign 수정
        [Route("api/admin/updateAssignInfluencer")]
        public Int64 UpdateAssignInfluencer([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_SEQ = item.USER_SEQ,
                STATUS_CD = item.STATUS_CD,
                ADMIN_SEQ = item.ADMIN_SEQ,
                CREATE_USER = Identity.USER_SEQ
            };

            AdminUserBiz biz = new AdminUserBiz();
            return biz.UpdateAssignInfluencer(user);
        }

        #endregion

        #region Admin 등록, 수정
        [Route("api/admin/registadmin")]
        public Int64 RegistAdmin([FromBody]InfUser item)
        {
            Int64 returnValue = -1;

            InfUser user = new InfUser
            {
                USER_SEQ = 0,
                USER_EMAIL = item.USER_EMAIL,
                //USER_PASS = Settings.SHA256Hash(item.USER_PASS),
                USER_PASS = Settings.SHA256Hash("ssdfds923423sdfd"),
                ROLE = "Admin",
                STATUS_CD = "Accept",
                USE_CD = "Active",
                FIRST_NM = item.FIRST_NM,
                LAST_NM = item.LAST_NM,
                //PHOTO_URL = item.PHOTO_URL,
                PHOTO_URL = null,
                CREATE_USER = Identity.USER_SEQ,
                INFLUENCER = item.INFLUENCER
            };

            AdminUserBiz biz = new AdminUserBiz();
            returnValue = biz.InsertAdmin(user);
            if (returnValue > 0)
            {
                // 사용자 등록이 성공 하면 회원가입 성공 및 비밀번호 설정 하라는 메일을 전송 해야함
            }
            return returnValue;
        }

        [HttpPost]
        [Route("api/admin/updateadmin")]
        public int Update([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_SEQ = item.USER_SEQ,
                USER_EMAIL = item.USER_EMAIL,
                USER_PASS = Settings.SHA256Hash(item.USER_PASS),
                FIRST_NM = item.FIRST_NM,
                LAST_NM = item.LAST_NM,
                PHOTO_URL = item.PHOTO_URL
            };

            AdminUserBiz biz = new AdminUserBiz();
            return biz.UpdateAdmin(user);
        }
        [HttpPost]
        [Route("api/admin/updateadminuphoto")]
        public int UpdatePhoto([FromBody]InfUser item)
        {
            InfUser user = new InfUser
            {
                USER_SEQ = item.USER_SEQ,
                PHOTO_URL = item.PHOTO_URL
            };

            AdminUserBiz biz = new AdminUserBiz();
            return biz.UpdateAdminPhoto(user);
        }

        [HttpPost]
        [Route("api/admin/deleteadmin")]
        public int Delete([FromBody]InfUser item)
        {
            string actionEmail = Identity.USER_EMAIL;

            InfUser user = new InfUser
            {
                USER_EMAIL = item.USER_EMAIL,
                ACTION_EMAIL = actionEmail,
                STATUS_CD = "Delete"
            };

            AdminUserBiz biz = new AdminUserBiz();
            return biz.DeleteUser(user);
        }

        #endregion
    }
}
