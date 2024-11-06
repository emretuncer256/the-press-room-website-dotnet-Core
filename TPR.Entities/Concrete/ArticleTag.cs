using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Entities;

namespace TPR.Entities.Concrete
{
    public class ArticleTag : IEntity
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int TagId { get; set; }
    }
}
