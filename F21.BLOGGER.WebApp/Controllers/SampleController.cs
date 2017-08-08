using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Dropbox.Api;
using System.IO;
using System.Text;
using Dropbox.Api.Files;
using System.Configuration;

namespace F21.BLOGGER.WebApp.Controllers
{
    public class SampleController : Controller
    {
        // GET: Sample
        public ActionResult Index()
        {
            //return View("Sample_List");
            return View("Sample_Upload");
        }
    }
}