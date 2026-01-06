using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000432 RID: 1074
	[RequiredByNativeCode]
	public interface INotificationReceiver
	{
		// Token: 0x0600256A RID: 9578
		[RequiredByNativeCode]
		void OnNotify(Playable origin, INotification notification, object context);
	}
}
