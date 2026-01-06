using System;
using Pathfinding.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001D7 RID: 471
	public struct LinkedVoxelField : IArenaDisposable
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x000474E8 File Offset: 0x000456E8
		public LinkedVoxelField(int width, int depth, int height)
		{
			this.width = width;
			this.depth = depth;
			this.height = height;
			this.flatten = true;
			this.linkedSpans = new NativeList<LinkedVoxelSpan>(0, Allocator.Persistent);
			this.removedStack = new NativeList<int>(128, Allocator.Persistent);
			this.linkedCellMinMax = new NativeList<CellMinMax>(0, Allocator.Persistent);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0004754B File Offset: 0x0004574B
		void IArenaDisposable.DisposeWith(DisposeArena arena)
		{
			arena.Add<LinkedVoxelSpan>(this.linkedSpans);
			arena.Add<int>(this.removedStack);
			arena.Add<CellMinMax>(this.linkedCellMinMax);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00047574 File Offset: 0x00045774
		public void ResetLinkedVoxelSpans()
		{
			int num = this.width * this.depth;
			LinkedVoxelSpan linkedVoxelSpan = new LinkedVoxelSpan(uint.MaxValue, uint.MaxValue, -1, -1);
			this.linkedSpans.ResizeUninitialized(num);
			this.linkedCellMinMax.Resize(num, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < num; i++)
			{
				this.linkedSpans[i] = linkedVoxelSpan;
				this.linkedCellMinMax[i] = new CellMinMax
				{
					objectID = -1,
					min = 0,
					max = 0
				};
			}
			this.removedStack.Clear();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00047604 File Offset: 0x00045804
		private void PushToSpanRemovedStack(int index)
		{
			this.removedStack.Add(in index);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00047614 File Offset: 0x00045814
		public int GetSpanCount()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295U)
				{
					num += ((this.linkedSpans[num3].area != 0) ? 1 : 0);
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00047688 File Offset: 0x00045888
		public void ResolveSolid(int index, int objectID, int voxelWalkableClimb)
		{
			CellMinMax cellMinMax = this.linkedCellMinMax[index];
			if (cellMinMax.objectID != objectID)
			{
				return;
			}
			if (cellMinMax.min < cellMinMax.max - 1)
			{
				this.AddLinkedSpan(index, cellMinMax.min, cellMinMax.max - 1, 0, voxelWalkableClimb, objectID);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000476D4 File Offset: 0x000458D4
		public void SetWalkableBackground()
		{
			int num = this.width * this.depth;
			for (int i = 0; i < num; i++)
			{
				this.linkedSpans[i] = new LinkedVoxelSpan(0U, 1U, 1);
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00047710 File Offset: 0x00045910
		public void AddFlattenedSpan(int index, int area)
		{
			if (this.linkedSpans[index].bottom == 4294967295U)
			{
				this.linkedSpans[index] = new LinkedVoxelSpan(0U, 1U, area);
				return;
			}
			this.linkedSpans[index] = new LinkedVoxelSpan(0U, 1U, (this.linkedSpans[index].area == 0 || area == 0) ? 0 : math.max(this.linkedSpans[index].area, area));
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0004778C File Offset: 0x0004598C
		public void AddLinkedSpan(int index, int bottom, int top, int area, int voxelWalkableClimb, int objectID)
		{
			CellMinMax cellMinMax = this.linkedCellMinMax[index];
			if (cellMinMax.objectID != objectID)
			{
				this.linkedCellMinMax[index] = new CellMinMax
				{
					objectID = objectID,
					min = bottom,
					max = top
				};
			}
			else
			{
				cellMinMax.min = math.min(cellMinMax.min, bottom);
				cellMinMax.max = math.max(cellMinMax.max, top);
				this.linkedCellMinMax[index] = cellMinMax;
			}
			top = math.min(top, this.height);
			bottom = math.max(bottom, 0);
			if (bottom >= top)
			{
				return;
			}
			uint num = (uint)top;
			uint num2 = (uint)bottom;
			if (this.linkedSpans[index].bottom == 4294967295U)
			{
				this.linkedSpans[index] = new LinkedVoxelSpan(num2, num, area);
				return;
			}
			int num3 = -1;
			int num4 = index;
			while (index != -1)
			{
				LinkedVoxelSpan linkedVoxelSpan = this.linkedSpans[index];
				if (linkedVoxelSpan.bottom > num)
				{
					break;
				}
				if (linkedVoxelSpan.top < num2)
				{
					num3 = index;
					index = linkedVoxelSpan.next;
				}
				else
				{
					if (math.abs((int)(num - linkedVoxelSpan.top)) < voxelWalkableClimb && (area == 0 || linkedVoxelSpan.area == 0))
					{
						area = math.max(area, linkedVoxelSpan.area);
					}
					else if (num < linkedVoxelSpan.top)
					{
						area = linkedVoxelSpan.area;
					}
					num2 = math.min(linkedVoxelSpan.bottom, num2);
					num = math.max(linkedVoxelSpan.top, num);
					int next = linkedVoxelSpan.next;
					if (num3 != -1)
					{
						LinkedVoxelSpan linkedVoxelSpan2 = this.linkedSpans[num3];
						linkedVoxelSpan2.next = next;
						this.linkedSpans[num3] = linkedVoxelSpan2;
						this.PushToSpanRemovedStack(index);
						index = next;
					}
					else
					{
						if (next == -1)
						{
							this.linkedSpans[num4] = new LinkedVoxelSpan(num2, num, area);
							return;
						}
						this.linkedSpans[num4] = this.linkedSpans[next];
						this.PushToSpanRemovedStack(next);
					}
				}
			}
			int num5;
			if (this.removedStack.Length > 0)
			{
				num5 = this.removedStack[this.removedStack.Length - 1];
				this.removedStack.RemoveAtSwapBack(this.removedStack.Length - 1);
			}
			else
			{
				num5 = this.linkedSpans.Length;
				this.linkedSpans.Resize(this.linkedSpans.Length + 1, NativeArrayOptions.UninitializedMemory);
			}
			if (num3 != -1)
			{
				this.linkedSpans[num5] = new LinkedVoxelSpan(num2, num, area, this.linkedSpans[num3].next);
				LinkedVoxelSpan linkedVoxelSpan3 = this.linkedSpans[num3];
				linkedVoxelSpan3.next = num5;
				this.linkedSpans[num3] = linkedVoxelSpan3;
				return;
			}
			this.linkedSpans[num5] = this.linkedSpans[num4];
			this.linkedSpans[num4] = new LinkedVoxelSpan(num2, num, area, num5);
		}

		// Token: 0x0400088F RID: 2191
		public const uint MaxHeight = 65536U;

		// Token: 0x04000890 RID: 2192
		public const int MaxHeightInt = 65536;

		// Token: 0x04000891 RID: 2193
		public const uint InvalidSpanValue = 4294967295U;

		// Token: 0x04000892 RID: 2194
		public const float AvgSpanLayerCountEstimate = 8f;

		// Token: 0x04000893 RID: 2195
		public int width;

		// Token: 0x04000894 RID: 2196
		public int depth;

		// Token: 0x04000895 RID: 2197
		public int height;

		// Token: 0x04000896 RID: 2198
		public bool flatten;

		// Token: 0x04000897 RID: 2199
		public NativeList<LinkedVoxelSpan> linkedSpans;

		// Token: 0x04000898 RID: 2200
		private NativeList<int> removedStack;

		// Token: 0x04000899 RID: 2201
		private NativeList<CellMinMax> linkedCellMinMax;
	}
}
