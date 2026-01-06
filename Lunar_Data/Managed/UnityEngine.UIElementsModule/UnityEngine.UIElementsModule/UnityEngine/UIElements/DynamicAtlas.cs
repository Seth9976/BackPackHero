using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000008 RID: 8
	internal class DynamicAtlas : AtlasBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000219E File Offset: 0x0000039E
		internal bool isInitialized
		{
			get
			{
				return this.m_PointPage != null || this.m_BilinearPage != null;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021B4 File Offset: 0x000003B4
		protected override void OnAssignedToPanel(IPanel panel)
		{
			base.OnAssignedToPanel(panel);
			this.m_Panels.Add(panel);
			bool flag = this.m_Panels.Count == 1;
			if (flag)
			{
				this.m_ColorSpace = QualitySettings.activeColorSpace;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021F4 File Offset: 0x000003F4
		protected override void OnRemovedFromPanel(IPanel panel)
		{
			this.m_Panels.Remove(panel);
			bool flag = this.m_Panels.Count == 0 && this.isInitialized;
			if (flag)
			{
				this.DestroyPages();
			}
			base.OnRemovedFromPanel(panel);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002238 File Offset: 0x00000438
		public override void Reset()
		{
			bool isInitialized = this.isInitialized;
			if (isInitialized)
			{
				this.DestroyPages();
				int i = 0;
				int count = this.m_Panels.Count;
				while (i < count)
				{
					AtlasBase.RepaintTexturedElements(this.m_Panels[i]);
					i++;
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002288 File Offset: 0x00000488
		private void InitPages()
		{
			int num = Mathf.Max(this.m_MaxSubTextureSize, 1);
			num = Mathf.NextPowerOfTwo(num);
			int num2 = Mathf.Max(this.m_MaxAtlasSize, 1);
			num2 = Mathf.NextPowerOfTwo(num2);
			num2 = Mathf.Min(num2, SystemInfo.maxRenderTextureSize);
			int num3 = Mathf.Max(this.m_MinAtlasSize, 1);
			num3 = Mathf.NextPowerOfTwo(num3);
			num3 = Mathf.Min(num3, num2);
			Vector2Int vector2Int = new Vector2Int(num3, num3);
			Vector2Int vector2Int2 = new Vector2Int(num2, num2);
			this.m_PointPage = new DynamicAtlasPage(RenderTextureFormat.ARGB32, FilterMode.Point, vector2Int, vector2Int2);
			this.m_BilinearPage = new DynamicAtlasPage(RenderTextureFormat.ARGB32, FilterMode.Bilinear, vector2Int, vector2Int2);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002318 File Offset: 0x00000518
		private void DestroyPages()
		{
			this.m_PointPage.Dispose();
			this.m_PointPage = null;
			this.m_BilinearPage.Dispose();
			this.m_BilinearPage = null;
			this.m_Database.Clear();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002350 File Offset: 0x00000550
		public override bool TryGetAtlas(VisualElement ve, Texture2D src, out TextureId atlas, out RectInt atlasRect)
		{
			bool flag = this.m_Panels.Count == 0 || src == null;
			bool flag2;
			if (flag)
			{
				atlas = TextureId.invalid;
				atlasRect = default(RectInt);
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.isInitialized;
				if (flag3)
				{
					this.InitPages();
				}
				DynamicAtlas.TextureInfo textureInfo;
				bool flag4 = this.m_Database.TryGetValue(src, ref textureInfo);
				if (flag4)
				{
					atlas = textureInfo.page.textureId;
					atlasRect = textureInfo.rect;
					textureInfo.counter++;
					flag2 = true;
				}
				else
				{
					Allocator2D.Alloc2D alloc2D;
					bool flag5 = this.IsTextureValid(src, FilterMode.Bilinear) && this.m_BilinearPage.TryAdd(src, out alloc2D, out atlasRect);
					if (flag5)
					{
						textureInfo = DynamicAtlas.TextureInfo.pool.Get();
						textureInfo.alloc = alloc2D;
						textureInfo.counter = 1;
						textureInfo.page = this.m_BilinearPage;
						textureInfo.rect = atlasRect;
						this.m_Database[src] = textureInfo;
						atlas = this.m_BilinearPage.textureId;
						flag2 = true;
					}
					else
					{
						bool flag6 = this.IsTextureValid(src, FilterMode.Point) && this.m_PointPage.TryAdd(src, out alloc2D, out atlasRect);
						if (flag6)
						{
							textureInfo = DynamicAtlas.TextureInfo.pool.Get();
							textureInfo.alloc = alloc2D;
							textureInfo.counter = 1;
							textureInfo.page = this.m_PointPage;
							textureInfo.rect = atlasRect;
							this.m_Database[src] = textureInfo;
							atlas = this.m_PointPage.textureId;
							flag2 = true;
						}
						else
						{
							atlas = TextureId.invalid;
							atlasRect = default(RectInt);
							flag2 = false;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002500 File Offset: 0x00000700
		public override void ReturnAtlas(VisualElement ve, Texture2D src, TextureId atlas)
		{
			DynamicAtlas.TextureInfo textureInfo;
			bool flag = this.m_Database.TryGetValue(src, ref textureInfo);
			if (flag)
			{
				textureInfo.counter--;
				bool flag2 = textureInfo.counter == 0;
				if (flag2)
				{
					textureInfo.page.Remove(textureInfo.alloc);
					this.m_Database.Remove(src);
					DynamicAtlas.TextureInfo.pool.Return(textureInfo);
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000256C File Offset: 0x0000076C
		protected override void OnUpdateDynamicTextures(IPanel panel)
		{
			bool flag = this.m_PointPage != null;
			if (flag)
			{
				this.m_PointPage.Commit();
				base.SetDynamicTexture(this.m_PointPage.textureId, this.m_PointPage.atlas);
			}
			bool flag2 = this.m_BilinearPage != null;
			if (flag2)
			{
				this.m_BilinearPage.Commit();
				base.SetDynamicTexture(this.m_BilinearPage.textureId, this.m_BilinearPage.atlas);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025EC File Offset: 0x000007EC
		internal static bool IsTextureFormatSupported(TextureFormat format)
		{
			switch (format)
			{
			case TextureFormat.Alpha8:
			case TextureFormat.ARGB4444:
			case TextureFormat.RGB24:
			case TextureFormat.RGBA32:
			case TextureFormat.ARGB32:
			case TextureFormat.RGB565:
			case TextureFormat.R16:
			case TextureFormat.DXT1:
			case TextureFormat.DXT5:
			case TextureFormat.RGBA4444:
			case TextureFormat.BGRA32:
			case TextureFormat.BC7:
			case TextureFormat.BC4:
			case TextureFormat.BC5:
			case TextureFormat.DXT1Crunched:
			case TextureFormat.DXT5Crunched:
			case TextureFormat.PVRTC_RGB2:
			case TextureFormat.PVRTC_RGBA2:
			case TextureFormat.PVRTC_RGB4:
			case TextureFormat.PVRTC_RGBA4:
			case TextureFormat.ETC_RGB4:
			case TextureFormat.EAC_R:
			case TextureFormat.EAC_R_SIGNED:
			case TextureFormat.EAC_RG:
			case TextureFormat.EAC_RG_SIGNED:
			case TextureFormat.ETC2_RGB:
			case TextureFormat.ETC2_RGBA1:
			case TextureFormat.ETC2_RGBA8:
			case TextureFormat.ASTC_4x4:
			case TextureFormat.ASTC_5x5:
			case TextureFormat.ASTC_6x6:
			case TextureFormat.ASTC_8x8:
			case TextureFormat.ASTC_10x10:
			case TextureFormat.ASTC_12x12:
			case TextureFormat.ASTC_RGBA_4x4:
			case TextureFormat.ASTC_RGBA_5x5:
			case TextureFormat.ASTC_RGBA_6x6:
			case TextureFormat.ASTC_RGBA_8x8:
			case TextureFormat.ASTC_RGBA_10x10:
			case TextureFormat.ASTC_RGBA_12x12:
			case TextureFormat.ETC_RGB4_3DS:
			case TextureFormat.ETC_RGBA8_3DS:
			case TextureFormat.RG16:
			case TextureFormat.R8:
			case TextureFormat.ETC_RGB4Crunched:
			case TextureFormat.ETC2_RGBA8Crunched:
				return true;
			case TextureFormat.RHalf:
			case TextureFormat.RGHalf:
			case TextureFormat.RGBAHalf:
			case TextureFormat.RFloat:
			case TextureFormat.RGFloat:
			case TextureFormat.RGBAFloat:
			case TextureFormat.YUY2:
			case TextureFormat.RGB9e5Float:
			case TextureFormat.BC6H:
			case TextureFormat.ASTC_HDR_4x4:
			case TextureFormat.ASTC_HDR_5x5:
			case TextureFormat.ASTC_HDR_6x6:
			case TextureFormat.ASTC_HDR_8x8:
			case TextureFormat.ASTC_HDR_10x10:
			case TextureFormat.ASTC_HDR_12x12:
			case TextureFormat.RG32:
			case TextureFormat.RGB48:
			case TextureFormat.RGBA64:
				return false;
			}
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002740 File Offset: 0x00000940
		public virtual bool IsTextureValid(Texture2D texture, FilterMode atlasFilterMode)
		{
			DynamicAtlasFilters activeFilters = this.m_ActiveFilters;
			bool flag = this.m_CustomFilter != null && !this.m_CustomFilter(texture, ref activeFilters);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = (activeFilters & DynamicAtlasFilters.Readability) > DynamicAtlasFilters.None;
				bool flag4 = (activeFilters & DynamicAtlasFilters.Size) > DynamicAtlasFilters.None;
				bool flag5 = (activeFilters & DynamicAtlasFilters.Format) > DynamicAtlasFilters.None;
				bool flag6 = (activeFilters & DynamicAtlasFilters.ColorSpace) > DynamicAtlasFilters.None;
				bool flag7 = (activeFilters & DynamicAtlasFilters.FilterMode) > DynamicAtlasFilters.None;
				bool flag8 = flag3 && texture.isReadable;
				if (flag8)
				{
					flag2 = false;
				}
				else
				{
					bool flag9 = flag4 && (texture.width > this.maxSubTextureSize || texture.height > this.maxSubTextureSize);
					if (flag9)
					{
						flag2 = false;
					}
					else
					{
						bool flag10 = flag5 && !DynamicAtlas.IsTextureFormatSupported(texture.format);
						if (flag10)
						{
							flag2 = false;
						}
						else
						{
							bool flag11 = flag6 && this.m_ColorSpace == ColorSpace.Linear && texture.activeTextureColorSpace > ColorSpace.Gamma;
							if (flag11)
							{
								flag2 = false;
							}
							else
							{
								bool flag12 = flag7 && texture.filterMode != atlasFilterMode;
								flag2 = !flag12;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002858 File Offset: 0x00000A58
		public void SetDirty(Texture2D tex)
		{
			bool flag = tex == null;
			if (!flag)
			{
				DynamicAtlas.TextureInfo textureInfo;
				bool flag2 = this.m_Database.TryGetValue(tex, ref textureInfo);
				if (flag2)
				{
					textureInfo.page.Update(tex, textureInfo.rect);
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002898 File Offset: 0x00000A98
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000028B0 File Offset: 0x00000AB0
		public int minAtlasSize
		{
			get
			{
				return this.m_MinAtlasSize;
			}
			set
			{
				bool flag = this.m_MinAtlasSize == value;
				if (!flag)
				{
					this.m_MinAtlasSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000028DC File Offset: 0x00000ADC
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000028F4 File Offset: 0x00000AF4
		public int maxAtlasSize
		{
			get
			{
				return this.m_MaxAtlasSize;
			}
			set
			{
				bool flag = this.m_MaxAtlasSize == value;
				if (!flag)
				{
					this.m_MaxAtlasSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000291F File Offset: 0x00000B1F
		public static DynamicAtlasFilters defaultFilters
		{
			get
			{
				return DynamicAtlasFilters.Readability | DynamicAtlasFilters.Size | DynamicAtlasFilters.Format | DynamicAtlasFilters.ColorSpace | DynamicAtlasFilters.FilterMode;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002924 File Offset: 0x00000B24
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000293C File Offset: 0x00000B3C
		public DynamicAtlasFilters activeFilters
		{
			get
			{
				return this.m_ActiveFilters;
			}
			set
			{
				bool flag = this.m_ActiveFilters == value;
				if (!flag)
				{
					this.m_ActiveFilters = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002968 File Offset: 0x00000B68
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002980 File Offset: 0x00000B80
		public int maxSubTextureSize
		{
			get
			{
				return this.m_MaxSubTextureSize;
			}
			set
			{
				bool flag = this.m_MaxSubTextureSize == value;
				if (!flag)
				{
					this.m_MaxSubTextureSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000029AC File Offset: 0x00000BAC
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000029C4 File Offset: 0x00000BC4
		public DynamicAtlasCustomFilter customFilter
		{
			get
			{
				return this.m_CustomFilter;
			}
			set
			{
				bool flag = this.m_CustomFilter == value;
				if (!flag)
				{
					this.m_CustomFilter = value;
					this.Reset();
				}
			}
		}

		// Token: 0x04000009 RID: 9
		private Dictionary<Texture, DynamicAtlas.TextureInfo> m_Database = new Dictionary<Texture, DynamicAtlas.TextureInfo>();

		// Token: 0x0400000A RID: 10
		private DynamicAtlasPage m_PointPage;

		// Token: 0x0400000B RID: 11
		private DynamicAtlasPage m_BilinearPage;

		// Token: 0x0400000C RID: 12
		private ColorSpace m_ColorSpace;

		// Token: 0x0400000D RID: 13
		private List<IPanel> m_Panels = new List<IPanel>(1);

		// Token: 0x0400000E RID: 14
		private int m_MinAtlasSize = 64;

		// Token: 0x0400000F RID: 15
		private int m_MaxAtlasSize = 4096;

		// Token: 0x04000010 RID: 16
		private int m_MaxSubTextureSize = 64;

		// Token: 0x04000011 RID: 17
		private DynamicAtlasFilters m_ActiveFilters = DynamicAtlas.defaultFilters;

		// Token: 0x04000012 RID: 18
		private DynamicAtlasCustomFilter m_CustomFilter;

		// Token: 0x02000009 RID: 9
		private class TextureInfo : LinkedPoolItem<DynamicAtlas.TextureInfo>
		{
			// Token: 0x06000030 RID: 48 RVA: 0x00002A45 File Offset: 0x00000C45
			[MethodImpl(256)]
			private static DynamicAtlas.TextureInfo Create()
			{
				return new DynamicAtlas.TextureInfo();
			}

			// Token: 0x06000031 RID: 49 RVA: 0x00002A4C File Offset: 0x00000C4C
			[MethodImpl(256)]
			private static void Reset(DynamicAtlas.TextureInfo info)
			{
				info.page = null;
				info.counter = 0;
				info.alloc = default(Allocator2D.Alloc2D);
				info.rect = default(RectInt);
			}

			// Token: 0x04000013 RID: 19
			public DynamicAtlasPage page;

			// Token: 0x04000014 RID: 20
			public int counter;

			// Token: 0x04000015 RID: 21
			public Allocator2D.Alloc2D alloc;

			// Token: 0x04000016 RID: 22
			public RectInt rect;

			// Token: 0x04000017 RID: 23
			public static readonly LinkedPool<DynamicAtlas.TextureInfo> pool = new LinkedPool<DynamicAtlas.TextureInfo>(new Func<DynamicAtlas.TextureInfo>(DynamicAtlas.TextureInfo.Create), new Action<DynamicAtlas.TextureInfo>(DynamicAtlas.TextureInfo.Reset), 1024);
		}
	}
}
