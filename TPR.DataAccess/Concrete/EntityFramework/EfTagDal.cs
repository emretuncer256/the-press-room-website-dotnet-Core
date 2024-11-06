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
    public class EfTagDal : EfEntityRepositoryBase<Tag, ThePressRoomDBContext>, ITagDal
    {
        public void AddToArticle(Article article, Tag tag)
        {
            using (var context = new ThePressRoomDBContext())
            {
                context.ArticleTags.Add(new ArticleTag
                {
                    ArticleId = article.Id,
                    TagId = tag.Id,
                });
                context.SaveChanges();
            }
        }

        public void DeleteArticleTags(Article article)
        {
            using (var context = new ThePressRoomDBContext())
            {
                var deletedTags = (from articleTags in context.ArticleTags
                                   join tags in context.Tags
                                   on articleTags.TagId equals tags.Id
                                   where articleTags.ArticleId == article.Id
                                   select new ArticleTag
                                   {
                                       ArticleId = articleTags.ArticleId,
                                       TagId = articleTags.TagId,
                                       Id = articleTags.Id
                                   }
                 ).ToList();
                context.ArticleTags.RemoveRange(deletedTags);
                context.SaveChanges();
            }
        }

        public List<Tag> GetAllByArticle(Article article)
        {
            using (var context = new ThePressRoomDBContext())
            {
                var result = (from articleTags in context.ArticleTags
                              join tags in context.Tags
                              on articleTags.TagId equals tags.Id
                              where articleTags.ArticleId == article.Id
                              select new Tag
                              {
                                  Id = tags.Id,
                                  Name = tags.Name
                              }
                              ).ToList();
                return result;
            }
        }
    }
}
