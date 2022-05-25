using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    /// <summary>ViewModel used for an article.</summary>
    public class UpdatePassword
    {
        [Display(Name = "Old password")]
        [Required]
        [StringLength(50)]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        [Required]
        [StringLength(50)]
        public string NewPassword { get; set; }
    }
}
