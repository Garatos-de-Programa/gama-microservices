using System.Net;
using Gama.Application.Contracts.Mappers;
using Gama.Application.Contracts.TrafficFineManagement;
using Gama.Application.DataContracts.Commands.TrafficFineManagement;
using Gama.Application.DataContracts.Responses.TrafficManagement;
using Gama.Domain.Entities;
using Gama.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Api.Controllers;

[ApiController]
[Route("v1/traffic-violation")]
public class TrafficViolationController : Controller
{
    private readonly ITrafficViolationService _trafficViolationService;
    private readonly IEntityMapper _entityMapper;

    public TrafficViolationController(
        ITrafficViolationService trafficViolationService, 
        IEntityMapper entityMapper
        )
    {
        _trafficViolationService = trafficViolationService;
        _entityMapper = entityMapper;
    }

    [HttpGet("{id:int}", Name = "GetTrafficViolation")]
    [ProducesResponseType(typeof(GetTrafficViolationResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get(short id)
    {
        var result = await _trafficViolationService.GetAsync(id);

        return result.ToOk((trafficViolation) => _entityMapper.Map<GetTrafficViolationResponse, TrafficViolation>(trafficViolation));
    }

    [HttpGet("", Name = "GetTrafficsViolations")]
    [ProducesResponseType(typeof(IEnumerable<GetTrafficViolationResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTrafficsViolations()
    {
        var result = await _trafficViolationService.GetTrafficsViolationsAsync();

        return result.ToOk((trafficViolation) => _entityMapper.Map<IEnumerable<GetTrafficViolationResponse>, IEnumerable<TrafficViolation>>(trafficViolation));
    }

    [HttpPost()]
    [ProducesResponseType(typeof(GetTrafficViolationResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateTrafficViolationCommand createTrafficViolationCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var trafficViolation = _entityMapper.Map<TrafficViolation, CreateTrafficViolationCommand>(createTrafficViolationCommand);

        var result = await _trafficViolationService.CreateAsync(trafficViolation);

        return result.ToOk((trafficViolation) => _entityMapper.Map<GetTrafficViolationResponse, TrafficViolation>(trafficViolation));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GetTrafficViolationResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateTrafficViolationCommand updateTrafficViolationCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trafficViolation = _entityMapper.Map<TrafficViolation, UpdateTrafficViolationCommand>(updateTrafficViolationCommand);
        
        var result = await _trafficViolationService.UpdateAsync(trafficViolation).ConfigureAwait(false);

        return result.ToOk((result) => _entityMapper.Map<GetTrafficViolationResponse, TrafficViolation>(result));
    }

    [HttpDelete("{id:int}", Name = "DeleteTrafficViolation")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete(short id)
    {
        var result = await _trafficViolationService.DeleteAsync(id);
        return result.ToNoContent();
    }
}