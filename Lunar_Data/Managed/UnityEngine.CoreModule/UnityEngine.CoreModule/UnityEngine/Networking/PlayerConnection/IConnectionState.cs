using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000385 RID: 901
	[MovedFrom("UnityEngine.Experimental.Networking.PlayerConnection")]
	public interface IConnectionState : IDisposable
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001EB8 RID: 7864
		ConnectionTarget connectedToTarget { get; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001EB9 RID: 7865
		string connectionName { get; }
	}
}
