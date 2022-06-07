using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace GraphDemo.Entities
{
    public class Artist : Vertex
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public override string ItemType => "Artist";

        public override string Id { get => $"{Name.Replace(" ", string.Empty)}|{Genre}"; }
    }
}