using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities.OccurrencesAgg.Models
{
    public class StartedOccurrenceStatus : OccurrenceStatus
    {
        public const string Status = "Started";
        public StartedOccurrenceStatus(int id) : base(id)
        {
            Name = Status;
            Id = 3;
        }

        public override Result<bool> UpdateStatus(Occurrence occurrence)
        {
            if (occurrence.Status?.Name?.Equals(ClosedOcurrenceStatus.Status) ?? false)
            {
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "Occurrence",
                    ErrorMessage = "Não é possível iniciar uma ocorrencia já finalizada"
                }));
            }

            occurrence.OccurrenceStatusId = Id;
            occurrence.Status = this;
            return true;
        }
    }
}
