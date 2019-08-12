using System.ComponentModel.DataAnnotations;

namespace WoWning.Areas.Identity.Pages.Account.Manage.Models
{
    public class DeletePersonalDataInputModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}