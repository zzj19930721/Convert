using Convert.Model;
using Convert.Other;
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
using static Convert.WebForm1;

namespace Convert
{
    public partial class WebForm1 : BaseOpen
    {
        //1,ado,mvc 2种时间
        //2.datagrid，free
        public class DataXml
        {
            public List<DtXml> MessageXml;
            public List<DtXml> TableXml;
        }

        private DataXml dataXml = new DataXml();
        private string tableName ;

        #region  tem
        public  class Tem
        {
            public string COLS = @" var cols = [[    {0}    ]];";
            public string NewLine = Environment.NewLine;
            public virtual string ColText { get; }
            public virtual string TableText  {get; }
            public virtual string TableXml  {get; }
            public virtual string MessageXml  {get; }
        }
        public class TemAdo : Tem
        {
            public override string ColText { get { return "{{ field: '{0}', title: '{1}', width: 100 }}"; } }
            public override string TableText { get { return ""; } }
        }
        public class TemMvc : Tem
        {

            public override string ColText { get { return "{{ field: '{0}', title: '{1}', width: 100 }}"; } } 
            public override string TableText { get { return "{{ field: '{0}', title: '@Html.DisplayNameFor(model => model.{1})', width: 100 }}";  } } 
        }
        protected Tem GetTem(EnTime time)
        {
            switch (time)
            {
                case EnTime.Ado: return new TemAdo();
                case EnTime.Mvc: return new TemMvc();
                default: throw new Exception();
            }
        }
        #endregion

        #region Time

        public enum EnTime
        {
            Ado = 0,
            Mvc = 1
        }
        public interface ITimeFac
        {
            ITypeFac ConvertType(EnType type);
        }
        public class ToAdo : ITimeFac
        {
            public ITypeFac ConvertType(EnType type)
            {
                switch (type)
                {
                    case EnType.dataGrid: return new CreTypeDataGrid();
                    case EnType.table: return new CreAdoTable();
                    case EnType.ModelInfor: return new CreTypeModel();
                    default: throw new Exception();
                }
            }
        }
        public class ToMvc : ITimeFac
        {
            public ITypeFac ConvertType(EnType type)
            {
                switch (type)
                {
                    case EnType.dataGrid: return new CreTypeDataGrid();
                    case EnType.table: return new CreTypeTable();
                    default: throw new Exception();
                }
            }
        }
        public ITimeFac CreateTimeFac(EnTime time)
        {
            switch (time)
            {
                case EnTime.Ado: return new ToAdo();
                case EnTime.Mvc: return new ToMvc();
                default: throw new Exception();
            }
        }
        #endregion

        #region Type
        public enum EnType
        {
            dataGrid,
            table,
            ModelInfor
        }
        public interface ITypeFac
        {
            string CreateText(Tem tem, string msg,string tableName, DataXml data);
        }
        public class CreTypeDataGrid : ITypeFac
        {
            public string CreateText(Tem tem, string msg, string tableName, DataXml data)
            {
                string text = Environment.NewLine;
                WebForm1 com = new WebForm1();

                string[] model = msg.Split('|');
                for (int i = 0; i < model.Length; i++)
                {
                    if (string.IsNullOrEmpty(tableName))
                    {
                        text += string.Format(tem.ColText, model[i], model[i]);
                    }
                    else
                    {
                        text += string.Format(tem.ColText, model[i], com.GetChina(model[i], tableName, data));
                    }
                    if (i < model.Length - 1) text += ",";
                    text += tem.NewLine;
                }

                return string.Format(tem.COLS, text);
            }
        }
        public class CreAdoTable : ITypeFac
        {
            public string CreateText(Tem tem, string msg, string tableName, DataXml data)
            {
                string head = @"<?xml version=""1.0"" encoding=""gb2312"" ?>  <freeforms> {0}</freeforms> ";
                msg = string.Format(head, msg);
                XmlDocument model = new XmlDocument();

                //存放保存方法
                StringBuilder SaveModel = new StringBuilder();
                WebForm1 com = new WebForm1();
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

                        if (num % 2 == 0)
                        {
                            rowValue = lab.Select("col_ID = " + num + "")[0]["dbname"] as string;
                            sb.AppendFormat(@"<td class=""label-bg"" style=""width: 100px; "">{0}</td>",com.GetChina( rowValue, tableName, data));
                        }
                        else
                        {
                            rowValue = edit.Select("col_ID = " + num + "")[0]["dbname"] as string;
                            sb.AppendFormat(@"<td>  <input type=""text"" size=""10"" id=""{0}"" name=""{0}"" value=""<%=head.{0}%>""/> </td>", rowValue);
                        }

                        sb.Append(Environment.NewLine);

                        SaveModel.AppendFormat(" head.{0} = Post(\"{0}\");", rowValue);
                        SaveModel.AppendLine();
                    }

                    sb.Append("</tr>");
                    sb.Append(Environment.NewLine);
                }

