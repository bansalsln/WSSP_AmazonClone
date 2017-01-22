using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System.Threading;

namespace DAL.Implementation
{
    public class DynamoDBImplementation : Contract.DAL_Contract
    {
        static string TABLE_ANDROIDWEARDATA = "AndroidWearData";
        static string TABLE_VEHICLEDATA = "VehicleData";
        static readonly string[] ENTITY_TABLE_NAMES = { TABLE_ANDROIDWEARDATA, TABLE_VEHICLEDATA };

        //AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        //DynamoDBContext context = new DynamoDBContext(client);

        public void CreateTables(AmazonDynamoDBClient client)
        {
            List<string> currentTables = client.ListTables().TableNames;
            bool tablesAdded = false;

            //check for TABLE_ANDROIDWEARDATA
            if (!currentTables.Contains(TABLE_ANDROIDWEARDATA))
            {
                //Console.WriteLine("Table AndroidWearData does not exist, creating");
                client.CreateTable(new CreateTableRequest
                {
                    TableName = TABLE_ANDROIDWEARDATA,
                    ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 3, WriteCapacityUnits = 1 },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "user_id",
                            KeyType = KeyType.HASH
                        },
                        new KeySchemaElement
                        {
                            AttributeName = "time_stamp",
                            KeyType = KeyType.RANGE
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition { AttributeName = "user_id", AttributeType =  ScalarAttributeType.S},
                        new AttributeDefinition { AttributeName = "time_stamp", AttributeType = ScalarAttributeType.N }
                    }
                });
                tablesAdded = true;
            }

            //check for TABLE_OPENXCDATA
            if (!currentTables.Contains(TABLE_VEHICLEDATA))
            {
                //Console.WriteLine("Table OpenXCData does not exist, creating");
                client.CreateTable(new CreateTableRequest
                {
                    TableName = TABLE_VEHICLEDATA,
                    ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 3, WriteCapacityUnits = 1 },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "user_id",
                            KeyType = KeyType.HASH
                        },
                        new KeySchemaElement
                        {
                            AttributeName = "time_stamp",
                            KeyType = KeyType.RANGE
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition { AttributeName = "user_id", AttributeType = ScalarAttributeType.S },
                        new AttributeDefinition { AttributeName = "time_stamp", AttributeType = ScalarAttributeType.N }
                    }
                });
                tablesAdded = true;
            }

            if (tablesAdded)
            {
                bool allActive;
                do
                {
                    allActive = true;
                    //Console.WriteLine("While tables are still being created, sleeping for 5 seconds...");
                    Thread.Sleep(TimeSpan.FromSeconds(5));

                    foreach (var tableName in ENTITY_TABLE_NAMES)
                    {
                        TableStatus tableStatus = GetTableStatus(client, tableName);
                        if (!object.Equals(tableStatus, TableStatus.ACTIVE))
                            allActive = false;
                    }
                } while (!allActive);
            }

            //Console.WriteLine("All sample tables created");
        }

        public void InsertPacket(Packet p, DynamoDBContext context)
        {
            //create sample packet for testing purpose... else use p 
            //Packet objPacket = new Packet();
            //objPacket = CreateSamplePacket();

            foreach (var itemAndroidWearData in p.AndroidWearData)
            {
                itemAndroidWearData.user_id = p.user_id;
                context.Save<AndroidWearData>(itemAndroidWearData);
            }

            foreach (var itemVehicleData in p.VehicleData)

            {
                itemVehicleData.user_id = p.user_id;
                context.Save<VehicleData>(itemVehicleData);
            }
        }

        public IEnumerable<AndroidWearData> RetrieveAndroidWearData(string user_id)
        {
            throw new NotImplementedException();
        }

        private static Packet CreateSamplePacket()
        {
            Packet objPacket = new Packet();
            AndroidWearData[] aw = new AndroidWearData[2];
            VehicleData[] vd = new VehicleData[2];
            AndroidWearData objAndroidWearData = new AndroidWearData();
            AndroidWearData objAndroidWearData1 = new AndroidWearData();
            VehicleData objVehicleData = new VehicleData();

            objPacket.user_id = "1009";

            //Console.WriteLine("Creating AndroidWearData data 1");
            objAndroidWearData.user_id = "1009";
            objAndroidWearData.TimeStamp = 14800000009;
            objAndroidWearData.Accelerometer_X = 9.0;
            objAndroidWearData.Accelerometer_Y = 0.0;
            objAndroidWearData.Accelerometer_Z = 9.0;
            objAndroidWearData.Gyroscope_X = 0.0;
            objAndroidWearData.Gyroscope_Y = 0.0;
            objAndroidWearData.Gyroscope_Z = 0.0;
            objAndroidWearData.HeartRate = 109.0;
            aw[0] = objAndroidWearData;


            //Console.WriteLine("Creating AndroidWearData data 2");
            objAndroidWearData1.user_id = "1009";
            objAndroidWearData1.TimeStamp = 1481000000;
            objAndroidWearData1.Gyroscope_X = null;
            objAndroidWearData1.Gyroscope_Y = 3.0;
            objAndroidWearData1.Gyroscope_Z = 0.0;
            objAndroidWearData1.HeartRate = 103.0;
            aw[1] = objAndroidWearData1;

            objPacket.AndroidWearData = aw;

            //Console.WriteLine("Creating OpenXCData data 1");
            objVehicleData.user_id = "1009";
            objVehicleData.TimeStamp = 1481603918157;
            objVehicleData.SteeringWheelAngle = 0.0;
            objVehicleData.TorqueAtTransmission = 0.0;
            objVehicleData.EngineSpeed = 0.0;
            objVehicleData.VehicleSpeed = 0.0;
            objVehicleData.AcceleratorPedalPosition = 0.0;
            objVehicleData.BrakePedalStatus = false;
            objVehicleData.TransmissionGearPosition = string.Empty;
            objVehicleData.Odometer = 0.0;
            vd[0] = objVehicleData;
            objPacket.VehicleData = vd;

            return objPacket;
        }

        #region GetTableStatus
        /// <summary>
        /// Retrieves a table status. Returns empty string if table does not exist.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static TableStatus GetTableStatus(AmazonDynamoDBClient client, string tableName)
        {
            try
            {
                var table = client.DescribeTable(new DescribeTableRequest { TableName = tableName }).Table;
                return (table == null) ? null : table.TableStatus;
            }
            catch (AmazonDynamoDBException db)
            {
                if (db.ErrorCode == "ResourceNotFoundException")
                    return string.Empty;
                throw;
            }
        }
        #endregion
    }
}
