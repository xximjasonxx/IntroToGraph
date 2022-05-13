using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace GraphDemo.Entities
{
    public class Artist : Vertex, IEqualityComparer<Artist>
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public override string partitionKey => Genre?.ToLower();

        public bool Equals(Artist? x, Artist? y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode([DisallowNull] Artist obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}