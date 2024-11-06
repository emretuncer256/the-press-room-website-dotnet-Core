using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Utilities.Results.Abstract;
using TPR.Entities.Concrete;

namespace TPR.Business.Abstract
{
    public interface ICategoryService
    {
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
        IDataResult<Category> GetById(int id);
        IDataResult<Category> GetByName(string name);
        IDataResult<List<Category>> GetCategories(int count, bool shuffle = false);
        IDataResult<List<Category>> GetAll();
    }
}
