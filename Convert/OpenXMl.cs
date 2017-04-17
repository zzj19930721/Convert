using Convert.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Convert
{
    /// <summary>
    /// 宏桥XML转成模型
    /// </summary>
    public  class OpenXMl
    {
        private string fileName = "/test";

        private string messageName = "messageconfig.xml";
        private string messagePath = "appconfig/message";

        private string tableName = "tableconfig.xml";
        private string tablePath = "tables/table";

        public List<DtXml> GetMessage()
        {
            List<DtXml> entity = new List<DtXml>();
            string[] Files = GetXmlFile(fileName, messageName);
            foreach (var i in Files)
            {
                entity.AddRange(ModelFromMessage(i, messagePath));
            }
            return entity;

        }
        public List<DtXml> GetTable()
        {
            List<DtXml> entity = new List<DtXml>();
            string[] Files = GetXmlFile(fileName, tableName);
            foreach (var i in Files)
            {
                entity.AddRange(ModelFromTable(i, tablePath));
            }
            return entity;
        }

        /// <summary>
        /// 获取路径下指定后缀文件
        /// </summary>
        /// <param name="path">/test</param>
        /// <param name="post"> "*.xml"</param>
        /// <returns></returns>
        private string[] GetXmlFile(string path, string post)
        {
            string filePath = System.Web.HttpContext.Current.Server.MapPath(path);
            string[] fileNames = Directory.GetFiles(filePath, post, SearchOption.AllDirectories);
            return fileNames;
        }

        /// <summary>
        /// 将XML转为MODEL
        /// </summary>
        /// <param name="file"></param>
        /// <param name="xpath">"appconfig/message";</param>
        /// <returns></returns>
        private List<DtXml> ModelFromMessage(string file, string xpath)
        {
            XmlDocument doc = new XmlDocument();
            List<DtXml> ls = new List<DtXml>();
            doc.Load(file);

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
                ls.Add(model);
            }

            return ls;
        }
        /// <summary>
        /// 将XML转为MODEL
        /// </summary>
        /// <param name="file"></param>
        /// <param name="xpath">"appconfig/message";</param>
        /// <returns></returns>
        private List<DtXml> ModelFromTable(string file, string xpath)
        {
            XmlDocument doc = new XmlDocument();
            List<DtXml> ls = new List<DtXml>();
            string currTable = "";
            doc.Load(file);

            XmlNodeList list = doc.SelectNodes(xpath);
            foreach (XmlNode xmlNode in list)
            {
           
                currTable = xmlNode.Attributes["name"].Value;

                XmlNodeList tableList = xmlNode.SelectNodes("columns/column");
                foreach (XmlNode tableNode in tableList)
                {
                    DtXml model = new DtXml()  ;
                    model.TableName = currTable;
                    model.ColumnName = tableNode.Attributes["name"].Value;
                    model.value = tableNode.Attributes["text"].Value;
                    model.DDL = tableNode.Attributes["ddlb"].Value;
                    ls.Add(model);
                }

            }

            return ls;
        }

        /// <summary>
        /// 将XML转为MODEL
        /// </summary>
        /// <param name="file"></param>
        /// <param name="xpath">"appconfig/message";</param>
        /// <returns></returns>
        private T XmlToT<T>(string file, string xpath) where T : new()
        {
            //XmlDocument doc = new XmlDocument();
            //T mdoel = new T();
            //doc.Load(file);

            //XmlNodeList list = doc.SelectNodes(xpath);
            //foreach (XmlNode xmlNode in list)
            //{
            //    DtXml model = new DtXml();
            //    model.value = xmlNode.Attributes["key"].Value;
            //    model.value = xmlNode.Attributes["value"].Value;
            //    string[] name = model.key.Split('.');
            //    if (name.Count() < 3) continue;
            //    model.TableName = name[1];
            //    model.ColumnName = name[2];
            //}

            //return mdoel;
            return new T();
        }
    }
}