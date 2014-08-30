var mapObj, markers = [], cluster;

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
            dragEnable: true,
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
            autoPosition: true//禁止自动定位
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
    var m = CreateMark(lat, lng);
    var c = CreateCircle(lat, lng, 1000);
    PanTo(lat, lng);

    //弹出标注是我的位置
    var infoW = CreateInfoWindow(m, '当前位置<br />附近有<span style="color:red;">100</span>条发布信息');
    infoW.open(mapObj, m.getPosition());

}