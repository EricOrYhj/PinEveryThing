<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="owner.aspx.cs" Inherits="PinEverything.Web.owner" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
<script src="js/jquery.min.js"></script>
<script src="js/owner.js"></script>
<link href="css/css.css" rel="stylesheet">
<title>我是车主</title>
</head>
<body>
<div class="sidebarBg">
    <div class="project">
        <div class="sidebarLeft">
            <a href="javascript:history.go(-1)"><img src="images/arrow.jpg" alt=""/></a>
        </div>
        <div class="sidebarRight">
            <img src="images/sz1.jpg" alt=""/>
        </div>
        <div class="sidebarTitle">
            我是车主
        </div>
        <div class="clear"></div>
    </div>
</div>
<div class="project">
    <div class="owner">
        <ul>
            <li>
                <div class="ownerICon1"></div>
                <div class="ownerTitle content" contenteditable="true" id="startPlace">
                   菲菲
                </div>
                <div class="ownerArrow"></div>
            </li>
            <li>
                <div class="ownerICon2"></div>
                <div class="ownerTitle content" contenteditable="true" id="destination">
                    长宁龙之梦
                </div>
                <div class="ownerArrow"></div>
            </li>
            <li>
                <div class="ownerICon3"></div>
                <div class="ownerTitle">
                    周一、周三
                </div>
                <div class="ownerArrow"></div>
            </li>
            <li>
                <div class="ownerICon4"></div>
                <div class="ownerTitle">
                    7:30 至 18:00
                </div>
                <div class="ownerArrow"></div>
            </li>
            <li>
                <div class="ownerICon5"></div>
                <div class="ownerTitle" id="carType">
                    特斯拉
                </div>
                <div class="ownerArrow"></div>
            </li>
            <li>
                <div class="ownerICon6"></div>
                <div class="ownerTitle" id="carColor">
                    蓝色
                </div>
                <div class="ownerArrow"></div>
            </li>
            <li>
                <div class="ownerICon7"></div>
                <div class="ownerTitle ownerTitleClear" id="ownerPhone">
                    13333333333333333
                </div>
                <div class="ownerArrow"></div>
            </li>
        </ul>
        <div class="message">
            <div class="ownerICon7"></div>
            <div class="ownerTitle ownerTitleClear" id="note">
                填写留言信息
            </div>
            <div class="ownerArrow"></div>
        </div>
        <div class="messageBtn" style="cursor:pointer;">立即发布</div>
    </div>
</div>
</body>
</html>
