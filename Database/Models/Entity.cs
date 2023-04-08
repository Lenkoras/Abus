namespace Database.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
