using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketApi.Models
{
    public class Users
    {
        [BsonId]
        public string _id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool status { get; set; }
    }

    public class Respond
    {
        public string svrp { get; set; }
    }
}