using Newtonsoft.Json;

namespace GraphDemo.Entities
{
    public class Artist : Vertex
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public override string partitionKey => Genre?.ToLower();

        public override string label => $"{Name.Replace(" ", string.Empty)}";
    }
}