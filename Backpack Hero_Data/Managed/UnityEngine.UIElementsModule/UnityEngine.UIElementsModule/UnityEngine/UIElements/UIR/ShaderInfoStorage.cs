using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000315 RID: 789
	internal class ShaderInfoStorage<T> : BaseShaderInfoStorage where T : struct
	{
		// Token: 0x060019A8 RID: 6568 RVA: 0x0006A5F4 File Offset: 0x000687F4
		public ShaderInfoStorage(TextureFormat format, Func<Color, T> convert, int initialSize = 64, int maxSize = 4096)
		{
			Debug.Assert(maxSize <= SystemInfo.maxTextureSize);
			Debug.Assert(initialSize <= maxSize);
			Debug.Assert(Mathf.IsPowerOfTwo(initialSize));
			Debug.Assert(Mathf.IsPowerOfTwo(maxSize));
			Debug.Assert(convert != null);
			this.m_InitialSize = initialSize;
			this.m_MaxSize = maxSize;
			this.m_Format = format;
			this.m_Convert = convert;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0006A66C File Offset: 0x0006886C
		protected override void Dispose(bool disposing)
		{
			bool flag = !base.disposed && disposing;
			if (flag)
			{
				UIRUtility.Destroy(this.m_Texture);
				this.m_Texture = null;
				this.m_Texels = default(NativeArray<T>);
				UIRAtlasAllocator allocator = this.m_Allocator;
				if (allocator != null)
				{
					allocator.Dispose();
				}
				this.m_Allocator = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0006A6CB File Offset: 0x000688CB
		public override Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0006A6D4 File Offset: 0x000688D4
		public override bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool disposed = base.disposed;
			bool flag;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				uvs = default(RectInt);
				flag = false;
			}
			else
			{
				bool flag2 = this.m_Allocator == null;
				if (flag2)
				{
					this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, this.m_MaxSize, 0);
				}
				bool flag3 = !this.m_Allocator.TryAllocate(width, height, out uvs);
				if (flag3)
				{
					flag = false;
				}
				else
				{
					uvs = new RectInt(uvs.x, uvs.y, width, height);
					this.CreateOrExpandTexture();
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0006A764 File Offset: 0x00068964
		public override void SetTexel(int x, int y, Color color)
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.m_Texels.IsCreated;
				if (flag)
				{
					BaseShaderInfoStorage.s_MarkerGetTextureData.Begin();
					this.m_Texels = this.m_Texture.GetRawTextureData<T>();
					BaseShaderInfoStorage.s_MarkerGetTextureData.End();
				}
				this.m_Texels[x + y * this.m_Texture.width] = this.m_Convert.Invoke(color);
			}
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0006A7E8 File Offset: 0x000689E8
		public override void UpdateTexture()
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_Texture == null || !this.m_Texels.IsCreated;
				if (!flag)
				{
					BaseShaderInfoStorage.s_MarkerUpdateTexture.Begin();
					this.m_Texture.Apply(false, false);
					this.m_Texels = default(NativeArray<T>);
					BaseShaderInfoStorage.s_MarkerUpdateTexture.End();
				}
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0006A860 File Offset: 0x00068A60
		private void CreateOrExpandTexture()
		{
			int physicalWidth = this.m_Allocator.physicalWidth;
			int physicalHeight = this.m_Allocator.physicalHeight;
			bool flag = false;
			bool flag2 = this.m_Texture != null;
			if (flag2)
			{
				bool flag3 = this.m_Texture.width == physicalWidth && this.m_Texture.height == physicalHeight;
				if (flag3)
				{
					return;
				}
				flag = true;
			}
			Texture2D texture2D = new Texture2D(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, this.m_Format, false)
			{
				name = "UIR Shader Info " + BaseShaderInfoStorage.s_TextureCounter++.ToString(),
				hideFlags = HideFlags.HideAndDontSave
			};
			bool flag4 = flag;
			if (flag4)
			{
				BaseShaderInfoStorage.s_MarkerCopyTexture.Begin();
				NativeArray<T> nativeArray = (this.m_Texels.IsCreated ? this.m_Texels : this.m_Texture.GetRawTextureData<T>());
				NativeArray<T> rawTextureData = texture2D.GetRawTextureData<T>();
				ShaderInfoStorage<T>.CpuBlit(nativeArray, this.m_Texture.width, this.m_Texture.height, rawTextureData, texture2D.width, texture2D.height);
				this.m_Texels = rawTextureData;
				BaseShaderInfoStorage.s_MarkerCopyTexture.End();
			}
			else
			{
				this.m_Texels = default(NativeArray<T>);
			}
			UIRUtility.Destroy(this.m_Texture);
			this.m_Texture = texture2D;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0006A9BC File Offset: 0x00068BBC
		private static void CpuBlit(NativeArray<T> src, int srcWidth, int srcHeight, NativeArray<T> dst, int dstWidth, int dstHeight)
		{
			Debug.Assert(dstWidth >= srcWidth && dstHeight >= srcHeight);
			int num = dstWidth - srcWidth;
			int num2 = dstHeight - srcHeight;
			int num3 = srcWidth * srcHeight;
			int i = 0;
			int num4 = 0;
			int num5 = srcWidth;
			while (i < num3)
			{
				while (i < num5)
				{
					dst[num4] = src[i];
					num4++;
					i++;
				}
				num5 += srcWidth;
				num4 += num;
			}
		}

		// Token: 0x04000B95 RID: 2965
		private readonly int m_InitialSize;

		// Token: 0x04000B96 RID: 2966
		private readonly int m_MaxSize;

		// Token: 0x04000B97 RID: 2967
		private readonly TextureFormat m_Format;

		// Token: 0x04000B98 RID: 2968
		private readonly Func<Color, T> m_Convert;

		// Token: 0x04000B99 RID: 2969
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x04000B9A RID: 2970
		private Texture2D m_Texture;

		// Token: 0x04000B9B RID: 2971
		private NativeArray<T> m_Texels;
	}
}
