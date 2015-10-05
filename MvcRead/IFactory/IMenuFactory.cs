using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcRead.IService;

namespace MvcRead.IFactory
{
    public interface IMenuFactory
    {
        IMenuService CreatMenuService();
    }
}