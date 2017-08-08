using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace F21.BLOGGER.WebApp.Common
{
    public class FileStreamUtil
    {
        public static int FileStreamWrite(MemoryStream ms, string localFilePath)
        {
            int result = 0;
            try
            {
                using (FileStream file = new FileStream(localFilePath, FileMode.Create, System.IO.FileAccess.Write))
                {

                    byte[] bytes = new byte[ms.Length];
                    ms.Read(bytes, 0, (int)ms.Length);
                    file.Write(bytes, 0, bytes.Length);
                    ms.Close();
                }
                result = 1;
            }
            catch(Exception ex)
            {
                result = -1;
            }
            return result;
        }

        public static byte[] FileStreamRead(string localFilePath)
        {
            byte[] bytes = null;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                using (FileStream file = new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                {
                    bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                }
            }
            catch (Exception ex) { }

            return bytes;
        }
    }

    
}