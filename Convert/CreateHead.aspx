<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateHead.aspx.cs" Inherits="Convert.CreateHead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Content/Css/bootstrap.css" rel="stylesheet" />
    <link href="/Content/Css/jquery-ui.css" rel="stylesheet" />
    <script src="/Content/Javascript/jquery-1.7.1.min.js"></script>
    <script src="/Content/Javascript/bootstrap.min.js"></script>
    <script src="/Content/Javascript/jquery-ui.min.js"></script>
    <style type="text/css">
        tr td {
            height: 40px;
            width: 70px;
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <script type="text/javascript">
        var tabAttr = "";
        var currType = "Input";
        $(function () {
            //用于切换标签类型


            $("#btn_sure").click(function () {
                //用于获取表名，然后拼接成拖拉控件
                $.get("/CreateHead.aspx", $("#frm_db").serialize(), function (data) {
                    columnData = data;
                    var dataList = data.split(',');
                    var str = "";

                    dataList.forEach(function (i) {
                        str += "        <div type='button' class=\"drag btn-default btn\" name=\"" + i + "\"  >" + i + "</div>";
                    })

                    $("#dropInput").html(str);

                    $('.drag').draggable({
                        helper: "clone",
                        cursor: "move"
                    });
                })
            })

            $("#myTab a").click(function () {
                currType = $(this).attr("name");
                console.log(currType);

            })

            function GetTem(type)
            {
                switch (type)
                {
                    case "Input": return '<input type="text" class="input-sm" name="{1}" id="{0}" {2}/>';
                    case "Date": return '<input type="datetime" class="input-sm" name="{0}" id="{0}" {1} {2} />';
                    case "Select": return '<input type="text" name="{0}" class="input-sm" id="{0}" {1} {2}/>';
                    case "Label": return '<label class="label-default" class="label-default" {1} {2}>{0}</label>';
                }
                return '{0}{1}{2}';
            }

            function format(text, one, two, three)
            {
                text = text.replace('{0}', one);
                text = text.replace('{1}', two);
                text = text.replace('{2}', three);
                return text;

            }
            //create table
            $("#btn_Tab").click(function () {
                var row = $("#row").val();
                var col = $("#col").val();
                var text = "";

                for (var i = 0; i < row; i++) {
                    text += "   <tr >";
                    for (var k = 0; k < col; k++) {
                        text += "   <td class='td'>  </td>";
                    }
                    text += "   </tr>";
                }
                //$("#tabCon").html("");
                $("#tabCon").append(text);

                $('.td').droppable({
                   
                    drop: function (event, ui) {                      
                        var temFor = GetTem(currType);
                        var name = ui.draggable.clone().attr("name");
                        tabAttr = $("#tabAttr").val();
                        var source = $(format(temFor, name, "", tabAttr));
                        var $img = $('<img />', {
                            src: 'btn_delete.png',
                            style: 'display:none',
                            click: function () {
                                source.remove();
                            }
                        }).appendTo(source);

                        source.mouseenter(function () {
                            $(this).find("img").show();
                        });

                        source.mouseleave(function () {
                            $(this).find("img").hide();
                        });

                        $(this).html(source);

                    }
                });

            })

            $("#tabCon tr td").live("dblclick", function () {
                var colNum = $(this).attr("colspan");
                if (confirm("确定：合并当前与下一个单元格。取消：拆分当前单元格，左边为一个？")) {
                    if (colNum != undefined) {
                        $(this).attr("colspan", colNum / 1 + 1);
                    }
                    else {
                        $(this).attr("colspan", 2);
                    }
                    $(this).next().remove();
                }
                else
                {
                    //拆分单元格
                    if (colNum == undefined) {
                        alert("兄弟，没有拆分什么");
                    }
                    else {
                        $(this).attr("colspan", colNum / 1 - 1);
                        console.log($(this).parent);
                        $(this).after("<td></td>");
                    }
                }
            });
        })
    </script>

    <div class=" container">
        <div class="row">
            <div class="col-md-4">
                <div class=" row">
                    <h2 class="h4 ">数据库连接</h2>
                    <form id="frm_db" class="form-inline ">
                        <input type="text" id="Server" class=" input-sm " name="Server" placeholder="Server" /><p />
                        <input type="text" id="DbName" class=" input-sm " name="DbName" placeholder="DbName" /><p />
                        <input type="text" id="UserCard" class=" input-sm " name="UserCard" placeholder="UserCard" /><p />
                        <input type="text" id="UserPwd" class=" input-sm " name="UserPwd" placeholder="UserPwd" /><p />
                        <input type="text" id="TbName" class=" input-sm " name="TbName" placeholder="tableName" /><p />
                        <input type="button" id="btn_sure" class=" btn-success btn text-center" value="获取" />
                    </form>
                </div>
                <div class="row">
                    <h2 class="h4 ">table生成</h2>
                    <input type="text" id="col" placeholder="col" class=" input-group-sm" /><p />
                    <input type="text" id="row" placeholder="row" class="input-group-sm" /><p />
                    <input type="button" id="btn_Tab" class="btn btn-success" value="生成" /><p />
                </div>
                <div class="row">
                    <h2 class="h4 ">基本信息配置</h2>
                    <input type="text" class=" input-sm" style="width: 300px;" placeholder="tabAttr" id="tabAttr" /><p />
                </div>
            </div>
            <div class="col-md-8">

                <div class="row ">
                    <h2 class="h4  ">工具栏</h2>
                    <ul id="myTab" class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" name="Input">Input</a></li>
                        <li><a  data-toggle="tab" name="Select">Select</a></li>
                        <li><a  data-toggle="tab" name="Label">Label</a></li>
                        <li><a  data-toggle="tab" name="Date">Date</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="dropInput">
                        </div>
                    </div>
                </div>

                <div class="row">
                    <table class=" table table-condensed table-bordered table-hover " style="border: 1px  dashed black;" id="tabCon">
                    </table>
                </div>

            </div>

        </div>


    </div>


</body>
</html>
