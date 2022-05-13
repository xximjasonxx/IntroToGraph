namespace GraphDemo.Entities
{
    public abstract class Edge
    {
        public Guid Id { get; init; }

        public abstract Guid FromId { get; }

        public abstract Guid ToId { get; }
    }
}