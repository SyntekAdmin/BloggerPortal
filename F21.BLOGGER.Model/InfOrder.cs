using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model
{
    public class InfOrder
    {
        public int ORDER_SEQ { get; set; }
        public int USER_SEQ { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_STATUS { get; set; }
        public decimal TOTAL_PRICE { get; set; }
        public int ITEM_COUNT { get; set; }
        public DateTime ORDERED_DATE { get; set; }
        public DateTime SHIPPED_DATE { get; set; }

    }
}
