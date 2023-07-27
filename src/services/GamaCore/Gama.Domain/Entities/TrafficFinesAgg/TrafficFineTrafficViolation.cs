namespace Gama.Domain.Models.TrafficFines
{
    public class TrafficFineTrafficViolation
    {
        public int TrafficFineId { get; set; }

        public int TrafficViolationId { get; set; }

        public TrafficFine? TrafficFine { get; set; }

        public TrafficViolation? TrafficViolation { get; set; }
    }
}
