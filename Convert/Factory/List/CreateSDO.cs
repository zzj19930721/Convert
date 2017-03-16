using Convert.Factor;
using Convert.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convert.Model;
using System.IO;
using Convert.Other;

namespace Convert.Factor
{
    public class CreateSDO : ICreateList
    {
        public string GetTem()
        {
            string path = "/Tem/DAO/HEAD_LIST.TXT";
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }


        public void MakeTem(Tem model)
        {
            foreach (var i in model.ListTableValue)
            {
                //1.读取文本流
                string str = File.ReadAllText(GetTem());
                //2.替换文本流
                str = str.Replace("@PB.NAME_SPACE@", model.nameSpace);
                str = str.Replace("@PB.MODUAL@", model.module);
                str = str.Replace("@PB.MENU@", model.menu);
                str = str.Replace("@PB.HEAD@", i);
                //3.生成文本流
                string path = System.Web.HttpContext.Current.Server.MapPath("/test/TwoPositions.Biz/DAO/" + model.module + "/" + i + "Dao.cs");
                OpenFile.WriteFile(str, path);
            }

        }

        public void MakeTem(Tem model, SqlInfor db)
        {
            throw new NotImplementedException();
        }
    }
}