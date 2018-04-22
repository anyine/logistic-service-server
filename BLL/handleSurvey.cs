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
                                    telephone,
                                    suggestion,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_survey
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //新增调查问卷
        public bool AddSurvey(string companyId, string satisfaction, string telephone, string suggestion)
        {
            string str = @"insert into dbo.ls_survey (companyId, satisfaction, telephone, suggestion)
                                values ('{0}', '{1}', '{2}', '{3}')";
            str = string.Format(str, companyId, satisfaction, telephone, suggestion);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

    }
}
