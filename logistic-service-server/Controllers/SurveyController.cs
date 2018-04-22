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
                    survey.telephone = dt.Rows[i]["telephone"].ToString();
                    survey.suggestion = dt.Rows[i]["suggestion"].ToString();
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
            string telephone = s.telephone;
            string suggestion = s.suggestion;
            Object data;

            try
            {
                BLL.handleSurvey survey = new BLL.handleSurvey();
                bool flag = false;
                flag = survey.AddSurvey(companyId, satisfaction, telephone, suggestion);


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
    }
}
