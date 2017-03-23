<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageToSql.aspx.cs" Inherits="Convert.MessageToSql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>文件目录</td>
            <td><asp:TextBox ID="fileDir" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>生成文件名</td>
            <td><asp:TextBox ID="fileName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="Btn_Ok" Text="提交" runat="server" OnClick="Btn_Ok_Click" /></td>

        </tr>
    </table>
    </div>
    </form>
</body>
</html>
