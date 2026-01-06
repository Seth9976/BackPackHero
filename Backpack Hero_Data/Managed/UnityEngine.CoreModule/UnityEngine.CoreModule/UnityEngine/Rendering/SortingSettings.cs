using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000414 RID: 1044
	public struct SortingSettings : IEquatable<SortingSettings>
	{
		// Token: 0x060023F8 RID: 9208 RVA: 0x0003CBF4 File Offset: 0x0003ADF4
		public SortingSettings(Camera camera)
		{
			ScriptableRenderContext.InitializeSortSettings(camera, out this);
			this.m_Criteria = this.criteria;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		// (set) Token: 0x060023FA RID: 9210 RVA: 0x0003CC24 File Offset: 0x0003AE24
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				return this.m_WorldToCameraMatrix;
			}
			set
			{
				this.m_WorldToCameraMatrix = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x0003CC30 File Offset: 0x0003AE30
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x0003CC48 File Offset: 0x0003AE48
		public Vector3 cameraPosition
		{
			get
			{
				return this.m_CameraPosition;
			}
			set
			{
				this.m_CameraPosition = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x0003CC54 File Offset: 0x0003AE54
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x0003CC6C File Offset: 0x0003AE6C
		public Vector3 customAxis
		{
			get
			{
				return this.m_CustomAxis;
			}
			set
			{
				this.m_CustomAxis = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x0003CC78 File Offset: 0x0003AE78
		// (set) Token: 0x06002400 RID: 9216 RVA: 0x0003CC90 File Offset: 0x0003AE90
		public SortingCriteria criteria
		{
			get
			{
				return this.m_Criteria;
			}
			set
			{
				this.m_Criteria = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x0003CCB4 File Offset: 0x0003AEB4
		public DistanceMetric distanceMetric
		{
			get
			{
				return this.m_DistanceMetric;
			}
			set
			{
				this.m_DistanceMetric = value;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0003CCC0 File Offset: 0x0003AEC0
		public bool Equals(SortingSettings other)
		{
			return this.m_WorldToCameraMatrix.Equals(other.m_WorldToCameraMatrix) && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_CustomAxis.Equals(other.m_CustomAxis) && this.m_Criteria == other.m_Criteria && this.m_DistanceMetric == other.m_DistanceMetric && this.m_PreviousVPMatrix.Equals(other.m_PreviousVPMatrix) && this.m_NonJitteredVPMatrix.Equals(other.m_NonJitteredVPMatrix);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0003CD50 File Offset: 0x0003AF50
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SortingSettings && this.Equals((SortingSettings)obj);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0003CD88 File Offset: 0x0003AF88
		public override int GetHashCode()
		{
			int num = this.m_WorldToCameraMatrix.GetHashCode();
			num = (num * 397) ^ this.m_CameraPosition.GetHashCode();
			num = (num * 397) ^ this.m_CustomAxis.GetHashCode();
			num = (num * 397) ^ (int)this.m_Criteria;
			num = (num * 397) ^ (int)this.m_DistanceMetric;
			num = (num * 397) ^ this.m_PreviousVPMatrix.GetHashCode();
			return (num * 397) ^ this.m_NonJitteredVPMatrix.GetHashCode();
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0003CE34 File Offset: 0x0003B034
		public static bool operator ==(SortingSettings left, SortingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0003CE50 File Offset: 0x0003B050
		public static bool operator !=(SortingSettings left, SortingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D50 RID: 3408
		private Matrix4x4 m_WorldToCameraMatrix;

		// Token: 0x04000D51 RID: 3409
		private Vector3 m_CameraPosition;

		// Token: 0x04000D52 RID: 3410
		private Vector3 m_CustomAxis;

		// Token: 0x04000D53 RID: 3411
		private SortingCriteria m_Criteria;

		// Token: 0x04000D54 RID: 3412
		private DistanceMetric m_DistanceMetric;

		// Token: 0x04000D55 RID: 3413
		private Matrix4x4 m_PreviousVPMatrix;

		// Token: 0x04000D56 RID: 3414
		private Matrix4x4 m_NonJitteredVPMatrix;
	}
}
