using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200041B RID: 1051
	[UsedByNativeCode]
	public struct VisibleReflectionProbe : IEquatable<VisibleReflectionProbe>
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x0003DCC5 File Offset: 0x0003BEC5
		public Texture texture
		{
			get
			{
				return (Texture)Object.FindObjectFromInstanceID(this.m_TextureId);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x0003DCD7 File Offset: 0x0003BED7
		public ReflectionProbe reflectionProbe
		{
			get
			{
				return (ReflectionProbe)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x0003DCEC File Offset: 0x0003BEEC
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x0003DD04 File Offset: 0x0003BF04
		public Bounds bounds
		{
			get
			{
				return this.m_Bounds;
			}
			set
			{
				this.m_Bounds = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x0003DD10 File Offset: 0x0003BF10
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x0003DD28 File Offset: 0x0003BF28
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_LocalToWorldMatrix;
			}
			set
			{
				this.m_LocalToWorldMatrix = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x0003DD34 File Offset: 0x0003BF34
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x0003DD4C File Offset: 0x0003BF4C
		public Vector4 hdrData
		{
			get
			{
				return this.m_HdrData;
			}
			set
			{
				this.m_HdrData = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x0003DD58 File Offset: 0x0003BF58
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x0003DD70 File Offset: 0x0003BF70
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x0003DD7C File Offset: 0x0003BF7C
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x0003DD94 File Offset: 0x0003BF94
		public float blendDistance
		{
			get
			{
				return this.m_BlendDistance;
			}
			set
			{
				this.m_BlendDistance = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x0003DDA0 File Offset: 0x0003BFA0
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		public int importance
		{
			get
			{
				return this.m_Importance;
			}
			set
			{
				this.m_Importance = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x0003DDE1 File Offset: 0x0003BFE1
		public bool isBoxProjection
		{
			get
			{
				return Convert.ToBoolean(this.m_BoxProjection);
			}
			set
			{
				this.m_BoxProjection = Convert.ToInt32(value);
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0003DDF0 File Offset: 0x0003BFF0
		public bool Equals(VisibleReflectionProbe other)
		{
			return this.m_Bounds.Equals(other.m_Bounds) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_HdrData.Equals(other.m_HdrData) && this.m_Center.Equals(other.m_Center) && this.m_BlendDistance.Equals(other.m_BlendDistance) && this.m_Importance == other.m_Importance && this.m_BoxProjection == other.m_BoxProjection && this.m_InstanceId == other.m_InstanceId && this.m_TextureId == other.m_TextureId;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleReflectionProbe && this.Equals((VisibleReflectionProbe)obj);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x0003DED8 File Offset: 0x0003C0D8
		public override int GetHashCode()
		{
			int num = this.m_Bounds.GetHashCode();
			num = (num * 397) ^ this.m_LocalToWorldMatrix.GetHashCode();
			num = (num * 397) ^ this.m_HdrData.GetHashCode();
			num = (num * 397) ^ this.m_Center.GetHashCode();
			num = (num * 397) ^ this.m_BlendDistance.GetHashCode();
			num = (num * 397) ^ this.m_Importance;
			num = (num * 397) ^ this.m_BoxProjection;
			num = (num * 397) ^ this.m_InstanceId;
			return (num * 397) ^ this.m_TextureId;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0003DF9C File Offset: 0x0003C19C
		public static bool operator ==(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return left.Equals(right);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0003DFB8 File Offset: 0x0003C1B8
		public static bool operator !=(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D92 RID: 3474
		private Bounds m_Bounds;

		// Token: 0x04000D93 RID: 3475
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x04000D94 RID: 3476
		private Vector4 m_HdrData;

		// Token: 0x04000D95 RID: 3477
		private Vector3 m_Center;

		// Token: 0x04000D96 RID: 3478
		private float m_BlendDistance;

		// Token: 0x04000D97 RID: 3479
		private int m_Importance;

		// Token: 0x04000D98 RID: 3480
		private int m_BoxProjection;

		// Token: 0x04000D99 RID: 3481
		private int m_InstanceId;

		// Token: 0x04000D9A RID: 3482
		private int m_TextureId;
	}
}
