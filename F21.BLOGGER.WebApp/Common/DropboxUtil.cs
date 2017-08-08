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

namespace F21.BLOGGER.WebApp.Common
{
   
    public class DropboxUtil
    {
        private static string ApiKey = ConfigurationManager.AppSettings["dropBoxToken"];
        private static string DownloadApiKey = ConfigurationManager.AppSettings["dropBoxDownloadToken"];
        private static Dropbox.Api.Users.FullAccount Full { get; set; }
        private static Dropbox.Api.Files.ListFolderResult List { get; set; }
        public static byte [] fileByte { get; set; }
        //
        public static byte[] Download(string folder, string file)
        {
            byte [] result = null;
            try
            {
                var task = Task.Run(async () => await _Download(folder, file));
                task.Wait();
                result = fileByte;
                task.Dispose();
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
        public static bool Upload(string folder, string file, byte[] contentByte)
        {
            bool result = false;
            try
            {
                var task = Task.Run(async () => await _Upload(folder, file, contentByte));
                task.Wait();
                task.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public static bool Delete(string folder, string file)
        {
            bool result = false;
            try
            {
                var task = Task.Run(async () => await _Delete(folder, file));
                task.Wait();
                task.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public static bool Copy(string fromFolder, string fromFile, string toFolder, string toFile)
        {
            bool result = false;
            try
            {
                var task = Task.Run(async () => await _Copy(fromFolder, fromFile, toFolder, toFile));
                task.Wait();
                task.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public static bool Move(string fromFolder, string fromFile, string toFolder, string toFile)
        {
            bool result = false;
            try
            {
                var task = Task.Run(async () => await _Move(fromFolder, fromFile, toFolder, toFile));
                task.Wait();
                task.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        static async Task _Run(int i)
        {
            using (var dbx = new DropboxClient(ApiKey))
            {
                Full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", Full.Name.DisplayName, Full.Email);
            }
        }
        static async Task _ListRootFolder()
        {
            using (var dbx = new DropboxClient(ApiKey))
            {
                List = await dbx.Files.ListFolderAsync(string.Empty);

                // show folders then files
                foreach (var item in List.Entries.Where(i => i.IsFolder))
                {
                    Console.WriteLine("D  {0}/", item.Name);
                }

                foreach (var item in List.Entries.Where(i => i.IsFile))
                {
                    Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
                }
            }
        }
        public static async Task _Download( string folder, string file)
        {
            using (var dbx = new DropboxClient(ApiKey))
            {
                using (var response = await dbx.Files.DownloadAsync(folder + "/" + file))
                {
                    //Console.WriteLine(await response.GetContentAsStringAsync());
                    //using (var fileStream = System.IO.File.Create(@"C:\ZZZ\aaa.pdf"))
                    //{
                    //    response.GetContentAsStreamAsync().Result.CopyTo(fileStream);
                    //}
                    using (MemoryStream ms = new MemoryStream())
                    {
                        response.GetContentAsStreamAsync().Result.CopyTo(ms);
                        fileByte = ms.ToArray();
                    }
                }
            }
        }
        public static async Task _Upload(string folder, string file, byte[] contentByte)
        {
            using (var dbx = new DropboxClient(ApiKey))
            {
                using (var mem = new MemoryStream(contentByte))
                {
                    var updated = await dbx.Files.UploadAsync(
                        folder + "/" + file,
                        WriteMode.Overwrite.Instance,
                        body: mem);

                    //업로드 한 파일을 공유 파일로 변경한다.
                    var shareInfo = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(folder + "/" + file);
                    //공유가 된 파일의 URL을 호스팅이 가능한 URL로 변경한다.(DB에 저장 필요)
                    string shareUrl = shareInfo.Url.Replace("https://www.dropbox.com", "https://dl.dropboxusercontent.com");

                    Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
                }
            }
        }
        public static async Task _Delete(string folder, string file)
        {
            using (var dbx = new DropboxClient(ApiKey))
            {
                var updated = await dbx.Files.DeleteAsync(folder + "/" + file);
            }
        }
        public static async Task _Copy(string fromFolder, string fromFile, string toFolder, string toFile, bool allowsharedFolder = false, bool autorename = false)
        {
            using (var dbx = new DropboxClient(ApiKey))
            {
                var updated = await dbx.Files.CopyAsync(fromFolder + "/" + fromFile, toFolder + "/" + toFile, allowsharedFolder, autorename);
            }
        }
        public static async Task _Move(string fromFolder, string fromFile, string toFolder, string toFile, bool allowsharedFolder = false, bool autorename = false)
        {
            using (var dbx = new DropboxClient(ApiKey))
            {
                var updated = await dbx.Files.MoveAsync(fromFolder + "/" + fromFile, toFolder + "/" + toFile, allowsharedFolder, autorename);
            }
        }
    }
}