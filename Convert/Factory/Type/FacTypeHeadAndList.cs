using Convert.InterFace;
using Convert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Convert.Facroty;

namespace Convert.Factor
{

    /// <summary>
    /// 用于生成一个表头表体的界面
    /// </summary>
    public class FacTypeHeadAndList : ICreateType
    {
        //获取链接
        //2.替换关键字
        public string GetTem()
        {
            return "";
        }

        public void MakeTem(Tem entity)
        {
            //页面
            Facroty fac = new Facroty();
            foreach (var i in entity.ListOpenList)
            {
                int num = int.Parse(i);
                ICreateList listFac = fac.CreateHLLList((ListModel)num);
                listFac.MakeTem(entity);
            }

        }
    }
}