using System.Collections.Generic;
using LinkCraft.Models.Interfaces;

namespace LinkCraft.Models
{
    public class ResultListModel : ResultBaseModel<IEnumerable<IBaseExperience>>
    {
        public ResultListModel(IEnumerable<IBaseExperience> results) : base(results)
        {
            if (results == null)
            {
                Result = new List<IBaseExperience>();
            }
        }

        public ResultListModel(string errors) : base(errors)
        {
        }
    }
}
