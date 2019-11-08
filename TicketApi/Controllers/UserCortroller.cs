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
    public class UserController : ControllerBase
    {
        IMongoCollection<Users> CollectionUser { get; set; }
        // public static List<Users> Userder = new List<Users>();

        public UserController()
        {
            // var client = new MongoClient("mongodb+srv://admin:1234@cluster0-vx805.azure.mongodb.net/admin?retryWrites=true&w=majority");//เอาคอนเน็คชั่นสตริงมาใส่
            // var databess = client.GetDatabase("TicketTherter");//เอาชื่อดาต้ามาใส่

            var client = new MongoClient("mongodb+srv://admin:1234@cluster0-vx805.azure.mongodb.net/admin?retryWrites=true&w=majority");
            var database = client.GetDatabase("TicketTherter");
            CollectionUser = database.GetCollection<Users>("User");//ชื่อคอเล็กชั่นมาใส่
        }

        // public static List<Users> Userder = new List<Users>
        // {
        //     new Users{
        //         _id=Guid.NewGuid().ToString(),
        //         Name="นายแดง",
        //         Email="sasd",
        //         UserName="0",
        //         Password="0"
        //     }
        // };

        [HttpGet]
        public List<Users> GetAllUser()
        {
            return CollectionUser.Find(it => true).ToList();
        }

        [HttpGet("{iduser}")]
        public ActionResult<Users> getById(string iduser)
        {
            var show = CollectionUser.Find(it => it._id == iduser.ToString()).FirstOrDefault();
            return show;
        }

        [HttpGet("{username}/{pass}")]
        /*
        - ฟังชั่นการเข้าล็อคอิน 
        - เรียก username/pass
        - ในฟังชั่นจะมีการตรวจสอบว่ามี username คนนี้หรือไม่ ถ้าไม่ก็จะทำตามเงื่อนไขที่ได้ประกาศไว้
        */
        public ActionResult<Users> Login(string username, string pass)
        {
            var uu = CollectionUser.Find(it => it.username == username).FirstOrDefault();
            if (uu == null)
            {
                return null;
            }
            else
            {
                if (uu.password == pass)
                {
                    return CollectionUser.Find(it => it.username == username).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPost]
        public void CreateUser([FromBody]Users users)
        {
            var item = new Users()
            {
                _id = Guid.NewGuid().ToString(),
                name = users.name,
                email = users.email,
                username = users.username,
                password = users.password,
                status = false
            };
            CollectionUser.InsertOne(item);
        }

        [HttpPut("{id}")]
        public async void upDateUser(string id, [FromBody]Users edituser)
        {
            // var olduser = CollectionUser.Find(it => it._id == edituser._id).FirstOrDefault();
            // var update = new Users()
            // {
            //     _id = edituser._id,
            //     name = edituser.name,
            //     email = edituser.email,
            //     username = edituser.username,
            //     password = edituser.password,
            // };
            // CollectionUser.ReplaceOne(it => it._id == id, update);

            var def = Builders<Users>.Update
                .Set(it => it.name, edituser.name)
                .Set(it => it.username, edituser.username)
                .Set(it => it.password, edituser.password)
                .Set(it => it.email, edituser.email)
                .Set(it => it.status, edituser.status);
            await CollectionUser.UpdateOneAsync(it => it._id == id, def);
        }
    }
}