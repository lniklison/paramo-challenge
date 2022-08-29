using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Services.Abstract;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = string.Empty;
            if(areFieldsEmpty(name, email, address, phone, ref errors))
            {
                return new Result(false, errors);
            }

            return await _userService.CreateUser(name, email, address, phone, userType, money);
        }

        //Validate errors
        private bool areFieldsEmpty(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null || name == String.Empty)
                errors = Constants.NAME_REQUIRED;

            if (email == null || name == String.Empty)
                errors = errors + Constants.EMAIL_REQUIRED;

            if (address == null || name == String.Empty)
                errors = errors + Constants.ADDRESS_REQUIRED;

            if (phone == null || name == String.Empty)
                errors = errors + Constants.PHONE_REQUIRED;

            return errors == null && errors != String.Empty;
        }


    }
}
