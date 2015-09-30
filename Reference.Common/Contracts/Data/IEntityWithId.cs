namespace Reference.Common.Contracts.Data
{
    /// <summary>
    /// For entites with <see cref="Id"/> as a primary key
    /// </summary>
    public interface IEntityWithId : IEntity
    {
        int Id { get; set; }
    }
}