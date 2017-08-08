using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F21.BLOGGER.WebApp.Common
{
    public class CallbackData
    {
        public bool IsSuccess { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public string ExceptioMessage { get; set; }
        public object ExtraData { get; set; }

        public string Url { get; set; }

        public CallbackData()
        {
            IsSuccess = false;
            Message = string.Empty;
        }

        public CallbackData(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public CallbackData(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public CallbackData(bool isSuccess, string message, object extraData)
        {
            IsSuccess = isSuccess;
            Message = message;
            ExtraData = extraData;
        }

        //public ResultData(bool isSuccess, int code, string successMessage = "")
        //{
        //    IsSuccess = isSuccess;
        //    ResultCode = code;
        //    if (IsSuccess)
        //    {
        //        Message = successMessage;
        //    }
        //    else
        //    {
        //        Message = GetMessageByCode(code);
        //    }
        //}

        //public static string GetMessageByCode(int code)
        //{
        //    string message = string.Empty;
        //    switch (code)
        //    {
        //        case -100: message = MessageCode.LOGIN_NOTEXISTS_USERID; break;
        //        case -101: message = MessageCode.LOGIN_NOTEXISTS_EMAIL; break;
        //        case -102: message = MessageCode.LOGIN_NOTAVAILABLE_USER; break;
        //        case -103: message = MessageCode.LOGIN_EXPIRE_PASSWORD; break;
        //        case -104: message = MessageCode.LOGIN_NOTMATCH_PASSWORD; break;
        //        case -110: message = MessageCode.FINDPASSWORD_NO_INFO; break;
        //        case -111: message = MessageCode.FINDPASSWORD_ALREADY_SENDMAIL; break;
        //        case -112: message = MessageCode.FINDPASSWORD_ERROR; break;
        //        case -120: message = MessageCode.PASSWORDRESET_ERROR; break;
        //        case -121: message = MessageCode.PASSWORDRESET_LINK_EXPIRED; break;
        //        case -122: message = MessageCode.PASSWORDRESET_LINK_ALREADY_USED; break;
        //        case -123: message = MessageCode.PASSWORDRESET_INVALID; break;
        //        case -124: message = MessageCode.PASSWORDRESET_DUPLICATE; break;
        //        case -130: message = MessageCode.MAIL_SEND_ERROR; break;
        //        case -140: message = MessageCode.BUYERCOMMENT_ADD_ERROR; break;
        //        case -141: message = MessageCode.BUYERCOMMENT_UPDATE_ERROR; break;
        //        case -142: message = MessageCode.BUYERCOMMENT_UPDATE_DIFFUSER; break;
        //        case -143: message = MessageCode.BUYERCOMMENT_DELETE_ERROR; break;
        //        case -144: message = MessageCode.BUYERCOMMENT_DELETE_DIFFUSER; break;
        //        case -145: message = MessageCode.BUYERCOMMENT_CONFIRM_ERROR; break;
        //        case -150: message = MessageCode.ORDER_CONFIRM_ERROR; break;
        //        //case -200: message = MessageCode.ORDER_PRIZE_SUSPENDED; break;
        //        //case -201: message = MessageCode.ORDER_PRIZE_OUTOFSTOCK; break;
        //        //case -202: message = MessageCode.ORDER_PRIZE_NOTEXISTS; break;
        //        //case -210: message = MessageCode.ORDER_NO_POINT; break;
        //        //case -220: message = MessageCode.ORDER_FAIL_POINT; break;
        //        //case -230: message = MessageCode.ORDER_ERROR; break;
        //        //case -240: message = MessageCode.ORDER_ERROR; break;
        //        //case -310: message = MessageCode.WISHLIST_DUPLICATE; break;
        //        default: message = MessageCode.GLABAL_ERROR_OCCURED; break;
        //    }
        //    return message;
        //}
    }

    public enum ResultType
    {
        LOGIN_NOTEXISTS_USERID = -100,
        LOGIN_NOTEXISTS_EMAIL = -101,
        LOGIN_NOTAVAILABLE_USER = -102,
        LOGIN_EXPIRE_PASSWORD = -103,
        LOGIN_NOTMATCH_PASSWORD = -104,
        FINDPASSWORD_NO_INFO = -110,
        FINDPASSWORD_ALREADY_SENDMAIL = -111,
        FINDPASSWORD_ERROR = -112,
        PASSWORDRESET_ERROR = -120,
        PASSWORDRESET_LINK_EXPIRED = -121,
        PASSWORDRESET_LINK_ALREADY_USED = -122,
        PASSWORDRESET_INVALID = -123,
        PASSWORDRESET_DUPLICATE = -124,
        MAIL_SEND_ERROR = -130,
        BUYERCOMMENT_ADD_ERROR = -140,
        BUYERCOMMENT_UPDATE_ERROR = -141,
        BUYERCOMMENT_UPDATE_DIFFUSER = -142,
        BUYERCOMMENT_DELETE_ERROR = -143,
        BUYERCOMMENT_DELETE_DIFFUSER = -144,
        BUYERCOMMENT_CONFIRM_ERROR = -145,
        ORDER_CONFIRM_ERROR = -150
    }

    public enum ResultCode
    {
        Undefined,
        UniqueConstraintViolation,//유니크 제약조건 위반
    }
}