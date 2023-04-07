using System;
using ATCer.HRCenter.Enums;

namespace ATCer.HRCenter.Dtos
{
    public class ImportUserAtoInfoDto
    {
        public string UserName { get; set; }
        public string HirachyName { get; set; }
        //[BsonIgnore]
        //public double HirachyMultiplier { get; set; } = 1;
        public ATCLevel UserLevel { get; set; }
        //[BsonIgnore]
        //public string LevelName { get; set; }
        public ATCDepartment Department { get; set; }
    }
}

