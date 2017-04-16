using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate.Criterion;
using TwoPositions.Model;
using TwoPositions.IService.Service;
using Framework.Biz.Model;
using TwoPositions.Ent.Web.Controllers;
using TwoPositions.Ent.Web.Authorize;
using TwoPositions.Ent.Web.Models;

namespace TwoPositions.Ent.Web.Areas.IOTHER.Controllers
{
    public class IOTHER_002Controller : BasePageMultiController
    {
        #region 业务逻辑层TestHead1Service
        public IICONC_MAX_ENTRY_HEADService ICONC_MAX_ENTRY_HEADService { get; set; }
        public IICONC_MAX_MINService ICONC_MAX_MINService { get; set; }
        #endregion

        #region 表头列表 

        public ActionResult Index()
        {
            return View(new ICONC_MAX_ENTRY_HEAD());//new model为了获取界面表格标题
        }

        [RoleAuthorize(FuncCode = TwoPositions.Ent.Web.Services.MenuManager.IOTHER_002)]
        public string QueryHead()
        {
            object obj = Request.Form;
            //base.FilterDeptData = true;//开启机构部门数据过滤
            //base.FilterDeptDataType = FilterType.FilterDeptDataType.DeptCode;
            return base.QueryList<ICONC_MAX_ENTRY_HEAD>(false, delegate (ICONC_MAX_ENTRY_HEAD entity) { return GetHead(entity); }, null);
        }

        protected IEnumerable<NameValue> GetHead(ICONC_MAX_ENTRY_HEAD entity)
        {
              yield return new NameValue() { Name = "OID", Value = entity.OID };
              yield return new NameValue() { Name = "SEQ_NO", Value = entity.SEQ_NO };
              yield return new NameValue() { Name = "MODIFY_TYPE", Value = entity.MODIFY_TYPE };
              yield return new NameValue() { Name = "CHK_STATUS", Value = entity.CHK_STATUS };
              yield return new NameValue() { Name = "RET_CHANNEL", Value = entity.RET_CHANNEL };
              yield return new NameValue() { Name = "DECLARE_MARK", Value = entity.DECLARE_MARK };
              yield return new NameValue() { Name = "CUSTOMS_CODE", Value = entity.CUSTOMS_CODE };
              yield return new NameValue() { Name = "AREA_CODE", Value = entity.AREA_CODE };
              yield return new NameValue() { Name = "DOC_TYPE", Value = entity.DOC_TYPE };
              yield return new NameValue() { Name = "POS_CODE", Value = entity.POS_CODE };
              yield return new NameValue() { Name = "CHK_TIME", Value = entity.CHK_TIME.ToString()  };
              yield return new NameValue() { Name = "DEC_TIME", Value = entity.DEC_TIME.ToString()  };
              yield return new NameValue() { Name = "INPUTER_CODE", Value = entity.INPUTER_CODE };
              yield return new NameValue() { Name = "INPUTER_NAME", Value = entity.INPUTER_NAME };
              yield return new NameValue() { Name = "INPUT_COP_CODE", Value = entity.INPUT_COP_CODE };
              yield return new NameValue() { Name = "INPUT_COP_NAME", Value = entity.INPUT_COP_NAME };
              yield return new NameValue() { Name = "CREATE_CODE", Value = entity.CREATE_CODE };
              yield return new NameValue() { Name = "CREATE_NAME", Value = entity.CREATE_NAME };
              yield return new NameValue() { Name = "CREATE_TIME", Value = entity.CREATE_TIME.ToString()  };
              yield return new NameValue() { Name = "LIST_NO", Value = entity.LIST_NO };
              yield return new NameValue() { Name = "CONC_NO", Value = entity.CONC_NO };
              yield return new NameValue() { Name = "CORRT_DOC_TYPE", Value = entity.CORRT_DOC_TYPE };
              yield return new NameValue() { Name = "CORRT_DOC_CODE", Value = entity.CORRT_DOC_CODE };
              yield return new NameValue() { Name = "TRADE_NAME", Value = entity.TRADE_NAME };
              yield return new NameValue() { Name = "TRADE_COUNTRY", Value = entity.TRADE_COUNTRY };
              yield return new NameValue() { Name = "I_E_PORT", Value = entity.I_E_PORT };
              yield return new NameValue() { Name = "WRAP_TYPE", Value = entity.WRAP_TYPE };
              yield return new NameValue() { Name = "TRAF_MODE", Value = entity.TRAF_MODE };
              yield return new NameValue() { Name = "GROSS_WT", Value = entity.GROSS_WT.ToString()  };
              yield return new NameValue() { Name = "NET_WT", Value = entity.NET_WT.ToString()  };
              yield return new NameValue() { Name = "PACK_NO", Value = entity.PACK_NO.ToString()  };
              yield return new NameValue() { Name = "IO_WH_DATE", Value = entity.IO_WH_DATE.ToString()  };
              yield return new NameValue() { Name = "BILL_DATE", Value = entity.BILL_DATE.ToString()  };
              yield return new NameValue() { Name = "TAX_MONEY", Value = entity.TAX_MONEY.ToString()  };
              yield return new NameValue() { Name = "NOTE", Value = entity.NOTE };
              yield return new NameValue() { Name = "EMS_NO", Value = entity.EMS_NO };
              yield return new NameValue() { Name = "TRANS_MODE", Value = entity.TRANS_MODE };
              yield return new NameValue() { Name = "BACK_FLAG", Value = entity.BACK_FLAG };

        }

