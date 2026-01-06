using System;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x0200021A RID: 538
	public struct JobAllocateNodes : IJob
	{
		// Token: 0x06000CDF RID: 3295 RVA: 0x00050850 File Offset: 0x0004EA50
		public unsafe void Execute()
		{
			int3 size = this.dataBounds.size;
			UnsafeSpan<float4> unsafeSpan = this.nodeNormals.AsUnsafeReadOnlySpan<float4>();
			for (int i = 1; i < size.y; i++)
			{
				for (int j = 0; j < size.z; j++)
				{
					int num = ((i + this.dataBounds.min.y) * this.nodeArrayBounds.z + (j + this.dataBounds.min.z)) * this.nodeArrayBounds.x + this.dataBounds.min.x;
					for (int k = 0; k < size.x; k++)
					{
						int num2 = num + k;
						bool flag = math.any(*unsafeSpan[num2]);
						GridNodeBase gridNodeBase = this.nodes[num2];
						bool flag2 = gridNodeBase != null;
						if (flag != flag2)
						{
							if (flag)
							{
								gridNodeBase = (this.nodes[num2] = this.newGridNodeDelegate());
								this.active.InitializeNode(gridNodeBase);
							}
							else
							{
								gridNodeBase.ClearCustomConnections(true);
								gridNodeBase.ResetConnectionsInternal();
								gridNodeBase.Destroy();
								this.nodes[num2] = null;
							}
						}
					}
				}
			}
		}

		// Token: 0x040009CA RID: 2506
		public AstarPath active;

		// Token: 0x040009CB RID: 2507
		[ReadOnly]
		public NativeArray<float4> nodeNormals;

		// Token: 0x040009CC RID: 2508
		public IntBounds dataBounds;

		// Token: 0x040009CD RID: 2509
		public int3 nodeArrayBounds;

		// Token: 0x040009CE RID: 2510
		public GridNodeBase[] nodes;

		// Token: 0x040009CF RID: 2511
		public Func<GridNodeBase> newGridNodeDelegate;
	}
}
