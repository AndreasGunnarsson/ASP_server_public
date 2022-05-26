using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    /// <summary>ViewModel used for an article.</summary>
    public class UpdatePassword
    {
        [FromForm]
        [Display(Name = "Old password")]
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [FromForm]
        [Display(Name = "New password")]
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
