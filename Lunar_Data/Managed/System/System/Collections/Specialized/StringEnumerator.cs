using System;
using Unity;

namespace System.Collections.Specialized
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
	// Token: 0x020007C6 RID: 1990
	public class StringEnumerator
	{
		// Token: 0x06003F55 RID: 16213 RVA: 0x000DDF97 File Offset: 0x000DC197
		internal StringEnumerator(StringCollection mappings)
		{
			this._temp = mappings;
			this._baseEnumerator = this._temp.GetEnumerator();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x000DDFB7 File Offset: 0x000DC1B7
		public string Current
		{
			get
			{
				return (string)this._baseEnumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06003F57 RID: 16215 RVA: 0x000DDFC9 File Offset: 0x000DC1C9
		public bool MoveNext()
		{
			return this._baseEnumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06003F58 RID: 16216 RVA: 0x000DDFD6 File Offset: 0x000DC1D6
		public void Reset()
		{
			this._baseEnumerator.Reset();
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x00013B26 File Offset: 0x00011D26
		internal StringEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400267D RID: 9853
		private IEnumerator _baseEnumerator;

		// Token: 0x0400267E RID: 9854
		private IEnumerable _temp;
	}
}
