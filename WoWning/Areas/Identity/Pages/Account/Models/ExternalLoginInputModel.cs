using System.ComponentModel.DataAnnotations;

namespace WoWning.Areas.Identity.Pages.Account.Models
{
    public class ExternalLoginInputModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
