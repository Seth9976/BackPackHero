using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000520 RID: 1312
	internal interface INetworkChange : IDisposable
	{
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002A3E RID: 10814
		// (remove) Token: 0x06002A3F RID: 10815
		event NetworkAddressChangedEventHandler NetworkAddressChanged;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002A40 RID: 10816
		// (remove) Token: 0x06002A41 RID: 10817
		event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged;

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002A42 RID: 10818
		bool HasRegisteredEvents { get; }
	}
}
