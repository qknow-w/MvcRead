var ViewModel = function() {
    var self = this;
    self.JsonData = ko.observableArray();
    //self.TotalPages = ko.observable(0);
    ///下拉框选择角色数据
    var role = function (roleId, roleName) {
        this.RoleID = roleId;
        this.RoleName = roleName;
    }
    $.ajax({
        type: "get",
        url: "/api/sys/User/GetUser",
        async: false,
        success: function (d) {
            // console.log(d);          
            //self.TotalPages();
            //$("#totalpage").val(d.TotalPages);
            self.JsonData(d.Items);
            paginator(d.TotalPages);
        }
    });
    self.availableRole = ko.observableArray();
    $.ajax({
        url: "/api/sys/roleapi/gett",
        type: "get",
        async: false,
        success: function (data) {
            //self.availableRole.removeAll();
            $.each(data, function (i, n) {
                self.availableRole.push(new role(n.RoleID, n.RoleName));
            });
            // console.log(self.editData);
        }

    });
    self.SelectData = ko.observableArray(["10", "25", "50", "100"]);
    self.ValueData = ko.observable("10");
    self.ValueData.subscribe(function () {
        $.ajax({
            type: "get",
            url: "/api/sys/User/GetUser",
            data: { curreent: "1", perPage: $("#perPageTotle").val() },
            dataType: "json",

            success: function (data) {
                //成功则执行表格刷新函数
                // console.log(data);
                //var v = new ViewModel();                        
                self.JsonData(data.Items);
                paginator(data.TotalPages);

            }
        });

    });

    //新增数据
    self.formRole = {
        Description: ko.observable(),
        RoleName: ko.observable()
    };
  
    
    self.bindRole = function () {
        self.editData.UserID("");
        self.editData.UserName("");
        self.editData.Password("");
        self.editData.RealName("");
        self.editData.Depart("");
        self.editData.Phone("");
        //$.ajax({
        //    url: "/api/sys/roleapi/gett",
        //    type: "get",
        //    success: function (data) {
        //        //self.availableRole.removeAll();
        //        $.each(data, function(i, n) {                                 
        //            self.availableRole.push(new role(n.RoleID, n.RoleName));                    
        //        });
        //       // console.log(self.editData);
        //    }
          
        //});       
    }
    //修改数据
    self.editData = {
        UserID: ko.observable(),
        UserName: ko.observable(),
        Password: ko.observable(),
        RealName: ko.observable(),
         Depart: ko.observable(),
         Phone: ko.observable(),
         RoleID: ko.observable()
};
    self.clickEditt = function (data) {
        //alert("123");
        // console.log(data);
        //var d = $.grep(self.JsonData(),function(row) {
        //    return row.RoleID = id;
        //});
       // console.log(data.UserID);
        // self.editData.roleid = d.RoleID;
        self.editData.UserID(data.UserID);
        self.editData.UserName(data.UserName);
        self.editData.Password(data.Password);
        self.editData.RealName(data.RealName);
        self.editData.Depart(data.Depart);
       // console.log(data.Depart);
        self.editData.Phone(data.Phone);
        //self.editData.RoleID(data.RoleID);
        self.editData.RoleID(data.RoleID);
      //  console.log(data.Depart);
       // self.bindRole();
       

    }
    //新增数据
    self.formUser = {
        Description: ko.observable(),
        RoleName: ko.observable()
    };
    self.submitdata = function (formElement) {
        if ($('#Password').val() != $('#rePassword').val()) {
            $('#myAlert').show();
            return false;
        }
        // console.log("123");
        //alert("新增成功,请关闭窗口后刷新");
       
        
        //alert("123");
        $.ajax({
            url: "/api/sys/User/AddUser",
            type: "post",
            data: ko.toJS(self.editData),
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
    self.submitdata1 = function (formElement) {
        if ($('#Password1').val() != $('#rePassword1').val()) {
            $('#myAlert1').show();
            return false;
        }
        console.log("123");
       
        //alert("123");
        $.ajax({
            url: "/api/sys/User/EditUser",
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
     //删除
   self.clickDelect = function (data) {
       if (confirm("是否删除！")) {
           $.ajax({
               url: "/api/sys/User/DeleteUser",
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

    ///下拉框选择部门数据
   var Depart = function (DepartmentName, DepartmentName) {
       this.DepartmentName = DepartmentName;
       this.DepartmentName = DepartmentName;
   }
   self.availableDepartment = ko.observableArray();
   $.ajax({
       type: "get",
       url: "/api/sys/DepartmentApi/get",
       async: false,
       success: function (data) {
           self.availableDepartment.removeAll();
           // self.availableDepart.push(new Depart(0, "最高级"));
           $.each(data, function (i, n) {
               // console.log(n.text);
               self.availableDepartment.push(new Depart(n.text, n.text));
           });
           //console.log(self.availableDepartment);
       }
   });

    function paginator(d) {
        //分页
        //alert(d);
        var options = {
            currentPage: 1,//当前页
            totalPages: d,//总页数
            numberofPages: 5,//显示的页数

            itemTexts: function (type, page, current) { //修改显示文字
                switch (type) {
                    case "first":
                        return "第一页";
                    case "prev":
                        return "上一页";
                    case "next":
                        return "下一页";
                    case "last":
                        return "最后一页";
                    case "page":
                        return page;
                }
            }, onPageClicked: function (event, originalEvent, type, page) { //异步换页

                $.ajax({
                    type: "get",
                    url: "/api/sys/User/GetUser",
                    data: { curreent: page, perPage: $("#perPageTotle").val() },
                    dataType: "json",

                    success: function (data) {
                        //成功则执行表格刷新函数
                        // console.log(data);
                        //var v = new ViewModel();                        
                        self.JsonData(data.Items);

                    }
                });

            },

        };
        $("#example").bootstrapPaginator(options);
    }
};
$(function () {
    $('#myAlert').hide();
    $('#myAlert1').hide();
    ko.applyBindings(new ViewModel());

});