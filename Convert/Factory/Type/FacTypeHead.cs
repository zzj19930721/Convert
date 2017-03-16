using Convert.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convert.Model;
using static Convert.Facroty;

namespace Convert.Factor
{

    /// <summary>
    /// 用于生成一个表头表体的界面
    /// </summary>
    public class FacTypeHead : ICreateType
    {
        public string GetTem()
        {
            throw new NotImplementedException();
        }

        public void MakeTem(Tem entity)
        {
            //页面
            Facroty fac = new Facroty();
            foreach (var i in entity.ListOpenList)
            {
                int num = int.Parse(i);
                ICreateList listFac = fac.CreateHList((ListModel)num);
                listFac.MakeTem(entity);
            }
        }
    }
}