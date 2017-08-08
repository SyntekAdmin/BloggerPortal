using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model
{
    public class InfAdmin
    {
        public int PAGE_NUM { get; set; }
        public int PAGE_SIZE { get; set; }
        public int CNT { get; set; }
        public int USER_SEQ { get; set; }
        public string USER_EMAIL { get; set; }
        public string STATUS_CD { get; set; }
        public string ROLE { get; set; }
        public string FIRST_NM { get; set; }
        public string LAST_NM { get; set; }
        public string PHOTO_URL { get; set; }
        public string JOIN_DATE { get; set; }
        public string APPLIED_DATE { get; set; }
        public string LAST_SIGN_IN { get; set; }
        public int INFLUENCERS { get; set; }
        public string USER_NAME
        {
            get
            {
                return FIRST_NM + " " + LAST_NM;
            }
        }
        public string ADMIN_NAME { get; set; }
        public int NEW_USER { get; set; }
        public Int64 BALANCE { get; set; }
    }
}
