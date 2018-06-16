var comments = function (cid, commentsid, toreplyid, formid, loadmoreid) {

    var pageindex = 0;
    var totalpages = 0;
    var isloading = false;

    window.onload = function () {
        LoadParentComments();

        //回复评论处理
        var replybtn = document.getElementById(commentsid);
        replybtn.onclick = function (ev) {
            var ev = ev || window.event;
            var target = ev.target || ev.srcElement;

            if (target.nodeName.toLowerCase() == 'a' && target.getAttribute('data-action') == "reply") {
                var parentcoid = document.getElementById("parentcoid");
                parentcoid.value = target.getAttribute('data-parentcoid');
                document.getElementById(toreplyid).innerHTML = "<a data-action=\"cancle\" href=\"#form\">[取消]</a> 正在回复 <a class=\"nickname\">" + target.getAttribute('data-reply') + "</a>";
            }

        }

        var cancle = document.getElementById(formid);
        cancle.onclick = function (ev) {
            var ev = ev || window.event;
            var target = ev.target || ev.srcElement;

            if (target.nodeName.toLowerCase() == 'a' && target.getAttribute('data-action') == "cancle") {
                var parentcoid = document.getElementById("parentcoid");
                parentcoid.value = "0";
                document.getElementById(toreplyid).innerHTML = "评论";
            }
        }
    }

    //加载父级评论
    function LoadParentComments() {
        if (!isloading && totalpages == 0 || pageindex + 1 <= totalpages) {
            isloading = true;
            pageindex++;
            console.log("准备加载");

            $.ajax({
                type: "get",
                dataType: "Json",
                url: "/commentsapi/get?cid=" + cid + "&page=" + pageindex,

                success: function (data) {
                    if (data.hasnextpage) {
                        $("#" + loadmoreid).removeClass("hidden");
                    }
                    else {
                        $("#" + loadmoreid).addClass("hidden");

                    }

                    totalpages = data.totalpages;
                    console.log(pageindex + "/" + totalpages);

                    isloading = false;
                    $.each(data.comments, function (i, c) {

                        var childhtml = '<ul class="commentitems"></ul>';


                        var html = '<li data-coid="' + c.coid + '"><div class="commenthead"><img src="/theme/apollo/favicon.png" /></div><div class="commentcontent"><div class="commentheader"><label class="nickname">' + c.nickname + '</label></div><div class="commentreply">' + c.content + '</div><div class="commentaction"><a href="#form" data-reply="' + c.nickname + '" data-parentcoid="' + c.coid + '" data-action="reply">回复</a></div></div>' + childhtml + '</li>';
                        $("#" + commentsid).append(html);
                        //加载默认子级评论
                        LoadDefaultChildComments(c.coid);

                    });

                }
            });
        }
    }

    function LoadDefaultChildComments(coid) {
        //每条父级评论加载2条子级评论
        LoadChildComments(coid, 2, 1);
    }

    function LoadChildComments(coid, pagesize, page, skip) {

        $.ajax({
            type: "get",
            dataType: "Json",
            url: "/commentsapi/get?cid=" + cid + "&parentcoid=" + coid + "&page=" + page + "&pagesize=" + pagesize + "&skip=" + skip,

            success: function (data) {
                console.log(data);
                var childmore = '';
                $("li[data-coid=" + coid + "] ul #childmore").remove();

                if (data.hasnextpage) {
                    if (pagesize == 2) {
                        page = 0;
                    }
                    childmore = '<li id="childmore"><label><a data-page="' + page + '" data-coid="' + coid + '" data-action="loadchildcomments">更多</a></label></li>';
                }
                $.each(data.comments, function (i, c) {


                    var html = '<li data-coid="' + c.coid + '"><div class="commenthead"><img src="/theme/apollo/favicon.png" /></div><div class="commentcontent"><div class="commentheader"><label class="nickname">' + c.nickname + '</label></div><div class="commentreply">' + c.content + '</div><div class="commentaction"><a href="#form" data-reply="' + c.nickname + '" data-parentcoid="' + c.coid + '" data-action="reply">回复</a></div></div></li>';
                    $("li[data-coid=" + coid + "] ul").append(html);

                });
                $("li[data-coid=" + coid + "] ul").append(childmore);
                onListenClickChildLoad();
            }
        });
        //return html;

    }
    //重置监听子级评论加载点击
    function onListenClickChildLoad() {
        $("#childmore a").unbind("click").click(function () {

            LoadChildComments($(this).data("coid"), 10, ($(this).data("page") + 1), 2);
            //  console.log(i + "加载子级评论：" + $(this).data("coid"));

        });
    }

    //点击加载更多->一级评论
    $("#" + loadmoreid + " a").on("click", function () {
        LoadParentComments();
    });




    //滚动条到底部时加载数据
    //$(window).bind("scroll", function () {
    //    if ($(document).scrollTop() + $(window).height() > $(document).height() - 100)// 接近底部100px
    //    {
    //        console.log(pageindex + "/" + totalpages);
    //        LoadParentComments();


    //    }
    //});


};

//window.onload = function () {


//    //回复评论处理
//    var reply = document.getElementById("commentitems");
//    reply.onclick = function (ev) {
//        var ev = ev || window.event;
//        var target = ev.target || ev.srcElement;

//        if (target.nodeName.toLowerCase() == 'a' && target.id == "reply") {
//            var parentcoid = document.getElementById("parentcoid");
//            parentcoid.value = target.getAttribute('data-parentcoid');
//            document.getElementById("toreply").innerHTML = "<a data-action=\"cancle\" href=\"#form\">[取消]</a> 正在回复 <a class=\"nickname\">" + target.getAttribute('data-reply')+"</a>";
//        }

//    }

//    var cancle = document.getElementById("form");
//    cancle.onclick = function (ev) {
//        var ev = ev || window.event;
//        var target = ev.target || ev.srcElement;

//        if (target.nodeName.toLowerCase() == 'a' && target.getAttribute('data-action')=="cancle") {
//            var parentcoid = document.getElementById("parentcoid");
//            parentcoid.value = "0";
//            document.getElementById("toreply").innerHTML = "评论";
//        }
//    }
//}