using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model
{
    public class InfInfluencer 
    {
        public InfUser INF_USER { get; set; }
        public decimal BALANCE { get; set; }
        public DateTime LAST_ORDER_DATE { get; set; }
        public int LAST_ORDER_ITEMS { get; set; }
        public DateTime LAST_UPLOAD_DATE { get; set; }
        public int LAST_UPLOAD_ITEMS { get; set; }
    }
}
