using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stis.Data;
using System.Data.SqlClient;
using System.Data;
using Stis.Data.DapperNet;
using F21.BLOGGER.Model;
using F21.BLOGGER.Model.Admin;
using Dapper;

namespace F21.BLOGGER.DataAccess
{
    public class InfluencersDao : DapperFactory
    {
        public InfluencersDao() : base("INFLUENCER")
        { }
        
        public virtual IList<MyOrders> GetMyOrdersList(Int64 userSeq,int pageNum, int pageSize)
        {
            IList<MyOrders> list = null;
            try
            {
                var x = QuerySP<MyOrders>("SP_INF_ORDER_Q", new
                {
                    USER_SEQ = userSeq,
                    PAGE_NUM = pageNum,
                    PAGE_SIZE = pageSize,
                });

                list = x;

                foreach (var t in list)
                {

                    t.TOTAL_PRICE = Common.MoneyFormat(t.TOTAL_PRICE);
                    t.ORDERED_DATE = Common.ShortDate(t.ORDERED_DATE);
                    t.SHIPPED_DATE = Common.ShortDate(t.SHIPPED_DATE);
                }

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
                var x = QuerySP<AdminItemsListModel>("SP_INF_ORDER_CNT_Q", new
                {
                    USER_SEQ = userSeq
                });

                list = x[0].CNT;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        
        public Int32 SetMyItemByOrder(OrderItems items)
        {
            return Execute<Int32>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PRODUCT_NO", items.PRODUCT_NO, DbType.String, ParameterDirection.Input);
                param.Add("@PRODUCT_NM", items.PRODUCT_NM, DbType.String, ParameterDirection.Input);
                param.Add("@PRODUCT_IMAGE", items.PRODUCT_IMAGE, DbType.String, ParameterDirection.Input);
                param.Add("@SKU_NO", items.SKU_NO, DbType.String, ParameterDirection.Input);
                param.Add("@SKU_NM", items.SKU_NM, DbType.String, ParameterDirection.Input);
                param.Add("@ORDER_NO", items.ORDER_NO, DbType.String, ParameterDirection.Input);
                param.Add("@ORDER_ITEM_SEQ", Convert.ToInt64(items.ORDER_ITEM_SEQ), DbType.Int64, ParameterDirection.Input);
                param.Add("@ORDER_DATE", Convert.ToDateTime(items.ORDERED_DATE), DbType.DateTime, ParameterDirection.Input);
                param.Add("@SHIPPED_DATE", Convert.ToDateTime(items.SHIPPED_DATE), DbType.DateTime, ParameterDirection.Input);
                param.Add("@USER_SEQ", Convert.ToInt64(items.USER_SEQ), DbType.Int64, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_MYITEM_I", param, commandType: CommandType.StoredProcedure);

                return param.Get<Int32>("@RowsAffected");
            });
        }

        /// <summary>
        /// Influencers - My Orders
        /// </summary>
        /// <param name="userSeq"></param>
        /// <returns></returns>
        public virtual IList<MyOrders> GetMyOrdersByUserSeq(Int64 userSeq)
        {
            IList<MyOrders> list = null;
            try
            {
                var x = QuerySP<MyOrders>("SP_INF_ORDER_Q", new
                {
                    USER_SEQ = userSeq,
                });

                list = x;
                
                foreach(var t in list)
                {

                    t.TOTAL_PRICE = Common.MoneyFormat(t.TOTAL_PRICE); 
                    t.ORDERED_DATE = Common.ShortDate(t.ORDERED_DATE);
                    t.SHIPPED_DATE = Common.ShortDate(t.SHIPPED_DATE);
                }
                    
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        /// <summary>
        /// Influencers - My Order Detail
        /// </summary>
        /// <param name="userSeq"></param>
        /// <returns></returns>
        public virtual MyOrders GetMyOrderDetailByOrderNo(string orderNo)
        {
            MyOrders data = null;
            try
            {
                var x = QuerySP<MyOrders>("SP_INF_ORDER_DETAIL_Q", new
                {
                    ORDER_NO = orderNo,
                });

                data = x.FirstOrDefault();
                if (data != null)
                {
                    data.TOTAL_PRICE = Common.MoneyFormat(data.TOTAL_PRICE);
                    data.ORDERED_DATE = Common.ShortDate(data.ORDERED_DATE);
                    data.SHIPPED_DATE = Common.ShortDate(data.SHIPPED_DATE);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return data;
        }

        /// <summary>
        /// Influencers - My Orders
        /// </summary>
        /// <param name="userSeq"></param>
        /// <returns></returns>
        public virtual IList<MyOrderItems> GetMyOrderItemsByOrderNo(string orderNo)
        {
            IList<MyOrderItems> list = null;
            try
            {
                var x = QuerySP<MyOrderItems>("SP_INF_ORDER_ITEM_Q", new
                {
                    ORDER_NO = orderNo,
                });

                list = x;
            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        /// <summary>
        /// Influencers - My Items
        /// </summary>
        /// <param name="userSeq"></param>
        /// <param name="myItemSeq"></param>
        /// <returns></returns>
        public virtual IList<MyItems> GetMyItemsByUserSeq(Int64 userSeq, Int64 myItemSeq)
        {
            IList<MyItems> list = null;
            try
            {
                var x = QuerySP<MyItems>("SP_INF_MYITEM_Q", new
                {
                    USER_SEQ = userSeq,
                    MYITEM_SEQ = myItemSeq
                });

                list = x;

                foreach (var t in list)
                {
                    t.ORDERED_DATE = Common.ShortDate(t.ORDERED_DATE);
                    t.SHIPPED_DATE = Common.ShortDate(t.SHIPPED_DATE);
                    t.APPROVED_DATE = Common.ShortDate(t.APPROVED_DATE);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        /// <summary>
        /// Influencers - Photos
        /// </summary>
        /// <param name="userSeq"></param>
        /// <param name="myItemSeq"></param>
        /// <returns></returns>
        public virtual IList<Photos> GetPhtosByUserSeq(Int64 userSeq, Int64 myItemSeq)
        {
            IList<Photos> list = null;
            try
            {
                var x = QuerySP<Photos>("SP_INF_PHOTO_Q", new
                {
                    USER_SEQ = userSeq,
                    MYITEM_SEQ = myItemSeq
                });

                list = x;

                foreach (var t in list)
                { 
                    t.UPLOAD_DATE = Common.ShortDate(t.UPLOAD_DATE);
                    t.PICKED_DATE = Common.ShortDate(t.PICKED_DATE);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        /// <summary>
        /// Influencers - Points
        /// </summary>
        /// <param name="userSeq"></param>
        /// <returns></returns>
        public virtual IList<Points> GetPointsByUserSeq(
            Int64 userSeq,
            int PAGE_NUM,
            int PAGE_SIZE,
            string SORT_TP
            )
        {
            IList<Points> list = null;
            try
            {
                var x = QuerySP<Points>("SP_INF_POINT_Q", new
                {
                    USER_SEQ = userSeq,
                    PAGE_NUM = PAGE_NUM,
                    PAGE_SIZE = PAGE_SIZE,
                    SORT_TP = SORT_TP
                });

                list = x;

                foreach (var t in list)
                {
                    t.ORDERED_DATE = Common.ShortDate(t.ORDERED_DATE);
                    t.SHIPPED_DATE = Common.ShortDate(t.SHIPPED_DATE);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        /// <summary>
        /// Influencers - PointsHistory
        /// </summary>
        /// <param name="userSeq"></param>
        /// <returns></returns>
        public virtual IList<PointsHistory> GetPointsHistoryByUserSeq(Int64 userSeq,
            int PAGE_NUM,
            int PAGE_SIZE,
            string SORT_TP)
        {
            IList<PointsHistory> list = null;
            try
            {
                var x = QuerySP<PointsHistory>("SP_INF_POINT_HISTORY_Q", new
                {
                    USER_SEQ = userSeq,
                    PAGE_NUM = PAGE_NUM,
                    PAGE_SIZE = PAGE_SIZE,
                    SORT_TP = SORT_TP
                });

                list = x;

                foreach (var t in list)
                {
                    t.PAID_DATE = Common.ShortDate(t.PAID_DATE);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        
        /// <summary>
        /// Influencers - PointsHistory
        /// </summary>
        /// <param name="userSeq"></param>
        /// <returns></returns>
        public virtual OrderItems GetMyItemForOrderItem(string ORDER_NO, string PRODUCT_NO, string SKU_NO)
        {
            OrderItems myitem = null;
            try
            {
                var x = QuerySP<OrderItems>("SP_INF_POINT_HISTORY_Q", new
                {
                    ORDER_NO = ORDER_NO,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO
                });
                myitem = x.FirstOrDefault();
                
                myitem.ORDERED_DATE = Common.ShortDate(myitem.ORDERED_DATE);
                myitem.SHIPPED_DATE = Common.ShortDate(myitem.SHIPPED_DATE);
                myitem.CREATED_DATE = Common.ShortDate(myitem.CREATED_DATE);
                myitem.POST_LINK_DATE = Common.ShortDate(myitem.POST_LINK_DATE);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return myitem;
        }
        /// <summary>
        /// Influencers - Faq
        /// </summary>
        /// <returns></returns>
        public virtual IList<Faq> GetFaq()
        {
            IList<Faq> list = null;
            try
            {
                var x = QuerySP<Faq>("SP_INF_FAQ_Q", new
                {
                    
                });

                list = x;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

    }
}
