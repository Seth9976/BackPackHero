using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000314 RID: 788
	internal abstract class BaseShaderInfoStorage : IDisposable
	{
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600199E RID: 6558
		public abstract Texture2D texture { get; }

		// Token: 0x0600199F RID: 6559
		public abstract bool AllocateRect(int width, int height, out RectInt uvs);

		// Token: 0x060019A0 RID: 6560
		public abstract void SetTexel(int x, int y, Color color);

		// Token: 0x060019A1 RID: 6561
		public abstract void UpdateTexture();

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0006A574 File Offset: 0x00068774
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x0006A57C File Offset: 0x0006877C
		private protected bool disposed { protected get; private set; }

		// Token: 0x060019A4 RID: 6564 RVA: 0x0006A585 File Offset: 0x00068785
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0006A598 File Offset: 0x00068798
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				bool flag = !disposing;
				if (flag)
				{
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000B90 RID: 2960
		protected static int s_TextureCounter;

		// Token: 0x04000B91 RID: 2961
		internal static ProfilerMarker s_MarkerCopyTexture = new ProfilerMarker("UIR.ShaderInfoStorage.CopyTexture");

		// Token: 0x04000B92 RID: 2962
		internal static ProfilerMarker s_MarkerGetTextureData = new ProfilerMarker("UIR.ShaderInfoStorage.GetTextureData");

		// Token: 0x04000B93 RID: 2963
		internal static ProfilerMarker s_MarkerUpdateTexture = new ProfilerMarker("UIR.ShaderInfoStorage.UpdateTexture");
	}
}
