using F21.BLOGGER.DataAccess;
using F21.BLOGGER.Model;
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
    public class AdminUserBiz : BusinessFactory
    {
        #region Admin 전체 리스트
        public int GetAdminAllCnt()
        {
            int COUNT = 0;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                COUNT = dao.GetAdminAllCnt();
            }
            catch (Exception ex)
            {
                COUNT = -1;
            }
            return COUNT;
        }

        public IList<InfAdmin> GetAdminAll(string sort, int pageNum, int pageSize)
        {
            IList<InfAdmin> list = null;
            try
            {
                var dao = new AdminUserDao();
                list = dao.GetAdminAll(sort, pageNum, pageSize);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        #endregion

        #region Admin 상세
        public InfAdmin GetAdmin(Int64 userSeq)
        {
            InfAdmin admin = null;
            try
            {
                var dao = new AdminUserDao();
                admin = dao.GetAdmin(userSeq);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return admin;
        }

        #region Influencer에 Admin 할당 및 해제
        public int UpdateInfluencerAdmin(IList<InfUser> item, string actionEmail)
        {
            int bizResult = -1;
            try
            {
                this.TransactionScope(x =>
                {
                    AdminUserDao dao = new AdminUserDao();

                    item.ForEach(d => {
                        InfUser user = new InfUser
                        {
                            USER_EMAIL = d.USER_EMAIL,
                            ACTION_EMAIL = actionEmail,
                            ADMIN_SEQ = d.ADMIN_SEQ
                        };
                        bizResult = dao.UpdateInfluencerAdmin(user);

                        if (bizResult < 1)
                        {
                            throw new Exception("UpdateInfluencerAdmin Error");
                        }

                    });
                });

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }

        public int ReleaseInfluencerAdmin(InfUser item)
        {
            int bizResult = -1;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                bizResult = dao.UpdateInfluencerAdmin(item);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        #endregion

        #endregion

        #region Influencer 정보 조회
        public InfInfluencer GetInfluencerInfoByUserId(string userEmail)
        {
            InfUser user = null;
            InfInfluencer infBalance = null;
            InfInfluencer influencer = null;

            try
            {
                UserDao dao = new UserDao();
                user = dao.GetUserInfoByUserId(userEmail);

                // BALANCE는 나중에 API로 변경 해야 함
                infBalance = dao.GetInfluencerBalanceByUserSeq(user.USER_SEQ);

                influencer.INF_USER = user;
                influencer.BALANCE = infBalance.BALANCE;

             }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return influencer;
        }
        #endregion
        #region Influencer Assign정보 수정
        public Int64 UpdateAssignInfluencer(InfUser user)
        {
            Int64 USER_SEQ = 0;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                USER_SEQ = dao.UpdateAssignInfluencer(user);
            }
            catch (Exception ex)
            {
                USER_SEQ = -1;
            }
            return USER_SEQ;
        }
        #endregion
        #region Admin 등록, 수정
        public Int64 InsertAdmin(InfUser user)
        {
            Int64 USER_SEQ = 0;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                USER_SEQ = dao.InsertAdmin(user);
            }
            catch (Exception ex)
            {
                USER_SEQ = -1;
            }
            return USER_SEQ;
        }

        public int UpdateAdmin(InfUser user)
        {
            int bizResult = -1;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                bizResult = dao.UpdateAdmin(user);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        public int UpdateAdminPhoto(InfUser user)
        {
            int bizResult = -1;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                bizResult = dao.UpdateAdminPhoto(user);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        #endregion

        #region Influencer 전체 리스트

        public int GetInfluencerAllCnt(int adminSeq)
        {
            int COUNT = 0;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                COUNT = dao.GetInfluencerAllCnt(adminSeq);
            }
            catch (Exception ex)
            {
                COUNT = -1;
            }
            return COUNT;
        }

        public IList<InfUser> GetInfluencerAll(string sort, int adminSeq, int pageNum, int pageSize)
        {
            IList<InfUser> list = null;
            try
            {
                var dao = new AdminUserDao();
                list = dao.GetInfluencerAll(sort, adminSeq, pageNum, pageSize);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }

        public IList<InfUser> GetNomatchInfluencerAll()
        {
            IList<InfUser> list = null;
            try
            {
                var dao = new AdminUserDao();
                list = dao.GetNomatchInfluencerAll();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        
        #endregion

        #region 활동중인 Admin 리스트
        public IList<InfAdmin> GetAdminAllActive()
        {
            IList<InfAdmin> list = null;
            try
            {
                var dao = new AdminUserDao();
                list = dao.GetAdminAllActive();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return list;
        }
        #endregion


        #region Admin 삭제
        public int DeleteUser(InfUser user)
        {
            int bizResult = -1;
            try
            {
                AdminUserDao dao = new AdminUserDao();
                bizResult = dao.DeleteUser(user);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        #endregion
    }
}
