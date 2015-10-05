var ViewModel = function() {
    //新增
    self.AddData = {
        DepartmentName: ko.observable(),
        ParentID: ko.observable(),
    };
    ///下拉框选择部门数据
    var Depart = function (DepartmentID, DepartmentName) {
        this.DepartmentID = DepartmentID;
        this.DepartmentName = DepartmentName;
    }
    self.availableDepart = ko.observableArray();
    self.bindDeparts = function () {
       // console.log("111");
        $.ajax({
            type: "get",
            url: "/api/sys/DepartmentApi/get",
            success: function (data) {
                self.availableDepart.removeAll();
                self.availableDepart.push(new Depart(0, "最高级"));
                $.each(data, function (i, n) {
                    self.availableDepart.push(new Depart(n.ID, n.text));
                });
            }
        });
    };
    self.submitdata = function (formElement) {
        $.ajax({
            url: "/api/sys/DepartmentApi/AddDepart",
            type: "post",
            data: ko.toJS(self.AddData),
            success: function (result) {
                if (result == "OK") {
                    alert("新增成功");


                } else {
                    alert("新增失败，请重新新增");

                }
                location.reload();
            }
        });
        // console.log(formElement);
    }

    
};
$(function () {
    ko.applyBindings(new ViewModel());
     getTree();
});
var DepartmentID = 0;
function getTree() {
    var treeData;
    $.ajax({
        type: "get",
        url: "/api/sys/DepartmentApi/get",
        success: function (data) {
            treeData = utils.toTreeData(data, 'ID', 'parentID', 'nodes'); //生成树形
            console.log(treeData);
            $('#tree').treeview({
                data: treeData,
                Collapsed: true,
                onNodeSelected: function (event, node) {
                    DepartmentID = node.ID;
                   // console.log(nodeId);
                }
            });
        }
    });
    
}
//删除  
function clickDelect() {
    if (confirm("是否删除！")) {
        if (DepartmentID > 0) {
            console.log(DepartmentID);
            $.ajax({
                url: "/api/sys/DepartmentApi/DeleteDepart",
                type: "post",
                data: { DepartmentID: DepartmentID },
                success: function(result) {
                    if (result == "OK") {
                        alert("删除成功");
                        location.reload();

                    } else {
                        alert("删除失败，请重新删除");
                        location.reload();
                    }
                }
            });
        } else {
            alert("请选择要删除的部门");
        }
        //console.log(nodeId);
        
    }
}