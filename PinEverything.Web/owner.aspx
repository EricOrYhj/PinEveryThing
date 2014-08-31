<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="owner.aspx.cs" Inherits="PinEverything.Web.owner" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link href="css/jquery.mobile-1.4.3.min.css" rel="stylesheet">
    <link href="css/css.css" rel="stylesheet">
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.mobile-1.4.3.min.js"></script>
    <script src="js/owner.js"></script>
    <title>我是车主</title>
</head>
<body>
    <div data-role="page" id="pageMain">
        <div class="sidebarBg">
            <div class="sidebarLeft">
                <a href="javascript:history.go(-1)">
                    <img src="images/arrow.png" alt="" /></a>
            </div>
            <div class="sidebarRight"></div>
            <div class="sidebarTitle">
                我是车主
            </div>
            <div class="clear"></div>
        </div>
        <div class="owner">
            <ul>
                <li>
                    <a href="#fromAddress" data-transition="flow">
                        <div class="ownerICon1"></div>
                        <div class="ownerTitle" id="startPosition">
                            中山西路虹桥路口
                        </div>
                        <div class="ownerArrow"></div>
                    </a>
                </li>
                <li>
                    <a href="#toAddress" data-transition="flow">
                        <div class="ownerICon2"></div>
                        <div class="ownerTitle" id="endPosition">
                            长宁龙之梦
                        </div>
                        <div class="ownerArrow"></div>
                    </a>
                </li>
                <li>
                    <a href="#weekDay" data-transition="flow">
                        <div class="ownerICon3"></div>
                        <div class="ownerTitle">
                            周一、周三
                        </div>
                        <div class="ownerArrow"></div>
                    </a>
                </li>
                <li>
                    <a href="#time" data-transition="flow">
                        <div class="ownerICon4"></div>
                        <div class="ownerTitle">
                            <span class="startH">7</span>:<span class="startM">30</span> 至 18:00
                        </div>
                        <div class="ownerArrow"></div>
                    </a>
                </li>
                <li>
                    <a href="#brand" data-transition="flow">
                        <div class="ownerICon5"></div>
                        <div class="ownerTitle" id="carType">
                            特斯拉
                        </div>
                        <div class="ownerArrow"></div>
                    </a>
                </li>
                <li style="display: none;" id="ifTaxi">
                    <a>
                        <div class="ownerICon5"></div>
                        <div class="ownerTitle" id="taxi">
                            出租车
                        </div>
                    </a>
                </li>
                <li>
                    <a href="#color" data-transition="flow">
                        <div class="ownerICon6"></div>
                        <div class="ownerTitle" id="carColor">
                            蓝色
                        </div>
                        <div class="ownerArrow"></div>
                    </a>
                </li>
                <li>
                    <a href="#tel" data-transition="flow">
                        <div class="ownerICon7"></div>
                        <div class="ownerTitle ownerTitleClear" id="ownerPhone">
                            13333333333333333
                        </div>
                        <div class="ownerArrow"></div>
                    </a>
                </li>
            </ul>
            <a href="#messageBox" data-transition="flow">
                <div class="message">
                    <div class="ownerICon8"></div>
                    <div class="ownerTitle ownerTitleClear" id="note">
                        填写留言信息
                    </div>
                    <div class="ownerArrow"></div>
                </div>
            </a>
            <div id="messageBtn" style="cursor: pointer;">发布线路</div>
        </div>
    </div>

    <div data-role="page" id="fromAddress">
        <div class="modelTitle">始发地</div>
        <input type="text" class="address" />
        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="fromAddressBtn">确定</a>
        <script>
            $(function () {
                $("#fromAddress").find(".address").val($(".owner li").eq(0).find(".ownerTitle").text().trim());
                $("#fromAddressBtn").click(function () {
                    $(".owner li").eq(0).find(".ownerTitle").html($(this).parent().find("input").val());
                });
            })
        </script>
    </div>

    <div data-role="page" id="toAddress">
        <div class="modelTitle">目的地</div>
        <input type="text" class="address" />
        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="toAddressBtn">确定</a>
        <script>
            $(function () {
                $("#toAddress").find(".address").val($(".owner li").eq(1).find(".ownerTitle").text().trim());
                $("#toAddressBtn").click(function () {
                    $(".owner li").eq(1).find(".ownerTitle").html($(this).parent().find("input").val());
                });
            })
        </script>
    </div>

    <div data-role="page" id="brand">
        <div class="modelTitle">轿车品牌</div>
        <input type="text" class="address" />
        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="brandBtn">确定</a>
        <script>
            $(function () {
                $("#brand").find(".address").val($(".owner li").eq(4).find(".ownerTitle").text().trim());
                $("#brandBtn").click(function () {
                    $(".owner li").eq(4).find(".ownerTitle").html($(this).parent().find("input").val());
                });
            })
        </script>
    </div>

    <div data-role="page" id="weekDay">
        <div class="modelTitle">星期几</div>
        <fieldset data-role="controlgroup">
            <input type="checkbox" name="checkbox-v-2c" id="checkbox-v-2a" value="周一">
            <label for="checkbox-v-2a">周一</label>
            <input type="checkbox" name="checkbox-v-2c" id="checkbox-v-2b" value="周二">
            <label for="checkbox-v-2b">周二</label>
            <input type="checkbox" name="checkbox-v-2c" id="checkbox-v-2c" value="周三">
            <label for="checkbox-v-2c">周三</label>
            <input type="checkbox" name="checkbox-v-2c" id="checkbox-v-2d" value="周四">
            <label for="checkbox-v-2d">周四</label>
            <input type="checkbox" name="checkbox-v-2c" id="checkbox-v-2e" value="周五">
            <label for="checkbox-v-2e">周五</label>
            <input type="checkbox" name="checkbox-v-2c" id="checkbox-v-2f" value="周六">
            <label for="checkbox-v-2f">周六</label>
            <input type="checkbox" name="checkbox-v-2c" id="checkbox-v-2g" value="周日">
            <label for="checkbox-v-2g">周日</label>
        </fieldset>
        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="weekDayBtn">确定</a>
        <script>
            $(function () {
                var weekDay = $(".owner li").eq(2).find(".ownerTitle").text().trim().split("、");
                var $name = $("[name=checkbox-v-2c]");
                for (var i = 0; i < weekDay.length; i++) {
                    for (var j = 0; j < $name.length; j++) {
                        if ($name.eq(j).val() == weekDay[i]) {
                            $name.eq(j).attr("checked", "checked");
                        }
                    }
                }
                $("#weekDayBtn").click(function () {
                    var $lable = $("#weekDay").find("label"), html = "";
                    for (var i = 0; i < $lable.length; i++) {
                        if ($lable.eq(i).hasClass("ui-checkbox-on")) {
                            html += $($name[i]).val() + "、";
                        }
                    }
                    html = html.replace(/、$/, "");
                    $(".owner li").eq(2).find(".ownerTitle").html(html);
                });
            })
        </script>
    </div>

    <div data-role="page" id="time">
        <div class="modelTitle">时间选择</div>

        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="timeBtn">确定</a>
        <script>
            $(function () {

            })
        </script>
    </div>

    <div data-role="page" id="color">
        <div class="modelTitle">汽车颜色</div>
        <fieldset data-role="controlgroup">
            <input type="radio" name="radio-choice-v-2" id="radio-choice-v-2a" value="红色">
            <label for="radio-choice-v-2a">红色</label>
            <input type="radio" name="radio-choice-v-2" id="radio-choice-v-2b" value="蓝色">
            <label for="radio-choice-v-2b">蓝色</label>
            <input type="radio" name="radio-choice-v-2" id="radio-choice-v-2c" value="白色">
            <label for="radio-choice-v-2c">白色</label>
            <input type="radio" name="radio-choice-v-2" id="radio-choice-v-2d" value="黑色">
            <label for="radio-choice-v-2d">黑色</label>
            <input type="radio" name="radio-choice-v-2" id="radio-choice-v-2e" value="黄色">
            <label for="radio-choice-v-2e">黄色</label>
        </fieldset>
        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="colorBtn">确定</a>
        <script>
            $(function () {
                var colorName = $(".owner li").eq(6).find(".ownerTitle").text().trim();
                var $name = $("[name=radio-choice-v-2]");
                for (var i = 0; i < $name.length; i++) {
                    if ($name.eq(i).val() == colorName) {
                        $name.eq(i).attr("checked", "checked");
                    }
                }
                $("#colorBtn").click(function () {
                    $(".owner li").eq(6).find(".ownerTitle").html($("[name=radio-choice-v-2]:checked").val());
                });
            })
        </script>
    </div>

    <div data-role="page" id="tel">
        <div class="modelTitle">联系方式</div>
        <input type="text" class="address" />
        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="telBtn">确定</a>
        <script>
            $(function () {
                $("#tel").find(".address").val($(".owner li").eq(7).find(".ownerTitle").text().trim());
                $("#telBtn").click(function () {
                    $(".owner li").eq(7).find(".ownerTitle").html($(this).parent().find("input").val());
                });
            })
        </script>
    </div>

    <div data-role="page" id="messageBox">
        <div class="modelTitle">留言信息</div>
        <textarea class="address"></textarea>
        <a href="#pageMain" data-transition="flow" data-direction="reverse" class="ui-btn" id="messageBoxBtn">确定</a>
        <script>
            $(function () {
                $("#messageBox").find(".address").val($(".message").find(".ownerTitle").text().trim());
                $("#messageBoxBtn").click(function () {
                    $(".message").find(".ownerTitle").html($(this).parent().find("textarea").val());
                });
            })
        </script>
    </div>
</body>
</html>

