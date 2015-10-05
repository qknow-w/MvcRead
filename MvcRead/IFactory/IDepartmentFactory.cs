using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcRead.IService;

namespace MvcRead.IFactory
{
     public interface IDepartmentFactory
     {
         IDepartmentService CreatService();
     }
}
