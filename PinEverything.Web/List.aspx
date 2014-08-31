<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="PinEverything.Web.List" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <script src="js/jquery.min.js"></script>
    <script src="js/list.js"></script>
    <link href="css/css.css" rel="stylesheet">
    <title>历史记录</title>
</head>
<body>
    <div class="sidebarBg">
        <div class="sidebarLeft">
            <a href="javascript:history.go(-1)">
                <img src="images/arrow.png" alt="" /></a>
        </div>
        <div class="sidebarRight"></div>
        <div class="sidebarTitle">
            历史记录
        </div>
        <div class="clear"></div>
    </div>
    <div class="passengerTab">
        <div class="passengerTabLeft active passengerTabClick" rel="0">
            <span class="passengerTabLeftIcon"></span>历史发布
        <div class="passengerTabArrow"></div>
        </div>
        <div class="passengerTabRight passengerTabClick" rel="1">
            <span class="passengerTabLeftIcon"></span>历史加入
        <div class="passengerTabArrow"></div>
        </div>
        <div class="clear"></div>
    </div>
    <div class="list" id="list">
        <ul id="listUl">
<%--            <li>
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li class="listBg">
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li>
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li class="listBg">
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li>
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li class="listBg">
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li>
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li class="listBg">
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li>
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>
            <li class="listBg">
                <a href="" class="listTitle">测试标题11111</a>
                <div class="listTime">2014-8-30 10:00</div>
            </li>--%>
        </ul>
        <div class="loadMore">加载更多...</div>
    </div>
    <div class="passengerMap">
    </div>
</body>
</html>
<script>
    $(function () {
        $(".passengerTabClick").click(function () {
            $(this).addClass("active").siblings().removeClass("active");
            var rel = $(this).attr("rel");
        });
    });
</script>

