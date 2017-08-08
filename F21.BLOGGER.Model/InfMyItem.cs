using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model
{
    public class InfMyItem
    {
        public int MYITEM_SEQ { get; set; }
        public int ORDER_ITEM_SEQ { get; set; }
        public int USER_SEQ { get; set; }
        public string ORDER_NO { get; set; }
        public string PRODUCT_NO { get; set; }
        public DateTime APPROVED_DATE { get; set; }
        public string POST_LINK { get; set; }
        public DateTime CREATED_DATE { get; set; }

    }
}
