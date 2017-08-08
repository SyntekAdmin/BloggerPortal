#region Using

using F21.BLOGGER.Business;
using F21.BLOGGER.Model;
using F21.BLOGGER.WebApp.Common;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

#endregion

namespace F21.BLOGGER.WebApp.Controllers
{
    [Authorize]
    public partial class AdminController : BaseExController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult MyInfluencers()
        {
            return View();
        }

        public ActionResult Influencers()
        {
            return View();
        }

        public ActionResult Members()
        {
            return View();
        }
        
        [HttpGet]
        [Route("admin/items/download/{fileName}")]
        public FileResult PhotoDownload(string fileName)
        {
            //var file = "54336902.jpg";
            var file = fileName;
            var folder = "/Images";
            var FileVirtualPath = DropboxUtil.Download(folder, file);
            return File(FileVirtualPath, "application/force-download", file);
        }

        [HttpGet]
        [Route("admin/items/viewer/{fileName}")]
        public ActionResult PhotoViewer(string fileName)
        {
            //var file = "54336902.jpg";
            //Response.ContentType = item.PHOTO_CONTENTTYPE;

            var file = fileName;
            var folder = "/Images";
            var bytes = DropboxUtil.Download(folder, file);

            Response.ContentType = "image/jpg";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            return Content("");
        }

        #region 아이템즈
        // GET: Admin
        [HttpGet]
        [Route("admin/Items")]
        public ActionResult ItemsList()
        {
            if (Identity.ROLE.Trim().ToUpper() == "SUPERADMIN" || Identity.ROLE.Trim().ToUpper() == "ADMIN")
            {
                if (Identity.ROLE.Trim().ToUpper() == "SUPERADMIN")
                {
                    ViewBag.AdminSeq = -1;
                }
                else
                {
                    ViewBag.AdminSeq = Identity.USER_SEQ;
                }

                return View("ItemsList");
            }
            else
            {
                return Content("Accese Denay!!!");
            }
        }

        // GET: Admin
        [HttpGet]
        [Route("admin/Items/ViewAll/{adminSeq}/{productNo}/{skuNo}")]
        public ActionResult MyItems(int adminSeq, string productNo, string skuNo)
        {
            ViewBag.AdminSeq = adminSeq;
            ViewBag.ProductNo = productNo;
            ViewBag.SkuNo = skuNo;
            if (Identity.ROLE.Trim().ToUpper() == "SUPERADMIN" || Identity.ROLE.Trim().ToUpper() == "ADMIN")
            {
                return View("MyItems");
            }
            else
            {
                return Content("Accese Denay!!!");
            }
        }
        // GET: Admin
        [HttpGet]
        [Route("admin/Items/View/{adminSeq}/{productNo}/{skuNo}/{userSeq}")]
        public ActionResult MyItemsView(int adminSeq, string productNo, string skuNo, int userSeq)
        {
            ViewBag.AdminSeq = adminSeq;
            ViewBag.ProductNo = productNo;
            ViewBag.SkuNo = skuNo;
            ViewBag.UserSeq = userSeq;

            if (Identity.ROLE.Trim().ToUpper() == "SUPERADMIN" || Identity.ROLE.Trim().ToUpper() == "ADMIN")
            {
                return View("MyItemsView");
            }
            else
            {
                return Content("Accese Denay!!!");
            }
        }
        #endregion

        #region 포인트

        // GET: admin/points
        public ActionResult Points()
        {
            return View();
        }

        // TODO: PointsDetail은 MyItems의 Detail 화면과 동일함.

        // GET: admin/pointsdetail
        public ActionResult PointsDetail()
        {
            // Stis.Core.FrameContext.Current.Logger.Error("");

            //Stis.Data.Attributes.LoggerAttribute
            return View();
        }
        #endregion

    }
}