using F21.BLOGGER.DataAccess;
using F21.BLOGGER.Model;
using Stis.Data;
using Stis.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Business
{
    [Logger]
    public class InfluencersBiz : BusinessFactory
    {
        public virtual IList<MyOrders> GetMyOrdersList(Int64 userSeq,int pageNum,int pageSize)
        {
            IList<MyOrders> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetMyOrdersList(userSeq, pageNum, pageSize);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public int GetMyOrdersListCnt(Int64 userSeq)
        {
            int list = 0;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetMyOrdersListCnt(userSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        
        public int SetMyItemByOrder(IList<OrderHistoryItemSummaryList> items, int USER_SEQ)
        {
            int bizResult = -1;
            
            try
            {
                this.TransactionScope(x =>
                {
                    InfluencersDao dao = new InfluencersDao();

                    items.ForEach(d => {
                        OrderItems item = new OrderItems
                        {
                            PRODUCT_NO = d.ProductId,
                            PRODUCT_NM = d.DisplayName,
                            PRODUCT_IMAGE = d.ImageFileSrc,
                            SKU_NO = d.VariantId,
                            SKU_NM = d.ItemColor+" "+d.ItemSize,
                            ORDER_NO = d.OrderNumber,
                            ORDER_ITEM_SEQ = d.LineItemId,
                            ORDERED_DATE = d.OrderDate,
                            SHIPPED_DATE = null,
                            USER_SEQ = Convert.ToString(USER_SEQ)
                        };
                        if (d.IsChecked && d.MyItem_Seq == "-1")
                        {
                            bizResult = dao.SetMyItemByOrder(item);
                        }
                        if (bizResult < 1)
                        {
                            throw new Exception("InsertMyItemByOrder Error");
                        }

                    });
                });

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }


        public virtual IList<MyOrders> GetMyOrdersByUserSeq(Int64 userSeq)
        {
            IList<MyOrders> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetMyOrdersByUserSeq(userSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public virtual MyOrders GetMyOrderDetailByOrderNo(string orderNo)
        {
            MyOrders data = null;
            try
            {
                var dao = new InfluencersDao();
                data = dao.GetMyOrderDetailByOrderNo(orderNo);
                data.Items = dao.GetMyOrderItemsByOrderNo(orderNo);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return data;
        }

        public virtual IList<MyOrderItems> GetMyOrderItemsByOrderNo(string orderNo)
        {
            IList<MyOrderItems> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetMyOrderItemsByOrderNo(orderNo);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public virtual IList<MyItems> GetMyItemsByUserSeq(Int64 userSeq, Int64 myItemSeq)
        {
            IList<MyItems> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetMyItemsByUserSeq(userSeq, myItemSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public virtual IList<Photos> GetPhtosByUserSeq(Int64 userSeq, Int64 myItemSeq)
        {
            IList<Photos> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetPhtosByUserSeq(userSeq, myItemSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public virtual IList<Points> GetPointsByUserSeq(Int64 userSeq,
            int PAGE_NUM,
            int PAGE_SIZE,
            string SORT_TP)
        {
            IList<Points> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetPointsByUserSeq(userSeq, PAGE_NUM, PAGE_SIZE, SORT_TP);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public virtual IList<PointsHistory> GetPointsHistoryByUserSeq(Int64 userSeq,
            int PAGE_NUM,
            int PAGE_SIZE,
            string SORT_TP)
        {
            IList<PointsHistory> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetPointsHistoryByUserSeq(userSeq, PAGE_NUM, PAGE_SIZE, SORT_TP);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public virtual OrderItems GetMyItemForOrderItem(string ORDER_NO, string PRODUCT_NO, string SKU_NO)
        {
            OrderItems item = null;
            try
            {
                var dao = new InfluencersDao();
                item = dao.GetMyItemForOrderItem(ORDER_NO, PRODUCT_NO, SKU_NO);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return item;
        }

        public virtual IList<Faq> GetFaq()
        {
            IList<Faq> list = null;
            try
            {
                var dao = new InfluencersDao();
                list = dao.GetFaq();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
    }
}
