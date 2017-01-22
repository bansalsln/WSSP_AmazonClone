

using Amazon.DynamoDBv2.DataModel;
using Entities;
using System.Collections.Generic;

namespace DAL.Contract
{
    public interface DAL_Contract
    {
        void InsertPacket(Packet p, DynamoDBContext context);
        IEnumerable<AndroidWearData> RetrieveAndroidWearData(string user_id);
    }
}
