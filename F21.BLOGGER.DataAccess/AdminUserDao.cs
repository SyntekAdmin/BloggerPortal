using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stis.Data;
using System.Data.SqlClient;
using System.Data;
using Stis.Data.DapperNet;
using F21.BLOGGER.Model;
using Dapper;

namespace F21.BLOGGER.DataAccess
{
    public class AdminUserDao : DapperFactory
    {
        public AdminUserDao() : base("INFLUENCER")
        { }

        #region Admin 전체 리스트

        public int GetAdminAllCnt()
        {
            int list = 0;
            try
            {
                var x = QuerySP<InfAdmin>("SP_ADM_ADMINS_CNT_Q", new { });

                list = x[0].CNT;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        public IList<InfAdmin> GetAdminAll(string sort, int pageNum, int pageSize)
        {
            IList<InfAdmin> list = null;
            try
            {
                var x = QuerySP<InfAdmin>("SP_ADM_ADMINS_Q", new {
                    SORT = sort,
                    PAGE_NUM = pageNum,
                    PAGE_SIZE = pageSize
                });

                list = x;

                foreach (var t in list)
                {
                    t.JOIN_DATE = Common.ShortDate(t.JOIN_DATE);
                    t.APPLIED_DATE = Common.ShortDate(t.APPLIED_DATE);
                    t.LAST_SIGN_IN = Common.ShortDate(t.LAST_SIGN_IN);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }
        #endregion

        #region Admin 상세
        public virtual InfAdmin GetAdmin(Int64 userSeq)
        {

            InfAdmin data = null;
            try
            {
                var x = QuerySP<InfAdmin>("SP_ADM_ADMINS_DETAIL_Q", new
                {
                    USER_SEQ = userSeq,
                });

                data = x.FirstOrDefault();
                if (data != null)
                {
                    data.JOIN_DATE = Common.ShortDate(data.JOIN_DATE);
                    data.APPLIED_DATE = Common.ShortDate(data.APPLIED_DATE);
                    data.LAST_SIGN_IN = Common.ShortDate(data.LAST_SIGN_IN);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return data;
        }

        #region Influencer에 Admin 할당 및 해제
        public int UpdateInfluencerAdmin(InfUser user)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@ACTION_EMAIL", user.ACTION_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@ADMIN_SEQ", user.ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_ADM_ADMIN_INFLUENCER_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

        #endregion
        
        #region Influencer Assign 정보 수정
        public Int64 UpdateAssignInfluencer(InfUser user)
        {
            return Execute<Int64>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_SEQ", user.USER_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@STATUS_CD", user.STATUS_CD, DbType.String, ParameterDirection.Input);
                param.Add("@ADMIN_SEQ", user.ADMIN_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@CREATE_USER", user.CREATE_USER, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_ADM_ADMIN_INFLUENCER_ASSIGN_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion
        #region Admin 등록, 수정
        public Int64 InsertAdmin(InfUser user)
        {
            return Execute<Int64>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@USER_PASS", user.USER_PASS, DbType.String, ParameterDirection.Input);
                param.Add("@ROLE", user.ROLE, DbType.String, ParameterDirection.Input);
                param.Add("@STATUS_CD", user.STATUS_CD, DbType.String, ParameterDirection.Input);
                param.Add("@USE_CD", user.USE_CD, DbType.String, ParameterDirection.Input);
                param.Add("@FIRST_NM", user.FIRST_NM, DbType.String, ParameterDirection.Input);
                param.Add("@LAST_NM", user.LAST_NM, DbType.String, ParameterDirection.Input);
                param.Add("@PHOTO_URL", user.PHOTO_URL, DbType.String, ParameterDirection.Input);
                param.Add("@CREATE_USER", user.CREATE_USER, DbType.String, ParameterDirection.Input);
                param.Add("@INFLUENCER", user.INFLUENCER, DbType.String, ParameterDirection.Input);                
                param.Add("@USER_SEQ", dbType: DbType.Int64, direction: ParameterDirection.Output);
                cn.Execute("SP_ADM_ADMIS_I", param, commandType: CommandType.StoredProcedure);

                return param.Get<Int64>("@USER_SEQ");
            });
        }

        public int UpdateAdmin(InfUser user)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_SEQ", user.USER_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@USER_PASS", user.USER_PASS, DbType.String, ParameterDirection.Input);
                param.Add("@FIRST_NM", user.FIRST_NM, DbType.String, ParameterDirection.Input);
                param.Add("@LAST_NM", user.LAST_NM, DbType.String, ParameterDirection.Input);
                param.Add("@PHOTO_URL", user.PHOTO_URL, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_ADM_ADMINS_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        public int UpdateAdminPhoto(InfUser user)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_SEQ", user.USER_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@PHOTO_URL", user.PHOTO_URL, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_ADM_ADMINS_PHOTO_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

        #region Influencer 전체 리스트

        public int GetInfluencerAllCnt(int adminSeq)
        {
            int list = 0;
            try
            {
                var x = QuerySP<InfUser>("SP_ADM_INFLUENCERS_CNT_Q", new {
                    ADMIN_SEQ = adminSeq
                });

                list = x[0].CNT;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return list;
        }

        public IList<InfUser> GetInfluencerAll(string sort, int adminSeq, int pageNum, int pageSize)
        {
            IList<InfUser> list = null;
            
            try
            {
                var x = QuerySP<InfUser>("SP_ADM_INFLUENCERS_Q", new
                {
                    SORT = sort,
                    ADMIN_SEQ = adminSeq,
                    PAGE_NUM = pageNum,
                    PAGE_SIZE = pageSize
                });

                list = x;
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
                var x = QuerySP<InfUser>("SP_ADM_INFLUENCERS_NOMATCH_Q", new
                {});

                list = x;
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
                var x = QuerySP<InfAdmin>("SP_ADM_ADMINS_ACTIVE_Q", new
                {});

                list = x;

                foreach (var t in list)
                {
                    t.ADMIN_NAME = t.FIRST_NM + " " + t.LAST_NM + " (" + t.INFLUENCERS.ToString() + ")";
                    t.JOIN_DATE = Common.ShortDate(t.JOIN_DATE);
                    t.APPLIED_DATE = Common.ShortDate(t.APPLIED_DATE);
                    t.LAST_SIGN_IN = Common.ShortDate(t.LAST_SIGN_IN);
                }

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
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@ACTION_EMAIL", user.ACTION_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_USER_D", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

    }
}
