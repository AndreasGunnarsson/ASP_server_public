using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    /// <summary>ViewModel used during login of a user.</summary>
    public class LoginUser 
    {
        [FromForm]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username cannot be empty.")]
        [StringLength(40, ErrorMessage = "The Username cannot exceed 40 characters.")]
        public string UserName { get; set; }
        
        [FromForm]
        [Required(ErrorMessage = "Password cannot be empty.")]
        [StringLength(100, ErrorMessage = "The password cannot exceed 100 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
