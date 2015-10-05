using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultConnection;

namespace MvcRead.IService
{
    public interface IUserService
    {
        dynamic GetUser(int curreent, int perPage);
        dynamic AddUser(sys_user user);
        dynamic EditUser(sys_user user);
        dynamic DeleteUser(sys_user user);
        dynamic LoginIn(sys_user user);
        dynamic ModifyPassword(int userID, string pass);
    }
}
