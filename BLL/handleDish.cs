using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class handleDish
    {
        //获取所有菜单
        public DataTable GetDishList()
        {
            string str = @"select   id,
                                    dish_title,
                                    dish_content,
                                    dish_img,
                                    companyId,
                                    dish_type,
                                    dish_week,
                                    ISNULL(is_online, 0) as is_online,
                                    CONVERT(varchar(19), update_time, 120) as update_time,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_dish
                               order by create_time desc";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //获取菜单详情
        public DataTable GetDishDetail(string id)
        {
            string str = @"select   id,
                                    dish_title,
                                    dish_content,
                                    dish_img,
                                    companyId,
                                    dish_type,
                                    dish_week,
                                    ISNULL(is_online, 0) as is_online,
                                    CONVERT(varchar(19), update_time, 120) as update_time,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_dish
                               where id='{0}'";
            str = string.Format(str, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //获取当日全天菜品信息
        public DataTable GetDailyDish(string companyId)
        {
            string str = @"select   id,
                                    dish_title,
                                    dish_content,
                                    dish_img,
                                    companyId,
                                    dish_type,
                                    dish_week,
                                    ISNULL(is_online, 0) as is_online,
                                    CONVERT(varchar(19), update_time, 120) as update_time,
                                    CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_dish
                               where companyId='{0}' and is_online=1 and DateDiff(dd, update_time, getdate())=0";
            str = string.Format(str, companyId);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //新增菜品
        public bool AddDish(string dish_title, string dish_content, string dish_img, string companyId, string dish_type)
        {
            string str = @"insert into dbo.ls_dish (dish_title, dish_content, dish_img, companyId, dish_type)
                                values ('{0}', '{1}', '{2}', '{3}', '{4}')";
            str = string.Format(str, dish_title, dish_content, dish_img, companyId, dish_type);
            CommonTool.WriteLog.Write("AddDish str === " + str);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //修改菜品信息
        public bool EditDish(string id, string dish_title, string dish_content, string dish_img, string companyId, string dish_type)
        {
            string str = @"update dbo.ls_dish set dish_title='{1}', dish_content='{2}', dish_img='{3}', companyId='{4}', dish_type='{5}' where id='{0}'";
            str = string.Format(str, id, dish_title, dish_content, dish_img, companyId, dish_type);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //更新菜品推荐状态
        public bool OnlineStateChange(string id, int onlineState, string dish_week)
        {
            DateTime dt = DateTime.Now;

            string str = @"update dbo.ls_dish set is_online={1}, dish_week='{2}', update_time='{3}' where id='{0}'";
            str = string.Format(str, id, onlineState, dish_week, dt);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }

        //删除菜品
        public bool DelDish(string id)
        {
            string str = @"delete dbo.ls_dish where id='{0}'";
            str = string.Format(str, id);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }
    }
}
