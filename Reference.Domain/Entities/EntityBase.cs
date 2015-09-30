using Reference.Common.Contracts.Data;

namespace Reference.Domain.Entities
{
    public abstract class EntityBase : IVersionedEntityWithId
    {
        public int Id { get; set; }
        public byte[] Version { get; set; }
    }
}