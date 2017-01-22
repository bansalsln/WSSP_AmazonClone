
using System.Runtime.Serialization;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Entities
{
    [DataContract]
    [DynamoDBTable("VehicleData")]
    public class VehicleData
    {
        [DynamoDBHashKey]
        public string user_id { get; set; }

        [DynamoDBRangeKey(AttributeName = "time_stamp")]
        [DataMember(Name = "time_stamp")]
        public double TimeStamp { get; set; }

        [DataMember(Name = "steering_wheel_angle")]
        public double? SteeringWheelAngle { get; set; }

        [DataMember(Name = "torque_at_transmission")]
        public double? TorqueAtTransmission { get; set; }

        [DataMember(Name = "engine_speed")]
        public double? EngineSpeed { get; set; }

        [DataMember(Name = "vehicle_speed")]
        public double? VehicleSpeed { get; set; }

        [DataMember(Name = "accelerator_pedal_position")]
        public double? AcceleratorPedalPosition { get; set; }

        [DataMember(Name = "brake_pedal_status")]
        public bool? BrakePedalStatus { get; set; }

        [DataMember(Name = "transmission_gear_position")]
        public string TransmissionGearPosition { get; set; }

        [DataMember(Name = "odometer")]
        public double? Odometer { get; set; }

        [DataMember(Name = "ignition_status")]
        public string IgnitionStatus { get; set; }

        [DataMember(Name = "fuel_level")]
        public double? FuelLevel { get; set; }

        [DataMember(Name = "fuel_consumed_since_restart")]
        public double? FuelConsumedSinceRestart { get; set; }

        [DataMember(Name = "headlamp_status")]
        public bool? HeadlampStatus { get; set; }

        [DataMember(Name = "high_beam_status")]
        public bool? HighBeamStatus { get; set; }

        [DataMember(Name = "windshield_wiper_status")]
        public bool? WindshieldWiperStatus { get; set; }

    }

    /*
    public enum Gear_Position
    {
        Reverse = -1, Neutral = 0, First = 1, Second = 2, Third = 3, Fourth = 4, Fifth = 5, Sixth = 6, Seventh = 7, Eighth = 8
    }
    public enum Ignition_Status
    {
        Off = 0, Accessory = 1, Run = 2, Start = 3
    }*/
}
