using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Utilities.Results.Abstract;

namespace TPR.Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult? Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
