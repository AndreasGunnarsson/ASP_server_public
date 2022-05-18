using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    /// <summary>ViewModel used for an article.</summary>
    public class UpdatePassword
    {
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
    }
}
