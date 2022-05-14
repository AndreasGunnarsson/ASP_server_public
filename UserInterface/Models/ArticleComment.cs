using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    /// <summary>ViewModel used for a specific article.</summary>
    public class ArticleComment
    {
        [FromForm]
        [Required]
        [HiddenInput]
        public int Articleeeid { get; set; }

        [FromForm]
        [StringLength(200, ErrorMessage = "The message cannot exceed 200 characters.")]
        [Required(ErrorMessage = "Comment required.")]
        public string Commentzz { get; set; }

        [FromForm]
        [HiddenInput]
        public int? CommentsIdzz { get; set; }
    }
}
