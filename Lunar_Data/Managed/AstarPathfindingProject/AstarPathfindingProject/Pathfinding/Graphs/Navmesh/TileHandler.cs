using System;
using System.Collections.Generic;
using Pathfinding.ClipperLib;
using Pathfinding.Graphs.Navmesh.Voxelization;
using Pathfinding.Graphs.Util;
using Pathfinding.Poly2Tri;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001BF RID: 447
	public class TileHandler
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x000438EC File Offset: 0x00041AEC
		private bool isBatching
		{
			get
			{
				return this.batchDepth > 0;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x000438F7 File Offset: 0x00041AF7
		public bool isValid
		{
			get
			{
				return this.graph != null && this.graph.exists && this.tileXCount == this.graph.tileXCount && this.tileZCount == this.graph.tileZCount;
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00043938 File Offset: 0x00041B38
		public TileHandler(NavmeshBase graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			if (graph.GetTiles() == null)
			{
				Debug.LogWarning("Creating a TileHandler for a graph with no tiles. Please scan the graph before creating a TileHandler");
			}
			this.tileXCount = graph.tileXCount;
			this.tileZCount = graph.tileZCount;
			this.activeTileTypes = new TileHandler.TileType[this.tileXCount * this.tileZCount];
			this.activeTileRotations = new int[this.activeTileTypes.Length];
			this.activeTileOffsets = new int[this.activeTileTypes.Length];
			this.reloadedInBatch = new bool[this.activeTileTypes.Length];
			this.cuts = new GridLookup<NavmeshClipper>(new Int2(this.tileXCount, this.tileZCount));
			this.graph = graph;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00043A10 File Offset: 0x00041C10
		public void Resize(IntRect newTileBounds)
		{
			TileHandler.TileType[] array = new TileHandler.TileType[newTileBounds.Area];
			int[] array2 = new int[array.Length];
			int[] array3 = new int[array.Length];
			bool[] array4 = new bool[array.Length];
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					if (newTileBounds.Contains(j, i))
					{
						int num = j + i * this.tileXCount;
						int num2 = j - newTileBounds.xmin + (i - newTileBounds.ymin) * newTileBounds.Width;
						array[num2] = this.activeTileTypes[num];
						array2[num2] = this.activeTileRotations[num];
						array3[num2] = this.activeTileOffsets[num];
					}
				}
			}
			this.tileXCount = newTileBounds.Width;
			this.tileZCount = newTileBounds.Height;
			this.activeTileTypes = array;
			this.activeTileRotations = array2;
			this.activeTileOffsets = array3;
			this.reloadedInBatch = array4;
			for (int k = 0; k < this.tileZCount; k++)
			{
				for (int l = 0; l < this.tileXCount; l++)
				{
					int num3 = l + k * this.tileXCount;
					if (this.activeTileTypes[num3] == null)
					{
						this.UpdateTileType(this.graph.GetTile(l, k));
					}
				}
			}
			this.cuts.Resize(newTileBounds);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00043B6C File Offset: 0x00041D6C
		public void OnRecalculatedTiles(NavmeshTile[] recalculatedTiles)
		{
			for (int i = 0; i < recalculatedTiles.Length; i++)
			{
				this.UpdateTileType(recalculatedTiles[i]);
			}
			this.StartBatchLoad();
			for (int j = 0; j < recalculatedTiles.Length; j++)
			{
				this.ReloadTile(recalculatedTiles[j].x, recalculatedTiles[j].z);
			}
			this.EndBatchLoad();
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00043BC0 File Offset: 0x00041DC0
		public void GetSourceTileData(int x, int z, out Int3[] verts, out int[] tris)
		{
			int num = x + z * this.tileXCount;
			this.activeTileTypes[num].Load(out verts, out tris, this.activeTileRotations[num], this.activeTileOffsets[num]);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00043BF8 File Offset: 0x00041DF8
		public TileHandler.TileType RegisterTileType(Mesh source, Int3 centerOffset, int width = 1, int depth = 1)
		{
			return new TileHandler.TileType(source, (Int3)new Vector3(this.graph.TileWorldSizeX, 0f, this.graph.TileWorldSizeZ), centerOffset, width, depth);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00043C2C File Offset: 0x00041E2C
		public void CreateTileTypesFromGraph()
		{
			NavmeshTile[] tiles = this.graph.GetTiles();
			if (tiles == null)
			{
				return;
			}
			if (!this.isValid)
			{
				throw new InvalidOperationException("Graph tiles are invalid (number of tiles is not equal to width*depth of the graph). You need to create a new tile handler if you have changed the graph.");
			}
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					NavmeshTile navmeshTile = tiles[j + i * this.tileXCount];
					this.UpdateTileType(navmeshTile);
				}
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00043C94 File Offset: 0x00041E94
		private void UpdateTileType(NavmeshTile tile)
		{
			int x = tile.x;
			int z = tile.z;
			Int3 @int = (Int3)new Vector3(this.graph.TileWorldSizeX, 0f, this.graph.TileWorldSizeZ);
			Int3 int2 = -((Int3)this.graph.GetTileBoundsInGraphSpace(x, z, 1, 1).min + new Int3(@int.x * tile.w / 2, 0, @int.z * tile.d / 2));
			TileHandler.TileType tileType = new TileHandler.TileType(tile.vertsInGraphSpace, tile.tris, @int, int2, tile.w, tile.d);
			int num = x + z * this.tileXCount;
			this.activeTileTypes[num] = tileType;
			this.activeTileRotations[num] = 0;
			this.activeTileOffsets[num] = 0;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00043D6E File Offset: 0x00041F6E
		public void StartBatchLoad()
		{
			this.batchDepth++;
			if (this.batchDepth > 1)
			{
				return;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
			{
				this.graph.StartBatchTileUpdate();
				return true;
			}));
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00043DA4 File Offset: 0x00041FA4
		public void EndBatchLoad()
		{
			if (this.batchDepth <= 0)
			{
				throw new Exception("Ending batching when batching has not been started");
			}
			this.batchDepth--;
			for (int i = 0; i < this.reloadedInBatch.Length; i++)
			{
				this.reloadedInBatch[i] = false;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext ctx, bool force)
			{
				this.graph.EndBatchTileUpdate();
				return true;
			}));
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00043E0C File Offset: 0x0004200C
		private TileHandler.CuttingResult CutPoly(Int3[] verts, int[] tris, Int3[] extraShape, GraphTransform graphTransform, IntRect tiles, TileHandler.CutMode mode = TileHandler.CutMode.CutAll | TileHandler.CutMode.CutDual, int perturbate = -1)
		{
			List<NavmeshAdd> list = this.cuts.QueryRect<NavmeshAdd>(tiles);
			if ((verts.Length == 0 || tris.Length == 0) && list.Count == 0)
			{
				TileHandler.CuttingResult cuttingResult = new TileHandler.CuttingResult
				{
					verts = ArrayPool<Int3>.Claim(0),
					tris = ArrayPool<int>.Claim(0)
				};
				return cuttingResult;
			}
			if (perturbate > 10)
			{
				Debug.LogError("Too many perturbations aborting.\nThis may cause a tile in the navmesh to become empty. Try to see see if any of your NavmeshCut or NavmeshAdd components use invalid custom meshes.");
				TileHandler.CuttingResult cuttingResult = new TileHandler.CuttingResult
				{
					verts = verts,
					tris = tris
				};
				return cuttingResult;
			}
			List<IntPoint> list2 = null;
			if (extraShape == null && (mode & TileHandler.CutMode.CutExtra) != (TileHandler.CutMode)0)
			{
				throw new Exception("extraShape is null and the CutMode specifies that it should be used. Cannot use null shape.");
			}
			Bounds tileBoundsInGraphSpace = this.graph.GetTileBoundsInGraphSpace(tiles);
			Vector3 min = tileBoundsInGraphSpace.min;
			GraphTransform graphTransform2 = graphTransform * Matrix4x4.TRS(min, Quaternion.identity, Vector3.one);
			Vector2 vector = new Vector2(tileBoundsInGraphSpace.size.x, tileBoundsInGraphSpace.size.z);
			float navmeshCuttingCharacterRadius = this.graph.NavmeshCuttingCharacterRadius;
			if ((mode & TileHandler.CutMode.CutExtra) != (TileHandler.CutMode)0)
			{
				list2 = ListPool<IntPoint>.Claim(extraShape.Length);
				for (int i = 0; i < extraShape.Length; i++)
				{
					Int3 @int = graphTransform2.InverseTransform(extraShape[i]);
					list2.Add(new IntPoint((long)@int.x, (long)@int.z));
				}
			}
			List<NavmeshCut> list3;
			if (mode == TileHandler.CutMode.CutExtra)
			{
				list3 = ListPool<NavmeshCut>.Claim();
			}
			else
			{
				list3 = this.cuts.QueryRect<NavmeshCut>(tiles);
			}
			List<int> list4 = ListPool<int>.Claim();
			List<TileHandler.Cut> list5 = TileHandler.PrepareNavmeshCutsForCutting(list3, graphTransform2, perturbate, navmeshCuttingCharacterRadius);
			List<Int3> list6 = ListPool<Int3>.Claim(verts.Length * 2);
			List<int> list7 = ListPool<int>.Claim(tris.Length);
			if (list3.Count == 0 && list.Count == 0 && (mode & ~(TileHandler.CutMode.CutAll | TileHandler.CutMode.CutDual)) == (TileHandler.CutMode)0 && (mode & TileHandler.CutMode.CutAll) != (TileHandler.CutMode)0)
			{
				TileHandler.CopyMesh(verts, tris, list6, list7);
			}
			else
			{
				List<IntPoint> list8 = ListPool<IntPoint>.Claim();
				Dictionary<TriangulationPoint, int> dictionary = new Dictionary<TriangulationPoint, int>();
				List<PolygonPoint> list9 = ListPool<PolygonPoint>.Claim();
				PolyTree polyTree = new PolyTree();
				List<List<IntPoint>> list10 = ListPool<List<IntPoint>>.Claim();
				Stack<Polygon> stack = StackPool<Polygon>.Claim();
				this.clipper.StrictlySimple = perturbate > -1;
				this.clipper.ReverseSolution = true;
				Int3[] array = null;
				Int3[] array2 = null;
				Int2 int2 = default(Int2);
				if (list.Count > 0)
				{
					array = new Int3[7];
					array2 = new Int3[7];
					int2 = new Int2(((Int3)vector).x, ((Int3)vector).y);
				}
				Int3[] array3 = null;
				for (int j = -1; j < list.Count; j++)
				{
					Int3[] array4;
					int[] array5;
					if (j == -1)
					{
						array4 = verts;
						array5 = tris;
					}
					else
					{
						list[j].GetMesh(ref array3, out array5, graphTransform2);
						array4 = array3;
					}
					for (int k = 0; k < array5.Length; k += 3)
					{
						Int3 int3 = array4[array5[k]];
						Int3 int4 = array4[array5[k + 1]];
						Int3 int5 = array4[array5[k + 2]];
						if (VectorMath.IsColinearXZ(int3, int4, int5))
						{
							Debug.LogWarning("Skipping degenerate triangle.");
						}
						else
						{
							IntRect intRect = new IntRect(int3.x, int3.z, int3.x, int3.z);
							intRect = intRect.ExpandToContain(int4.x, int4.z);
							intRect = intRect.ExpandToContain(int5.x, int5.z);
							int num = Math.Min(int3.y, Math.Min(int4.y, int5.y));
							int num2 = Math.Max(int3.y, Math.Max(int4.y, int5.y));
							list4.Clear();
							bool flag = false;
							for (int l = 0; l < list5.Count; l++)
							{
								int x = list5[l].boundsY.x;
								int y = list5[l].boundsY.y;
								if (IntRect.Intersects(intRect, list5[l].bounds) && y >= num && x <= num2 && (list5[l].cutsAddedGeom || j == -1))
								{
									Int3 int6 = int3;
									int6.y = x;
									Int3 int7 = int3;
									int7.y = y;
									list4.Add(l);
									flag |= list5[l].isDual;
								}
							}
							if (list4.Count == 0 && (mode & TileHandler.CutMode.CutExtra) == (TileHandler.CutMode)0 && (mode & TileHandler.CutMode.CutAll) != (TileHandler.CutMode)0 && j == -1)
							{
								list7.Add(list6.Count);
								list7.Add(list6.Count + 1);
								list7.Add(list6.Count + 2);
								list6.Add(int3);
								list6.Add(int4);
								list6.Add(int5);
							}
							else
							{
								list8.Clear();
								if (j == -1)
								{
									list8.Add(new IntPoint((long)int3.x, (long)int3.z));
									list8.Add(new IntPoint((long)int4.x, (long)int4.z));
									list8.Add(new IntPoint((long)int5.x, (long)int5.z));
								}
								else
								{
									array[0] = int3;
									array[1] = int4;
									array[2] = int5;
									int num3 = this.ClipAgainstRectangle(array, array2, int2);
									if (num3 == 0)
									{
										goto IL_086F;
									}
									for (int m = 0; m < num3; m++)
									{
										list8.Add(new IntPoint((long)array[m].x, (long)array[m].z));
									}
								}
								dictionary.Clear();
								for (int n = 0; n < 4; n++)
								{
									if (((mode >> (n & 31)) & TileHandler.CutMode.CutAll) != (TileHandler.CutMode)0)
									{
										if (1 << n == 1)
										{
											this.CutAll(list8, list4, list5, polyTree);
										}
										else if (1 << n == 2)
										{
											if (!flag)
											{
												goto IL_0861;
											}
											this.CutDual(list8, list4, list5, flag, list10, polyTree);
										}
										else if (1 << n == 4)
										{
											this.CutExtra(list8, list2, polyTree);
										}
										for (int num4 = 0; num4 < polyTree.ChildCount; num4++)
										{
											PolyNode polyNode = polyTree.Childs[num4];
											List<IntPoint> contour = polyNode.Contour;
											List<PolyNode> childs = polyNode.Childs;
											if (childs.Count == 0 && contour.Count == 3 && j == -1)
											{
												for (int num5 = 0; num5 < 3; num5++)
												{
													Int3 int8 = new Int3((int)contour[num5].X, 0, (int)contour[num5].Y);
													int8.y = Polygon.SampleYCoordinateInTriangle(int3, int4, int5, int8);
													list7.Add(list6.Count);
													list6.Add(int8);
												}
											}
											else
											{
												Polygon polygon = null;
												int num6 = -1;
												for (List<IntPoint> list11 = contour; list11 != null; list11 = ((num6 < childs.Count) ? childs[num6].Contour : null))
												{
													list9.Clear();
													for (int num7 = 0; num7 < list11.Count; num7++)
													{
														PolygonPoint polygonPoint = new PolygonPoint((double)list11[num7].X, (double)list11[num7].Y);
														list9.Add(polygonPoint);
														Int3 int9 = new Int3((int)list11[num7].X, 0, (int)list11[num7].Y);
														int9.y = Polygon.SampleYCoordinateInTriangle(int3, int4, int5, int9);
														dictionary[polygonPoint] = list6.Count;
														list6.Add(int9);
													}
													Polygon polygon2;
													if (stack.Count > 0)
													{
														polygon2 = stack.Pop();
														polygon2.AddPoints(list9);
													}
													else
													{
														polygon2 = new Polygon(list9);
													}
													if (num6 == -1)
													{
														polygon = polygon2;
													}
													else
													{
														polygon.AddHole(polygon2);
													}
													num6++;
												}
												try
												{
													P2T.Triangulate(polygon);
												}
												catch (PointOnEdgeException)
												{
													Debug.LogWarning("PointOnEdgeException, perturbating vertices slightly.\nThis is usually fine. It happens sometimes because of rounding errors. Cutting will be retried a few more times.");
													return this.CutPoly(verts, tris, extraShape, graphTransform, tiles, mode, perturbate + 1);
												}
												try
												{
													for (int num8 = 0; num8 < polygon.Triangles.Count; num8++)
													{
														DelaunayTriangle delaunayTriangle = polygon.Triangles[num8];
														list7.Add(dictionary[delaunayTriangle.Points._0]);
														list7.Add(dictionary[delaunayTriangle.Points._1]);
														list7.Add(dictionary[delaunayTriangle.Points._2]);
													}
												}
												catch (KeyNotFoundException)
												{
													Debug.LogWarning("KeyNotFoundException, perturbating vertices slightly.\nThis is usually fine. It happens sometimes because of rounding errors. Cutting will be retried a few more times.");
													return this.CutPoly(verts, tris, extraShape, graphTransform, tiles, mode, perturbate + 1);
												}
												TileHandler.PoolPolygon(polygon, stack);
											}
										}
									}
									IL_0861:;
								}
							}
						}
						IL_086F:;
					}
				}
				if (array3 != null)
				{
					ArrayPool<Int3>.Release(ref array3, false);
				}
				StackPool<Polygon>.Release(stack);
				ListPool<List<IntPoint>>.Release(ref list10);
				ListPool<IntPoint>.Release(ref list8);
				ListPool<PolygonPoint>.Release(ref list9);
			}
			TileHandler.CuttingResult cuttingResult2 = default(TileHandler.CuttingResult);
			Polygon.CompressMesh(list6, list7, out cuttingResult2.verts, out cuttingResult2.tris);
			for (int num9 = 0; num9 < list3.Count; num9++)
			{
				list3[num9].UsedForCut();
			}
			ListPool<Int3>.Release(ref list6);
			ListPool<int>.Release(ref list7);
			ListPool<int>.Release(ref list4);
			for (int num10 = 0; num10 < list5.Count; num10++)
			{
				ListPool<IntPoint>.Release(list5[num10].contour);
			}
			ListPool<TileHandler.Cut>.Release(ref list5);
			ListPool<NavmeshCut>.Release(ref list3);
			return cuttingResult2;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00044784 File Offset: 0x00042984
		private unsafe static List<TileHandler.Cut> PrepareNavmeshCutsForCutting(List<NavmeshCut> navmeshCuts, GraphTransform transform, int perturbate, float characterRadius)
		{
			global::System.Random random = null;
			if (perturbate > 0)
			{
				random = new global::System.Random();
			}
			UnsafeList<float2> unsafeList = new UnsafeList<float2>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			UnsafeList<NavmeshCut.ContourBurst> unsafeList2 = new UnsafeList<NavmeshCut.ContourBurst>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			List<TileHandler.Cut> list = ListPool<TileHandler.Cut>.Claim();
			for (int i = 0; i < navmeshCuts.Count; i++)
			{
				Int2 @int = new Int2(0, 0);
				if (perturbate > 0)
				{
					@int.x = random.Next() % 6 * perturbate - 3 * perturbate;
					if (@int.x >= 0)
					{
						@int.x++;
					}
					@int.y = random.Next() % 6 * perturbate - 3 * perturbate;
					if (@int.y >= 0)
					{
						@int.y++;
					}
				}
				navmeshCuts[i].GetContourBurst(&unsafeList, &unsafeList2, transform.inverseMatrix, characterRadius);
				for (int j = 0; j < unsafeList2.Length; j++)
				{
					NavmeshCut.ContourBurst contourBurst = unsafeList2[j];
					if (contourBurst.endIndex <= contourBurst.startIndex)
					{
						Debug.LogError("A NavmeshCut component had a zero length contour. Ignoring that contour.");
					}
					else
					{
						List<IntPoint> list2 = ListPool<IntPoint>.Claim(contourBurst.endIndex - contourBurst.startIndex);
						for (int k = contourBurst.startIndex; k < contourBurst.endIndex; k++)
						{
							float2 @float = unsafeList[k] * 1000f;
							IntPoint intPoint = new IntPoint((long)@float.x, (long)@float.y);
							if (perturbate > 0)
							{
								intPoint.X += (long)@int.x;
								intPoint.Y += (long)@int.y;
							}
							list2.Add(intPoint);
						}
						IntRect intRect = new IntRect((int)list2[0].X, (int)list2[0].Y, (int)list2[0].X, (int)list2[0].Y);
						for (int l = 0; l < list2.Count; l++)
						{
							IntPoint intPoint2 = list2[l];
							intRect = intRect.ExpandToContain((int)intPoint2.X, (int)intPoint2.Y);
						}
						list.Add(new TileHandler.Cut
						{
							boundsY = new Int2((int)(contourBurst.ymin * 1000f), (int)(contourBurst.ymax * 1000f)),
							bounds = intRect,
							isDual = navmeshCuts[i].isDual,
							cutsAddedGeom = navmeshCuts[i].cutsAddedGeom,
							contour = list2
						});
					}
				}
				unsafeList2.Clear();
				unsafeList.Clear();
			}
			unsafeList2.Dispose();
			unsafeList.Dispose();
			return list;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00044A38 File Offset: 0x00042C38
		private static void PoolPolygon(Polygon polygon, Stack<Polygon> pool)
		{
			if (polygon.Holes != null)
			{
				for (int i = 0; i < polygon.Holes.Count; i++)
				{
					polygon.Holes[i].Points.Clear();
					polygon.Holes[i].ClearTriangles();
					if (polygon.Holes[i].Holes != null)
					{
						polygon.Holes[i].Holes.Clear();
					}
					pool.Push(polygon.Holes[i]);
				}
			}
			polygon.ClearTriangles();
			if (polygon.Holes != null)
			{
				polygon.Holes.Clear();
			}
			polygon.Points.Clear();
			pool.Push(polygon);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00044AF0 File Offset: 0x00042CF0
		private void CutAll(List<IntPoint> poly, List<int> intersectingCutIndices, List<TileHandler.Cut> cuts, PolyTree result)
		{
			this.clipper.Clear();
			this.clipper.AddPolygon(poly, PolyType.ptSubject);
			for (int i = 0; i < intersectingCutIndices.Count; i++)
			{
				this.clipper.AddPolygon(cuts[intersectingCutIndices[i]].contour, PolyType.ptClip);
			}
			result.Clear();
			this.clipper.Execute(ClipType.ctDifference, result, PolyFillType.pftNonZero, PolyFillType.pftNonZero);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00044B60 File Offset: 0x00042D60
		private void CutDual(List<IntPoint> poly, List<int> tmpIntersectingCuts, List<TileHandler.Cut> cuts, bool hasDual, List<List<IntPoint>> intermediateResult, PolyTree result)
		{
			this.clipper.Clear();
			this.clipper.AddPolygon(poly, PolyType.ptSubject);
			for (int i = 0; i < tmpIntersectingCuts.Count; i++)
			{
				if (cuts[tmpIntersectingCuts[i]].isDual)
				{
					this.clipper.AddPolygon(cuts[tmpIntersectingCuts[i]].contour, PolyType.ptClip);
				}
			}
			this.clipper.Execute(ClipType.ctIntersection, intermediateResult, PolyFillType.pftEvenOdd, PolyFillType.pftNonZero);
			this.clipper.Clear();
			if (intermediateResult != null)
			{
				for (int j = 0; j < intermediateResult.Count; j++)
				{
					this.clipper.AddPolygon(intermediateResult[j], Clipper.Orientation(intermediateResult[j]) ? PolyType.ptClip : PolyType.ptSubject);
				}
			}
			for (int k = 0; k < tmpIntersectingCuts.Count; k++)
			{
				if (!cuts[tmpIntersectingCuts[k]].isDual)
				{
					this.clipper.AddPolygon(cuts[tmpIntersectingCuts[k]].contour, PolyType.ptClip);
				}
			}
			result.Clear();
			this.clipper.Execute(ClipType.ctDifference, result, PolyFillType.pftEvenOdd, PolyFillType.pftNonZero);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00044C7F File Offset: 0x00042E7F
		private void CutExtra(List<IntPoint> poly, List<IntPoint> extraClipShape, PolyTree result)
		{
			this.clipper.Clear();
			this.clipper.AddPolygon(poly, PolyType.ptSubject);
			this.clipper.AddPolygon(extraClipShape, PolyType.ptClip);
			result.Clear();
			this.clipper.Execute(ClipType.ctIntersection, result, PolyFillType.pftEvenOdd, PolyFillType.pftNonZero);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00044CC0 File Offset: 0x00042EC0
		private int ClipAgainstRectangle(Int3[] clipIn, Int3[] clipOut, Int2 size)
		{
			int num = this.simpleClipper.ClipPolygon(clipIn, 3, clipOut, 1, 0, 0);
			if (num == 0)
			{
				return num;
			}
			num = this.simpleClipper.ClipPolygon(clipOut, num, clipIn, -1, size.x, 0);
			if (num == 0)
			{
				return num;
			}
			num = this.simpleClipper.ClipPolygon(clipIn, num, clipOut, 1, 0, 2);
			if (num == 0)
			{
				return num;
			}
			return this.simpleClipper.ClipPolygon(clipOut, num, clipIn, -1, size.y, 2);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00044D30 File Offset: 0x00042F30
		private static void CopyMesh(Int3[] vertices, int[] triangles, List<Int3> outVertices, List<int> outTriangles)
		{
			outTriangles.Capacity = Math.Max(outTriangles.Capacity, triangles.Length);
			outVertices.Capacity = Math.Max(outVertices.Capacity, vertices.Length);
			for (int i = 0; i < vertices.Length; i++)
			{
				outVertices.Add(vertices[i]);
			}
			for (int j = 0; j < triangles.Length; j++)
			{
				outTriangles.Add(triangles[j]);
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00044D98 File Offset: 0x00042F98
		private void DelaunayRefinement(Int3[] verts, int[] tris, ref int tCount, bool delaunay, bool colinear)
		{
			if (tCount % 3 != 0)
			{
				throw new ArgumentException("Triangle array length must be a multiple of 3");
			}
			Dictionary<Int2, int> dictionary = this.cached_Int2_int_dict;
			dictionary.Clear();
			for (int i = 0; i < tCount; i += 3)
			{
				if (!VectorMath.IsClockwiseXZ(verts[tris[i]], verts[tris[i + 1]], verts[tris[i + 2]]))
				{
					int num = tris[i];
					tris[i] = tris[i + 2];
					tris[i + 2] = num;
				}
				dictionary[new Int2(tris[i], tris[i + 1])] = i + 2;
				dictionary[new Int2(tris[i + 1], tris[i + 2])] = i;
				dictionary[new Int2(tris[i + 2], tris[i])] = i + 1;
			}
			for (int j = 0; j < tCount; j += 3)
			{
				for (int k = 0; k < 3; k++)
				{
					int num2;
					if (dictionary.TryGetValue(new Int2(tris[j + (k + 1) % 3], tris[j + k % 3]), out num2))
					{
						Int3 @int = verts[tris[j + (k + 2) % 3]];
						Int3 int2 = verts[tris[j + (k + 1) % 3]];
						Int3 int3 = verts[tris[j + (k + 3) % 3]];
						Int3 int4 = verts[tris[num2]];
						@int.y = 0;
						int2.y = 0;
						int3.y = 0;
						int4.y = 0;
						bool flag = false;
						if (!VectorMath.RightOrColinearXZ(@int, int3, int4) || VectorMath.RightXZ(@int, int2, int4))
						{
							if (!colinear)
							{
								goto IL_03C6;
							}
							flag = true;
						}
						if (colinear && VectorMath.SqrDistancePointSegmentApproximate(@int, int4, int2) < 9f && !dictionary.ContainsKey(new Int2(tris[j + (k + 2) % 3], tris[j + (k + 1) % 3])) && !dictionary.ContainsKey(new Int2(tris[j + (k + 1) % 3], tris[num2])))
						{
							tCount -= 3;
							int num3 = num2 / 3 * 3;
							tris[j + (k + 1) % 3] = tris[num2];
							if (num3 != tCount)
							{
								tris[num3] = tris[tCount];
								tris[num3 + 1] = tris[tCount + 1];
								tris[num3 + 2] = tris[tCount + 2];
								dictionary[new Int2(tris[num3], tris[num3 + 1])] = num3 + 2;
								dictionary[new Int2(tris[num3 + 1], tris[num3 + 2])] = num3;
								dictionary[new Int2(tris[num3 + 2], tris[num3])] = num3 + 1;
								tris[tCount] = 0;
								tris[tCount + 1] = 0;
								tris[tCount + 2] = 0;
							}
							else
							{
								tCount += 3;
							}
							dictionary[new Int2(tris[j], tris[j + 1])] = j + 2;
							dictionary[new Int2(tris[j + 1], tris[j + 2])] = j;
							dictionary[new Int2(tris[j + 2], tris[j])] = j + 1;
						}
						else if (delaunay && !flag)
						{
							float num4 = Int3.Angle(int2 - @int, int3 - @int);
							if (Int3.Angle(int2 - int4, int3 - int4) > 6.2831855f - 2f * num4)
							{
								tris[j + (k + 1) % 3] = tris[num2];
								int num5 = num2 / 3 * 3;
								int num6 = num2 - num5;
								tris[num5 + (num6 - 1 + 3) % 3] = tris[j + (k + 2) % 3];
								dictionary[new Int2(tris[j], tris[j + 1])] = j + 2;
								dictionary[new Int2(tris[j + 1], tris[j + 2])] = j;
								dictionary[new Int2(tris[j + 2], tris[j])] = j + 1;
								dictionary[new Int2(tris[num5], tris[num5 + 1])] = num5 + 2;
								dictionary[new Int2(tris[num5 + 1], tris[num5 + 2])] = num5;
								dictionary[new Int2(tris[num5 + 2], tris[num5])] = num5 + 1;
							}
						}
					}
					IL_03C6:;
				}
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00045188 File Offset: 0x00043388
		public void ClearTile(int x, int z)
		{
			if (AstarPath.active == null)
			{
				return;
			}
			if (x < 0 || z < 0 || x >= this.tileXCount || z >= this.tileZCount)
			{
				return;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext context, bool force)
			{
				this.graph.ReplaceTile(x, z, new Int3[0], new int[0]);
				this.activeTileTypes[x + z * this.tileXCount] = null;
				if (!this.isBatching)
				{
					context.SetGraphDirty(this.graph);
				}
				return true;
			}));
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00045208 File Offset: 0x00043408
		public void ReloadInBounds(Bounds bounds)
		{
			this.ReloadInBounds(this.graph.GetTouchingTiles(bounds, 0f));
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00045224 File Offset: 0x00043424
		public void ReloadInBounds(IntRect tiles)
		{
			tiles = IntRect.Intersection(tiles, new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
			if (!tiles.IsValid())
			{
				return;
			}
			for (int i = tiles.ymin; i <= tiles.ymax; i++)
			{
				for (int j = tiles.xmin; j <= tiles.xmax; j++)
				{
					this.ReloadTile(j, i);
				}
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00045290 File Offset: 0x00043490
		public void ReloadTile(int x, int z)
		{
			if (x < 0 || z < 0 || x >= this.tileXCount || z >= this.tileZCount)
			{
				return;
			}
			int num = x + z * this.tileXCount;
			if (this.activeTileTypes[num] != null)
			{
				this.LoadTile(this.activeTileTypes[num], x, z, this.activeTileRotations[num], this.activeTileOffsets[num]);
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000452F0 File Offset: 0x000434F0
		public void LoadTile(TileHandler.TileType tile, int x, int z, int rotation, int yoffset)
		{
			if (tile == null)
			{
				throw new ArgumentNullException("tile");
			}
			if (AstarPath.active == null)
			{
				return;
			}
			int index = x + z * this.tileXCount;
			rotation %= 4;
			if (this.isBatching && this.reloadedInBatch[index] && this.activeTileOffsets[index] == yoffset && this.activeTileRotations[index] == rotation && this.activeTileTypes[index] == tile)
			{
				return;
			}
			this.reloadedInBatch[index] |= this.isBatching;
			this.activeTileOffsets[index] = yoffset;
			this.activeTileRotations[index] = rotation;
			this.activeTileTypes[index] = tile;
			Int2 originalSize = new Int2(this.tileXCount, this.tileZCount);
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext context, bool force)
			{
				if (this.activeTileOffsets[index] != yoffset || this.activeTileRotations[index] != rotation || this.activeTileTypes[index] != tile)
				{
					return true;
				}
				if (originalSize != new Int2(this.tileXCount, this.tileZCount))
				{
					return true;
				}
				context.PreUpdate();
				Int3[] array;
				int[] array2;
				tile.Load(out array, out array2, rotation, yoffset);
				IntRect intRect = new IntRect(x, z, x + tile.Width - 1, z + tile.Depth - 1);
				TileHandler.CuttingResult cuttingResult = this.CutPoly(array, array2, null, this.graph.transform, intRect, TileHandler.CutMode.CutAll | TileHandler.CutMode.CutDual, -1);
				int num = cuttingResult.tris.Length;
				this.DelaunayRefinement(cuttingResult.verts, cuttingResult.tris, ref num, true, false);
				if (num != cuttingResult.tris.Length)
				{
					cuttingResult.tris = Memory.ShrinkArray<int>(cuttingResult.tris, num);
				}
				int num2 = ((rotation % 2 == 0) ? tile.Width : tile.Depth);
				int num3 = ((rotation % 2 == 0) ? tile.Depth : tile.Width);
				if (num2 != 1 || num3 != 1)
				{
					throw new Exception("Only tiles of width = depth = 1 are supported at this time");
				}
				this.graph.ReplaceTile(x, z, cuttingResult.verts, cuttingResult.tris);
				return true;
			}));
		}

		// Token: 0x0400082E RID: 2094
		public readonly NavmeshBase graph;

		// Token: 0x0400082F RID: 2095
		private int tileXCount;

		// Token: 0x04000830 RID: 2096
		private int tileZCount;

		// Token: 0x04000831 RID: 2097
		private readonly Clipper clipper = new Clipper(0);

		// Token: 0x04000832 RID: 2098
		private readonly Dictionary<Int2, int> cached_Int2_int_dict = new Dictionary<Int2, int>();

		// Token: 0x04000833 RID: 2099
		private TileHandler.TileType[] activeTileTypes;

		// Token: 0x04000834 RID: 2100
		private int[] activeTileRotations;

		// Token: 0x04000835 RID: 2101
		private int[] activeTileOffsets;

		// Token: 0x04000836 RID: 2102
		private bool[] reloadedInBatch;

		// Token: 0x04000837 RID: 2103
		public readonly GridLookup<NavmeshClipper> cuts;

		// Token: 0x04000838 RID: 2104
		private int batchDepth;

		// Token: 0x04000839 RID: 2105
		private Int3PolygonClipper simpleClipper;

		// Token: 0x020001C0 RID: 448
		public class TileType
		{
			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00045471 File Offset: 0x00043671
			public int Width
			{
				get
				{
					return this.width;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00045479 File Offset: 0x00043679
			public int Depth
			{
				get
				{
					return this.depth;
				}
			}

			// Token: 0x06000BD7 RID: 3031 RVA: 0x00045484 File Offset: 0x00043684
			public unsafe TileType(UnsafeSpan<Int3> sourceVerts, UnsafeSpan<int> sourceTris, Int3 tileSize, Int3 centerOffset, int width = 1, int depth = 1)
			{
				this.tris = sourceTris.ToArray();
				this.verts = new Int3[sourceVerts.Length];
				this.offset = tileSize / 2f;
				this.offset.x = this.offset.x * width;
				this.offset.z = this.offset.z * depth;
				this.offset.y = 0;
				this.offset += centerOffset;
				for (int i = 0; i < sourceVerts.Length; i++)
				{
					this.verts[i] = *sourceVerts[i] + this.offset;
				}
				this.lastRotation = 0;
				this.lastYOffset = 0;
				this.width = width;
				this.depth = depth;
			}

			// Token: 0x06000BD8 RID: 3032 RVA: 0x0004555C File Offset: 0x0004375C
			public TileType(Mesh source, Int3 tileSize, Int3 centerOffset, int width = 1, int depth = 1)
			{
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				Vector3[] vertices = source.vertices;
				this.tris = source.triangles;
				this.verts = new Int3[vertices.Length];
				for (int i = 0; i < vertices.Length; i++)
				{
					this.verts[i] = (Int3)vertices[i] + centerOffset;
				}
				this.offset = tileSize / 2f;
				this.offset.x = this.offset.x * width;
				this.offset.z = this.offset.z * depth;
				this.offset.y = 0;
				for (int j = 0; j < vertices.Length; j++)
				{
					this.verts[j] = this.verts[j] + this.offset;
				}
				this.lastRotation = 0;
				this.lastYOffset = 0;
				this.width = width;
				this.depth = depth;
			}

			// Token: 0x06000BD9 RID: 3033 RVA: 0x00045660 File Offset: 0x00043860
			public void Load(out Int3[] verts, out int[] tris, int rotation, int yoffset)
			{
				rotation = (rotation % 4 + 4) % 4;
				int num = rotation;
				rotation = (rotation - this.lastRotation % 4 + 4) % 4;
				this.lastRotation = num;
				verts = this.verts;
				int num2 = yoffset - this.lastYOffset;
				this.lastYOffset = yoffset;
				if (rotation != 0 || num2 != 0)
				{
					for (int i = 0; i < verts.Length; i++)
					{
						Int3 @int = verts[i] - this.offset;
						Int3 int2 = @int;
						int2.y += num2;
						int2.x = @int.x * TileHandler.TileType.Rotations[rotation * 4] + @int.z * TileHandler.TileType.Rotations[rotation * 4 + 1];
						int2.z = @int.x * TileHandler.TileType.Rotations[rotation * 4 + 2] + @int.z * TileHandler.TileType.Rotations[rotation * 4 + 3];
						verts[i] = int2 + this.offset;
					}
				}
				tris = this.tris;
			}

			// Token: 0x0400083A RID: 2106
			private Int3[] verts;

			// Token: 0x0400083B RID: 2107
			private int[] tris;

			// Token: 0x0400083C RID: 2108
			private Int3 offset;

			// Token: 0x0400083D RID: 2109
			private int lastYOffset;

			// Token: 0x0400083E RID: 2110
			private int lastRotation;

			// Token: 0x0400083F RID: 2111
			private int width;

			// Token: 0x04000840 RID: 2112
			private int depth;

			// Token: 0x04000841 RID: 2113
			private static readonly int[] Rotations = new int[]
			{
				1, 0, 0, 1, 0, 1, -1, 0, -1, 0,
				0, -1, 0, -1, 1, 0
			};
		}

		// Token: 0x020001C1 RID: 449
		[Flags]
		public enum CutMode
		{
			// Token: 0x04000843 RID: 2115
			CutAll = 1,
			// Token: 0x04000844 RID: 2116
			CutDual = 2,
			// Token: 0x04000845 RID: 2117
			CutExtra = 4
		}

		// Token: 0x020001C2 RID: 450
		private class Cut
		{
			// Token: 0x04000846 RID: 2118
			public IntRect bounds;

			// Token: 0x04000847 RID: 2119
			public Int2 boundsY;

			// Token: 0x04000848 RID: 2120
			public bool isDual;

			// Token: 0x04000849 RID: 2121
			public bool cutsAddedGeom;

			// Token: 0x0400084A RID: 2122
			public List<IntPoint> contour;
		}

		// Token: 0x020001C3 RID: 451
		private struct CuttingResult
		{
			// Token: 0x0400084B RID: 2123
			public Int3[] verts;

			// Token: 0x0400084C RID: 2124
			public int[] tris;
		}
	}
}
