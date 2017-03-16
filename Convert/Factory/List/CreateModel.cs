using Convert.Factor;
using Convert.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convert.Model;
using System.IO;
using Convert.Other;
using static Convert.Facroty;
using LeaRun.DataAccess;
using System.Data;
using System.Xml;
using System.Text;

namespace Convert.Factor
{
    public class CreateModel : ICreateList
    {
        public string GetModelName(string name)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/Tem/tableconfig.xml");
            string str = File.ReadAllText(path, Encoding.GetEncoding("GB2312"));
            string msg = name;

            StringReader sRead = new StringReader(str);
            DataSet ds = new DataSet();
            ds.ReadXml(sRead, XmlReadMode.InferTypedSchema);
            DataRow[] dr = ds.Tables["column"].Select(" name = '" + name + "'");
            foreach (DataRow d in dr)
            {
                if (d["text"].ToString().Length > 0)
                {
                    msg = d["text"].ToString();
                    break;
                }
            }
            return msg;
        }
        public string GetTem()
        {
            string path = "/Tem/TemModel/HModel.TXT";
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }
        public string GetTem(ModelModel model)
        {
            string path = "";
            switch (model)
            {
                case ModelModel.CS: path = "/Tem/TemModel/CSModel.TXT"; break;
                case ModelModel.Nh: path = "/Tem/TemModel/HModel.TXT"; break;
                default: throw new Exception();
            }
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }
        public void MakeTem(Tem model)
        {
            foreach(var tab in model.ListTableValue)
            { 
                //几种表类型
                for(int i = 0; i < 2; i++)
                { 
                //1.读取文本流
                string str = File.ReadAllText(GetTem(  (ModelModel) i ));
                //2.替换文本流
                str = str.Replace("@PB.NAME_SPACE@", model.nameSpace);
                str = str.Replace("@PB.MODUAL@", model.module);
                str = str.Replace("@PB.MENU@", model.menu);
                str = str.Replace("@PB.HEAD@", tab);

                    string temStr = str;
  
                switch ((ModelModel) i)
                {
                    case ModelModel.CS:
                        GetCS(model, "@PB.MODEL@", temStr, tab);break;
                    case ModelModel.Nh:
                        GetNB(model, "@PB.MODEL@", temStr, tab); ;break;

                }
                }
            }


        }
        public void GetNB(Tem model, string rep, string str, string tableName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/test/TwoPositions.Biz/Resource/Model/" + model.module + "/" + tableName + ".hbm.xml");

            DbHelper db = new DbHelper(model.Db);
            string sql = string.Format("select * from {0} where 1 = 2", tableName);
            DataTable dt = db.GetDataTable(CommandType.Text, sql);
            string sb = "";
            
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == "OID") continue; 
                sb +=string.Format(@"    <property name=""{0}"" column=""{0}"" type=""{1}""  not-null=""{2}"" />", column.ColumnName, column.DataType.Name,  column.AllowDBNull.ToString().ToLower());
                sb += (Environment.NewLine);
            }

            str = str.Replace(rep, sb);
            OpenFile.WriteFile(str, path);
        }
        public void GetCS(Tem model, string rep, string str, string tableName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/test/TwoPositions.Model/Model/"  + model.module + "/" + tableName + ".cs");

            DbHelper db = new DbHelper(model.Db);
            string sql = string.Format("select * from {0} where 1 = 2", tableName);
            DataTable dt = db.GetDataTable(CommandType.Text, sql);
            string sb = "";
            foreach (DataColumn column in dt.Columns)
            {
                try
                {

                    sb += string.Format(@"
            [Display(Name = ""{2}"")]
            public virtual {1} {0} ", column.ColumnName, column.DataType.Name, GetModelName(column.ColumnName));
                    sb += "{ get; set; }";
                }
                catch (Exception ex)
                {

                }
                sb += (Environment.NewLine);
            }
            str = str.Replace(rep, sb);
            OpenFile.WriteFile(str, path);
        }

        public void MakeTem(Tem model, SqlInfor db)
        {
            throw new NotImplementedException();
        }
    }
}