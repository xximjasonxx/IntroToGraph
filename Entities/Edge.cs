namespace GraphDemo.Entities
{
    public abstract class Edge
    {
        public Guid Id { get; init; }

        public abstract string FromId { get; }

        public abstract string ToId { get; }
    }
}