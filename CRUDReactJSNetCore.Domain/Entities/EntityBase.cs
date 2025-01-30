namespace CRUDReactJSNetCore.Domain.Entities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public bool Active { get; set; } = true;
    }
}
