﻿using Convert.Other;
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
    public partial class MessageToSql : BaseOpen
    {
        
        private string IFilePath = "/test/";
        private string OFilePath = "/test/1.txt";
        protected void Page_Load(object sender, EventArgs e)
        {
            TestConn();
        }
        private class DtXml
        {
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string Text { get; set; }
            public string Key { get; set; }
            public string GetSql
            {
                get
                {
                    return string.Format("Comment on column  {0}.{1} is '{2}';" + Environment.NewLine, TableName, ColumnName, Text);
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
                    model.Key = xmlNode.Attributes["key"].Value;
                    model.Text = xmlNode.Attributes["value"].Value;
                    string[] name = model.Key.Split('.');
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

                db = new DbHelper(model.Db);
                DtXml temModel = new DtXml();

                int count = 1;
                int num = 0;
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
    }
}