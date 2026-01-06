using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040F RID: 1039
	[UsedByNativeCode]
	public struct ShadowSplitData : IEquatable<ShadowSplitData>
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x0003C788 File Offset: 0x0003A988
		// (set) Token: 0x060023E0 RID: 9184 RVA: 0x0003C7A0 File Offset: 0x0003A9A0
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to ShadowSplitData.maximumCullingPlaneCount ({1}), but was {2}.", 0, 10, value));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x0003C800 File Offset: 0x0003AA00
		public Vector4 cullingSphere
		{
			get
			{
				return this.m_CullingSphere;
			}
			set
			{
				this.m_CullingSphere = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x0003C80C File Offset: 0x0003AA0C
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x0003C824 File Offset: 0x0003AA24
		public float shadowCascadeBlendCullingFactor
		{
			get
			{
				return this.m_ShadowCascadeBlendCullingFactor;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to {1}, but was {2}.", 0, 1, value));
				}
				this.m_ShadowCascadeBlendCullingFactor = value;
			}
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x0003C874 File Offset: 0x0003AA74
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0003C8F0 File Offset: 0x0003AAF0
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x0003C968 File Offset: 0x0003AB68
		public bool Equals(ShadowSplitData other)
		{
			bool flag = this.m_CullingPlaneCount != other.m_CullingPlaneCount;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.cullingPlaneCount; i++)
				{
					bool flag3 = !this.GetCullingPlane(i).Equals(other.GetCullingPlane(i));
					if (flag3)
					{
						return false;
					}
				}
				flag2 = this.m_CullingSphere.Equals(other.m_CullingSphere);
			}
			return flag2;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x0003C9F0 File Offset: 0x0003ABF0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowSplitData && this.Equals((ShadowSplitData)obj);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0003CA28 File Offset: 0x0003AC28
		public override int GetHashCode()
		{
			return (this.m_CullingPlaneCount * 397) ^ this.m_CullingSphere.GetHashCode();
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0003CA5C File Offset: 0x0003AC5C
		public static bool operator ==(ShadowSplitData left, ShadowSplitData right)
		{
			return left.Equals(right);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x0003CA78 File Offset: 0x0003AC78
		public static bool operator !=(ShadowSplitData left, ShadowSplitData right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D37 RID: 3383
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000D38 RID: 3384
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000D39 RID: 3385
		private int m_CullingPlaneCount;

		// Token: 0x04000D3A RID: 3386
		[FixedBuffer(typeof(byte), 160)]
		internal ShadowSplitData.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000D3B RID: 3387
		private Vector4 m_CullingSphere;

		// Token: 0x04000D3C RID: 3388
		private float m_ShadowCascadeBlendCullingFactor;

		// Token: 0x04000D3D RID: 3389
		private float m_CullingNearPlane;

		// Token: 0x02000410 RID: 1040
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000D3E RID: 3390
			public byte FixedElementField;
		}
	}
}
