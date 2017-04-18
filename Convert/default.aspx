<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Convert.WebForm1" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/Css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">

        <div  class="col-md-5">
        <asp:TextBox ID="text1" runat="server" Height="600px" Width="100%"   TextMode="MultiLine"></asp:TextBox>
        </div>


        <div class="col-md-2">

        <asp:DropDownList ID="dd_size" runat="server" DataValueField="Key" DataTextField="Value"  CssClass=" form-control"/><p />
         <asp:TextBox ID="txt_tb" runat="server"  CssClass=" form-control" TextMode="MultiLine" >34234</asp:TextBox><p />
        <asp:DropDownList ID="dd_time" runat="server" DataValueField="Key" DataTextField="Value"  CssClass=" form-control">        </asp:DropDownList><p />
        <asp:DropDownList ID="dd_type" runat="server" DataValueField="Key" DataTextField="Value"  CssClass=" form-control">        </asp:DropDownList><p />
        <asp:Button ID="Button1" runat="server" CssClass=" btn-success btn" Text="转换" OnClick="Button1_Click" />

        </div>

        <div  class="col-md-5">
        <asp:TextBox ID="text2" runat="server" Height="600px" Width="100%" TextMode="MultiLine"></asp:TextBox>    
        </div>

        </div>
    </form>

</body>
</html>
