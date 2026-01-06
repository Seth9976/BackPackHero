using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a collection of header values.</summary>
	/// <typeparam name="T">The header collection type.</typeparam>
	// Token: 0x02000043 RID: 67
	public sealed class HttpHeaderValueCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00008DCC File Offset: 0x00006FCC
		internal HttpHeaderValueCollection(HttpHeaders headers, HeaderInfo headerInfo)
		{
			this.list = new List<T>();
			this.headers = headers;
			this.headerInfo = headerInfo;
		}

		/// <summary>Gets the number of headers in the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The number of headers in a collection</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00008DED File Offset: 0x00006FED
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008DFA File Offset: 0x00006FFA
		internal List<string> InvalidValues
		{
			get
			{
				return this.invalidValues;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance is read-only.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance is read-only; otherwise, false.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008E02 File Offset: 0x00007002
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an entry to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <param name="item">The item to add to the header collection.</param>
		// Token: 0x0600024B RID: 587 RVA: 0x00008E05 File Offset: 0x00007005
		public void Add(T item)
		{
			this.list.Add(item);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008E13 File Offset: 0x00007013
		internal void AddRange(List<T> values)
		{
			this.list.AddRange(values);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008E21 File Offset: 0x00007021
		internal void AddInvalidValue(string invalidValue)
		{
			if (this.invalidValues == null)
			{
				this.invalidValues = new List<string>();
			}
			this.invalidValues.Add(invalidValue);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		// Token: 0x0600024E RID: 590 RVA: 0x00008E42 File Offset: 0x00007042
		public void Clear()
		{
			this.list.Clear();
			this.invalidValues = null;
		}

		/// <summary>Determines if the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> contains an item.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the entry is contained in the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance; otherwise, false</returns>
		/// <param name="item">The item to find to the header collection.</param>
		// Token: 0x0600024F RID: 591 RVA: 0x00008E56 File Offset: 0x00007056
		public bool Contains(T item)
		{
			return this.list.Contains(item);
		}

		/// <summary>Copies the entire <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000250 RID: 592 RVA: 0x00008E64 File Offset: 0x00007064
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		/// <summary>Parses and adds an entry to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <param name="input">The entry to add.</param>
		// Token: 0x06000251 RID: 593 RVA: 0x00008E73 File Offset: 0x00007073
		public void ParseAdd(string input)
		{
			this.headers.AddValue(input, this.headerInfo, false);
		}

		/// <summary>Removes the specified item from the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <paramref name="item" /> was removed from the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance; otherwise, false</returns>
		/// <param name="item">The item to remove.</param>
		// Token: 0x06000252 RID: 594 RVA: 0x00008E89 File Offset: 0x00007089
		public bool Remove(T item)
		{
			return this.list.Remove(item);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> object. object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x06000253 RID: 595 RVA: 0x00008E98 File Offset: 0x00007098
		public override string ToString()
		{
			string text = string.Join<T>(this.headerInfo.Separator, this.list);
			if (this.invalidValues != null)
			{
				text += string.Join(this.headerInfo.Separator, this.invalidValues);
			}
			return text;
		}

		/// <summary>Determines whether the input could be parsed and added to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <paramref name="input" /> could be parsed and added to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance; otherwise, false</returns>
		/// <param name="input">The entry to validate.</param>
		// Token: 0x06000254 RID: 596 RVA: 0x00008EE2 File Offset: 0x000070E2
		public bool TryParseAdd(string input)
		{
			return this.headers.AddValue(input, this.headerInfo, true);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerator`1" />.An enumerator for the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance.</returns>
		// Token: 0x06000255 RID: 597 RVA: 0x00008EF7 File Offset: 0x000070F7
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>Returns <see cref="T:System.Collections.IEnumerator" />.An enumerator for the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance.</returns>
		// Token: 0x06000256 RID: 598 RVA: 0x00008F09 File Offset: 0x00007109
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008F11 File Offset: 0x00007111
		internal T Find(Predicate<T> predicate)
		{
			return this.list.Find(predicate);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008F20 File Offset: 0x00007120
		internal void Remove(Predicate<T> predicate)
		{
			T t = this.Find(predicate);
			if (t != null)
			{
				this.Remove(t);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008DC5 File Offset: 0x00006FC5
		internal HttpHeaderValueCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000102 RID: 258
		private readonly List<T> list;

		// Token: 0x04000103 RID: 259
		private readonly HttpHeaders headers;

		// Token: 0x04000104 RID: 260
		private readonly HeaderInfo headerInfo;

		// Token: 0x04000105 RID: 261
		private List<string> invalidValues;
	}
}
