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
    public class EfCommentDal : EfEntityRepositoryBase<Comment, ThePressRoomDBContext>, ICommentDal
    {
        public List<Comment> GetCommentsByArticle(Article article)
        {
            using (var context = new ThePressRoomDBContext())
            {
                var result = from comments in context.Comments
                             join articles in context.Articles
                             on comments.ArticleId equals articles.Id
                             where articles.Id == article.Id
                             select new Comment
                             {
                                 Id = comments.Id,
                                 ArticleId = comments.Id,
                                 CreatedAt = comments.CreatedAt,
                                 Status = comments.Status,
                                 Text = comments.Text,
                                 UserId = comments.UserId
                             };
                return result.ToList();
            }
        }
    }
}
