using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate.Criterion;
using 3333.Model;
using 3333.IService.Service;
using Framework.Biz.Model;
using 3333.Ent.Web.Controllers;
using 3333.Ent.Web.Authorize;
using 3333.Ent.Web.Models;

namespace 3333.Ent.Web.Areas.333.Controllers
{
    public class 33Controller : BasePageMultiController
    {
        #region 业务逻辑层TestHead1Service
        public ICOM_ENT_INFOService COM_ENT_INFOService { get; set; }
        public I@PB.LIST@Service @PB.LIST@Service { get; set; }
        #endregion

        #region 表头列表 

        public ActionResult Index()
        {
            return View(new COM_ENT_INFO());//new model为了获取界面表格标题
        }

        [RoleAuthorize(FuncCode = 3333.Ent.Web.Services.MenuManager.33)]
        public string QueryHead()
        {
            object obj = Request.Form;
            //base.FilterDeptData = true;//开启机构部门数据过滤
            //base.FilterDeptDataType = FilterType.FilterDeptDataType.DeptCode;
            return base.QueryList<COM_ENT_INFO>(false, delegate (COM_ENT_INFO entity) { return GetHead(entity); }, null);
        }

        protected IEnumerable<NameValue> GetHead(COM_ENT_INFO entity)
        {
              yield return new NameValue() { Name = "OID ", Value = entity.OID };
              yield return new NameValue() { Name = "SEQ_NO ", Value = entity.SEQ_NO };
              yield return new NameValue() { Name = "MODIFY_TYPE ", Value = entity.MODIFY_TYPE };
              yield return new NameValue() { Name = "CHK_STATUS ", Value = entity.CHK_STATUS };
              yield return new NameValue() { Name = "RET_CHANNEL ", Value = entity.RET_CHANNEL };
              yield return new NameValue() { Name = "DECLARE_MARK ", Value = entity.DECLARE_MARK };
              yield return new NameValue() { Name = "CUSTOMS_CODE ", Value = entity.CUSTOMS_CODE };
              yield return new NameValue() { Name = "AREA_CODE ", Value = entity.AREA_CODE };
              yield return new NameValue() { Name = "DOC_TYPE ", Value = entity.DOC_TYPE };
              yield return new NameValue() { Name = "POS_CODE ", Value = entity.POS_CODE };
              yield return new NameValue() { Name = "CHK_TIME ", Value = entity.CHK_TIME.ToString()  };
              yield return new NameValue() { Name = "DEC_TIME ", Value = entity.DEC_TIME.ToString()  };
              yield return new NameValue() { Name = "INPUTER_CODE ", Value = entity.INPUTER_CODE };
              yield return new NameValue() { Name = "INPUTER_NAME ", Value = entity.INPUTER_NAME };
              yield return new NameValue() { Name = "INPUT_COP_CODE ", Value = entity.INPUT_COP_CODE };
              yield return new NameValue() { Name = "INPUT_COP_NAME ", Value = entity.INPUT_COP_NAME };
              yield return new NameValue() { Name = "CREATE_CODE ", Value = entity.CREATE_CODE };
              yield return new NameValue() { Name = "CREATE_NAME ", Value = entity.CREATE_NAME };
              yield return new NameValue() { Name = "CREATE_TIME ", Value = entity.CREATE_TIME.ToString()  };
              yield return new NameValue() { Name = "DOCID ", Value = entity.DOCID };
              yield return new NameValue() { Name = "TRADE_CODE ", Value = entity.TRADE_CODE };
              yield return new NameValue() { Name = "TRADE_NAME ", Value = entity.TRADE_NAME };
              yield return new NameValue() { Name = "ENT_ADDR ", Value = entity.ENT_ADDR };
              yield return new NameValue() { Name = "DECLARE_TYPE ", Value = entity.DECLARE_TYPE };
              yield return new NameValue() { Name = "MASTER_CUSTOMS ", Value = entity.MASTER_CUSTOMS };
              yield return new NameValue() { Name = "ENT_TYPE ", Value = entity.ENT_TYPE };
              yield return new NameValue() { Name = "ENT_CLASSIFY ", Value = entity.ENT_CLASSIFY };
              yield return new NameValue() { Name = "REG_CAPITAL ", Value = entity.REG_CAPITAL };
              yield return new NameValue() { Name = "LEGAL_PERSON ", Value = entity.LEGAL_PERSON };
              yield return new NameValue() { Name = "PERSON ", Value = entity.PERSON };
              yield return new NameValue() { Name = "PHONE ", Value = entity.PHONE };
              yield return new NameValue() { Name = "ACCREDITED_AGENCY ", Value = entity.ACCREDITED_AGENCY };
              yield return new NameValue() { Name = "CREDIT_CODE ", Value = entity.CREDIT_CODE };
              yield return new NameValue() { Name = "TRADE_AREA ", Value = entity.TRADE_AREA };
              yield return new NameValue() { Name = "NOTE ", Value = entity.NOTE };
              yield return new NameValue() { Name = "ORG_CODE ", Value = entity.ORG_CODE };
              yield return new NameValue() { Name = "REG_CO ", Value = entity.REG_CO };
              yield return new NameValue() { Name = "EN_FULL_CO ", Value = entity.EN_FULL_CO };
              yield return new NameValue() { Name = "VALID_DATE ", Value = entity.VALID_DATE.ToString()  };

        }

