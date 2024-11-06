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
    public class TagManager : ITagService
    {
        ITagDal _tagDal;
        public TagManager(ITagDal tagDal)
        {
            _tagDal = tagDal;
        }

        public IResult Add(Tag tag)
        {
            _tagDal.Add(tag);
            return new SuccessResult();
        }

        public IResult AddToArticle(Article article, Tag tag)
        {
            _tagDal.AddToArticle(article, tag);
            return new SuccessResult();
        }

        public IResult Delete(Tag tag)
        {
            _tagDal.Delete(tag);
            return new SuccessResult();
        }

        public IResult DeleteArticleTags(Article article)
        {
            _tagDal.DeleteArticleTags(article);
            return new SuccessResult();
        }

        public IDataResult<List<Tag>> GetAll()
        {
            var data = _tagDal.GetAll();
            return new SuccessDataResult<List<Tag>>(data);
        }

        public IDataResult<Tag> GetById(int id)
        {
            var data = _tagDal.Get(x => x.Id == id);
            return new SuccessDataResult<Tag>(data);
        }

        public IDataResult<Tag> GetByName(string name)
        {
            var data = _tagDal.Get(x => x.Name == name);
            return new SuccessDataResult<Tag>(data);
        }

        public IDataResult<List<Tag>> GetTags(int count, bool shuffle = false)
        {
            var data = shuffle
                ? _tagDal.GetAll().OrderBy(x => Guid.NewGuid()).Take(count)
                : _tagDal.GetAll().Take(count);
            return new SuccessDataResult<List<Tag>>(data.ToList());
        }

        public IDataResult<List<Tag>> GetTagsByArticle(Article article)
        {
            var data = _tagDal.GetAllByArticle(article);
            return new SuccessDataResult<List<Tag>>(data);

        }

        public IResult Update(Tag tag)
        {
            _tagDal.Update(tag);
            return new SuccessResult();
        }
    }
}
