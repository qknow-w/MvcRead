using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultConnection;

namespace MvcRead.IService
{
    public interface ILogService
    {
        dynamic GetLog(int curreent, int perPage);
        dynamic AddLog(sys_log log);
    }
}
