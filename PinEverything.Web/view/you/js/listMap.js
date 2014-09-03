
//封装StringBuilder
function StringBuilder() {
    this._string_ = new Array();
}
StringBuilder.prototype.Append = function (str) {
    this._string_.push(str);
}
StringBuilder.prototype.toString = function () {
    return this._string_.join("");
}
StringBuilder.prototype.AppendFormat = function () {
    if (arguments.length > 1) {
        var TString = arguments[0];
        if (arguments[1] instanceof Array) {
            for (var i = 0, iLen = arguments[1].length; i < iLen; i++) {
                var jIndex = i;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[1][i]);
            }
        } else {
            for (var i = 1, iLen = arguments.length; i < iLen; i++) {
                var jIndex = i - 1;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[i]);
            }
        }
        this.Append(TString);
    } else if (arguments.length == 1) {
        this.Append(arguments[0]);
    }
};


var mapObj, markers = [], cluster, nearByDataList;

$(function () {
    initialize();
});

function initialize() {
    var
        lng = 121.23259,
        lat = 31.109207;
    var position = new AMap.LngLat(lng, lat);
    mapObj = new AMap.Map("container", {
        view: new AMap.View2D({//创建地图二维视口
            dragEnable: false,
            //center: position,//创建中心点坐标
            zoom: 14, //设置地图缩放级别
            rotation: 0 //设置地图旋转角度
        }),
        lang: "zh_cn"//设置地图语言类型，默认：中文简体
    });//创建地图实例

    mapObj.plugin(["AMap.ToolBar", "AMap.OverView", "AMap.Scale"], function () {
        //加载工具条
        tool = new AMap.ToolBar({
            direction: false,//隐藏方向导航
            ruler: true,//隐藏视野级别控制尺
            autoPosition: false//禁止自动定位
        });
        mapObj.addControl(tool);

        //加载鹰眼
        view = new AMap.OverView();
        mapObj.addControl(view);

        //加载比例尺
        scale = new AMap.Scale();
        mapObj.addControl(scale);
    });

    setTimeout(function () {
        getCurrLocation();
    }, 1000);

    $('.btnShowBannerMenu').on('click', function () {
        if ($(this).hasClass('rightHover')) {
            $(this).removeClass('rightHover');
            $('.warpBannerMenu').slideUp('fast');
        } else {
            $(this).addClass('rightHover');
            $('.warpBannerMenu').slideDown('fast');
        }
    });
}

//创建标注
function CreateMark(lat, lng) {
    var marker = new AMap.Marker({ //自定义构造AMap.Marker对象                 
        map: mapObj,
        position: new AMap.LngLat(lng, lat),
        offset: new AMap.Pixel(-10, -34),
        icon: "http://webapi.amap.com/images/0.png"
    });

    return marker;
}

//创建信息窗体
function CreateInfoWindow(marker, content) {
    var inforWindow = new AMap.InfoWindow({
        isCustom: false,
        offset: new AMap.Pixel(0, -23),
        content: content
    });
    AMap.event.addListener(marker, "click", function (e) {
        inforWindow.open(mapObj, marker.getPosition());
    });

    return inforWindow;
}

//创建圆
function CreateCircle(lat, lng, radius) {
    var circle = new AMap.Circle({
        map: mapObj,//要显示覆盖物的地图对象                 
        center: new AMap.LngLat(lng, lat),//圆心，基点                 
        radius: radius,//半径                 
        strokeColor: "#F33",//线颜色 
        strokeOpacity: 1,//线透明度                 
        strokeWeight: 3,//线宽                 
        fillColor: "#ee2200",//填充颜色                 
        fillOpacity: 0.35//填充透明度                 
    });

    return circle;
}

function PanTo(lat, lng) {
    mapObj.panTo(new AMap.LngLat(lng, lat));
}

function getCurrLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showCurrPosition, handleLocationError);
    }
    else {
        alert('浏览器不支持定位');
    }
}

