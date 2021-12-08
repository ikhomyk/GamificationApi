using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Gamification.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [Route("api/thank")]
    [ApiController]
    [Authorize]
    public class ThankController : ControllerBase
    {
        private IThankService _thankService { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ThankController(IThankService thankService, IHttpContextAccessor httpContextAccessor)
        {
            _thankService = thankService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<CreateThankDTO>> GetLastThankAsync(CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                GetThankDTO lastThank = await _thankService.GetLastThankAsync(userId, cancellationToken);
                return Ok(lastThank);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateThankDTO>> SayThankAsync(CreateThankDTO newThank, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                CreateThankDTO thank = await _thankService.SayThankAsync(userId, newThank, cancellationToken);
                return Ok(thank);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}