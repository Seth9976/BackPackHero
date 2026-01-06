using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.OidCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020002B7 RID: 695
	public sealed class OidEnumerator : IEnumerator
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x0005784A File Offset: 0x00055A4A
		internal OidEnumerator(OidCollection oids)
		{
			this._oids = oids;
			this._current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</returns>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00057860 File Offset: 0x00055A60
		public Oid Current
		{
			get
			{
				return this._oids[this._current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00057873 File Offset: 0x00055A73
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015DC RID: 5596 RVA: 0x0005787B File Offset: 0x00055A7B
		public bool MoveNext()
		{
			if (this._current >= this._oids.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015DD RID: 5597 RVA: 0x000578A3 File Offset: 0x00055AA3
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00013B26 File Offset: 0x00011D26
		internal OidEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C35 RID: 3125
		private readonly OidCollection _oids;

		// Token: 0x04000C36 RID: 3126
		private int _current;
	}
}
