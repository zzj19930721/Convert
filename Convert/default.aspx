<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Convert.WebForm1" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:TextBox ID="text1" runat="server" Height="600px" Width="43%"  TextMode="MultiLine"></asp:TextBox>

        <asp:DropDownList ID="type" runat="server">
            <asp:ListItem Text="查询页面" Value="head"></asp:ListItem>
            <asp:ListItem Text="编辑页面" Value="list"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="转换" OnClick="Button1_Click" />

        <asp:TextBox ID="text2" runat="server" Height="600px" Width="43%" TextMode="MultiLine"></asp:TextBox>    
    </div>

    </form>

</body>
</html>
