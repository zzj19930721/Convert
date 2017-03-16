using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convert.Model
{
    public class SqlInfor
    {
        public int DbType { get; set; }
        public string Server { get; set; }
        public string Connection { get; set; }
        public string DbName { get; set; }
        public string UserCard {get;set;}
        public string UserPwd { get; set; }
    }
}