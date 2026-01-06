using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000253 RID: 595
	internal class DynamicAtlasCore : IDisposable
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00044EAD File Offset: 0x000430AD
		public int maxImageSize { get; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00044EB5 File Offset: 0x000430B5
		public RenderTextureFormat format { get; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00044EBD File Offset: 0x000430BD
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x00044EC5 File Offset: 0x000430C5
		public RenderTexture atlas { get; private set; }

		// Token: 0x060011E0 RID: 4576 RVA: 0x00044ED0 File Offset: 0x000430D0
		public DynamicAtlasCore(RenderTextureFormat format = RenderTextureFormat.ARGB32, FilterMode filterMode = FilterMode.Bilinear, int maxImageSize = 64, int initialSize = 64, int maxAtlasSize = 4096)
		{
			Debug.Assert(filterMode == FilterMode.Bilinear || filterMode == FilterMode.Point);
			Debug.Assert(maxAtlasSize <= SystemInfo.maxRenderTextureSize);
			Debug.Assert(initialSize <= maxAtlasSize);
			Debug.Assert(Mathf.IsPowerOfTwo(maxImageSize));
			Debug.Assert(Mathf.IsPowerOfTwo(initialSize));
			Debug.Assert(Mathf.IsPowerOfTwo(maxAtlasSize));
			this.m_MaxAtlasSize = maxAtlasSize;
			this.format = format;
			this.maxImageSize = maxImageSize;
			this.m_FilterMode = filterMode;
			this.m_UVs = new Dictionary<Texture2D, RectInt>(64);
			this.m_Blitter = new TextureBlitter(64);
			this.m_InitialSize = initialSize;
			this.m_2SidePadding = ((filterMode == FilterMode.Point) ? 0 : 2);
			this.m_1SidePadding = ((filterMode == FilterMode.Point) ? 0 : 1);
			this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, this.m_MaxAtlasSize, this.m_1SidePadding);
			this.m_ColorSpace = QualitySettings.activeColorSpace;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00044FBE File Offset: 0x000431BE
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x00044FC6 File Offset: 0x000431C6
		private protected bool disposed { protected get; private set; }

		// Token: 0x060011E3 RID: 4579 RVA: 0x00044FCF File Offset: 0x000431CF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00044FE4 File Offset: 0x000431E4
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.atlas);
					this.atlas = null;
					bool flag = this.m_Allocator != null;
					if (flag)
					{
						this.m_Allocator.Dispose();
						this.m_Allocator = null;
					}
					bool flag2 = this.m_Blitter != null;
					if (flag2)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00045067 File Offset: 0x00043267
		private static void LogDisposeError()
		{
			Debug.LogError("An attempt to use a disposed atlas manager has been detected.");
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00045078 File Offset: 0x00043278
		public bool IsReleased()
		{
			return this.atlas != null && !this.atlas.IsCreated();
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000450AC File Offset: 0x000432AC
		public bool TryGetRect(Texture2D image, out RectInt uvs, Func<Texture2D, bool> filter = null)
		{
			uvs = default(RectInt);
			bool disposed = this.disposed;
			bool flag;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
				flag = false;
			}
			else
			{
				bool flag2 = image == null;
				if (flag2)
				{
					flag = false;
				}
				else
				{
					bool flag3 = this.m_UVs.TryGetValue(image, ref uvs);
					if (flag3)
					{
						flag = true;
					}
					else
					{
						bool flag4 = filter != null && !filter.Invoke(image);
						if (flag4)
						{
							flag = false;
						}
						else
						{
							bool flag5 = !this.AllocateRect(image.width, image.height, out uvs);
							if (flag5)
							{
								flag = false;
							}
							else
							{
								this.m_UVs[image] = uvs;
								this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(uvs.x, uvs.y), true, Color.white);
								flag = true;
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004518C File Offset: 0x0004338C
		public void UpdateTexture(Texture2D image)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
			}
			else
			{
				RectInt rectInt;
				bool flag = !this.m_UVs.TryGetValue(image, ref rectInt);
				if (!flag)
				{
					this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(rectInt.x, rectInt.y), true, Color.white);
				}
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00045200 File Offset: 0x00043400
		public bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool flag = !this.m_Allocator.TryAllocate(width + this.m_2SidePadding, height + this.m_2SidePadding, out uvs);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				uvs = new RectInt(uvs.x + this.m_1SidePadding, uvs.y + this.m_1SidePadding, width, height);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00045261 File Offset: 0x00043461
		public void EnqueueBlit(Texture image, RectInt srcRect, int x, int y, bool addBorder, Color tint)
		{
			this.m_Blitter.QueueBlit(image, srcRect, new Vector2Int(x, y), addBorder, tint);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00045280 File Offset: 0x00043480
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
			}
			else
			{
				this.UpdateAtlasTexture();
				bool forceReblitAll = this.m_ForceReblitAll;
				if (forceReblitAll)
				{
					this.m_ForceReblitAll = false;
					this.m_Blitter.Reset();
					foreach (KeyValuePair<Texture2D, RectInt> keyValuePair in this.m_UVs)
					{
						this.m_Blitter.QueueBlit(keyValuePair.Key, new RectInt(0, 0, keyValuePair.Key.width, keyValuePair.Key.height), new Vector2Int(keyValuePair.Value.x, keyValuePair.Value.y), true, Color.white);
					}
				}
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00045380 File Offset: 0x00043580
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				bool flag2 = this.m_UVs.Count > this.m_Blitter.queueLength;
				if (flag2)
				{
					this.m_ForceReblitAll = true;
				}
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag3 = this.atlas.width != this.m_Allocator.physicalWidth || this.atlas.height != this.m_Allocator.physicalHeight;
				if (flag3)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					bool flag4 = renderTexture == null;
					if (flag4)
					{
						Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_Allocator.physicalWidth,
							this.m_Allocator.physicalHeight
						});
					}
					else
					{
						this.m_Blitter.BlitOneNow(renderTexture, this.atlas, new RectInt(0, 0, this.atlas.width, this.atlas.height), new Vector2Int(0, 0), false, Color.white);
					}
					UIRUtility.Destroy(this.atlas);
					this.atlas = renderTexture;
				}
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000454DC File Offset: 0x000436DC
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_Allocator.physicalWidth == 0 || this.m_Allocator.physicalHeight == 0;
			RenderTexture renderTexture;
			if (flag)
			{
				renderTexture = null;
			}
			else
			{
				renderTexture = new RenderTexture(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Dynamic Atlas " + DynamicAtlasCore.s_TextureCounter++.ToString(),
					filterMode = this.m_FilterMode
				};
			}
			return renderTexture;
		}

		// Token: 0x040007EF RID: 2031
		private int m_InitialSize;

		// Token: 0x040007F0 RID: 2032
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x040007F1 RID: 2033
		private Dictionary<Texture2D, RectInt> m_UVs;

		// Token: 0x040007F2 RID: 2034
		private bool m_ForceReblitAll;

		// Token: 0x040007F3 RID: 2035
		private FilterMode m_FilterMode;

		// Token: 0x040007F4 RID: 2036
		private ColorSpace m_ColorSpace;

		// Token: 0x040007F5 RID: 2037
		private TextureBlitter m_Blitter;

		// Token: 0x040007F6 RID: 2038
		private int m_2SidePadding;

		// Token: 0x040007F7 RID: 2039
		private int m_1SidePadding;

		// Token: 0x040007F8 RID: 2040
		private int m_MaxAtlasSize;

		// Token: 0x040007F9 RID: 2041
		private static ProfilerMarker s_MarkerReset = new ProfilerMarker("UIR.AtlasManager.Reset");

		// Token: 0x040007FC RID: 2044
		private static int s_TextureCounter;
	}
}
