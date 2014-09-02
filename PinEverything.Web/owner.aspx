<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="owner.aspx.cs" Inherits="PinEverything.Web.owner" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
<link href="css/css.css?v=0.0.1" rel="stylesheet">
<script src="js/jquery.min.js"></script>
<script src="js/mobiscroll.core.js"></script>
<script src="js/mobiscroll.widget.js"></script>
<script src="js/mobiscroll.scroller.js"></script>
<script src="js/mobiscroll.datetime.js"></script>
<script src="js/mobiscroll.select.js"></script>
<script src="js/mobiscroll.i18n.zh.js"></script>
<script src="js/owner.js"></script>
<link href="css/mobiscroll.widget.css" rel="stylesheet" type="text/css" />
<link href="css/mobiscroll.scroller.css" rel="stylesheet" type="text/css" />
<link href="css/mobiscroll.animation.css" rel="stylesheet" type="text/css" />
<title>我是车主</title>
</head>
<body>
<div class="sidebarBg">
    <div class="sidebarLeft">
        <a href="javascript:history.go(-1)"><img src="images/arrow.png" alt=""/></a>
    </div>
    <div class="sidebarRight"></div>
    <div class="sidebarTitle">
        我是车主
    </div>
    <div class="clear"></div>
</div>
<div class="owner">
    <ul>
        <li class="ownerInput">
            <div class="ownerICon1"></div>
            <div class="ownerTitle">
                <input type="text" class="address" placeholder="请输入始发地地" id="startPosition"/>
            </div>
            <div class="ownerArrow"></div>
        </li>
        <li class="ownerInput">
            <div class="ownerICon2"></div>
            <div class="ownerTitle">
                <input type="text" class="address" placeholder="请输入始发地地" id="endPosition"/>
            </div>
            <div class="ownerArrow"></div>
        </li>
        <li class="ownerCheckBox">
            <div class="ownerICon3"></div>
            <div class="ownerTitle">
                <select id="number" data-role="none">
                    <option value="1">限1人</option>
                    <option value="2">限2人</option>
                    <option value="3">限3人</option>
                    <option value="4">限4人</option>
                    <option value="5">限5人</option>
                    <option value="6">限6人</option>
                    <option value="7">限7人</option>
                    <option value="8">限8人</option>
                    <option value="9">限9人</option>
                </select>
            </div>
            <div class="ownerArrow"></div>
        </li>
        <li>
            <div class="ownerICon4"></div>
            <div class="ownerTitle">
                <input type="text" class="ownerTime" value="2014/9/1"/>
            </div>
            <div class="ownerArrow"></div>
        </li>
        <li>
            <div class="ownerICon5"></div>
            <div class="ownerTitle">
                <select id="car" data-role="none">
                    <option value="大众">大众</option>
                    <option value="本田">本田</option>
                    <option value="福特">福特</option>
                    <option value="雪弗兰">雪弗兰</option>
                    <option value="奔驰">奔驰</option>
                    <option value="宝马">宝马</option>
                    <option value="长城">长城</option>
                    <option value="奥迪">奥迪</option>
                    <option value="丰田">丰田</option>
                    <option value="别克">别克</option>
                    <option value="沃尔沃">沃尔沃</option>
                    <option value="雪铁龙">雪铁龙</option>
                </select>
            </div>
            <div class="ownerArrow"></div>
        </li>
        <li style="display: none;" id="ifTaxi">
            <div class="ownerICon5"></div>
            <div class="ownerTitle">
                出租车
            </div>
        </li>
        <li>
            <div class="ownerICon6"></div>
            <div class="ownerTitle">
                <select id="color" data-role="none">
                    <option value="白色">白色</option>
                    <option value="黑色">黑色</option>
                    <option value="蓝色">蓝色</option>
                    <option value="红色">红色</option>
                    <option value="银色">银色</option>
                    <option value="黄色">黄色</option>
                    <option value="绿色">绿色</option>
                    <option value="香槟色">香槟色</option>
                </select>
            </div>
            <div class="ownerArrow"></div>
        </li>
        <li class="ownerInput">
            <div class="ownerICon7"></div>
            <div class="ownerTitle ownerTitleClear">
                 <input type="text" class="address" placeholder="请输入始发地地" id="ownerPhone"/>
            </div>
            <div class="ownerArrow"></div>
        </li>
    </ul>
    <div class="message">
        <div class="ownerICon8"></div>
        <div class="txtMessageBox">
            <textarea class="txtMessage" placeholder="填写留言信息"></textarea>
        </div>
    </div>
    <a href="javascript:Owner.AddPublic();" id="messageBtn">发布线路</a>
</div>
</body>
</html>
<script>
    $(function () {
        $(".owner").find(".ownerInput").click(function () {
            if (!$(this).find(".ownerOperation").is(":visible")) {
                var inputText = $(this).find(".ownerTitles").text().trim();
                $(this).find(".address").val(inputText);
                $(".ownerOperation").slideUp(100);
                $(this).find(".ownerOperation").slideDown(300);
            }
        });

        $(".fromAddressBtn").click(function () {
            $(this).parents(".ownerTitle").find(".ownerTitles").html($(this).parent().find("input").val());
            $(this).parent().slideUp(100);
            event.stopPropagation();
        });

        $(".fromAddressCancel").click(function () {
            $(this).parent().slideUp(100);
            event.stopPropagation();
        });

        var opt = {
            'datetime': {
                preset: 'datetime',
                minDate: new Date(2014, 7, 18, 09, 00),
                maxDate: new Date(2020, 7, 18, 09, 00),
                stepMinute: 5
            },
            'select': {
                preset: 'select'
            }
        };

        //设置时间
        $('.ownerTime').scroller('destroy').scroller($.extend(opt['datetime'], {
            theme: 'iOS7',
            mode: 'scroller',
            lang: 'zh',
            display: 'bottom',
            animate: 'none'
        }));

        //汽车品牌
        $('#car,#color,#number').scroller('destroy').scroller($.extend(opt['select'], {
            theme: 'iOS7',
            mode: 'scroller',
            lang: 'zh',
            display: 'bottom',
            animate: 'none'
        }));
    })
</script>

