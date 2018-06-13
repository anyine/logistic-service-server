using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class handleSurvey
    {
        //获取所有调查问卷
        public DataTable GetSurveyList()
        {
            string str = @"select   id,
                                    companyId,
                                    satisfaction,
                                    dish,
                                    clean,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_survey
                               where datediff(month, create_time, getdate())=0
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //新增调查问卷
        public bool AddSurvey(string companyId, string satisfaction, string dish, string clean)
        {
            string str = @"insert into dbo.ls_survey (companyId, satisfaction, dish, clean)
                                values ('{0}', '{1}', '{2}', '{3}')";
            str = string.Format(str, companyId, satisfaction, dish, clean);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //新增需求
        public bool AddNeed(string companyId, string telephone, string suggestion)
        {
            string str = @"insert into dbo.ls_need (companyId, telephone, suggestion)
                                values ('{0}', '{1}', '{2}')";
            str = string.Format(str, companyId, telephone, suggestion);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //获取所有球场信息
        public DataTable GetGymList()
        {
            string str = @"select   id,
                                    ball_type,
                                    ball_content,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_ball
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //新增运动
        public bool AddBall(string ball_type, string ball_content)
        {
            string str = @"insert into dbo.ls_ball (ball_type,  ball_content)
                                values ('{0}', '{1}')";
            str = string.Format(str, ball_type, ball_content);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //修改运动信息
        public bool EditBall(string id, string ball_type, string ball_content)
        {
            string str = @"update dbo.ls_ball set ball_type='{1}', ball_content='{2}' where id='{0}'";
            str = string.Format(str, id, ball_type, ball_content);
            CommonTool.WriteLog.Write("str == " + str);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //获取运动详情
        public DataTable GetBallDetail(string id)
        {
            string str = @"select   id,
                                    companyId,
                                    service_type,
                                    service_title,
                                    service_cover,
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
