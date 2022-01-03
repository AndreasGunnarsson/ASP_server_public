using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    /// <summary>ViewModel used to create a new account.</summary>
    public class CreateUser
    {
        [FromForm]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username cannot be empty.")]
        [StringLength(40, ErrorMessage = "The Username cannot exceed 40 characters.")]
        [UsernameAvailable]
        public string UserName { get; set; }
        
        [FromForm]
        [Required(ErrorMessage = "Password cannot be empty.")]
        [StringLength(100, ErrorMessage = "The password cannot exceed 100 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
