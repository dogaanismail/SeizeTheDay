using System.ComponentModel.DataAnnotations;

namespace SeizeTheDay.DataDomain.ViewModels
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "{0} Invalid password !"),
        DataType(DataType.Password),
        StringLength(30, ErrorMessage = "{0} max {1} must be character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} Invalid password !"),
        DataType(DataType.Password),
        StringLength(30, ErrorMessage = "{0} max {1} must be character."),
        Compare("Password", ErrorMessage = "{0} with {1} are not equal")]
        public string RePassword { get; set; }
    }
}
