
var wrapper = {};
//初始化 
wrapper.init = function () {
    $.ajax({ type: 'GET', url: '/api/sys/MenuApi/get', success: wrapper.initMenu }); //ajax到/sys/api/menu 获取当前用户所拥有的权限菜单
    

};
wrapper.initMenu=function(d) {
    // alert(d);
    if (!d || !d.length) {
        alert("系统提示:您没有任何权限！请联系管理员.");
        location.href = "/admin/login";
        return false;
    }

    var visibleMenu = $.grep(d, function (row) { return row.IsVisible; });//筛选出权限中被隐藏了的
    var menus = utils.toTreeData(visibleMenu, 'MenuID', 'ParentID', 'children'); //生成树形
    var tree = "[";
    $.each(menus, function(i, n) {
        tree += '{text:"' + n.MenuName + '"},';
    });
    
    //console.log(tree);
    tree=tree.substring(0, tree.length-1);
    tree += "]";
   // var node = $.parseJSON(tree);
   // console.log(node);
    return tree;
    //alert(menus[0].MenuName);

    //$.each(menus, function (i, n) {
    //    var menulist = "";
    //    var childMenu = '';
    //    if ((n.children || []).length > 0) {
    //        childMenu = '';
    //        childMenu += ' <ul class="nav nav-pills nav-stacked"  style="display: none;">';
    //        childMenu += wrapper.menuButtonChild(n); //生成子菜单
    //        childMenu += '</ul>';
    //    }
    //    menulist = utils.formatString('<li class="{0}"><a href="#" ><i class="{1}"></i><span>{2}</span></a>{3}</li>',
    //      n.LiClass, n.Class, n.MenuName,childMenu);


    //    $('#menu').append(menulist);

    //});

}
  //var viewModel=function() {
  //    var self = this;
  //    console.log("111");
  //    self.availableDepartment = ko.observableArray();
  //    $.ajax({
  //        type: "get",
  //        url: "/api/sys/DepartmentApi/get",
  //        async:false,
  //        success: function (data) {
  //            self.availableDepartment.removeAll();
  //           // self.availableDepart.push(new Depart(0, "最高级"));
  //            $.each(data, function (i, n) {
  //                self.availableDepartment.push(n.text, n.text);
  //            });
  //            console.log(availableDepartment());
  //        }
  //    });
      
  //}
  $(function () {
      //ko.applyBindings(new viewModel());
      //console.log(wrapper.init);
      //$('#tree').treeview({ data: wrapper.init });
  });
//[{ text: "Parent 1" }]
//生成menuButton子菜单
//wrapper.menuButtonChild = function (n) {
//    var str = '';
//    $.each(n.children, function (j, o) {
//        if (o.children) {
//            //str += ' <li>';
//            //str += '<span iconCls="' + o.IconClass + '">' + o.MenuName + '</span><div style="width:120px;">';
//            //str = wrapper.menuButtonChild(o);
//            //str += '</div></div>';
//        } else
//            str += utils.formatString('<li><a href="{0}"><i class="glyphicon glyphicon-map-marker"></i>{1}</a></li> ', o.URL, o.MenuName);
//    });
//    return str;
//}
