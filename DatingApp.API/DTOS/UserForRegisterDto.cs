using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOS
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4, ErrorMessage="you must specify password between 4 to 8 characters")]
        public string Password { get; set; }
    }
}