using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class need
    {
        public string id { get; set; }
        public string companyId { get; set; }
        public string telephone { get; set; }
        public string suggestion { get; set; }
        public string create_time { get; set; }
    }
}