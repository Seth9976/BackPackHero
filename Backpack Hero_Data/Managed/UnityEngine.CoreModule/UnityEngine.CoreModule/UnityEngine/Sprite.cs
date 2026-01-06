using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200026A RID: 618
	[ExcludeFromPreset]
	[NativeType("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Runtime/Graphics/SpriteUtility.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/2D/Common/ScriptBindings/SpritesMarshalling.h")]
	public sealed class Sprite : Object
	{
		// Token: 0x06001AD0 RID: 6864 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		[RequiredByNativeCode]
		private Sprite()
		{
		}

		// Token: 0x06001AD1 RID: 6865
		[MethodImpl(4096)]
		internal extern int GetPackingMode();

		// Token: 0x06001AD2 RID: 6866
		[MethodImpl(4096)]
		internal extern int GetPackingRotation();

		// Token: 0x06001AD3 RID: 6867
		[MethodImpl(4096)]
		internal extern int GetPacked();

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0002AE94 File Offset: 0x00029094
		internal Rect GetTextureRect()
		{
			Rect rect;
			this.GetTextureRect_Injected(out rect);
			return rect;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0002AEAC File Offset: 0x000290AC
		internal Vector2 GetTextureRectOffset()
		{
			Vector2 vector;
			this.GetTextureRectOffset_Injected(out vector);
			return vector;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0002AEC4 File Offset: 0x000290C4
		internal Vector4 GetInnerUVs()
		{
			Vector4 vector;
			this.GetInnerUVs_Injected(out vector);
			return vector;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0002AEDC File Offset: 0x000290DC
		internal Vector4 GetOuterUVs()
		{
			Vector4 vector;
			this.GetOuterUVs_Injected(out vector);
			return vector;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0002AEF4 File Offset: 0x000290F4
		internal Vector4 GetPadding()
		{
			Vector4 vector;
			this.GetPadding_Injected(out vector);
			return vector;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0002AF0A File Offset: 0x0002910A
		[FreeFunction("SpritesBindings::CreateSpriteWithoutTextureScripting")]
		internal static Sprite CreateSpriteWithoutTextureScripting(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting_Injected(ref rect, ref pivot, pixelsToUnits, texture);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0002AF17 File Offset: 0x00029117
		[FreeFunction("SpritesBindings::CreateSprite")]
		internal static Sprite CreateSprite(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			return Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape);
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0002AF2C File Offset: 0x0002912C
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0002AF44 File Offset: 0x00029144
		public Rect rect
		{
			get
			{
				Rect rect;
				this.get_rect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x0002AF5C File Offset: 0x0002915C
		public Vector4 border
		{
			get
			{
				Vector4 vector;
				this.get_border_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001ADE RID: 6878
		public extern Texture2D texture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001ADF RID: 6879
		[MethodImpl(4096)]
		internal extern Texture2D GetSecondaryTexture(int index);

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001AE0 RID: 6880
		public extern float pixelsPerUnit
		{
			[NativeMethod("GetPixelsToUnits")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001AE1 RID: 6881
		public extern float spriteAtlasTextureScale
		{
			[NativeMethod("GetSpriteAtlasTextureScale")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001AE2 RID: 6882
		public extern Texture2D associatedAlphaSplitTexture
		{
			[NativeMethod("GetAlphaTexture")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0002AF74 File Offset: 0x00029174
		public Vector2 pivot
		{
			[NativeMethod("GetPivotInPixels")]
			get
			{
				Vector2 vector;
				this.get_pivot_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0002AF8C File Offset: 0x0002918C
		public bool packed
		{
			get
			{
				return this.GetPacked() == 1;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0002AFA8 File Offset: 0x000291A8
		public SpritePackingMode packingMode
		{
			get
			{
				return (SpritePackingMode)this.GetPackingMode();
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0002AFC0 File Offset: 0x000291C0
		public SpritePackingRotation packingRotation
		{
			get
			{
				return (SpritePackingRotation)this.GetPackingRotation();
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0002AFD8 File Offset: 0x000291D8
		public Rect textureRect
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Rect rect;
				if (flag)
				{
					rect = Rect.zero;
				}
				else
				{
					rect = this.GetTextureRect();
				}
				return rect;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0002B014 File Offset: 0x00029214
		public Vector2 textureRectOffset
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Vector2 vector;
				if (flag)
				{
					vector = Vector2.zero;
				}
				else
				{
					vector = this.GetTextureRectOffset();
				}
				return vector;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001AE9 RID: 6889
		public extern Vector2[] vertices
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteVertices", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001AEA RID: 6890
		public extern ushort[] triangles
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteIndices", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001AEB RID: 6891
		public extern Vector2[] uv
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteUVs", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001AEC RID: 6892
		[MethodImpl(4096)]
		public extern int GetPhysicsShapeCount();

		// Token: 0x06001AED RID: 6893 RVA: 0x0002B050 File Offset: 0x00029250
		public int GetPhysicsShapePointCount(int shapeIdx)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			return this.Internal_GetPhysicsShapePointCount(shapeIdx);
		}

		// Token: 0x06001AEE RID: 6894
		[NativeMethod("GetPhysicsShapePointCount")]
		[MethodImpl(4096)]
		private extern int Internal_GetPhysicsShapePointCount(int shapeIdx);

		// Token: 0x06001AEF RID: 6895 RVA: 0x0002B0A0 File Offset: 0x000292A0
		public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			Sprite.GetPhysicsShapeImpl(this, shapeIdx, physicsShape);
			return physicsShape.Count;
		}

		// Token: 0x06001AF0 RID: 6896
		[FreeFunction("SpritesBindings::GetPhysicsShape", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetPhysicsShapeImpl(Sprite sprite, int shapeIdx, [NotNull("ArgumentNullException")] List<Vector2> physicsShape);

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0002B0F8 File Offset: 0x000292F8
		public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
		{
			bool flag = physicsShapes == null;
			if (flag)
			{
				throw new ArgumentNullException("physicsShapes");
			}
			for (int i = 0; i < physicsShapes.Count; i++)
			{
				Vector2[] array = physicsShapes[i];
				bool flag2 = array == null;
				if (flag2)
				{
					throw new ArgumentNullException("physicsShape", string.Format("Physics Shape at {0} is null.", i));
				}
				bool flag3 = array.Length < 3;
				if (flag3)
				{
					throw new ArgumentException(string.Format("Physics Shape at {0} has less than 3 vertices ({1}).", i, array.Length));
				}
			}
			Sprite.OverridePhysicsShapeCount(this, physicsShapes.Count);
			for (int j = 0; j < physicsShapes.Count; j++)
			{
				Sprite.OverridePhysicsShape(this, physicsShapes[j], j);
			}
		}

		// Token: 0x06001AF2 RID: 6898
		[FreeFunction("SpritesBindings::OverridePhysicsShapeCount")]
		[MethodImpl(4096)]
		private static extern void OverridePhysicsShapeCount(Sprite sprite, int physicsShapeCount);

		// Token: 0x06001AF3 RID: 6899
		[FreeFunction("SpritesBindings::OverridePhysicsShape", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void OverridePhysicsShape(Sprite sprite, Vector2[] physicsShape, int idx);

		// Token: 0x06001AF4 RID: 6900
		[FreeFunction("SpritesBindings::OverrideGeometry", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void OverrideGeometry([NotNull("ArgumentNullException")] Vector2[] vertices, [NotNull("ArgumentNullException")] ushort[] triangles);

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0002B1C4 File Offset: 0x000293C4
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, texture);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0002B1E0 File Offset: 0x000293E0
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, null);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0002B1FC File Offset: 0x000293FC
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			bool flag = texture == null;
			Sprite sprite;
			if (flag)
			{
				sprite = null;
			}
			else
			{
				bool flag2 = rect.xMax > (float)texture.width || rect.yMax > (float)texture.height;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Could not create sprite ({0}, {1}, {2}, {3}) from a {4}x{5} texture.", new object[] { rect.x, rect.y, rect.width, rect.height, texture.width, texture.height }));
				}
				bool flag3 = pixelsPerUnit <= 0f;
				if (flag3)
				{
					throw new ArgumentException("pixelsPerUnit must be set to a positive non-zero value.");
				}
				sprite = Sprite.CreateSprite(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape);
			}
			return sprite;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0002B2E0 File Offset: 0x000294E0
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, false);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0002B304 File Offset: 0x00029504
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, Vector4.zero);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0002B328 File Offset: 0x00029528
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, SpriteMeshType.Tight);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0002B348 File Offset: 0x00029548
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0U);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0002B364 File Offset: 0x00029564
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			return Sprite.Create(texture, rect, pivot, 100f);
		}

		// Token: 0x06001AFD RID: 6909
		[MethodImpl(4096)]
		private extern void GetTextureRect_Injected(out Rect ret);

		// Token: 0x06001AFE RID: 6910
		[MethodImpl(4096)]
		private extern void GetTextureRectOffset_Injected(out Vector2 ret);

		// Token: 0x06001AFF RID: 6911
		[MethodImpl(4096)]
		private extern void GetInnerUVs_Injected(out Vector4 ret);

		// Token: 0x06001B00 RID: 6912
		[MethodImpl(4096)]
		private extern void GetOuterUVs_Injected(out Vector4 ret);

		// Token: 0x06001B01 RID: 6913
		[MethodImpl(4096)]
		private extern void GetPadding_Injected(out Vector4 ret);

		// Token: 0x06001B02 RID: 6914
		[MethodImpl(4096)]
		private static extern Sprite CreateSpriteWithoutTextureScripting_Injected(ref Rect rect, ref Vector2 pivot, float pixelsToUnits, Texture2D texture);

		// Token: 0x06001B03 RID: 6915
		[MethodImpl(4096)]
		private static extern Sprite CreateSprite_Injected(Texture2D texture, ref Rect rect, ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, ref Vector4 border, bool generateFallbackPhysicsShape);

		// Token: 0x06001B04 RID: 6916
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06001B05 RID: 6917
		[MethodImpl(4096)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x06001B06 RID: 6918
		[MethodImpl(4096)]
		private extern void get_border_Injected(out Vector4 ret);

		// Token: 0x06001B07 RID: 6919
		[MethodImpl(4096)]
		private extern void get_pivot_Injected(out Vector2 ret);
	}
}
