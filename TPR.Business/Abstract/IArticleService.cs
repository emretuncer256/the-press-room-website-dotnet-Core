using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Utilities.Results.Abstract;
using TPR.Entities.Concrete;

namespace TPR.Business.Abstract
{
    public interface IArticleService
    {
        IResult Add(Article article);
        IResult AddTag(Article article, Tag tag);
        IResult AddTags(Article article, List<Tag> tags);
        IResult DeleteAllTags(Article article);
        IResult AddWithTags(Article article, int[] tags);
        IResult Update(Article article);
        IResult Delete(Article article);
        IResult PublishArticle(int id);
        IResult UnpublishArticle(int id);
        IResult ApproveNews(int id);
        IDataResult<Article> GetById(int id);
        IDataResult<List<Tag>> GetTags(Article article);
        IDataResult<List<Article>> GetAll();
        IDataResult<List<Article>> GetAllByAuthorId(int id);
        IDataResult<List<Article>> GetFeaturedNews(int count, bool shuffle = false);
        IDataResult<List<Article>> GetLatestNews(int count);
        IDataResult<List<Article>> GetBreakingNews(int count);
        IDataResult<List<Article>> GetNotApprovedNews();
        IDataResult<Category> GetCategory(Article article);
        IResult UpdateWithTags(Article article, int[] tags);
        IResult ToggleFeaturedNews(int id);
        IResult ToggleBreakingNews(int id);
        IResult NewsHited(Article article);
        IDataResult<Article> GetNewsById(int id);
        IDataResult<List<Comment>> GetComments(Article article);
        IResult MakeComment(Comment comment);
        IDataResult<List<Article>> GetNewsByCategoryWithCount(Category category, int count);
    }
}
