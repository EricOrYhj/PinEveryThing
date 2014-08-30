var Index = new Object();

Index.options = {
};

//加载用户信息
Index.LoadUserDetail = function () {
    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "LoadUserDetail" },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                parent.location.replace("../callback.aspx");
            } else if (data.MSG == "Y") {
                if (data.Users) {
                    Index.LoadAuditUsersHtml(data.Users);
                }
                else {
                    alert("错误");
                }
            }
        }
    });
};

//用户生成HTML
Index.LoadAuditUsersHtml = function (JSON) {
    var item = JSON;
    var username = item.UserName;
};


//事件绑定
Index.BindEvent = function () {
}

//脚本加载事件
$(function () {
    Index.LoadUserDetail();
});