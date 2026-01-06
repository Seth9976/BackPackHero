using System;
using Pathfinding.Util;
using Unity.Collections.LowLevel.Unsafe;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001C7 RID: 455
	public struct TileMesh
	{
		// Token: 0x0400085D RID: 2141
		public int[] triangles;

		// Token: 0x0400085E RID: 2142
		public Int3[] verticesInTileSpace;

		// Token: 0x0400085F RID: 2143
		public uint[] tags;

		// Token: 0x020001C8 RID: 456
		public struct TileMeshUnsafe
		{
			// Token: 0x06000BE7 RID: 3047 RVA: 0x00045C5D File Offset: 0x00043E5D
			public void Dispose()
			{
				this.triangles.Dispose();
				this.verticesInTileSpace.Dispose();
				this.tags.Dispose();
			}

			// Token: 0x06000BE8 RID: 3048 RVA: 0x00045C80 File Offset: 0x00043E80
			public TileMesh ToManaged()
			{
				return new TileMesh
				{
					triangles = Memory.UnsafeAppendBufferToArray<int>(this.triangles),
					verticesInTileSpace = Memory.UnsafeAppendBufferToArray<Int3>(this.verticesInTileSpace),
					tags = Memory.UnsafeAppendBufferToArray<uint>(this.tags)
				};
			}

			// Token: 0x04000860 RID: 2144
			public UnsafeAppendBuffer triangles;

			// Token: 0x04000861 RID: 2145
			public UnsafeAppendBuffer verticesInTileSpace;

			// Token: 0x04000862 RID: 2146
			public UnsafeAppendBuffer tags;
		}
	}
}
