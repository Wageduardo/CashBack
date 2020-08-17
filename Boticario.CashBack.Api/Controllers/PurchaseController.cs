// ----------------------------------------
// <copyright file=PurchaseController.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core.SecurityRoles;
using Boticario.CashBack.Models;
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
    /// Purchase Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : Controller
    {
        #region [ Fields ]

        private readonly ILogger<PurchaseController> logger;

        #endregion [ Fields ]

        #region [ Constructors ]

        public PurchaseController(ILogger<PurchaseController> logger)
        {
            this.logger = logger;
        }

        #endregion [ Constructors ]

        #region [ Endpoints ]

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="purchaseService">The purchase service.</param>
        /// <param name="userService">The user service.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policies.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync([FromServices] IPurchaseService purchaseService,
                                                 [FromServices] IUserService userService)
        {
            try
            {
                User user = await GetUser(userService);
                if (user == null)
                {
                    return TreatUserNotFound();
                }
                var items = await purchaseService.GetUserPurchases(a => a.Email.ToLower() == user.Email.ToLower());
                logger.LogInformation($"user '{ user.Id }' searched for purchases and found {items.Count} results");
                return Ok(items);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="purchaseService">The purchase service.</param>
        /// <param name="userService">The user service.</param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromServices] IPurchaseService purchaseService,
                                                [FromServices] IUserService userService)
        {
            try
            {
                User user = await GetUser(userService);
                if (user == null)
                {
                    return TreatUserNotFound();
                }
                var items = await purchaseService.GetAll();
                logger.LogInformation($"User '{user.Id}' exec GetAll method.");
                return Ok(items);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Gets the identifier asynchronous.
        /// </summary>
        /// <param name="purchaseService">The purchase service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIdAsync([FromServices] IPurchaseService purchaseService,
                                             [FromServices] IUserService userService,
                                             Guid id)
        {
            try
            {
                User user = await GetUser(userService);
                if (user == null)
                {
                    return TreatUserNotFound();
                }
                var purchase = await purchaseService.GetPurchase(id);
                logger.LogInformation($"user '{user.Id}' searched for purchase order:  {id}");
                if (purchase == null)
                {
                    logger.LogInformation($"Order: {id} not found");
                    return NotFound(new ErrorResponse() { Message = $"Order: {id} not found" });
                }
                return Ok(purchase);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
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
        /// <param name="purchaseService">The purchase service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="purchase">The purchase.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policies.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromServices] IPurchaseService purchaseService,
                                                [FromServices] IUserService userService,
                                                Purchase purchase)
        {
            try
            {
                User user = await GetUser(userService);
                if (user == null)
                {
                    return TreatUserNotFound();
                }

                var ret = await purchaseService.CreatePurchase(purchase, user);
                logger.LogInformation($"User {user.Id} created purchase code: {ret.Code}.");
                return Ok(ret);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError($"{ex.Message}");
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="purchaseService">The purchase service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="purchase">The purchase.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Policies.All)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromServices] IPurchaseService purchaseService,
                                                [FromServices] IUserService userService,
                                                Guid id,
                                                Purchase purchase)
        {
            try
            {
                User user = await GetUser(userService);
                if (user == null)
                {
                    return TreatUserNotFound();
                }
                var ret = await purchaseService.UpdatePurchase(purchase, user);
                if (ret == null)
                {
                    logger.LogWarning($"Order not found {id}");
                    return BadRequest(new ErrorResponse() { Message = $"Order not found {id}" });
                }
                logger.LogInformation($"User {user.Id} update the purchase Id: {id}.");
                return Ok(ret);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError($"{ex.Message}");
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="purchaseService">The purchase service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policies.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromServices] IPurchaseService purchaseService,
                                                [FromServices] IUserService userService,
                                                Guid id)
        {
            try
            {
                User user = await GetUser(userService);
                if (user == null)
                {
                    return TreatUserNotFound();
                }
                var purchase = await purchaseService.GetPurchase(id);
                if (purchase == null)
                {
                    logger.LogInformation($"Purchase ID: {id} not found");
                    return NotFound(new ErrorResponse() { Message = $"Purchase ID: {id} not found" });
                }

                await purchaseService.DeletePurchase(purchase);
                logger.LogInformation($"User {user.Id} Deleted purchase Id: {id}.");
                return Ok(purchase);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        #endregion [ Endpoints ]

        #region [ Private methods ]

        /// <summary>
        /// Treats the user not found.
        /// </summary>
        /// <returns></returns>
        private IActionResult TreatUserNotFound()
        {
            logger.LogInformation($"User not found");
            return NotFound(new ErrorResponse() { Message = "User not found" });
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <returns></returns>
        private async Task<User> GetUser(IUserService userService)
        {
            var email = User?.Identity.Name;
            var user = await userService.GetUserByEmail(email);
            return user;
        } 

        #endregion [ Private methods ]
    }
}