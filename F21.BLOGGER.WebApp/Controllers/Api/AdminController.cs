#region Using

using System;
using System.Collections.Generic;
using System.Web.Http;
using F21.BLOGGER.Business;
using F21.BLOGGER.Model.Admin;
using System.Web;

#endregion


namespace F21.BLOGGER.WebApp.Controllers.Api
{
    public partial class AdminController : BaseApiController
    {
        #region 아이템리스트
        /// GET: Admin
        [HttpGet]
        [Route("api/admin/getitemslist")]
        public IEnumerable<AdminItemsListModel> GetItemsList([FromUri] AdminListParam adminListParam)
        {
            adminListParam.searchText = (adminListParam.searchText == null) ? "" : adminListParam.searchText;
            //
            int adminSeq = -1;
            if(Identity.ROLE.ToUpper() == "SUPERADMIN" || Identity.ROLE.ToUpper() == "ADMIN")
            {
                if(Identity.ROLE.ToUpper() == "SUPERADMIN")
                {
                    adminSeq = -1;
                }
                else
                {
                    adminSeq = (int)userSeq;
                }
                //
                AdminBiz adminBiz = new AdminBiz();
                var items = adminBiz.GetItemList(
                    adminListParam.pageNum,
                    adminListParam.pageSize,
                    adminSeq,
                    "",
                    "",
                    adminListParam.orderText,
                    adminListParam.searchText);
                return items;
            }
            else
            {
                return null;
            }
        }

