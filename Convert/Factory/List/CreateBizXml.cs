using Convert.Factor;
using Convert.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convert.Model;
using System.IO;
using Convert.Other;
using static Convert.Facroty;

namespace Convert.Factor
{
    public class CreateBizXml : ICreateList
    {
        public string GetTem()
        {
            string path = "/Tem/Server/HEAD_LIST.TXT";
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }
        public string GetTem(ListServerModel model)
        {
            string path = "";
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }

        public void MakeTem(Tem model)
        {
            string str = File.ReadAllText(GetTem());
            foreach (var i in model.ListTableValue)
            {
                for (int k = 0; k < 2; k++)
                {
                    //1.读取文本流


                    //2.替换文本流
                    str = str.Replace("@PB.NAME_SPACE@", model.nameSpace);
                    str = str.Replace("@PB.MODUAL@", model.module);
                    str = str.Replace("@PB.MENU@", model.menu);
                    str = str.Replace("@PB.HEAD@", i );

                    string path = k == 0 ? "/TwoPositions.Biz/Service/Implement/" + model.module + "/" : "/TwoPositions.IService/Service/" + model.module + "/";
                    path = System.Web.HttpContext.Current.Server.MapPath("/test/" + path);
                    //3.生成文本流
                    OpenFile.WriteFile(str, path);
                }
            }
   
        }

        public void MakeTem(Tem model, SqlInfor db)
        {
            throw new NotImplementedException();
        }
    }
}