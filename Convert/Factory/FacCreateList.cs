using Convert.Factor;
using Convert.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Convert.Fac;

namespace Convert
{
    public  class Facroty {

        public enum ListModel
        {
            SDO = 0,
            Server = 1,
            Controller = 2,
            Html = 3,
            Model = 4,
            bizXml = 5,
            objectXml = 6
        }

        public enum TypeModel
        {
            Head = 0,
            List = 1,
            HeadAndList = 2,
            HeadAndManyList = 3
            
        }
        public enum ListHtmlModel
        {
            index = 0,
            EditHead = 1,
            IndexList = 2,
            EditList = 3
        }
        public enum ListServerModel
        {
            Server = 0,
            IServer = 1
        }
        public enum ModelModel
        {
            CS = 0,
            Nh = 1
        }

        public ICreateType CreateType(TypeModel model)
        {

            switch (model)
            {
                case TypeModel.HeadAndList: return new FacTypeHeadAndList();
                case TypeModel.Head: return new FacTypeHead();
                case TypeModel.List: return new FacTypeList();
                case TypeModel.HeadAndManyList: return new FacHeadAndManyList();
                default: throw new Exception();
            }

        }

        public ICreateList CreateHList(ListModel model)
        {
            switch (model)
            {
                case ListModel.SDO        : return new CreateSDO();
                case ListModel.Controller : return new CreateHController();
                case ListModel.Html       : return new CreateHHtml();
                case ListModel.Server     : return new CreateServer();
                case ListModel.Model      : return new CreateModel();
                case ListModel.bizXml  : return new CreateModel();
                case ListModel.objectXml: return new CreateModel();
                default: throw new Exception();
            }

        }
        public ICreateList CreateHLLList(ListModel model)
        {
            switch (model)
            {
                case ListModel.SDO: return new CreateSDO();
                case ListModel.Controller: return new CreateHLController();
                case ListModel.Html: return new CreateHLHtml();
                case ListModel.Server: return new CreateServer();
                case ListModel.Model: return new CreateModel();
                case ListModel.bizXml: return new CreateModel();
                case ListModel.objectXml: return new CreateModel();
                default: throw new Exception();
            }

        }


    }

}