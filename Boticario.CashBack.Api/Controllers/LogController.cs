// ----------------------------------------
// <copyright file=LogController.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core.SecurityRoles;
using Boticario.CashBack.Repositories.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Boticario.CashBack.Api.Controllers
{
    /// <summary>
    /// Los Controllers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        #region [ Fields ]

        private readonly ILogger<LogController> logger;

        #endregion [ Fields ]

        #region [ Constructors ]        
        /// <summary>
        /// Initializes a new instance of the <see cref="LogController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LogController(ILogger<LogController> logger)
        {
            this.logger = logger;
        }

        #endregion [ Constructors ]

        #region [ Endpoints ]        
        /// <summary>
        /// Gets the specified log context.
        /// </summary>
        /// <param name="logContext">The log context.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policies.Administrator)]
        public async Task<IActionResult> Get([FromServices] LogContext logContext)
        {
            try
            {
                return Ok(await logContext.Logs?.ToListAsync());
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
