using System;
using Unity.Collections;
using Unity.Mathematics;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001E7 RID: 487
	internal static class VoxelUtilityBurst
	{
		// Token: 0x06000C51 RID: 3153 RVA: 0x0004BCA8 File Offset: 0x00049EA8
		public static void CalculateDistanceField(CompactVoxelField field, NativeArray<ushort> output)
		{
			int num = field.width * field.depth;
			for (int i = 0; i < num; i += field.width)
			{
				for (int j = 0; j < field.width; j++)
				{
					CompactVoxelCell compactVoxelCell = field.cells[j + i];
					int k = compactVoxelCell.index;
					int num2 = compactVoxelCell.index + compactVoxelCell.count;
					while (k < num2)
					{
						CompactVoxelSpan compactVoxelSpan = field.spans[k];
						int num3 = 0;
						int num4 = 0;
						while (num4 < 4 && (long)compactVoxelSpan.GetConnection(num4) != 63L)
						{
							num3++;
							num4++;
						}
						output[k] = ((num3 == 4) ? ushort.MaxValue : 0);
						k++;
					}
				}
			}
			for (int l = 0; l < num; l += field.width)
			{
				for (int m = 0; m < field.width; m++)
				{
					int num5 = m + l;
					CompactVoxelCell compactVoxelCell2 = field.cells[num5];
					int n = compactVoxelCell2.index;
					int num6 = compactVoxelCell2.index + compactVoxelCell2.count;
					while (n < num6)
					{
						CompactVoxelSpan compactVoxelSpan2 = field.spans[n];
						int num7 = (int)output[n];
						if ((long)compactVoxelSpan2.GetConnection(0) != 63L)
						{
							int neighbourIndex = field.GetNeighbourIndex(num5, 0);
							int num8 = field.cells[neighbourIndex].index + compactVoxelSpan2.GetConnection(0);
							num7 = math.min(num7, (int)(output[num8] + 2));
							CompactVoxelSpan compactVoxelSpan3 = field.spans[num8];
							if ((long)compactVoxelSpan3.GetConnection(3) != 63L)
							{
								int neighbourIndex2 = field.GetNeighbourIndex(neighbourIndex, 3);
								int num9 = field.cells[neighbourIndex2].index + compactVoxelSpan3.GetConnection(3);
								num7 = math.min(num7, (int)(output[num9] + 3));
							}
						}
						if ((long)compactVoxelSpan2.GetConnection(3) != 63L)
						{
							int neighbourIndex3 = field.GetNeighbourIndex(num5, 3);
							int num10 = field.cells[neighbourIndex3].index + compactVoxelSpan2.GetConnection(3);
							num7 = math.min(num7, (int)(output[num10] + 2));
							CompactVoxelSpan compactVoxelSpan4 = field.spans[num10];
							if ((long)compactVoxelSpan4.GetConnection(2) != 63L)
							{
								int neighbourIndex4 = field.GetNeighbourIndex(neighbourIndex3, 2);
								int num11 = field.cells[neighbourIndex4].index + compactVoxelSpan4.GetConnection(2);
								num7 = math.min(num7, (int)(output[num11] + 3));
							}
						}
						output[n] = (ushort)num7;
						n++;
					}
				}
			}
			for (int num12 = num - field.width; num12 >= 0; num12 -= field.width)
			{
				for (int num13 = field.width - 1; num13 >= 0; num13--)
				{
					int num14 = num13 + num12;
					CompactVoxelCell compactVoxelCell3 = field.cells[num14];
					int num15 = compactVoxelCell3.index;
					int num16 = compactVoxelCell3.index + compactVoxelCell3.count;
					while (num15 < num16)
					{
						CompactVoxelSpan compactVoxelSpan5 = field.spans[num15];
						int num17 = (int)output[num15];
						if ((long)compactVoxelSpan5.GetConnection(2) != 63L)
						{
							int neighbourIndex5 = field.GetNeighbourIndex(num14, 2);
							int num18 = field.cells[neighbourIndex5].index + compactVoxelSpan5.GetConnection(2);
							num17 = math.min(num17, (int)(output[num18] + 2));
							CompactVoxelSpan compactVoxelSpan6 = field.spans[num18];
							if ((long)compactVoxelSpan6.GetConnection(1) != 63L)
							{
								int neighbourIndex6 = field.GetNeighbourIndex(neighbourIndex5, 1);
								int num19 = field.cells[neighbourIndex6].index + compactVoxelSpan6.GetConnection(1);
								num17 = math.min(num17, (int)(output[num19] + 3));
							}
						}
						if ((long)compactVoxelSpan5.GetConnection(1) != 63L)
						{
							int neighbourIndex7 = field.GetNeighbourIndex(num14, 1);
							int num20 = field.cells[neighbourIndex7].index + compactVoxelSpan5.GetConnection(1);
							num17 = math.min(num17, (int)(output[num20] + 2));
							CompactVoxelSpan compactVoxelSpan7 = field.spans[num20];
							if ((long)compactVoxelSpan7.GetConnection(0) != 63L)
							{
								int neighbourIndex8 = field.GetNeighbourIndex(neighbourIndex7, 0);
								int num21 = field.cells[neighbourIndex8].index + compactVoxelSpan7.GetConnection(0);
								num17 = math.min(num17, (int)(output[num21] + 3));
							}
						}
						output[num15] = (ushort)num17;
						num15++;
					}
				}
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0004C170 File Offset: 0x0004A370
		public static void BoxBlur(CompactVoxelField field, NativeArray<ushort> src, NativeArray<ushort> dst)
		{
			ushort num = 20;
			for (int i = field.width * field.depth - field.width; i >= 0; i -= field.width)
			{
				for (int j = field.width - 1; j >= 0; j--)
				{
					int num2 = j + i;
					CompactVoxelCell compactVoxelCell = field.cells[num2];
					int k = compactVoxelCell.index;
					int num3 = compactVoxelCell.index + compactVoxelCell.count;
					while (k < num3)
					{
						CompactVoxelSpan compactVoxelSpan = field.spans[k];
						ushort num4 = src[k];
						if (num4 < num)
						{
							dst[k] = num4;
						}
						else
						{
							int num5 = (int)num4;
							for (int l = 0; l < 4; l++)
							{
								if ((long)compactVoxelSpan.GetConnection(l) != 63L)
								{
									int neighbourIndex = field.GetNeighbourIndex(num2, l);
									int num6 = field.cells[neighbourIndex].index + compactVoxelSpan.GetConnection(l);
									num5 += (int)src[num6];
									CompactVoxelSpan compactVoxelSpan2 = field.spans[num6];
									int num7 = (l + 1) & 3;
									if ((long)compactVoxelSpan2.GetConnection(num7) != 63L)
									{
										int neighbourIndex2 = field.GetNeighbourIndex(neighbourIndex, num7);
										int num8 = field.cells[neighbourIndex2].index + compactVoxelSpan2.GetConnection(num7);
										num5 += (int)src[num8];
									}
									else
									{
										num5 += (int)num4;
									}
								}
								else
								{
									num5 += (int)(num4 * 2);
								}
							}
							dst[k] = (ushort)((float)(num5 + 5) / 9f);
						}
						k++;
					}
				}
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0004C323 File Offset: 0x0004A523
		// Note: this type is marked as 'beforefieldinit'.
		static VoxelUtilityBurst()
		{
			int[] array = new int[4];
			array[0] = -1;
			array[2] = 1;
			VoxelUtilityBurst.DX = array;
			VoxelUtilityBurst.DZ = new int[]
			{
				default(int),
				1,
				default(int),
				-1
			};
		}

		// Token: 0x040008E1 RID: 2273
		public const int TagRegMask = 16383;

		// Token: 0x040008E2 RID: 2274
		public const int TagReg = 16384;

		// Token: 0x040008E3 RID: 2275
		public const ushort BorderReg = 32768;

		// Token: 0x040008E4 RID: 2276
		public const int RC_BORDER_VERTEX = 65536;

		// Token: 0x040008E5 RID: 2277
		public const int RC_AREA_BORDER = 131072;

		// Token: 0x040008E6 RID: 2278
		public const int VERTEX_BUCKET_COUNT = 4096;

		// Token: 0x040008E7 RID: 2279
		public const int RC_CONTOUR_TESS_WALL_EDGES = 1;

		// Token: 0x040008E8 RID: 2280
		public const int RC_CONTOUR_TESS_AREA_EDGES = 2;

		// Token: 0x040008E9 RID: 2281
		public const int RC_CONTOUR_TESS_TILE_EDGES = 4;

		// Token: 0x040008EA RID: 2282
		public const int ContourRegMask = 65535;

		// Token: 0x040008EB RID: 2283
		public static readonly int[] DX;

		// Token: 0x040008EC RID: 2284
		public static readonly int[] DZ;
	}
}
