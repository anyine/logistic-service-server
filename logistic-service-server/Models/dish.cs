using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace logistic_service_server.Models
{
    public class dish
    {
        public string id { set; get; }
        public string dish_title { set; get; }
        public string dish_content { set; get; }
        public string dish_img { set; get; }
        public string companyId { set; get; }
        public string dish_type { set; get; }
        public int is_online { set; get; }
        public string update_time { set; get; }
        public string create_time { set; get; }
    }
}