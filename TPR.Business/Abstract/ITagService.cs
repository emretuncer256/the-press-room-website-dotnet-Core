using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Utilities.Results.Abstract;
using TPR.Entities.Concrete;

using IResult = TPR.Core.Utilities.Results.Abstract.IResult;

namespace TPR.Business.Abstract
{
    public interface ITagService
    {
        IResult Add(Tag tag);
        IResult AddToArticle(Article article, Tag tag);
        IResult DeleteArticleTags(Article article);
        IResult Update(Tag tag);
        IResult Delete(Tag tag);
        IDataResult<List<Tag>> GetAll();
        IDataResult<List<Tag>> GetTagsByArticle(Article article);
        IDataResult<Tag> GetById(int id);
        IDataResult<Tag> GetByName(string name);
        IDataResult<List<Tag>> GetTags(int count, bool shuffle = false);
    }
}
