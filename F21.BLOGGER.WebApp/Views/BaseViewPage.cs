using System.Web.Mvc;
using F21.BLOGGER.Model;
using System;
using System.Globalization;
using F21.BLOGGER.Business;

namespace F21.BLOGGER.WebApp
{
    public abstract class BaseViewPage<T> : WebViewPage<T>
    {
        public string LoginUserEmail
        {
            get { return ((InfUser)User).USER_EMAIL.Trim().ToLower() as string; }
        }

        public string LoginUserRole
        {
            get { return ((InfUser)User).ROLE.Trim().ToLower() as string; }
        }

        public int LoginUserSeq
        {
            get { return ((InfUser)User).USER_SEQ ; }
        }

        public string LoginUserFirstName
        {
            get
            {
                AdminUserBiz biz = new AdminUserBiz();
                return biz.GetAdmin(LoginUserSeq).FIRST_NM;
            }
        }

        public string LoginUserPhotoUrl
        {
            get
            {
                AdminUserBiz biz = new AdminUserBiz();
                return biz.GetAdmin(LoginUserSeq).PHOTO_URL;
            }
        }

        public string LoginUserLastSignIn
        {
            get { return ((InfUser)User).LAST_SIGN_IN.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture); }
        }

        #region 메인(SuperAdmin)
        public int SuperAdminCurrentInfluencers
        {
            get
            {
                AdminBiz adminBiz = new AdminBiz();
                return adminBiz.GetCurrentInfluencers();
            }
        }
        public int SuperAdminNewInfluencers
        {
            get
            {
                AdminBiz adminBiz = new AdminBiz();
                return adminBiz.GetCurrentInfluencers();
            }
        }
        public int SuperAdminNewPhotos
        {
            get
            {
                AdminBiz adminBiz = new AdminBiz();
                return adminBiz.GetNewPhotos();
            }
        }
        #endregion

        #region 메인(Admin)
        public int AdminMyInfluencers
        {
            get
            {
                AdminBiz adminBiz = new AdminBiz();
                return adminBiz.GetMyInfluencers(LoginUserSeq);
            }
        }
        public int AdminNewPhotos
        {
            get
            {
                AdminBiz adminBiz = new AdminBiz();
                return adminBiz.GetAdminNewPhotos(LoginUserSeq);
            }
        }
        public string AdminCreateUser
        {
            get
            {
                AdminBiz adminBiz = new AdminBiz();
                return adminBiz.GetAdminCreateUser(LoginUserSeq);
            }
        }
        #endregion

        public string fnGetQuickLayout()
        {
            string rtn = "";
            string strUrl = Request.Url.AbsolutePath.ToUpper();
            if (strUrl.IndexOf("/ADMIN/PROFILE") == 0 || strUrl.IndexOf("/ACCOUNT/PROFILE") == 0)
            {
                rtn = "_Quick_UserPhoto";
            }
            else if (strUrl.IndexOf("/ADMIN/ITEMS/VIEWALL") == 0 || strUrl.IndexOf("/ADMIN/ITEMS/VIEW") == 0 || strUrl.IndexOf("/ADMIN/ITEMS") == 0)
            {
                rtn = "_Quick_Items";
            }
            else if (strUrl.IndexOf("/ADMIN/MEMBERS") == 0)
            {
                rtn = "_Quick_TeamMembers";
            }
            else if (strUrl.IndexOf("/ADMIN/INFLUENCERS") == 0 || strUrl.IndexOf("/ADMIN/MYINFLUENCERS") == 0)
            {
                rtn = "_Quick_Influencers";
            }
            else if (strUrl.IndexOf("/ADMIN") == 0)
            {
                rtn = "_Quick_UserPhoto";
            }
            else
            {
                rtn = "_Quick_Default";
            }
            //
            return rtn;
        }
    }
}