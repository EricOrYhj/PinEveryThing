<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="PinEverything.Web.view.chi.list" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <script src="/js/jquery.min.js"></script>
    <script src="/js/Common.js?v=0.0.8"></script>
    <script src="js/list.js?v=0.0.8"></script>
    <script type="text/javascript"
        src="http://webapi.amap.com/maps?v=1.3&key=638e916deadfc862f823942b67a01c09">
    </script>
    <script src="js/listMap.js?v=0.0.8"></script>
    <link href="css/css.css?v=0.0.8" rel="stylesheet">
    <link href="css/list.css?v=0.0.8" rel="stylesheet" />
    
    <title>拼吃列表</title>
</head>
<body>
    <div class="positionFixed">
        <div class="sidebarBg">
            <div class="sidebarLeft sidebarPlayWidth">
                <a href="javascript:history.go(-1)">
                    <img src="/images/arrow.png" alt="" /></a>
            </div>
            <div class="sidebarPlay">
                <a href="/owner.aspx?pubType=2">发布</a>
            </div>
            <div class="sidebarTitle">
                拼吃列表
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
        </ul>
        <div class="loadMorePub" style="display:none">加载更多...</div>
    </div>
    <div class="passengerMap">
        <div id="mapMsg"  style="display: block;top:105px" class="positionFixed">
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
        <div id="nearbyListMsg"  style="display: block;top:105px" class="positionFixed">
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
