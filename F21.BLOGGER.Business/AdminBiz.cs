using F21.BLOGGER.DataAccess;
using F21.BLOGGER.Model;
using F21.BLOGGER.Model.Admin;
using Stis.Data;
using Stis.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.Business
{
    [Logger]
    public class AdminBiz : BusinessFactory
    {
        #region 메인 (SuperAdmin)
        public int GetCurrentInfluencers()
        {
            int list = 0;
            try
            {
                var dao = new AdminDao();
                list = dao.GetCurrentInfluencers();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public int GetNewInfluencers()
        {
            int list = 0;
            try
            {
                var dao = new AdminDao();
                list = dao.GetNewInfluencers();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public int GetNewPhotos()
        {
            int list = 0;
            try
            {
                var dao = new AdminDao();
                list = dao.GetNewPhotos();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        #endregion

        #region 메인(Admin)
        public int GetMyInfluencers(
            int adminSeq)
        {
            int list = 0;
            try
            {
                var dao = new AdminDao();
                list = dao.GetMyInfluencers(
                    adminSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public int GetAdminNewPhotos(
            int adminSeq)
        {
            int list = 0;
            try
            {
                var dao = new AdminDao();
                list = dao.GetAdminNewPhotos(
                    adminSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public string GetAdminCreateUser(
            int adminSeq)
        {
            string list = "";
            try
            {
                var dao = new AdminDao();
                list = dao.GetAdminCreateUser(
                    adminSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        #endregion

        #region 아이템리스트
        public IList<AdminItemsListModel> GetItemList(
            int pageNum,
            int pageSize,
            int admSeq,
            string productNo,
            string skuNo,
            string sortTp,
            string searchText = "")
        {
            IList<AdminItemsListModel> list = null;
            try
            {
                var dao = new AdminDao();
                list = dao.GetItemList(
                    pageNum,
                    pageSize,
                    admSeq,
                    productNo,
                    skuNo,
                    sortTp,
                    searchText
                    );
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public int GetItemListCnt(
            int admSeq,
            string productNo,
            string skuNo,
            string searchText = "")
        {
            int list = 0;
            try
            {
                var dao = new AdminDao();
                list = dao.GetItemListCnt(
                    admSeq,
                    productNo,
                    skuNo,
                    searchText
                    );
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public int UpdateReviewingStatus(
            int admSeq,
            string productNo,
            string skuNo)
        {
            int bizResult = -1;
            this.TransactionScope(x =>
            {
                var dao = new AdminDao();
                bizResult = dao.UpdateReviewingStatus(
                    admSeq,
                    productNo,
                    skuNo);
                if (bizResult < 1)
                {
                    throw new Exception("UpdateReviewingStatus Error");
                }
            });

            return bizResult;
        }
        public int UpdatePickedStatus(
            int admSeq,
            string productNo,
            string skuNo,
            int userSeq,
            int myitemSeq,
            int photoSeq)
        {
            int bizResult = -1;
            this.TransactionScope(x =>
            {
                var dao = new AdminDao();
                // Picked
                bizResult = dao.UpdatePickedStatus(
                    admSeq,
                    productNo,
                    skuNo,
                    userSeq,
                    photoSeq);
                // Photo
                bizResult = dao.UpdatePickedStatusPhoto(
                    admSeq,
                    photoSeq);
                // Points
                bizResult = dao.UpdatePickedPoints(
                    admSeq,
                    productNo,
                    skuNo,
                    userSeq,
                    photoSeq);
                if (bizResult < 1)
                {
                    throw new Exception("UpdatePickedStatus Error");
                }
            });

            return bizResult;
        }
        public int UpdateNominated(
            int admSeq,
            string productNo,
            string skuNo,
            int userSeq)
        {
            int bizResult = -1;
            this.TransactionScope(x =>
            {
                var dao = new AdminDao();
                // Nominated
                bizResult = dao.UpdateNominated(
                    admSeq,
                    productNo,
                    skuNo,
                    userSeq);
                if (bizResult < 1)
                {
                    throw new Exception("UpdateNominated Error");
                }
            });

            return bizResult;
        }
        public InfPhoto GetPhoto(
            int photoSeq)
        {
            InfPhoto list = null;
            try
            {
                var dao = new AdminDao();
                list = dao.GetPhoto(photoSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public int UpdatePhoto(
            int adminSeq,
            int photoSeq,
            string photoUrl)
        {
            int bizResult = -1;
            this.TransactionScope(x =>
            {
                var dao = new AdminDao();
                bizResult = dao.SetPhoto(adminSeq, photoSeq, photoUrl);
                if (bizResult < 1)
                {
                    throw new Exception("UpdatePhoto Error");
                }
            });

            return bizResult;
        }
        #endregion

        #region 마이아이템
        public AdminItemsListModel GetMyItem(
            int admSeq,
            string productNo,
            string skuNo)
        {
            AdminItemsListModel list = null;
            try
            {
                var dao = new AdminDao();
                list = dao.GetMyItem(
                    admSeq,
                    productNo,
                    skuNo
                    );
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public IList<AdminMyitemUserListModel> GetMyItemList(
            int pageNum,
            int pageSize,
            int admSeq,
            string productNo,
            string skuNo,
            string sortTp,
            string searchText = "")
        {
            IList<AdminMyitemUserListModel> list = null;
            try
            {
                var dao = new AdminDao();
                list = dao.GetMyItemList(
                    pageNum,
                    pageSize,
                    admSeq,
                    productNo,
                    skuNo,
                    sortTp
                    );
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public int GetMyItemListCnt(
            int admSeq,
            string productNo,
            string skuNo)
        {
            int list = 0;
            try
            {
                var dao = new AdminDao();
                list = dao.GetMyItemListCnt(
                    admSeq,
                    productNo,
                    skuNo
                    );
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public IList<AdminPhotoListModel> GetMyItemPhotoList(
            int admSeq,
            string productNo,
            string skuNo,
            int userSeq)
        {
            IList<AdminPhotoListModel> list = null;
            try
            {
                var dao = new AdminDao();
                list = dao.GetMyItemPhotoList(
                    admSeq,
                    productNo,
                    skuNo,
                    userSeq
                    );
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        public AdminMyitemUserListViewModel GetMyItemListView(
            int admSeq,
            string productNo,
            string skuNo,
            int userSeq)
        {
            AdminMyitemUserListViewModel list = null;
            try
            {
                var dao = new AdminDao();
                list = dao.GetMyItemListView(
                    admSeq,
                    productNo,
                    skuNo,
                    userSeq
                    );
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        #endregion
    }
}
