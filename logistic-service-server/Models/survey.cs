﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class survey
    {
        public string id { get; set; }
        public string companyId { get; set; }
        public string satisfaction { get; set; }
        public string dish { get; set; }
        public string clean { get; set; }
        public string create_time { get; set; }
    }
}