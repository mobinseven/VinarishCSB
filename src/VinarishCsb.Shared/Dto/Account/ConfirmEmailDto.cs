using System.ComponentModel.DataAnnotations;

namespace VinarishCsb.Shared.Dto
{

    public class ConfirmEmailDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
