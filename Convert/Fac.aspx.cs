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
            TestConn();

        }

        private bool TestConn()
        {
            bool isVal = false;
            string message = "";
            try
            {
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