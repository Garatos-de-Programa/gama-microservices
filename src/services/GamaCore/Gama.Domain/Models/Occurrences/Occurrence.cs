using Gama.Domain.Common;
using Gama.Domain.Exceptions;
using Gama.Domain.Models.Users;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Models.Occurrences
{
    public class Occurrence : AuditableEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Location { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public short OccurrenceStatusId { get; set; }

        public short OccurrenceTypeId { get; set; }

        public short OccurrenceUrgencyLevelId { get; set; }

        public bool Active { get; set; }

        public OccurrenceStatus? Status { get; set; }

        public OccurrenceType? OccurrenceType { get; set; }

        public OccurrenceUrgencyLevel? OccurrenceUrgencyLevel { get; set; }

        public Result<bool> Delete(User user)
        {
            if (user.IsDiferentUser(UserId))
            {
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "Occurrence",
                    ErrorMessage = "Operação invalida"
                }));
            }

            Active = false;
            AddEvent(new DeletedOccurrenceEvent(this));
            base.Delete();
            return true;
        }

        public void PrepareToInsert()
        {
            Active = true;
            CreatedAt = DateTime.UtcNow;
            AddEvent(new CreatedOccurrenceEvent(this, ""));
        }
    }
}
