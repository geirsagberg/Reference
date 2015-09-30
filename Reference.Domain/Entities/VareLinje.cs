namespace Reference.Domain.Entities
{
    public class VareLinje : EntityBase
    {
        public int Antall { get; set; }
        public virtual Vare Vare { get; set; }
    }
}