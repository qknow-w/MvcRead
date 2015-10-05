using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultConnection;

namespace MvcRead.IService
{
    public interface IDataService
    {
        dynamic Get(int curreent, int perPage, string queryString);
        dynamic ModifyData(bu_datum datum);
        dynamic DeleteData(bu_userDataMap userDataMap);
        int AddApply(bu_apply apply);
        dynamic GetApply(int curreent, int perPage);
        dynamic GetBySeq(string seq, string gene);
        dynamic GetById(int id);
        dynamic GetByAppID(int id);
        dynamic Uploaded(bu_datum datum, int userId);
    }
}
