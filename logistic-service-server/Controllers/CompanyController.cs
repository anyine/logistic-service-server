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

        #region 新增公司服务节日信息
        /// <summary>  
        /// 新增公司服务节日信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveAPService(dynamic s)
        {
            string id = s.id;
            string companyId = s.companyId;
            string service_type = s.service_type;
            string service_title = s.service_title;
            string service_content = s.service_content;
            Object data;

            try
            {
                BLL.handleCompany company = new BLL.handleCompany();
                bool flag = false;
                if (string.IsNullOrEmpty(id))
                {
                    flag = company.AddService(companyId, service_type, service_title, service_content);
                }
                else
                {
                    flag = company.EditService(id, companyId, service_type, service_title, service_content);
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

        #region 获取服务信息详情
        /// <summary>  
        /// 获取服务信息详情 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getServiceDetail(string id)
        {
            DataTable dt = new BLL.handleCompany().GetServiceDetail(id);
            Object data;
            if (dt.Rows.Count == 1)
            {
                service service = new service();
                service.id = dt.Rows[0]["id"].ToString();
                service.companyId = dt.Rows[0]["companyId"].ToString();
                service.service_type = dt.Rows[0]["service_type"].ToString();
                service.service_title = dt.Rows[0]["service_title"].ToString();
                service.service_content = dt.Rows[0]["service_content"].ToString();
                service.service_cover = dt.Rows[0]["service_cover"].ToString();
                service.create_time = dt.Rows[0]["create_time"].ToString();

                data = new
                {
                    success = true,
                    backData = service
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

        #region 删除服务信息
        /// <summary>  
        /// 删除服务信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage delService(dynamic d)
        {
            string id = d.id;
            object data = new object();
            try
            {
                BLL.handleCompany company = new BLL.handleCompany();
                bool flag = false;

                flag = company.DelService(id);

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
                        backMsg = "删除服务节日信息失败"

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

        #region 修改班车信息
        /// <summary>  
        /// 修改班车信息 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [SupportFilter]
        [AcceptVerbs("OPTIONS", "POST")]
        public HttpResponseMessage saveBus(dynamic r)
        {
            string bus_content = r.bus_content;
            Object data;

            try
            {
                BLL.handleCompany company = new BLL.handleCompany();
                bool flag = false;
                flag = company.saveBus(bus_content);

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
                        backMsg = "更新班车信息失败"

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

        #region 获取bus信息详情
        /// <summary>  
        /// 获取bus信息详情 
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns></returns>
        [AcceptVerbs("OPTIONS", "GET")]
        public HttpResponseMessage getBusDetail()
        {
            DataTable dt = new BLL.handleCompany().GetBusDetail();
            Object data;
            if (dt.Rows.Count == 1)
            {
                string bus_content = dt.Rows[0]["bus_content"].ToString();

                data = new
                {
                    success = true,
                    backData = bus_content
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
