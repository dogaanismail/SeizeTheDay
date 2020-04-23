using System.ComponentModel.DataAnnotations;

namespace SeizeTheDay.DataDomain.Enumerations
{
    public enum ApiStatusEnum
    {
        [Display(Name = "Ok")]
        Ok = 200,

        [Display(Name = "BadRequest")]
        BadRequest = 400,

        [Display(Name = "Forbidden")]
        Forbidden = 403,

        [Display(Name = "NotFound")]
        NotFound = 404,
    }
}
