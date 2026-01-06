using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002B RID: 43
	internal struct LayerBatch
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public unsafe void InitRTIds(int index)
		{
			for (int i = 0; i < 4; i++)
			{
				*((ref this.renderTargetUsed.FixedElementField) + i) = false;
				*((ref this.renderTargetIds.FixedElementField) + (IntPtr)i * 4) = Shader.PropertyToID(string.Format("_LightTexture_{0}_{1}", index, i));
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000E508 File Offset: 0x0000C708
		public unsafe RenderTargetIdentifier GetRTId(CommandBuffer cmd, RenderTextureDescriptor desc, int index)
		{
			if (!(*((ref this.renderTargetUsed.FixedElementField) + index)))
			{
				cmd.GetTemporaryRT(*((ref this.renderTargetIds.FixedElementField) + (IntPtr)index * 4), desc, FilterMode.Bilinear);
				*((ref this.renderTargetUsed.FixedElementField) + index) = true;
			}
			return new RenderTargetIdentifier(*((ref this.renderTargetIds.FixedElementField) + (IntPtr)index * 4));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000E564 File Offset: 0x0000C764
		public unsafe void ReleaseRT(CommandBuffer cmd)
		{
			for (int i = 0; i < 4; i++)
			{
				if (*((ref this.renderTargetUsed.FixedElementField) + i))
				{
					cmd.ReleaseTemporaryRT(*((ref this.renderTargetIds.FixedElementField) + (IntPtr)i * 4));
					*((ref this.renderTargetUsed.FixedElementField) + i) = false;
				}
			}
		}

		// Token: 0x040000F0 RID: 240
		public int startLayerID;

		// Token: 0x040000F1 RID: 241
		public int endLayerValue;

		// Token: 0x040000F2 RID: 242
		public SortingLayerRange layerRange;

		// Token: 0x040000F3 RID: 243
		public LightStats lightStats;

		// Token: 0x040000F4 RID: 244
		[FixedBuffer(typeof(int), 4)]
		private LayerBatch.<renderTargetIds>e__FixedBuffer renderTargetIds;

		// Token: 0x040000F5 RID: 245
		[FixedBuffer(typeof(bool), 4)]
		private LayerBatch.<renderTargetUsed>e__FixedBuffer renderTargetUsed;

		// Token: 0x02000147 RID: 327
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <renderTargetIds>e__FixedBuffer
		{
			// Token: 0x040008BA RID: 2234
			public int FixedElementField;
		}

		// Token: 0x02000148 RID: 328
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 4)]
		public struct <renderTargetUsed>e__FixedBuffer
		{
			// Token: 0x040008BB RID: 2235
			public bool FixedElementField;
		}
	}
}
