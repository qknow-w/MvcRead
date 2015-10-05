using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRead.IService
{
    public interface IMenuService
    {
        dynamic Get(int userID);
        dynamic GetByRoleID(int RoleID);
    }
}
