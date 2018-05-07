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
    public class HealthController : ApiController
    {
        #region 获取所有健康信息
        /// <summary>  
        /// 获取所有健康信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getHealthList()
        {
            DataTable dt = new BLL.handleHealth().GetHealthList();
            Object data;
            if (dt.Rows.Count >= 0)
            {
                List<health> list = new List<health>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    health health = new health();
                    health.id = dt.Rows[i]["id"].ToString();
                    health.companyId = dt.Rows[i]["companyId"].ToString();
                    health.health_cover = dt.Rows[i]["health_cover"].ToString();
                    health.health_title = dt.Rows[i]["health_title"].ToString();
                    health.health_desc = dt.Rows[i]["health_desc"].ToString();
                    health.health_content = dt.Rows[i]["health_content"].ToString();
                    health.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(health);
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

        #region 新增健康信息
        /// <summary>  
        /// 新增健康信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveHealth(dynamic s)
        {
            if (s == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }

            string companyId = s.companyId;
            string health_cover = s.health_cover;
            string health_title = s.health_title;
            string health_desc = s.health_desc;
            string health_content = s.health_content;
            Object data;

            try
            {
                BLL.handleHealth health = new BLL.handleHealth();
                bool flag = false;
                flag = health.AddHealth(companyId, health_cover, health_title, health_desc, health_content);


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
