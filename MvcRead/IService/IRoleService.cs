using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultConnection;
using Newtonsoft.Json.Linq;

namespace MvcRead.IService
{
    public interface IRoleService
    {
        dynamic Get(int curreent , int perPage);
        dynamic ChangeRoleMenu(dynamic jsonObject);       
        dynamic ModifyRole(sys_role role);
        dynamic DelectRole(sys_role role);
        dynamic AddRole(sys_role role);
        dynamic Gett();
    }
}
