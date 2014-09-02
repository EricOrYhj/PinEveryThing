var Passenger = new Object();

Passenger.options = {
    dataType: "LoadPublicList",
    pageIndex: "1",
    pageSize:"5",
    pubType: "1"//发布类型
};

//加载发布列表
Passenger.publicList = function () {
    $.ajax({
        url: "/ajaxpage/user.aspx",
        dataType: "JSON",
        data: { op: "LoadPublicList", pageIndex: Passenger.options.pageIndex, pageSize: Passenger.options.pageSize},
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
                for (var i = 0; i < len;i++)
                {
                    var pubItem = pubList[i];
                    var publishId= pubItem.PublishId;
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

                    joinColor = "Yellow";
                    if (joinType == 1)
                        joinType = "发布人";
                    else if (joinType == 2) {
                        joinType = "已加入";
                        joinColor = "Blue";
                    }
                    else if (joinType == 3) {
                        joinType = "未加入";
                        joinColor = "Red";
                    }

                    if ((i + (pageIndex-1)*5) % 2 == 0)
                        html += '<li>';
                    else 
                        html += '<li class="passengerListPosition">';
                        html += '<div class="passengerListLeft">';
                        html += '<div class="passengerListLeftHead">';
                        html += '<img src="' + avatar + '" alt="" /></div>';
                        html += '<div class="passengerListLeftName">' + userName + '</div>';
                        html += '</div>';
                        html += '<div class="passengerListRight">';
                        html += '<a href="/detail.aspx?publishId=' + publishId + '">';
                        html += '<span class="passengerListArrow"></span>';
                        html += ' <span class="passengerListArrowInner"></span>';
                        html += ' <div class="passengerListFromAddress">' + startPosition + '</div>';
                        html += ' <div class="passengerListToAddress">' + endPosition + '</div>';
                        html += ' <div class="passengerListTime">' + startTime + '  <span style="color:'+joinColor+';">' + joinType + '</span></div>';
                        html += ' <div class="passengerListCar">' + carType + '</div>';
                        html += '  <div class="passengerListDate">' + createTime + '</div>';
                        html += '</a>';
                        html += '</div>';
                        html += '<div class="clear"></div>';
                        html += '</li>';
                }
                $(".passengerList #listUl").append(html);
                $("#pageIndex").val(pageIndex)
            }
        }
    });
};

//滑动加载
Passenger.GetScroll = function () {
    $(window).bind("scroll", function () {
        var bottom = $(document).height() - document.documentElement.scrollTop - document.body.scrollTop - $(window).height();
        if (bottom <= 50) {
            setTimeout(function () {
                Passenger.options.pageIndex++;
                Passenger.publicList(Passenger.options.pageIndex);
            }, 1000);
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
}

//脚本加载事件
$(function () {
    Passenger.BindEvent();
    Passenger.publicList();
    Passenger.GetScroll();
});