namespace GatherApp.Contracts.Entities.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }

    }
}
