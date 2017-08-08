using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model
{
    public class InfPointHistory
    {
        public int POINT_HISTORY_SEQ { get; set; }
        public int USER_SEQ { get; set; }
        public string REMARK { get; set; }
        public double AMOUNT { get; set; }
        public double BALANCE { get; set; }
        public DateTime PAID_DATE { get; set; }
        public int PHOTO_SEQ { get; set; }

    }
}
