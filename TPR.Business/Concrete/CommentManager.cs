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
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public IResult Add(Comment comment)
        {
            _commentDal.Add(comment);
            return new SuccessResult("Comment sent successfully.");
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Comment>> GetAllCommentsByArticle(Article article)
        {
            var data = _commentDal.GetCommentsByArticle(article);
            return new SuccessDataResult<List<Comment>>(data);
        }

        public IResult Update(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
