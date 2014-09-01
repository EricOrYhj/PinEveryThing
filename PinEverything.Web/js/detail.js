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
                var msgObj = data.msgObj;
                var userName = msgObj.userName;
                var userAvatar = msgObj.userAvatar;
                var msg = msgObj.msg;

                var userHtml = '<span title="' + userName + '"><img src="' + avatar + '" alt="" /></span>';
                $("#members").append(userHtml);

                var dialogHtml = '';
                dialogHtml = dialogHtml + '<li>';
                dialogHtml = dialogHtml + '<div class="messageListLeft detail">';
                dialogHtml = dialogHtml + '<span><img src="' + userAvatar + '" width="20px" height="20px" alt=""/></span>';
                dialogHtml = dialogHtml + '</div>';
                dialogHtml = dialogHtml + '<div class="messageListRight">';
                dialogHtml = dialogHtml + '<div class="messageListTitle">' + userName + '</div>';
                dialogHtml = dialogHtml + '<div class="messageListContent">' + msg + '</div>';
                dialogHtml = dialogHtml + '<div class="messageListTime">2014-8-30 1:45</div>';
                dialogHtml = dialogHtml + '</div>';
                dialogHtml = dialogHtml + ' </li>';
                $("#messageList").append(dialogHtml);

                $("#sidebarPlay").html("<a href=\"javascript:Detail.ExitJoin();\" id=\"exit\" style=\"display: block;\">退出</a>");

                alert("加入成功");
            }
            else if (data.MSG == "S") {
                alert("你已加入");
            }
        }
    });
};

Detail.ExitJoin = function () {
    Detail.options.publishId = $("#hidPublishId").val();

    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "ExitJoin", publishId: Detail.options.publishId },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                alert("退出失败");
            } else if (data.MSG == "Y") {
                window.location.href = "/passenge.aspx";
            }
        }
    });
};

//获取对话表  和加入成员的信息
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

                var joinMemberList = data.joinMemberList;
                var joinlen = joinMemberList.length;
                var joinHtml = '';
                for (var j = 0; j < joinlen; j++) {
                    var joinMemberItem = joinMemberList[j];
                    var userName = joinMemberItem.UserName;
                    var avatar = joinMemberItem.Avatar;
                    var userId = joinMemberItem.UserId;
                    joinHtml = joinHtml + '<span title="' + userName + '">';
                    joinHtml = joinHtml + '<img src="' + avatar + '" alt="" /></span>';
                }
                $("#members").append(joinHtml);
            }
        }
    });
};

//联系Owner
Detail.ContactOwner = function () {
    Detail.options.publishId = $("#hidPublishId").val();
    var contacText = $("#txtMessage").val();
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
    $(document).on('click', '.thinkMessage', function () {
        if ($(this).hasClass('textmessage')) {
            $(this).removeClass('textmessage');
            $('#txtMessage').slideUp();
            $(this).html("<span></span>我想留言");
            if ($("#txtMessage").val())
            {
                Detail.ContactOwner();
            }
        } else {
            $(this).addClass('textmessage');
            $('#txtMessage').slideDown();
            $(this).html("<span></span>确认");
        }
    });
}

//脚本加载事件
$(function () {
    Detail.BindEvent();
    Detail.LoadDialogMsg();
});