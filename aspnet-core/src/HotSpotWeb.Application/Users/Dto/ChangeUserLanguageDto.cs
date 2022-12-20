using System.ComponentModel.DataAnnotations;

namespace HotSpotWeb.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}