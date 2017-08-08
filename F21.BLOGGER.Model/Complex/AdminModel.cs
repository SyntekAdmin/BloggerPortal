using System;
using System.Collections.Generic;

namespace F21.BLOGGER.Model.Admin
{
    
    public class Point
    {
        public Int64 MYITEM_SEQ { get; set; }
        
        public string POST_LINK_DATE { get; set; }
        public string UPLOAD_DATE { get; set; }
        public string REMARK { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string EARNED { get; set; }
        public Int64 USER_SEQ { get; set; }
        public string USER_NAME { get; set; }
    }

    public class MyItem // InfluencersModel와 동일
    {
        public int PAGE_NUM { get; set; }
        public int PAGE_SIZE { get; set; }
        public int CNT { get; set; }
        //
        public Int64 MYITEM_SEQ { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDERED_DATE { get; set; }
        public string SHIPPED_DATE { get; set; }
        public string PRODUCT_NO { get; set; }
        public string SKU_NO { get; set; }
        public string APPROVED_DATE { get; set; }
        public string POST_LINK { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string ITEM_OPTIONS { get; set; }
        public string PRODUCT_IMAGE { get; set; }

        public string FIRST_UPLOAD { get; set; }
        public string LAST_UPLOAD { get; set; }
        public string PHOTOS { get; set; }
        public string EARNED { get; set; }
        public string PICKED { get; set; }

        public IList<Photo> PhotoList { get; set; }
    }

    public class Photo // InfluencersModel와 거의 동일
    {
        public Int64 PHOTO_SEQ { get; set; }
        public Int64 MYITEM_SEQ { get; set; }
        public string PHOTO_URL { get; set; }
        public string UPLOAD_DATE { get; set; }
        public string PICKED_DATE { get; set; }
        //public string EARNED { get; set; }

        public Int64 USER_SEQ { get; set; }
        public string FIRST_NM { get; set; }
        public string LAST_NM { get; set; }

    }
}
