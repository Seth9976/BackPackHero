using System;
using Pathfinding.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.Graphs.Grid
{
	// Token: 0x020001FF RID: 511
	public static class GridIterationUtilities
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x0004F034 File Offset: 0x0004D234
		public static void ForEachCellIn3DSlice<T>(Slice3D slice, ref T action) where T : struct, GridIterationUtilities.ISliceAction
		{
			int3 size = slice.slice.size;
			ValueTuple<int, int, int> outerStrides = slice.outerStrides;
			int item = outerStrides.Item2;
			int item2 = outerStrides.Item3;
			int outerStartIndex = slice.outerStartIndex;
			uint num = 0U;
			for (int i = 0; i < size.y; i++)
			{
				for (int j = 0; j < size.z; j++)
				{
					int num2 = i * item + j * item2 + outerStartIndex;
					int k = 0;
					while (k < size.x)
					{
						action.Execute((uint)(num2 + k), num);
						k++;
						num += 1U;
					}
				}
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0004F0D8 File Offset: 0x0004D2D8
		public static void ForEachCellIn3DSliceWithCoords<T>(Slice3D slice, ref T action) where T : struct, GridIterationUtilities.ISliceActionWithCoords
		{
			int3 size = slice.slice.size;
			ValueTuple<int, int, int> outerStrides = slice.outerStrides;
			int item = outerStrides.Item2;
			int item2 = outerStrides.Item3;
			int outerStartIndex = slice.outerStartIndex;
			uint num = (uint)(size.x * size.y * size.z - 1);
			for (int i = size.y - 1; i >= 0; i--)
			{
				for (int j = size.z - 1; j >= 0; j--)
				{
					int num2 = i * item + j * item2 + outerStartIndex;
					int k = size.x - 1;
					while (k >= 0)
					{
						action.Execute((uint)(num2 + k), num, new int3(k, i, j));
						k--;
						num -= 1U;
					}
				}
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0004F1A0 File Offset: 0x0004D3A0
		public static void ForEachCellIn3DArray<T>(int3 size, ref T action) where T : struct, GridIterationUtilities.ICellAction
		{
			uint num = (uint)(size.x * size.y * size.z - 1);
			for (int i = size.y - 1; i >= 0; i--)
			{
				for (int j = size.z - 1; j >= 0; j--)
				{
					int k = size.x - 1;
					while (k >= 0)
					{
						action.Execute(num, k, i, j);
						k--;
						num -= 1U;
					}
				}
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0004F214 File Offset: 0x0004D414
		public static void ForEachNode<T>(int3 arrayBounds, NativeArray<float4> nodeNormals, ref T callback) where T : struct, GridIterationUtilities.INodeModifier
		{
			int num = 0;
			for (int i = 0; i < arrayBounds.y; i++)
			{
				for (int j = 0; j < arrayBounds.z; j++)
				{
					int k = 0;
					while (k < arrayBounds.x)
					{
						if (math.any(nodeNormals[num]))
						{
							callback.ModifyNode(num, k, i, j);
						}
						k++;
						num++;
					}
				}
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0004F27C File Offset: 0x0004D47C
		public unsafe static void FilterNodeConnections<T>(IntBounds bounds, NativeArray<ulong> nodeConnections, bool layeredDataLayout, ref T filter) where T : struct, GridIterationUtilities.IConnectionFilter
		{
			int3 size = bounds.size;
			int* ptr = stackalloc int[(UIntPtr)32];
			for (int i = 0; i < 8; i++)
			{
				ptr[i] = GridGraph.neighbourZOffsets[i] * size.x + GridGraph.neighbourXOffsets[i];
			}
			int num = size.x * size.z;
			int num2 = 0;
			for (int j = 0; j < size.y; j++)
			{
				for (int k = 0; k < size.z; k++)
				{
					int l = 0;
					while (l < size.x)
					{
						ulong num3 = nodeConnections[num2];
						if (layeredDataLayout)
						{
							for (int m = 0; m < 8; m++)
							{
								int num4 = (int)((num3 >> 4 * m) & 15UL);
								if (num4 != 15 && !filter.IsValidConnection(num2, l, j, k, m, num2 + ptr[m] + (num4 - j) * num))
								{
									num3 |= 15UL << 4 * m;
								}
							}
						}
						else
						{
							for (int n = 0; n < 8; n++)
							{
								if (((int)num3 & (1 << n)) != 0 && !filter.IsValidConnection(num2, l, j, k, n, num2 + ptr[n]))
								{
									num3 &= ~(1UL << n);
								}
							}
						}
						nodeConnections[num2] = num3;
						l++;
						num2++;
					}
				}
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		public static int? GetNeighbourDataIndex(IntBounds bounds, NativeArray<ulong> nodeConnections, bool layeredDataLayout, int dataX, int dataLayer, int dataZ, int direction)
		{
			int num = GridGraph.neighbourXOffsets[direction];
			int num2 = GridGraph.neighbourZOffsets[direction];
			int num3 = dataX + num;
			int num4 = dataZ + num2;
			int x = bounds.size.x;
			int num5 = bounds.size.x * bounds.size.z;
			int num6 = dataLayer * num5 + dataZ * x + dataX;
			int num7 = num4 * x + num3;
			if (layeredDataLayout)
			{
				ulong num8 = (nodeConnections[num6] >> 4 * direction) & 15UL;
				if (num8 == 15UL)
				{
					return null;
				}
				if (num3 < 0 || num4 < 0 || num3 >= bounds.size.x || num4 >= bounds.size.z)
				{
					throw new Exception("Node has an invalid connection to a node outside the bounds of the graph");
				}
				num7 += (int)num8 * num5;
			}
			else if ((nodeConnections[num6] & (1UL << direction)) == 0UL)
			{
				return null;
			}
			if (num3 < 0 || num4 < 0 || num3 >= bounds.size.x || num4 >= bounds.size.z)
			{
				throw new Exception("Node has an invalid connection to a node outside the bounds of the graph");
			}
			return new int?(num7);
		}

		// Token: 0x02000200 RID: 512
		public interface ISliceAction
		{
			// Token: 0x06000CA4 RID: 3236
			void Execute(uint outerIdx, uint innerIdx);
		}

		// Token: 0x02000201 RID: 513
		public interface ISliceActionWithCoords
		{
			// Token: 0x06000CA5 RID: 3237
			void Execute(uint outerIdx, uint innerIdx, int3 innerCoords);
		}

		// Token: 0x02000202 RID: 514
		public interface ICellAction
		{
			// Token: 0x06000CA6 RID: 3238
			void Execute(uint idx, int x, int y, int z);
		}

		// Token: 0x02000203 RID: 515
		public interface INodeModifier
		{
			// Token: 0x06000CA7 RID: 3239
			void ModifyNode(int dataIndex, int dataX, int dataLayer, int dataZ);
		}

		// Token: 0x02000204 RID: 516
		public interface IConnectionFilter
		{
			// Token: 0x06000CA8 RID: 3240
			bool IsValidConnection(int dataIndex, int dataX, int dataLayer, int dataZ, int direction, int neighbourDataIndex);
		}
	}
}
