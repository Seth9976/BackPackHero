using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003FE RID: 1022
	public struct LODParameters : IEquatable<LODParameters>
	{
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x0003A7A8 File Offset: 0x000389A8
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x0003A7C5 File Offset: 0x000389C5
		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x0003A7D4 File Offset: 0x000389D4
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x0003A7EC File Offset: 0x000389EC
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

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0003A7F8 File Offset: 0x000389F8
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x0003A810 File Offset: 0x00038A10
		public float fieldOfView
		{
			get
			{
				return this.m_FieldOfView;
			}
			set
			{
				this.m_FieldOfView = value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x0003A81C File Offset: 0x00038A1C
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x0003A834 File Offset: 0x00038A34
		public float orthoSize
		{
			get
			{
				return this.m_OrthoSize;
			}
			set
			{
				this.m_OrthoSize = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x0003A840 File Offset: 0x00038A40
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x0003A858 File Offset: 0x00038A58
		public int cameraPixelHeight
		{
			get
			{
				return this.m_CameraPixelHeight;
			}
			set
			{
				this.m_CameraPixelHeight = value;
			}
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x0003A864 File Offset: 0x00038A64
		public bool Equals(LODParameters other)
		{
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_FieldOfView.Equals(other.m_FieldOfView) && this.m_OrthoSize.Equals(other.m_OrthoSize) && this.m_CameraPixelHeight == other.m_CameraPixelHeight;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0003A8D0 File Offset: 0x00038AD0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LODParameters && this.Equals((LODParameters)obj);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0003A908 File Offset: 0x00038B08
		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397) ^ this.m_CameraPosition.GetHashCode();
			num = (num * 397) ^ this.m_FieldOfView.GetHashCode();
			num = (num * 397) ^ this.m_OrthoSize.GetHashCode();
			return (num * 397) ^ this.m_CameraPixelHeight;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0003A974 File Offset: 0x00038B74
		public static bool operator ==(LODParameters left, LODParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0003A990 File Offset: 0x00038B90
		public static bool operator !=(LODParameters left, LODParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CE5 RID: 3301
		private int m_IsOrthographic;

		// Token: 0x04000CE6 RID: 3302
		private Vector3 m_CameraPosition;

		// Token: 0x04000CE7 RID: 3303
		private float m_FieldOfView;

		// Token: 0x04000CE8 RID: 3304
		private float m_OrthoSize;

		// Token: 0x04000CE9 RID: 3305
		private int m_CameraPixelHeight;
	}
}
