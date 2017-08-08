using System.Collections.Generic;

namespace F21.BLOGGER.Model
{
    public class OrderHistory
    {
        public string ErrorMessage { get; set; }
        public string ErrorTitle { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string TotalRecords { get; set; }
        public string UserId { get; set; }
        
        public IList<OrderHistoryInfoList> OrderHistoryInfoList { get; set; }
    }
    public class OrderHistoryInfoList
    {
        public string IsAvailableCancel { get; set; }
        public string OrderDate { get; set; }
        public string OrderItemQty { get; set; }
        public string OrderNumber { get; set; }
        public string OrderStatusCode { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderTotal { get; set; }
        public string ShipmentCarrier { get; set; }
        public string ShipmentCountryCode { get; set; }
        public string ShipmentCountryName { get; set; }
        public string ShipmentTrackingNumber { get; set; }
        public string ShipmentTrackingUrl { get; set; }
        public string SelectItem { get; set; }
        public IList<OrderHistoryItemSummaryList> OrderHistoryItemSummaryList { get; set; }
    }

    public class OrderHistoryItemSummaryList
    {
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string DisplayName { get; set; }
        public string ExtendedPrice { get; set; }
        public string ImageFileSrc { get; set; }
        public string IsReturnable { get; set; }
        public string ItemColor { get; set; }
        public string ItemSize { get; set; }
        public string LineItemId { get; set; }
        public string ListPrice { get; set; }
        public string PlacedPrice { get; set; }
        public string ProductId { get; set; }
        public string PromoCode { get; set; }
        public string Quantity { get; set; }
        public string VariantId { get; set; }
        public string MyItem_Seq { get; set; }
        public bool IsChecked { get; set; }
    }
    public class OrderItems
    {
        public string MYITEM_SEQ { get; set; }
        public string PRODUCT_NO { get; set; }
        public string PRODUCT_NM { get; set; }
        public string PRODUCT_IMAGE { get; set; }
        public string SKU_NO { get; set; }
        public string SKU_NM { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_ITEM_SEQ { get; set; }
        public string ORDERED_DATE { get; set; }
        public string SHIPPED_DATE { get; set; }
        public string USER_SEQ { get; set; }
        public string CREATED_DATE { get; set; }
        public string POST_LINK { get; set; }
        public string POST_LINK_DATE { get; set; }        
    }
    public class MyOrders
    {
        public string ORDER_SEQ { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_STATUS { get; set; }
		public string TOTAL_PRICE { get; set; }
		public string ITEM_COUNT { get; set; }
        public string ORDERED_DATE { get; set; }
        public string SHIPPED_DATE { get; set; }

        public IList<MyOrderItems> Items { get; set; }
    }

    public class MyOrderItems
    {
        public string ORDER_ITEM_SEQ { get; set; }
        public string ORDER_NO { get; set; }
        public string PRODUCT_NO { get; set; }
        public string ITEM_OPTIONS { get; set; }
        public string ITEM_STATUS { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string SELLING_CD { get; set; }
        public string PRODUCT_IMAGE { get; set; }
    }

    public class MyItems
    {
        public string MYITEM_SEQ { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDERED_DATE { get; set; }
        public string SHIPPED_DATE { get; set; }
        public string PRODUCT_NO { get; set; }
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
    }

    public class Photos
    {
        public string PHOTO_SEQ { get; set; }
        public string MYITEM_SEQ { get; set; }
        public string PHOTO_URL { get; set; }
        public string UPLOAD_DATE { get; set; }
        public string PICKED_DATE { get; set; }
        public string EARNED { get; set; }
    }

    public class Points
    {
        public string MYITEM_SEQ { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string ITEM_OPTIONS { get; set; }
        public string PRODUCT_IMAGE { get; set; }
        public string ORDERED_DATE { get; set; }
        public string SHIPPED_DATE { get; set; }        
        public string UPLOAD_DATE { get; set; }
        public string PICKED { get; set; }
        public string EARNED { get; set; }
    }

    public class PointsHistory
    {
        public string POINT_HISTORY_SEQ { get; set; }
        public string REMARK { get; set; }
        public string AMOUNT { get; set; }
        public string BALANCE { get; set; }
        public string PAID_DATE { get; set; }
    }

    public class Faq
    {
        public string FAQ_SEQ { get; set; }
        public string TITLE { get; set; }
        public string REMARK { get; set; }
    }

}
