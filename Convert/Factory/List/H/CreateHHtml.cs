using Convert.Factor;
using Convert.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convert.Model;
using System.IO;
using static Convert.Facroty;
using Convert.Other;

namespace Convert.Factor
{
    public class CreateHHtml : ICreateList
    {

        public string GetTem()
        {
            string path = "/Tem/Controller/HEAD_LIST.TXT";
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }
        public string GetTem(ListHtmlModel model)
        {
            string str;
            switch (model)
            {
                case ListHtmlModel.index     : str = "/Tem/CSHTML/index.TXT"; break;
                case ListHtmlModel.EditHead  : str = "/Tem/CSHTML/EditHead.TXT"; break;
                case ListHtmlModel.IndexList : str = "/Tem/CSHTML/IndexList.TXT"; break;
                case ListHtmlModel.EditList  : str = "/Tem/CSHTML/EditList.TXT"; break;
                default: throw new Exception();
            }
            return System.Web.HttpContext.Current.Server.MapPath(str);
        }

        public void MakeTem(Tem model)
        {
            for (int i = 0; i < 2; i++)
            {
                //1.读取文本流
                string str = File.ReadAllText( GetTem( (ListHtmlModel) i ) );
                //2.替换文本流
                str = ReplaceTem(str, model);
                //3.生成文本流
                //3.生成文本流
                string path = System.Web.HttpContext.Current.Server.MapPath("/test/TwoPositions.Ent.Web/Areas/" + model.module + "/Views/" + model.menu + "/" + ((ListHtmlModel)i).ToString() + ".cshtml");
                OpenFile.WriteFile(str, path);
            }

        }

        public void MakeTem(Tem model, SqlInfor db)
        {
            throw new NotImplementedException();
        }

        public string ReplaceTem( string str,Tem model)
        {
            str = str.Replace("@PB.NAME_SPACE@", model.nameSpace);
            str = str.Replace("@PB.MODUAL@", model.module);
            str = str.Replace("@PB.MENU@", model.menu);
            str = str.Replace("@PB.HEAD@", model.Head);
            return str;
        }
    }
}