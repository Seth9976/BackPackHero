using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039C RID: 924
	[Obsolete("GPUFence has been deprecated. Use GraphicsFence instead (UnityUpgradable) -> GraphicsFence", false)]
	public struct GPUFence
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x00032F20 File Offset: 0x00031120
		public bool passed
		{
			get
			{
				return true;
			}
		}
	}
}
