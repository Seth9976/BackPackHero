using System;
using System.Runtime.InteropServices;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001F5 RID: 501
	public struct JobWriteNodeConnections : IJob
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x0004D5E4 File Offset: 0x0004B7E4
		public void Execute()
		{
			NavmeshTile[] array = (NavmeshTile[])this.tiles.Target;
			for (int i = 0; i < array.Length; i++)
			{
				JobCalculateTriangleConnections.TileNodeConnectionsUnsafe tileNodeConnectionsUnsafe = this.nodeConnections[i];
				this.Apply(array[i].nodes, tileNodeConnectionsUnsafe);
				tileNodeConnectionsUnsafe.neighbourCounts.Dispose();
				tileNodeConnectionsUnsafe.neighbours.Dispose();
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0004D644 File Offset: 0x0004B844
		private void Apply(TriangleMeshNode[] nodes, JobCalculateTriangleConnections.TileNodeConnectionsUnsafe connections)
		{
			UnsafeAppendBuffer.Reader reader = connections.neighbourCounts.AsReader();
			UnsafeAppendBuffer.Reader reader2 = connections.neighbours.AsReader();
			foreach (TriangleMeshNode triangleMeshNode in nodes)
			{
				int num = reader.ReadNext<int>();
				Connection[] array = (triangleMeshNode.connections = ArrayPool<Connection>.ClaimWithExactLength(num));
				for (int j = 0; j < num; j++)
				{
					int num2 = reader2.ReadNext<int>();
					byte b = (byte)reader2.ReadNext<int>();
					TriangleMeshNode triangleMeshNode2 = nodes[num2];
					int costMagnitude = (triangleMeshNode.position - triangleMeshNode2.position).costMagnitude;
					array[j] = new Connection(triangleMeshNode2, (uint)costMagnitude, b);
				}
			}
		}

		// Token: 0x04000940 RID: 2368
		[ReadOnly]
		public NativeArray<JobCalculateTriangleConnections.TileNodeConnectionsUnsafe> nodeConnections;

		// Token: 0x04000941 RID: 2369
		public GCHandle tiles;
	}
}
