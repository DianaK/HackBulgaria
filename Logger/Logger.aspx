<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logger.aspx.cs" Inherits="Logger.Logger" Async="True"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txtMessage" placeholder="Message"></asp:TextBox>
        <asp:Button runat="server" ID="btnFileLogger" OnClick="btnFileLogger_OnClick" Text="Log to file"/>
        <asp:Button runat="server" ID="btnHttpLogger" OnClick="btnHttpLogger_OnClick" Text="Log to Http"/>
        <asp:Button runat="server" ID="btnConsoleLogger" OnClick="btnConsoleLogger_OnClick" Text="Log to console"/>
    </div>
    </form>
</body>
</html>
