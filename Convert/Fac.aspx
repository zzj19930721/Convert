<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fac.aspx.cs" Inherits="Convert.Fac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>模板生成器</title>
    <script src="/Content/Javascript/jquery-1.7.1.min.js"></script>
    <link href="/Content/Css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .pro_config,.sql_config  {
        /*float:left;
        width:40%;*/
        }
        .glyphicon-text-width {}
    </style>
</head>
<body>
    <div class="container">
            <form id="form1" runat="server"  style="float:left" >
                <div class="pro_config row col-md-6">
                    <table class="table table-hover table-responsive">
                        <tr>
                            <td class="">
                                <label>命名空间</label>
                            </td>
                            <td class="col-sm-8">
                                <asp:TextBox ID="text_namespace" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label>项目</label>
                            </td>
                            <td class="col-sm-8">
                                <asp:TextBox ID="text_modul" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label>菜单</label>
                            </td>
                            <td class="col-sm-8">
                                <asp:TextBox ID="text_menu" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label>生成类型</label>
                            </td>
                            <td class="">
                                <asp:DropDownList ID="drop_type" runat="server" DataValueField="Key" DataTextField="Value">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label>表结构（第一个为表头，用 | 分割）</label>
                            </td>
                            <td class="col-sm-8">
                                <asp:TextBox ID="text_value" runat="server" CssClass="glyphicon-text-width" Width="99%" MaxLength="200" Height="136px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label>生成类型 </label>
                            </td>
                            <td class="col-sm-8">
                                <asp:CheckBoxList ID="drop_list" runat="server"  DataValueField="Key" DataTextField="Value" RepeatLayout="Flow"  >
                                </asp:CheckBoxList>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2"  class="align-content text-center">
                                <asp:Button ID="btn_sub" CssClass="btn btn-default btn-sm" runat="server" OnClick="btn_sub_Click" Text="生成" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="sql_config row col-md-6">
                    <table  class="table table-hover">
                        <tr>
                            <td>
                                <label class="">数据库类型</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="sql_type" runat="server">
                                    <asp:ListItem Value="0" Text="ORACLE"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="MSSQL"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="">服务器</label>
                            </td>
                            <td>
                                <asp:TextBox ID="sql_server" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="">账号</label>
                            </td>
                            <td>
                                <asp:TextBox ID="sql_card" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="">密码</label>
                            </td>
                            <td>
                                <asp:TextBox ID="sql_pwd" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="">数据库名</label>
                            </td>
                            <td>
                                <asp:TextBox ID="sql_db" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  class="text-center">
                                <asp:Button ID="sql_test" Text="测试连接" CssClass="btn-success btn" runat="server" OnClick="sql_test_Click" /></td>
                            <td>
                                <asp:Label ID="sql_infor" CssClass="label-danger" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </form>
    </div>
    <div class="row">
        <h2 class=" h2 text-center">样式代码</h2>
    </div>
</body>
</html>
