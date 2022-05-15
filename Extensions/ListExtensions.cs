using System;
using System.Diagnostics.CodeAnalysis;
using GraphDemo.Entities;

namespace GraphDemo.Extensions
{
	public static class ListExtensions
	{
		public static IList<T> Filter<T>(this IList<T> sourceList, IList<T> filterList, IEqualityComparer<T> comparer)
        {
			return sourceList
				.Where(srcItem => filterList.Contains(srcItem, comparer) == false)
				.ToList();
        }
	}
}

