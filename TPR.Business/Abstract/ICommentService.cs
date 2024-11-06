using TPR.Core.Utilities.Results.Abstract;
using TPR.Entities.Concrete;

namespace TPR.Business.Abstract
{
    public interface ICommentService
    {
        IResult Add(Comment comment);
        IResult Update(Comment comment);
        IResult Delete(int id);
        IDataResult<List<Comment>> GetAllCommentsByArticle(Article article);
    }
}
