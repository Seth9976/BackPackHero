using System;
using Unity;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x020002A3 RID: 675
	public class TokenBinding
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x000559C4 File Offset: 0x00053BC4
		internal TokenBinding(TokenBindingType bindingType, byte[] rawData)
		{
			this.BindingType = bindingType;
			this._rawTokenBindingId = rawData;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000559DA File Offset: 0x00053BDA
		public byte[] GetRawTokenBindingId()
		{
			if (this._rawTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._rawTokenBindingId.Clone();
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000559F6 File Offset: 0x00053BF6
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x000559FE File Offset: 0x00053BFE
		public TokenBindingType BindingType { get; private set; }

		// Token: 0x0600152B RID: 5419 RVA: 0x00013B26 File Offset: 0x00011D26
		internal TokenBinding()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000BF2 RID: 3058
		private byte[] _rawTokenBindingId;
	}
}
