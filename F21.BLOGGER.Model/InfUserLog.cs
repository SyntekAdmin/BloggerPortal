using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model
{
    public class InfUserLog
    {
        public int USER_LOG_SEQ { get; set; }
        public int USER_SEQ { get; set; }
        public int TARGET_SEQ { get; set; }
        public string REASON { get; set; }
        public string ACTION { get; set; }
        public string REMARK { get; set; }
        public DateTime CREATE_DATE { get; set; }

    }
}
