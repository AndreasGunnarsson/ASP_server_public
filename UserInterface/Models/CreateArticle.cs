using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    /// <summary>ViewModel used when editing/creating an article.</summary>
    public class CreateArticle
    {
        public int Id { get; set; }

        [FromForm]
        [Display(Name = "Title")]
        [Required]
        public string Titleee { get; set; }

        [FromForm]
        [Display(Name = "Text")]
        [Required]
        public string Articleee { get; set; }
    }
}
