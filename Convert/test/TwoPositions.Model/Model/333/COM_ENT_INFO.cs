using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace 3333.Model
{
    public class COM_ENT_INFO
    {

            [Display(Name = "GUID")]
            public virtual String OID { get; set; }

            [Display(Name = "单据编号")]
            public virtual String SEQ_NO { get; set; }

            [Display(Name = "修改类型")]
            public virtual String MODIFY_TYPE { get; set; }

            [Display(Name = "单据状态")]
            public virtual String CHK_STATUS { get; set; }

            [Display(Name = "回执状态")]
            public virtual String RET_CHANNEL { get; set; }

            [Display(Name = "申报方式(备案/变更)")]
            public virtual String DECLARE_MARK { get; set; }

            [Display(Name = "主管海关")]
            public virtual String CUSTOMS_CODE { get; set; }

            [Display(Name = "监管场所")]
            public virtual String AREA_CODE { get; set; }

            [Display(Name = "单据类型")]
            public virtual String DOC_TYPE { get; set; }

            [Display(Name = "待审岗位编号")]
            public virtual String POS_CODE { get; set; }

            [Display(Name = "审批时间")]
            public virtual DateTime CHK_TIME { get; set; }

            [Display(Name = "申报日期")]
            public virtual DateTime DEC_TIME { get; set; }

            [Display(Name = "申报代码")]
            public virtual String INPUTER_CODE { get; set; }

            [Display(Name = "申报名称")]
            public virtual String INPUTER_NAME { get; set; }

            [Display(Name = "申报企业代码")]
            public virtual String INPUT_COP_CODE { get; set; }

            [Display(Name = "申报企业名称")]
            public virtual String INPUT_COP_NAME { get; set; }

            [Display(Name = "创建人代码")]
            public virtual String CREATE_CODE { get; set; }

            [Display(Name = "创建人名称")]
            public virtual String CREATE_NAME { get; set; }

            [Display(Name = "创建人时间")]
            public virtual DateTime CREATE_TIME { get; set; }

            [Display(Name = "企业备案号")]
            public virtual String DOCID { get; set; }

            [Display(Name = "企业代码")]
            public virtual String TRADE_CODE { get; set; }

            [Display(Name = "企业名称")]
            public virtual String TRADE_NAME { get; set; }

            [Display(Name = "ENT_ADDR")]
            public virtual String ENT_ADDR { get; set; }

            [Display(Name = "提交类型")]
            public virtual String DECLARE_TYPE { get; set; }

            [Display(Name = "主管海关")]
            public virtual String MASTER_CUSTOMS { get; set; }

            [Display(Name = "ENT_TYPE")]
            public virtual String ENT_TYPE { get; set; }

            [Display(Name = "ENT_CLASSIFY")]
            public virtual String ENT_CLASSIFY { get; set; }

            [Display(Name = "注册资本(万元)")]
            public virtual String REG_CAPITAL { get; set; }

            [Display(Name = "LEGAL_PERSON")]
            public virtual String LEGAL_PERSON { get; set; }

            [Display(Name = "联系人")]
            public virtual String PERSON { get; set; }

            [Display(Name = "联系电话")]
            public virtual String PHONE { get; set; }

            [Display(Name = "派驻企业情况")]
            public virtual String ACCREDITED_AGENCY { get; set; }

            [Display(Name = "统一社会信用代码")]
            public virtual String CREDIT_CODE { get; set; }

            [Display(Name = "TRADE_AREA")]
            public virtual String TRADE_AREA { get; set; }

            [Display(Name = "备注")]
            public virtual String NOTE { get; set; }

            [Display(Name = "企业组织机构代码")]
            public virtual String ORG_CODE { get; set; }

            [Display(Name = "REG_CO")]
            public virtual String REG_CO { get; set; }

            [Display(Name = "EN_FULL_CO")]
            public virtual String EN_FULL_CO { get; set; }

            [Display(Name = "有效期")]
            public virtual DateTime VALID_DATE { get; set; }


        #region  自定义内容
        #endregion
    }
}