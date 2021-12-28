using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFormBD.Api.EF;

namespace WebFormBD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutUserController : ControllerBase
    {

        [HttpGet("HelloyAppi")]
        public  string HelloyAppi ()
        {
            return "Привет я Апи для  1С";
        }



        [HttpPost ("AutUser")]
        public async Task<ActionResult< string>> Aut ( UserRequst user)
        {
            using (SqlContext sqlContext = new SqlContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(user.login))
                        return BadRequest("Логин не может быть пустым");

                    if (string.IsNullOrWhiteSpace(user.password))
                        return BadRequest("Пароль не может быть пустым");

                   var login = sqlContext.Users.SingleOrDefault(x => x.Login == user.login);
                   if (login == null)
                        return NotFound("Пользователь с таким  логином не найден");
                   
                    var us =   sqlContext.Users.SingleOrDefault(x => x.Login == user.login && x.Password == user.password);
                    if (us != null)
                        return Ok($"{us.LastName} {us.Name} {us.Patronumic}");
                    else return NotFound("Не верный пароль");
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }
    }

    public class UserRequst
    {
        public string login { get; set;  }
        public string password { get; set;   }
    }
}
