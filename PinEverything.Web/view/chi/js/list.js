var Passenger = new Object();

Passenger.options = {
    dataType: "LoadPublicList",
    pageIndex: "1",
    pageSize: "5",
    pubType: "1"//发布类型
};

//加载发布列表
Passenger.publicList = function () {
    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "LoadPublicList", pageIndex: Passenger.options.pageIndex, pageSize: Passenger.options.pageSize,pubType:'3' },
        beforeSend: function () {
        },
        success: function (data) {
            if (data.MSG == "N") {
                parent.location.replace("../callback.aspx");
            } else if (data.MSG == "Y") {
                var pubList = data.pubList;
                var pageIndex = data.pageIndex;
                var len = pubList.length;
                var html = '';
                for (var i = 0; i < len; i++) {
                    var pubItem = pubList[i];
                    var publishId = pubItem.PublishId;
                    var pubTitle = pubItem.PubTitle;
                    var startPosition = pubItem.StartPosition;
                    var endPosition = pubItem.EndPosition;
                    var startTime = pubItem.StartTime;
                    var carType = pubItem.CarType;
                    var carColor = pubItem.CarColor;
                    var userId = pubItem.UserId;
                    var avatar = pubItem.Avatar;
                    var userName = pubItem.UserName;
                    var lat = pubItem.Lat;
                    var lng = pubItem.Lng;
                    var joinType = pubItem.JoinType;
                    var createTime = pubItem.CreateTime;

                    var operateStr = new StringBuilder();

                    joinColor = "Yellow";
                    if (joinType == 1) {
                        joinType = "发布人";
                        operateStr.AppendFormat('<a class="btn" href="detail.aspx?publishId={0}">详情</a>', publishId);
                    }
                    else if (joinType == 2) {
                        joinType = "已加入";
                        joinColor = "Blue";
                        //operateStr.AppendFormat('<a class="btn" href="/detail.aspx?publishId={0}">退出</a>', publishId);
                        operateStr.AppendFormat("<a class=\"btn\" onclick=\"Passenger.ExitJoin('" + publishId + "');\" style=\"cursor:pointer;\">退出</a>");
                    }
                    else if (joinType == 3) {
                        joinType = "未加入";
                        joinColor = "Red";
                        //operateStr.AppendFormat('<a class="btn" href="/detail.aspx?publishId={0}">加入</a>', publishId);
                        operateStr.AppendFormat("<a class=\"btn\" onclick=\"Passenger.JoinPublic('" + publishId + "');\" style=\"cursor:pointer;\">加入</a>");
                    }

                    if ((i + (pageIndex - 1) * 5) % 2 == 0)
                        html += '<li>';
                    else
                        html += '<li class="passengerListPosition">';
                    html += '<div class="passengerListLeft">';
                    html += '<div class="passengerListLeftHead">';
                    html += '<img src="' + avatar + '" alt="" style="width:70px;"/></div>';
                    html += '<div class="passengerListLeftName">' + userName + '</div>';
                    html += '</div>';
                    html += '<a href="detail.aspx?publishId=' + publishId + '">';
                    html += '<div class="passengerListRight">';
                    html += '<span class="passengerListArrow"></span>';
                    html += ' <span class="passengerListArrowInner"></span>';
                    html += ' <div class="passengerListFromAddress">' + startPosition + '</div>';
                    html += ' <div class="passengerListToAddress">' + endPosition + '</div>';
                    html += ' <div class="passengerListTime">' + startTime + '</div>';
                    html += ' <div class="passengerListCar">' + carType + '</div>';
                    html += '  <div class="passengerListDate">' + operateStr + '</div>';
                    html += '</div>';
                    html += '</a>';
                    html += '<div class="clear"></div>';
                    html += '</li>';
                }
                $(".passengerList #listUl").append(html);
                $(".loadMorePub").css("display", "none");
            }
        }
    });
};

//加入
Passenger.JoinPublic = function (publishId) {
    if (confirm("确定加入?")) {
        $.ajax({
            url: "/ajaxpage/user.aspx",
            dataType: "JSON",
            data: { op: "JoinPublic", publishId: publishId },
            beforeSend: function () {
            },
            success: function (data) {
                if (data.MSG == "N") {
                    alert("加入失败");
                } else if (data.MSG == "Y") {
                    window.location.href = "detail.aspx?publishId=" + publishId + "";
                    alert("加入成功");
                }
                else if (data.MSG == "S") {
                    alert("你已加入");
                } else if (data.MSG == "M") {
                    alert("人数已满");
                }
            }
        });
    }
};

//退出
Passenger.ExitJoin = function (publishId) {
    if (confirm("确定退出?")) {
        $.ajax({
            url: "/ajaxpage/user.aspx",
            dataType: "JSON",
            data: { op: "ExitJoin", publishId: publishId },
            beforeSend: function () {
            },
            success: function (data) {
                if (data.MSG == "N") {
                    alert("退出失败");
                } else if (data.MSG == "Y") {
                    alert("你已退出");
                    window.location.href = "list.aspx";
                }
            }
        });
    }
}

//滑动加载
Passenger.GetScroll = function () {
    $(window).bind("scroll", function () {
        var bottom = $(document).height() - document.documentElement.scrollTop - document.body.scrollTop - $(window).height();
        if (bottom <= 100) {
            $(".loadMorePub").css("display", "block");
            setTimeout(function () {
                Passenger.options.pageIndex++;
                Passenger.publicList(Passenger.options.pageIndex);
            }, 2000);
        }
    });
}

//事件绑定
Passenger.BindEvent = function () {
    //$(".loadMorePub").click(function () {
    //    var curpageIndex = $("#pageIndex").val();
    //    Passenger.options.pageIndex = Number(curpageIndex) + 1;
    //    Passenger.publicList();
    //});
    $(".passengerTabClick").click(function () {
        $(this).addClass("active").siblings().removeClass("active");
        var className = $(this).attr("rel");
        if (className == 'passengerList') {
            $(".passengerList").fadeIn();
            $(".passengerMap").fadeOut();

            setCookie('passengerViewState', 1);
            viewChange();
            $(".passengerList #listUl").html('');
            Passenger.options.pageIndex = 1;
            Passenger.publicList();
        } else {
            $(".passengerList").fadeOut();
            $(".passengerMap").fadeIn();

            setCookie('passengerViewState', 2);
            viewChange();
        }
    });
}

function viewChange() {
    //页面视图保持
    if (getCookie('passengerViewState')) {
        $('.passengerTabClick').removeClass('active');
        switch (getCookie('passengerViewState')) {
            case '1':
                $('#nearbyListMsg').hide();
                $('#nearbyList').hide();
                $('#mapMsg').hide();
                $('#container').hide();
                $('.passengerTabLeft').addClass("active");
                break;
            case '2':
                $(".passengerList").hide();
                $(".passengerMap").show();
                $('#nearbyListMsg').hide();
                $('#nearbyList').hide();
                $('#mapMsg').show();
                $('#container').show();
                $('.passengerTabRight').addClass("active");
                break;
            case '3':
                $(".passengerList").hide();
                $(".passengerMap").show();
                $('#nearbyListMsg').show();
                $('#nearbyList').show();
                $('#mapMsg').hide();
                $('#container').hide();
                $('.passengerTabRight').addClass("active");
                break;
        }
    }
}

//脚本加载事件
$(function () {
    viewChange();
    Passenger.BindEvent();
    Passenger.publicList();
    Passenger.GetScroll();
});