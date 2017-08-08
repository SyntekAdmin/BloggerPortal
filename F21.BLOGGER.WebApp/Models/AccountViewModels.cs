#region Using

using System.ComponentModel.DataAnnotations;
using System.Web;

#endregion

namespace F21.BLOGGER.WebApp.Models
{
    public class AccountLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
        public string ErrorMessege { get; set; }
    }

    public class AccountForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class AccountResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }

    public class AccountRegistrationModel
    {
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string PhotoUrl { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Compare("Email")]
        public string EmailConfirm { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string forever21Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }

        [Required]
        public string InstagramId { get; set; }

        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }

    }

}