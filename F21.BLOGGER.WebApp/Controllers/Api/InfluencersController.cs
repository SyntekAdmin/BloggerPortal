using F21.BLOGGER.Business;
using F21.BLOGGER.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using F21.BLOGGER.WebApp.Common;

namespace F21.BLOGGER.WebApp.Controllers.Api
{
    public class InfluencersController : BaseApiController
    {
        private NetworkFunctions nf = new NetworkFunctions();

        [HttpPost]
        [Route("api/influencers/myorders")]
        public OrderHistory GetMyOrders([FromBody] ListParam listParam)
        {
            OrderHistory list = null;
            string msg = "";
            try
            {
                string guid = Identity.FOREVER21_ID;
                string page = listParam.pageNum.ToString();
                string pageSize = listParam.pageSize.ToString();
                string url = "/f21api/UserService.svc/{0}/profile/orderhistory/?userid={1}&page={2}&pagesize={3}";

                url = string.Format(url, F21.BLOGGER.WebApp.Common.Settings.API_Version, guid, page, pageSize);

                msg =  nf.GetWebResponseData(url, NetworkFunctions.CallType.GET);

                list = JsonConvert.DeserializeObject<OrderHistory>(msg);
                if (list.OrderHistoryInfoList != null && list.OrderHistoryInfoList.Count > 0) {
                    var biz = new InfluencersBiz();
                    string OrderNumber = "";
                    string ProductId = "";
                    string VariantId = "";
                    OrderHistoryInfoList infoList = null;
                    OrderHistoryItemSummaryList historyList = null;
                    OrderItems myitem = null;
                    for (int i = 0; i < list.OrderHistoryInfoList.Count; i++) {
                        infoList = list.OrderHistoryInfoList[i];
                        OrderNumber = infoList.OrderNumber;
                        int selectItemCount = 0;
                        for (int j = 0; j < infoList.OrderHistoryItemSummaryList.Count; j++) {
                            historyList = infoList.OrderHistoryItemSummaryList[j];
                            historyList.OrderDate = list.OrderHistoryInfoList[i].OrderDate;
                            historyList.OrderNumber = list.OrderHistoryInfoList[i].OrderNumber;
                            ProductId = historyList.ProductId;
                            VariantId = historyList.VariantId;
                            myitem = biz.GetMyItemForOrderItem(OrderNumber, ProductId, VariantId);
                            if (myitem != null)
                            {
                                list.OrderHistoryInfoList[i].OrderHistoryItemSummaryList[j].MyItem_Seq = myitem.MYITEM_SEQ;
                                list.OrderHistoryInfoList[i].OrderHistoryItemSummaryList[j].IsChecked = true;
                                selectItemCount++;
                            }
                            else
                            {
                                list.OrderHistoryInfoList[i].OrderHistoryItemSummaryList[j].MyItem_Seq = "-1";
                                list.OrderHistoryInfoList[i].OrderHistoryItemSummaryList[j].IsChecked = false;
                            }



                        }
                        list.OrderHistoryInfoList[i].SelectItem = "N";
                        if (infoList.OrderHistoryItemSummaryList.Count == selectItemCount)
                        {
                            list.OrderHistoryInfoList[i].SelectItem = "Y";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        [HttpPost]
        [Route("api/influencers/myordercnt")]
        public int GetMyOrderCnt()
        {
            OrderHistory list = null;
            string msg = "";
            int item = 0;
            try
            {
  
                string guid = Identity.FOREVER21_ID;
                string page = "1";
                string pageSize = "1";
                
                string url = "/f21api/UserService.svc/{0}/profile/orderhistory/?userid={1}&page={2}&pagesize={3}";

                url = string.Format(url, F21.BLOGGER.WebApp.Common.Settings.API_Version, guid, page, pageSize);

                msg = nf.GetWebResponseData(url, NetworkFunctions.CallType.GET);

                list = JsonConvert.DeserializeObject<OrderHistory>(msg);

                item = Convert.ToInt32(list.TotalRecords);

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return item;
        }


        #region myitem등록

        [HttpPost]
        [Route("api/influencers/insertmyitems")]
        //public int InsertMyItemByOrder([FromBody]IList<OrderHistoryItemSummaryList> items)
        public int InsertMyItemByOrder([FromBody]OrderHistoryInfoList meals)
        {
            int USER_SEQ = Identity.USER_SEQ;
            InfluencersBiz biz = new InfluencersBiz();            
            return biz.SetMyItemByOrder(meals.OrderHistoryItemSummaryList, USER_SEQ);
        }
        #endregion
               
        [HttpGet]
        [Route("api/influencers/myorderdetail/{orderNo}")]
        public MyOrders GetMyOrderDetail(string orderNo)
        {
            MyOrders data = null;
            try
            {
                var biz = new InfluencersBiz();

                data = biz.GetMyOrderDetailByOrderNo(orderNo);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return data;
        }

        [HttpGet]
        [Route("api/influencers/myitems")]
        public IList<MyItems> GetMyItems(Int64 myItemSeq)
        {
            IList<MyItems> list = null;
            try
            {
                var biz = new InfluencersBiz();

                list = biz.GetMyItemsByUserSeq(this.userSeq, myItemSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        [HttpGet]
        [Route("api/influencers/myitem/{myItemSeq}")]
        public IList<MyItems> GetMyItemDetail(Int64 myItemSeq)
        {
            IList<MyItems> list = null;
            try
            {
                var biz = new InfluencersBiz();

                list = biz.GetMyItemsByUserSeq(this.userSeq, myItemSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        //[HttpGet]
        //[Route("api/influencers/points")]
        //public IList<Points> GetPoints()
        //{
        //    IList<Points> list = null;
        //    try
        //    {
        //        var biz = new InfluencersBiz();

        //        int PAGE_NUM = 1;
        //        int PAGE_SIZE = 4;
        //        string SORT_TP = "01";

        //        list = biz.GetPointsByUserSeq(this.userSeq, PAGE_NUM, PAGE_SIZE, SORT_TP);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.ToString());
        //    }

        //    return list;
        //}

        //[HttpGet]
        //[Route("api/influencers/pointshistory")]
        //public IList<PointsHistory> GetPointsHistory()
        //{
        //    IList<PointsHistory> list = null;
        //    try
        //    {
        //        var biz = new InfluencersBiz();

        //        int PAGE_NUM = 1;
        //        int PAGE_SIZE = 4;
        //        string SORT_TP = "01";

        //        list = biz.GetPointsHistoryByUserSeq(this.userSeq, PAGE_NUM, PAGE_SIZE, SORT_TP);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.ToString());
        //    }

        //    return list;
        //}


        [HttpGet]
        [Route("api/influencers/faq")]
        public IList<Faq> GetFaq()
        {
            IList<Faq> list = null;
            try
            {
                var biz = new InfluencersBiz();

                list = biz.GetFaq();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
    }
}
