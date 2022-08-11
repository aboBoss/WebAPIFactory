using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.BAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {

        private readonly UserBAL _bl = new UserBAL();
        //public UserController()
        //{

        //}

        [HttpGet]
        [Route("api/user/all")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_bl.GetAllUserFromDb());

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/user/add")]
        public IHttpActionResult Add([FromBody] User user)
        {
            try
            {
                return Ok(_bl.AddUser(user));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        [Route("api/user/login")]
        public IHttpActionResult Login([FromBody] User user)
        {
            try
            {
                return Ok(_bl.Login(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}