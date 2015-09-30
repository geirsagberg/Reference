namespace Reference.Common.Contracts.Data
{
    /// <summary>
    /// For objects with a version property.
    /// </summary>
    public interface IVersioned
    {
        byte[] Version { get; set; }
    }
}