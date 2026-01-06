using System;

namespace System.Collections.Generic
{
	/// <summary>Provides the base interface for the abstraction of sets.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x02000806 RID: 2054
	public interface ISet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		/// <summary>Adds an element to the current set and returns a value to indicate if the element was successfully added. </summary>
		/// <returns>true if the element is added to the set; false if the element is already in the set.</returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x060041DC RID: 16860
		bool Add(T item);

		/// <summary>Modifies the current set so that it contains all elements that are present in either the current set or the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041DD RID: 16861
		void UnionWith(IEnumerable<T> other);

		/// <summary>Modifies the current set so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041DE RID: 16862
		void IntersectWith(IEnumerable<T> other);

		/// <summary>Removes all elements in the specified collection from the current set.</summary>
		/// <param name="other">The collection of items to remove from the set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041DF RID: 16863
		void ExceptWith(IEnumerable<T> other);

		/// <summary>Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both. </summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041E0 RID: 16864
		void SymmetricExceptWith(IEnumerable<T> other);

		/// <summary>Determines whether a set is a subset of a specified collection.</summary>
		/// <returns>true if the current set is a subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041E1 RID: 16865
		bool IsSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a superset of a specified collection.</summary>
		/// <returns>true if the current set is a superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041E2 RID: 16866
		bool IsSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a proper (strict) superset of a specified collection.</summary>
		/// <returns>true if the current set is a proper superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041E3 RID: 16867
		bool IsProperSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a proper (strict) subset of a specified collection.</summary>
		/// <returns>true if the current set is a proper subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041E4 RID: 16868
		bool IsProperSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set overlaps with the specified collection.</summary>
		/// <returns>true if the current set and <paramref name="other" /> share at least one common element; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041E5 RID: 16869
		bool Overlaps(IEnumerable<T> other);

		/// <summary>Determines whether the current set and the specified collection contain the same elements.</summary>
		/// <returns>true if the current set is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060041E6 RID: 16870
		bool SetEquals(IEnumerable<T> other);
	}
}
