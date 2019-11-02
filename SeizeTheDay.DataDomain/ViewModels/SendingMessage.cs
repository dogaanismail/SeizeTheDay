using System.ComponentModel.DataAnnotations;

namespace SeizeTheDay.DataDomain.ViewModels
{
    public class SendingMessage
    {
        [Required(ErrorMessage = "{0} Invalid username !")]
        public string Text { get; set; }

        [Required(ErrorMessage = "{0} Invalid receiver !")]
        public string Receiver { get; set; }
    }
}
