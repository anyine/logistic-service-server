using logistic_service_server.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace logistic_service_server.Controllers
{
    public class CompanyController : ApiController
    {
        #region 获取所有公司信息
        /// <summary>  
        /// 获取所有公司信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getCompanyList()
        {
            DataTable dt = new BLL.handleCompany().GetCompanyList();
            Object data;
            if (dt.Rows.Count >= 0)
            {
                List<company> list = new List<company>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    company company = new company();
                    company.id = dt.Rows[i]["id"].ToString();
                    company.companyId = dt.Rows[i]["companyId"].ToString();
                    company.culture = dt.Rows[i]["culture"].ToString();
                    company.photo = dt.Rows[i]["photo"].ToString();
                    company.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(company);
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

        #region 获取公司信息详情
        /// <summary>  
        /// 获取公司信息详情 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getCompanyDetail(string id)
        {
            DataTable dt = new BLL.handleCompany().GetCompanyDetail(id);
            Object data;
            if (dt.Rows.Count == 1)
            {
                company company = new company();
                company.id = dt.Rows[0]["id"].ToString();
                company.companyId = dt.Rows[0]["companyId"].ToString();
                company.culture = dt.Rows[0]["culture"].ToString();
                company.photo = dt.Rows[0]["photo"].ToString();
                company.create_time = dt.Rows[0]["create_time"].ToString();

                data = new
                {
                    success = true,
                    backData = company
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

        #region 更新公司文化信息
        /// <summary>  
        /// 更新公司文化信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage updateComCulture(dynamic s)
        {
            if (s == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }

            string companyId = s.companyId;
            string culture = s.culture;
            Object data;

            try
            {
                BLL.handleCompany company = new BLL.handleCompany();
                bool flag = false;
                flag = company.updateComCulture(companyId, culture);


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

        #region 更新公司相册信息
        /// <summary>  
        /// 更新公司相册信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage updateComPhoto(dynamic s)
        {
            if (s == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
                };
            }

            string companyId = s.companyId;
            string photo = s.photo;
            Object data;

            try
            {
                BLL.handleCompany company = new BLL.handleCompany();
                bool flag = false;
                flag = company.updateComPhoto(companyId, photo);


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

        #region 获取所有服务咨询和节日信息
        /// <summary>  
        /// 获取所有服务咨询和节日信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getServiceList()
        {
            DataTable dt = new BLL.handleCompany().GetServiceList();
            Object data;
            if (dt.Rows.Count >= 0)
            {
                List<service> list = new List<service>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    service service = new service();
                    service.id = dt.Rows[i]["id"].ToString();
                    service.companyId = dt.Rows[i]["companyId"].ToString();
                    service.service_type = dt.Rows[i]["service_type"].ToString();
                    service.service_title = dt.Rows[i]["service_title"].ToString();
                    service.service_content = dt.Rows[i]["service_content"].ToString();
                    service.service_cover = dt.Rows[i]["service_cover"].ToString();
                    service.create_time = dt.Rows[i]["create_time"].ToString();

                    list.Add(service);
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
    }
}
