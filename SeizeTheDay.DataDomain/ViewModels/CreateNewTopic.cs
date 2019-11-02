using System.ComponentModel.DataAnnotations;

namespace SeizeTheDay.DataDomain.ViewModels
{
    public class CreateNewTopic
    {
        [Required(ErrorMessage = "{0} Invalid title !")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} Invalid content !")]
        public string Content { get; set; }
    }
}
