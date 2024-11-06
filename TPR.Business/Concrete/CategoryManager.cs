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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult();
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult();
        }

        public IDataResult<List<Category>> GetAll()
        {
            var data = _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(data);
        }

        public IDataResult<Category> GetById(int id)
        {
            var data = _categoryDal.Get(x => x.Id == id);
            return new SuccessDataResult<Category>(data);
        }

        public IDataResult<Category> GetByName(string name)
        {
            var data = _categoryDal.Get(x => x.Name == name);
            return new SuccessDataResult<Category>(data);
        }

        public IDataResult<List<Category>> GetCategories(int count, bool shuffle = false)
        {
            var data = shuffle
                ? _categoryDal.GetAll().OrderBy(x => Guid.NewGuid()).Take(count)
                : _categoryDal.GetAll().Take(count);
            return new SuccessDataResult<List<Category>>(data.ToList());
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult();
        }
    }
}
