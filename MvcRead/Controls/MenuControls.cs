using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcRead.Factoty;
using MvcRead.IFactory;
using MvcRead.Service;

namespace MvcRead.Controls
{
    public class MenuControls
    {
        public static MvcHtmlString Menu()
        {

            //string str = ToTreeData();

            return new MvcHtmlString(ToTreeData());

        }

        public static string ToTreeData()
        {
            IMenuFactory MenuFactory = new MenuFacoty();
            //List<Menu> list = (List<Menu>)MenuFactory.CreatMenuService().Get(1);
            List<Menu> list = (List<Menu>)MenuFactory.CreatMenuService().Get((int)HttpContext.Current.Session["UserID"]);
            List<Menu> listParent = list.Where(a => a.ParentID == 0).ToList();
            string menulist = "";
            
            for (int i = 0; i < listParent.Count; i++)
            {
                string childMenu = "";
                List<Menu> listChildren = list.Where(a => a.ParentID == listParent[i].MenuID).Where(b=>b.isEnble==true).ToList();
                childMenu = listChildren.Aggregate(childMenu, (current, c) => current + string.Format("<li><i class='glyphicon glyphicon-map-marker'></i> <a href='{0}' >{1}</a></li>", c.URL, c.MenuName));
                menulist += string.Format( "<div class='panel panel-default'>"+
                    "<div class='panel-heading'>"+
                      "<h4 class='panel-title'>"  +
                        "<a data-toggle='collapse'  data-parent='#accordion'" +  
                         "href='#collapse{0}'>{1}  </a></h4>  </div>" +                          
                   "<div id='collapse{2}' class='panel-collapse collapse  '>" +
                     "<div class='panel-body'>"+
                     " <ul>{3}"+
                       " </ul>"   +
                     "</div>"   +
                  "</div>"  +
             " </div>",i, listParent[i].MenuName, i,childMenu);


            }
            return menulist;
        }

    }
}