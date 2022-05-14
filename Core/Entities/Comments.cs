using System;

namespace Core.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public int AccountsId { get; set; }
        public int? CommentsId { get; set; }
        public int ArticlesId { get; set; }
    }
}
