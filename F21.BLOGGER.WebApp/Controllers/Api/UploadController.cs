#region Using

using System;
using System.Collections.Generic;
using F21.BLOGGER.WebApp.Models;
using F21.BLOGGER.Business;
using F21.BLOGGER.WebApp.Common;
using F21.BLOGGER.Model;
using F21.BLOGGER.Model.Admin;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Net;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Threading.Tasks;

#endregion

namespace F21.BLOGGER.WebApp.Controllers.Api
{
   
    public partial class UploadController : BaseApiController
    {
        

        // GET: Upload
        [HttpPost]
        [Route("api/upload/image")]
        public IEnumerable<string> AdminList()
        {
            IEnumerable<string> list = null;
            var request = HttpContext.Current.Request;
            var length = request.ContentLength;
            var bytes = new byte[length];
            request.InputStream.Read(bytes, 0, length);

            var fileName = request.Headers["X-File-Name"];
            var fileSize = request.Headers["X-File-Size"];
            var fileType = request.Headers["X-File-Type"];

            bool b = Common.DropboxUtil.Upload("", fileName, bytes);
            //var saveToFileLoc = @"C:\ZZZ\" + fileName;

            // save the file.
            //var fileStream = new FileStream(saveToFileLoc, FileMode.Create, FileAccess.ReadWrite);
            //fileStream.Write(bytes, 0, length);
            //fileStream.Close();


            //return string.Format("{0} bytes uploaded", bytes.Length);
            list.Append<string>(string.Format("{0} bytes uploaded", bytes.Length));
            return list;
        }

    }
}