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
    }
}
