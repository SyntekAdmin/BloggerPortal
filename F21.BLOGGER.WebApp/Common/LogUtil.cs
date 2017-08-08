using System;
using System.Text;
using System.IO;

namespace F21.BLOGGER.WebApp.Common
{
    public class LogUtil
    {
        #region 디버깅용 로그 메서드
        public void LogWrite(string fileName, string strLog)
        {
            string TempFolder = @"C:\Temp\";

            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            DateTime dt = DateTime.Now;

            using (FileStream fs = new FileStream(String.Format("{0}{1:yyyy-MM-dd}_{2}.txt", TempFolder, dt, fileName), FileMode.OpenOrCreate))
            {
                StreamWriter w = new StreamWriter(fs, Encoding.Default);
                w.BaseStream.Seek(0, SeekOrigin.End);
                w.Write(String.Format("{0:HH:mm:ss}\r\n{1}\r\n\r\n", dt, strLog));
                w.Flush();
                w.Close();
                fs.Close();
            }
        }
        #endregion
    }
}
