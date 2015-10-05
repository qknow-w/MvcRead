using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRead.Areas.Sys.Models
{
    public class TreeNode
    {
        public int  ID { get; set; }
        public int parentID { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string color { get; set; }
        public string backColor { get; set; }
        public string href { get; set; }
        public int tags { get; set; }
    }
}