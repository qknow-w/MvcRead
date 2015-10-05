var ViewModel = function () {
    var self = this;    
    self.JsonData = ko.observableArray();  
    //self.TotalPages = ko.observable(0);
    $.ajax({
        type: "get",
        url: "/api/sys/roleapi/get",
        success: function (d) {
            // console.log(d);          
            //self.TotalPages();
            //$("#totalpage").val(d.TotalPages);
            self.JsonData(d.Items);
            paginator(d.TotalPages);
        }
    });
    self.SelectData = ko.observableArray(["10", "25", "50", "100"]);
    self.ValueData = ko.observable("10");
    self.ValueData.subscribe(function() {
        $.ajax({
            type: "get",
            url: "/api/sys/roleapi/get",
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
                    url: "/api/sys/roleapi/get",
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

    self.JsonMeneData = ko.observableArray();
    //self.TotalPages = ko.observable(0);
    self.clickMenu = function (data) {
        //alert("123");
       
        $.ajax({
            type: "get",
            url: "/api/sys/MenuApi/GetByRole",
            data:"RoleID="+data.RoleID,
            success: function (d) {
               // console.log(d);
                //self.TotalPages();
                //$("#totalpage").val(d.TotalPages);
                self.JsonMeneData($.grep(d,function(row) {
                    return row.ParentID != "0";
                }));
            }
        });
    }
   
    //保存权限
    self.btnMenuSave = function () {
       // console.log(self.JsonMeneData());
        var postModel = [];
        $.each(self.JsonMeneData(), function (i, n) {
            postModel.push({ ID: n.ID, isEnble: n.isEnble });
            //console.log(postModel);
        });
        //$.each(self.JsonMeneData(), function (i, n) {
        //    postModel[ID].push[n.isEnble];
        //    //console.log(postModel);
        //});
       // console.log(postModel);
        $.ajax({
            url: "/api/sys/RoleApi/Change",
            type: "post",
           contentType: "application/json",
            dataType: 'json',
            //data: self.JsonMeneData(),
            data: ko.toJSON(postModel),
            //data: JSON.stringify({
            //    "MenuData": [
            //                        { "foo": "123", "boo": "123" },
            //                        { "foo": "123", "boo": "123" },
            //                        { "foo": "123", "boo": "123" }
            //    ]

            //}),
           
            
          success:function(data) {
              //console.log(data);
              if (data == "OK") {
                  alert("修改成功");
                  location.reload();
                  
              } else {
                  alert("修改失败，请重新修改");
                  location.reload();
              }
              

            }
    });
    }
    // self.editData = ko.observableArray(RoleID:"123");
    //修改数据
    self.editData = {
        RoleID: ko.observable(),
        Description: ko.observable(),
        RoleName: ko.observable()
    };
    self.clickEditt = function (data) {
        //alert("123");
       // console.log(data);
        //var d = $.grep(self.JsonData(),function(row) {
        //    return row.RoleID = id;
        //});
        //console.log(d);
        // self.editData.roleid = d.RoleID;

        self.editData.RoleID(data.RoleID);
        self.editData.Description(data.Description);
        self.editData.RoleName(data.RoleName);
      //  console.log(self.editData);

    }
    //提交编辑
   self.clickEditSubmit=function() {
      // console.log(ko.toJS(self.editData));
       $.ajax({
           url: "/api/sys/RoleApi/ModifyRole",
           type: "post",
           data:ko.toJS(self.editData),
           success: function(data) {
               if (data == "OK") {
                   alert("修改成功");
                   location.reload();

               } else {
                   alert("修改失败，请重新修改");
                   location.reload();
               }
           }
       });
   }
    //删除
   self.clickDelect = function (data) {
       if (confirm("是否删除！")) {
           $.ajax({
               url: "/api/sys/RoleApi/DelectRole",
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
    //新增数据
    self.formRole = {
        Description: ko.observable(),
        RoleName: ko.observable()
    };
    self.submitdata = function (formElement) {
        //alert("123");
        $.ajax({
            url: "/api/sys/RoleApi/AddRole",
            type: "post",
            data: ko.toJS(self.formRole),
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
//function selectData() {
//    //var v = new ViewModel();
//    ViewModel.paginator($("#perPageTotle").val());
//    //alert($("#perPageTotle").val());
//}