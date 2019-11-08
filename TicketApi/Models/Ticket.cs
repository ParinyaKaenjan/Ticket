using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketApi.Models
{
    public class Ticket
    {
        [BsonId]
        public string _id { get; set; }
        public string iduser { get; set; }
        public string imgticket { get; set; }
        public string namemovie { get; set; }
        public int typeticket { get; set; }
        public int totalticket { get; set; }
        public string timemovie { get; set; }
        public string totalprice { get; set; }
        public bool ticketstatus { get; set; }

        internal void Add(Ticket ticketmovie)
        {
            throw new NotImplementedException();
        }
    }
}