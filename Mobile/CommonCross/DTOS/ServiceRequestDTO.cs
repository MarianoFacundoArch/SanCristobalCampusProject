using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.DTOS
{
    public class ServiceRequestDTO
    {
        public ServiceRequestDTO(CoordinateDTO coordinates, string description)
        {
            this.coordinates = coordinates;
            this.description = description;
        }

        public CoordinateDTO coordinates { get; set; }
        public string description { get; set; }

    }
}
