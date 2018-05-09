using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class service
    {
        public string id { set; get; }
        public string companyId { set; get; }
        public string service_type { set; get; }
        public string service_title { set; get; }
        public string service_content { set; get; }
        public string service_cover { set; get; }
        public string create_time { set; get; }
    }
}