using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class Lost
    {
        public string id { set; get; }
        public string companyId { set; get; }
        public string lost_title { set; get; }
        public string lost_content { set; get; }
        public string publish_time { set; get; }
        public string create_time { set; get; }
    }
}