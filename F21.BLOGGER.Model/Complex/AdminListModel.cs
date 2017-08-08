using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model.Admin
{
    #region Common
    public static class Common
    {
        public static string LongDate(string s)
        {
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(s, out dt))
            {
                return dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            return s;
        }

        public static string ShortDate(string s)
        {
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(s, out dt))
            {
                return dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            return s;
        }

        public static string LongDate(DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string ShortDate(DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        public static string MoneyFormat(string s)
        {
            return string.Format("{0:C2}", s); // $12,345.00
        }
    }
    #endregion
    public class AdminItemsListModel
    {
        public int CNT { get; set; }
        //
        public string PRODUCT_NO { get; set; }
        public string PRODUCT_NM { get; set; }
        public string SKU_NO { get; set; }
        public string SKU_NM { get; set; }
        public string PRODUCT_IMAGE { get; set; }
        public int ADMIN_SEQ { get; set; }
        public string ADMIN_NM { get; set; }
        public string ADMIN_PHOTO_URL { get; set; }
        public int INF_CNT { get; set; }
        public DateTime FIRST_UPLOAD { get; set; }
        public DateTime LAST_UPLOAD { get; set; }
        public int PHOTO_CNT { get; set; }
        public string PICKED_USER_SEQ { get; set; }
        public string PICKED_USER_NM { get; set; }
        public string PICKED_STATUS { get; set; }
        public DateTime PICKED_DATE { get; set; }
        public DateTime ORDERED_DATE { get; set; }
        public DateTime SHIPPED_DATE { get; set; }
        public DateTime RETOUCHED_DATE { get; set; }

        public string FIRST_UPLOAD_STR
        {
            get { return (FIRST_UPLOAD == null || Common.ShortDate(FIRST_UPLOAD) == "01/01/0001") ? "TBD" : Common.ShortDate(FIRST_UPLOAD); }
        }
        public string LAST_UPLOAD_STR
        {
            get { return (LAST_UPLOAD == null || Common.ShortDate(LAST_UPLOAD) == "01/01/0001") ? "TBD" : Common.ShortDate(LAST_UPLOAD); }
        }
        public string PICKED_DATE_STR
        {
            get { return (PICKED_DATE == null || Common.ShortDate(PICKED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(PICKED_DATE); }
        }
        public string ORDERED_DATE_STR
        {
            get { return (ORDERED_DATE == null || Common.ShortDate(ORDERED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(ORDERED_DATE); }
        }
        public string SHIPPED_DATE_STR
        {
            get { return (SHIPPED_DATE == null || Common.ShortDate(SHIPPED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(SHIPPED_DATE); }
        }
        public string RETOUCHED_DATE_STR
        {
            get { return (RETOUCHED_DATE == null || Common.ShortDate(RETOUCHED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(RETOUCHED_DATE); }
        }
        public IList<AdminPhotoListModel> Photos { get; set; }
    }
    public class AdminMyitemUserListModel
    {
        public int CNT { get; set; }
        public string PRODUCT_NO { get; set; }
        public string SKU_NO { get; set; }
        public int ADMIN_SEQ { get; set; }
        public string ADMIN_PHOTO_URL { get; set; }
        //
        public int USER_SEQ { get; set; }
        public string USER_NM { get; set; }
        public string INSTAGRAM_ID { get; set; }
        public DateTime UPLOAD_DATE { get; set; }
        public int PHOTO_CNT { get; set; }
        public DateTime NOMINATED_DATE { get; set; }
        public string UPLOAD_DATE_STR
        {
            get { return (UPLOAD_DATE == null || Common.ShortDate(UPLOAD_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(UPLOAD_DATE); }
        }
        public string NOMINATED_DATE_STR
        {
            get { return (NOMINATED_DATE == null || Common.ShortDate(NOMINATED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(NOMINATED_DATE); }
        }
        public string NOMINATED_STATUS
        {
            get { return (NOMINATED_DATE == null || Common.ShortDate(NOMINATED_DATE) == "01/01/0001") ? null : "Nominated"; }
        }
        //
        public IList<AdminPhotoListModel> Photos { get; set; }
    }

    public class AdminMyitemUserListViewModel
    {
        public int USER_SEQ { get; set; }
        public string INSTAGRAM_ID { get; set; }
        public string USER_NM { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public DateTime SHIPPED_DATE { get; set; }
        public DateTime UPLOAD_DATE { get; set; }
        public int PHOTO_CNT { get; set; }
        public string POST_LINK { get; set; }
        public DateTime POST_LINK_DATE { get; set; }
        public string PICKED_PHOTO_URL { get; set; }
        public string PICKED_UPLOAD_DATE { get; set; }
        public string PICKED_STATUS { get; set; }
        public int PICKED_PHOTO_SEQ { get; set; }
        public DateTime PICKED_DATE { get; set; }
        public int PICKED_USER_SEQ { get; set; }
        public DateTime PICKED_RETOUCHED_DATE { get; set; }
        public string ORDER_DATE_STR
        {
            get { return (ORDER_DATE == null || Common.ShortDate(ORDER_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(ORDER_DATE); }
        }
        public string SHIPPED_DATE_STR
        {
            get { return (SHIPPED_DATE == null || Common.ShortDate(SHIPPED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(SHIPPED_DATE); }
        }
        public string UPLOAD_DATE_STR
        {
            get { return (UPLOAD_DATE == null || Common.ShortDate(UPLOAD_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(UPLOAD_DATE); }
        }
        public string POST_LINK_DATE_STR
        {
            get { return (POST_LINK_DATE == null || Common.ShortDate(POST_LINK_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(POST_LINK_DATE); }
        }
        public string PICKED_DATE_STR
        {
            get { return (PICKED_DATE == null || Common.ShortDate(PICKED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(PICKED_DATE); }
        }
        public string PICKED_UPLOAD_DATE_STR
        {
            get { return (PICKED_UPLOAD_DATE == null || Common.ShortDate(PICKED_UPLOAD_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(PICKED_UPLOAD_DATE); }
        }
        public string PICKED_RETOUCHED_DATE_STR
        {
            get { return (PICKED_RETOUCHED_DATE == null || Common.ShortDate(PICKED_RETOUCHED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(PICKED_RETOUCHED_DATE); }
        }
        public string RETOUCHED_STATUS
        {
            get { return (PICKED_RETOUCHED_DATE == null || Common.ShortDate(PICKED_RETOUCHED_DATE) == "01/01/0001") ? "" : "Retouched"; }
        }
        public IList<AdminPhotoListModel> Photos { get; set; }
    }
    
    public class AdminPhotoListModel
    {
        public int PHOTO_SEQ { get; set; }
        public int MYITEM_SEQ { get; set; }
        public string PHOTO_URL { get; set; }
        public string PICKED_STATUS { get; set; }
        public string PICKED_USER_SEQ { get; set; }
        public DateTime UPLOAD_DATE { get; set; }
        public DateTime PICKED_DATE { get; set; }
        public DateTime RETOUCHED_DATE { get; set; }
        public int USER_SEQ { get; set; }
        public string UPLOAD_DATE_STR
        {
            get { return (UPLOAD_DATE == null || Common.ShortDate(UPLOAD_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(UPLOAD_DATE); }
        }
        public string PICKED_DATE_STR
        {
            get { return (PICKED_DATE == null || Common.ShortDate(PICKED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(PICKED_DATE); }
        }
        public string RETOUCHED_DATE_STR
        {
            get { return (RETOUCHED_DATE == null || Common.ShortDate(RETOUCHED_DATE) == "01/01/0001") ? "TBD" : Common.ShortDate(RETOUCHED_DATE); }
        }
    }

    
}
