namespace GraphDemo.Entities
{
    public abstract class Vertex
    {
        public Guid Id { get; init; }

        public abstract string partitionKey { get; }
    }
}