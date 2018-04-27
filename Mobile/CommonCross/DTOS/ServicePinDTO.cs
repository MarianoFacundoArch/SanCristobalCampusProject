using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.DTOS
{
    public class ServicePinDTO : MapPinDTO
    {
        public int service_id { get; set; }

        public ServicePinDTO(string name, CoordinateDTO coordinates, string plate, string otherDetails, long lastSeenTimestamp, int pinType, int userId, int serviceId) : base(name, coordinates, plate, otherDetails, lastSeenTimestamp, pinType, userId)
        {
            service_id = serviceId;
        }
    }
}
