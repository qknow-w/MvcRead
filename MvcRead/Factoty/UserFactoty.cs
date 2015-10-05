using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcRead.IFactory;
using MvcRead.IService;
using MvcRead.Service;

namespace MvcRead.Factoty
{
    public class UserFactoty:IUserFactoty
    {
        public IUserService CreatUserService()
        {
            return new UserService();
        }
    }
}