function handleLocationError(error) {
    try {
        switch (error.code) {

            case 0:

                alert("获取位置信息出错！");

                break;

            case 1:

                alert("您设置了阻止该页面获取位置信息！");

                break;

            case 2:

                alert("浏览器无法确定您的位置！");
                break;

            case 3:
                alert("获取位置信息超时！");
                break;
        }
    } catch (e) {
        alert(e);
    }
}

function showCurrPosition(position) {
    var lat = position.coords.latitude, lng = position.coords.longitude;
    $.ajax({
        type: 'post',
        url: '/ajaxpage/user.aspx',
        data: { op: 'UpdateLbs', lat: lat, lng: lng, showNearbyPub: true, showNotNearPub: true,pubType:'5' },
        success: function (result) {

            //var m = CreateMark(result.lat, result.lng);


            //var m = new AMap.Marker({ //自定义构造AMap.Marker对象                 
            //    map: mapObj,
            //    position: new AMap.LngLat(result.lng, result.lat),
            //    offset: new AMap.Pixel(-10, -34),
            //    icon: "http://webapi.amap.com/images/0.png",
            //    draggable:true
            //});


            geocoder(new AMap.LngLat(result.lng, result.lat));

            PanTo(result.lat, result.lng);

            var c = CreateCircle(result.lat, result.lng, 1000);
            

            var infoWStr = new StringBuilder();

            infoWStr.AppendFormat('<div class="winfoTitle">当前您附近1000米内有{0}条发布信息</div><a href="javascript:showNearbyList();">点击切换到列表视图</a></div>',
                    result.nearbyPubList.length
                );

            $('#mapMsg').html(infoWStr.toString());

            $('#nearbyListMsg').html('<div class="winfoTitle">当前您附近1000米内有' + result.nearbyPubList.length + '条发布信息</div><a href="javascript:showMapView();">点击切换到地图</a></div>');

            //弹出标注是我的位置
            //var infoW = CreateInfoWindow(m, infoWStr.toString());
            //infoW.open(mapObj, m.getPosition());

            var html = '';
            //添加附近的标注点
            for (var i = 0; i < result.nearbyPubList.length; i++) {
                var pubMarker = new AMap.Marker({
                    map: mapObj,
                    position: new AMap.LngLat(result.nearbyPubList[i].Lng, result.nearbyPubList[i].Lat), //基点位置
                    icon: "/images/car_red.png", //marker图标，直接传递地址url
                    offset: { x: -8, y: -34 } //相对于基点的位置
                });

                var showInfoStr = new StringBuilder();
                showInfoStr.AppendFormat('<div class="winfoTitle">【{0}】发布了一条内容</div><div class="">{1}</div><div class=""><a href="detail.aspx?publishId={2}">点击进入详情</a></div>',
                        result.nearbyPubList[i].UserName,
                        result.nearbyPubList[i].PubTitle,
                        result.nearbyPubList[i].PublishId
                    );

                var pubInfoW = CreateInfoWindow(pubMarker, showInfoStr.toString());

                markers.push(pubMarker);


                //构造列表
                var pubItem = result.nearbyPubList[i];
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
                    operateStr.AppendFormat('<a class="btn" href="detail.aspx?publishId={0}">详情</a>', publishId);
                }
                else if (joinType == 3) {
                    joinType = "未加入";
                    joinColor = "Red";
                    operateStr.AppendFormat('<a class="btn" href="detail.aspx?publishId={0}">详情</a>', publishId);
                }

                if (i % 2 == 0)
                    html += '<li>';
                else
                    html += '<li class="passengerListPosition">';
                html += '<div class="passengerListLeft">';
                html += '<div class="passengerListLeftHead">';
                html += '<img src="' + avatar + '" alt="" style="width:70px;"/></div>';
                html += '<div class="passengerListLeftName">' + userName + '</div>';
                html += '</div>';
                html += '<div class="passengerListRight">';
                //html += '<a href="/detail.aspx?publishId=' + publishId + '">';
                html += '<span class="passengerListArrow"></span>';
                html += ' <span class="passengerListArrowInner"></span>';
                html += ' <div class="passengerListFromAddress">' + startPosition + '</div>';
                html += ' <div class="passengerListToAddress">' + endPosition + '</div>';
                html += ' <div class="passengerListTime">' + startTime+ '</div>';
                html += ' <div class="passengerListCar">' + carType + '</div>';
                html += '  <div class="passengerOperate">' + operateStr + '</div>';
                //html += '</a>';
                html += '</div>';
                html += '<div class="clear"></div>';
                html += '</li>';


            }

            $('#nearbyList ul').html(html);

            //添加不是附近的标注点
            for (var i = 0; i < result.notNearbyPubList.length; i++) {
                var pubMarker = new AMap.Marker({
                    map: mapObj,
                    position: new AMap.LngLat(result.notNearbyPubList[i].Lng, result.notNearbyPubList[i].Lat), //基点位置
                    icon: "http://developer.amap.com/wp-content/uploads/2014/06/marker.png", //marker图标，直接传递地址url
                    offset: { x: -8, y: -34 } //相对于基点的位置
                });

                var showInfoStr = new StringBuilder();
                showInfoStr.AppendFormat('<div class="winfoTitle">{0}发布了一条内容</div><div class="">{1}</div><div class=""><a href="/detail.aspx?publishId={2}">点击进入详情</a></div>',
                        result.notNearbyPubList[i].UserName,
                        result.notNearbyPubList[i].PubTitle,
                        result.notNearbyPubList[i].PublishId
                    );

                var pubInfoW = CreateInfoWindow(pubMarker, showInfoStr.toString());

                markers.push(pubMarker);
            }


            addCluster(0);

        }
    });
}

