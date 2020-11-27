using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MARKET.Services;

namespace MARKET.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IConfiguration configuration;

        //constructor
        public AuthController(IUserServices userServices, IConfiguration configuration)
        {
            this.userServices = userServices;
            this.configuration = configuration;
        }

     
   
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserManagerResponse response = await userServices.RegisterUserAsync(model);
                if (response.IsSuccess)
                    return Ok(response); // Status Code: 200 

                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserManagerResponse serverResponse = await userServices.LoginUserAsync(model);
                if (serverResponse.IsSuccess)
                    return Ok(serverResponse); // Status Code: 200 

                return BadRequest(serverResponse);
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }
    }
}
