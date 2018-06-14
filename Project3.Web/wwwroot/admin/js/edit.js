/*
 提交表单
*/
$("#submit").on("click", function () {
    edit_post();
});
function edit_post() {
    $("#submit").button('loading');

    var fd = new FormData($("form")[0]);
   
    var allowcomment = $("#allowcomment").get(0).checked ? 0 : 1;
    var status = $("#status").get(0).checked ? 0 : 1;
    fd.append("allowcomment", allowcomment);
    fd.append("status", status);
    fd.append("cid", cid);
    fd.append("type", type);

    //获取文件
    $("#files li").each(function () {
        fd.append("files", $(this).data("fid"));
       
    });


    $.ajax({
        type: "POST",
        data: fd,
        url: "/admin/content/EditPost",
        processData: false,  // 不处理数据
        contentType: false,   // 不设置内容类型
        success: function (data) {
            $("#submit").button('reset');
            window.location.reload();
            
        },
        error: function (data) {
            console.log(data);
           

        }
    });
}