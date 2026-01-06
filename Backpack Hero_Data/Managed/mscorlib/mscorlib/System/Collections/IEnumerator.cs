using System;

namespace System.Collections
{
	/// <summary>Supports a simple iteration over a non-generic collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A17 RID: 2583
	public interface IEnumerator
	{
		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005B92 RID: 23442
		bool MoveNext();

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005B93 RID: 23443
		object Current { get; }

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005B94 RID: 23444
		void Reset();
	}
}
