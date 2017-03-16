using Convert.InterFace;
using Convert.Model;
using LeaRun.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Convert.Other
{
    public  class BaseOpen : System.Web.UI.Page
    {
        /// <summary>
        /// 类型生产工厂
        /// </summary>
        public ICreateType TypeFac;

        /// <summary>
        /// 数据库信息
        /// </summary>
        public DataSet ds;

        /// <summary>
        /// 生成器模型，包含数据库模型
        /// </summary>
        protected Tem  model ;

        /// <summary>
        /// 类型生成器
        /// </summary>
        protected Facroty fac = new Facroty();

        /// <summary>
        /// 数据库连接符
        /// </summary>
        protected DbHelper db;

        protected Dictionary<int, string> GetEnumToDic<T>()
        {
            Dictionary<int, string> obj = new Dictionary<int, string>();
            foreach (var i in Enum.GetValues(typeof(T)))
            {
                obj.Add((int)i, ((T)i).ToString());
            }
            return obj;
        }

    }
}