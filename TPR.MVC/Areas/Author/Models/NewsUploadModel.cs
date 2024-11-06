using TPR.Entities.Concrete;
using TPR.Business.Abstract;

namespace TPR.MVC.Areas.Author.Models
{
    public class NewsUploadModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Thumbnail { get; set; }
        public string? ThumbnailName { get; set; }
        public string Content { get; set; }
        public int[] Tags { get; set; }

        public static NewsUploadModel FromArticle(Article article, IArticleService _articleService)
        {
            return new NewsUploadModel
            {
                ArticleId = article.Id,
                Title = article.Title,
                CategoryId = article.CategoryId,
                Content = article.Content,
                Tags = (from t in _articleService.GetTags(article).Data
                        select t.Id).ToArray(),
                ThumbnailName = article.Thumbnail
            };
        }
    }
}
