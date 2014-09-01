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
    this.options.startPosition = $("#startPosition").text();
    this.options.endPosition = $("#endPosition").text();
    this.options.userLimCount = $("#number_dummy").val();
    this.options.startTime = $(".ownerTime").val();
    this.options.carType = $("#car_dummy").val();
    this.options.carColor = $("#color_dummy").text();
    this.options.ownerPhone = $("#ownerPhone").text();
    this.options.note = $(".txtMessage").val();

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
    $("#messageBtn").click(function () {
        Owner.AddPublic();
    });
}

//脚本加载事件
$(function () {
    Owner.BindEvent();
});