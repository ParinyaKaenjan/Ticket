using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TicketApi.Models;

namespace TicketApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        IMongoCollection<Ticket> CollectionTicket { get; set; }
        IMongoCollection<Movie> CollectionMovie { get; set; }
        // public static List<Users> Userder = new List<Users>();
        public TicketController()
        {
            // var client = new MongoClient("mongodb+srv://admin:1234@cluster0-vx805.azure.mongodb.net/admin?retryWrites=true&w=majority");//เอาคอนเน็คชั่นสตริงมาใส่
            // var databess = client.GetDatabase("TicketTherter");//เอาชื่อดาต้ามาใส่
            var client = new MongoClient("mongodb+srv://admin:1234@cluster0-vx805.azure.mongodb.net/admin?retryWrites=true&w=majority");
            var database = client.GetDatabase("TicketTherter");
            CollectionTicket = database.GetCollection<Ticket>("Ticket");//ชื่อคอเล็กชั่นมาใส่
            CollectionMovie = database.GetCollection<Movie>("Movie");
        }

        // public static List<Ticket> Tickets = new List<Ticket>{
        //         new Ticket{
        //         idticket = Guid.NewGuid().ToString(),
        //         imgticket ="http://media.comicbook.com/2017/12/new-black-panther-movie-poster-2018--1064120.jpg",
        //         namemovie="BlackPanter",
        //         typeticket= 30,
        //         totalticket= 5,
        //         timemovie="10:00",
        //         totalprice= "150",
        //         }
        // };

        [HttpGet]
        public List<Ticket> GetAllTicket()
        {
            return CollectionTicket.Find(it => true).ToList();
        }

        [HttpGet("{iduser}")]
        public List<Ticket> getbyID(string iduser)
        {
            var getticket = CollectionTicket.Find(it => it.iduser == iduser && it.ticketstatus == true).ToList();

            return getticket;
        }

        [HttpPost("{iduser}")]
        public void Createticket([FromBody]Ticket ticket, string iduser)
        {
            var ticketmovie = new Ticket()
            {
                _id = Guid.NewGuid().ToString(),
                iduser = iduser,
                imgticket = ticket.imgticket,
                namemovie = ticket.namemovie,
                typeticket = ticket.typeticket,
                totalticket = ticket.totalticket,
                timemovie = ticket.timemovie,
                totalprice = ticket.totalprice,
                ticketstatus = true
            };
            CollectionTicket.InsertOne(ticketmovie);
            //     //การรีเทรินสตริง
            //     var sd = new Respond
            //     {
            //         svrp = "string"
            //     };
            //     return sd.svrp;
        }

        // [HttpDelete("{_id}")]
        // public void DeleteTicket([FromBody]Ticket delticket)
        // {
        //     var del = CollectionTicket.Find(it => it._id == delticket._id);
        //     CollectionTicket.DeleteOne(it => it._id == delticket._id);

        // }

        [HttpPost("{id}/{amount}")]
        public void BuyTicket(string id, int amount)
        {
            var count = CollectionMovie.Find(it => it._id == id).FirstOrDefault();
            var sum = int.Parse(count.totalticket) - amount;
            var def = Builders<Movie>.Update.Set(it => it.totalticket, sum.ToString());
            CollectionMovie.UpdateOne(it => it._id == id, def);
        }
        [HttpPost("{id}")]
        public void UseTicket(string id)
        {
            var dd = CollectionTicket.Find(it => it._id == id).FirstOrDefault();
            CollectionTicket.UpdateOne(it => it._id == id, Builders<Ticket>.Update.Set(it => dd.ticketstatus, false));
        }

    }
}