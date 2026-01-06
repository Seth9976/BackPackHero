using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000A1 RID: 161
	public class VoxelArea
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0002DE38 File Offset: 0x0002C038
		public void Reset()
		{
			this.ResetLinkedVoxelSpans();
			for (int i = 0; i < this.compactCells.Length; i++)
			{
				this.compactCells[i].count = 0U;
				this.compactCells[i].index = 0U;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002DE84 File Offset: 0x0002C084
		private void ResetLinkedVoxelSpans()
		{
			int num = this.linkedSpans.Length;
			this.linkedSpanCount = this.width * this.depth;
			LinkedVoxelSpan linkedVoxelSpan = new LinkedVoxelSpan(uint.MaxValue, uint.MaxValue, -1, -1);
			for (int i = 0; i < num; i++)
			{
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
			}
			this.removedStackCount = 0;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0002DFE0 File Offset: 0x0002C1E0
		public VoxelArea(int width, int depth)
		{
			this.width = width;
			this.depth = depth;
			int num = width * depth;
			this.compactCells = new CompactVoxelCell[num];
			this.linkedSpans = new LinkedVoxelSpan[((int)((float)num * 8f) + 15) & -16];
			this.ResetLinkedVoxelSpans();
			int[] array = new int[4];
			array[0] = -1;
			array[2] = 1;
			this.DirectionX = array;
			this.DirectionZ = new int[]
			{
				0,
				width,
				0,
				-width
			};
			this.VectorDirection = new Vector3[]
			{
				Vector3.left,
				Vector3.forward,
				Vector3.right,
				Vector3.back
			};
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002E0A8 File Offset: 0x0002C2A8
		public int GetSpanCountAll()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295U)
				{
					num++;
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002E104 File Offset: 0x0002C304
		public int GetSpanCount()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295U)
				{
					if (this.linkedSpans[num3].area != 0)
					{
						num++;
					}
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0002E174 File Offset: 0x0002C374
		private void PushToSpanRemovedStack(int index)
		{
			if (this.removedStackCount == this.removedStack.Length)
			{
				int[] array = new int[this.removedStackCount * 4];
				Buffer.BlockCopy(this.removedStack, 0, array, 0, this.removedStackCount * 4);
				this.removedStack = array;
			}
			this.removedStack[this.removedStackCount] = index;
			this.removedStackCount++;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		public void AddLinkedSpan(int index, uint bottom, uint top, int area, int voxelWalkableClimb)
		{
			LinkedVoxelSpan[] array = this.linkedSpans;
			if (array[index].bottom == 4294967295U)
			{
				array[index] = new LinkedVoxelSpan(bottom, top, area);
				return;
			}
			int num = -1;
			int num2 = index;
			while (index != -1)
			{
				LinkedVoxelSpan linkedVoxelSpan = array[index];
				if (linkedVoxelSpan.bottom > top)
				{
					break;
				}
				if (linkedVoxelSpan.top < bottom)
				{
					num = index;
					index = linkedVoxelSpan.next;
				}
				else
				{
					bottom = Math.Min(linkedVoxelSpan.bottom, bottom);
					top = Math.Max(linkedVoxelSpan.top, top);
					if (Math.Abs((int)(top - linkedVoxelSpan.top)) <= voxelWalkableClimb)
					{
						area = Math.Max(area, linkedVoxelSpan.area);
					}
					int next = linkedVoxelSpan.next;
					if (num != -1)
					{
						array[num].next = next;
						this.PushToSpanRemovedStack(index);
						index = next;
					}
					else
					{
						if (next == -1)
						{
							array[num2] = new LinkedVoxelSpan(bottom, top, area);
							return;
						}
						array[num2] = array[next];
						this.PushToSpanRemovedStack(next);
					}
				}
			}
			if (this.linkedSpanCount >= array.Length)
			{
				LinkedVoxelSpan[] array2 = array;
				int num3 = this.linkedSpanCount;
				int num4 = this.removedStackCount;
				array = (this.linkedSpans = new LinkedVoxelSpan[array.Length * 2]);
				this.ResetLinkedVoxelSpans();
				this.linkedSpanCount = num3;
				this.removedStackCount = num4;
				for (int i = 0; i < this.linkedSpanCount; i++)
				{
					array[i] = array2[i];
				}
			}
			int num5;
			if (this.removedStackCount > 0)
			{
				this.removedStackCount--;
				num5 = this.removedStack[this.removedStackCount];
			}
			else
			{
				num5 = this.linkedSpanCount;
				this.linkedSpanCount++;
			}
			if (num != -1)
			{
				array[num5] = new LinkedVoxelSpan(bottom, top, area, array[num].next);
				array[num].next = num5;
				return;
			}
			array[num5] = array[num2];
			array[num2] = new LinkedVoxelSpan(bottom, top, area, num5);
		}

		// Token: 0x04000440 RID: 1088
		public const uint MaxHeight = 65536U;

		// Token: 0x04000441 RID: 1089
		public const int MaxHeightInt = 65536;

		// Token: 0x04000442 RID: 1090
		public const uint InvalidSpanValue = 4294967295U;

		// Token: 0x04000443 RID: 1091
		public const float AvgSpanLayerCountEstimate = 8f;

		// Token: 0x04000444 RID: 1092
		public readonly int width;

		// Token: 0x04000445 RID: 1093
		public readonly int depth;

		// Token: 0x04000446 RID: 1094
		public CompactVoxelSpan[] compactSpans;

		// Token: 0x04000447 RID: 1095
		public CompactVoxelCell[] compactCells;

		// Token: 0x04000448 RID: 1096
		public int compactSpanCount;

		// Token: 0x04000449 RID: 1097
		public ushort[] tmpUShortArr;

		// Token: 0x0400044A RID: 1098
		public int[] areaTypes;

		// Token: 0x0400044B RID: 1099
		public ushort[] dist;

		// Token: 0x0400044C RID: 1100
		public ushort maxDistance;

		// Token: 0x0400044D RID: 1101
		public int maxRegions;

		// Token: 0x0400044E RID: 1102
		public int[] DirectionX;

		// Token: 0x0400044F RID: 1103
		public int[] DirectionZ;

		// Token: 0x04000450 RID: 1104
		public Vector3[] VectorDirection;

		// Token: 0x04000451 RID: 1105
		private int linkedSpanCount;

		// Token: 0x04000452 RID: 1106
		public LinkedVoxelSpan[] linkedSpans;

		// Token: 0x04000453 RID: 1107
		private int[] removedStack = new int[128];

		// Token: 0x04000454 RID: 1108
		private int removedStackCount;
	}
}
