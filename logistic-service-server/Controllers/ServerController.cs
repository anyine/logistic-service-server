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
            string strSql = @"select    id
                                        from dbo.ls_user
                                        where user_name = '{0}' and user_pwd = '{1}'";
            strSql = string.Format(strSql, userName, userPwd);
            DataTable dt_user = DBHelper.SqlHelper.GetDataTable(strSql);
            var data = new object { };
            if (dt_user.Rows.Count == 1)
            {
                FormsAuthenticationTicket token = new FormsAuthenticationTicket(0, userName, DateTime.Now,
                            DateTime.Now.AddHours(5), true, string.Format("{0}&{1}", userName, userPwd),
                            FormsAuthentication.FormsCookiePath);
                //返回登录结果、用户信息、用户验证票据信息
                var Token = FormsAuthentication.Encrypt(token);
                //将身份信息保存在数据库中，验证当前请求是否是有效请求
                string str_token = @"insert into dbo.ls_token (userId, token, expireDate) values ('{0}', '{1}', '{2}')";
                str_token = string.Format(str_token, dt_user.Rows[0]["id"], Token, DateTime.Now.AddHours(3));
                DBHelper.SqlHelper.ExecuteSql(str_token);

                data = new
                {
                    success = true,
                    token = Token,
                    userId = dt_user.Rows[0]["id"]
                };
            }
            else
            {
                data = new
                {
                    success = false,
                    backMsg = "身份验证失败，请重试！"
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
                string str_clear = @"delete dbo.ls_token where userId='{0}'";
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
                    dish.dish_week = dt.Rows[i]["dish_week"].ToString();
                    dish.is_online = Convert.ToInt32(dt.Rows[i]["is_online"]);
                    dish.update_time = dt.Rows[i]["update_time"].ToString();
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

        #region 获取当日全天菜品信息
        /// <summary>  
        /// 获取所有菜单 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getDailyDish(string id)
        {
            DataTable dt = new BLL.handleDish().GetDailyDish(id);
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
                    dish.update_time = dt.Rows[i]["update_time"].ToString();
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

        #region 获取菜单详情
        /// <summary>  
        /// 获取菜单详情 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getDishDetail(string id)
        {
            DataTable dt = new BLL.handleDish().GetDishDetail(id);
            Object data;
            if (dt.Rows.Count == 1)
            {
                dish dish = new dish();
                dish.id = dt.Rows[0]["id"].ToString();
                dish.dish_title = dt.Rows[0]["dish_title"].ToString();
                dish.dish_content = dt.Rows[0]["dish_content"].ToString();
                dish.dish_img = dt.Rows[0]["dish_img"].ToString();
                dish.companyId = dt.Rows[0]["companyId"].ToString();
                dish.dish_type = dt.Rows[0]["dish_type"].ToString();
                dish.is_online = Convert.ToInt32(dt.Rows[0]["is_online"]);
                dish.update_time = dt.Rows[0]["update_time"].ToString();
                dish.create_time = dt.Rows[0]["create_time"].ToString();


                data = new
                {
                    success = true,
                    backData = dish
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
                    flag = dish.EditDish(id, dish_title, dish_content, dish_img, companyId, dish_type);
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
            string dish_week = d.dish_week;
            Object data;

            try
            {
                BLL.handleDish dish = new BLL.handleDish();
                bool flag = false;

                flag = dish.OnlineStateChange(id, is_online, dish_week);

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

        #region 删除菜品
        /// <summary>  
        /// 删除菜品 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage delDish(dynamic d)
        {
            string id = d.id;
            object data = new object();
            try
            {
                BLL.handleDish dish = new BLL.handleDish();
                bool flag = false;

                flag = dish.DelDish(id);

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
                        backMsg = "删除菜品失败"

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

        #region 修改房屋信息
        /// <summary>  
        /// 修改房屋信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveResidence(dynamic r)
        {
            string companyId = r.companyId;
            string residence_content = r.residence_content;
            Object data;

            try
            {
                BLL.handleResidence residence = new BLL.handleResidence();
                bool flag = false;
                flag = residence.EditResidence(residence_content, companyId);

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
                        backMsg = "更新房屋信息失败"

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

        #region 获取房屋信息
        /// <summary>  
        /// 获取房屋信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getResidenceInfo()
        {
            DataTable dt = new BLL.handleResidence().GetResidenceInfo();
            Object data;
            if (dt.Rows.Count > 0)
            {
                List<residence> list = new List<residence>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    residence residence = new residence();
                    residence.id = dt.Rows[i]["id"].ToString();
                    residence.companyId = dt.Rows[i]["companyId"].ToString();
                    residence.residence_content = dt.Rows[i]["residence_content"].ToString();
                    residence.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(residence);
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

        #region 获取单家公司房屋信息
        /// <summary>  
        /// 获取单家公司房屋信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getCompanyResidenceInfo(string id)
        {
            DataTable dt = new BLL.handleResidence().GetCompanyResidenceInfo(id);
            Object data;
            if (dt.Rows.Count == 1)
            {
                residence residence = new residence();
                residence.id = dt.Rows[0]["id"].ToString();
                residence.companyId = dt.Rows[0]["companyId"].ToString();
                residence.residence_content = dt.Rows[0]["residence_content"].ToString();
                residence.create_time = dt.Rows[0]["create_time"].ToString();

                data = new
                {
                    success = true,
                    backData = residence
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
