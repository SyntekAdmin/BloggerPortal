using System.ComponentModel.DataAnnotations;
using System.Web;

namespace F21.BLOGGER.WebApp.Models
{
    public class FileModels
    {
        public HttpPostedFileBase uploadFile { get; set; }
    }
}