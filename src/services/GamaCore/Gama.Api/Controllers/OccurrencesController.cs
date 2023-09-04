using Gama.Application.Contracts.Mappers;
using Gama.Application.Seedworks.Pagination;
using Gama.Application.Seedworks.Queries;
using Gama.Application.UseCases.OccurrenceAgg.Commands;
using Gama.Application.UseCases.OccurrenceAgg.Interfaces;
using Gama.Application.UseCases.OccurrenceAgg.Responses;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.ValueTypes;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Gama.Domain.Entities.OccurrencesAgg.Models;

namespace Gama.Api.Controllers
{
    [ApiController]
    [Route("v1/occurrences")]
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
            var occurrence = await _occurrenceService.GetAsync(id);

            return occurrence.ToOk((result) => _entityMapper.Map<GetOccurrenceResponse, Occurrence>(result));
        }

        [Authorize]
        [HttpGet(Name = "SearchOccurrence")]
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

            var trafficFines = await _occurrenceService.GetByDateSearchAsync(search);

            return trafficFines.ToOk((result) => _entityMapper.Map<OffsetPageResponse<SearchOcurrenceResponse>, OffsetPage<Occurrence>>(result));
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(GetOccurrenceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateOccurrenceCommand createOccurrenceCommand
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trafficFine = _entityMapper.Map<Occurrence, CreateOccurrenceCommand>(createOccurrenceCommand);

            var result = await _occurrenceService.CreateAsync(trafficFine);

            return result.ToCreated();
        }

        [Authorize(Roles = RolesName.Cop)]
        [HttpDelete("{id:int}", Name = "DeleteOccurrence")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _occurrenceService.DeleteAsync(id);

            return result.ToNoContent();
        }

        [Authorize(Roles = RolesName.Cop)]
        [HttpPut("{id:int}/close", Name = "CloseOccurrence")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Close([FromRoute] int id)
        {
            var result = await _occurrenceService.UpdateStatus(new ClosedOcurrenceStatus(id));

            return result.ToNoContent();
        }


        [Authorize(Roles = RolesName.Cop)]
        [HttpPut("{id:int}/start", Name = "CloseOccurrence")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Start([FromRoute] int id)
        {
            var result = await _occurrenceService.UpdateStatus(new StartedOccurrenceStatus(id));

            return result.ToNoContent();
        }

        [Authorize]
        [HttpGet("properties")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOccurrencePropertiesAsync()
        {
            var result = await _occurrenceService.GetOccurrencePropertiesAsync();

            return result.ToOk();
        }
    }
}
