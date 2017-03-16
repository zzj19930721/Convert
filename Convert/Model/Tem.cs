using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convert.Model
{
    public class Tem
    {
        /// <summary>
        /// 数据连接模型
        /// </summary>
        public SqlInfor Db { get; set; }
        //命名空间
        public string nameSpace { get; set; }
        //项目名
        public string module { get; set; }
        //菜单
        public string menu { get; set; }

        public string TableValue { get; set ; }
        public List<string> ListTableValue
        {
            get { return TableValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList(); }
        }

        public int OpenType { get;set; }

        /// <summary>
        /// 需要生成的模板
        /// </summary>
        public string OpenList { get; set; }
        public List<string> ListOpenList
        {
            get { return OpenList.Split( new char[] { '|'},StringSplitOptions.RemoveEmptyEntries ).ToList(); }
        }

        public string Head { get { return ListTableValue[0]; } }
        public List<string> List { get { return ListTableValue.Skip(1).ToList(); } }
    }
}