using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities.OccurrencesAgg.Models
{
    public class ClosedOcurrenceStatus : OccurrenceStatus
    {
        public ClosedOcurrenceStatus(int id) : base(id)
        {
            Name = "Closed";
            Id = 2;
        }

        public override Result<bool> UpdateStatus(Occurrence occurrence)
        {
            if (occurrence.Status is not StartedOccurrenceStatus)
            {
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "Occurrence",
                    ErrorMessage = "Não é possível finalizar uma ocorrencia não iniciada"
                }));
            }

            occurrence.OccurrenceStatusId = Id;
            occurrence.Status = this;
            return true;
        }
    }
}
