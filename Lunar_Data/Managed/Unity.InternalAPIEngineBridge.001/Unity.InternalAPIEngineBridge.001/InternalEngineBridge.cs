using System;
using Unity.Collections;

namespace UnityEngine.U2D.Common
{
	// Token: 0x02000002 RID: 2
	internal static class InternalEngineBridge
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void SetLocalAABB(SpriteRenderer spriteRenderer, Bounds aabb)
		{
			spriteRenderer.SetLocalAABB(aabb);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
		public static void SetDeformableBuffer(SpriteRenderer spriteRenderer, NativeArray<byte> src)
		{
			spriteRenderer.SetDeformableBuffer(src);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002062 File Offset: 0x00000262
		public static bool IsUsingDeformableBuffer(SpriteRenderer spriteRenderer, IntPtr buffer)
		{
			return spriteRenderer.IsUsingDeformableBuffer(buffer);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
		public static Vector2 GUIUnclip(Vector2 v)
		{
			return GUIClip.Unclip(v);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002073 File Offset: 0x00000273
		public static Rect GetGUIClipTopMostRect()
		{
			return GUIClip.topmostRect;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public static Rect GetGUIClipTopRect()
		{
			return GUIClip.GetTopRect();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002081 File Offset: 0x00000281
		public static Rect GetGUIClipVisibleRect()
		{
			return GUIClip.visibleRect;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002088 File Offset: 0x00000288
		public static void SetBatchDeformableBufferAndLocalAABBArray(SpriteRenderer[] spriteRenderers, NativeArray<IntPtr> buffers, NativeArray<int> bufferSizes, NativeArray<Bounds> bounds)
		{
			SpriteRendererDataAccessExtensions.SetBatchDeformableBufferAndLocalAABBArray(spriteRenderers, buffers, bufferSizes, bounds);
		}
	}
}
