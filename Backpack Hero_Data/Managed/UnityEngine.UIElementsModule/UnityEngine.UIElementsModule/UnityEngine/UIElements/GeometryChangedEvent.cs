using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EB RID: 491
	public class GeometryChangedEvent : EventBase<GeometryChangedEvent>
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x0003CB7C File Offset: 0x0003AD7C
		public static GeometryChangedEvent GetPooled(Rect oldRect, Rect newRect)
		{
			GeometryChangedEvent pooled = EventBase<GeometryChangedEvent>.GetPooled();
			pooled.oldRect = oldRect;
			pooled.newRect = newRect;
			return pooled;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003CBA5 File Offset: 0x0003ADA5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003CBB6 File Offset: 0x0003ADB6
		private void LocalInit()
		{
			this.oldRect = Rect.zero;
			this.newRect = Rect.zero;
			this.layoutPass = 0;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0003CBD9 File Offset: 0x0003ADD9
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x0003CBE1 File Offset: 0x0003ADE1
		public Rect oldRect { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0003CBEA File Offset: 0x0003ADEA
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x0003CBF2 File Offset: 0x0003ADF2
		public Rect newRect { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0003CBFB File Offset: 0x0003ADFB
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0003CC03 File Offset: 0x0003AE03
		internal int layoutPass { get; set; }

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		public GeometryChangedEvent()
		{
			this.LocalInit();
		}
	}
}
