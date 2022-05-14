using System.Collections.Generic;
using Core.Entities;

namespace UserInterface.Models
{
    /// <summary>ViewModel used for an article.</summary>
    public class Article
    {
        public IEnumerable<Articles> articles { get; set; }
    }
}
