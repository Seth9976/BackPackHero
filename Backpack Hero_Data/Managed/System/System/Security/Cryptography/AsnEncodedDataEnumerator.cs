using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020002B4 RID: 692
	public sealed class AsnEncodedDataEnumerator : IEnumerator
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x000574E6 File Offset: 0x000556E6
		internal AsnEncodedDataEnumerator(AsnEncodedDataCollection asnEncodedDatas)
		{
			this._asnEncodedDatas = asnEncodedDatas;
			this._current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in the collection.</returns>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x000574FC File Offset: 0x000556FC
		public AsnEncodedData Current
		{
			get
			{
				return this._asnEncodedDatas[this._current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x000574FC File Offset: 0x000556FC
		object IEnumerator.Current
		{
			get
			{
				return this._asnEncodedDatas[this._current];
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015C0 RID: 5568 RVA: 0x0005750F File Offset: 0x0005570F
		public bool MoveNext()
		{
			if (this._current >= this._asnEncodedDatas.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015C1 RID: 5569 RVA: 0x00057537 File Offset: 0x00055737
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00013B26 File Offset: 0x00011D26
		internal AsnEncodedDataEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C2F RID: 3119
		private readonly AsnEncodedDataCollection _asnEncodedDatas;

		// Token: 0x04000C30 RID: 3120
		private int _current;
	}
}
