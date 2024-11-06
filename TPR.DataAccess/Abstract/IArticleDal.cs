﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.DataAccess;
using TPR.Entities.Concrete;

namespace TPR.DataAccess.Abstract
{
    public interface IArticleDal : IEntityRepository<Article>
    {
        Category GetCategory(Article article);
    }
}
