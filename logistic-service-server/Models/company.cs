using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class company
    {
        public string id { get; set; }
        public string companyId { get; set; }
        public string culture { get; set; }
        public string photo { get; set; }
        public string create_time { get; set; }
    }
}