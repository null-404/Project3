ReListenEvents();
//上传附件
function uploadfile() {
    $("#btn-uploadfile").button('loading');
    var fd = new FormData();

    for (var i = 0; i < $('#File')[0].files.length; i++) {
        fd.append("file" + i, $('#File')[0].files[i]);
    }
    fd.append("cid", cid);
    $.ajax({
        type: "POST",
        data: fd,
        url: "/admin/content/UploadFiles",
        contentType: false,
        processData: false,
        success: function (data) {
            $("#btn-uploadfile").button('reset');
            updatefile(data);

        },
        error: function (data) {
            $("#btn-uploadfile").button('reset');



        }
    });
}
//更新附件
function updatefile(data) {
    var t = eval(data);


    $.each(t, function (i, n) {


        var row = '<li class="list-group-item" data-fid="' + n.fid + '"><a data-toggle="tooltip" data-placement="top" title = "' + n.filepath + '" data-trigger="click" >' + n.filename + '</a>(' + n.size + ')<a data-action="delete" data-fid="' + n.fid+'" class="pull-right"> <span class="glyphicon glyphicon-remove"></span></a></li>';


        $('#files ul').prepend(row);

    });

    ReListenEvents();
}

//删除附件
function delete_file(fid) {
    $.ajax({
        type: "get",
        data: { fid: fid },
        dataType: "Json",
        url: "/admin/content/DeleteFile",

        success: function (data) {
            console.log(data);
            if (data.issuccess) {
                $("#files ul [data-fid='" + fid + "']").remove();
            }
        }
    });
}

//重新监听事件
function ReListenEvents() {
    $("#files ul a").on("click", function () {
        if ($(this).data("action") == "delete") {
            console.log("delete");
            delete_file($(this).data("fid"));
        }
    });
    $("[data-toggle='tooltip']").tooltip();
}


