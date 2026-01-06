using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x02000003 RID: 3
[AddComponentMenu("")]
internal class SpriteShapeGeometryCache : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	internal ushort[] indexArray
	{
		get
		{
			return this.m_IndexArray;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	internal Vector3[] posArray
	{
		get
		{
			return this.m_PosArray;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	public Vector4[] tanArray
	{
		get
		{
			return this.m_TanArray;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
	internal int maxArrayCount
	{
		get
		{
			return this.m_MaxArrayCount;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
	internal bool requiresUpdate
	{
		get
		{
			return this.m_RequiresUpdate;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
	internal bool requiresUpload
	{
		get
		{
			return this.m_RequiresUpload;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002080 File Offset: 0x00000280
	private void OnEnable()
	{
		this.m_RequiresUpload = true;
		this.m_RequiresUpdate = false;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002090 File Offset: 0x00000290
	internal void SetGeometryCache(int _maxArrayCount, NativeSlice<Vector3> _posArray, NativeSlice<Vector2> _uv0Array, NativeSlice<Vector4> _tanArray, NativeArray<ushort> _indexArray, NativeArray<SpriteShapeSegment> _geomArray)
	{
		this.m_RequiresUpdate = true;
		this.m_PosArrayCache = _posArray;
		this.m_Uv0ArrayCache = _uv0Array;
		this.m_TanArrayCache = _tanArray;
		this.m_GeomArrayCache = _geomArray;
		this.m_IndexArrayCache = _indexArray;
		this.m_MaxArrayCount = _maxArrayCount;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020C8 File Offset: 0x000002C8
	internal void UpdateGeometryCache()
	{
		if (this.m_RequiresUpdate && this.m_GeomArrayCache.IsCreated && this.m_IndexArrayCache.IsCreated)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < this.m_GeomArrayCache.Length; i++)
			{
				SpriteShapeSegment spriteShapeSegment = this.m_GeomArrayCache[i];
				num2 += spriteShapeSegment.indexCount;
				num3 += spriteShapeSegment.vertexCount;
				if (spriteShapeSegment.vertexCount > 0)
				{
					num = i + 1;
				}
			}
			this.m_GeomArray = new SpriteShapeGeometryInfo[num];
			NativeArray<SpriteShapeGeometryInfo> nativeArray = this.m_GeomArrayCache.Reinterpret<SpriteShapeGeometryInfo>();
			SpriteShapeCopyUtility<SpriteShapeGeometryInfo>.Copy(this.m_GeomArray, nativeArray, num);
			this.m_PosArray = new Vector3[num3];
			this.m_Uv0Array = new Vector2[num3];
			this.m_IndexArray = new ushort[num2];
			SpriteShapeCopyUtility<ushort>.Copy(this.m_IndexArray, this.m_IndexArrayCache, num2);
			SpriteShapeCopyUtility<Vector3>.Copy(this.m_PosArray, this.m_PosArrayCache, num3);
			SpriteShapeCopyUtility<Vector2>.Copy(this.m_Uv0Array, this.m_Uv0ArrayCache, num3);
			this.m_TanArray = new Vector4[(this.m_TanArrayCache.Length >= num3) ? num3 : 1];
			if (this.m_TanArrayCache.Length >= num3)
			{
				SpriteShapeCopyUtility<Vector4>.Copy(this.m_TanArray, this.m_TanArrayCache, num3);
			}
			this.m_MaxArrayCount = ((num3 > num2) ? num3 : num2);
			this.m_RequiresUpdate = false;
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002230 File Offset: 0x00000430
	internal JobHandle Upload(SpriteShapeRenderer sr, SpriteShapeController sc)
	{
		JobHandle jobHandle = default(JobHandle);
		if (this.m_RequiresUpload)
		{
			sr.GetSegments(this.m_GeomArray.Length).Reinterpret<SpriteShapeGeometryInfo>().CopyFrom(this.m_GeomArray);
			NativeArray<ushort> nativeArray;
			NativeSlice<Vector3> nativeSlice;
			NativeSlice<Vector2> nativeSlice2;
			if (sc.enableTangents && this.m_TanArray.Length > 1)
			{
				NativeSlice<Vector4> nativeSlice3;
				sr.GetChannels(this.m_MaxArrayCount, out nativeArray, out nativeSlice, out nativeSlice2, out nativeSlice3);
				SpriteShapeCopyUtility<Vector4>.Copy(nativeSlice3, this.m_TanArray, this.m_TanArray.Length);
			}
			else
			{
				sr.GetChannels(this.m_MaxArrayCount, out nativeArray, out nativeSlice, out nativeSlice2);
			}
			SpriteShapeCopyUtility<Vector3>.Copy(nativeSlice, this.m_PosArray, this.m_PosArray.Length);
			SpriteShapeCopyUtility<Vector2>.Copy(nativeSlice2, this.m_Uv0Array, this.m_Uv0Array.Length);
			SpriteShapeCopyUtility<ushort>.Copy(nativeArray, this.m_IndexArray, this.m_IndexArray.Length);
			sr.Prepare(jobHandle, sc.spriteShapeParameters, sc.spriteArray);
			this.m_RequiresUpload = false;
		}
		return jobHandle;
	}

	// Token: 0x04000005 RID: 5
	[SerializeField]
	[HideInInspector]
	private int m_MaxArrayCount;

	// Token: 0x04000006 RID: 6
	[SerializeField]
	[HideInInspector]
	private Vector3[] m_PosArray;

	// Token: 0x04000007 RID: 7
	[SerializeField]
	[HideInInspector]
	private Vector2[] m_Uv0Array;

	// Token: 0x04000008 RID: 8
	[SerializeField]
	[HideInInspector]
	private Vector4[] m_TanArray;

	// Token: 0x04000009 RID: 9
	[SerializeField]
	[HideInInspector]
	private ushort[] m_IndexArray;

	// Token: 0x0400000A RID: 10
	[SerializeField]
	[HideInInspector]
	private SpriteShapeGeometryInfo[] m_GeomArray;

	// Token: 0x0400000B RID: 11
	private bool m_RequiresUpdate;

	// Token: 0x0400000C RID: 12
	private bool m_RequiresUpload;

	// Token: 0x0400000D RID: 13
	private NativeSlice<Vector3> m_PosArrayCache;

	// Token: 0x0400000E RID: 14
	private NativeSlice<Vector2> m_Uv0ArrayCache;

	// Token: 0x0400000F RID: 15
	private NativeSlice<Vector4> m_TanArrayCache;

	// Token: 0x04000010 RID: 16
	private NativeArray<ushort> m_IndexArrayCache;

	// Token: 0x04000011 RID: 17
	private NativeArray<SpriteShapeSegment> m_GeomArrayCache;
}
