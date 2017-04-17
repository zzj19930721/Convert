using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convert.Model
{
    public class DtXml
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

        public string GetChinaSql
        {
            get
            {
                return string.Format("Comment on column  {0}.{1} is '{2}';" + Environment.NewLine, TableName, ColumnName, value);
            }
        }
    }
}