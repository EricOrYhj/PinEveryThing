<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="passenger.aspx.cs" Inherits="PinEverything.Web.passenger" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <script src="/js/jquery.min.js"></script>
    <script src="/js/Common.js?v=0.0.7"></script>
    <script src="/js/passenger.js?v=0.0.7"></script>
    <script type="text/javascript"
        src="http://webapi.amap.com/maps?v=1.3&key=638e916deadfc862f823942b67a01c09">
    </script>
    <script src="/js/passenderMap.js?v=0.0.7"></script>
    <link href="css/css.css?v=0.0.7" rel="stylesheet">
    <link href="css/passender.css?v=0.0.7" rel="stylesheet" />
    
    <title>我是乘客</title>
</head>
<body>
    <div class="positionFixed">
        <div class="sidebarBg">
            <div class="sidebarLeft sidebarPlayWidth">
                <a href="javascript:history.go(-1)">
                    <img src="images/arrow.png" alt="" /></a>
            </div>
            <div class="sidebarPlay">
                <a href="/owner.aspx?pubType=2">打车</a>
            </div>
            <div class="sidebarTitle">
                我是乘客
            </div>
            <div class="clear"></div>
        </div>
        <div class="passengerTab">
            <div class="passengerTabLeft active passengerTabClick" rel="passengerList">
                <span class="passengerTabLeftIcon"></span>计划
            <div class="passengerTabArrow"></div>
            </div>
            <div class="passengerTabRight passengerTabClick" rel="passengerMap">
                <span class="passengerTabRightIcon"></span>附近
            <div class="passengerTabArrow"></div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <div class="passengerList" style="margin-top: 110px;">
        <ul id="listUl">
