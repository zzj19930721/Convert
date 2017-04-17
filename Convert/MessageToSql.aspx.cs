using Convert.Other;
using LeaRun.DataAccess;
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
    /// <summary>
    /// 用于指定文件转换
    /// </summary>
    public partial class MessageToSql  : BaseOpen
    {
        
        private string IFilePath ;
        private string OFilePath ;
        protected override void Binding()
        {
            fileDir.Text = "/test";
            fileName.Text = "/test/1.txt";
        }
        private class DtXml
        {
            public string TableName { get; set; }
            public string ColumnName { get; set; }


            #region table
            public string Type { get; set; }
            public string DDL { get; set; }
            #endregion

            #region meessage
            public string value { get; set; }
            public string key { get; set; }
            #endregion

            public string GetSql
            {
                get
                {
                    return string.Format("Comment on column  {0}.{1} is '{2}';" + Environment.NewLine, TableName, ColumnName, value);
                }
            }
        }
        private List<DtXml> AddTableRemark()
        {
            List<DtXml> entity = new List<DtXml>();
            string filePath = System.Web.HttpContext.Current.Server.MapPath(IFilePath);
            string[] fileNames = Directory.GetFiles(filePath, "*.xml", SearchOption.AllDirectories);

            foreach (string filename in fileNames)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);

                string xpath = "appconfig/message";
                XmlNodeList list = doc.SelectNodes(xpath);
                foreach (XmlNode xmlNode in list)
                {
                    DtXml model = new DtXml();
                    model.key = xmlNode.Attributes["key"].Value;
                    model.value = xmlNode.Attributes["value"].Value;
                    string[] name = model.key.Split('.');
                    if (name.Count() < 3) continue;
                    model.TableName = name[1];
                    model.ColumnName = name[2];
                    entity.Add(model);
                }
            }
            return entity;
        }


        private bool TestConn()
        {
            bool isVal = true;
            string message = "";
            try
            {
                List<DtXml> entity = AddTableRemark();
                StringBuilder sb = new StringBuilder();

                foreach (var m in entity)
                {
                    sb.Append(m.GetSql);
                }

                string filePath = System.Web.HttpContext.Current.Server.MapPath(OFilePath );
                Convert.OpenFile.WriteFile(sb.ToString(), filePath);
               
            }
            catch (Exception ex)
            {
                isVal = false;
            }
            return isVal;
        }

        protected void Btn_Ok_Click(object sender, EventArgs e)
        {
            IFilePath = fileDir.Text;
            OFilePath = fileName.Text;
            TestConn();
        }
    }
}