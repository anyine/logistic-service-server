using logistic_service_server.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using treePlanting.Core;

namespace logistic_service_server.Controllers
{
    public class SurveyController : ApiController
    {
        #region 获取所有调查问卷
        /// <summary>  
        /// 获取所有调查问卷 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getSurveyList()
        {
            DataTable dt = new BLL.handleSurvey().GetSurveyList();
            Object data;
            if (dt.Rows.Count >= 0)
            {
                List<survey> list = new List<survey>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    survey survey = new survey();
                    survey.id = dt.Rows[i]["id"].ToString();
                    survey.companyId = dt.Rows[i]["companyId"].ToString();
                    survey.satisfaction = dt.Rows[i]["satisfaction"].ToString();
                    survey.dish = dt.Rows[i]["dish"].ToString();
                    survey.clean = dt.Rows[i]["clean"].ToString();
                    survey.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(survey);
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

        #region 新增调查信息
        /// <summary>  
        /// 新增调查信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveSurvey(dynamic s)
        {
            if (s == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }

            string companyId = s.companyId;
            string satisfaction = s.satisfaction;
            string dish = s.dish;
            string clean = s.clean;
            Object data;

            try
            {
                BLL.handleSurvey survey = new BLL.handleSurvey();
                bool flag = false;
                flag = survey.AddSurvey(companyId, satisfaction, dish, clean);


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
                        backMsg = "保存信息失败"

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

        #region 新增需求信息
        /// <summary>  
        /// 新增需求信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveNeed(dynamic s)
        {
            if (s == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }

            string companyId = s.companyId;
            string telephone = s.telephone;
            string suggestion = s.suggestion;
            Object data;

            try
            {
                BLL.handleSurvey survey = new BLL.handleSurvey();
                bool flag = false;
                flag = survey.AddNeed(companyId, telephone, suggestion);


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
                        backMsg = "保存信息失败"

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

        #region 获取所有需求
        /// <summary>  
        /// 获取所有需求 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getNeedList()
        {
            DataTable dt = new BLL.handleSurvey().GetNeedList();
            Object data;
            if (dt.Rows.Count >= 0)
            {
                List<need> list = new List<need>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    need need = new need();
                    need.id = dt.Rows[i]["id"].ToString();
                    need.companyId = dt.Rows[i]["companyId"].ToString();
                    need.telephone = dt.Rows[i]["telephone"].ToString();
                    need.suggestion = dt.Rows[i]["suggestion"].ToString();
                    need.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(need);
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

        #region 获取所有球场信息
        /// <summary>  
        /// 获取所有球场信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getGymList()
        {
            DataTable dt = new BLL.handleSurvey().GetGymList();
            Object data;
            if (dt.Rows.Count >= 0)
            {
                List<ball> list = new List<ball>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ball ball = new ball();
                    ball.id = dt.Rows[i]["id"].ToString();
                    ball.ball_type = dt.Rows[i]["ball_type"].ToString();
                    ball.ball_content = dt.Rows[i]["ball_content"].ToString();
                    ball.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(ball);
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

        #region 获取球场信息详情
        /// <summary>  
        /// 获取获取球场信息详情信息详情 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getGymDetail(string id)
        {
            DataTable dt = new BLL.handleSurvey().GetBallDetail(id);
            Object data;
            if (dt.Rows.Count == 1)
            {
                ball ball = new ball();
                ball.id = dt.Rows[0]["id"].ToString();
                ball.ball_type = dt.Rows[0]["ball_type"].ToString();
                ball.ball_content = dt.Rows[0]["ball_content"].ToString();
                ball.create_time = dt.Rows[0]["create_time"].ToString();

                data = new
                {
                    success = true,
                    backData = ball
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

        #region 获取球场信息详情通过类型
        /// <summary>  
        /// 获取球场信息详情通过类型 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getGymDetailByType(string type)
        {
            DataTable dt = new BLL.handleSurvey().GetBallDetailByType(type);
            Object data;
            if (dt.Rows.Count == 1)
            {
                ball ball = new ball();
                ball.id = dt.Rows[0]["id"].ToString();
                ball.ball_type = dt.Rows[0]["ball_type"].ToString();
                ball.ball_content = dt.Rows[0]["ball_content"].ToString();
                ball.create_time = dt.Rows[0]["create_time"].ToString();

                data = new
                {
                    success = true,
                    backData = ball
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

        #region 新增运动
        /// <summary>  
        /// 新增运动 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveAPBall(dynamic s)
        {
            string id = s.id;
            string ball_type = s.ball_type;
            string ball_content = s.ball_content;
            Object data;

            try
            {
                BLL.handleSurvey survey = new BLL.handleSurvey();
                bool flag = false;
                if (string.IsNullOrEmpty(id))
                {
                    flag = survey.AddBall(ball_type, ball_content);
                }
                else
                {
                    flag = survey.EditBall(id, ball_type, ball_content);
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
                        backMsg = "更新信息失败"

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

        #region 删除球类
        /// <summary>  
        /// 删除球类 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage delBall(dynamic d)
        {
            string id = d.id;
            object data = new object();
            try
            {
                BLL.handleSurvey survey = new BLL.handleSurvey();
                bool flag = false;

                flag = survey.DelBall(id);

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
                        backMsg = "删除球类失败"

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
    }
}
