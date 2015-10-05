using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcRead.IFactory;
using MvcRead.IService;
using MvcRead.Service;

namespace MvcRead.Factoty
{
    public class DataFactoty:IDataFactoty
    {
        public IDataService CreatDataService()
        {
            return new DataService(); 
        }
    }
}