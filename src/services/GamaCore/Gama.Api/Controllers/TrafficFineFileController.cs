using Gama.Application.Contracts.Mappers;
using Gama.Application.Contracts.TrafficFineManagement;
using Gama.Application.DataContracts.Responses.TrafficManagement;
using Gama.Domain.Constants;
using Gama.Domain.Models.TrafficFines;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace Gama.Api.Controllers
{
    [ApiController]
    [Route("v1/traffic-fines/files")]
    public class TrafficFineFileController : Controller
    {
        private readonly ITrafficFineFileService _trafficFineFileService;
        private readonly IEntityMapper _entityMapper;

        public TrafficFineFileController(
            ITrafficFineFileService trafficFineFileService, 
            IEntityMapper entityMapper
            )
        {
            _trafficFineFileService = trafficFineFileService;
            _entityMapper = entityMapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UploadAsync(IFormFile trafficFineFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _trafficFineFileService.UploadAsync(trafficFineFile).ConfigureAwait(false);

            return result.ToOk((trafficFineFile) => _entityMapper.Map<GetTrafficFineResponse, TrafficFineFile>(trafficFineFile));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesName.Cop)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var result = await _trafficFineFileService.GetAsync(id).ConfigureAwait(false);

            if (result is null)
            {
                return NoContent();
            }

            return File(result, "application/octet-stream", $"{Guid.NewGuid}.jpg");
        }
    }
}
