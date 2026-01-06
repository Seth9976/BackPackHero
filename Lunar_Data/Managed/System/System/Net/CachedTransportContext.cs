using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000414 RID: 1044
	internal class CachedTransportContext : TransportContext
	{
		// Token: 0x0600211D RID: 8477 RVA: 0x00078B11 File Offset: 0x00076D11
		internal CachedTransportContext(ChannelBinding binding)
		{
			this.binding = binding;
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x00078B20 File Offset: 0x00076D20
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind != ChannelBindingKind.Endpoint)
			{
				return null;
			}
			return this.binding;
		}

		// Token: 0x04001308 RID: 4872
		private ChannelBinding binding;
	}
}
