using F21.BLOGGER.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace F21.BLOGGER.Web.Models
{
    public class CustomPrincipal : UserIdentity, IPrincipal
    {
        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public CustomPrincipal(string Username, UserIdentity identity)
        {
            this.Identity = new GenericIdentity(Username);
            this.USER_SEQ = identity.USER_SEQ;
            this.USER_EMAIL = identity.USER_EMAIL;
            this.USER_PASS = identity.USER_PASS;
            this.ROLE = identity.ROLE;
            this.STATUS_CD = identity.STATUS_CD;
            this.USE_CD = identity.USE_CD;
            this.FIRST_NM = identity.FIRST_NM;
            this.LAST_NM = identity.LAST_NM;
            this.PHONE_NO = identity.PHONE_NO;
            this.INSTAGRAM_ID = identity.INSTAGRAM_ID;
            this.FOREVER21_ID = identity.FOREVER21_ID;
            this.PHOTO_URL = identity.PHOTO_URL;
            this.ABOUT_ME = identity.ABOUT_ME;
            this.GENDER = identity.GENDER;
            this.BIRTHDAY = identity.BIRTHDAY;
            this.PREFERENCE = identity.PREFERENCE;
            this.JOIN_DATE = identity.JOIN_DATE;
            this.APPLIED_DATE = identity.APPLIED_DATE;
            this.ADMIN_SEQ = identity.ADMIN_SEQ;
            this.LAST_SIGN_IN = identity.LAST_SIGN_IN;
            this.CREATE_DATE = identity.CREATE_DATE;
            this.CREATE_USER = identity.CREATE_USER;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}