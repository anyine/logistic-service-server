using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class handleHealth
    {
        //获取所有健康信息
        public DataTable GetHealthList()
        {
            string str = @"select   id,
                                    companyId,
                                    health_cover,
                                    health_title,
                                    health_desc,
                                    health_content,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_health
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        public DataTable GetHealthDetail(string id)
        {
            string str = @"select   id,
                                    companyId,
                                    health_cover,
                                    health_title,
                                    health_desc,
                                    health_content,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_health
                               where id='{0}'";
            str = string.Format(str, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //新增健康信息                                
        public bool AddHealth(string companyId, string health_cover, string health_title, string health_desc, string health_content)
        {
            string str = @"insert into dbo.ls_health (companyId, health_cover, health_title, health_desc, health_content)
                                values ('{0}', '{1}', '{2}', '{3}', '{4}')";
            str = string.Format(str, companyId, health_cover, health_title, health_desc, health_content);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //获取所有动态信息
        public DataTable GetLiveList()
        {
            string str = @"select   id,
                                    companyId,
                                    live_cover,
                                    live_title,
                                    live_desc,
                                    live_content,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_live
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        public DataTable GetLiveDetail(string id)
        {
            string str = @"select   id,
                                    companyId,
                                    live_cover,
                                    live_title,
                                    live_desc,
                                    live_content,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_live
                               where id='{0}'";
            str = string.Format(str, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //新增动态信息                                
        public bool AddLive(string live_cover, string live_title, string live_desc, string live_content)
        {
            string str = @"insert into dbo.ls_live (live_cover, live_title, live_desc, live_content)
                                values ('{0}', '{1}', '{2}', '{3}')";
            str = string.Format(str, live_cover, live_title, live_desc, live_content);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }
    }
}
