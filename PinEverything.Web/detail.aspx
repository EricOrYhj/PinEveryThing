<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="PinEverything.Web.detail" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <script src="js/jquery.min.js"></script>
    <script src="js/detail.js"></script>
    <link href="css/css.css" rel="stylesheet">
    <title>详情</title>
</head>
<body>
    <input type="hidden" id="hidPublishId" runat="server" />
    <div class="sidebarBg">
        <div class="sidebarLeft sidebarPlayWidth">
            <a href="javascript:history.go(-1)">
                <img src="images/arrow.png" alt="" /></a>
        </div>
        <div class="sidebarPlay">
            <a href="javascript:Detail.JoinPublic();" id="join" style="display: block;">加入</a>
            <a href="" id="exit">退出</a>
        </div>
        <div class="sidebarTitle">
            详情
        </div>
        <div class="clear"></div>
    </div>
    <div class="detailMeaasge">
        <div class="detailMeaasgeHead" id="userAvatar" runat="server"></div>
        <div class="detailMeaasgeName" id="userName" runat="server"></div>
        <div class="detailMeaasgeContact">
            <div id="contactOwner">联系TA</div>
        </div>
        <div class="clear"></div>
    </div>
<%--    <div class="contactDiv">
        <div class="contactWord">输入</div>
        <div style="padding-left: 20px;">
            <textarea class="contacText"></textarea></div>
        <div style="float: left">
            <input type="button" class="contacTextSubmit" value="提交" onclick="javascript: Detail.ContactOwner()" /></div>
        <div style="float: left">
            <input type="button" class="contacTextCancel" value="取消" onclick="javascript: Detail.Cancel()" /></div>
        <div class="clear"></div>
    </div>--%>

    <div class="owner" style="padding: 0;">
        <ul>
            <li>
                <div class="ownerICon1"></div>
                <div class="ownerTitle" id="startPlace" runat="server">
                    中山西路虹桥路口
                </div>
            </li>
            <li>
                <div class="ownerICon2"></div>
                <div class="ownerTitle" id="destination" runat="server">
                    长宁龙之梦
                </div>
            </li>
            <li>
                <div class="ownerICon3"></div>
                <div class="ownerTitle">
                    周一、周三
                </div>
            </li>
            <li>
                <div class="ownerICon4"></div>
                <div class="ownerTitle">
                    7:30 至 18:00
                </div>
            </li>
            <li>
                <div class="ownerICon5"></div>
                <div class="ownerTitle" id="carType" runat="server">
                    出租车
                </div>
            </li>
            <li>
                <div class="ownerICon9"></div>
                <div class="ownerTitle ownerTitleClear detail" id="members" runat="server">
                    <%--<span>
                        <img src="images/1.jpg" alt="" /></span>
                    <span>
                        <img src="images/1.jpg" alt="" /></span>
                    <span>
                        <img src="images/1.jpg" alt="" /></span>
                    <span>
                        <img src="images/1.jpg" alt="" /></span>--%>
                </div>
            </li>
        </ul>
        <div class="messageTitle">
            留言：
        </div>
    </div>
    <div class="thinkMessage">
        <span></span>我想留言
    </div>
    <div class="messageList" id="messageList">
        <%-- <ul>
       <li>
           <div class="messageListLeft detail">
               <span><img src="images/1.jpg" alt=""/></span>
           </div>
           <div class="messageListRight">
               <div class="messageListTitle">11111</div>
               <div class="messageListContent">测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容</div>
               <div class="messageListTime">2014-8-30 1:45</div>
           </div>
       </li>
        <li>
            <div class="messageListLeft detail">
                <span><img src="images/1.jpg" alt=""/></span>
            </div>
            <div class="messageListRight">
                <div class="messageListTitle">11111</div>
                <div class="messageListContent">测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容</div>
                <div class="messageListTime">2014-8-30 1:45</div>
            </div>
        </li>
        <li>
            <div class="messageListLeft detail">
                <span><img src="images/1.jpg" alt=""/></span>
            </div>
            <div class="messageListRight">
                <div class="messageListTitle">11111</div>
                <div class="messageListContent">测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容</div>
                <div class="messageListTime">2014-8-30 1:45</div>
            </div>
        </li>
        <li>
            <div class="messageListLeft detail">
                <span><img src="images/1.jpg" alt=""/></span>
            </div>
            <div class="messageListRight">
                <div class="messageListTitle">11111</div>
                <div class="messageListContent">测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容</div>
                <div class="messageListTime">2014-8-30 1:45</div>
            </div>
        </li>
        <li>
            <div class="messageListLeft detail">
                <span><img src="images/1.jpg" alt=""/></span>
            </div>
            <div class="messageListRight">
                <div class="messageListTitle">11111</div>
                <div class="messageListContent">测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容</div>
                <div class="messageListTime">2014-8-30 1:45</div>
            </div>
        </li>
        <li>
            <div class="messageListLeft detail">
                <span><img src="images/1.jpg" alt=""/></span>
            </div>
            <div class="messageListRight">
                <div class="messageListTitle">11111</div>
                <div class="messageListContent">测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容测试内容</div>
                <div class="messageListTime">2014-8-30 1:45</div>
            </div>
        </li>
    </ul>--%>
    </div>
</body>
</html>

