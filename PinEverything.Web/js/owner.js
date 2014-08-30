var Owner = new Object();

Owner.options = {
    dataType: "AddPublic",
    startPlace:"",
    destination:"",
    carType:"",
    carColor:"",
    ownerPhone:"",
    note:"",
    pubType: "1"//发布类型
};

//发布信息
Owner.AddPublic = function () {
    this.options.startPlace = $("#startPlace").text();
    this.options.destination = $("#destination").text();
    this.options.carType = $("#carType").text();
    this.options.carColor = $("#carColor").text();
    this.options.ownerPhone = $("#ownerPhone").text();
    this.options.note = $("#note").text();


    $.ajax({
        type: "POST",
        url: "/ajaxpage/user.aspx",
        data: {
            op: Owner.options.dataType,
            startPlace: Owner.options.startPlace,
            destination: Owner.options.destination,
            carType: Owner.options.carType,
            carColor: Owner.options.carColor,
            ownerPhone: Owner.options.ownerPhone,
            note: Owner.options.note,
            pubType:Owner.options.pubType
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
    $(".messageBtn").click(function () {
        Owner.AddPublic();
    });
}

//脚本加载事件
$(function () {
    Owner.BindEvent();
});