using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities
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
            base.Delete();
            return true;
        }

        public void PrepareToInsert()
        {
            Active = true;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
