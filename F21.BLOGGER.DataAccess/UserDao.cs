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
    public class UserDao : DapperFactory
    {
        public UserDao() : base("INFLUENCER")
        { }

        #region Email이 시스템에 등록 된 계정인지 체크
        public int GetUserCountByUserId(string userEmail)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_EMAIL", userEmail, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_USER_CNT_Q", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }

        #endregion

        #region 사용자정보 조회
        public InfUser GetUserInfoByUserId(string userEmail)
        {
            InfUser user = null;
            try
            {
                var x = QuerySP<InfUser>("SP_INF_USER_Q", new
                {
                    USER_EMAIL = userEmail,
                });

                user = x.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return user;
        }
        #endregion

        #region Influencer BALANCE 조회
        public InfInfluencer GetInfluencerBalanceByUserSeq(Int64 userSeq)
        {
            InfInfluencer user = null;
            try
            {
                var x = QuerySP<InfInfluencer>("SP_INF_BALANCE_Q", new
                {
                    USER_SEQ = userSeq
                });

                user = x.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return user;
        }
        #endregion

        #region 로그인
        public InfUser LoginUser(string userEmail, string PassWord)
        {
            InfUser user = null;
            try
            {
                var x = QuerySP<InfUser>("SP_INF_USER_LOGIN_Q", new
                {
                    USER_EMAIL = userEmail,
                    USER_PASS = PassWord
                });

                user = x.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return user;
        }
        #endregion

        #region 사용자 등록
        public Int64 InsertUser(InfUser user)
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
                param.Add("@PHONE_NO", user.PHONE_NO, DbType.String, ParameterDirection.Input);
                param.Add("@INSTAGRAM_ID", user.INSTAGRAM_ID, DbType.String, ParameterDirection.Input);
                param.Add("@FOREVER21_ID", user.FOREVER21_ID, DbType.String, ParameterDirection.Input);
                param.Add("@PHOTO_URL", user.PHOTO_URL, DbType.String, ParameterDirection.Input);
                param.Add("@ABOUT_ME", user.ABOUT_ME, DbType.String, ParameterDirection.Input);
                param.Add("@CREATE_USER", user.CREATE_USER, DbType.String, ParameterDirection.Input);
                param.Add("@USER_SEQ", dbType: DbType.Int64, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_USER_I", param, commandType: CommandType.StoredProcedure);

                return param.Get<Int64>("@USER_SEQ");
            });
        }
        #endregion

        #region 사용자 정보 변경
        public int UpdateUser(InfUser user)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_SEQ", user.USER_SEQ, DbType.Int64, ParameterDirection.Input);
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@USER_PASS", user.USER_PASS, DbType.String, ParameterDirection.Input);
                param.Add("@FIRST_NM", user.FIRST_NM, DbType.String, ParameterDirection.Input);
                param.Add("@LAST_NM", user.LAST_NM, DbType.String, ParameterDirection.Input);
                param.Add("@PHONE_NO", user.PHONE_NO, DbType.String, ParameterDirection.Input);
                param.Add("@INSTAGRAM_ID", user.INSTAGRAM_ID, DbType.String, ParameterDirection.Input);
                param.Add("@PHOTO_URL", user.PHOTO_URL, DbType.String, ParameterDirection.Input);
                param.Add("@ABOUT_ME", user.ABOUT_ME, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_USER_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

        #region 사용자 상태 변경
        public int UpdateStatus(InfUser user)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@ACTION_EMAIL", user.ACTION_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@STATUS_CD", user.STATUS_CD, DbType.String, ParameterDirection.Input);
                param.Add("@REJECT", user.REJECT, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_USER_STATUS_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

        #region 사용자 비밀번호 변경
        public int UpdatePassword(InfUser user)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@USER_PASS", user.USER_PASS, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_USER_PASS_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

        #region 사용자 대표 사진 변경
        public int UpdatePhoto(InfUser user)
        {
            return Execute<int>(cn =>
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@USER_EMAIL", user.USER_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@ACTION_EMAIL", user.ACTION_EMAIL, DbType.String, ParameterDirection.Input);
                param.Add("@PHOTO_URL", user.PHOTO_URL, DbType.String, ParameterDirection.Input);
                param.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                cn.Execute("SP_INF_USER_PHOTO_URL_U", param, commandType: CommandType.StoredProcedure);

                return param.Get<int>("@RowsAffected");
            });
        }
        #endregion

    }
}
