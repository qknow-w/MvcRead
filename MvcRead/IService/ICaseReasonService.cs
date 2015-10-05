using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultConnection;

namespace MvcRead.IService
{
    public interface ICaseReasonService
    {
        List<ww_CaseReason> Get();
        List<ww_CaseReason> GetById(int id);
    }
}
