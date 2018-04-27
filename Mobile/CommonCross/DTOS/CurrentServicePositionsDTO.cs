using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.DTOS
{
    public class CurrentServicePositionsDTO
    {
        public CoordinateDTO RequestedPosition { get; set; }
        public CoordinateDTO OtherUserPosition { get; set; }
        public PublicUserDTO OtherUser { get; set; }
        public string TimeRemaining { get; set;  }
        public int ServiceId { get; set; }
    }
}
