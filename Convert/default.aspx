<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Convert.WebForm1" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div >
        <asp:TextBox ID="text1" runat="server" Height="600px" Width="40%"   TextMode="MultiLine"></asp:TextBox>
        </div>


        <div  style="width:15%; ">

         <asp:TextBox ID="txt_tb" runat="server"></asp:TextBox><p />
        <asp:DropDownList ID="dd_time" runat="server" DataValueField="Key" DataTextField="Value">        </asp:DropDownList><p />
        <asp:DropDownList ID="dd_type" runat="server" DataValueField="Key" DataTextField="Value">        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="转换" OnClick="Button1_Click" />

        </div>

        <div >
        <asp:TextBox ID="text2" runat="server" Height="600px" Width="40%" TextMode="MultiLine"></asp:TextBox>    
        </div>


    </form>

</body>
</html>
