namespace Gama.Application.UseCases.TrafficFineAgg.Responses;

public class GetTrafficViolationResponse
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public bool Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}