        public string DeleteHead(string p)
        {
            ExecResult ret = new ExecResult();
            try
            {
                this.COM_ENT_INFOService.Delete(p);
                ret.Success = true;
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.ErrMsg = ex.Message;
            }
            string result = JsonConvert.SerializeObject(ret);
            return result;
        }

        public ActionResult DetailsHead(string p)
        {
            return RedirectToAction("EditHead", new { p = p, details = ActionType.Details });
        }

        public ActionResult AddHead()
        {
            return RedirectToAction("EditHead");
        }

        public ActionResult EditHead(string p, string details)
        {
            COM_ENT_INFO currModel = null;
            if (string.IsNullOrWhiteSpace(p))
            {
                ViewBag.OperationType = ActionType.Add;
            }
            else if (details == ActionType.Details)
            {
                ViewBag.OperationType = ActionType.Details;
            }
            else
            {
                ViewBag.OperationType = ActionType.Edit;
            }
            //
            if (ViewBag.OperationType == ActionType.Details || ViewBag.OperationType == ActionType.Edit)
            {
                currModel = COM_ENT_INFOService.Get(p);
                if (currModel != null)
                {
                    //ViewBag.ComplexId2_Text = Framework.Biz.Service.Common.DropdownMgr.DropdownList("ddlb_ComplexCode").GetTextByValue(currModel.ComplexId2);//用于加载原来的值
                }
                else
                {
                    //ViewBag.ComplexId2_Text = currModel.ComplexId2 + " 对应的数据丢失";
                }
            }
            else
            {
                currModel = new COM_ENT_INFO();
                //新增时,对象初始值
                //currModel.RegDate = currModel.TestDate = DateTime.Now;
            }
            return View(currModel);
        }

        public string SaveHead(string p, COM_ENT_INFO entity)
        {
            //check标签 对应的 bool类型需要特别处理
            if (entity != null)
            {
                //entity.STATUS = GetPageParams("STATUS");
            }
            object obj = Request.Form;
            ExecResult ret = new ExecResult();
            try
            {
                //检查名称是否存在
                var existsObj = COM_ENT_INFOService.Get(entity.OID);
                if (existsObj != null && existsObj.OID != entity.OID)
                {
                    ret.Success = false;
                    ret.ErrMsg = "名称已存在";
                    return ret.ToJson();
                }
                //
                if (p == ActionType.Add)
                {
                    //添加
                    entity.OID = System.Guid.NewGuid().ToString();
                    //添加时,默认值
                    entity.CREATE_TIME = DateTime.Now;
                    //entity.SEQ_NO = this.COM_ENT_INFOService.CreateSeqNo();
                    //entity.LAST_MODIFY_TIME = DateTime.Now;
                    //entity.CREATE_USER = entity.LAST_MODIFY_USER = string.Empty;
                    //
                    COM_ENT_INFOService.Save(entity);
                }
                else
                {

                    //原来的数据
                    COM_ENT_INFO original = this.COM_ENT_INFOService.Get(entity.OID);
                    //entity.LAST_MODIFY_TIME = DateTime.Now;
                    //entity.CREATE_TIME = original.CREATE_TIME;
                    //entity.CREATE_USER = original.CREATE_USER;
                    //修改
                    COM_ENT_INFOService.Update(original);
                }
                ret.DataId = entity.OID;
                ret.Success = true;
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.ErrMsg = ex.Message;
            }
            return ret.ToJson();
        }
        #endregion

