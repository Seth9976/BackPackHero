using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000009 RID: 9
	internal struct Tessellator
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00003A88 File Offset: 0x00001C88
		private static float FindSplit(UHull hull, UEvent edge)
		{
			float num;
			if (hull.a.x < edge.a.x)
			{
				num = ModuleHandle.OrientFast(hull.a, hull.b, edge.a);
			}
			else
			{
				num = ModuleHandle.OrientFast(edge.b, edge.a, hull.a);
			}
			if (0f != num)
			{
				return num;
			}
			if (edge.b.x < hull.b.x)
			{
				num = ModuleHandle.OrientFast(hull.a, hull.b, edge.b);
			}
			else
			{
				num = ModuleHandle.OrientFast(edge.b, edge.a, hull.b);
			}
			if (0f != num)
			{
				return num;
			}
			return (float)(hull.idx - edge.idx);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003B51 File Offset: 0x00001D51
		private void SetAllocator(Allocator allocator)
		{
			this.m_Allocator = allocator;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003B5C File Offset: 0x00001D5C
		private bool AddPoint(NativeArray<UHull> hulls, int hullCount, NativeArray<float2> points, float2 p, int idx)
		{
			int lower = ModuleHandle.GetLower<UHull, float2, Tessellator.TestHullPointL>(hulls, hullCount, p, default(Tessellator.TestHullPointL));
			int upper = ModuleHandle.GetUpper<UHull, float2, Tessellator.TestHullPointU>(hulls, hullCount, p, default(Tessellator.TestHullPointU));
			if (lower < 0 || upper < 0)
			{
				return false;
			}
			for (int i = lower; i < upper; i++)
			{
				UHull uhull = hulls[i];
				int num = uhull.ilcount;
				while (num > 1 && ModuleHandle.OrientFast(points[uhull.ilarray[num - 2]], points[uhull.ilarray[num - 1]], p) > 0f)
				{
					int3 @int = default(int3);
					@int.x = uhull.ilarray[num - 1];
					@int.y = uhull.ilarray[num - 2];
					@int.z = idx;
					int num2 = this.m_CellCount;
					this.m_CellCount = num2 + 1;
					this.m_Cells[num2] = @int;
					num--;
				}
				uhull.ilcount = num + 1;
				if (uhull.ilcount > uhull.ilarray.Length)
				{
					return false;
				}
				uhull.ilarray[num] = idx;
				num = uhull.iucount;
				while (num > 1 && ModuleHandle.OrientFast(points[uhull.iuarray[num - 2]], points[uhull.iuarray[num - 1]], p) < 0f)
				{
					int3 int2 = default(int3);
					int2.x = uhull.iuarray[num - 2];
					int2.y = uhull.iuarray[num - 1];
					int2.z = idx;
					int num2 = this.m_CellCount;
					this.m_CellCount = num2 + 1;
					this.m_Cells[num2] = int2;
					num--;
				}
				uhull.iucount = num + 1;
				if (uhull.iucount > uhull.iuarray.Length)
				{
					return false;
				}
				uhull.iuarray[num] = idx;
				hulls[i] = uhull;
			}
			return true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003D94 File Offset: 0x00001F94
		private static void InsertHull(NativeArray<UHull> Hulls, int Pos, ref int Count, UHull Value)
		{
			if (Count < Hulls.Length - 1)
			{
				for (int i = Count; i > Pos; i--)
				{
					Hulls[i] = Hulls[i - 1];
				}
				Hulls[Pos] = Value;
				Count++;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003DDC File Offset: 0x00001FDC
		private static void EraseHull(NativeArray<UHull> Hulls, int Pos, ref int Count)
		{
			if (Count < Hulls.Length)
			{
				for (int i = Pos; i < Count - 1; i++)
				{
					Hulls[i] = Hulls[i + 1];
				}
				Count--;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003E1C File Offset: 0x0000201C
		private bool SplitHulls(NativeArray<UHull> hulls, ref int hullCount, NativeArray<float2> points, UEvent evt)
		{
			int lower = ModuleHandle.GetLower<UHull, UEvent, Tessellator.TestHullEventLe>(hulls, hullCount, evt, default(Tessellator.TestHullEventLe));
			if (lower < 0)
			{
				return false;
			}
			UHull uhull = hulls[lower];
			UHull uhull2;
			uhull2.a = evt.a;
			uhull2.b = evt.b;
			uhull2.idx = evt.idx;
			int num = uhull.iuarray[uhull.iucount - 1];
			uhull2.iuarray = new ArraySlice<int>(this.m_IUArray, uhull2.idx * this.m_NumHulls, this.m_NumHulls);
			uhull2.iucount = uhull.iucount;
			for (int i = 0; i < uhull2.iucount; i++)
			{
				uhull2.iuarray[i] = uhull.iuarray[i];
			}
			uhull.iuarray[0] = num;
			uhull.iucount = 1;
			hulls[lower] = uhull;
			uhull2.ilarray = new ArraySlice<int>(this.m_ILArray, uhull2.idx * this.m_NumHulls, this.m_NumHulls);
			uhull2.ilarray[0] = num;
			uhull2.ilcount = 1;
			Tessellator.InsertHull(hulls, lower + 1, ref hullCount, uhull2);
			return true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003F58 File Offset: 0x00002158
		private bool MergeHulls(NativeArray<UHull> hulls, ref int hullCount, NativeArray<float2> points, UEvent evt)
		{
			float2 a = evt.a;
			evt.a = evt.b;
			evt.b = a;
			int equal = ModuleHandle.GetEqual<UHull, UEvent, Tessellator.TestHullEventE>(hulls, hullCount, evt, default(Tessellator.TestHullEventE));
			if (equal < 0)
			{
				return false;
			}
			UHull uhull = hulls[equal];
			UHull uhull2 = hulls[equal - 1];
			uhull2.iucount = uhull.iucount;
			for (int i = 0; i < uhull2.iucount; i++)
			{
				uhull2.iuarray[i] = uhull.iuarray[i];
			}
			hulls[equal - 1] = uhull2;
			Tessellator.EraseHull(hulls, equal, ref hullCount);
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004004 File Offset: 0x00002204
		private static void InsertUniqueEdge(NativeArray<int2> edges, int2 e, ref int edgeCount)
		{
			TessEdgeCompare tessEdgeCompare = default(TessEdgeCompare);
			bool flag = true;
			int num = 0;
			while (flag && num < edgeCount)
			{
				if (tessEdgeCompare.Compare(e, edges[num]) == 0)
				{
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				int num2 = edgeCount;
				edgeCount = num2 + 1;
				edges[num2] = e;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004054 File Offset: 0x00002254
		private void PrepareDelaunay(NativeArray<int2> edges, int edgeCount)
		{
			this.m_StarCount = this.m_CellCount * 3;
			this.m_Stars = new NativeArray<UStar>(this.m_StarCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_SPArray = new NativeArray<int>(this.m_StarCount * this.m_StarCount, this.m_Allocator, NativeArrayOptions.UninitializedMemory);
			int num = 0;
			NativeArray<int2> nativeArray = new NativeArray<int2>(this.m_StarCount, this.m_Allocator, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < edgeCount; i++)
			{
				int2 @int = edges[i];
				@int.x = ((edges[i].x < edges[i].y) ? edges[i].x : edges[i].y);
				@int.y = ((edges[i].x > edges[i].y) ? edges[i].x : edges[i].y);
				edges[i] = @int;
				Tessellator.InsertUniqueEdge(nativeArray, @int, ref num);
			}
			this.m_Edges = new NativeArray<int2>(num, this.m_Allocator, NativeArrayOptions.ClearMemory);
			for (int j = 0; j < num; j++)
			{
				this.m_Edges[j] = nativeArray[j];
			}
			nativeArray.Dispose();
			ModuleHandle.InsertionSort<int2, TessEdgeCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int2>(this.m_Edges), 0, this.m_Edges.Length - 1, default(TessEdgeCompare));
			for (int k = 0; k < this.m_StarCount; k++)
			{
				UStar ustar = this.m_Stars[k];
				ustar.points = new ArraySlice<int>(this.m_SPArray, k * this.m_StarCount, this.m_StarCount);
				ustar.pointCount = 0;
				this.m_Stars[k] = ustar;
			}
			for (int l = 0; l < this.m_CellCount; l++)
			{
				int x = this.m_Cells[l].x;
				int y = this.m_Cells[l].y;
				int z = this.m_Cells[l].z;
				UStar ustar2 = this.m_Stars[x];
				UStar ustar3 = this.m_Stars[y];
				UStar ustar4 = this.m_Stars[z];
				int num2 = ustar2.pointCount;
				ustar2.pointCount = num2 + 1;
				ustar2.points[num2] = y;
				num2 = ustar2.pointCount;
				ustar2.pointCount = num2 + 1;
				ustar2.points[num2] = z;
				num2 = ustar3.pointCount;
				ustar3.pointCount = num2 + 1;
				ustar3.points[num2] = z;
				num2 = ustar3.pointCount;
				ustar3.pointCount = num2 + 1;
				ustar3.points[num2] = x;
				num2 = ustar4.pointCount;
				ustar4.pointCount = num2 + 1;
				ustar4.points[num2] = x;
				num2 = ustar4.pointCount;
				ustar4.pointCount = num2 + 1;
				ustar4.points[num2] = y;
				this.m_Stars[x] = ustar2;
				this.m_Stars[y] = ustar3;
				this.m_Stars[z] = ustar4;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000439C File Offset: 0x0000259C
		private int OppositeOf(int a, int b)
		{
			ArraySlice<int> points = this.m_Stars[b].points;
			int i = 1;
			int pointCount = this.m_Stars[b].pointCount;
			while (i < pointCount)
			{
				if (points[i] == a)
				{
					return points[i - 1];
				}
				i += 2;
			}
			return -1;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000043F0 File Offset: 0x000025F0
		private int FindConstraint(int a, int b)
		{
			int2 @int;
			@int.x = ((a < b) ? a : b);
			@int.y = ((a > b) ? a : b);
			return ModuleHandle.GetEqual<int2, int2, Tessellator.TestEdgePointE>(this.m_Edges, this.m_Edges.Length, @int, default(Tessellator.TestEdgePointE));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000443C File Offset: 0x0000263C
		private void AddTriangle(int i, int j, int k)
		{
			UStar ustar = this.m_Stars[i];
			UStar ustar2 = this.m_Stars[j];
			UStar ustar3 = this.m_Stars[k];
			int num = ustar.pointCount;
			ustar.pointCount = num + 1;
			ustar.points[num] = j;
			num = ustar.pointCount;
			ustar.pointCount = num + 1;
			ustar.points[num] = k;
			num = ustar2.pointCount;
			ustar2.pointCount = num + 1;
			ustar2.points[num] = k;
			num = ustar2.pointCount;
			ustar2.pointCount = num + 1;
			ustar2.points[num] = i;
			num = ustar3.pointCount;
			ustar3.pointCount = num + 1;
			ustar3.points[num] = i;
			num = ustar3.pointCount;
			ustar3.pointCount = num + 1;
			ustar3.points[num] = j;
			this.m_Stars[i] = ustar;
			this.m_Stars[j] = ustar2;
			this.m_Stars[k] = ustar3;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004540 File Offset: 0x00002740
		private void RemovePair(int r, int j, int k)
		{
			UStar ustar = this.m_Stars[r];
			ArraySlice<int> points = ustar.points;
			int i = 1;
			int pointCount = ustar.pointCount;
			while (i < pointCount)
			{
				if (points[i - 1] == j && points[i] == k)
				{
					points[i - 1] = points[pointCount - 2];
					points[i] = points[pointCount - 1];
					ustar.points = points;
					ustar.pointCount -= 2;
					this.m_Stars[r] = ustar;
					return;
				}
				i += 2;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000045D7 File Offset: 0x000027D7
		private void RemoveTriangle(int i, int j, int k)
		{
			this.RemovePair(i, j, k);
			this.RemovePair(j, k, i);
			this.RemovePair(k, i, j);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000045F4 File Offset: 0x000027F4
		private void EdgeFlip(int i, int j)
		{
			int num = this.OppositeOf(i, j);
			int num2 = this.OppositeOf(j, i);
			this.RemoveTriangle(i, j, num);
			this.RemoveTriangle(j, i, num2);
			this.AddTriangle(i, num2, num);
			this.AddTriangle(j, num, num2);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004638 File Offset: 0x00002838
		private bool Flip(NativeArray<float2> points, ref Array<int> stack, ref int stackCount, int a, int b, int x)
		{
			int num = this.OppositeOf(a, b);
			if (num < 0)
			{
				return true;
			}
			if (b < a)
			{
				int num2 = a;
				a = b;
				b = num2;
				int num3 = x;
				x = num;
				num = num3;
			}
			if (this.FindConstraint(a, b) != -1)
			{
				return true;
			}
			if (ModuleHandle.IsInsideCircle(points[a], points[b], points[x], points[num]))
			{
				if (2 + stackCount >= stack.Length)
				{
					return false;
				}
				int num4 = stackCount;
				stackCount = num4 + 1;
				stack[num4] = a;
				num4 = stackCount;
				stackCount = num4 + 1;
				stack[num4] = b;
			}
			return true;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000046D4 File Offset: 0x000028D4
		private Array<int3> GetCells(ref int count)
		{
			Array<int3> array = new Array<int3>(this.m_NumPoints * 4, this.m_NumPoints * (this.m_NumPoints + 1), this.m_Allocator, NativeArrayOptions.UninitializedMemory);
			count = 0;
			int i = 0;
			int length = this.m_Stars.Length;
			while (i < length)
			{
				ArraySlice<int> points = this.m_Stars[i].points;
				int j = 0;
				int pointCount = this.m_Stars[i].pointCount;
				while (j < pointCount)
				{
					int num = points[j];
					int num2 = points[j + 1];
					if (i < math.min(num, num2))
					{
						int3 @int = default(int3);
						@int.x = i;
						@int.y = num;
						@int.z = num2;
						int num3 = count;
						count = num3 + 1;
						array[num3] = @int;
					}
					j += 2;
				}
				i++;
			}
			return array;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000047BC File Offset: 0x000029BC
		internal bool ApplyDelaunay(NativeArray<float2> points, NativeArray<int2> edges)
		{
			if (this.m_CellCount == 0)
			{
				return false;
			}
			Array<int> array = new Array<int>(this.m_NumPoints * 4, this.m_NumPoints * (this.m_NumPoints + 1), this.m_Allocator, NativeArrayOptions.UninitializedMemory);
			int num = 0;
			bool flag = true;
			this.PrepareDelaunay(edges, this.m_NumEdges);
			int num2 = 0;
			while (flag && num2 < this.m_NumPoints)
			{
				UStar ustar = this.m_Stars[num2];
				for (int i = 1; i < ustar.pointCount; i += 2)
				{
					int num3 = ustar.points[i];
					if (num3 >= num2 && this.FindConstraint(num2, num3) < 0)
					{
						int num4 = ustar.points[i - 1];
						int num5 = -1;
						for (int j = 1; j < ustar.pointCount; j += 2)
						{
							if (ustar.points[j - 1] == num3)
							{
								num5 = ustar.points[j];
								break;
							}
						}
						if (num5 >= 0 && ModuleHandle.IsInsideCircle(points[num2], points[num3], points[num4], points[num5]))
						{
							if (2 + num >= array.Length)
							{
								flag = false;
								break;
							}
							array[num++] = num2;
							array[num++] = num3;
						}
					}
				}
				num2++;
			}
			int num6 = this.m_NumPoints * this.m_NumPoints;
			while (num > 0 && flag)
			{
				int num7 = array[num - 1];
				num--;
				int num8 = array[num - 1];
				num--;
				int num9 = -1;
				int num10 = -1;
				UStar ustar2 = this.m_Stars[num8];
				for (int k = 1; k < ustar2.pointCount; k += 2)
				{
					int num11 = ustar2.points[k - 1];
					int num12 = ustar2.points[k];
					if (num11 == num7)
					{
						num10 = num12;
					}
					else if (num12 == num7)
					{
						num9 = num11;
					}
				}
				if (num9 >= 0 && num10 >= 0 && ModuleHandle.IsInsideCircle(points[num8], points[num7], points[num9], points[num10]))
				{
					this.EdgeFlip(num8, num7);
					flag = this.Flip(points, ref array, ref num, num9, num8, num10);
					flag = flag && this.Flip(points, ref array, ref num, num8, num10, num9);
					flag = flag && this.Flip(points, ref array, ref num, num10, num7, num9);
					flag = flag && this.Flip(points, ref array, ref num, num7, num9, num10);
					flag = flag && --num6 > 0;
				}
			}
			array.Dispose();
			return flag;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004A80 File Offset: 0x00002C80
		private int FindNeighbor(Array<int3> cells, int count, int a, int b, int c)
		{
			int num = a;
			int num2 = b;
			int num3 = c;
			if (b < c)
			{
				if (b < a)
				{
					num = b;
					num2 = c;
					num3 = a;
				}
			}
			else if (c < a)
			{
				num = c;
				num2 = a;
				num3 = b;
			}
			if (num < 0)
			{
				return -1;
			}
			int3 @int;
			@int.x = num;
			@int.y = num2;
			@int.z = num3;
			return ModuleHandle.GetEqual<int3, int3, Tessellator.TestCellE>(cells, count, @int, default(Tessellator.TestCellE));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004AE8 File Offset: 0x00002CE8
		private Array<int3> Constrain(ref int count)
		{
			Array<int3> cells = this.GetCells(ref count);
			int num = count;
			for (int i = 0; i < num; i++)
			{
				int3 @int = cells[i];
				int x = @int.x;
				int y = @int.y;
				int z = @int.z;
				if (y < z)
				{
					if (y < x)
					{
						@int.x = y;
						@int.y = z;
						@int.z = x;
					}
				}
				else if (z < x)
				{
					@int.x = z;
					@int.y = x;
					@int.z = y;
				}
				cells[i] = @int;
			}
			ModuleHandle.InsertionSort<int3, TessCellCompare>(cells.UnsafePtr, 0, this.m_CellCount - 1, default(TessCellCompare));
			this.m_Flags = new NativeArray<int>(num, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_Neighbors = new NativeArray<int>(num * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_Constraints = new NativeArray<int>(num * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			NativeArray<int> nativeArray = new NativeArray<int>(num * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			NativeArray<int> nativeArray2 = new NativeArray<int>(num * 3, this.m_Allocator, NativeArrayOptions.ClearMemory);
			int num2 = 1;
			int num3 = 0;
			int j = 0;
			for (int k = 0; k < num; k++)
			{
				int3 int2 = cells[k];
				for (int l = 0; l < 3; l++)
				{
					int num4 = l;
					int num5 = (l + 1) % 3;
					num4 = ((num4 == 0) ? int2.x : ((l == 1) ? int2.y : int2.z));
					num5 = ((num5 == 0) ? int2.x : ((num5 == 1) ? int2.y : int2.z));
					int num6 = this.OppositeOf(num5, num4);
					int num7 = (this.m_Neighbors[3 * k + l] = this.FindNeighbor(cells, count, num5, num4, num6));
					int num8 = (this.m_Constraints[3 * k + l] = ((-1 != this.FindConstraint(num4, num5)) ? 1 : 0));
					if (num7 < 0)
					{
						if (num8 != 0)
						{
							nativeArray[num3++] = k;
						}
						else
						{
							nativeArray2[j++] = k;
							this.m_Flags[k] = 1;
						}
					}
				}
			}
			while (j > 0 || num3 > 0)
			{
				while (j > 0)
				{
					int num9 = nativeArray2[j - 1];
					j--;
					if (this.m_Flags[num9] != -num2)
					{
						this.m_Flags[num9] = num2;
						int3 int3 = cells[num9];
						for (int m = 0; m < 3; m++)
						{
							int num10 = this.m_Neighbors[3 * num9 + m];
							if (num10 >= 0 && this.m_Flags[num10] == 0)
							{
								if (this.m_Constraints[3 * num9 + m] != 0)
								{
									nativeArray[num3++] = num10;
								}
								else
								{
									nativeArray2[j++] = num10;
									this.m_Flags[num10] = num2;
								}
							}
						}
					}
				}
				for (int n = 0; n < num3; n++)
				{
					nativeArray2[n] = nativeArray[n];
				}
				j = num3;
				num3 = 0;
				num2 = -num2;
			}
			nativeArray2.Dispose();
			nativeArray.Dispose();
			return cells;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004E4C File Offset: 0x0000304C
		internal NativeArray<int3> RemoveExterior(ref int cellCount)
		{
			int num = 0;
			Array<int3> array = this.Constrain(ref num);
			NativeArray<int3> nativeArray = new NativeArray<int3>(num, this.m_Allocator, NativeArrayOptions.ClearMemory);
			cellCount = 0;
			for (int i = 0; i < num; i++)
			{
				if (this.m_Flags[i] == -1)
				{
					int num2 = cellCount;
					cellCount = num2 + 1;
					nativeArray[num2] = array[i];
				}
			}
			array.Dispose();
			return nativeArray;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004EB4 File Offset: 0x000030B4
		internal NativeArray<int3> RemoveInterior(int cellCount)
		{
			int num = 0;
			Array<int3> array = this.Constrain(ref num);
			NativeArray<int3> nativeArray = new NativeArray<int3>(num, this.m_Allocator, NativeArrayOptions.ClearMemory);
			cellCount = 0;
			for (int i = 0; i < num; i++)
			{
				if (this.m_Flags[i] == 1)
				{
					nativeArray[cellCount++] = array[i];
				}
			}
			array.Dispose();
			return nativeArray;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004F18 File Offset: 0x00003118
		internal bool Triangulate(NativeArray<float2> points, int pointCount, NativeArray<int2> edges, int edgeCount)
		{
			this.m_NumEdges = edgeCount;
			this.m_NumHulls = edgeCount * 2;
			this.m_NumPoints = pointCount;
			this.m_CellCount = 0;
			int num = this.m_NumHulls * (this.m_NumHulls + 1);
			this.m_Cells = new Array<int3>(num, ModuleHandle.kMaxTriangleCount, this.m_Allocator, NativeArrayOptions.UninitializedMemory);
			this.m_ILArray = new NativeArray<int>(num, this.m_Allocator, NativeArrayOptions.ClearMemory);
			this.m_IUArray = new NativeArray<int>(num, this.m_Allocator, NativeArrayOptions.ClearMemory);
			NativeArray<UHull> nativeArray = new NativeArray<UHull>(this.m_NumPoints * 8, this.m_Allocator, NativeArrayOptions.ClearMemory);
			int num2 = 0;
			NativeArray<UEvent> nativeArray2 = new NativeArray<UEvent>(this.m_NumPoints + this.m_NumEdges * 2, this.m_Allocator, NativeArrayOptions.ClearMemory);
			int num3 = 0;
			for (int i = 0; i < this.m_NumPoints; i++)
			{
				UEvent uevent = default(UEvent);
				uevent.a = points[i];
				uevent.b = default(float2);
				uevent.idx = i;
				uevent.type = 0;
				nativeArray2[num3++] = uevent;
			}
			for (int j = 0; j < this.m_NumEdges; j++)
			{
				int2 @int = edges[j];
				float2 @float = points[@int.x];
				float2 float2 = points[@int.y];
				if (@float.x < float2.x)
				{
					UEvent uevent2 = default(UEvent);
					uevent2.a = @float;
					uevent2.b = float2;
					uevent2.idx = j;
					uevent2.type = 2;
					UEvent uevent3 = default(UEvent);
					uevent3.a = float2;
					uevent3.b = @float;
					uevent3.idx = j;
					uevent3.type = 1;
					nativeArray2[num3++] = uevent2;
					nativeArray2[num3++] = uevent3;
				}
				else if (@float.x > float2.x)
				{
					UEvent uevent4 = default(UEvent);
					uevent4.a = float2;
					uevent4.b = @float;
					uevent4.idx = j;
					uevent4.type = 2;
					UEvent uevent5 = default(UEvent);
					uevent5.a = @float;
					uevent5.b = float2;
					uevent5.idx = j;
					uevent5.type = 1;
					nativeArray2[num3++] = uevent4;
					nativeArray2[num3++] = uevent5;
				}
			}
			ModuleHandle.InsertionSort<UEvent, TessEventCompare>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<UEvent>(nativeArray2), 0, num3 - 1, default(TessEventCompare));
			bool flag = true;
			float num4 = nativeArray2[0].a.x - (1f + math.abs(nativeArray2[0].a.x)) * math.pow(2f, -16f);
			UHull uhull;
			uhull.a.x = num4;
			uhull.a.y = 1f;
			uhull.b.x = num4;
			uhull.b.y = 0f;
			uhull.idx = -1;
			uhull.ilarray = new ArraySlice<int>(this.m_ILArray, this.m_NumHulls * this.m_NumHulls, this.m_NumHulls);
			uhull.iuarray = new ArraySlice<int>(this.m_IUArray, this.m_NumHulls * this.m_NumHulls, this.m_NumHulls);
			uhull.ilcount = 0;
			uhull.iucount = 0;
			nativeArray[num2++] = uhull;
			int k = 0;
			int num5 = num3;
			while (k < num5)
			{
				int type = nativeArray2[k].type;
				if (type != 0)
				{
					if (type != 2)
					{
						flag = this.MergeHulls(nativeArray, ref num2, points, nativeArray2[k]);
					}
					else
					{
						flag = this.SplitHulls(nativeArray, ref num2, points, nativeArray2[k]);
					}
				}
				else
				{
					flag = this.AddPoint(nativeArray, num2, points, nativeArray2[k].a, nativeArray2[k].idx);
				}
				if (!flag)
				{
					break;
				}
				k++;
			}
			nativeArray2.Dispose();
			nativeArray.Dispose();
			return flag;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005324 File Offset: 0x00003524
		internal static bool Tessellate(Allocator allocator, NativeArray<float2> pgPoints, int pgPointCount, NativeArray<int2> pgEdges, int pgEdgeCount, ref NativeArray<float2> outputVertices, ref int vertexCount, ref NativeArray<int> outputIndices, ref int indexCount)
		{
			Tessellator tessellator = default(Tessellator);
			tessellator.SetAllocator(allocator);
			int num = 0;
			int num2 = 0;
			bool flag = tessellator.Triangulate(pgPoints, pgPointCount, pgEdges, pgEdgeCount);
			flag = flag && tessellator.ApplyDelaunay(pgPoints, pgEdges);
			if (flag)
			{
				NativeArray<int3> nativeArray = tessellator.RemoveExterior(ref num2);
				for (int i = 0; i < num2; i++)
				{
					ushort num3 = (ushort)nativeArray[i].x;
					ushort num4 = (ushort)nativeArray[i].y;
					ushort num5 = (ushort)nativeArray[i].z;
					if (num3 != num4 && num4 != num5 && num3 != num5)
					{
						int num6 = indexCount;
						indexCount = num6 + 1;
						outputIndices[num6] = (int)num3;
						num6 = indexCount;
						indexCount = num6 + 1;
						outputIndices[num6] = (int)num5;
						num6 = indexCount;
						indexCount = num6 + 1;
						outputIndices[num6] = (int)num4;
					}
					num = math.max(math.max(math.max(nativeArray[i].x, nativeArray[i].y), nativeArray[i].z), num);
				}
				num = ((num != 0) ? (num + 1) : 0);
				for (int j = 0; j < num; j++)
				{
					int num6 = vertexCount;
					vertexCount = num6 + 1;
					outputVertices[num6] = pgPoints[j];
				}
				nativeArray.Dispose();
			}
			tessellator.Cleanup();
			return flag;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005498 File Offset: 0x00003698
		internal void Cleanup()
		{
			if (this.m_Edges.IsCreated)
			{
				this.m_Edges.Dispose();
			}
			if (this.m_Stars.IsCreated)
			{
				this.m_Stars.Dispose();
			}
			if (this.m_SPArray.IsCreated)
			{
				this.m_SPArray.Dispose();
			}
			if (this.m_Cells.IsCreated)
			{
				this.m_Cells.Dispose();
			}
			if (this.m_ILArray.IsCreated)
			{
				this.m_ILArray.Dispose();
			}
			if (this.m_IUArray.IsCreated)
			{
				this.m_IUArray.Dispose();
			}
			if (this.m_Flags.IsCreated)
			{
				this.m_Flags.Dispose();
			}
			if (this.m_Neighbors.IsCreated)
			{
				this.m_Neighbors.Dispose();
			}
			if (this.m_Constraints.IsCreated)
			{
				this.m_Constraints.Dispose();
			}
		}

		// Token: 0x04000011 RID: 17
		private NativeArray<int2> m_Edges;

		// Token: 0x04000012 RID: 18
		private NativeArray<UStar> m_Stars;

		// Token: 0x04000013 RID: 19
		private Array<int3> m_Cells;

		// Token: 0x04000014 RID: 20
		private int m_CellCount;

		// Token: 0x04000015 RID: 21
		private NativeArray<int> m_ILArray;

		// Token: 0x04000016 RID: 22
		private NativeArray<int> m_IUArray;

		// Token: 0x04000017 RID: 23
		private NativeArray<int> m_SPArray;

		// Token: 0x04000018 RID: 24
		private int m_NumEdges;

		// Token: 0x04000019 RID: 25
		private int m_NumHulls;

		// Token: 0x0400001A RID: 26
		private int m_NumPoints;

		// Token: 0x0400001B RID: 27
		private int m_StarCount;

		// Token: 0x0400001C RID: 28
		private NativeArray<int> m_Flags;

		// Token: 0x0400001D RID: 29
		private NativeArray<int> m_Neighbors;

		// Token: 0x0400001E RID: 30
		private NativeArray<int> m_Constraints;

		// Token: 0x0400001F RID: 31
		private Allocator m_Allocator;

		// Token: 0x0200001C RID: 28
		private struct TestHullPointL : ICondition2<UHull, float2>
		{
			// Token: 0x06000078 RID: 120 RVA: 0x0000717C File Offset: 0x0000537C
			public bool Test(UHull h, float2 p, ref float t)
			{
				t = ModuleHandle.OrientFast(h.a, h.b, p);
				return t < 0f;
			}
		}

		// Token: 0x0200001D RID: 29
		private struct TestHullPointU : ICondition2<UHull, float2>
		{
			// Token: 0x06000079 RID: 121 RVA: 0x0000719B File Offset: 0x0000539B
			public bool Test(UHull h, float2 p, ref float t)
			{
				t = ModuleHandle.OrientFast(h.a, h.b, p);
				return t > 0f;
			}
		}

		// Token: 0x0200001E RID: 30
		private struct TestHullEventLe : ICondition2<UHull, UEvent>
		{
			// Token: 0x0600007A RID: 122 RVA: 0x000071BA File Offset: 0x000053BA
			public bool Test(UHull h, UEvent p, ref float t)
			{
				t = Tessellator.FindSplit(h, p);
				return t <= 0f;
			}
		}

		// Token: 0x0200001F RID: 31
		private struct TestHullEventE : ICondition2<UHull, UEvent>
		{
			// Token: 0x0600007B RID: 123 RVA: 0x000071D1 File Offset: 0x000053D1
			public bool Test(UHull h, UEvent p, ref float t)
			{
				t = Tessellator.FindSplit(h, p);
				return t == 0f;
			}
		}

		// Token: 0x02000020 RID: 32
		private struct TestEdgePointE : ICondition2<int2, int2>
		{
			// Token: 0x0600007C RID: 124 RVA: 0x000071E8 File Offset: 0x000053E8
			public bool Test(int2 h, int2 p, ref float t)
			{
				t = (float)default(TessEdgeCompare).Compare(h, p);
				return t == 0f;
			}
		}

		// Token: 0x02000021 RID: 33
		private struct TestCellE : ICondition2<int3, int3>
		{
			// Token: 0x0600007D RID: 125 RVA: 0x00007214 File Offset: 0x00005414
			public bool Test(int3 h, int3 p, ref float t)
			{
				t = (float)default(TessCellCompare).Compare(h, p);
				return t == 0f;
			}
		}
	}
}
