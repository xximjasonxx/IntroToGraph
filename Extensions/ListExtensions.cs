using System;
namespace GraphDemo.Extensions
{
	public static class ListExtensions
	{
		public static IEnumerable<T> Filter<T>(this IList<T> sourceList, IList<T> filterList) where T : IEqualityComparer<T>
        {
			foreach (var sourceItem in sourceList)
            {
				if (filterList.Any(x => x.Equals(sourceItem)) == false)
					yield return sourceItem;
            }
        }
	}
}

