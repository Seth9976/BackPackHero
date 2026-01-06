using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000032 RID: 50
	[Obsolete("IDataWatchHandle is no longer supported and will be removed soon", true)]
	internal interface IDataWatchHandle : IDisposable
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600014B RID: 331
		Object watched { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014C RID: 332
		bool disposed { get; }
	}
}
