var ViewModel=function() {
    var self = this;
    self.visi = ko.observable(false);
    self.Check = function(data) {
        //// 下面代码都是处理IE浏览器的情况 
        if (window.ActiveXObject || "ActiveXObject" in window) {
            //判断是否为IE浏览器，"ActiveXObject" in window判断是否为IE11
            //判断是否安装了adobe Reader
            for (var x = 2; x < 10; x++) {
                try {
                    oAcro = eval("new ActiveXObject('PDF.PdfCtrl." + x + "');");
                    if (oAcro) {
                        flag = true;
                    }
                } catch (e) {
                    flag = false;
                }
            }
            try {
                oAcro4 = new ActiveXObject('PDF.PdfCtrl.1');
                if (oAcro4) {
                    flag = true;

                }
            } catch (e) {
                flag = false;
            }
            try {
                oAcro7 = new ActiveXObject('AcroPDF.PDF.1');
                if (oAcro7) {
                    flag = true;
                }
            } catch (e) {
                flag = false;
            }
            if (flag) {
                window.open("/Home/Ie?url=" + data.DataURL.trim() + "&&id=" + data.ApplyID);
               // location = "/Home/Ie?url=" + data.DataURL;
                //  location = "/Home/PDF";
            } else {
                this.visi(true);
                return false;

            }
        } else {
            this.visi(false);
            window.open("/Home/NoIe?url=" + data.DataURL.trim() + "&&id=" + data.ApplyID);
            // location = "/Home/NoIe?url=" + data.DataURL;
        //    $.ajax({
        //        url: "/Home/NoIe",
        //        type: "post",
        //        data: { url: data.DataURL },
        //        success: function (result) {
        //            console.log(result);
        //            document.write(result)
        //        }

        //});
        }
        return false;
    };
    self.JsonData = ko.observableArray();
    $.ajax({
        type: "get",
        url: "/api/sys/DataApi/GetData",
        success: function (d) {
            // console.log(d);          
            //self.TotalPages();
            //$("#totalpage").val(d.TotalPages);
            self.JsonData(d.Items);
            paginator(d.TotalPages);
        }
    });
    //下拉框
    self.SelectData = ko.observableArray(["10", "25", "50", "100"]);
    self.ValueData = ko.observable("10");
    self.ValueData.subscribe(function () {
        $.ajax({
            type: "get",
            url: "/api/sys/DataApi/GetData",
            data: { curreent: "1", perPage: $("#perPageTotle").val(), queryString: $("#queryString").val() },
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

    //绑定修改数据
    self.editData = {
        DataID: ko.observable(),
        DataName: ko.observable(),
        /* CaseReasonId: ko.observable(),//下拉*/
        CaseReason: ko.observable(),
        /*     CaseReasonId_Paent: ko.observable(),下拉*/
        source: ko.observable(),
        totals: ko.observable(),
        DataDescription: ko.observable(),
        suspect: ko.observable()

    };

    ///下拉框选择罪名
    var Reason = function (CaseReasonId, CaseReason) {
        this.CaseReasonId = CaseReasonId;
        this.CaseReason = CaseReason;
    }
    self.availableCaseReason = ko.observableArray();
    
    //编辑
    self.clickEditt = function (data) {
        //alert("123");
        // console.log(data);
        //var d = $.grep(self.JsonData(),function(row) {
        //    return row.RoleID = id;
        //});
        //console.log(d);
        // self.editData.roleid = d.RoleID;

        //$.ajax({
        //    type: "get",
        //    url: "/api/sys/CaseReasonApi/getdata",
        //    async: false,
        //    success: function (result) {
        //        self.availableCaseReason.removeAll();
        //        // self.availableDepart.push(new Depart(0, "最高级"));
        //        $.each(result, function (i, n) {
        //            // console.log(n.CaseReasonName);

        //            self.availableCaseReason.push(new Reason(n.CaseReasonId, n.CaseReasonName));
        //        });
        //        //console.log(self.availableDepartment);
        //    }
        //});

        //下拉
       /* $.ajax({
            url: "/api/sys/CaseReasonApi/GetDataByID/" + data.CaseReasonId_Paent,
            type: "get",
            async: false,
            success: function (result) {
               // console.log(data);
                document.getElementById("CaseReasonId").options.length = 0;
                //        $("#CaseReasonId option").length = 0;
                $.each(result, function (index, value) {

                    var varItem = new Option(value.CaseReasonName, value.CaseReasonId);
                    //console.log(varItem);
                    document.getElementById("CaseReasonId").options.add(varItem);

                    //objSelect.options[objSelect.options.length] = varItem;  
                    //    //    //    $("#CaseReasonId").options.add
                    //    //    //});
                    //    //    //console.log($("#CaseReasonId option").length);
                    //    //    //for (var i = $("#CaseReasonId option").length - 1; i >= 0; i--) {
                    //    //    //    $(this).remove(i);
                    //    //    //}
                    //    //    console.log($("#CaseReasonId").length);

                });
            }
        });*/

        console.log(data);
        self.editData.DataID(data.DataID);
        self.editData.DataName(data.DataName.replace(/\s+/g,""));     
        /*self.editData.CaseReasonId(data.CaseReasonId);下拉*/
        self.editData.source(data.source.replace(/\s+/g,""));
        self.editData.totals(data.totals);
        self.editData.DataDescription(data.DataDescription.replace(/\s+/g,""));
        self.editData.suspect(data.suspect.replace(/\s+/g, ""));
        self.editData.CaseReason(data.CaseReason.replace(/\s+/g, ""));
       // self.editData.CaseReasonId_Paent(data.CaseReasonId_Paent + 'first');下拉
       
        //self.editData.RoleName(data.RoleName);
       // console.log(data.CaseReasonId);

    }
    //提交编辑
    self.submitEditdata = function () {
         console.log(ko.toJS(self.editData));        
        $.ajax({
            url: "/api/sys/dataApi/ModifyData",
            type: "post",
            data: ko.toJS(self.editData),
            success: function (data) {
                if (data == "OK") {
                    alert("修改成功");
                    //location.reload();

                } else {
                    alert("修改失败，请重新修改");
                    //location.reload();
                }
                location.reload();
            }
        });
    }

    //删除
    self.clickDelect = function (data) {
        if (confirm("是否删除！")) {
            $.ajax({
                url: "/api/sys/DataApi/DeleteData",
                type: "post",
                data: ko.toJS(data),
                success: function (result) {
                    if (result == "OK") {
                        alert("删除成功");
                        location.reload();

                    } else {
                        alert("删除失败，请重新修改");
                        location.reload();
                    }
                }
            });
        }



    }
    //绑定提交数据
    self.formDataa = {
        DataID: ko.observable(),
        DataSeq: ko.observable(),
        DataName: ko.observable(),
        ApplyName: ko.observable(),
        Depart: ko.observable(),
        StartTime: ko.observable(),
        EndTime: ko.observable()        
    };
    self.clickGen=function(data) {
        self.formDataa.DataID(data.DataID);
        self.formDataa.DataSeq(data.DataSeq);
        self.formDataa.DataName(data.DataName);
    }
    //提交生成审核码
    self.submitdata = function (formElement) {
       // console.log($("#StartTime").val());

        self.formDataa.StartTime($("#StartTime").val());
        self.formDataa.EndTime($("#EndTime").val());
        $.ajax({
            url: "/api/sys/DataApi/AddApply",
            type: "post",
            data: ko.toJS(self.formDataa),
            success: function (result) {
                if (result>0) {
                    alert("生成成功!!,请在申请日志中查看");
                    window.open("/admin/look/" + result, "_blank");
                    // location.reload();

                } else {
                    alert("生成失败，请重新生成");
                    location.reload();
                }
               // location.reload();
            }
        });


        //var re = /^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$/;
        //console.log(self.formDataa.StartTime());
        //if (re.test(self.formDataa.StartTime()) && re.test(self.formDataa.EndTime())) {
        //    console.log("234");
        //        $.ajax({
        //            url: "/api/sys/DataApi/AddApply",
        //            type: "post",
        //            data: ko.toJS(self.formDataa),
        //            success: function (result) {
        //                if (result == "OK") {
        //                    alert("生成成功!!,请在申请日志中查看");
        //                    location.reload();

        //                } else {
        //                    alert("生成失败，请重新生成");
        //                    location.reload();
        //                }
        //            }
        //        });
        //    } else {
        //        $('#myAlert').show();
        //    }

    };
    //搜索
    self.clickSearch = function () {
        //console.log("123");
        $.ajax({
            type: "get",
            url: "/api/sys/DataApi/GetData",
            data: { curreent: "1", perPage: $("#perPageTotle").val(), queryString: $("#queryString").val() },
            dataType: "json",

            success: function (data) {
                //成功则执行表格刷新函数
                // console.log(data);
                //var v = new ViewModel();                        
                self.JsonData(data.Items);
                paginator(data.TotalPages);

            }
        });
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
            async:false,
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

    //分页
      function paginator(d) {
            //分页
            //alert(d);
            var options = {
                currentPage: 1, //当前页
                totalPages: d, //总页数
                numberofPages: 5, //显示的页数

                itemTexts: function(type, page, current) { //修改显示文字
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
                },
                onPageClicked: function(event, originalEvent, type, page) { //异步换页

                    $.ajax({
                        type: "get",
                        url: "/api/sys/DataApi/GetData",
                        data: { curreent: page, perPage: $("#perPageTotle").val(), queryString: $("#queryString").val() },
                        dataType: "json",

                        success: function(data) {
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
   // var re = /^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$/;
    //if (re.test(self.formDataa.StartTime) && re.test(self.formDataa.EndTime))
  //  alert(re.test("2014-12-19"));
    $('#myAlert').hide();
    ko.applyBindings(new ViewModel());

});