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
        public string HelloyAppi()
        {
            return "Привет я Апи для  1С";
        }

        [HttpPost("AutUser")]
        public async Task<ActionResult<string>> Aut(UserRequst user)
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

                    var us = sqlContext.Users.SingleOrDefault(x => x.Login == user.login && x.Password == user.password);
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

        [HttpPost("AddUser")]
        public async Task<ActionResult<string>> AddUser(UserAddRequst user)
        {
            using (SqlContext sqlContext = new SqlContext())
            {
                if(string.IsNullOrWhiteSpace(user.name))
                return BadRequest("Имя пользователя не может  быть пустым");

                if (string.IsNullOrWhiteSpace(user.lastName))
                    return BadRequest("Фамилия пользователя не может  быть пустым");

                if (string.IsNullOrWhiteSpace(user.login))
                    return BadRequest("Логин не может быть пустым");

                if (string.IsNullOrWhiteSpace(user.password))
                    return BadRequest("Пароль не может быть пустым");
                
                if (sqlContext.Users.Where(x => x.Login.ToLower() == user.login.ToLower()).Count() > 0)
                   return    StatusCode(202, "Пользователь с таким  логином уже существует  в базе данных");

                try
                {
                    var newUser = new User()
                    {
                        LastName = user.lastName,
                        Login = user.login,
                        Name = user.name,
                        Password = user.password,
                    };

                    if (!string.IsNullOrEmpty(user.patronumic))
                       newUser.Patronumic = user.patronumic;

                    sqlContext.Users.Add(newUser);
                    sqlContext.SaveChanges();
                    return Ok($"пользователь {user.lastName} добавлен  в  базу данных");
                }
                catch (Exception ex)
                {
                   return   StatusCode(400, ex.Message);
                }
            }
        }


        [HttpPost("AutUserAdmin")]
        public async Task<ActionResult<UserResponse>> AutAdmin(UserRequst user)
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

                    var us = sqlContext.Users.SingleOrDefault(x => x.Login == user.login && x.Password == user.password);
                    if (us != null)
                        return Ok( new UserResponse {  lastName = us.LastName , name = us.Name , patronumic = us.Patronumic 
                        , typeuser =  us.isAdmin
                        });
                    else return NotFound("Не верный пароль");
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

    }

    public class UserResponse
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string patronumic { get; set; }
        public  string typeuser { get; set; }

    }

    public class UserRequst
   {
        public string login { get; set;  }
        public string password { get; set;   }
   }

   public class UserAddRequst
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string patronumic { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }


}
