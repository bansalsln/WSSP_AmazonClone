using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DAL.Implementation;
using Entities;

[assembly: OwinStartup(typeof(Ford_Env_Service.Startup))]

namespace Ford_Env_Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            //DynamoDBContext context = new DynamoDBContext(client);
            //DynamoDBImplementation.CreateTables(client);
            //Packet p = new Packet();
            //InsertPacket(p, context);
            ConfigureAuth(app);
        }
    }
}
