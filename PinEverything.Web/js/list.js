var List = new Object();

List.options = {
    pageIndex: "1",
    pageSize: "20",
    pubType: "1",//发布类型
    joinType:" "//加入角色
};

//加载历史发布列表
List.hisPubList = function () {
    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "HisPubList", pageIndex: List.options.pageIndex, pageSize: List.options.pageSize },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                parent.location.replace("../callback.aspx");
            } else if (data.MSG == "Y") {
                var hisPubList = data.hisPubList;
                var len = hisPubList.length;
                var html = "";
                for (var i = 0; i < len; i++) {
                    var pubItem = hisPubList[i];
                    var publishId = pubItem.PublishId;
                    var pubTitle = pubItem.PubTitle;
                    var createTime = pubItem.CreateTime;
                    html = html + '<li>';
                    html = html + ' <a href="/detail.aspx?publishId=' + publishId + '" class="listTitle">' + pubTitle + '</a>';
                    html = html + '<div class="listTime">' + createTime + '</div>';
                    html = html + '</li>';
                }
                $("#publist").find(" #pubUl").append(html);
            }
        }
    });
};

//加载历史加入列表
List.hisJoinList = function () {
    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "HisJoinList", pageIndex: List.options.pageIndex, pageSize: List.options.pageSize },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                parent.location.replace("../callback.aspx");
            } else if (data.MSG == "Y") {
                var hisJoinList = data.hisJoinList;
                var len = hisJoinList.length;
                $("#joinList").find(" #joinUl").html('');
                var html = '';
                for (var i = 0; i < len; i++) {
                    var pubItem = hisJoinList[i];
                    var publishId = pubItem.PublishId;
                    var pubTitle = pubItem.PubTitle;
                    var createTime = pubItem.CreateTime;
                    html = html + '<li>';
                    html = html + ' <a href="/detail.aspx?publishId=' + publishId + '" class="listTitle">' + pubTitle + '</a>';
                    html = html + '<div class="listTime">' + createTime + '</div>';
                    html = html + '</li>';
                }
                $("#joinList").find(" #joinUl").append(html);
            }
        }
    });
};

//事件绑定
List.BindEvent = function () {
};

//脚本加载事件
$(function () {
    List.BindEvent();
    List.hisPubList();
});