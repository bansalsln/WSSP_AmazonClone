

using System.Runtime.Serialization;

namespace Entities
{
    [DataContract]
    public class Packet
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "android_wear_data")]
        public AndroidWearData[] AndroidWearData { get; set; }

        [DataMember(Name = "vehicle_data")]
        public VehicleData[] VehicleData { get; set; }
    }
}
