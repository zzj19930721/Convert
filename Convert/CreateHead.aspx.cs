using Convert.Model;
using LeaRun.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Convert
{
    public partial class CreateHead : System.Web.UI.Page
    {
       public  string json = "";
        protected void Page_Load(object sender, EventArgs e)
        {

           if(Request["TbName"] != null)
            {  
                GetTable(Request);
                Response.Write(json);
                Response.End();
            }
        }
        public void GetTable(HttpRequest Request)
        {
            SqlInfor db = new SqlInfor();
            db.Server = "LocalHost"; //Request["Server"];
            db.DbName = "Doc";// Request["DbName"];
            db.UserCard = "sa";// Request["UserCard"];
            db.UserPwd = "12345";// Request["UserPwd"];
            string tableName = Request["TbName"];

            DbHelper dbhelper = new DbHelper(db);
            string sql = string.Format("select * from {0} where 1 = 2", tableName);
            DataTable dt = dbhelper.GetDataTable(CommandType.Text, sql);

            foreach (DataColumn col in dt.Columns)
                json += col.ColumnName + ",";

        }
    }
}