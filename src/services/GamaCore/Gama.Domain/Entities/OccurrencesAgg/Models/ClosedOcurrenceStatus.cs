using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities.OccurrencesAgg.Models
{
    public class ClosedOcurrenceStatus : OccurrenceStatus
    {
        public const string Status = "Atendimento concluído";
        public ClosedOcurrenceStatus(int id) : base(id)
        {
            Name = Status;
            Id = 2;
        }

        public override Result<bool> UpdateStatus(Occurrence occurrence)
        {
            if (!occurrence.Status!.Name!.Equals(StartedOccurrenceStatus.Status))
            {
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "Occurrence",
                    ErrorMessage = "Não é possível finalizar uma ocorrencia não iniciada"
                }));
            }

            occurrence.OccurrenceStatusId = Id;
            occurrence.Status = this;
            occurrence.Active = false;
            return true;
        }
    }
}
