using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class residence
    {
        public string id { get; set; }
        public string companyId { get; set; }
        public string companyName { get; set; }
        public string residence_content { get; set; }
        public string create_time { get; set; }
    }
}