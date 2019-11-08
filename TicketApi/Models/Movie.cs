using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketApi.Models
{
    public class Movie
    {
        [BsonId]
        public string _id { get; set; }
        public string img { get; set; }
        public string faculty { get; set; }
        public string showtime { get; set; }
        public string namemovie { get; set; }
        public string datemovie { get; set; }
        public int ticketvip { get; set; }
        public int ticketnomal { get; set; }
        public string totalticket { get; set; }
        public string detailmovie { get; set; }

        internal static object Find(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }

}