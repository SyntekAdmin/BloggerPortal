using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace F21.BLOGGER.Web.Models
{
    // AccountController 작업에서 반환된 모델입니다.
    public class LoginViewModel
    {
        [Required(ErrorMessage = "아이디를 입력해 주세요.")]
        [Display(Name = "아이디")]
        public string Id { get; set; }

        [Required(ErrorMessage = "패스워드를 입력해 주세요.")]
        [DataType(DataType.Password)]
        [Display(Name = "패스워드")]
        public string Password { get; set; }
    }
}
