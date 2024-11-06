using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.DataAccess.EntityFramework;
using TPR.DataAccess.Abstract;
using TPR.DataAccess.Concrete.EntityFramework.Context;
using TPR.Entities.Concrete;

namespace TPR.DataAccess.Concrete.EntityFramework
{
    public class EfArticleDal : EfEntityRepositoryBase<Article, ThePressRoomDBContext>, IArticleDal
    {
        public Category GetCategory(Article article)
        {
            using (var context = new ThePressRoomDBContext())
            {
                var result = from category in context.Categories
                             join newsArticle in context.Articles
                             on category.Id equals article.CategoryId
                             where article.CategoryId == category.Id
                             select new Category
                             {
                                 Id = category.Id,
                                 Name = category.Name,
                                 Description = category.Description,
                             };
                return result.SingleOrDefault();
            }
        }
    }
}
