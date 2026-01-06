using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001F4 RID: 500
	[BurstCompile(FloatMode = FloatMode.Fast)]
	public struct JobTransformTileCoordinates : IJob
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x0004D570 File Offset: 0x0004B770
		public unsafe void Execute()
		{
			int num = this.vertices->Length / UnsafeUtility.SizeOf<Int3>();
			for (int i = 0; i < num; i++)
			{
				Int3* ptr = (Int3*)(this.vertices->Ptr + (IntPtr)i * (IntPtr)sizeof(Int3));
				Vector3 vector = new Vector3((float)ptr->x, (float)ptr->y, (float)ptr->z);
				*ptr = (Int3)this.matrix.MultiplyPoint3x4(vector);
			}
		}

		// Token: 0x0400093E RID: 2366
		public unsafe UnsafeAppendBuffer* vertices;

		// Token: 0x0400093F RID: 2367
		public Matrix4x4 matrix;
	}
}