        #region 表体列表
        //[RoleAuthorize(FuncCode = 3333.Ent.Web.Services.MenuManager.MENU_TEST2)]
        public ActionResult IndexList(string p)
        {
            ViewBag.pid = p;
            ViewBag.OperationType = GetPageStringParams("details");
            return View(new @PB.LIST@());//new model为了获取界面表格标题
        }

        //[RoleAuthorize(FuncCode = 3333.Ent.Web.Services.MenuManager.33)]
        public string QueryList(string p)
        {
            object obj = Request.Form;
            List<ICriterion> conds = new List<ICriterion>();
            conds.Add(Restrictions.Eq("SEQ_NO", p));
            return base.QueryList<@PB.LIST@>(false, delegate (@PB.LIST@ entity) { return GetList(entity); }, conds);
        }

        protected IEnumerable<NameValue> GetList(@PB.LIST@ entity)
        {
@PB.LIST_VALUE@
        }

        public string DeleteList(string p)
        {
            ExecResult ret = new ExecResult();
            try
            {
                this.@PB.LIST@Service.Delete(p);
                ret.Success = true;
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.ErrMsg = ex.Message;
            }
            string result = JsonConvert.SerializeObject(ret);
            return result;
        }

        public ActionResult DetailsList(string p)
        {
            return RedirectToAction("EditList", new { p = p, details = ActionType.Details });
        }


        public ActionResult AddList(string  p )
        {
            return RedirectToAction("EditList" , new { seq_no = p });
        }

        public ActionResult EditList(string p, string details,string seq_no)
        {
            @PB.LIST@ currModel = null;
            if (string.IsNullOrWhiteSpace(p))
            {
                ViewBag.OperationType = ActionType.Add;
            }
            else if (details == ActionType.Details)
            {
                ViewBag.OperationType = ActionType.Details;
            }
            else
            {
                ViewBag.OperationType = ActionType.Edit;
            }
            //
            if (ViewBag.OperationType == ActionType.Details || ViewBag.OperationType == ActionType.Edit)
            {
                currModel = @PB.LIST@Service.Get(p);
                if (currModel != null)
                {
                    //ViewBag.ComplexId2_Text = Framework.Biz.Service.Common.DropdownMgr.DropdownList("ddlb_ComplexCode").GetTextByValue(currModel.ComplexId2);//用于加载原来的值
                }
                else
                {
                    //ViewBag.ComplexId2_Text = currModel.ComplexId2 + " 对应的数据丢失";
                }
            }
            else
            {
                currModel = new @PB.LIST@();
                currModel.SEQ_NO = seq_no;
                //新增时,对象初始值
                //currModel.RegDate = currModel.TestDate = DateTime.Now;
            }
            return View(currModel);
        }


        public string SaveList(string p, @PB.LIST@ entity)
        {
            //check标签 对应的 bool类型需要特别处理
            if (entity != null)
            {
                //entity.STATUS = GetPageParams("STATUS");
            }
            object obj = Request.Form;
            ExecResult ret = new ExecResult();
            try
            {
                //检查名称是否存在
                var existsObj = @PB.LIST@Service.Get(entity.OID);
                if (existsObj != null && existsObj.OID != entity.OID)
                {
                    ret.Success = false;
                    ret.ErrMsg = "名称已存在";
                    return ret.ToJson();
                }
                //
                if (p == ActionType.Add)
                {
                    //添加
                    entity.OID = System.Guid.NewGuid().ToString();
                    //添加时,默认值
                    //entity.START_DATE = DateTime.Now;
                    //entity.SEQ_NO = 
                    //entity.LAST_MODIFY_TIME = DateTime.Now;
                    //entity.CREATE_USER = entity.LAST_MODIFY_USER = string.Empty;

                    @PB.LIST@Service.Save(entity);
                }
                else
                {

                    //原来的数据
                    @PB.LIST@ original = this.@PB.LIST@Service.Get(entity.OID);
                    //entity.LAST_MODIFY_TIME = DateTime.Now;
                    //entity.CREATE_TIME = original.CREATE_TIME;
                    //entity.CREATE_USER = original.CREATE_USER;
                    //修改

                    //entity.EFFECTIVE_FLAG = "1";
                    //entity.WAREHOUSE_FLAG = "1";
                    @PB.LIST@Service.Update(entity);
                }
                ret.DataId = entity.OID;
                ret.Success = true;
            }
            catch (Exception ex)
            {
                ret.Success = false;
                ret.ErrMsg = ex.Message;
            }
            return ret.ToJson();
        }
        #endregion

    }
}