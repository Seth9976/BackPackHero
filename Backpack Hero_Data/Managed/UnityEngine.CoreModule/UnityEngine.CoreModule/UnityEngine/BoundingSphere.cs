using System;

namespace UnityEngine
{
	// Token: 0x020000FF RID: 255
	public struct BoundingSphere
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00007F1E File Offset: 0x0000611E
		public BoundingSphere(Vector3 pos, float rad)
		{
			this.position = pos;
			this.radius = rad;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00007F2F File Offset: 0x0000612F
		public BoundingSphere(Vector4 packedSphere)
		{
			this.position = new Vector3(packedSphere.x, packedSphere.y, packedSphere.z);
			this.radius = packedSphere.w;
		}

		// Token: 0x04000363 RID: 867
		public Vector3 position;

		// Token: 0x04000364 RID: 868
		public float radius;
	}
}
