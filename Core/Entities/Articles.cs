using System;

namespace Core.Entities
{
    public class Articles
    {
        int Id { get; set; }
        string Title { get; set; }
        DateTime CreateDate { get; set; }
        DateTime EditDate { get; set; }
    }
}
