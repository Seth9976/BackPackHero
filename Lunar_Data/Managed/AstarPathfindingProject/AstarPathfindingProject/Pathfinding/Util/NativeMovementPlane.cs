using System;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200026A RID: 618
	public readonly struct NativeMovementPlane
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0005AD30 File Offset: 0x00058F30
		public float3 up
		{
			get
			{
				return 2f * new float3(this.rotation.value.x * this.rotation.value.y - this.rotation.value.w * this.rotation.value.z, 0.5f - this.rotation.value.x * this.rotation.value.x - this.rotation.value.z * this.rotation.value.z, this.rotation.value.w * this.rotation.value.x + this.rotation.value.y * this.rotation.value.z);
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0005AE1B File Offset: 0x0005901B
		public NativeMovementPlane(quaternion rotation)
		{
			this.rotation = math.normalizesafe(rotation);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0005AE29 File Offset: 0x00059029
		public NativeMovementPlane(SimpleMovementPlane plane)
		{
			this = new NativeMovementPlane(plane.rotation);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0005AE3C File Offset: 0x0005903C
		public ToPlaneMatrix AsWorldToPlaneMatrix()
		{
			return new ToPlaneMatrix(this);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0005AE49 File Offset: 0x00059049
		public ToWorldMatrix AsPlaneToWorldMatrix()
		{
			return new ToWorldMatrix(this);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0005AE56 File Offset: 0x00059056
		public float ProjectedLength(float3 v)
		{
			return math.length(this.ToPlane(v));
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0005AE64 File Offset: 0x00059064
		public float2 ToPlane(float3 p)
		{
			return math.mul(math.conjugate(this.rotation), p).xz;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0005AE8A File Offset: 0x0005908A
		public float2 ToPlane(float3 p, out float elevation)
		{
			p = math.mul(math.conjugate(this.rotation), p);
			elevation = p.y;
			return p.xz;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0005AEAE File Offset: 0x000590AE
		public float3 ToWorld(float2 p, float elevation = 0f)
		{
			return math.mul(this.rotation, new float3(p.x, elevation, p.y));
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0005AED0 File Offset: 0x000590D0
		public float ToPlane(quaternion rotation)
		{
			quaternion quaternion = math.mul(math.conjugate(this.rotation), rotation);
			if (quaternion.value.y < 0f)
			{
				quaternion.value = -quaternion.value;
			}
			return -VectorMath.QuaternionAngle(math.normalizesafe(new quaternion(0f, quaternion.value.y, 0f, quaternion.value.w)));
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0005AF43 File Offset: 0x00059143
		public quaternion ToWorldRotation(float angle)
		{
			return math.mul(this.rotation, quaternion.RotateY(-angle));
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0005AF57 File Offset: 0x00059157
		public quaternion ToWorldRotationDelta(float deltaAngle)
		{
			return quaternion.AxisAngle(this.ToWorld(float2.zero, 1f), -deltaAngle);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0005AF70 File Offset: 0x00059170
		public Bounds ToWorld(Bounds bounds)
		{
			return this.AsPlaneToWorldMatrix().ToWorld(bounds);
		}

		// Token: 0x04000AFF RID: 2815
		public readonly quaternion rotation;
	}
}
