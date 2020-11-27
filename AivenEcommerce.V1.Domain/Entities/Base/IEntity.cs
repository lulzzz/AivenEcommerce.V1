namespace AivenEcommerce.V1.Domain.Entities.Base
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
