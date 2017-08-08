using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Model
{
    public class InfPhoto
    {
        public int PHOTO_SEQ { get; set; }
        public int MYITEM_SEQ { get; set; }
        public string PHOTO_URL { get; set; }
        public string PHOTO_CONTENTTYPE { get; set; }
        public DateTime UPLOAD_DATE { get; set; }
        public DateTime PICKED_DATE { get; set; }
        public DateTime RETOUCHED_DATE { get; set; }

    }
}
