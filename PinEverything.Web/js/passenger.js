var Passenger = new Object();

Passenger.options = {
    dataType: "LoadPublicList",
    pageIndex: "1",
    pageSize:"20",
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
                var len = pubList.length;
                var html = '<ul>';
                for (var i = 0; i < len;i++)
                {
                    var pubItem = pubList[i];
                    var publishId= pubItem.PublishId;
                    var pubTitle = pubItem.PubTitle;
                    var startPosition = pubItem.StartPosition;
                    var endPosition = pubItem.EndPosition;
                    var carType = pubItem.CarType;
                    var carColor = pubItem.CarColor;
                    var userId = pubItem.UserId;
                    var avatar = pubItem.Avatar;
                    var userName = pubItem.UserName;
                    var lat = pubItem.Lat;
                    var lng = pubItem.Lng;
                    if (i % 2 == 0)
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
                        html += ' <div class="passengerListTime">9:10 至 18:00</div>';
                        html += ' <div class="passengerListCar">' + carType + '</div>';
                        html += '  <div class="passengerListDate">8月28日 16:00</div>';
                        html += '</a>';
                        html += '</div>';
                        html += '<div class="clear"></div>';
                        html += '</li>';
                }
                html = html + '</ul>';
                $(".passengerList").append(html);
            }
        }
    });
};


//事件绑定
Passenger.BindEvent = function () {
}

//脚本加载事件
$(function () {
    Passenger.BindEvent();
    Passenger.publicList();
});