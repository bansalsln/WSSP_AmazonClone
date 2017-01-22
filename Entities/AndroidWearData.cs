

using System.Runtime.Serialization;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Entities
{
    [DataContract]
    [DynamoDBTable("AndroidWearData")]
    public class AndroidWearData
    {
        [DynamoDBHashKey]
        public string user_id { get; set; }

        [DynamoDBRangeKey(AttributeName = "time_stamp")]
        [DataMember(Name = "time_stamp")]
        public double TimeStamp { get; set; }

        [DataMember(Name = "accelerometer_x")]
        public double? Accelerometer_X { get; set; }

        [DataMember(Name = "accelerometer_y")]
        public double? Accelerometer_Y { get; set; }

        [DataMember(Name = "accelerometer_z")]
        public double? Accelerometer_Z { get; set; }

        [DataMember(Name = "gyroscope_x")]
        public double? Gyroscope_X { get; set; }

        [DataMember(Name = "gyroscope_y")]
        public double? Gyroscope_Y { get; set; }

        [DataMember(Name = "gyroscope_z")]
        public double? Gyroscope_Z { get; set; }

        [DataMember(Name = "heart_rate")]
        public double? HeartRate { get; set; }
    }
}
