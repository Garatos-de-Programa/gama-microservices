using Gama.Application.Contracts.Mappers;
using Gama.Application.Seedworks.Pagination;
using Gama.Application.Seedworks.Queries;
using Gama.Application.UseCases.TrafficFineAgg.Commands;
using Gama.Application.UseCases.TrafficFineAgg.Interfaces;
using Gama.Application.UseCases.TrafficFineAgg.Queries;
using Gama.Application.UseCases.TrafficFineAgg.Responses;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.ValueTypes;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gama.Api.Controllers;

[ApiController]
[Route("v1/traffic-fines")]
public class TrafficFineController : Controller
{
    private readonly ITrafficFineService _trafficFineService;
    private readonly IEntityMapper _entityMapper;

    public TrafficFineController(
        ITrafficFineService trafficFineService,
        IEntityMapper entityMapper
    )
    {
        _trafficFineService = trafficFineService;
        _entityMapper = entityMapper;
    }

    [HttpGet("{id:int}", Name = "GetTrafficFine")]
    [Authorize(Roles = $"{RolesName.Cop},{RolesName.Admin}")]
    [ProducesResponseType(typeof(GetTrafficFineResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var trafficFine = await _trafficFineService.GetAsync(id);

        return trafficFine.ToOk((result) => _entityMapper.Map<GetTrafficFineResponse, TrafficFine>(result));
    }

    [HttpGet(Name = "Search")]
    [Authorize(Roles = $"{RolesName.Cop},{RolesName.Admin}")]
    [ProducesResponseType(typeof(IEnumerable<GetTrafficFineResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SearchAsync([FromQuery] TrafficFineListingQuery search)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trafficFines = await _trafficFineService.GetByDateSearchAsync(search);

        return trafficFines.ToOk((result) => _entityMapper.Map<OffsetPageResponse<GetTrafficFineResponse>, OffsetPage<TrafficFine>>(result));
    }

    [HttpPost]
    [Authorize(Roles = RolesName.Cop)]
    [ProducesResponseType(typeof(GetTrafficFineResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateTrafficFineCommand createTrafficFineCommand
        )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trafficFine = _entityMapper.Map<TrafficFine, CreateTrafficFineCommand>(createTrafficFineCommand);

        var result = await _trafficFineService.CreateAsync(trafficFine);

        return result.ToCreated();
    }

    [HttpPost("{id:int}/compute")]
    [Authorize(Roles = RolesName.Admin)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ComputeAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _trafficFineService.ComputeAsync(id);

        return result.ToNoContent();
    }

    [Authorize(Roles = RolesName.Cop)]
    [HttpDelete("{id:int}", Name = "DeleteTrafficFine")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var result = await _trafficFineService.DeleteAsync(id);

        return result.ToNoContent();
    }
}