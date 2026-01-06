using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000033 RID: 51
	[Obsolete("IDataWatchService is no longer supported and will be removed soon", true)]
	internal interface IDataWatchService
	{
		// Token: 0x0600014D RID: 333
		IDataWatchHandle AddWatch(Object watched, Action<Object> onDataChanged);

		// Token: 0x0600014E RID: 334
		void RemoveWatch(IDataWatchHandle handle);

		// Token: 0x0600014F RID: 335
		void ForceDirtyNextPoll(Object obj);
	}
}
