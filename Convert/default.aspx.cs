using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Convert
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string msg = "error";
            try
            {
                if (type.SelectedValue == "head")
                {
                    msg = this.ConvertHead(text1.Text);
                }
                else if (type.SelectedValue == "list")
                {
                    msg = this.ConvertList(text1.Text);
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            text2.Text = msg;
        }
        private string ConvertHead(string msg)
        {
            string temp1 = @" var cols = [[    {0}    ]];";

            string text =  Environment.NewLine;

            string[] model = msg.Split('|');
            for (int i = 0; i < model.Length; i++)
            {
                //text += "{ field: '" + model[i] + "', title: '', width: 100 }";
                text += "{ field: '"+ model[i] +"', title: '@Html.DisplayNameFor(model => model." + model[i] + ")', width: 100 }";
                if (i < model.Length - 1) text += ",";
                text += Environment.NewLine; ;
            }

            return string.Format(temp1, text);
        }

        private string ConvertList(string msg)
        {
            return XmlCovertToModel(msg);
        }

        private string XmlCovertToModel(string msg)
        {
            string tem = @"<?xml version=""1.0"" encoding=""gb2312"" ?>  <freeforms> {0}</freeforms> ";
            msg = string.Format(tem, msg);
            XmlDocument model = new XmlDocument();
            model.LoadXml(msg);

            StringReader sRead = new StringReader(msg);
            DataSet ds = new DataSet();
            ds.ReadXml(sRead, XmlReadMode.InferTypedSchema);

            StringBuilder sb = new StringBuilder();
            DataTable row = ds.Tables["row"];
            DataTable col = ds.Tables["col"];
            DataTable lab = ds.Tables["label"];
            DataTable edit = ds.Tables["editor"];

            for (int i = 0; i < row.Rows.Count; i++)
            {
                sb.Append("<tr>");

                DataRow[] colView = col.AsDataView().Table.Select("row_ID= '" + i + "'");
                var rowValue = "";

                for (int k = 0; k < colView.Length; k++)
                {             
                    int num = (int)colView[k]["col_ID"];

                    //10 - 10
                    //if (num % 2 == 0)
                    //{
                    //    rowValue = lab.Select("col_ID = " + num + "")[0]["dbname"] as string;
                    //    sb.AppendFormat(@"<td class=""label-bg"" style=""width: 100px; "">{0}</td>", rowValue);
                    //}
                    //else
                    //{
                    //    rowValue = edit.Select("col_ID = " + num + "")[0]["dbname"] as string;
                    //    sb.AppendFormat(@"<td>  <input type=""text"" size=""10"" id=""{0}"" name=""{0}"" value=""<%=head.{0}%>""/> </td>", rowValue);
                    //}


                    //10 - > 2015

                    if (num % 2 == 0)
                    {
                        rowValue = lab.Select("col_ID = " + num + "")[0]["dbname"] as string;
                        sb.AppendFormat(@"<td style='width: 100px;' class='label-bg'> @Html.LabelFor( x => x.{0}) </td>", rowValue);
                    }
                    else
                    {
                        var temModel = edit.Select("col_ID = " + num + "")[0];
                        rowValue = temModel["dbname"] as string;
                        if (temModel["controltype"].ToString() == "UltraTextEditor")
                        {
                            sb.AppendFormat(@"<td> <input type='text' id='{0}' name ='{0}' class='easyui-validatebox tb' data-options='required: true' value = '@Model.{0}' /> </td>", rowValue);
                        }
                        else
                        {
                            sb.AppendFormat(@" <td>                                <select id=""{0}"" name=""{0}""  class=""easyui-combobox"" style =""width: 200px; height: 27px; ""
                                            title = ""@Html.DisplayNameFor(model => model.{0})"" panelwidth = ""300"" data-options = ""required:true""
                                            panelheight = '200' >
                                        <option id = """" value = """" ></option >
                                           @Html.Raw({0})
                                       </select> </td>", rowValue);
                        }

                    }
                    sb.Append(Environment.NewLine);
                }   

                sb.Append("</tr>");
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

    }
}