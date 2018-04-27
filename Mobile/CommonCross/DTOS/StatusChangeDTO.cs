using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.DTOS
{
    public class StatusChangeDTO
    {
        public string status { get; set; }

        public StatusChangeDTO(string status)
        {
            this.status = status;
        }
    }
}
