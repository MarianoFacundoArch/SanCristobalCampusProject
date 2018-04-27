using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.DTOS
{
    public class MapPinDTO
    {
        public MapPinDTO(string name, CoordinateDTO coordinates, string plate, string otherDetails, long lastSeenTimestamp, int pinType, int userId)
        {
            this.name = name;
            this.coordinates = coordinates;
            this.plate = plate;
            this.otherDetails = otherDetails;
            last_seen_timestamp = lastSeenTimestamp;
            pin_type = pinType;
            user_id = userId;
        }

        public string name { get; set; }
        public CoordinateDTO coordinates { get; set; }
        public string plate { get; set; }
        public string otherDetails { get; set; }
        public long last_seen_timestamp { get; set; }
        public int pin_type { get; set; }
        public int user_id { get; set; }
    }
}
