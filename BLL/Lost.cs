using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Lost
    {
        //获取失物招领详情
        public DataTable GetLostInfo()
        {
            string str = @"select     id,
                                      companyId,
                                      lost_content, 
                                      CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_lost";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //获取单家公司失物招领信息
        public DataTable GetCompanyLostInfo(string companyId)
        {
            string str = @"select     id,
                                      companyId,
                                      lost_content, 
                                      CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_lost
                               where companyId='{0}'";
            str = string.Format(str, companyId);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //更新失物招领信息
        public bool EditLost(string lost_content, string id)
        {
            string str = @"update dbo.ls_lost set lost_content='{0}' where companyId='{1}'";
            str = string.Format(str, lost_content, id);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }
    }
}
