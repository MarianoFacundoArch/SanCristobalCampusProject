using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.DTOS
{
    public class PublicUserDTO
    {

        public string username { get; set; }
        public int user_id { get; set; }
        public string mail { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public Nullable<long> creation_timestamp { get; set; }
        public string mobile_phone { get; set; }
        public int is_provider { get; set; }
        public string status { get; set; }
        public string plate { get; set; }
    }
}
