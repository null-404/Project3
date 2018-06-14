(function () {
    //下拉菜单超链接提交表单
    $(".dropdown-menu.action a").on("click", function () {
        var tip = $(this).data("tip");
        if (typeof (tip) != "undefined" && confirm(tip)) {
            $("form").first().attr("action", $(this).attr('href'));
            $("form").first().attr("method", "POST");
            $("form").first().submit();
        }

        return false;
    });

    /*TABLE点击选择checkbox*/
    var ischeckboxclick = false;
    $("table tbody tr input[type='checkbox']").on("click", function (e) {
        ischeckboxclick = true;
        onselectedstyle($(this).is(':checked'), $(this).parent().parent());
        
    });
    $("table tbody tr").on("click", function () {
        if (!ischeckboxclick) {
            var c = $(this).find("input[type='checkbox']");
            c.prop("checked", !c.is(':checked'));
            onselectedstyle(c.is(':checked'), $(this));

        }
        ischeckboxclick = false;
    });
    //响应选中样式
    function onselectedstyle(isselected,tr) {
        if (isselected) {

            tr.addClass("active");

        }
        else {

            tr.removeClass("active");

        }
    }
    //全选反选
    $("table thead input[type='checkbox']").on("click", function () {
        if ($(this).is(':checked')) {
            $("table tbody input[type='checkbox']").prop("checked", true);
            $("table tbody tr").addClass("active");
        }
        else {
            $("table tbody input[type='checkbox']").prop("checked", false);
            $("table tbody tr").removeClass("active");

        }
    });



  

})();
