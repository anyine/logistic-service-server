using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class User
    {

        private Common commonBll = null;
        public User()
        {
            this.commonBll = new Common();
        }

        public DataTable GetUserBaseInfo(string id)
        {
            string strSql = @"select    a.id, 
                                        a.user_name,
                                        ISNULL(a.user_realname, '') as user_realname,
                                        ISNULL(a.user_level, 0) as user_level,
                                        ISNULL(a.telephone, '') as telephone,
                                        ISNULL(a.qq, '') as qq,
                                        ISNULL(a.weixin, '') as weixin,
                                        ISNULL(b.user_store, 0) as user_store,
                                        ISNULL(b.user_gains, 0) as user_gains,
                                        ISNULL(b.user_split_rate, 0) as user_split_rate,
                                        ISNULL(b.peach_1_1, 0) as peach_1_1,
                                        ISNULL(b.peach_1_2, 0) as peach_1_2,
                                        ISNULL(b.peach_1_3, 0) as peach_1_3,
                                        ISNULL(b.peach_1_4, 0) as peach_1_4,
                                        ISNULL(b.peach_1_5, 0) as peach_1_5,
                                        ISNULL(b.peach_1_6, 0) as peach_1_6,
                                        ISNULL(b.peach_1_7, 0) as peach_1_7,
                                        ISNULL(b.peach_1_8, 0) as peach_1_8,
                                        ISNULL(b.peach_1_9, 0) as peach_1_9,
                                        ISNULL(b.peach_1_10, 0) as peach_1_10,
                                        ISNULL(b.peach_2_1, 0) as peach_2_1,
                                        ISNULL(b.peach_2_2, 0) as peach_2_2,
                                        ISNULL(b.peach_2_3, 0) as peach_2_3,
                                        ISNULL(b.peach_2_4, 0) as peach_2_4,
                                        ISNULL(b.peach_2_5, 0) as peach_2_5,
                                        ISNULL(b.peach_3_1, 0) as peach_3_1
                                        from dbo.plant_user as a
                                        left join dbo.plant_user_tree as b on a.user_name=b.user_name
                                        where a.id = '{0}'";
            strSql = string.Format(strSql, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        public bool UpdateUserInfo(string userID, string fieldName, string fieldValue)
        {
            bool bRtn = false;

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldValue))
            {
                bRtn = false;
                return bRtn;
            }

            string strSql = "update dbo.hzw_user set {0} = '{1}' where user_openid = '{2}'";
            strSql = string.Format(strSql, fieldName, fieldValue, userID);
            int tag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (tag > 0)
            {
                bRtn = true;
            }

            return bRtn;
        }

        public bool CheckLogin(string i, string b)
        {
            return true;
        }
    }
}