        /// GET: Admin
        [HttpGet]
        [Route("api/admin/getitemslistcnt")]
        public int GetItemsListCnt([FromUri] AdminListParam adminListParam)
        {
            adminListParam.searchText = (adminListParam.searchText == null) ? "": adminListParam.searchText;
            //
            int adminSeq = -1;
            if (Identity.ROLE.ToUpper() == "SUPERADMIN" || Identity.ROLE.ToUpper() == "ADMIN")
            {
                if (Identity.ROLE.ToUpper() == "SUPERADMIN")
                {
                    adminSeq = -1;
                }
                else
                {
                    adminSeq = (int)userSeq;
                }
                //
                AdminBiz adminBiz = new AdminBiz();
                var item = adminBiz.GetItemListCnt(
                    adminSeq,
                    "",
                    "",
                    adminListParam.searchText);
                return item;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region 마이아이템
        [HttpGet]
        [Route("api/admin/getmyitem")]
        public AdminItemsListModel GetMyItem([FromUri] AdminListParam adminListParam)
        {
            AdminBiz adminBiz = new AdminBiz();
            var item = adminBiz.GetMyItem(
                adminListParam.adminSeq,
                adminListParam.productNo,
                adminListParam.skuNo);
            return item;
        }
        /// GET: Admin
        [HttpGet]
        [Route("api/admin/getmyitemlist")]
        public IEnumerable<AdminMyitemUserListModel> GetMyItemList([FromUri] AdminListParam adminListParam)
        {
            AdminBiz adminBiz = new AdminBiz();
            var items = adminBiz.GetMyItemList(
                adminListParam.pageNum,
                adminListParam.pageSize,
                adminListParam.adminSeq,
                adminListParam.productNo,
                adminListParam.skuNo,
                adminListParam.orderText);

            return items;
        }
        /// GET: Admin
        [HttpGet]
        [Route("api/admin/getmyitemlistcnt")]
        public int GetMyItemListCnt([FromUri] AdminListParam adminListParam)
        {
            AdminBiz adminBiz = new AdminBiz();
            var item = adminBiz.GetMyItemListCnt(
                adminListParam.adminSeq,
                adminListParam.productNo,
                adminListParam.skuNo);
            return item;
        }
        /// GET: Admin
        [HttpGet]
        [Route("api/admin/getmyitemlistview")]
        public AdminMyitemUserListViewModel GetMyItemListView([FromUri] AdminListParam adminListParam)
        {
            AdminBiz adminBiz = new AdminBiz();
            var item = adminBiz.GetMyItemListView(
                adminListParam.adminSeq,
                adminListParam.productNo,
                adminListParam.skuNo,
                adminListParam.userSeq);
            //
            item.Photos = adminBiz.GetMyItemPhotoList(adminListParam.adminSeq, adminListParam.productNo, adminListParam.skuNo, item.USER_SEQ);
            return item;
        }
        #endregion

        #region Reviewing Picked Status 변경
        /// POST: Admin
        [HttpPost]
        [Route("api/admin/setreviewingstatus")]
        public int UpdateReviewingStatus([FromBody] AdminListParam adminListParam)
        {
            //
            int adminSeq = -1;
            if (Identity.ROLE.ToUpper() == "SUPERADMIN" || Identity.ROLE.ToUpper() == "ADMIN")
            {
                if (Identity.ROLE.ToUpper() == "SUPERADMIN")
                {
                    adminSeq = adminListParam.adminSeq;
                }
                else
                {
                    adminSeq = (int)userSeq;
                }
                //
                AdminBiz adminBiz = new AdminBiz();
                var item = adminBiz.UpdateReviewingStatus(
                    adminSeq,
                    adminListParam.productNo,
                    adminListParam.skuNo);
                return item;
            }
            else
            {
                return -1;
            }
        }
        /// POST: Admin
        [HttpPost]
        [Route("api/admin/setpickedstatus")]
        public int UpdatePickedStatus([FromBody] AdminListParam adminListParam)
        {
            //
            int adminSeq = -1;
            if (Identity.ROLE.ToUpper() == "SUPERADMIN" || Identity.ROLE.ToUpper() == "ADMIN")
            {
                if (Identity.ROLE.ToUpper() == "SUPERADMIN")
                {
                    adminSeq = adminListParam.adminSeq;
                }
                else
                {
                    adminSeq = (int)userSeq;
                }
                //
                AdminBiz adminBiz = new AdminBiz();
                var item = adminBiz.UpdatePickedStatus(
                    adminSeq,
                    adminListParam.productNo,
                    adminListParam.skuNo,
                    adminListParam.userSeq,
                    adminListParam.myitemSeq,
                    adminListParam.photoSeq);
                return item;
            }
            else
            {
                return -1;
            }
        }

        /// POST: Admin
        [HttpPost]
        [Route("api/admin/setnominated")]
        public int UpdateNominated([FromBody] AdminListParam adminListParam)
        {
            //
            int adminSeq = -1;
            if (Identity.ROLE.ToUpper() == "SUPERADMIN" || Identity.ROLE.ToUpper() == "ADMIN")
            {
                if (Identity.ROLE.ToUpper() == "SUPERADMIN")
                {
                    adminSeq = adminListParam.adminSeq;
                }
                else
                {
                    adminSeq = (int)userSeq;
                }
                //
                AdminBiz adminBiz = new AdminBiz();
                var item = adminBiz.UpdateNominated(
                    adminSeq,
                    adminListParam.productNo,
                    adminListParam.skuNo,
                    adminListParam.userSeq);
                return item;
            }
            else
            {
                return -1;
            }
        }

        /// POST: Admin
        [HttpPost]
        [Route("api/admin/setretouched")]
        public int UpdateRetouched()
        {
            int result = -1;
            //
            var request = HttpContext.Current.Request;
            var length = request.ContentLength;
            var bytes = new byte[length];
            request.InputStream.Read(bytes, 0, length);

            var fileName = DateTime.Now.Ticks.ToString() + "_" + request.Headers["X-File-Name"];
            var fileSize = request.Headers["X-File-Size"];
            var fileType = request.Headers["X-File-Type"];
            var adminSeq = request.Headers["X-File-AdminSeq"];
            var photoSeq = request.Headers["X-File-PhotoSeq"];

            // File Upload
            bool bu = Common.DropboxUtil.Upload("/Images", fileName, bytes);

            AdminBiz adminBiz = new AdminBiz();
            var photos = adminBiz.GetPhoto(int.Parse(photoSeq));
            result = adminBiz.UpdatePhoto(
                int.Parse(adminSeq),
                int.Parse(photoSeq),
                fileName);
            if (result > 0)
            {
                // Old File Delete
                bool bd = Common.DropboxUtil.Delete("/Images", photos.PHOTO_URL);
            }
            return result;
        }
        #endregion
    }

    #region FormData
    public class AdminListParam
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public int adminSeq { get; set; }
        public int userSeq { get; set; }
        public string productNo { get; set; }
        public string skuNo { get; set; }
        public int myitemSeq { get; set; }
        public int photoSeq { get; set; }
        public string searchText { get; set; }
        public string orderText { get; set; }
    }
    #endregion
}
