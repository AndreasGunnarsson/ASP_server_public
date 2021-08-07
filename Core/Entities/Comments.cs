using System;

namespace Core.Entities
{
    public class Comments
    {
        int Id { get; set; }
        string Comment { get; set; }
        DateTime CreateDate { get; set; }
        DateTime EditDate { get; set; }
        int AccountsId { get; set; }
        int CommentsId { get; set; }
        int ArticlesId { get; set; }
    }
}
