using System.ComponentModel.DataAnnotations;

namespace SeizeTheDay.DataDomain.ViewModels
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "{0} Invalid E-Mail !"),
        EmailAddress(ErrorMessage = "{0} Invalid e-mail adress"),
        StringLength(70, ErrorMessage = "{0} max {1} must be character.")]
        public string Email { get; set; }
    }
}
