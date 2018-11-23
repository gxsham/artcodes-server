using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkCraft.Models.Interfaces;

namespace LinkCraft.Models
{
    public class ResultModel : ResultBaseModel<IBaseExperience>
    {
        public ResultModel(IBaseExperience results) : base(results)
        {
        }
        public ResultModel(string errors) : base(errors)
        {
        }
    }
}
