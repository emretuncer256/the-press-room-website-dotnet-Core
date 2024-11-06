using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Business.Abstract;
using TPR.Core.Utilities.Results;
using TPR.Core.Utilities.Results.Abstract;
using TPR.DataAccess.Abstract;
using TPR.Entities.Concrete;

namespace TPR.Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        IArticleDal _articleDal;
        ITagService _tagService;
        ICommentService _commentService;
        public ArticleManager(IArticleDal articleDal, ITagService tagService, ICommentService commentService)
        {
            _articleDal = articleDal;
            _tagService = tagService;
            _commentService = commentService;
        }

        public IResult Add(Article article)
        {
            _articleDal.Add(article);
            return new SuccessResult();
        }

        public IResult AddTag(Article article, Tag tag)
        {
            _tagService.AddToArticle(article, tag);
            return new SuccessResult($"{tag.Name} tag added the article with title {article.Title}");
        }

        public IResult AddTags(Article article, List<Tag> tags)
        {
            foreach (var tag in tags)
            {
                _tagService.AddToArticle(article, tag);
            }
            return new SuccessResult();
        }

        public IResult AddWithTags(Article article, int[] tags)
        {
            _articleDal.Add(article);
            var tagsList = new List<Tag>();
            foreach (var tag in tags)
            {
                tagsList.Add(_tagService.GetById(tag).Data);
            }
            AddTags(article, tagsList);
            return new SuccessResult();
        }

        public IResult ApproveNews(int id)
        {
            var article = _articleDal.Get(x => x.Id == id);
            if (article != null && !article.IsApproved)
            {
                article.IsApproved = true;
                _articleDal.Update(article);
                return new SuccessResult("The News Approved Successfully.");
            }
            return new ErrorResult("News Already Approved");
        }

        public IResult Delete(Article article)
        {
            _articleDal.Delete(article);
            return new SuccessResult($"{article.Title} has been deleted successfully.");
        }

        public IResult DeleteAllTags(Article article)
        {
            _tagService.DeleteArticleTags(article);
            return new SuccessResult();
        }

        public IDataResult<List<Article>> GetAll()
        {
            var data = _articleDal.GetAll();
            return new SuccessDataResult<List<Article>>(data);
        }

        public IDataResult<List<Article>> GetAllByAuthorId(int id)
        {
            var data = _articleDal.GetAll(x => x.UserId == id);
            return new SuccessDataResult<List<Article>>(data);
        }

        public IDataResult<List<Article>> GetBreakingNews(int count)
        {
            var data = _articleDal.GetAll(x => (x.IsBreakingNews && x.IsPublished && x.IsApproved))
                .OrderByDescending(x => x.CreatedAt).Take(count).ToList();
            return new SuccessDataResult<List<Article>>(data);
        }

        public IDataResult<Article> GetById(int id)
        {
            var data = _articleDal.Get(x => x.Id == id);
            return new SuccessDataResult<Article>(data);
        }

        public IDataResult<Category> GetCategory(Article article)
        {
            var data = _articleDal.GetCategory(article);
            return new SuccessDataResult<Category>(data);
        }

        public IDataResult<List<Comment>> GetComments(Article article)
        {
            var data = _commentService.GetAllCommentsByArticle(article).Data;
            return new SuccessDataResult<List<Comment>>(data);
        }

        public IDataResult<List<Article>> GetFeaturedNews(int count, bool shuffle = false)
        {
            var data = shuffle
                ? _articleDal.GetAll(x => (x.IsFeatured && x.IsPublished && x.IsApproved)).OrderBy(x => Guid.NewGuid()).Take(count)
                : _articleDal.GetAll(x => (x.IsFeatured && x.IsPublished && x.IsApproved)).Take(count);
            return new SuccessDataResult<List<Article>>(data.ToList());
        }

        public IDataResult<List<Article>> GetLatestNews(int count)
        {
            var data = _articleDal.GetAll(x => (x.IsApproved && x.IsPublished)).OrderByDescending(x => x.CreatedAt).Take(count);
            return new SuccessDataResult<List<Article>>(data.ToList());
        }

        public IDataResult<List<Article>> GetNewsByCategoryWithCount(Category category, int count)
        {
            var data = _articleDal.GetAll(x => (x.CategoryId == category.Id && x.IsApproved && x.IsPublished)).OrderByDescending(x => x.CreatedAt).Take(count);
            return new SuccessDataResult<List<Article>>(data.ToList());
        }

        public IDataResult<Article> GetNewsById(int id)
        {
            var article = _articleDal.Get(x => x.Id == id);
            if (article.IsPublished && article.IsApproved) return new SuccessDataResult<Article>(article);
            return new ErrorDataResult<Article>();
        }

        public IDataResult<List<Article>> GetNotApprovedNews()
        {
            var data = _articleDal.GetAll(x => (x.IsApproved == false && x.IsPublished == true));
            return new SuccessDataResult<List<Article>>(data);
        }

        public IDataResult<List<Tag>> GetTags(Article article)
        {
            var data = _tagService.GetTagsByArticle(article).Data;
            return new SuccessDataResult<List<Tag>>(data);
        }

        public IResult MakeComment(Comment comment)
        {
            return _commentService.Add(comment);
        }

        public IResult NewsHited(Article article)
        {
            if(article.Hits != null)
            {
                article.Hits += 1;
            }
            else
            {
                article.Hits = 1;
            }
            _articleDal.Update(article);
            return new SuccessResult();
        }

        public IResult PublishArticle(int id)
        {
            var article = _articleDal.Get(x => x.Id == id);
            if (article != null)
            {
                if (article.IsPublished) return new ErrorResult("The news has already been published");
                article.IsPublished = true;
                _articleDal.Update(article);
                return new SuccessResult("The News has been published successfully. Please wait until admins confirm.");
            }
            return new ErrorResult("News Not Found");
        }

        public IResult ToggleBreakingNews(int id)
        {
            var article = _articleDal.Get(x => x.Id == id);
            article.IsBreakingNews = !article.IsBreakingNews;
            _articleDal.Update(article);
            return article.IsBreakingNews ? new SuccessResult("The news is breaking news now.") : new SuccessResult("The news is not breaking news now.");
        }

        public IResult ToggleFeaturedNews(int id)
        {
            var article = _articleDal.Get(x => x.Id == id);
            article.IsFeatured = !article.IsFeatured;
            _articleDal.Update(article);
            return article.IsFeatured ? new SuccessResult("The news is featured now.") : new SuccessResult("The news is not featured now.");
        }

        public IResult UnpublishArticle(int id)
        {
            var article = _articleDal.Get(x => x.Id == id);
            if (article != null)
            {
                if (!article.IsPublished) return new ErrorResult("The news has already been unpublished");
                article.IsPublished = false;
                _articleDal.Update(article);
                return new SuccessResult("The News has been unpublished successfully.");
            }
            return new ErrorResult("News Not Found");
        }

        public IResult Update(Article article)
        {
            _articleDal.Update(article);
            return new SuccessResult();
        }

        public IResult UpdateWithTags(Article article, int[] tags)
        {
            DeleteAllTags(article);
            foreach (var tagId in tags)
            {
                _tagService.AddToArticle(article, _tagService.GetById(tagId).Data);
            }
            _articleDal.Update(article);
            return new SuccessResult($"Article {article.Title} wiht Id {article.Id} updated");
        }
    }
}
