var Detail = new Object();

Detail.options = {
    dataType: "JoinPublic",
    publishId: "",
    pageIndex: "1",
    pageSize: "20",
    pubType: "1"//发布类型
};

//加入
Detail.JoinPublic = function () {
    Detail.options.publishId = $("#hidPublishId").val();

    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "JoinPublic", publishId: Detail.options.publishId },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                alert("加入失败");
            } else if (data.MSG == "Y") {
                alert("加入成功");
            }
            else if (data.MSG == "S") {
                alert("你已加入");
            }
        }
    });
};

//获取对话表
Detail.LoadDialogMsg = function () {
    Detail.options.publishId = $("#hidPublishId").val();
    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "LoadDialogMsg", publishId: Detail.options.publishId, pageIndex: Detail.options.pageIndex, pageSize: Detail.options.pageSize },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                alert("加载失败");
            } else if (data.MSG == "Y") {
                var dialogList = data.dialogList;
                var len = dialogList.length;
                var html = '<ul>';
                for (var i = 0; i < len; i++) {
                    var dialogItem = dialogList[i];
                    var fromUserName = dialogItem.FromUserName;
                    var fromUserAvatar = dialogItem.FromUserAvatar;
                    var msg = dialogItem.Msg;
                    html = html + '<li>';
                    html = html + '<div class="messageListLeft detail">';
                    html = html + '<span><img src="' + fromUserAvatar + '" width="20px" height="20px" alt=""/></span>';
                    html = html + '</div>';
                    html = html + '<div class="messageListRight">';
                    html = html + '<div class="messageListTitle">' + fromUserName + '</div>';
                    html = html + '<div class="messageListContent">' + msg + '</div>';
                    html = html + '<div class="messageListTime">2014-8-30 1:45</div>';
                    html = html + '</div>';
                    html = html + ' </li>';
                }
                html = html + '</ul>';
                $("#messageList").append(html);
            }
        }
    });
};

//联系Owner
Detail.ContactOwner = function () {
    Detail.options.publishId = $("#hidPublishId").val();
    var contacText = $(".contacText").val();
    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "ContactOwner", publishId: Detail.options.publishId, contacText: contacText },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                alert("发送失败");
            } else if (data.MSG == "Y") {
                $(".contactDiv").css("display", "none");
                alert("消息已发");
            }
        }
    });
};

Detail.Cancel = function () {
    $(".contactDiv").css("display", "none");
};

//事件绑定
Detail.BindEvent = function () {
    $("#contactOwner").click(function () {
        $(".contactDiv").css("display", "block")
    });
}

//脚本加载事件
$(function () {
    Detail.BindEvent();
    Detail.LoadDialogMsg();
});