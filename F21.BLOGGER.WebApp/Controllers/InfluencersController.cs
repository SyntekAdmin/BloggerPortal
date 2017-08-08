#region Using

using F21.BLOGGER.Business;
using F21.BLOGGER.Model;
using F21.BLOGGER.WebApp.Models;
using System;
using System.Web.Mvc;

#endregion

namespace F21.BLOGGER.WebApp.Controllers
{
    [Authorize]
    public class InfluencersController : Controller
    {
        // GET: influencers/myorders
        public ActionResult MyOrders()
        {
            return View();
        }
        // GET: influencers/Orders
        public ActionResult Orders()
        {
            return View();
        }

        // GET: influencers/myorderdetauil
        public ActionResult MyOrderDetail()
        {
            return View();
        }

        // GET: influencers/myitems
        public ActionResult MyItems()
        {
            return View();
        }

        // GET: influencers/points
        public ActionResult Points()
        {
            return View();
        }

        // GET: influencers/pointshistory
        public ActionResult PointsHistory()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }
    }

    #region FormData
    public class ListParam
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public int userSeq { get; set; }
        public string orderText { get; set; }
    }
    #endregion
}