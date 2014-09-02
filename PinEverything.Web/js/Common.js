//封装StringBuilder
function StringBuilder() {
    this._string_ = new Array();
}
StringBuilder.prototype.Append = function (str) {
    this._string_.push(str);
}
StringBuilder.prototype.toString = function () {
    return this._string_.join("");
}
StringBuilder.prototype.AppendFormat = function () {
    if (arguments.length > 1) {
        var TString = arguments[0];
        if (arguments[1] instanceof Array) {
            for (var i = 0, iLen = arguments[1].length; i < iLen; i++) {
                var jIndex = i;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[1][i]);
            }
        } else {
            for (var i = 1, iLen = arguments.length; i < iLen; i++) {
                var jIndex = i - 1;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[i]);
            }
        }
        this.Append(TString);
    } else if (arguments.length == 1) {
        this.Append(arguments[0]);
    }
};
//Cookies 操作
//写入
function setCookie(name, value, expire) {
    //过期时间处理
    var expireDate;
    if (!expire) {
        var nextyear = new Date();
        nextyear.setFullYear(nextyear.getFullYear() + 10);
        expireDate = nextyear.toGMTString();
    } else
        expireDate = expire.toGMTString();

    if (document.domain.indexOf('.mingdao.com') == -1) {
        document.cookie = name + "=" + escape(value) + ";expires=" + expireDate + ";path=/";
    } else {
        document.cookie = name + "=" + escape(value) + ";expires=" + expireDate + ";path=/;domain=.mingdao.com";
    }
}
//读取
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) {
        return unescape(arr[2]);
    }
    return null;
}
//删除
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 10000);
    if (getCookie(name) == null) {
        return;
    }
    var cval = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"))[2];
    if (cval != null) {
        if (document.domain.indexOf('.mingdao.com') == -1) {
            document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
        } else {
            document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/;domain=.mingdao.com";
        }

    }
}

