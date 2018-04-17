using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;
using logistic_service_server.Models;
using treePlanting.Core;

namespace logistic_service_server.Controllers
{
    public class SessionRouteHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionRouteHandler(RouteData routeData)
            : base(routeData)
        {

        }
    }

    public class SessionControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SessionRouteHandler(requestContext.RouteData);
        }
    }

    public class ServerController : ApiController
    {
        #region 用户登录授权
        /// <summary>  
        /// 用户登录授权  
        /// </summary>  
        /// <param name="user">用户实体</param> 
        /// <returns></returns>  
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage login(dynamic user)
        {
            if (user == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }
            string userName = Convert.ToString(user.user_name);
            string userPwd = Convert.ToString(user.user_pwd);
            string strSql = @"select u.id, 
                                     pid,
                                     ISNULL(p_name, '') as p_name, 
                                     ISNULL(u.user_name, '') as user_name,
                                     ISNULL(user_pwd, '') as user_pwd,
                                     ISNULL(user_secondpwd, '') as user_secondpwd,
                                     ISNULL(user_realname, '') as user_realname,
                                     ISNULL(user_sex, '男') as user_sex,
                                     ISNULL(user_telephone, '') as user_telephone,
                                     ISNULL(user_qq, '') as user_qq,
                                     ISNULL(user_weixin, '') as user_weixin,
                                     ISNULL(user_alipay, '') as user_alipay,
                                     ISNULL(i.user_points, 0) as user_points
                              from dbo.plant_user as u
                              left join dbo.plant_user_income as i
                              on u.user_name = i.user_name
                              where u.user_name = '{0}' and user_pwd = '{1}' and u.is_remove = 0";
            strSql = string.Format(strSql, userName, userPwd);
            DataTable dt_user = DBHelper.SqlHelper.GetDataTable(strSql);
            var data = new object { };
            if (dt_user.Rows.Count == 1)
            {
                FormsAuthenticationTicket token = new FormsAuthenticationTicket(0, userName, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", userName, userPwd),
                            FormsAuthentication.FormsCookiePath);
                //返回登录结果、用户信息、用户验证票据信息
                var Token = FormsAuthentication.Encrypt(token);
                //将身份信息保存在数据库中，验证当前请求是否是有效请求
                string str_token = @"insert into dbo.plant_token (userId, token, ExpireDate) values ('{0}', '{1}', '{2}')";
                str_token = string.Format(str_token, dt_user.Rows[0]["id"], Token, DateTime.Now.AddHours(1));
                DBHelper.SqlHelper.ExecuteSql(str_token);

                data = new
                {
                    success = true,
                    backData = CommonTool.JsonHelper.DataTableToJSON(dt_user),
                    token = Token
                };
            }
            else
            {
                data = new
                {
                    success = false,
                    backMsg = "auth check error"
                };
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion

        #region 用户退出登录，清空Token
        /// <summary>  
        /// 用户退出登录，清空Token  
        /// </summary>  
        /// <param name="id">用户ID</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage LoginOut(dynamic id)
        {
            if (id == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }
            string userId = Convert.ToString(id.userId);
            int flag = 0;
            try
            {
                //清空数据库该用户票据数据  
                string str_clear = @"delete dbo.plant_token where userId='{0}'";
                str_clear = string.Format(str_clear, userId);
                flag = DBHelper.SqlHelper.ExecuteSql(str_clear);
            }
            catch (Exception ex) { }
            //返回信息
            var data = new object { };
            if (flag > 0)
            {
                data = new
                {
                    success = true
                };
            }
            else
            {
                data = new
                {
                    success = false,
                    backMsg = "安全退出失败，请重试！"
                };
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion

        #region 获取所有菜单
        /// <summary>  
        /// 获取所有菜单 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getDishList()
        {
            DataTable dt = new BLL.handleDish().GetDishList();
            Object data;
            if (dt.Rows.Count >= 0)
            {
                List<dish> list = new List<dish>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dish dish = new dish();
                    dish.id = dt.Rows[i]["id"].ToString();
                    dish.dish_title = dt.Rows[i]["dish_title"].ToString();
                    dish.dish_content = dt.Rows[i]["dish_content"].ToString();
                    dish.dish_img = dt.Rows[i]["dish_img"].ToString();
                    dish.companyId = dt.Rows[i]["companyId"].ToString();
                    dish.dish_type = dt.Rows[i]["dish_type"].ToString();
                    dish.is_online = Convert.ToInt32(dt.Rows[i]["is_online"]);
                    dish.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(dish);
                }


                data = new
                {
                    success = true,
                    backData = list
                };
            }
            else
            {
                data = new
                {
                    success = false,
                    backMsg = "数据异常"
                };
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion

        #region 新增或修改菜品信息
        /// <summary>  
        /// 新增或修改菜品信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveAPDish(dynamic d)
        {
            string id = d.id;
            string dish_title = d.dish_title;
            string dish_content = d.dish_content;
            string dish_img = d.dish_img;
            string companyId = d.companyId;
            string dish_type = d.dish_type;
            Object data;

            try
            {
                BLL.handleDish dish = new BLL.handleDish();
                bool flag = false;
                if (string.IsNullOrEmpty(id))
                {
                    flag = dish.AddDish(dish_title, dish_content, dish_img, companyId, dish_type);
                }
                else
                {

                }


                if (flag)
                {
                    data = new
                    {
                        success = true
                    };
                }
                else
                {
                    data = new
                    {
                        success = false,
                        backMsg = "更新菜品信息失败"

                    };
                }
            }
            catch (Exception ex)
            {
                data = new
                {
                    success = false,
                    backMsg = "服务异常"

                };
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion

        #region 修改菜品信息
        /// <summary>  
        /// 新增或修改菜品信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage onlineStateChange(dynamic d)
        {
            string id = d.id;
            int is_online = d.is_online;
            Object data;

            try
            {
                BLL.handleDish dish = new BLL.handleDish();
                bool flag = false;

                flag = dish.OnlineStateChange(id, is_online);

                if (flag)
                {
                    data = new
                    {
                        success = true
                    };
                }
                else
                {
                    data = new
                    {
                        success = false,
                        backMsg = "更新菜品信息失败"

                    };
                }
            }
            catch (Exception ex)
            {
                data = new
                {
                    success = false,
                    backMsg = "服务异常"

                };
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion

        #region 修改失物招领信息
        /// <summary>  
        /// 修改失物招领信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveLost(dynamic l)
        {
            string companyId = l.companyId;
            string lost_content = l.lost_content;
            Object data;

            try
            {
                BLL.Lost lost = new BLL.Lost();
                bool flag = false;
                flag = lost.EditLost(lost_content, companyId);

                if (flag)
                {
                    data = new
                    {
                        success = true
                    };
                }
                else
                {
                    data = new
                    {
                        success = false,
                        backMsg = "更新失物招领信息失败"

                    };
                }
            }
            catch (Exception ex)
            {
                data = new
                {
                    success = false,
                    backMsg = "服务异常"

                };
            }
            string str = @"select * from dbo.ls_lost";
            DataTable dt = DBHelper.SqlHelper.GetDataTable(str);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion

        #region 获取失物招领信息
        /// <summary>  
        /// 获取失物招领信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getLostInfo()
        {
            DataTable dt = new BLL.Lost().GetLostInfo();
            Object data;
            if (dt.Rows.Count > 0)
            {
                List<lost> list = new List<lost>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lost lost = new lost();
                    lost.id = dt.Rows[i]["id"].ToString();
                    lost.companyId = dt.Rows[i]["companyId"].ToString();
                    lost.lost_content = dt.Rows[i]["lost_content"].ToString();
                    lost.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(lost);
                }


                data = new
                {
                    success = true,
                    backData = list
                };
            }
            else
            {
                data = new
                {
                    success = false,
                    backMsg = "数据异常"
                };
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion

        #region 获取单家公司失物招领信息
        /// <summary>  
        /// 获取失物招领信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getCompanyLostInfo(string companyId)
        {
            DataTable dt = new BLL.Lost().GetCompanyLostInfo(companyId);
            Object data;
            if (dt.Rows.Count == 1)
            {
                lost lost = new lost();
                lost.id = dt.Rows[0]["id"].ToString();
                lost.companyId = dt.Rows[0]["companyId"].ToString();
                lost.lost_content = dt.Rows[0]["lost_content"].ToString();
                lost.create_time = dt.Rows[0]["create_time"].ToString();

                data = new
                {
                    success = true,
                    backData = lost
                };
            }
            else
            {
                data = new
                {
                    success = false,
                    backMsg = "数据异常"
                };
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            return new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        #endregion
    }
}
