//function  toTree(d) {
//    if (!d || !d.length) {
//        alert("系统提示:您没有任何权限！请联系管理员.");
//        location.href = "/admin/login";
//        return false;
//    }

//    var visibleMenu = $.grep(d, function (row) { return row.IsVisible; });//筛选出权限中被隐藏了的
//    var menus = utils.toTreeData(visibleMenu, 'MenuID', 'ParentID', 'children'); //生成树形
//    var tree = "[";
//    $.each(menus, function (i, n) {
//        tree += '{"text":"<input   />' + n.MenuName + '"},';
//    });

//    //console.log(tree);
//    tree = tree.substring(0, tree.length - 1);
//    tree += "]";
//    $('#tree').treeview({ data: tree });
//    return true;
//}

//$(function () {
//    $.ajax({ type: 'GET', url: '/api/sys/MenuApi/get', success:function(d) {
//        // alert(d[0].MenuName);
//        toTree(d);
//    }});
//    //console.log(getTree());
//    //$('#tree').treeview({ data: getTree() });
//});

var ViewModell = function () {
    var self = this;
    self.JsonMeneData = ko.observableArray();
    //self.TotalPages = ko.observable(0);
    $.ajax({
        type: "get",
        url: "/api/sys/MenuApi/get",
        success: function (d) {
            // console.log(d);          
            //self.TotalPages();
            //$("#totalpage").val(d.TotalPages);
            self.JsonMeneData(d);
        }
    });
};

$(function () {
    ko.applyBindings(new ViewModell());
});
//function selectData() {
//    //var v = new ViewModel();
//    ViewModel.paginator($("#perPageTotle").val());
//    //alert($("#perPageTotle").val());
//}