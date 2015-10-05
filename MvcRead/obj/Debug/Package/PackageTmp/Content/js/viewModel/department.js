var ViewModel = function() {
    var self = this;
    self.JsonData = ko.observableArray();
    $.ajax({
        type: "get",
        url: "/api/sys/DepartmentApi/get",
        success: function (d) {
            // console.log(d);          
            //self.TotalPages();
            //$("#totalpage").val(d.TotalPages);
            self.JsonData(d);         
        }
    });
    //编辑
    self.editData = {
        DepartmentID: ko.observable(),
        DepartmentName: ko.observable(),
        Description: ko.observable(),     
    };
    self.clickEditt = function (data) {
        //alert("123");
        // console.log(data);
        //var d = $.grep(self.JsonData(),function(row) {
        //    return row.RoleID = id;
        //});
        console.log(data.DepartmentID);
        // self.editData.roleid = d.RoleID;
        self.editData.DepartmentID(data.DepartmentID);
        self.editData.DepartmentName(data.DepartmentName);
        self.editData.Description(data.Description);      
        //console.log(self.editData);
        // self.bindRole();


    }
    self.submitdata1 = function (formElement) {
        console.log("123");

        //alert("123");
        $.ajax({
            url: "/api/sys/DepartmentApi/EditDepart",
            type: "post",
            data: ko.toJS(self.editData),
            success: function (result) {
                if (result == "OK") {
                    alert("修改成功");


                } else {
                    alert("修改失败，请重新修改");

                }
                location.reload();
            }
        });
        // console.log(formElement);
    }
    //删除部门
    self.clickDelect = function (data) {
        if (confirm("是否删除！")) {
            $.ajax({
                url: "/api/sys/DepartmentApi/DeleteDepart",
                type: "post",
                data: ko.toJS(data),
                success: function (result) {
                    if (result == "OK") {
                        alert("删除成功");
                        location.reload();

                    } else {
                        alert("删除失败，请重新删除");
                        location.reload();
                    }
                }
            });
        }



    }
    //新增
    self.AddData = {
        DepartmentID: ko.observable(),
        DepartmentName: ko.observable(),
        Description: ko.observable(),
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
});
