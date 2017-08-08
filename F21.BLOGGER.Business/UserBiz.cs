using F21.BLOGGER.DataAccess;
using F21.BLOGGER.Model;
using Stis.Data;
using Stis.Data.Attributes;
using System;


namespace F21.BLOGGER.Business
{
    [Logger]
    public class UserBiz : BusinessFactory
    {
        #region Email이 시스템에 등록 된 계정인지 체크
        public int GetUserCountByUserId(string userEmail)
        {
            int bizResult = -1;
            try
            {
                UserDao dao = new UserDao();
                bizResult = dao.GetUserCountByUserId(userEmail);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }

        #endregion

        #region 사용자정보 조회
        public InfUser GetUserInfoByUserId(string userEmail)
        {
            InfUser user = null;
            try
            {
                UserDao dao = new UserDao();
                user = dao.GetUserInfoByUserId(userEmail);
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
                UserDao dao = new UserDao();
                user = dao.LoginUser(userEmail, PassWord);
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
            Int64 USER_SEQ = 0;
          
                UserDao dao = new UserDao();
                USER_SEQ = dao.InsertUser(user);
            
            return USER_SEQ;
        }
        #endregion

        #region 사용자 정보 변경
        public int UpdateUser(InfUser user)
        {
            int bizResult = -1;
            try
            {
                UserDao dao = new UserDao();
                bizResult = dao.UpdateUser(user);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        #endregion

        #region 사용자 상태 변경
        public int UpdateStatus(InfUser user)
        {
            int bizResult = -1;
            try
            {
                UserDao dao = new UserDao();
                bizResult = dao.UpdateStatus(user);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        #endregion

        #region 사용자 비밀번호 변경 (단순 profile에서 변경)
        public int UpdatePassword(InfUser user)
        {
            int bizResult = -1;
            try
            {
                UserDao dao = new UserDao();
                bizResult = dao.UpdatePassword(user);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        #endregion

        #region 사용자 비밀번호 변경 (Reset Password 에서 변경)
        public int UpdatePasswordAndStatus(InfUser user)
        {
            int bizResult = -1;
            try
            {
                this.TransactionScope(x =>
                {
                    UserDao dao = new UserDao();
                    bizResult = dao.UpdatePassword(user);

                    if (bizResult > 0)
                    {
                        user.STATUS_CD = "Reactivate";
                        bizResult = dao.UpdateStatus(user);
                    }
                });

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return bizResult;
        }
        #endregion

        #region 사용자 대표 사진 변경
        public int UpdatePhoto(InfUser user)
        {
            int bizResult = -1;
            try
            {
                UserDao dao = new UserDao();
                bizResult = dao.UpdatePhoto(user);
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
