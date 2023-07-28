using Gama.Domain.Common;

namespace Gama.Domain.Entities.TrafficFinesAgg
{
    public class TrafficFineFile : AuditableEntity
    {
        public int Id { get; set; }

        public int TrafficFineId { get; set; }

        public string Path { get; set; }
    }
}
