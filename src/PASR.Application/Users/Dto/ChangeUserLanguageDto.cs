using System.ComponentModel.DataAnnotations;

namespace PASR.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}