using System;
using Pathfinding.Graphs.Navmesh.Jobs;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001B3 RID: 435
	public struct RecastBuilder
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00040FCC File Offset: 0x0003F1CC
		public static TileBuilder BuildTileMeshes(RecastGraph graph, TileLayout tileLayout, IntRect tileRect)
		{
			return new TileBuilder(graph, tileLayout, tileRect);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00040FD6 File Offset: 0x0003F1D6
		public static JobBuildNodes BuildNodeTiles(RecastGraph graph, TileLayout tileLayout)
		{
			return new JobBuildNodes(graph, tileLayout);
		}
	}
}
