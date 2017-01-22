using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ford_Env_Service.Controllers
{
  
    [Authorize]
    public class PacketController : ApiController
    {
        private DAL.Contract.DAL_Contract repository { get; set; }

        public PacketController(DAL.Contract.DAL_Contract dal)
        {
            repository = dal;
        }


        [HttpPost]
        public IHttpActionResult Post(Packet p)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            DynamoDBContext context = new DynamoDBContext(client);
            var id = User.Identity.GetUserId();

            if (p.user_id != id)
                p.user_id = id;

            try
            {
                repository.InsertPacket(p, context);
                return Ok();
            }
            catch (Exception E)
            {
                return InternalServerError(E);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAndroidWearData(string id)
        {
            try
            {
                var data = repository.RetrieveAndroidWearData(id);
                return Ok(data);
            }
            catch(Exception E)
            {
                return NotFound();
            }
        }
    }
}
