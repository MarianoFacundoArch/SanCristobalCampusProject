using System;
using System.Collections.Generic;
using System.Text;
using CommonCross.DTOS;

namespace InvestigacionSocial
{
    public class GlobalData
    {
        public static UserTokenDTO LoginData;
        public static PublicUserDTO myUser;
        internal static readonly int BackgroundServiceInterval = 3000;


        /*
        public static object PeopleWaitingToAppearLock = new object();
        public static object QueueGeneralDetailsLock = new object();
        public static object TableCallsLock = new object();
        public static object TableAssignationLock = new object();
        public static int BackgroundServiceInterval = 2000;

        public static string SilhouetteURL =
                "https://images.vexels.com/media/users/3/140756/isolated/lists/bc1bf9c05d5a30332c97edc73e9b70ee-male-standing-with-umbrella.png"
            ;

        public static int QueueYellowFromMinutes = 20;
        public static int QueueYellowUntilMinutes = 40;


        public static int WaitingToAppearYellowFromMinutes = 2;
        public static int WaitingToAppearYellowUntilMinutes = 4;
        */
    }
}
