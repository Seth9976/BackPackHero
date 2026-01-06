using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000436 RID: 1078
	public class Notification : INotification
	{
		// Token: 0x06002575 RID: 9589 RVA: 0x0003F235 File Offset: 0x0003D435
		public Notification(string name)
		{
			this.id = new PropertyName(name);
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x0003F24B File Offset: 0x0003D44B
		public PropertyName id { get; }
	}
}
