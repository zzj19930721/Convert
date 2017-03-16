using Convert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convert.InterFace
{
    /// <summary>
    /// 用于操作ado，controller的功能
    /// </summary>
    public  interface ICreateList
    {
        string GetTem();
        void MakeTem(Tem model);
        void MakeTem(Tem model, SqlInfor db);
    }
}
