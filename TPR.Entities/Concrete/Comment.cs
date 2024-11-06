using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Entities;

namespace TPR.Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
    }
}
