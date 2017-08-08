#region Using

using F21.BLOGGER.Model;
using F21.BLOGGER.WebApp.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

#endregion

namespace F21.BLOGGER.WebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseExController
    {
        // GET: home/index
        public ActionResult Index()
        {
            if (Identity.ROLE.ToString().ToUpper().Trim() == "USER")
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "admin");
            }
        }

        [AllowAnonymous]
        [Route("uploadfile")]
        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase myFile = Request.Files["uploadFile"];
            bool isUploaded = false;
            string message = "File upload failed";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string configUpload = ConfigurationManager.AppSettings["Upload_Nas"];
                string pathForSaving = Server.MapPath(configUpload);
                string toDay = DateTime.Now.ToString("yyyyMMdd");
                string uploadFolder = pathForSaving + "\\" + toDay;
                string fileExt = Path.GetExtension(myFile.FileName);

                string newFileName = Guid.NewGuid().ToString() + fileExt;

                if (CreateFolderForUpload(uploadFolder))
                {
                    try
                    {
                        myFile.SaveAs(Path.Combine(uploadFolder, newFileName));
                        isUploaded = true;
                        message = configUpload + "/" + toDay + "/" + newFileName;
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }

        [Route("uploadfileprofile")]
        [HttpPost]
        public ActionResult UploadFileProfile()
        {
            HttpPostedFileBase myFile = Request.Files["uploadFile"];
            bool isUploaded = false;
            string message = "File upload failed";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string configUpload = ConfigurationManager.AppSettings["Upload_Nas"];
                string pathForSaving = Server.MapPath(configUpload);
                string toDay = DateTime.Now.ToString("yyyyMMdd");
                string uploadFolder = pathForSaving + "\\" + toDay;
                string fileExt = Path.GetExtension(myFile.FileName);

                string newFileName = Guid.NewGuid().ToString() + fileExt;

                if (CreateFolderForUpload(uploadFolder))
                {
                    try
                    {
                        myFile.SaveAs(Path.Combine(uploadFolder, newFileName));
                        isUploaded = true;
                        message = configUpload + "/" + toDay + "/" + newFileName;
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }
    }

}