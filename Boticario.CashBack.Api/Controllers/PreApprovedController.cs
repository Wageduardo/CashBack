// ----------------------------------------
// <copyright file=PreApprovedController.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core.SecurityRoles;
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories;
using Boticario.CashBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.CashBack.Api.Controllers
{
    /// <summary>
    /// PreApproved Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class PreApprovedController : ControllerBase
    {
        #region [ Fields ]

        private readonly ILogger<PreApprovedController> logger; 

        #endregion [ Fields ]

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="PreApprovedController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PreApprovedController(ILogger<PreApprovedController> logger)
        {
            this.logger = logger;
        }

        #endregion [ Constructors ]

        #region [ Endpoints ]

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="preApprovedRepository">The pre approved repository.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromServices] IRepository<PreApproved> preApprovedRepository)
        {
            try
            {
                return Ok(await preApprovedRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="preApprovedRepository">The pre approved repository.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="preApproved">The pre approved.</param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromServices] IRepository<PreApproved> preApprovedRepository,
                                               [FromServices] IUserService userService,
                                               [FromBody] PreApproved preApproved)
        {
            try
            {
                var email = User?.Identity.Name;
                var user = await userService.GetUserByEmail(email);
                if (!preApproved.Cpf.All(char.IsDigit))
                {
                    logger.LogWarning($"User '{user.Id}' CPF is invalid use only numbers.");
                    return BadRequest(new ErrorResponse() { Message = "CPF is invalid, please use only numbers" });
                }
                var u = await userService.FindUser(a => a.Cpf == preApproved.Cpf);
                if (u == null)
                {
                    return BadRequest(new ErrorResponse() { Message = $"CPF: {preApproved.Cpf} does not exists in the application." });
                }
                if (await preApprovedRepository.AnyAsync(a => a.Cpf == preApproved.Cpf))
                {
                    return BadRequest(new ErrorResponse() { Message = $"CPF: {preApproved.Cpf} already exists in the pre-approved table" });
                }
                var ret = await preApprovedRepository.AddAsync(preApproved);
                logger.LogInformation($"User with Id: { user.Id } Add new pre-approved CPF {preApproved.Cpf}.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        } 

        #endregion [ Endpoints ]
    }
}
