using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultConnection;

namespace MvcRead.IService
{
     public interface IDepartmentService
    {
         dynamic Get();
         dynamic EditDepart(sys_department department);
         dynamic DeleteDepart(sys_department department);
         dynamic AddDepart(sys_department department);
    }
}
