using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200013A RID: 314
	internal readonly struct RenderInstancedDataLayout
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x0000F084 File Offset: 0x0000D284
		public RenderInstancedDataLayout(Type t)
		{
			this.size = Marshal.SizeOf(t);
			this.offsetObjectToWorld = ((t == typeof(Matrix4x4)) ? 0 : Marshal.OffsetOf(t, "objectToWorld").ToInt32());
			try
			{
				this.offsetPrevObjectToWorld = Marshal.OffsetOf(t, "prevObjectToWorld").ToInt32();
			}
			catch (ArgumentException)
			{
				this.offsetPrevObjectToWorld = -1;
			}
			try
			{
				this.offsetRenderingLayerMask = Marshal.OffsetOf(t, "renderingLayerMask").ToInt32();
			}
			catch (ArgumentException)
			{
				this.offsetRenderingLayerMask = -1;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0000F138 File Offset: 0x0000D338
		public int size { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0000F140 File Offset: 0x0000D340
		public int offsetObjectToWorld { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0000F148 File Offset: 0x0000D348
		public int offsetPrevObjectToWorld { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0000F150 File Offset: 0x0000D350
		public int offsetRenderingLayerMask { get; }
	}
}
