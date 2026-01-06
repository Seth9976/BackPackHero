using System;
using System.Threading.Tasks;

namespace Unity.Services.Wire.Internal
{
	// Token: 0x02000003 RID: 3
	public interface IChannel : IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1
		// (remove) Token: 0x06000002 RID: 2
		event Action<string> MessageReceived;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3
		// (remove) Token: 0x06000004 RID: 4
		event Action<byte[]> BinaryMessageReceived;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000005 RID: 5
		// (remove) Token: 0x06000006 RID: 6
		event Action KickReceived;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000007 RID: 7
		// (remove) Token: 0x06000008 RID: 8
		event Action<SubscriptionState> NewStateReceived;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000009 RID: 9
		// (remove) Token: 0x0600000A RID: 10
		event Action<string> ErrorReceived;

		// Token: 0x0600000B RID: 11
		Task SubscribeAsync();

		// Token: 0x0600000C RID: 12
		Task UnsubscribeAsync();
	}
}
