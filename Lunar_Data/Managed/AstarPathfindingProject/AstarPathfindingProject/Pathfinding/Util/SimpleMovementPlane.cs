using System;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200026B RID: 619
	public readonly struct SimpleMovementPlane : IMovementPlane
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0005AF8C File Offset: 0x0005918C
		public bool isXY
		{
			get
			{
				return this.plane == 1;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0005AF97 File Offset: 0x00059197
		public bool isXZ
		{
			get
			{
				return this.plane == 2;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0005AFA4 File Offset: 0x000591A4
		public SimpleMovementPlane(Quaternion rotation)
		{
			this.rotation = rotation;
			this.inverseRotation = Quaternion.Inverse(rotation);
			if (rotation == SimpleMovementPlane.XYPlane.rotation)
			{
				this.plane = 1;
				return;
			}
			if (rotation == Quaternion.identity)
			{
				this.plane = 2;
				return;
			}
			this.plane = 0;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0005AFFC File Offset: 0x000591FC
		public Vector2 ToPlane(Vector3 point)
		{
			if (this.isXY)
			{
				return new Vector2(point.x, point.y);
			}
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			return new Vector2(point.x, point.z);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0005B04C File Offset: 0x0005924C
		public float2 ToPlane(float3 point)
		{
			return (this.inverseRotation * point).xz;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0005B077 File Offset: 0x00059277
		public Vector2 ToPlane(Vector3 point, out float elevation)
		{
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			elevation = point.y;
			return new Vector2(point.x, point.z);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0005B0A8 File Offset: 0x000592A8
		public float2 ToPlane(float3 point, out float elevation)
		{
			point = math.mul(this.inverseRotation, point);
			elevation = point.y;
			return point.xz;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0005B0CC File Offset: 0x000592CC
		public Vector3 ToWorld(Vector2 point, float elevation = 0f)
		{
			return this.rotation * new Vector3(point.x, elevation, point.y);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0005B0EB File Offset: 0x000592EB
		public float3 ToWorld(float2 point, float elevation = 0f)
		{
			return this.rotation * new Vector3(point.x, elevation, point.y);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0005B10F File Offset: 0x0005930F
		public SimpleMovementPlane ToSimpleMovementPlane()
		{
			return this;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0005B117 File Offset: 0x00059317
		public static bool operator ==(SimpleMovementPlane lhs, SimpleMovementPlane rhs)
		{
			return lhs.rotation == rhs.rotation;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0005B12A File Offset: 0x0005932A
		public static bool operator !=(SimpleMovementPlane lhs, SimpleMovementPlane rhs)
		{
			return lhs.rotation != rhs.rotation;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0005B13D File Offset: 0x0005933D
		public override bool Equals(object other)
		{
			return other is SimpleMovementPlane && this.rotation == ((SimpleMovementPlane)other).rotation;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0005B160 File Offset: 0x00059360
		public override int GetHashCode()
		{
			return this.rotation.GetHashCode();
		}

		// Token: 0x04000B00 RID: 2816
		public readonly Quaternion rotation;

		// Token: 0x04000B01 RID: 2817
		public readonly Quaternion inverseRotation;

		// Token: 0x04000B02 RID: 2818
		private readonly byte plane;

		// Token: 0x04000B03 RID: 2819
		public static readonly SimpleMovementPlane XYPlane = new SimpleMovementPlane(Quaternion.Euler(-90f, 0f, 0f));

		// Token: 0x04000B04 RID: 2820
		public static readonly SimpleMovementPlane XZPlane = new SimpleMovementPlane(Quaternion.identity);
	}
}
