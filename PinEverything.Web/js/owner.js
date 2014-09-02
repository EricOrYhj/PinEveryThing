var Owner = new Object();

Owner.options = {
    dataType: "AddPublic",
    startPosition: "",
    endPosition: "",
    userLimCount:"",
    startTime:"",
    carType:"",
    carColor:"",
    ownerPhone:"",
    note:"",
    pubType: "1"//发布类型
};

//发布信息
Owner.AddPublic = function () {
    this.options.startPosition = $("#startPosition").val();
    this.options.endPosition = $("#endPosition").val();
    var number_dummy = $("#number_dummy").val();
    this.options.userLimCount = number_dummy.replace(/[^0-9]/ig, "");
    this.options.startTime = $(".ownerTime").val();
    this.options.carType = $("#car_dummy").val();
    this.options.carColor = $("#color_dummy").val();
    this.options.ownerPhone = $("#ownerPhone").text();
    this.options.note = $(".txtMessage").val();
    this.options.pubType = $("#hidPubType").val();

    $.ajax({
        type: "POST",
        url: "/ajaxpage/user.aspx",
        data: {
            op: this.options.dataType,
            startPosition: this.options.startPosition,
            endPosition: this.options.endPosition,
            userLimCount: this.options.userLimCount,
            startTime: this.options.startTime,
            carType: this.options.carType,
            carColor: this.options.carColor,
            ownerPhone: this.options.ownerPhone,
            note: this.options.note,
            pubType: this.options.pubType
        },
        success: function (data) {
            if (data.MSG == "N") {
                alert("发布失败");
            } else if (data.MSG == "Y") {
                window.location.href = "/passenger.aspx";
            }
        }
    });
};


//事件绑定
Owner.BindEvent = function () {
    var pubType = $("#hidPubType").val();
    if (pubType=="2")
    {
        $("#ifTaxi").show().css("display", "box");
        $("#ifCar").css("display", "none");
    }
}

//脚本加载事件
$(function () {
    Owner.BindEvent();
});