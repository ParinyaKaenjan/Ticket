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
    public class MovieController : ControllerBase
    {
        IMongoCollection<Movie> CollectionMovie { get; set; }
        // IMongoCollection<Ticket> CollectionTicket { get; set; }
        // public static List<Users> Userder = new List<Users>();
        public MovieController()
        {
            // var client = new MongoClient("mongodb+srv://admin:1234@cluster0-vx805.azure.mongodb.net/admin?retryWrites=true&w=majority");//เอาคอนเน็คชั่นสตริงมาใส่
            // var databess = client.GetDatabase("TicketTherter");//เอาชื่อดาต้ามาใส่

            var client = new MongoClient("mongodb+srv://admin:1234@cluster0-vx805.azure.mongodb.net/admin?retryWrites=true&w=majority");
            var database = client.GetDatabase("TicketTherter");
            CollectionMovie = database.GetCollection<Movie>("Movie");//ชื่อคอเล็กชั่นมาใส่

        }

        // public static List<Movie> Movies = new List<Movie>
        // {
        //      new Movie{
        //         idmovie = Guid.NewGuid().ToString(),
        //         img = "http://media.comicbook.com/2017/12/new-black-panther-movie-poster-2018--1064120.jpg" ,
        //         namemovie ="BlackPanter",
        //         datemovie = "25 ตุลาคม 2562",
        //         detailmovie = "เรื่องย่อ Black Panther 2018 เรื่องราวต่อมา ของกษัตริย์องค์ใหม่ต้องรับมือกับศัตรูคนใหม่และศัตรูทางการเมืองเพื่อปกป้องประเทศ Wakada ของเขาให้รอดพ้นจากสงครามโลกครั้งใหม่ โดยภาพยนตร์เรื่องนี้ได้นักแสดงผิวสีมากมายฝีมือ เช่น Michael B. Jordan, Danai Gurira Forest Whitaker นอกจากนี้ยังได้นักแสดงดีกรีรางวัลออสการ์อย่าง Lupita Nyong’o มาร่วมแสดงในเรื่องนี้ด้วย กำกับโดย Ryan Coogler เจอกัน แบล็ค แพนเธอร์"

        //         },
        //     new Movie{
        //         idmovie = Guid.NewGuid().ToString(),
        //         img = "https://amc-theatres-res.cloudinary.com/v1566839258/amc-cdn/production/2/movies/61500/61497/PosterDynamic/90289.jpg" ,
        //         namemovie ="SpiderMan",
        //         datemovie = "25 ตุลาคม 2562",
        //         }
        // };

        [HttpGet]
        public List<Movie> GetAllMovie()
        {
            return CollectionMovie.Find(it => true).ToList();
        }

        [HttpGet("{idmovie}")]
        public ActionResult<Movie> getbyID(string idmovie)
        {
            var getID = CollectionMovie.Find(it => it._id == idmovie.ToString()).FirstOrDefault();
            return getID;
        }

        [HttpPost]
        public void Createmovie([FromBody]Movie Createmovie)
        {
            var Newmovie = new Movie()
            {
                _id = Guid.NewGuid().ToString(),
                img = Createmovie.img,
                faculty = Createmovie.faculty,
                showtime = Createmovie.showtime,
                namemovie = Createmovie.namemovie,
                datemovie = Createmovie.datemovie,
                ticketvip = Createmovie.ticketvip,
                ticketnomal = Createmovie.ticketnomal,
                totalticket = Createmovie.totalticket,
                detailmovie = Createmovie.detailmovie
            };
            CollectionMovie.InsertOne(Newmovie);
        }

        [HttpPut("{idmovie}")]
        public async void updatemovie([FromBody]Movie idmovie)
        {
            var um = Builders<Movie>.Update
            .Set(it => it._id, idmovie._id)
           .Set(it => it.img, idmovie.img)
           .Set(it => it.faculty, idmovie.faculty)
           .Set(it => it.showtime, idmovie.showtime)
           .Set(it => it.namemovie, idmovie.namemovie)
           .Set(it => it.datemovie, idmovie.datemovie)
           .Set(it => it.ticketvip, idmovie.ticketvip)
           .Set(it => it.ticketnomal, idmovie.ticketnomal)
           .Set(it => it.totalticket, idmovie.totalticket)
           .Set(it => it.detailmovie, idmovie.detailmovie);
            await CollectionMovie.UpdateOneAsync(it => it._id == idmovie._id, um);
        }

        [HttpDelete("{_id}")]
        public void deleteMovie(string _id)
        {
            var delM = CollectionMovie.Find(it => it._id == _id);
            CollectionMovie.DeleteOne(it => it._id == _id);
        }
    }
}