using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032C RID: 812
	internal struct UIRVEShaderInfoAllocator
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x0006F100 File Offset: 0x0006D300
		private static int pageWidth
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0006F114 File Offset: 0x0006D314
		private static int pageHeight
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0006F128 File Offset: 0x0006D328
		private static Vector2Int AllocToTexelCoord(ref BitmapAllocator32 allocator, BMPAlloc alloc)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Vector2Int((int)alloc.bitIndex * allocator.entryWidth + (int)num, (int)alloc.pageLine * allocator.entryHeight + (int)num2);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0006F170 File Offset: 0x0006D370
		private static int AllocToConstantBufferIndex(BMPAlloc alloc)
		{
			return (int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0006F198 File Offset: 0x0006D398
		private static bool AtlasRectMatchesPage(ref BitmapAllocator32 allocator, BMPAlloc defAlloc, RectInt atlasRect)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(defAlloc.page, out num, out num2);
			return (int)num == atlasRect.xMin && (int)num2 == atlasRect.yMin && allocator.entryWidth * UIRVEShaderInfoAllocator.pageWidth == atlasRect.width && allocator.entryHeight * UIRVEShaderInfoAllocator.pageHeight == atlasRect.height;
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0006F1FC File Offset: 0x0006D3FC
		public NativeSlice<Transform3x4> transformConstants
		{
			get
			{
				return this.m_Transforms;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0006F21C File Offset: 0x0006D41C
		public NativeSlice<Vector4> clipRectConstants
		{
			get
			{
				return this.m_ClipRects;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x0006F23C File Offset: 0x0006D43C
		public Texture atlas
		{
			get
			{
				bool storageReallyCreated = this.m_StorageReallyCreated;
				Texture texture;
				if (storageReallyCreated)
				{
					texture = this.m_Storage.texture;
				}
				else
				{
					texture = (this.m_VertexTexturingEnabled ? UIRenderDevice.defaultShaderInfoTexFloat : UIRenderDevice.defaultShaderInfoTexARGB8);
				}
				return texture;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0006F27C File Offset: 0x0006D47C
		public bool internalAtlasCreated
		{
			get
			{
				return this.m_StorageReallyCreated;
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0006F294 File Offset: 0x0006D494
		public void Construct()
		{
			this.m_OpacityAllocator = (this.m_ColorAllocator = (this.m_ClipRectAllocator = (this.m_TransformAllocator = (this.m_TextSettingsAllocator = default(BitmapAllocator32)))));
			this.m_TransformAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 3);
			this.m_TransformAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.identityTransformTexel.x, (ushort)UIRVEShaderInfoAllocator.identityTransformTexel.y);
			this.m_ClipRectAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ClipRectAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, (ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.y);
			this.m_OpacityAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_OpacityAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.x, (ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.y);
			this.m_ColorAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ColorAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.clearColorTexel.x, (ushort)UIRVEShaderInfoAllocator.clearColorTexel.y);
			this.m_TextSettingsAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 4);
			this.m_TextSettingsAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, (ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y);
			this.m_VertexTexturingEnabled = UIRenderDevice.vertexTexturingIsAvailable;
			bool flag = !this.m_VertexTexturingEnabled;
			if (flag)
			{
				int num = 20;
				this.m_Transforms = new NativeArray<Transform3x4>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_ClipRects = new NativeArray<Vector4>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_Transforms[0] = new Transform3x4
				{
					v0 = UIRVEShaderInfoAllocator.identityTransformRow0Value,
					v1 = UIRVEShaderInfoAllocator.identityTransformRow1Value,
					v2 = UIRVEShaderInfoAllocator.identityTransformRow2Value
				};
				this.m_ClipRects[0] = UIRVEShaderInfoAllocator.infiniteClipRectValue;
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0006F484 File Offset: 0x0006D684
		private void ReallyCreateStorage()
		{
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_Storage = new ShaderInfoStorageRGBAFloat(64, 4096);
			}
			else
			{
				this.m_Storage = new ShaderInfoStorageRGBA32(64, 4096);
			}
			RectInt rectInt;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TransformAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TransformAllocator.entryHeight, out rectInt);
			RectInt rectInt2;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ClipRectAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ClipRectAllocator.entryHeight, out rectInt2);
			RectInt rectInt3;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_OpacityAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_OpacityAllocator.entryHeight, out rectInt3);
			RectInt rectInt4;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ColorAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ColorAllocator.entryHeight, out rectInt4);
			RectInt rectInt5;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TextSettingsAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TextSettingsAllocator.entryHeight, out rectInt5);
			bool flag = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TransformAllocator, UIRVEShaderInfoAllocator.identityTransform, rectInt);
			if (flag)
			{
				throw new Exception("Atlas identity transform allocation failed unexpectedly");
			}
			bool flag2 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ClipRectAllocator, UIRVEShaderInfoAllocator.infiniteClipRect, rectInt2);
			if (flag2)
			{
				throw new Exception("Atlas infinite clip rect allocation failed unexpectedly");
			}
			bool flag3 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_OpacityAllocator, UIRVEShaderInfoAllocator.fullOpacity, rectInt3);
			if (flag3)
			{
				throw new Exception("Atlas full opacity allocation failed unexpectedly");
			}
			bool flag4 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ColorAllocator, UIRVEShaderInfoAllocator.clearColor, rectInt4);
			if (flag4)
			{
				throw new Exception("Atlas clear color allocation failed unexpectedly");
			}
			bool flag5 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TextSettingsAllocator, UIRVEShaderInfoAllocator.defaultTextCoreSettings, rectInt5);
			if (flag5)
			{
				throw new Exception("Atlas text setting allocation failed unexpectedly");
			}
			bool vertexTexturingEnabled2 = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled2)
			{
				this.SetTransformValue(UIRVEShaderInfoAllocator.identityTransform, UIRVEShaderInfoAllocator.identityTransformValue);
				this.SetClipRectValue(UIRVEShaderInfoAllocator.infiniteClipRect, UIRVEShaderInfoAllocator.infiniteClipRectValue);
			}
			this.SetOpacityValue(UIRVEShaderInfoAllocator.fullOpacity, UIRVEShaderInfoAllocator.fullOpacityValue.w);
			this.SetColorValue(UIRVEShaderInfoAllocator.clearColor, UIRVEShaderInfoAllocator.clearColorValue);
			this.SetTextCoreSettingValue(UIRVEShaderInfoAllocator.defaultTextCoreSettings, UIRVEShaderInfoAllocator.defaultTextCoreSettingsValue);
			this.m_StorageReallyCreated = true;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0006F6E4 File Offset: 0x0006D8E4
		public void Dispose()
		{
			bool flag = this.m_Storage != null;
			if (flag)
			{
				this.m_Storage.Dispose();
			}
			this.m_Storage = null;
			bool isCreated = this.m_ClipRects.IsCreated;
			if (isCreated)
			{
				this.m_ClipRects.Dispose();
			}
			bool isCreated2 = this.m_Transforms.IsCreated;
			if (isCreated2)
			{
				this.m_Transforms.Dispose();
			}
			this.m_StorageReallyCreated = false;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0006F74F File Offset: 0x0006D94F
		public void IssuePendingStorageChanges()
		{
			BaseShaderInfoStorage storage = this.m_Storage;
			if (storage != null)
			{
				storage.UpdateTexture();
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0006F764 File Offset: 0x0006D964
		public BMPAlloc AllocTransform()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			BMPAlloc bmpalloc;
			if (vertexTexturingEnabled)
			{
				bmpalloc = this.m_TransformAllocator.Allocate(this.m_Storage);
			}
			else
			{
				BMPAlloc bmpalloc2 = this.m_TransformAllocator.Allocate(null);
				bool flag2 = UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(bmpalloc2) < this.m_Transforms.Length;
				if (flag2)
				{
					bmpalloc = bmpalloc2;
				}
				else
				{
					this.m_TransformAllocator.Free(bmpalloc2);
					bmpalloc = BMPAlloc.Invalid;
				}
			}
			return bmpalloc;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0006F7E4 File Offset: 0x0006D9E4
		public BMPAlloc AllocClipRect()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			BMPAlloc bmpalloc;
			if (vertexTexturingEnabled)
			{
				bmpalloc = this.m_ClipRectAllocator.Allocate(this.m_Storage);
			}
			else
			{
				BMPAlloc bmpalloc2 = this.m_ClipRectAllocator.Allocate(null);
				bool flag2 = UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(bmpalloc2) < this.m_ClipRects.Length;
				if (flag2)
				{
					bmpalloc = bmpalloc2;
				}
				else
				{
					this.m_ClipRectAllocator.Free(bmpalloc2);
					bmpalloc = BMPAlloc.Invalid;
				}
			}
			return bmpalloc;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0006F864 File Offset: 0x0006DA64
		public BMPAlloc AllocOpacity()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_OpacityAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0006F89C File Offset: 0x0006DA9C
		public BMPAlloc AllocColor()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_ColorAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0006F8D4 File Offset: 0x0006DAD4
		public BMPAlloc AllocTextCoreSettings(TextCoreSettings settings)
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_TextSettingsAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0006F90C File Offset: 0x0006DB0C
		public void SetTransformValue(BMPAlloc alloc, Matrix4x4 xform)
		{
			Debug.Assert(alloc.IsValid());
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TransformAllocator, alloc);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, xform.GetRow(0));
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, xform.GetRow(1));
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, xform.GetRow(2));
			}
			else
			{
				this.m_Transforms[UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(alloc)] = new Transform3x4
				{
					v0 = xform.GetRow(0),
					v1 = xform.GetRow(1),
					v2 = xform.GetRow(2)
				};
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0006FA08 File Offset: 0x0006DC08
		public void SetClipRectValue(BMPAlloc alloc, Vector4 clipRect)
		{
			Debug.Assert(alloc.IsValid());
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ClipRectAllocator, alloc);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, clipRect);
			}
			else
			{
				this.m_ClipRects[UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(alloc)] = clipRect;
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0006FA74 File Offset: 0x0006DC74
		public void SetOpacityValue(BMPAlloc alloc, float opacity)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_OpacityAllocator, alloc);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, new Color(1f, 1f, 1f, opacity));
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0006FACC File Offset: 0x0006DCCC
		public void SetColorValue(BMPAlloc alloc, Color color)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ColorAllocator, alloc);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, color);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0006FB10 File Offset: 0x0006DD10
		public void SetTextCoreSettingValue(BMPAlloc alloc, TextCoreSettings settings)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TextSettingsAllocator, alloc);
			Color color = new Color(-settings.underlayOffset.x, settings.underlayOffset.y, settings.underlaySoftness, settings.outlineWidth);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, settings.faceColor);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, settings.outlineColor);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, settings.underlayColor);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 3, color);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0006FBE3 File Offset: 0x0006DDE3
		public void FreeTransform(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TransformAllocator.Free(alloc);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0006FC00 File Offset: 0x0006DE00
		public void FreeClipRect(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ClipRectAllocator.Free(alloc);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0006FC1D File Offset: 0x0006DE1D
		public void FreeOpacity(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_OpacityAllocator.Free(alloc);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0006FC3A File Offset: 0x0006DE3A
		public void FreeColor(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ColorAllocator.Free(alloc);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0006FC57 File Offset: 0x0006DE57
		public void FreeTextCoreSettings(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TextSettingsAllocator.Free(alloc);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0006FC74 File Offset: 0x0006DE74
		public Color32 TransformAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_TransformAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			}
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0006FCE8 File Offset: 0x0006DEE8
		public Color32 ClipRectAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_ClipRectAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			}
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0006FD5C File Offset: 0x0006DF5C
		public Color32 OpacityAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_OpacityAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0006FDC0 File Offset: 0x0006DFC0
		public Color32 ColorAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_ColorAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0006FE24 File Offset: 0x0006E024
		public Color32 TextCoreSettingsToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_TextSettingsAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x04000BFB RID: 3067
		private BaseShaderInfoStorage m_Storage;

		// Token: 0x04000BFC RID: 3068
		private BitmapAllocator32 m_TransformAllocator;

		// Token: 0x04000BFD RID: 3069
		private BitmapAllocator32 m_ClipRectAllocator;

		// Token: 0x04000BFE RID: 3070
		private BitmapAllocator32 m_OpacityAllocator;

		// Token: 0x04000BFF RID: 3071
		private BitmapAllocator32 m_ColorAllocator;

		// Token: 0x04000C00 RID: 3072
		private BitmapAllocator32 m_TextSettingsAllocator;

		// Token: 0x04000C01 RID: 3073
		private bool m_StorageReallyCreated;

		// Token: 0x04000C02 RID: 3074
		private bool m_VertexTexturingEnabled;

		// Token: 0x04000C03 RID: 3075
		private NativeArray<Transform3x4> m_Transforms;

		// Token: 0x04000C04 RID: 3076
		private NativeArray<Vector4> m_ClipRects;

		// Token: 0x04000C05 RID: 3077
		internal static readonly Vector2Int identityTransformTexel = new Vector2Int(0, 0);

		// Token: 0x04000C06 RID: 3078
		internal static readonly Vector2Int infiniteClipRectTexel = new Vector2Int(0, 32);

		// Token: 0x04000C07 RID: 3079
		internal static readonly Vector2Int fullOpacityTexel = new Vector2Int(32, 32);

		// Token: 0x04000C08 RID: 3080
		internal static readonly Vector2Int clearColorTexel = new Vector2Int(0, 40);

		// Token: 0x04000C09 RID: 3081
		internal static readonly Vector2Int defaultTextCoreSettingsTexel = new Vector2Int(32, 0);

		// Token: 0x04000C0A RID: 3082
		internal static readonly Matrix4x4 identityTransformValue = Matrix4x4.identity;

		// Token: 0x04000C0B RID: 3083
		internal static readonly Vector4 identityTransformRow0Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(0);

		// Token: 0x04000C0C RID: 3084
		internal static readonly Vector4 identityTransformRow1Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(1);

		// Token: 0x04000C0D RID: 3085
		internal static readonly Vector4 identityTransformRow2Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(2);

		// Token: 0x04000C0E RID: 3086
		internal static readonly Vector4 infiniteClipRectValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000C0F RID: 3087
		internal static readonly Vector4 fullOpacityValue = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04000C10 RID: 3088
		internal static readonly Vector4 clearColorValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000C11 RID: 3089
		internal static readonly TextCoreSettings defaultTextCoreSettingsValue = new TextCoreSettings
		{
			faceColor = Color.white,
			outlineColor = Color.clear,
			outlineWidth = 0f,
			underlayColor = Color.clear,
			underlayOffset = Vector2.zero,
			underlaySoftness = 0f
		};

		// Token: 0x04000C12 RID: 3090
		public static readonly BMPAlloc identityTransform;

		// Token: 0x04000C13 RID: 3091
		public static readonly BMPAlloc infiniteClipRect;

		// Token: 0x04000C14 RID: 3092
		public static readonly BMPAlloc fullOpacity;

		// Token: 0x04000C15 RID: 3093
		public static readonly BMPAlloc clearColor;

		// Token: 0x04000C16 RID: 3094
		public static readonly BMPAlloc defaultTextCoreSettings;
	}
}
