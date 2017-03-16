using Convert.Factor;
using Convert.InterFace;
using Convert.Model;
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
using static Convert.Facroty;

namespace Convert
{
    public partial class Fac : BaseOpen
    {

        protected void Page_Load(object sender, EventArgs e)
        {            
            if(!IsPostBack)
            {
                Binding();
            }
        }
        private void Binding()
        {
            //生成类型           
            drop_type.DataSource = GetEnumToDic<TypeModel>();          
            drop_type.DataBind();
            //生成模块
            drop_list.DataSource = GetEnumToDic<ListModel>();
            drop_list.DataBind();
        }


        protected void btn_sub_Click(object sender, EventArgs e)
        {
            try
            {

                LoadData();

                //校验是否配置数据库
                if (TestConn())
                {
                    TypeFac = fac.CreateType((TypeModel)model.OpenType);
                    TypeFac.MakeTem(model);
                }


            }
            catch (Exception ex)
            {

            }


        }

        protected void sql_test_Click(object sender, EventArgs e)
        {
            LoadData();
            //TestConn();

        }
        private class DtXml
        {
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string Text { get; set; }
            public string Key { get; set; }
            public string GetSql
            {
                get {
                    return string.Format("Comment on column  {0}.{1} is '{2}';" + Environment.NewLine, TableName, ColumnName, Text);
                }
            }
        }

        private List<DtXml> AddTableRemark(string path)
        {
            List<DtXml> entity = new List<DtXml>();
            string filePath = System.Web.HttpContext.Current.Server.MapPath(path);
            //var i = Directory.GetFiles(filePath, "*.xml");
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
                    model.Key        = xmlNode.Attributes["key"].Value;
                    model.Text       = xmlNode.Attributes["value"].Value;
                    string[] name = model.Key.Split('.');
                    if (name.Count() < 3) continue;
                    model.TableName  = name[1];
                    model.ColumnName = name[2];
                    entity.Add(model);
                }
            }
            return entity;
        }
        private bool TestConn()
        {
            bool isVal = false;
            string message = "";
            try
            {
                string path = "/Tem/jm/",// "/Tem/jm"
                       temSql,
                       GetTableSql = "select * from user_tables";
                DataTable colDt;
                List<DtXml> entity = AddTableRemark(path);
                StringBuilder sb = new StringBuilder();

                db = new DbHelper(model.Db);
                DtXml temModel = new DtXml();

                //DataTable dt = db.GetDataTable(CommandType.Text, GetTableSql);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    temSql = string.Format("select * from {0} where 1=2", dr["TABLE_NAME"]);
                //    colDt  = db.GetDataTable(CommandType.Text, temSql);
                //}
                int count = 1;
                int num = 0;
                foreach (var m in entity)
                {
                    //if (count <= 1000)
                    //{
                    //    count++;
                        sb.Append(m.GetSql);
                    //}
                    //else
                    //{
                    //    count = 1;
                        //string filename = string.Format("{0}.sql", num++);
                        //string filePath = System.Web.HttpContext.Current.Server.MapPath("/test/"+filename);
                        //Convert.OpenFile.WriteFile(sb.ToString(), filePath);
                    //    sb.Clear();
                    //}
                         
                }

                string filename = string.Format("{0}.sql", "jm");
                string filePath = System.Web.HttpContext.Current.Server.MapPath("/test/" + filename);
                Convert.OpenFile.WriteFile(sb.ToString(), filePath);


                message = string.Format("{0} connection is  {1}", model.Db.Server, "oK");
                isVal = true;
            }
            catch (Exception ex)
            {
                message = string.Format("{0} connection is  {1}", model.Db.Server, "NO");
            }

            this.sql_infor.Text = message;
            return isVal;
        }

        private void LoadData()
        {
            model = new Tem();
            model.nameSpace = this.text_namespace.Text;
            model.module = this.text_modul.Text;
            model.menu = this.text_menu.Text;
            model.TableValue = this.text_value.Text;
            model.OpenType = int.Parse(this.drop_type.SelectedValue);
            model.Db = new SqlInfor();
            model.Db.Server = this.sql_server.Text;
            model.Db.UserCard = this.sql_card.Text;
            model.Db.UserPwd = this.sql_pwd.Text;
            model.Db.DbType = int.Parse(this.sql_type.SelectedValue);
            model.Db.DbName = this.sql_db.Text;
            for (int i = 0; i < this.drop_list.Items.Count; i++)
            {
                if (this.drop_list.Items[i].Selected == true)
                    model.OpenList += this.drop_list.Items[i].Value + "|";
            }
        }
       
    }
}