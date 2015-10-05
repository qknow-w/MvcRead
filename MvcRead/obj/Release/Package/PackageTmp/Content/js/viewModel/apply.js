var ViewModel = function () {
    var self = this;
    self.JsonData = ko.observableArray();
    $.ajax({
        type: "get",
        url: "/api/sys/DataApi/GetApply",
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
    self.ValueData.subscribe(function () {
        $.ajax({
            type: "get",
            url: "/api/sys/DataApi/GetApply",
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


    //分页
    function paginator(d) {
        //分页
        //alert(d);
        var options = {
            currentPage: 1, //当前页
            totalPages: d, //总页数
            numberofPages: 5, //显示的页数

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
            },
            onPageClicked: function (event, originalEvent, type, page) { //异步换页

                $.ajax({
                    type: "get",
                    url: "/api/sys/roleapi/GetApply",
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
}
$(function () {
   // $('#myAlert').hide();
    ko.applyBindings(new ViewModel());

});