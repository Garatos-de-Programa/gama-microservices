using System.Net;
using Gama.Application.Contracts.Mappers;
using Gama.Application.Contracts.TrafficFineManagement;
using Gama.Application.DataContracts.Commands.TrafficFineManagement;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Application.DataContracts.Responses.TrafficManagement;
using Gama.Domain.Entities;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[ApiController]
[Route("v1/traffic-fine")]
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

    [HttpGet("{id:long}", Name = "GetTrafficFine")]
    [ProducesResponseType(typeof(GetTrafficFineResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAsync(long id)
    {
        var trafficFine = await _trafficFineService.GetAsync(id).ConfigureAwait(false);

        return trafficFine.ToOk((result) => _entityMapper.Map<GetTrafficFineResponse, TrafficFine>(result));
    }

    [HttpGet(Name = "Search")]
    [ProducesResponseType(typeof(IEnumerable<GetTrafficFineResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SearchAsync([FromQuery] DateSearchQuery search)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trafficFines = await _trafficFineService.GetByDateSearchAsync(search).ConfigureAwait(false);

        return trafficFines.ToOk((result) => _entityMapper.Map<IEnumerable<GetTrafficFineResponse>, IEnumerable<TrafficFine>>(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetTrafficFineResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTrafficFineCommand createTrafficFineCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trafficFine = _entityMapper.Map<TrafficFine, CreateTrafficFineCommand>(createTrafficFineCommand);

        var result = await _trafficFineService.CreateAsync(trafficFine).ConfigureAwait(false);

        return result.ToOk((result) => _entityMapper.Map<GetTrafficFineResponse, TrafficFine>(result));
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(GetTrafficFineResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTrafficFineCommand updateTrafficFineCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trafficFine = _entityMapper.Map<TrafficFine, UpdateTrafficFineCommand>(updateTrafficFineCommand);
        
        var result = await _trafficFineService.UpdateAsync(trafficFine).ConfigureAwait(false);

        return result.ToOk((result) => _entityMapper.Map<GetTrafficFineResponse, TrafficFine>(result));
    }

    [HttpDelete("{id:long}", Name = "DeleteTrafficFine")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _trafficFineService.DeleteAsync(id);

        return result.ToNoContent();
    }
}