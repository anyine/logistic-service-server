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

        //更新健康信息
        public bool EditHealth(string id, string companyId, string health_cover, string health_title, string health_desc, string health_content)
        {
            string str = @"update dbo.ls_health set companyId='{1}', health_cover='{2}', health_title='{3}', health_desc='{4}', health_content='{5}'
                                  where id='{0}'";
            str = string.Format(str, id, companyId, health_cover, health_title, health_desc, health_content);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //删除健康信息
        public bool DelHealth(string id)
        {
            string str = @"delete dbo.ls_health where id='{0}'";
            str = string.Format(str, id);
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

        //更新动态信息
        public bool EditLive(string id, string live_cover, string live_title, string live_desc, string live_content)
        {
            string str = @"update dbo.ls_live set live_cover='{1}', live_title='{2}', live_desc='{3}', live_content='{4}'
                                  where id='{0}'";
            str = string.Format(str, id, live_cover, live_title, live_desc, live_content);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //删除动态
        public bool DelLive(string id)
        {
            string str = @"delete dbo.ls_live where id='{0}'";
            str = string.Format(str, id);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }
    }
}
