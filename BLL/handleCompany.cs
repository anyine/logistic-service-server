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
                               from dbo.ls_service
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

        //删除服务信息
        public bool DelService(string id)
        {
            string str = @"delete dbo.ls_service where id='{0}'";
            str = string.Format(str, id);
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

        //新增服务
        public bool AddService(string companyId, string service_type, string service_title, string service_content)
        {
            string str = @"insert into dbo.ls_service (companyId, service_type, service_title,  service_content)
                                values ('{0}', '{1}', '{2}', '{3}')";
            str = string.Format(str, companyId, service_type, service_title, service_content);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //修改服务信息
        public bool EditService(string id, string companyId, string service_type, string service_title, string service_content)
        {
            string str = @"update dbo.ls_service set companyId='{1}', service_type='{2}', service_title='{3}', service_content='{4}' where id='{0}'";
            str = string.Format(str, id, companyId, service_type, service_title, service_content);
            CommonTool.WriteLog.Write("str == " + str);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //获取服务详情
        public DataTable GetServiceDetail(string id)
        {
            string str = @"select   id,
                                    companyId,
                                    service_type,
                                    service_title,
                                    service_content,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_service
                               where id='{0}'";
            str = string.Format(str, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }
    }
}
