(function () {
   
    var TextArea = document.getElementById("md");
    var md_link = "";
    //document.getElementById('naiveblog_textarea').onclick = function () {
    //    console.log("ok");
    //};
    var md = {
        link: "[[naivemd_a]]([naivemd_b])",
        bold: "**[naivemd_a]**",
        image: "![[naivemd_a]]([naivemd_b])",
        code: "```js\r[naivemd_a]\r```",
        italic: "*[naivemd_a]*",
        header: "\r# [naivemd_a]\r",
        underline: "++[naivemd_a]++",
        strikethrough: "~~[naivemd_a]~~",
        ordered_list: "ordered_list",
        list: "list",
        bookmark: "==[naivemd_a]=="
    };

    onclick("btns-insertlink", function () {
        insert(md.link, g("link_describe").value, g("link").value);
    });
    onclick("ui_btns_bold", function () {
        insert(md.bold);
    });

    onclick("btns-insertimage", function () {

        insert(md.image, g("imageurl_describe").value, g("imageurl").value);

    });
    onclick("ui_btns_code", function () {

        insert(md.code);

    });
    onclick("ui_btns_italic", function () {

        insert(md.italic);

    });
    onclick("ui_btns_header", function () {

        insert(md.header);

    });
    //onclick("ui_btns_underline", function () {

    //    insert(md.underline);

    //});
    //onclick("ui_btns_strikethrough", function () {

    //    insert(md.strikethrough);

    //});
    onclick("ui_btns_ordered_list", function () {

        insert(md.ordered_list);

    });
    onclick("ui_btns_list", function () {

        insert(md.list);

    });
    //onclick("ui_btns_bookmark", function () {

    //    insert(md.bookmark);

    //});
    function g(a) {
        return document.getElementById(a);
    }
    function onclick(a, b) {
        //console.log("没有找到ID为："+a+"的元素");

        if ( g(a) != null) {
            //console.log("-->" + a + "/" + typeof g(a));
            g(a).onclick = b;
        }
       
    }
    TextArea.onkeydown = onTextAreaKeyDown;

    function insert(a, b, c) {
        //console.log("selectline:" + getselectionline(TextArea));
        TextArea.focus();
        //获得选中内容起始位置
        var TextArea_selectionStart = TextArea.selectionStart;
        //获得选中内容
        var TextArea_selectcontent = window.getSelection().toString();
        //获得完整内容
        var TextArea_content = TextArea.value.toString();
        //起始内容
        var StartContent = TextArea_content.substring(0, TextArea_selectionStart);
        //是否键盘处理
        var iskeyboard = false;
        if (a == "ordered_list" || a == "list") {
            //处理列表
            if (TextArea_selectcontent.length == 0) {
                //处理没选择内容状态时
                a = a == "ordered_list" ? " 1. " : " - ";
            }
            else {
                //处理已选中内容状态时
                var tslist = TextArea_selectcontent.split('\n');
                var lines = tslist.length;
                console.log("lines:" + lines);
                if (lines > 0) {


                    var newa = "";
                    for (var i = 0; i < lines; i++) {
                        var st = a == "ordered_list" ? (i + 1) + "." : "-";
                        newa += " " + st + " " + tslist[i] + "\r";
                    }
                    a = newa;
                }
                else {
                    a = " 1. " + TextArea_selectcontent + "\r";
                }
            }
        }
        else if (b == undefined) {
            TextArea_selectcontent = TextArea_selectcontent.length == 0 ? "内容" : TextArea_selectcontent;
            a = a.replace("[naivemd_a]", TextArea_selectcontent);

        }
        else if (c !== undefined) {
            if (TextArea_selectcontent.length > 0) {
                a = a.replace("[naivemd_a]", TextArea_selectcontent);
            }
            else {
                a = a.replace("[naivemd_a]", b);

            }
            a = a.replace("[naivemd_b]", c);

        }
        else {
            a = a.replace("[naivemd_a]", b);
            iskeyboard = true;
        }


        //完成
        TextArea.value = StartContent + a + TextArea_content.substring(TextArea_selectionStart + TextArea_selectcontent.length, TextArea_content.length);

        //设置选中
        if (TextArea_selectcontent.length > 0) {
            TextArea.setSelectionRange(StartContent.length, StartContent.length + a.length);

        }
        else {
            TextArea.selectionStart = StartContent.length + a.length;
            TextArea.selectionEnd = StartContent.length + a.length;
            //TextArea.selectiuonStart = TextArea.value.length;
            //TextArea.selectionEnd = TextArea.value.length;
          
        }
  
        console.log(TextArea.selectionStart);
        //设置焦点
        TextArea.focus();

    }

    function getselectionline(a) {

        var v = a.value;
        // 开始到光标位置的内容
        var cv = '';
        cv = v.substr(0, a.selectionStart);

        // 获取当前是几行
        var cl = cv.split('\n').length - 1;
        // 当前行的内容
        var clv = v.split('\n')[cl];
        return clv;

    }
    //处理键盘事件
    function onTextAreaKeyDown() {
        var oEvent = window.event;
        if (oEvent.key == "Enter") {
            //获得选中行内容
            selectionline = getselectionline(TextArea);
            var islistback = islist(selectionline);
            if (islistback == 1) {
                window.event.returnValue = false;  
                var index = getorderedlistnum(selectionline);
                index++;
                insert("[naivemd_a]", "\r " + index + ". ");
                //insert("[naivemd_a]", index + ". ");

            }
            else if (islistback == 2) {
                insert("[naivemd_a]", "\r - ");

            }
        }
    }
    //获取带排序的列表序号，不是返回-1
    function getorderedlistnum(str) {
        var reg = /\s([0-9])\.\s/;
        var result = str.match(reg);

        if (result != null) {
            return result[1];
        }
        return -1;

    }

    //判断是否是列表，不是返回-1，排序列表返回1，列表返回2
    function islist(str) {

        if (getorderedlistnum(str) != -1) {
            return 1;
        }
        else {
            var reg = /\s([\-])\s/;
            var result = str.match(reg);

            if (result != null) {
                return 2;
            }
        }
        return -1;
    }
    //console.log(islist(" 3. 676676767"));
    //console.log(islist(" - 676676767"));
    //console.log(islist("3. 676676767"));

})();
