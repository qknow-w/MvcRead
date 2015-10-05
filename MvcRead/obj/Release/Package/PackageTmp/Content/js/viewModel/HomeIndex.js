var ViewModel = function () {
    var self = this;
    self.formm= {
        numberr: ko.observable(),//编号
        genree : ko.observable()//审核码
    }
    self.JsonData = ko.observableArray();
    //提交
    self.submitdata = function () {
        //console.log("123");
        $.ajax({
            type: "get",
            url: "/api/sys/DataApi/GetBySeq",           
            data: { numberr: $("#numberr").val(), genree: $("#genree").val() },
            success: function (data) {
               // console.log(data.length);
                if (data.length==0) {
                    alert("未找到数据,请输入正确的内容和正确的访问时间！！");
                } else {
                    self.JsonData(data);
                }
               
            }
        });
    } 

    self.visi = ko.observable(false);
    self.Check = function (data) {
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
                    // location = "/Home/Ie";
                    //  location = "/Home/PDF";
                } else {
                    this.visi(true);
                    return false;

                }
            } else {
                this.visi(false);
                window.open("/Home/NoIe?url=" + data.DataURL.trim() + "&&id=" + data.ApplyID);
                // location = "/Home/NoIe";
            }
            return false;
        }

}
$(function () {
    ko.applyBindings(new ViewModel());


});
