function submit() {
    if ($('#Password').val() != $('#RePassword').val()) {
        $('#ModifyAlert').show();
        return ;
    } else {
        $.ajax({
            url: "/api/sys/User/ModifyPassword",
            type: "get",
            data: { userID: $("#UserID").val(), pass: $("#Password").val() },
            success:function(result) {
                if (result == "OK") {
                    alert("修改成功");


                } else {
                    alert("修改失败，请重新修改");

                }
                location.reload();
            }
        });
    }
    
}

$(function() {
    $('#ModifyAlert').hide();
});