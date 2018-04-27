using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.DTOS
{
    public class UserTokenDTO
    {
        public int user_id { get; set; }
        public string token { get; set; }
        public Nullable<long> expire_timestamp { get; set; }

    }
}
