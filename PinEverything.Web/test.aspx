<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="PinEverything.Web.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grvData" runat="server"></asp:GridView>
        <asp:Button ID="btnLoadPub" runat="server" Text="加载发布信息" OnClick="btnLoadPub_Click" />
        <asp:Button ID="btnAddPub" runat="server" Text="添加发布信息" OnClick="btnAddPub_Click" />
    </div>
    </form>
</body>
</html>
