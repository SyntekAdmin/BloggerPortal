using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace F21.BLOGGER.Model
{
    public class InfUser
    {
        public int PAGE_NUM { get; set; }
        public int PAGE_SIZE { get; set; }
        public int CNT { get; set; }
        public int USER_SEQ { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_PASS { get; set; }
        public string ROLE { get; set; }
        public string STATUS_CD { get; set; }
        public string USE_CD { get; set; }
        public string FIRST_NM { get; set; }
        public string LAST_NM { get; set; }
        public string PHONE_NO { get; set; }
        public string INSTAGRAM_ID { get; set; }
        public string FOREVER21_ID { get; set; }
        public string PHOTO_URL { get; set; }
        public string ABOUT_ME { get; set; }
        public DateTime JOIN_DATE { get; set; }
        public DateTime APPLIED_DATE { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "not valid")]
        public int ADMIN_SEQ { get; set; }
        public string ADMIN_NAME { get; set; }
        public DateTime LAST_SIGN_IN { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public int CREATE_USER { get; set; }
        public int INFLUENCER { get; set; }        
        public string USER_NAME 
        {
            get
            {
                return FIRST_NM + " " + LAST_NM;
            }
        }

        public string ACTION_EMAIL { get; set; }   // 실제 행위자. 본인이 하는거면 본인, Admin이 하는거면 Admin 이메일
        public decimal BALANCE { get; set; }
        public int NEW_USER { get; set; }
        public string REJECT { get; set; }   // REJECT 사유

        public IList<InfAdmin> ADMINS { get; set; }

    }

    public class InfForever21User
    {
        public string ErrorMessage { get; set; }
        public string ErrorTitle { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string UserId { get; set; }
    }
}
