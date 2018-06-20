var comments = function (cid, commentsid, toreplyid, formid, loadmoreid) {

    var pageindex = 0;
    var totalpages = 0;
    var isloading = false;

    window.onload = function () {
        LoadParentComments();

        //回复评论处理
        var replybtn = document.getElementById(commentsid);
     
        if (replybtn != null) {
            replybtn.onclick = function (ev) {
                var ev = ev || window.event;
                var target = ev.target || ev.srcElement;

                if (target.nodeName.toLowerCase() == 'a' && target.getAttribute('data-action') == "reply") {
                    var parentcoid = document.getElementById("parentcoid");
                    parentcoid.value = target.getAttribute('data-parentcoid');
                    document.getElementById(toreplyid).innerHTML = "<a data-action=\"cancle\" href=\"#form\">[取消]</a> 正在回复 <a class=\"nickname\">" + target.getAttribute('data-reply') + "</a>";
                }

            }
        }

        var cancle = document.getElementById(formid);
        if (cancle != null) {
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
    }

    //加载父级评论
    function LoadParentComments() {
        if (!isloading && totalpages == 0 || pageindex + 1 <= totalpages) {
            isloading = true;
            pageindex++;

            $.ajax({
                type: "get",
                url: "/commentsapi/get?cid=" + cid + "&page=" + pageindex,
                success: function (data) {
                    console.log(data);
                  
                    //重置加载状态
                    isloading = false;
                    if (data.hasnextpage) {
                        $("#" + loadmoreid).removeClass("hidden");
                        $("#" + loadmoreid + " a").html("更多" + data.hascount + "条回复");
                    }
                    else {
                        $("#" + loadmoreid).addClass("hidden");


                    }

                    totalpages = data.totalpages;

                    isloading = false;
                    $.each(data.comments, function (i, c) {

                        var childhtml = '<ul class="commentitems"></ul>';


                        //填充父级评论
                        $("#" + commentsid).append(getCommentsHtml(c, childhtml));
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

    //加载子级评论
    function LoadChildComments(coid, pagesize, page, skip) {

        $.ajax({
            type: "get",
            dataType: "Json",
            url: "/commentsapi/get?cid=" + cid + "&parentcoid=" + coid + "&page=" + page + "&pagesize=" + pagesize + "&skip=" + skip,

            success: function (data) {
                var childmore = '';

                //$("li[data-coid=" + coid + "] ul #childmore").remove();


                if (data.hasnextpage) {
                    if (pagesize == 2) {
                        page = 0;
                    }
                    childmore = '<li id="childmore"><label><a href="javascript:void;" data-page="' + page + '" data-coid="' + coid + '" data-action="loadchildcomments">更多' + data.hascount + '条回复</a></label></li>';

                }
                if (data.totalcontents == 0) {
                    $("li[data-coid=" + coid + "] ul").remove();
                }
                $.each(data.comments, function (i, c) {
                    console.log("子级评论：" + c.coid);
                    //填充子级评论数据
                    $("li[data-coid=" + coid + "] ul").append(getCommentsHtml(c));
                   
                });
                //显示加载更多按钮
                $("li[data-coid=" + coid + "] ul").append(childmore);
                //重置监听
                onListenClickChildLoad();
            }
        });

    }
    //重置监听子级评论加载点击
    function onListenClickChildLoad() {
        $("#childmore a").unbind("click").click(function () {
            $(this).parent().parent().remove();
            var page = ($(this).data("page") + 1);
            //每次加载10条评论，跳过前面2条（因为默认加载了2条
            LoadChildComments($(this).data("coid"), 10, page, 2);

        });
    }

    //点击加载更多->一级评论
    $("#" + loadmoreid + " a").on("click", function () {
        LoadParentComments();
    });


    function getCommentsHtml(data, child) {
        if (typeof child == "undefined") {
            child = "";
        }
        var reply = "";
        if (data.reply != null) {
            reply = " <label>回复</label> <label class=\"nickname\">@" + data.reply + "</label>";
        }
        var html = '<li data-coid="' + data.coid + '"><div class="commenthead"><img src="https://www.gravatar.com/avatar/' + data.headmd5 + '?s=256&d=monsterid" /></div><div class="commentcontent"><div class="commentheader"><label class="nickname">' + data.nickname + '</label>' + reply + " " + data.createtime + '</div><div class="commentreply">' + data.content + '</div><div class="commentaction"><a href="#form" data-reply="' + data.nickname + '" data-parentcoid="' + data.coid + '" data-action="reply">回复</a></div></div>' + child + '</li>';
        return html;
    }




};
