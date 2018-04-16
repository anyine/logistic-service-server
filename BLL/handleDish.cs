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
            string str = @"select     id,
                                      CONVERT(varchar(19), create_time, 120) as create_time
                               from dbo.ls_dish";
            str = string.Format(str);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            return dt;
        }

        //新增或修改菜品
        public bool AddDish(string dish_title, string dish_content, string dish_img, string companyId, string dish_type)
        {
            string str = @"insert into dbo.ls_dish (dish_title, dish_content, dish_img, companyId, dish_type)
                                values ('{0}', '{1}', '{2}', '{3}', '{4}')";
            str = string.Format(str, dish_title, dish_content, dish_img, companyId, dish_type);
            CommonTool.WriteLog.Write("AddDish str === " + str);
            int flag = DBHelper.SqlHelper.ExecuteSql(str);

            return flag > 0 ? true : false;
        }
    }
}
