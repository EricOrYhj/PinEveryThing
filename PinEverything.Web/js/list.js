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
                    var pubType = pubItem.PubType;
                    var pubName="拼车"
                    if (pubType == "3") {
                        html = html + '<a href="/view/chi/detail.aspx?publishId=' + publishId + '" ><li>';
                        pubName = "拼吃";
                    }
                    else if (pubType == "4") {
                        html = html + '<a href="/view/wan/detail.aspx?publishId=' + publishId + '" ><li>';
                        pubName = "拼玩";
                    }
                    else if (pubType == "5") {
                        html = html + '<a href="/view/wan/detail.aspx?publishId=' + publishId + '" ><li>';
                        pubName = "拼游";
                    }
                    else {
                        html = html + '<a href="/detail.aspx?publishId=' + publishId + '" ><li>';
                        if (pubType == "2")
                        {
                            pubName = "打车";
                        }
                    }
                    html = html + ' <div class="listTitle">' + pubTitle + '</div>';
                    html = html + ' <div style="position: absolute;top: 0;right: 100px;font-size: 12px;">' + pubName + '</div>';
                    html = html + '<div class="listTime">' + createTime + '</div>';
                    html = html + '</li></a>';
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
                    var pubType = pubItem.PubType;
                    var pubName = "拼车"
                    if (pubType == "3") {
                        html = html + '<a href="/view/chi/detail.aspx?publishId=' + publishId + '" ><li>';
                        pubName = "拼吃";
                    }
                    else if (pubType == "4") {
                        html = html + '<a href="/view/wan/detail.aspx?publishId=' + publishId + '" ><li>';
                        pubName = "拼玩";
                    }
                    else if (pubType == "5") {
                        html = html + '<a href="/view/wan/detail.aspx?publishId=' + publishId + '" ><li>';
                        pubName = "拼游";
                    }
                    else {
                        html = html + '<a href="/detail.aspx?publishId=' + publishId + '" ><li>';
                        if (pubType == "2") {
                            pubName = "打车";
                        }
                    }
                    html = html + ' <div class="listTitle">' + pubTitle + '</div>';
                    html = html + ' <div style="position: absolute;top: 0;right: 100px;font-size: 12px;">' + pubName + '</div>';
                    html = html + '<div class="listTime">' + createTime + '</div>';
                    html = html + '</li></a>';
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