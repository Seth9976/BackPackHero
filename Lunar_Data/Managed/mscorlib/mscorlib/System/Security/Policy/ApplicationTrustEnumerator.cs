using System;
using System.Collections;
using System.Runtime.InteropServices;
using Unity;

namespace System.Security.Policy
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Policy.ApplicationTrust" /> objects in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
	// Token: 0x02000405 RID: 1029
	[ComVisible(true)]
	public sealed class ApplicationTrustEnumerator : IEnumerator
	{
		// Token: 0x06002A15 RID: 10773 RVA: 0x0009890D File Offset: 0x00096B0D
		internal ApplicationTrustEnumerator(ApplicationTrustCollection atc)
		{
			this.trusts = atc;
			this.current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Policy.ApplicationTrust" /> object in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Policy.ApplicationTrust" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x00098923 File Offset: 0x00096B23
		public ApplicationTrust Current
		{
			get
			{
				return this.trusts[this.current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x00098923 File Offset: 0x00096B23
		object IEnumerator.Current
		{
			get
			{
				return this.trusts[this.current];
			}
		}

		/// <summary>Resets the enumerator to the beginning of the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		// Token: 0x06002A18 RID: 10776 RVA: 0x00098936 File Offset: 0x00096B36
		public void Reset()
		{
			this.current = -1;
		}

		/// <summary>Moves to the next element in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002A19 RID: 10777 RVA: 0x0009893F File Offset: 0x00096B3F
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			if (this.current == this.trusts.Count - 1)
			{
				return false;
			}
			this.current++;
			return true;
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000173AD File Offset: 0x000155AD
		internal ApplicationTrustEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F62 RID: 8034
		private ApplicationTrustCollection trusts;

		// Token: 0x04001F63 RID: 8035
		private int current;
	}
}
