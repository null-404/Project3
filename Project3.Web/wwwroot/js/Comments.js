window.onload = function () {


    //回复评论处理
    var reply = document.getElementById("commentitems");
    reply.onclick = function (ev) {
        var ev = ev || window.event;
        var target = ev.target || ev.srcElement;

        if (target.nodeName.toLowerCase() == 'a' && target.id == "reply") {
            var parentcoid = document.getElementById("parentcoid");
            parentcoid.value = target.getAttribute('data-parentcoid');
            document.getElementById("toreply").innerHTML = "<a data-action=\"cancle\" href=\"#form\">[取消]</a> 正在回复 <a class=\"nickname\">" + target.getAttribute('data-reply')+"</a>";
        }

    }

    var cancle = document.getElementById("form");
    cancle.onclick = function (ev) {
        var ev = ev || window.event;
        var target = ev.target || ev.srcElement;

        if (target.nodeName.toLowerCase() == 'a' && target.getAttribute('data-action')=="cancle") {
            var parentcoid = document.getElementById("parentcoid");
            parentcoid.value = "0";
            document.getElementById("toreply").innerHTML = "评论";
        }
    }
}