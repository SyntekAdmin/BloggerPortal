using System;
using System.Collections.Generic;
using System.Linq;
using Stis.Data;
using F21.BLOGGER.Model.Admin;
using Dapper;
using System.Data;
using F21.BLOGGER.Model.Complex;
using F21.BLOGGER.Model;

namespace F21.BLOGGER.DataAccess
{
    public class AdminDao : DapperFactory
    {
        public AdminDao() : base("INFLUENCER")
        { }

        #region 메인(SuperAdmin)
        public int GetCurrentInfluencers()
        {
            int list = 0;
            try
            {
                var x = QuerySP<MainModel>("SP_ADM_MAIN_CURRENT_INF_Q", new { });

                list = x[0].CNT;

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
                var x = QuerySP<MainModel>("SP_ADM_MAIN_NEW_INF_Q", new { });

                list = x[0].CNT;

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
                var x = QuerySP<MainModel>("SP_ADM_MAIN_NEW_PHOTOS_Q", new { });

                list = x[0].CNT;

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
                int ADMIN_SEQ)
        {
            int list = 0;
            try
            {
                var x = QuerySP<MainModel>("SP_ADM_MAIN_MY_INF_Q", new {
                    ADMIN_SEQ = ADMIN_SEQ
                });

                list = x[0].CNT;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        public int GetAdminNewPhotos(
                int ADMIN_SEQ)
        {
            int list = 0;
            try
            {
                var x = QuerySP<MainModel>("SP_ADM_MAIN_INF_NEW_PHOTOS_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ
                });

                list = x[0].CNT;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        public string GetAdminCreateUser(
                int ADMIN_SEQ)
        {
            string list = "";
            try
            {
                var x = QuerySP<MainModel>("SP_ADM_MAIN_CREATE_USER_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ
                });

                list = x[0].ADMIN_NM;

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
            int PAGE_NUM,
            int PAGE_SIZE,
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            string SORT_TP,
            string SEARCH_TEXT = "")
        {
            IList<AdminItemsListModel> list = null;
            try
            {
                var x = QuerySP<AdminItemsListModel>("SP_ADM_ITEMSLIST_Q", new
                {
                    PAGE_NUM = PAGE_NUM,
                    PAGE_SIZE = PAGE_SIZE,
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO,
                    SORT_TP = SORT_TP,
                    SEARCH_TEXT = SEARCH_TEXT
                });
                
                list = x;

                foreach (var item in list)
                {
                    item.Photos = GetItemListPhotoList(item.ADMIN_SEQ, item.PRODUCT_NO, item.SKU_NO);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        public int GetItemListCnt(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            string SEARCH_TEXT = "")
        {
            int list = 0;
            try
            {
                var x = QuerySP<AdminItemsListModel>("SP_ADM_ITEMSLIST_CNT_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SEARCH_TEXT = SEARCH_TEXT
                });

                list = x[0].CNT;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        public IList<AdminPhotoListModel> GetItemListPhotoList(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO)
        {
            IList<AdminPhotoListModel> list = null;
            try
            {
                var x = QuerySP<AdminPhotoListModel>("SP_ADM_ITEMSLIST_PHOTOS_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO
                });

                list = x;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        public int UpdateReviewingStatus(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ADMIN_SEQ", ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PRODUCT_NO", PRODUCT_NO, DbType.String, ParameterDirection.Input);
                param.Add("@SKU_NO", SKU_NO, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //
                cn.Execute("SP_ADM_ITEMS_STATUS_REVIEWING_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        public int UpdatePickedStatus(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            int USER_SEQ,
            int PHOTO_SEQ)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ADMIN_SEQ", ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PRODUCT_NO", PRODUCT_NO, DbType.String, ParameterDirection.Input);
                param.Add("@SKU_NO", SKU_NO, DbType.String, ParameterDirection.Input);
                param.Add("@USER_SEQ", USER_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PHOTO_SEQ", PHOTO_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //
                cn.Execute("SP_ADM_ITEMS_STATUS_PICKED_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        public int UpdatePickedPoints(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            int USER_SEQ,
            int PHOTO_SEQ)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ADMIN_SEQ", ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PRODUCT_NO", PRODUCT_NO, DbType.String, ParameterDirection.Input);
                param.Add("@SKU_NO", SKU_NO, DbType.String, ParameterDirection.Input);
                param.Add("@USER_SEQ", USER_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PHOTO_SEQ", PHOTO_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //
                cn.Execute("SP_ADM_ITEMS_STATUS_PICKED_POINT_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        public int UpdateNominated(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            int USER_SEQ)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ADMIN_SEQ", ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PRODUCT_NO", PRODUCT_NO, DbType.String, ParameterDirection.Input);
                param.Add("@SKU_NO", SKU_NO, DbType.String, ParameterDirection.Input);
                param.Add("@USER_SEQ", USER_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //
                cn.Execute("SP_ADM_ITEMS_NOMINATED_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        public int UpdatePickedStatusPhoto(
            int ADMIN_SEQ,
            int PHOTO_SEQ)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ADMIN_SEQ", ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PHOTO_SEQ", PHOTO_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //
                cn.Execute("SP_ADM_ITEMS_STATUS_PICKED_PHOTO_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

        #region 마이아이템
        public AdminItemsListModel GetMyItem(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO)
        {
            AdminItemsListModel list = null;
            try
            {
                var x = QuerySP<AdminItemsListModel>("SP_ADM_MYITEM_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO
                });

                list = x[0];

                list.Photos = GetItemListPhotoList(list.ADMIN_SEQ, list.PRODUCT_NO, list.SKU_NO);

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        public IList<AdminMyitemUserListModel> GetMyItemList(
            int PAGE_NUM,
            int PAGE_SIZE,
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            string SORT_TP)
        {
            IList<AdminMyitemUserListModel> list = null;
            try
            {
                var x = QuerySP<AdminMyitemUserListModel>("SP_ADM_MYITEM_LIST_Q", new
                {
                    PAGE_NUM = PAGE_NUM,
                    PAGE_SIZE = PAGE_SIZE,
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO,
                    SORT_TP = SORT_TP
                });

                list = x;

                foreach(var item in list)
                {
                    item.Photos = GetMyItemPhotoList(ADMIN_SEQ, PRODUCT_NO, SKU_NO, item.USER_SEQ);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        public int GetMyItemListCnt(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO)
        {
            int list = 0;
            try
            {
                var x = QuerySP<AdminItemsListModel>("SP_ADM_MYITEM_LIST_CNT_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO
                });

                list = x[0].CNT;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        public IList<AdminPhotoListModel> GetMyItemPhotoList(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            int USER_SEQ)
        {
            IList<AdminPhotoListModel> list = null;
            try
            {
                var x = QuerySP<AdminPhotoListModel>("SP_ADM_MYITEM_PHOTOS_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO,
                    USER_SEQ = USER_SEQ
                });

                list = x;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        public InfPhoto GetPhoto(
            int PHOTO_SEQ)
        {
            InfPhoto list = null;
            try
            {
                var x = QuerySP<InfPhoto>("SP_ADM_ITEMS_PHOTO_Q", new
                {
                    PHOTO_SEQ = PHOTO_SEQ
                });

                list = x[0];
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        public int SetPhoto(
            int ADMIN_SEQ,
            int PHOTO_SEQ,
            string PHOTO_URL)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ADMIN_SEQ", ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PHOTO_SEQ", PHOTO_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PHOTO_URL", PHOTO_URL, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //
                cn.Execute("SP_ADM_ITEMS_PHOTO_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

        #region 마이아이템 뷰
        public AdminMyitemUserListViewModel GetMyItemListView(
            int ADMIN_SEQ,
            string PRODUCT_NO,
            string SKU_NO,
            int USER_SEQ)
        {
            AdminMyitemUserListViewModel list = null;
            try
            {
                var x = QuerySP<AdminMyitemUserListViewModel>("SP_ADM_MYITEM_LIST_VIEW_Q", new
                {
                    ADMIN_SEQ = ADMIN_SEQ,
                    PRODUCT_NO = PRODUCT_NO,
                    SKU_NO = SKU_NO,
                    USER_SEQ = USER_SEQ
                });

                list = x[0];

                list.Photos = GetMyItemPhotoList(ADMIN_SEQ, PRODUCT_NO, SKU_NO, USER_SEQ);

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
