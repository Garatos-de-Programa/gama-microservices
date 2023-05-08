using Gama.Application.Contracts.Mappers;
using Gama.Application.Contracts.OccurrenceManagement;
using Gama.Application.DataContracts.Commands.OccurrenceManagement;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Application.DataContracts.Responses.OccurrenceManagement;
using Gama.Application.DataContracts.Responses.Pagination;
using Gama.Application.Seedworks.Pagination;
using Gama.Domain.Constants;
using Gama.Domain.Entities;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gama.Api.Controllers
{
    [ApiController]
    [Route("v1/occurrence")]
    public class OccurrencesController : Controller
    {
        private readonly IOccurrenceService _occurrenceService;
        private readonly IEntityMapper _entityMapper;

        public OccurrencesController(
            IOccurrenceService occurrenceService,
            IEntityMapper entityMapper
            )
        {
            _occurrenceService = occurrenceService;
            _entityMapper = entityMapper;
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetOccurrence")]
        [ProducesResponseType(typeof(GetOccurrenceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var occurrence = await _occurrenceService.GetAsync(id).ConfigureAwait(false);

            return occurrence.ToOk((result) => _entityMapper.Map<GetOccurrenceResponse, Occurrence>(result));
        }

        [Authorize]
        [HttpGet(Name = "Search")]
        [ProducesResponseType(typeof(OffsetPageResponse<SearchOcurrenceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SearchAsync([FromQuery] DateSearchQuery search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trafficFines = await _occurrenceService.GetByDateSearchAsync(search).ConfigureAwait(false);

            return trafficFines.ToOk((result) => _entityMapper.Map<OffsetPageResponse<SearchOcurrenceResponse>, OffsetPage<Occurrence>>(result));
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(GetOccurrenceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOccurrenceCommand createOccurrenceCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trafficFine = _entityMapper.Map<Occurrence, CreateOccurrenceCommand>(createOccurrenceCommand);

            var result = await _occurrenceService.CreateAsync(trafficFine).ConfigureAwait(false);

            return result.ToCreated();
        }

        [Authorize(Roles = RolesName.Cop)]
        [HttpDelete("{id:int}", Name = "DeleteTrafficFine")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _occurrenceService.DeleteAsync(id).ConfigureAwait(false);

            return result.ToNoContent();
        }
    }
}