                return sb.ToString() + tem.NewLine + SaveModel.ToString();
            }
        }
        public class CreTypeModel : ITypeFac
        {
            public string CreateText(Tem tem, string msg, string tableName, DataXml data)
            {

                StringBuilder GetModel = new StringBuilder();
                string DDL = "            yield return new NameValue() {{ Name = \"{0}\", Value = cod.GetDDLTextByValue(\"{1}\", entity.{0}) }};";
                string noDDL = "            yield return new NameValue() {{ Name = \"{0}\", Value = entity.{0} }};";

                var models = data.TableXml.Where(x => x.TableName == tableName);
                foreach (var i in models)
                {
                    if (string.IsNullOrEmpty(i.DDL))
                    {
                        GetModel.AppendFormat(noDDL, i.ColumnName);
                    }
                    else
                    {
                        GetModel.AppendFormat(DDL, i.ColumnName, i.DDL);
                    }
                    GetModel.AppendLine();
                }

                return  GetModel.ToString();
            }
        }

        public class CreTypeTable : ITypeFac
        {
            public string CreateText(Tem tem, string msg, string tableName, DataXml data)
            {
                return "";
                //    string tem = @"<?xml version=""1.0"" encoding=""gb2312"" ?>  <freeforms> {0}</freeforms> ";
                //    msg = string.Format(tem, msg);
                //    XmlDocument model = new XmlDocument();
                //    model.LoadXml(msg);

                //    StringReader sRead = new StringReader(msg);
                //    DataSet ds = new DataSet();
                //    ds.ReadXml(sRead, XmlReadMode.InferTypedSchema);

                //    StringBuilder sb = new StringBuilder();
                //    DataTable row = ds.Tables["row"];
                //    DataTable col = ds.Tables["col"];
                //    DataTable lab = ds.Tables["label"];
                //    DataTable edit = ds.Tables["editor"];

                //    for (int i = 0; i < row.Rows.Count; i++)
                //    {
                //        sb.Append("<tr>");

                //        DataRow[] colView = col.AsDataView().Table.Select("row_ID= '" + i + "'");
                //        var rowValue = "";

                //        for (int k = 0; k < colView.Length; k++)
                //        {
                //            int num = (int)colView[k]["col_ID"];

                //            //10 - 10
                //            //if (num % 2 == 0)
                //            //{
                //            //    rowValue = lab.Select("col_ID = " + num + "")[0]["dbname"] as string;
                //            //    sb.AppendFormat(@"<td class=""label-bg"" style=""width: 100px; "">{0}</td>", rowValue);
                //            //}
                //            //else
                //            //{
                //            //    rowValue = edit.Select("col_ID = " + num + "")[0]["dbname"] as string;
                //            //    sb.AppendFormat(@"<td>  <input type=""text"" size=""10"" id=""{0}"" name=""{0}"" value=""<%=head.{0}%>""/> </td>", rowValue);
                //            //}


                //            //10 - > 2015

                //            if (num % 2 == 0)
                //            {
                //                rowValue = lab.Select("col_ID = " + num + "")[0]["dbname"] as string;
                //                sb.AppendFormat(@"<td style='width: 100px;' class='label-bg'> @Html.LabelFor( x => x.{0}) </td>", rowValue);
                //            }
                //            else
                //            {
                //                var temModel = edit.Select("col_ID = " + num + "")[0];
                //                rowValue = temModel["dbname"] as string;
                //                if (temModel["controltype"].ToString() == "UltraTextEditor")
                //                {
                //                    sb.AppendFormat(@"<td> <input type='text' id='{0}' name ='{0}' class='easyui-validatebox tb' data-options='required: true' value = '@Model.{0}' /> </td>", rowValue);
                //                }
                //                else
                //                {
                //                    sb.AppendFormat(@" <td>                                <select id=""{0}"" name=""{0}""  class=""easyui-combobox"" style =""width: 200px; height: 27px; ""
                //                                title = ""@Html.DisplayNameFor(model => model.{0})"" panelwidth = ""300"" data-options = ""required:true""
                //                                panelheight = '200' >
                //                            <option id = """" value = """" ></option >
                //                               @Html.Raw({0})
                //                           </select> </td>", rowValue);
                //                }

                //            }
                //            sb.Append(Environment.NewLine);
                //        }

                //        sb.Append("</tr>");
                //        sb.Append(Environment.NewLine);
                //    }

                //    return sb.ToString();
                //}
            }
        }
        #endregion

        protected override void Binding()
        {
            //生成类型           
            dd_time.DataSource = GetEnumToDic<EnTime>();
            dd_time.DataBind();
            //生成模块
            dd_type.DataSource = GetEnumToDic<EnType>();
            dd_type.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                tableName = txt_tb.Text;
                if (tableName.Length > 0) LoadXml();

                int currTime = int.Parse(dd_time.SelectedValue);
                int currType = int.Parse(dd_type.SelectedValue);
                var TimeFac = CreateTimeFac((EnTime)currTime);
                var TypeFac = TimeFac.ConvertType((EnType)currType);
                text2.Text = TypeFac.CreateText( GetTem((EnTime)currTime), text1.Text, tableName, dataXml);
            }
            catch (Exception ex)
            {
                text2.Text = ex.ToString();
            }

        }

        private void LoadXml()
        {
            dataXml.MessageXml  = openXml.GetMessage();
            dataXml.TableXml  = openXml.GetTable() ;
        }

        public  string GetChina(string col, string table, DataXml data)
        {
            var china = data.MessageXml.Where(x => x.TableName == table && x.ColumnName == col).FirstOrDefault();
            if (china == null) return col;
            return china.value;
        }

    }
}