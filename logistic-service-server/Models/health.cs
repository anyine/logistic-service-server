using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class health
    {
        public string id { set; get; }
        public string companyId { set; get; }
        public string health_cover { set; get; }
        public string health_title { set; get; }
        public string health_desc { set; get; }
        public string health_content { set; get; }
        public string create_time { set; get; }
    }
}