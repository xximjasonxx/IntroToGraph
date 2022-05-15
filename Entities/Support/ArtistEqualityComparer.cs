using System;
using System.Diagnostics.CodeAnalysis;

namespace GraphDemo.Entities.Support
{
	public class ArtistEqualityComparer : IEqualityComparer<Artist>
	{
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