function showNearbyList() {
    $('#nearbyListMsg').show();
    $('#nearbyList').show();
    $('#mapMsg').hide();
    $('#container').hide();
    setCookie('passengerViewState', 3);
}

function showMapView() {
    $('#nearbyListMsg').hide();
    $('#nearbyList').hide();
    $('#mapMsg').show();
    $('#container').show();
    setCookie('passengerViewState', 2);
}

function addCluster(tag) {
    if (cluster) {
        cluster.setMap(null);
    }
    if (tag == 1) {
        var sts = [{ url: "http://developer.amap.com/wp-content/uploads/2014/06/1.png", size: new AMap.Size(32, 32), offset: new AMap.Pixel(-16, -30) },
            { url: "http://developer.amap.com/wp-content/uploads/2014/06/2.png", size: new AMap.Size(32, 32), offset: new AMap.Pixel(-16, -30) },
            { url: "http://developer.amap.com/wp-content/uploads/2014/06/3.png", size: new AMap.Size(48, 48), offset: new AMap.Pixel(-24, -45), textColor: '#CC0066' }];
        mapObj.plugin(["AMap.MarkerClusterer"], function () {
            cluster = new AMap.MarkerClusterer(mapObj, markers, { styles: sts });
        });
    }
    else {
        mapObj.plugin(["AMap.MarkerClusterer"], function () {
            cluster = new AMap.MarkerClusterer(mapObj, markers);
        });
    }
}


function geocoder(position) {
    var MGeocoder;
    //加载地理编码插件
    mapObj.plugin(["AMap.Geocoder"], function () {
        MGeocoder = new AMap.Geocoder({
            radius: 1000,
            extensions: "all"
        });
        //返回地理编码结果
        AMap.event.addListener(MGeocoder, "complete", geocoder_CallBack);
        //逆地理编码
        MGeocoder.getAddress(position);
    });
}

//回调函数
function geocoder_CallBack(data) {
    var road = '';

    if (data.regeocode.roads.length > 0) {
        road = data.regeocode.roads[0].name;

        $.ajax({
            type: 'post',
            url: '/ajaxpage/user.aspx',
            data: { op: 'UpdateLocalState', value: road },
            success: function (result) {

            }
        });
    }
}
