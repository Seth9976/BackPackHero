using System;
using Pathfinding.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001D3 RID: 467
	public struct CompactVoxelField : IArenaDisposable
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x0004723C File Offset: 0x0004543C
		public CompactVoxelField(int width, int depth, int voxelWalkableHeight, Allocator allocator)
		{
			this.spans = new NativeList<CompactVoxelSpan>(0, allocator);
			this.cells = new NativeList<CompactVoxelCell>(0, allocator);
			this.areaTypes = new NativeList<int>(0, allocator);
			this.width = width;
			this.depth = depth;
			this.voxelWalkableHeight = voxelWalkableHeight;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00047297 File Offset: 0x00045497
		void IArenaDisposable.DisposeWith(DisposeArena arena)
		{
			arena.Add<CompactVoxelSpan>(this.spans);
			arena.Add<CompactVoxelCell>(this.cells);
			arena.Add<int>(this.areaTypes);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000472BD File Offset: 0x000454BD
		public int GetNeighbourIndex(int index, int direction)
		{
			return index + VoxelUtilityBurst.DX[direction] + VoxelUtilityBurst.DZ[direction] * this.width;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000472D8 File Offset: 0x000454D8
		public void BuildFromLinkedField(LinkedVoxelField field)
		{
			int num = 0;
			int num2 = field.width;
			int num3 = field.depth;
			int num4 = num2 * num3;
			int spanCount = field.GetSpanCount();
			this.spans.Resize(spanCount, NativeArrayOptions.UninitializedMemory);
			this.areaTypes.Resize(spanCount, NativeArrayOptions.UninitializedMemory);
			this.cells.Resize(num4, NativeArrayOptions.UninitializedMemory);
			NativeList<LinkedVoxelSpan> linkedSpans = field.linkedSpans;
			for (int i = 0; i < num4; i += num2)
			{
				for (int j = 0; j < num2; j++)
				{
					int num5 = j + i;
					if (linkedSpans[num5].bottom == 4294967295U)
					{
						this.cells[j + i] = new CompactVoxelCell(0, 0);
					}
					else
					{
						int num6 = num;
						int num7 = 0;
						while (num5 != -1)
						{
							if (linkedSpans[num5].area != 0)
							{
								int top = (int)linkedSpans[num5].top;
								int next = linkedSpans[num5].next;
								int num8 = (int)((next != -1) ? linkedSpans[next].bottom : 65536U);
								this.spans[num] = new CompactVoxelSpan((ushort)math.min(top, 65535), (uint)math.min(num8 - top, 65535));
								this.areaTypes[num] = linkedSpans[num5].area;
								num++;
								num7++;
							}
							num5 = linkedSpans[num5].next;
						}
						this.cells[j + i] = new CompactVoxelCell(num6, num7);
					}
				}
			}
		}

		// Token: 0x0400087D RID: 2173
		public const int UnwalkableArea = 0;

		// Token: 0x0400087E RID: 2174
		public const uint NotConnected = 63U;

		// Token: 0x0400087F RID: 2175
		public readonly int voxelWalkableHeight;

		// Token: 0x04000880 RID: 2176
		public readonly int width;

		// Token: 0x04000881 RID: 2177
		public readonly int depth;

		// Token: 0x04000882 RID: 2178
		public NativeList<CompactVoxelSpan> spans;

		// Token: 0x04000883 RID: 2179
		public NativeList<CompactVoxelCell> cells;

		// Token: 0x04000884 RID: 2180
		public NativeList<int> areaTypes;

		// Token: 0x04000885 RID: 2181
		public const int MaxLayers = 65535;
	}
}
