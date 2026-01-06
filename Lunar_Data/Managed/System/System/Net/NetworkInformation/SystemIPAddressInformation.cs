using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000513 RID: 1299
	internal class SystemIPAddressInformation : IPAddressInformation
	{
		// Token: 0x060029F7 RID: 10743 RVA: 0x0009A168 File Offset: 0x00098368
		public SystemIPAddressInformation(IPAddress address, bool isDnsEligible, bool isTransient)
		{
			this.address = address;
			this.dnsEligible = isDnsEligible;
			this.transient = isTransient;
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x0009A18C File Offset: 0x0009838C
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x0009A194 File Offset: 0x00098394
		public override bool IsTransient
		{
			get
			{
				return this.transient;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x0009A19C File Offset: 0x0009839C
		public override bool IsDnsEligible
		{
			get
			{
				return this.dnsEligible;
			}
		}

		// Token: 0x04001896 RID: 6294
		private IPAddress address;

		// Token: 0x04001897 RID: 6295
		internal bool transient;

		// Token: 0x04001898 RID: 6296
		internal bool dnsEligible = true;
	}
}