<%--            <li>
                <div class="passengerListLeft">
                    <div class="passengerListLeftHead">
                        <img src="images/1.jpg" alt="" /></div>
                    <div class="passengerListLeftName">Elen</div>
                </div>
                <div class="passengerListRight">
                    <a href="detail.html">
                        <span class="passengerListArrow"></span>
                        <span class="passengerListArrowInner"></span>
                        <div class="passengerListFromAddress">中山西路虹桥路</div>
                        <div class="passengerListToAddress">长宁龙之梦</div>
                        <div class="passengerListTime">9:10 至 18:00</div>
                        <div class="passengerListCar">特斯拉</div>
                        <div class="passengerListDate">8月28日 16:00</div>
                    </a>
                </div>
                <div class="clear"></div>
            </li>
            <li class="passengerListPosition">
                <div class="passengerListLeft">
                    <div class="passengerListLeftHead">
                        <img src="images/1.jpg" alt="" /></div>
                    <div class="passengerListLeftName">Elen</div>
                </div>
                <div class="passengerListRight">
                    <a href="detail.html">
                        <span class="passengerListArrow"></span>
                        <span class="passengerListArrowInner"></span>
                        <div class="passengerListFromAddress">中山西路虹桥路</div>
                        <div class="passengerListToAddress">长宁龙之梦</div>
                        <div class="passengerListTime">9:10 至 18:00</div>
                        <div class="passengerListCar">特斯拉</div>
                        <div class="passengerListDate">8月28日 16:00</div>
                    </a>
                </div>
                <div class="clear"></div>
            </li>
            <li>
                <div class="passengerListLeft">
                    <div class="passengerListLeftHead">
                        <img src="images/1.jpg" alt="" /></div>
                    <div class="passengerListLeftName">Elen</div>
                </div>
                <div class="passengerListRight">
                    <a href="detail.html">
                        <span class="passengerListArrow"></span>
                        <span class="passengerListArrowInner"></span>
                        <div class="passengerListFromAddress">中山西路虹桥路</div>
                        <div class="passengerListToAddress">长宁龙之梦</div>
                        <div class="passengerListTime">9:10 至 18:00</div>
                        <div class="passengerListCar">特斯拉</div>
                        <div class="passengerListDate">8月28日 16:00</div>
                    </a>
                </div>
                <div class="clear"></div>
            </li>
            <li class="passengerListPosition">
                <div class="passengerListLeft">
                    <div class="passengerListLeftHead">
                        <img src="images/1.jpg" alt="" /></div>
                    <div class="passengerListLeftName">Elen</div>
                </div>
                <div class="passengerListRight">
                    <a href="detail.html">
                        <span class="passengerListArrow"></span>
                        <span class="passengerListArrowInner"></span>
                        <div class="passengerListFromAddress">中山西路虹桥路</div>
                        <div class="passengerListToAddress">长宁龙之梦</div>
                        <div class="passengerListTime">9:10 至 18:00</div>
                        <div class="passengerListCar">特斯拉</div>
                        <div class="passengerListDate">8月28日 16:00</div>
                    </a>
                </div>
                <div class="clear"></div>
            </li>
            <li>
                <div class="passengerListLeft">
                    <div class="passengerListLeftHead">
                        <img src="images/1.jpg" alt="" /></div>
                    <div class="passengerListLeftName">Elen</div>
                </div>
                <div class="passengerListRight">
                    <a href="detail.html">
                        <span class="passengerListArrow"></span>
                        <span class="passengerListArrowInner"></span>
                        <div class="passengerListFromAddress">中山西路虹桥路</div>
                        <div class="passengerListToAddress">长宁龙之梦</div>
                        <div class="passengerListTime">9:10 至 18:00</div>
                        <div class="passengerListCar">特斯拉</div>
                        <div class="passengerListDate">8月28日 16:00</div>
                    </a>
                </div>
                <div class="clear"></div>
            </li>
            <li class="passengerListPosition">
                <div class="passengerListLeft">
                    <div class="passengerListLeftHead">
                        <img src="images/1.jpg" alt="" /></div>
                    <div class="passengerListLeftName">Elen</div>
                </div>
                <div class="passengerListRight">
                    <a href="detail.html">
                        <span class="passengerListArrow"></span>
                        <span class="passengerListArrowInner"></span>
                        <div class="passengerListFromAddress">中山西路虹桥路</div>
                        <div class="passengerListToAddress">长宁龙之梦</div>
                        <div class="passengerListTime">9:10 至 18:00</div>
                        <div class="passengerListCar">特斯拉</div>
                        <div class="passengerListDate">8月28日 16:00</div>
                    </a>
                </div>
                <div class="clear"></div>
            </li>--%>
        </ul>
        <div class="loadMorePub" style="display:none">加载更多...</div>
    </div>
    <div class="passengerMap">
        <div id="mapMsg">
            <div id="floatingCirclesG">
<div class="f_circleG" id="frotateG_01">
</div>
<div class="f_circleG" id="frotateG_02">
</div>
<div class="f_circleG" id="frotateG_03">
</div>
<div class="f_circleG" id="frotateG_04">
</div>
<div class="f_circleG" id="frotateG_05">
</div>
<div class="f_circleG" id="frotateG_06">
</div>
<div class="f_circleG" id="frotateG_07">
</div>
<div class="f_circleG" id="frotateG_08">
</div>
</div>
            <span class="loadMsg">正在扫描附近信息。。。</span>
        </div>
        <div id="container"></div>
        <div id="nearbyListMsg">
            <div id="floatingCirclesG">
<div class="f_circleG" id="frotateG_01">
</div>
<div class="f_circleG" id="frotateG_02">
</div>
<div class="f_circleG" id="frotateG_03">
</div>
<div class="f_circleG" id="frotateG_04">
</div>
<div class="f_circleG" id="frotateG_05">
</div>
<div class="f_circleG" id="frotateG_06">
</div>
<div class="f_circleG" id="frotateG_07">
</div>
<div class="f_circleG" id="frotateG_08">
</div>
</div>
            <span class="loadMsg">正在扫描附近信息。。。</span>
        </div>
        <div id="nearbyList">
            <ul></ul>
        </div>
    </div>
</body>
</html>

