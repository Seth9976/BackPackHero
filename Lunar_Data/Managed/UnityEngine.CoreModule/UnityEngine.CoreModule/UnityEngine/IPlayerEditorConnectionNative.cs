using System;

namespace UnityEngine
{
	// Token: 0x020001CE RID: 462
	internal interface IPlayerEditorConnectionNative
	{
		// Token: 0x06001595 RID: 5525
		void Initialize();

		// Token: 0x06001596 RID: 5526
		void DisconnectAll();

		// Token: 0x06001597 RID: 5527
		void SendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x06001598 RID: 5528
		bool TrySendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x06001599 RID: 5529
		void Poll();

		// Token: 0x0600159A RID: 5530
		void RegisterInternal(Guid messageId);

		// Token: 0x0600159B RID: 5531
		void UnregisterInternal(Guid messageId);

		// Token: 0x0600159C RID: 5532
		bool IsConnected();
	}
}
