using Convert.Factor;
using Convert.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convert.Model;
using System.IO;
using Convert.Other;
using System.Data;
using LeaRun.DataAccess;
using System.Text;

namespace Convert.Factor
{
    public class CreateHLController : ICreateList
    {
        public string GetTem()
        {
            string path = "/Tem/Controller/HEAD_LIST.TXT";
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }

        public void MakeTem(Tem model)
        {
            //1.读取文本流
            string str = File.ReadAllText(GetTem());
            //2.替换文本流
            str = str.Replace("@PB.NAME_SPACE@", model.nameSpace);
            str = str.Replace("@PB.MODUAL@", model.module);
            str = str.Replace("@PB.MENU@", model.menu);
            str = str.Replace("@PB.HEAD@", model.Head);
            str = str.Replace("@PB.LIST@", model.List.First());
            str = replaceValue(str, model, model.Head, "@PB.HEAD_VALUE@");
            str = replaceValue(str, model, model.List[0], "@PB.LIST_VALUE@");
            //3.生成文本流
            string path = string.Format("/test/TwoPositions.Ent.Web/Areas/{0}/Controllers/{1}Controller.cs", model.module, model.menu);
            path = System.Web.HttpContext.Current.Server.MapPath(path);
            OpenFile.WriteFile(str, path);
        }
        //反射获取dt信息
        public string replaceValue(string str,Tem model, string tableName, string rep)
        {
            DbHelper db = new DbHelper(model.Db);
            string sql = string.Format("select * from {0} where 1 = 2", tableName);
            DataTable dt = db.GetDataTable(CommandType.Text, sql);

            string sb = "";
            foreach (DataColumn column in dt.Columns)
            {
                string isString = column.DataType == typeof(string) ? "" : ".ToString() ";
                sb += @"              yield return new NameValue() { Name = """ + column.ColumnName + @""", Value = entity." + column.ColumnName + isString + @" };";
                sb += (Environment.NewLine);
            }
            str = str.Replace(rep, sb);
            return str;
        }
        
   

        public void MakeTem(Tem model, SqlInfor db)
        {
        }
    }
}