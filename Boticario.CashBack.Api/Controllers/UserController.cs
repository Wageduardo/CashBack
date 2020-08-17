// ----------------------------------------
// <copyright file=UserController.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core.SecurityRoles;
using Boticario.CashBack.Models;
using Boticario.CashBack.Models.ViewModel;
using Boticario.CashBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.CashBack.Api.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get all users - Role Administrator
        /// </summary>
        [HttpGet]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromServices] IUserService userService)
        {
            try
            {
                var ret = await GetResponse(userService);
                if (ret == null || ret.Count == 0)
                {
                    logger.LogInformation($"Users not found");
                    return NotFound(new ErrorResponse() { Message = "Users not found" });
                }
                logger.LogInformation($"User '{ (await userService.GetUserByEmail(User?.Identity.Name)).Id }' get all user(s) count {ret.Count} results.");
                return Ok(ret);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Gets the specified user by id.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policies.All)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromServices] IUserService userService, Guid id)
        {
            try
            {
                var user = await userService.GetUser(id);
                if (user == null)
                {
                    logger.LogInformation($"User id: {id} not found.");
                    return NotFound(new ErrorResponse() { Message = $"User id: {id} not found." });
                }
                logger.LogInformation($"User '{ (await userService.GetUserByEmail(User?.Identity.Name)).Id }' performed a search for the user with ID: {id}");
                return Ok(new UserViewModel(user));
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Gets me.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <returns></returns>
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policies.All)]
        public async Task<IActionResult> GetMe([FromServices] IUserService userService)
        {
            try
            {
                var email = User?.Identity.Name;
                var user = await userService.GetUserByEmail(email);
                if (user == null)
                {
                    logger.LogWarning($"User with email: {email} was not found!");
                    return NotFound( new ErrorResponse() { Message = $"User with email: {email} was not found!" });
                }

                logger.LogInformation($"User '{user.Id}' logged into the application");
                return Ok(new UserViewModel(user));
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromServices] IUserService userService, [FromBody] User user)
        {
            try
            {
                var ret = await userService.CreateUser(user);
                logger.LogInformation($"User '{(await userService.GetUserByEmail(User?.Identity.Name)).Id}' created a new user with permission: {ret.Role} and E-mail: {ret.Email}.");
                return Created($"{ GetType().Name.Replace("Controller", "").ToLower()}/", new UserViewModel(ret));
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost("CreateAdmintUser")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiExplorerSettings(IgnoreApi = true)] // Hide from Swagger Docs
        public async Task<IActionResult> CreateAdmintUser([FromServices] IUserService userService, [FromBody] User user)
        {
            try
            {
                if (user.Role != Policies.Administrator)
                    return BadRequest(new ErrorResponse() { Message = $"Role must be {Policies.Administrator}" });

                var ret = await userService.CreateUser(user);
                logger.LogInformation($"Administrator user created with email: {user.Email}.");
                return Created($"{ GetType().Name.Replace("Controller", "").ToLower()}/", new UserViewModel(ret));
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetMyExternalAccumulatedCashback")]
        [Authorize(Policies.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyExternalAccumulatedCashback([FromServices] IUserService userService, [FromServices] IExternalService externalService)
        {
            try
            {
                var email = User?.Identity.Name;
                var user = await userService.GetUserByEmail(email);
                if (user != null)
                {
                    var cashback = await externalService.GetCashback(user.Cpf);
                    logger.LogInformation($"User {user.Id} cash-back is: {cashback.Body.Credit}.");

                    return Ok(cashback);
                }
                logger.LogInformation($"User not found");
                return NotFound(new ErrorResponse() { Message = $"User not found" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetMyInternalAccumulatedCashback")]
        [Authorize(Policies.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyInternalAccumulatedCashback([FromServices] IUserService userService,
                                                                          [FromServices] IPurchaseService purchaseService)
        {
            try
            {
                var email = User?.Identity.Name;
                var user = await userService.GetUserByEmail(email);
                if (user != null)
                {
                    var cashback = await purchaseService.GetCashback(a => a.Email.ToLower() == email.ToLower());
                    logger.LogInformation($"User {user.Id} cash-back is: {cashback}.");
                    return Ok(new ExternalViewModel() { StatusCode = StatusCodes.Status200OK, Body = new Body() { Credit = (decimal)cashback } });
                }
                logger.LogInformation($"User not found");
                return NotFound(new ErrorResponse() { Message = $"User not found" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetExternalAccumulatedCashback")]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExternalAccumulatedCashback([FromServices]IUserService userService,
                                                                        [FromServices] IExternalService externalService,
                                                                        [FromQuery] string cpf)
        {
            try
            {
                if (!cpf.All(char.IsDigit))
                {
                    return await TreatInvalidCpf(userService, cpf);
                }

                var cashback = await externalService.GetCashback(cpf);
                logger.LogInformation($"User '{(await userService.GetUserByEmail(User?.Identity.Name)).Id}' performed a user cash-back search with CPF: {cpf}.");
                return Ok(cashback);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetInternalAccumulatedCashback")]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInternalAccumulatedCashback([FromServices] IUserService userService,
                                                                        [FromServices] IPurchaseService purchaseService,
                                                                        [FromQuery] string cpf)
        {
            try
            {
                if (!cpf.All(char.IsDigit))
                {
                    return await TreatInvalidCpf(userService, cpf);
                }

                var cashback = await purchaseService.GetCashback(a => a.Cpf == cpf);
                logger.LogInformation($"User '{(await userService.GetUserByEmail(User?.Identity.Name)).Id}' performed a user cash-back search with CPF: {cpf}.");
                return Ok(new ExternalViewModel() { StatusCode = StatusCodes.Status200OK, Body = new Body() { Credit = (decimal)cashback } });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Treats the invalid CPF.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="cpf">The CPF.</param>
        /// <returns></returns>
        private async Task<IActionResult> TreatInvalidCpf(IUserService userService, string cpf)
        {
            logger.LogInformation($"User '{(await userService.GetUserByEmail(User?.Identity.Name)).Id}' performed a user cash-back search with invalid CPF: {cpf}.");
            return BadRequest(new ErrorResponse() { Message = "CPF is invalid, please use only numbers" });
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <returns></returns>
        private static async Task<List<UserViewModel>> GetResponse(IUserService userService)
        {
            var list = new List<UserViewModel>();
            (await userService.GetAll()).ForEach(a => list.Add(new UserViewModel(a)));
            return list;
        }
    }
}