using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class handleCompany
    {
        //获取所有健康信息
        public DataTable GetCompanyList()
        {
            string str = @"select   id,
                                    companyId,
                                    culture,
                                    photo,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_company
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //获取所有服务信息
        public DataTable GetServiceList()
        {
            string str = @"select   id,
                                    companyId,
                                    service_type,
                                    service_title,
                                    service_content,
                                    service_cover,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_serivce
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        public DataTable GetCompanyDetail(string id)
        {
            string str = @"select   id,
                                    companyId,
                                    culture,
                                    photo,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_company
                               where id='{0}'";
            str = string.Format(str, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //更新公司文化信息                                
        public bool updateComCulture(string companyId, string culture)
        {
            string str = @"update dbo.ls_company 
                                  set culture='{1}'
                                  where companyId='{0}'";
            str = string.Format(str, companyId, culture);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //更新公司相册信息                                
        public bool updateComPhoto(string companyId, string photo)
        {
            string str = @"update dbo.ls_company 
                                  set photo='{1}'
                                  where companyId='{0}'";
            str = string.Format(str, companyId, photo);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }
    }
}
