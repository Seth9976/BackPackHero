using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000029 RID: 41
	public interface IConnection<out TSource, out TDestination>
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600019C RID: 412
		TSource source { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019D RID: 413
		TDestination destination { get; }
	}
}
