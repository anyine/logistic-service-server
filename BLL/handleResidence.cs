using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class handleResidence
    {
        //获取房屋信息详情
        public DataTable GetResidenceInfo()
        {
            string str = @"select     id,
                                      companyId,
                                      residence_content, 
                                      CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_residence";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //获取单家公司房屋信息
        public DataTable GetCompanyResidenceInfo(string companyId)
        {
            string str = @"select     id,
                                      companyId,
                                      residence_content, 
                                      CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_residence
                               where companyId='{0}'";
            str = string.Format(str, companyId);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //更新房屋信息
        public bool EditResidence(string residence_content, string id)
        {
            string str = @"update dbo.ls_residence set residence_content='{0}' where companyId='{1}'";
            str = string.Format(str, residence_content, id);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }
    }
}
