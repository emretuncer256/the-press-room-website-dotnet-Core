using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.DataAccess;
using TPR.Entities.Concrete;

namespace TPR.DataAccess.Abstract
{
    public interface ITagDal : IEntityRepository<Tag>
    {
        void AddToArticle(Article article, Tag tag);
        void DeleteArticleTags(Article article);
        List<Tag> GetAllByArticle(Article article);
    }
}
