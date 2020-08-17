// ----------------------------------------
// <copyright file=AuthController.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Models.ViewModel;
using Boticario.CashBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Boticario.CashBack.Api.Controllers
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        #region [ Endpoints ]

        /// <summary>
        /// Logins the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="authModel">The authentication model.</param>
        /// <returns></returns>
        [HttpPost()]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromServices] ILogger<AuthController> logger, [FromServices] IUserService userService, [FromBody] AuthModel authModel)
        {
            try
            {
                (User user, string token) = await userService.Authenticate(authModel.Email, authModel.Password);
                if (user == null)
                {
                    logger.LogInformation($"User '{authModel.Email}' or password invalid");
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                logger.LogInformation($"User with E-mail: { authModel.Email} logged into the application. ");
                return Ok(new AuthResponse(user, token));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{ex.Message}");
                return BadRequest(ex);
            }
        } 

        #endregion [ Endpoints ]
    }
}