        public string DeleteHead(string p)
        {
            ExecResult ret = new ExecResult();
            try
            {
                this.ICONC_MAX_ENTRY_HEADService.Delete(p);
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
            ICONC_MAX_ENTRY_HEAD currModel = null;
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
                currModel = ICONC_MAX_ENTRY_HEADService.Get(p);
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
                currModel = new ICONC_MAX_ENTRY_HEAD();
                //新增时,对象初始值
                //currModel.RegDate = currModel.TestDate = DateTime.Now;
            }
            return View(currModel);
        }

        public string SaveHead(string p, ICONC_MAX_ENTRY_HEAD entity)
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
                var existsObj = ICONC_MAX_ENTRY_HEADService.Get(entity.OID);
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
                    //entity.SEQ_NO = this.ICONC_MAX_ENTRY_HEADService.CreateSeqNo();
                    //entity.LAST_MODIFY_TIME = DateTime.Now;
                    //entity.CREATE_USER = entity.LAST_MODIFY_USER = string.Empty;
                    //
                    ICONC_MAX_ENTRY_HEADService.Save(entity);
                }
                else
                {

                    //原来的数据
                    ICONC_MAX_ENTRY_HEAD original = this.ICONC_MAX_ENTRY_HEADService.Get(entity.OID);
                    //entity.LAST_MODIFY_TIME = DateTime.Now;
                    //entity.CREATE_TIME = original.CREATE_TIME;
                    //entity.CREATE_USER = original.CREATE_USER;
                    //修改
                    ICONC_MAX_ENTRY_HEADService.Update(original);
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
        //[RoleAuthorize(FuncCode = TwoPositions.Ent.Web.Services.MenuManager.MENU_TEST2)]
        public ActionResult IndexList(string p)
        {
            ViewBag.pid = p;
            ViewBag.OperationType = GetPageStringParams("details");
            return View(new ICONC_MAX_MIN());//new model为了获取界面表格标题
        }

        //[RoleAuthorize(FuncCode = TwoPositions.Ent.Web.Services.MenuManager.IOTHER_002)]
        public string QueryList(string p)
        {
            object obj = Request.Form;
            List<ICriterion> conds = new List<ICriterion>();
            conds.Add(Restrictions.Eq("SEQ_NO", p));
            return base.QueryList<ICONC_MAX_MIN>(false, delegate (ICONC_MAX_MIN entity) { return GetList(entity); }, conds);
        }

        protected IEnumerable<NameValue> GetList(ICONC_MAX_MIN entity)
        {
              yield return new NameValue() { Name = "OID", Value = entity.OID };
              yield return new NameValue() { Name = "SEQ_NO", Value = entity.SEQ_NO };
              yield return new NameValue() { Name = "MODIFY_TYPE", Value = entity.MODIFY_TYPE };
              yield return new NameValue() { Name = "MODIFY_NUMBER", Value = entity.MODIFY_NUMBER.ToString()  };
              yield return new NameValue() { Name = "MODIFY_TIME", Value = entity.MODIFY_TIME.ToString()  };
              yield return new NameValue() { Name = "LIST_NO", Value = entity.LIST_NO };
              yield return new NameValue() { Name = "CONC_NO", Value = entity.CONC_NO };
              yield return new NameValue() { Name = "CONC_BATCH_NO", Value = entity.CONC_BATCH_NO };

        }

        public string DeleteList(string p)
        {
            ExecResult ret = new ExecResult();
            try
            {
                this.ICONC_MAX_MINService.Delete(p);
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
            ICONC_MAX_MIN currModel = null;
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
                currModel = ICONC_MAX_MINService.Get(p);
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
                currModel = new ICONC_MAX_MIN();
                currModel.SEQ_NO = seq_no;
                //新增时,对象初始值
                //currModel.RegDate = currModel.TestDate = DateTime.Now;
            }
            return View(currModel);
        }


        public string SaveList(string p, ICONC_MAX_MIN entity)
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
                var existsObj = ICONC_MAX_MINService.Get(entity.OID);
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

                    ICONC_MAX_MINService.Save(entity);
                }
                else
                {

                    //原来的数据
                    ICONC_MAX_MIN original = this.ICONC_MAX_MINService.Get(entity.OID);
                    //entity.LAST_MODIFY_TIME = DateTime.Now;
                    //entity.CREATE_TIME = original.CREATE_TIME;
                    //entity.CREATE_USER = original.CREATE_USER;
                    //修改

                    //entity.EFFECTIVE_FLAG = "1";
                    //entity.WAREHOUSE_FLAG = "1";
                    ICONC_MAX_MINService.Update(entity);
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