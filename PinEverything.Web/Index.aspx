<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PinEverything.Web.Index" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <script src="js/jquery.min.js"></script>
    <script src="js/index.js?v=0.0.6"></script>
    <script type="text/javascript"
        src="http://webapi.amap.com/maps?v=1.3&key=638e916deadfc862f823942b67a01c09">
    </script>
    <script src="/js/mapbase.js?v=0.0.6"></script>
    <link href="/css/css.css?v=0.0.6" rel="stylesheet">
    <link href="/css/index.css?v=0.0.6" rel="stylesheet" />
    <title>拼你所想</title>
</head>
<body class="indexBg">
    <div class="project">
        <div class="header">
            <div class="headerLeft">
                <a href="javascript:;" class="headerLeftIcon"></a>
            </div>
            <div class="headerRight">
                <a href="/list.aspx" class=""></a>
            </div>
            <div class="headerContent">
                <div class="headerContentIcon">
                    <span><div id="avatar" runat="server" visible="true"></div></span>
                </div>
                <div class="headerContentTitle" id="userNanme" runat="server" visible="true"></div>
                <div class="headerContentArrow"></div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <div class="indexContent">
        <div class="passengerMap">
            <div id="mapMsg"></div>
            <div id="container"></div>
            <%--<div id="geocoderResult"></div>--%>
        </div>
        <div class="project">
            <div class="indexContentMain">
                <ul>
                    <li class="borderColor1"><a href="/pinche.html">拼车</a></li>
                    <li class="borderColor2"><a href="">拼吃</a></li>
                    <li class="borderColor3"><a href="">拼旅</a></li>
                    <li class="borderColor4"><a href="">拼玩</a></li>
                    <div class="clear"></div>
                </ul>
            </div>
        </div>
    </div>
</body>
</html>

