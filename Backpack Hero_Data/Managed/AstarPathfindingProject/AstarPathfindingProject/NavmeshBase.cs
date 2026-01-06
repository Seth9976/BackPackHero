using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000060 RID: 96
	public abstract class NavmeshBase : NavGraph, INavmesh, INavmeshHolder, ITransformedGraph, IRaycastableGraph
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004E6 RID: 1254
		public abstract float TileWorldSizeX { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004E7 RID: 1255
		public abstract float TileWorldSizeZ { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004E8 RID: 1256
		protected abstract float MaxTileConnectionEdgeDistance { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001ADB6 File Offset: 0x00018FB6
		GraphTransform ITransformedGraph.transform
		{
			get
			{
				return this.transform;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004EA RID: 1258
		protected abstract bool RecalculateNormals { get; }

		// Token: 0x060004EB RID: 1259
		public abstract GraphTransform CalculateTransform();

		// Token: 0x060004EC RID: 1260 RVA: 0x0001ADBE File Offset: 0x00018FBE
		public NavmeshTile GetTile(int x, int z)
		{
			return this.tiles[x + z * this.tileXCount];
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		public Int3 GetVertex(int index)
		{
			int num = (index >> 12) & 524287;
			return this.tiles[num].GetVertex(index);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001ADFC File Offset: 0x00018FFC
		public Int3 GetVertexInGraphSpace(int index)
		{
			int num = (index >> 12) & 524287;
			return this.tiles[num].GetVertexInGraphSpace(index);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001AE22 File Offset: 0x00019022
		public static int GetTileIndex(int index)
		{
			return (index >> 12) & 524287;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001AE2E File Offset: 0x0001902E
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001AE37 File Offset: 0x00019037
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			z = tileIndex / this.tileXCount;
			x = tileIndex - z * this.tileXCount;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001AE50 File Offset: 0x00019050
		public NavmeshTile[] GetTiles()
		{
			return this.tiles;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001AE58 File Offset: 0x00019058
		public Bounds GetTileBounds(IntRect rect)
		{
			return this.GetTileBounds(rect.xmin, rect.ymin, rect.Width, rect.Height);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001AE7A File Offset: 0x0001907A
		public Bounds GetTileBounds(int x, int z, int width = 1, int depth = 1)
		{
			return this.transform.Transform(this.GetTileBoundsInGraphSpace(x, z, width, depth));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001AE92 File Offset: 0x00019092
		public Bounds GetTileBoundsInGraphSpace(IntRect rect)
		{
			return this.GetTileBoundsInGraphSpace(rect.xmin, rect.ymin, rect.Width, rect.Height);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001AEB4 File Offset: 0x000190B4
		public Bounds GetTileBoundsInGraphSpace(int x, int z, int width = 1, int depth = 1)
		{
			Bounds bounds = default(Bounds);
			bounds.SetMinMax(new Vector3((float)x * this.TileWorldSizeX, 0f, (float)z * this.TileWorldSizeZ), new Vector3((float)(x + width) * this.TileWorldSizeX, this.forcedBoundsSize.y, (float)(z + depth) * this.TileWorldSizeZ));
			return bounds;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001AF14 File Offset: 0x00019114
		public Int2 GetTileCoordinates(Vector3 position)
		{
			position = this.transform.InverseTransform(position);
			position.x /= this.TileWorldSizeX;
			position.z /= this.TileWorldSizeZ;
			return new Int2((int)position.x, (int)position.z);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001AF64 File Offset: 0x00019164
		protected override void OnDestroy()
		{
			base.OnDestroy();
			TriangleMeshNode.SetNavmeshHolder(this.active.data.GetGraphIndex(this), null);
			if (this.tiles != null)
			{
				for (int i = 0; i < this.tiles.Length; i++)
				{
					ObjectPool<BBTree>.Release(ref this.tiles[i].bbTree);
				}
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001AFBB File Offset: 0x000191BB
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			this.RelocateNodes(deltaMatrix * this.transform);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001AFD0 File Offset: 0x000191D0
		public void RelocateNodes(GraphTransform newTransform)
		{
			this.transform = newTransform;
			if (this.tiles != null)
			{
				for (int i = 0; i < this.tiles.Length; i++)
				{
					NavmeshTile navmeshTile = this.tiles[i];
					if (navmeshTile != null)
					{
						navmeshTile.vertsInGraphSpace.CopyTo(navmeshTile.verts, 0);
						this.transform.Transform(navmeshTile.verts);
						for (int j = 0; j < navmeshTile.nodes.Length; j++)
						{
							navmeshTile.nodes[j].UpdatePositionFromVertices();
						}
						navmeshTile.bbTree.RebuildFrom(navmeshTile.nodes);
					}
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001B060 File Offset: 0x00019260
		protected NavmeshTile NewEmptyTile(int x, int z)
		{
			return new NavmeshTile
			{
				x = x,
				z = z,
				w = 1,
				d = 1,
				verts = new Int3[0],
				vertsInGraphSpace = new Int3[0],
				tris = new int[0],
				nodes = new TriangleMeshNode[0],
				bbTree = ObjectPool<BBTree>.Claim(),
				graph = this
			};
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001B0D0 File Offset: 0x000192D0
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.tiles == null)
			{
				return;
			}
			for (int i = 0; i < this.tiles.Length; i++)
			{
				if (this.tiles[i] != null && this.tiles[i].x + this.tiles[i].z * this.tileXCount == i)
				{
					TriangleMeshNode[] nodes = this.tiles[i].nodes;
					if (nodes != null)
					{
						for (int j = 0; j < nodes.Length; j++)
						{
							action(nodes[j]);
						}
					}
				}
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001B150 File Offset: 0x00019350
		public IntRect GetTouchingTiles(Bounds bounds, float margin = 0f)
		{
			bounds = this.transform.InverseTransform(bounds);
			return IntRect.Intersection(new IntRect(Mathf.FloorToInt((bounds.min.x - margin) / this.TileWorldSizeX), Mathf.FloorToInt((bounds.min.z - margin) / this.TileWorldSizeZ), Mathf.FloorToInt((bounds.max.x + margin) / this.TileWorldSizeX), Mathf.FloorToInt((bounds.max.z + margin) / this.TileWorldSizeZ)), new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001B1F4 File Offset: 0x000193F4
		public IntRect GetTouchingTilesInGraphSpace(Rect rect)
		{
			return IntRect.Intersection(new IntRect(Mathf.FloorToInt(rect.xMin / this.TileWorldSizeX), Mathf.FloorToInt(rect.yMin / this.TileWorldSizeZ), Mathf.FloorToInt(rect.xMax / this.TileWorldSizeX), Mathf.FloorToInt(rect.yMax / this.TileWorldSizeZ)), new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001B270 File Offset: 0x00019470
		public IntRect GetTouchingTilesRound(Bounds bounds)
		{
			bounds = this.transform.InverseTransform(bounds);
			return IntRect.Intersection(new IntRect(Mathf.RoundToInt(bounds.min.x / this.TileWorldSizeX), Mathf.RoundToInt(bounds.min.z / this.TileWorldSizeZ), Mathf.RoundToInt(bounds.max.x / this.TileWorldSizeX) - 1, Mathf.RoundToInt(bounds.max.z / this.TileWorldSizeZ) - 1), new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001B310 File Offset: 0x00019510
		protected void ConnectTileWithNeighbours(NavmeshTile tile, bool onlyUnflagged = false)
		{
			if (tile.w != 1 || tile.d != 1)
			{
				throw new ArgumentException("Tile widths or depths other than 1 are not supported. The fields exist mainly for possible future expansions.");
			}
			for (int i = -1; i <= 1; i++)
			{
				int num = tile.z + i;
				if (num >= 0 && num < this.tileZCount)
				{
					for (int j = -1; j <= 1; j++)
					{
						int num2 = tile.x + j;
						if (num2 >= 0 && num2 < this.tileXCount && j == 0 != (i == 0))
						{
							NavmeshTile navmeshTile = this.tiles[num2 + num * this.tileXCount];
							if (!onlyUnflagged || !navmeshTile.flag)
							{
								this.ConnectTiles(navmeshTile, tile);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001B3B0 File Offset: 0x000195B0
		protected void RemoveConnectionsFromTile(NavmeshTile tile)
		{
			if (tile.x > 0)
			{
				int num = tile.x - 1;
				for (int i = tile.z; i < tile.z + tile.d; i++)
				{
					this.RemoveConnectionsFromTo(this.tiles[num + i * this.tileXCount], tile);
				}
			}
			if (tile.x + tile.w < this.tileXCount)
			{
				int num2 = tile.x + tile.w;
				for (int j = tile.z; j < tile.z + tile.d; j++)
				{
					this.RemoveConnectionsFromTo(this.tiles[num2 + j * this.tileXCount], tile);
				}
			}
			if (tile.z > 0)
			{
				int num3 = tile.z - 1;
				for (int k = tile.x; k < tile.x + tile.w; k++)
				{
					this.RemoveConnectionsFromTo(this.tiles[k + num3 * this.tileXCount], tile);
				}
			}
			if (tile.z + tile.d < this.tileZCount)
			{
				int num4 = tile.z + tile.d;
				for (int l = tile.x; l < tile.x + tile.w; l++)
				{
					this.RemoveConnectionsFromTo(this.tiles[l + num4 * this.tileXCount], tile);
				}
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001B50C File Offset: 0x0001970C
		protected void RemoveConnectionsFromTo(NavmeshTile a, NavmeshTile b)
		{
			if (a == null || b == null)
			{
				return;
			}
			if (a == b)
			{
				return;
			}
			int num = b.x + b.z * this.tileXCount;
			for (int i = 0; i < a.nodes.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = a.nodes[i];
				if (triangleMeshNode.connections != null)
				{
					for (int j = 0; j < triangleMeshNode.connections.Length; j++)
					{
						TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[j].node as TriangleMeshNode;
						if (triangleMeshNode2 != null && ((triangleMeshNode2.GetVertexIndex(0) >> 12) & 524287) == num)
						{
							triangleMeshNode.RemoveConnection(triangleMeshNode.connections[j].node);
							j--;
						}
					}
				}
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001B5BC File Offset: 0x000197BC
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			return this.GetNearestForce(position, (constraint != null && constraint.distanceXZ) ? NavmeshBase.NNConstraintDistanceXZ : null);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001B5D8 File Offset: 0x000197D8
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.tiles == null)
			{
				return default(NNInfoInternal);
			}
			Int2 tileCoordinates = this.GetTileCoordinates(position);
			tileCoordinates.x = Mathf.Clamp(tileCoordinates.x, 0, this.tileXCount - 1);
			tileCoordinates.y = Mathf.Clamp(tileCoordinates.y, 0, this.tileZCount - 1);
			int num = Math.Max(this.tileXCount, this.tileZCount);
			NNInfoInternal nninfoInternal = default(NNInfoInternal);
			float positiveInfinity = float.PositiveInfinity;
			bool flag = this.nearestSearchOnlyXZ || (constraint != null && constraint.distanceXZ);
			int num2 = 0;
			while (num2 < num && positiveInfinity >= (float)(num2 - 2) * Math.Max(this.TileWorldSizeX, this.TileWorldSizeX))
			{
				int num3 = Math.Min(num2 + tileCoordinates.y + 1, this.tileZCount);
				for (int i = Math.Max(-num2 + tileCoordinates.y, 0); i < num3; i++)
				{
					int num4 = Math.Abs(num2 - Math.Abs(i - tileCoordinates.y));
					int num5 = num4;
					do
					{
						int num6 = -num5 + tileCoordinates.x;
						if (num6 >= 0 && num6 < this.tileXCount)
						{
							NavmeshTile navmeshTile = this.tiles[num6 + i * this.tileXCount];
							if (navmeshTile != null)
							{
								if (flag)
								{
									nninfoInternal = navmeshTile.bbTree.QueryClosestXZ(position, constraint, ref positiveInfinity, nninfoInternal);
								}
								else
								{
									nninfoInternal = navmeshTile.bbTree.QueryClosest(position, constraint, ref positiveInfinity, nninfoInternal);
								}
							}
						}
						num5 = -num5;
					}
					while (num5 != num4);
				}
				num2++;
			}
			nninfoInternal.node = nninfoInternal.constrainedNode;
			nninfoInternal.constrainedNode = null;
			nninfoInternal.clampedPosition = nninfoInternal.constClampedPosition;
			return nninfoInternal;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001B788 File Offset: 0x00019988
		public GraphNode PointOnNavmesh(Vector3 position, NNConstraint constraint)
		{
			if (this.tiles == null)
			{
				return null;
			}
			Int2 tileCoordinates = this.GetTileCoordinates(position);
			if (tileCoordinates.x < 0 || tileCoordinates.y < 0 || tileCoordinates.x >= this.tileXCount || tileCoordinates.y >= this.tileZCount)
			{
				return null;
			}
			NavmeshTile tile = this.GetTile(tileCoordinates.x, tileCoordinates.y);
			if (tile != null)
			{
				return tile.bbTree.QueryInside(position, constraint);
			}
			return null;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001B7FC File Offset: 0x000199FC
		protected void FillWithEmptyTiles()
		{
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					this.tiles[i * this.tileXCount + j] = this.NewEmptyTile(j, i);
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001B844 File Offset: 0x00019A44
		protected static void CreateNodeConnections(TriangleMeshNode[] nodes)
		{
			List<Connection> list = ListPool<Connection>.Claim();
			Dictionary<Int2, int> dictionary = ObjectPoolSimple<Dictionary<Int2, int>>.Claim();
			dictionary.Clear();
			for (int i = 0; i < nodes.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = nodes[i];
				int vertexCount = triangleMeshNode.GetVertexCount();
				for (int j = 0; j < vertexCount; j++)
				{
					Int2 @int = new Int2(triangleMeshNode.GetVertexIndex(j), triangleMeshNode.GetVertexIndex((j + 1) % vertexCount));
					if (!dictionary.ContainsKey(@int))
					{
						dictionary.Add(@int, i);
					}
				}
			}
			foreach (TriangleMeshNode triangleMeshNode2 in nodes)
			{
				list.Clear();
				int vertexCount2 = triangleMeshNode2.GetVertexCount();
				for (int l = 0; l < vertexCount2; l++)
				{
					int vertexIndex = triangleMeshNode2.GetVertexIndex(l);
					int vertexIndex2 = triangleMeshNode2.GetVertexIndex((l + 1) % vertexCount2);
					int num;
					if (dictionary.TryGetValue(new Int2(vertexIndex2, vertexIndex), out num))
					{
						TriangleMeshNode triangleMeshNode3 = nodes[num];
						int vertexCount3 = triangleMeshNode3.GetVertexCount();
						for (int m = 0; m < vertexCount3; m++)
						{
							if (triangleMeshNode3.GetVertexIndex(m) == vertexIndex2 && triangleMeshNode3.GetVertexIndex((m + 1) % vertexCount3) == vertexIndex)
							{
								list.Add(new Connection(triangleMeshNode3, (uint)(triangleMeshNode2.position - triangleMeshNode3.position).costMagnitude, (byte)l));
								break;
							}
						}
					}
				}
				triangleMeshNode2.connections = list.ToArrayFromPool<Connection>();
				triangleMeshNode2.SetConnectivityDirty();
			}
			dictionary.Clear();
			ObjectPoolSimple<Dictionary<Int2, int>>.Release(ref dictionary);
			ListPool<Connection>.Release(ref list);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		protected void ConnectTiles(NavmeshTile tile1, NavmeshTile tile2)
		{
			if (tile1 == null || tile2 == null)
			{
				return;
			}
			if (tile1.nodes == null)
			{
				throw new ArgumentException("tile1 does not contain any nodes");
			}
			if (tile2.nodes == null)
			{
				throw new ArgumentException("tile2 does not contain any nodes");
			}
			int num = Mathf.Clamp(tile2.x, tile1.x, tile1.x + tile1.w - 1);
			int num2 = Mathf.Clamp(tile1.x, tile2.x, tile2.x + tile2.w - 1);
			int num3 = Mathf.Clamp(tile2.z, tile1.z, tile1.z + tile1.d - 1);
			int num4 = Mathf.Clamp(tile1.z, tile2.z, tile2.z + tile2.d - 1);
			int num5;
			int num6;
			int num7;
			int num8;
			float num9;
			if (num == num2)
			{
				num5 = 2;
				num6 = 0;
				num7 = num3;
				num8 = num4;
				num9 = this.TileWorldSizeZ;
			}
			else
			{
				if (num3 != num4)
				{
					throw new ArgumentException("Tiles are not adjacent (neither x or z coordinates match)");
				}
				num5 = 0;
				num6 = 2;
				num7 = num;
				num8 = num2;
				num9 = this.TileWorldSizeX;
			}
			if (Math.Abs(num7 - num8) != 1)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Tiles are not adjacent (tile coordinates must differ by exactly 1. Got '",
					num7.ToString(),
					"' and '",
					num8.ToString(),
					"')"
				}));
			}
			int num10 = (int)Math.Round((double)((float)Math.Max(num7, num8) * num9 * 1000f));
			TriangleMeshNode[] nodes = tile1.nodes;
			TriangleMeshNode[] nodes2 = tile2.nodes;
			TriangleMeshNode[] array = ArrayPool<TriangleMeshNode>.Claim(nodes2.Length);
			int num11 = 0;
			for (int i = 0; i < nodes2.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = nodes2[i];
				int vertexCount = triangleMeshNode.GetVertexCount();
				for (int j = 0; j < vertexCount; j++)
				{
					Int3 vertexInGraphSpace = triangleMeshNode.GetVertexInGraphSpace(j);
					Int3 vertexInGraphSpace2 = triangleMeshNode.GetVertexInGraphSpace((j + 1) % vertexCount);
					if (Math.Abs(vertexInGraphSpace[num5] - num10) < 2 && Math.Abs(vertexInGraphSpace2[num5] - num10) < 2)
					{
						array[num11] = nodes2[i];
						num11++;
						break;
					}
				}
			}
			foreach (TriangleMeshNode triangleMeshNode2 in nodes)
			{
				int vertexCount2 = triangleMeshNode2.GetVertexCount();
				for (int l = 0; l < vertexCount2; l++)
				{
					Int3 vertexInGraphSpace3 = triangleMeshNode2.GetVertexInGraphSpace(l);
					Int3 vertexInGraphSpace4 = triangleMeshNode2.GetVertexInGraphSpace((l + 1) % vertexCount2);
					if (Math.Abs(vertexInGraphSpace3[num5] - num10) < 2 && Math.Abs(vertexInGraphSpace4[num5] - num10) < 2)
					{
						int num12 = Math.Min(vertexInGraphSpace3[num6], vertexInGraphSpace4[num6]);
						int num13 = Math.Max(vertexInGraphSpace3[num6], vertexInGraphSpace4[num6]);
						if (num12 != num13)
						{
							for (int m = 0; m < num11; m++)
							{
								TriangleMeshNode triangleMeshNode3 = array[m];
								int vertexCount3 = triangleMeshNode3.GetVertexCount();
								for (int n = 0; n < vertexCount3; n++)
								{
									Int3 vertexInGraphSpace5 = triangleMeshNode3.GetVertexInGraphSpace(n);
									Int3 vertexInGraphSpace6 = triangleMeshNode3.GetVertexInGraphSpace((n + 1) % vertexCount3);
									if (Math.Abs(vertexInGraphSpace5[num5] - num10) < 2 && Math.Abs(vertexInGraphSpace6[num5] - num10) < 2)
									{
										int num14 = Math.Min(vertexInGraphSpace5[num6], vertexInGraphSpace6[num6]);
										int num15 = Math.Max(vertexInGraphSpace5[num6], vertexInGraphSpace6[num6]);
										if (num14 != num15 && num13 > num14 && num12 < num15 && ((vertexInGraphSpace3 == vertexInGraphSpace5 && vertexInGraphSpace4 == vertexInGraphSpace6) || (vertexInGraphSpace3 == vertexInGraphSpace6 && vertexInGraphSpace4 == vertexInGraphSpace5) || VectorMath.SqrDistanceSegmentSegment((Vector3)vertexInGraphSpace3, (Vector3)vertexInGraphSpace4, (Vector3)vertexInGraphSpace5, (Vector3)vertexInGraphSpace6) < this.MaxTileConnectionEdgeDistance * this.MaxTileConnectionEdgeDistance))
										{
											uint costMagnitude = (uint)(triangleMeshNode2.position - triangleMeshNode3.position).costMagnitude;
											triangleMeshNode2.AddConnection(triangleMeshNode3, costMagnitude, (byte)l);
											triangleMeshNode3.AddConnection(triangleMeshNode2, costMagnitude, (byte)n);
										}
									}
								}
							}
						}
					}
				}
			}
			ArrayPool<TriangleMeshNode>.Release(ref array, false);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001BE07 File Offset: 0x0001A007
		public void StartBatchTileUpdate()
		{
			if (this.batchTileUpdate)
			{
				throw new InvalidOperationException("Calling StartBatchLoad when batching is already enabled");
			}
			this.batchTileUpdate = true;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001BE24 File Offset: 0x0001A024
		private void DestroyNodes(List<MeshNode> nodes)
		{
			for (int i = 0; i < this.batchNodesToDestroy.Count; i++)
			{
				this.batchNodesToDestroy[i].TemporaryFlag1 = true;
			}
			for (int j = 0; j < this.batchNodesToDestroy.Count; j++)
			{
				MeshNode meshNode = this.batchNodesToDestroy[j];
				for (int k = 0; k < meshNode.connections.Length; k++)
				{
					GraphNode node = meshNode.connections[k].node;
					if (!node.TemporaryFlag1)
					{
						node.RemoveConnection(meshNode);
					}
				}
				ArrayPool<Connection>.Release(ref meshNode.connections, true);
				meshNode.Destroy();
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001BEC5 File Offset: 0x0001A0C5
		private void TryConnect(int tileIdx1, int tileIdx2)
		{
			if (this.tiles[tileIdx1].flag && this.tiles[tileIdx2].flag && tileIdx1 >= tileIdx2)
			{
				return;
			}
			this.ConnectTiles(this.tiles[tileIdx1], this.tiles[tileIdx2]);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001BF00 File Offset: 0x0001A100
		public void EndBatchTileUpdate()
		{
			if (!this.batchTileUpdate)
			{
				throw new InvalidOperationException("Calling EndBatchTileUpdate when batching had not yet been started");
			}
			this.batchTileUpdate = false;
			this.DestroyNodes(this.batchNodesToDestroy);
			this.batchNodesToDestroy.ClearFast<MeshNode>();
			for (int i = 0; i < this.batchUpdatedTiles.Count; i++)
			{
				this.tiles[this.batchUpdatedTiles[i]].flag = true;
			}
			for (int j = 0; j < this.batchUpdatedTiles.Count; j++)
			{
				int num = this.batchUpdatedTiles[j] % this.tileXCount;
				int num2 = this.batchUpdatedTiles[j] / this.tileXCount;
				if (num > 0)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] - 1);
				}
				if (num < this.tileXCount - 1)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] + 1);
				}
				if (num2 > 0)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] - this.tileXCount);
				}
				if (num2 < this.tileZCount - 1)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] + this.tileXCount);
				}
			}
			for (int k = 0; k < this.batchUpdatedTiles.Count; k++)
			{
				this.tiles[this.batchUpdatedTiles[k]].flag = false;
			}
			this.batchUpdatedTiles.ClearFast<int>();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001C094 File Offset: 0x0001A294
		protected void ClearTile(int x, int z)
		{
			if (!this.batchTileUpdate)
			{
				throw new Exception("Must be called during a batch update. See StartBatchTileUpdate");
			}
			NavmeshTile tile = this.GetTile(x, z);
			if (tile == null)
			{
				return;
			}
			TriangleMeshNode[] nodes = tile.nodes;
			for (int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i] != null)
				{
					this.batchNodesToDestroy.Add(nodes[i]);
				}
			}
			ObjectPool<BBTree>.Release(ref tile.bbTree);
			this.tiles[x + z * this.tileXCount] = null;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001C108 File Offset: 0x0001A308
		private void PrepareNodeRecycling(int x, int z, Int3[] verts, int[] tris, TriangleMeshNode[] recycledNodeBuffer)
		{
			NavmeshTile tile = this.GetTile(x, z);
			if (tile == null || tile.nodes.Length == 0)
			{
				return;
			}
			TriangleMeshNode[] nodes = tile.nodes;
			Dictionary<int, int> dictionary = this.nodeRecyclingHashBuffer;
			int i = 0;
			int num = 0;
			while (i < tris.Length)
			{
				dictionary[verts[tris[i]].GetHashCode() + verts[tris[i + 1]].GetHashCode() + verts[tris[i + 2]].GetHashCode()] = num;
				i += 3;
				num++;
			}
			List<Connection> list = ListPool<Connection>.Claim();
			for (int j = 0; j < nodes.Length; j++)
			{
				TriangleMeshNode triangleMeshNode = nodes[j];
				Int3 @int;
				Int3 int2;
				Int3 int3;
				triangleMeshNode.GetVerticesInGraphSpace(out @int, out int2, out int3);
				int num2 = @int.GetHashCode() + int2.GetHashCode() + int3.GetHashCode();
				int num3;
				if (dictionary.TryGetValue(num2, out num3) && verts[tris[3 * num3]] == @int && verts[tris[3 * num3 + 1]] == int2 && verts[tris[3 * num3 + 2]] == int3)
				{
					recycledNodeBuffer[num3] = triangleMeshNode;
					nodes[j] = null;
					for (int k = 0; k < triangleMeshNode.connections.Length; k++)
					{
						if (triangleMeshNode.connections[k].node.GraphIndex != triangleMeshNode.GraphIndex)
						{
							list.Add(triangleMeshNode.connections[k]);
						}
					}
					ArrayPool<Connection>.Release(ref triangleMeshNode.connections, true);
					if (list.Count > 0)
					{
						triangleMeshNode.connections = list.ToArrayFromPool<Connection>();
						triangleMeshNode.SetConnectivityDirty();
						list.Clear();
					}
				}
			}
			dictionary.Clear();
			ListPool<Connection>.Release(ref list);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001C2F8 File Offset: 0x0001A4F8
		public void ReplaceTile(int x, int z, Int3[] verts, int[] tris)
		{
			int num = 1;
			int num2 = 1;
			if (x + num > this.tileXCount || z + num2 > this.tileZCount || x < 0 || z < 0)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Tile is placed at an out of bounds position or extends out of the graph bounds (",
					x.ToString(),
					", ",
					z.ToString(),
					" [",
					num.ToString(),
					", ",
					num2.ToString(),
					"] ",
					this.tileXCount.ToString(),
					" ",
					this.tileZCount.ToString(),
					")"
				}));
			}
			if (tris.Length % 3 != 0)
			{
				throw new ArgumentException("Triangle array's length must be a multiple of 3 (tris)");
			}
			if (verts.Length > 4095)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Too many vertices in the tile (",
					verts.Length.ToString(),
					" > ",
					4095.ToString(),
					")\nYou can enable ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* Inspector to raise this limit. Or you can use a smaller tile size to reduce the likelihood of this happening."
				}));
				verts = new Int3[0];
				tris = new int[0];
			}
			bool flag = !this.batchTileUpdate;
			if (flag)
			{
				this.StartBatchTileUpdate();
			}
			NavmeshTile navmeshTile = new NavmeshTile
			{
				x = x,
				z = z,
				w = num,
				d = num2,
				tris = tris,
				bbTree = ObjectPool<BBTree>.Claim(),
				graph = this
			};
			if (!Mathf.Approximately((float)x * this.TileWorldSizeX * 1000f, (float)Math.Round((double)((float)x * this.TileWorldSizeX * 1000f))))
			{
				Debug.LogWarning("Possible numerical imprecision. Consider adjusting tileSize and/or cellSize");
			}
			if (!Mathf.Approximately((float)z * this.TileWorldSizeZ * 1000f, (float)Math.Round((double)((float)z * this.TileWorldSizeZ * 1000f))))
			{
				Debug.LogWarning("Possible numerical imprecision. Consider adjusting tileSize and/or cellSize");
			}
			Int3 @int = (Int3)new Vector3((float)x * this.TileWorldSizeX, 0f, (float)z * this.TileWorldSizeZ);
			for (int i = 0; i < verts.Length; i++)
			{
				verts[i] += @int;
			}
			navmeshTile.vertsInGraphSpace = verts;
			navmeshTile.verts = (Int3[])verts.Clone();
			this.transform.Transform(navmeshTile.verts);
			TriangleMeshNode[] array = (navmeshTile.nodes = new TriangleMeshNode[tris.Length / 3]);
			this.PrepareNodeRecycling(x, z, navmeshTile.vertsInGraphSpace, tris, navmeshTile.nodes);
			this.ClearTile(x, z);
			this.tiles[x + z * this.tileXCount] = navmeshTile;
			this.batchUpdatedTiles.Add(x + z * this.tileXCount);
			this.CreateNodes(array, navmeshTile.tris, x + z * this.tileXCount, (uint)this.active.data.GetGraphIndex(this));
			navmeshTile.bbTree.RebuildFrom(array);
			NavmeshBase.CreateNodeConnections(navmeshTile.nodes);
			if (flag)
			{
				this.EndBatchTileUpdate();
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001C60C File Offset: 0x0001A80C
		protected void CreateNodes(TriangleMeshNode[] buffer, int[] tris, int tileIndex, uint graphIndex)
		{
			if (buffer == null || buffer.Length < tris.Length / 3)
			{
				throw new ArgumentException("buffer must be non null and at least as large as tris.Length/3");
			}
			tileIndex <<= 12;
			for (int i = 0; i < buffer.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = buffer[i];
				if (triangleMeshNode == null)
				{
					triangleMeshNode = (buffer[i] = new TriangleMeshNode(this.active));
				}
				triangleMeshNode.Walkable = true;
				triangleMeshNode.Tag = 0U;
				triangleMeshNode.Penalty = this.initialPenalty;
				triangleMeshNode.GraphIndex = graphIndex;
				triangleMeshNode.v0 = tris[i * 3] | tileIndex;
				triangleMeshNode.v1 = tris[i * 3 + 1] | tileIndex;
				triangleMeshNode.v2 = tris[i * 3 + 2] | tileIndex;
				if (this.RecalculateNormals && !VectorMath.IsClockwiseXZ(triangleMeshNode.GetVertexInGraphSpace(0), triangleMeshNode.GetVertexInGraphSpace(1), triangleMeshNode.GetVertexInGraphSpace(2)))
				{
					Memory.Swap<int>(ref tris[i * 3], ref tris[i * 3 + 2]);
					Memory.Swap<int>(ref triangleMeshNode.v0, ref triangleMeshNode.v2);
				}
				triangleMeshNode.UpdatePositionFromVertices();
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001C708 File Offset: 0x0001A908
		public NavmeshBase()
		{
			this.navmeshUpdateData = new NavmeshUpdates.NavmeshUpdateSettings(this);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001C787 File Offset: 0x0001A987
		public bool Linecast(Vector3 origin, Vector3 end)
		{
			return this.Linecast(origin, end, null);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001C792 File Offset: 0x0001A992
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit)
		{
			return NavmeshBase.Linecast(this, origin, end, hint, out hit, null, null);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint)
		{
			GraphHitInfo graphHitInfo;
			return NavmeshBase.Linecast(this, origin, end, hint, out graphHitInfo, null, null);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001C7BE File Offset: 0x0001A9BE
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace)
		{
			return NavmeshBase.Linecast(this, origin, end, hint, out hit, trace, null);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001C7CE File Offset: 0x0001A9CE
		public bool Linecast(Vector3 origin, Vector3 end, out GraphHitInfo hit, List<GraphNode> trace, Func<GraphNode, bool> filter)
		{
			return NavmeshBase.Linecast(this, origin, end, null, out hit, trace, filter);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001C7DE File Offset: 0x0001A9DE
		public static bool Linecast(NavmeshBase graph, Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit)
		{
			return NavmeshBase.Linecast(graph, origin, end, hint, out hit, null, null);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
		static NavmeshBase()
		{
			Side[] array = new Side[3];
			for (int i = 0; i < NavmeshBase.LinecastShapeEdgeLookup.Length; i++)
			{
				array[0] = (Side)(i & 3);
				array[1] = (Side)((i >> 2) & 3);
				array[2] = (Side)((i >> 4) & 3);
				NavmeshBase.LinecastShapeEdgeLookup[i] = byte.MaxValue;
				if (array[0] != (Side)3 && array[1] != (Side)3 && array[2] != (Side)3)
				{
					int num = int.MaxValue;
					for (int j = 0; j < 3; j++)
					{
						if ((array[j] == Side.Left || array[j] == Side.Colinear) && (array[(j + 1) % 3] == Side.Right || array[(j + 1) % 3] == Side.Colinear))
						{
							int num2 = ((array[j] == Side.Colinear) ? 1 : 0) + ((array[(j + 1) % 3] == Side.Colinear) ? 1 : 0);
							if (num2 < num)
							{
								NavmeshBase.LinecastShapeEdgeLookup[i] = (byte)j;
								num = num2;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001C904 File Offset: 0x0001AB04
		public static bool Linecast(NavmeshBase graph, Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace, Func<GraphNode, bool> filter = null)
		{
			hit = default(GraphHitInfo);
			if (float.IsNaN(origin.x + origin.y + origin.z))
			{
				throw new ArgumentException("origin is NaN");
			}
			if (float.IsNaN(end.x + end.y + end.z))
			{
				throw new ArgumentException("end is NaN");
			}
			TriangleMeshNode triangleMeshNode = hint as TriangleMeshNode;
			if (triangleMeshNode == null)
			{
				triangleMeshNode = graph.GetNearest(origin, NavmeshBase.NNConstraintNoneXZ).node as TriangleMeshNode;
				if (triangleMeshNode == null)
				{
					Debug.LogError("Could not find a valid node to start from");
					hit.origin = origin;
					hit.point = origin;
					return true;
				}
			}
			Int3 @int = triangleMeshNode.ClosestPointOnNodeXZInGraphSpace(origin);
			hit.origin = graph.transform.Transform((Vector3)@int);
			if (!triangleMeshNode.Walkable || (filter != null && !filter(triangleMeshNode)))
			{
				hit.node = triangleMeshNode;
				hit.point = hit.origin;
				hit.tangentOrigin = hit.origin;
				return true;
			}
			Int3 int2 = (Int3)graph.transform.InverseTransform(end);
			if (@int == int2)
			{
				hit.point = hit.origin;
				hit.node = triangleMeshNode;
				if (trace != null)
				{
					trace.Add(triangleMeshNode);
				}
				return false;
			}
			int num = 0;
			Int3 int3;
			Int3 int4;
			Int3 int5;
			int num3;
			for (;;)
			{
				num++;
				if (num > 2000)
				{
					break;
				}
				if (trace != null)
				{
					trace.Add(triangleMeshNode);
				}
				triangleMeshNode.GetVerticesInGraphSpace(out int3, out int4, out int5);
				int num2 = (int)VectorMath.SideXZ(@int, int2, int3);
				num2 |= (int)((int)VectorMath.SideXZ(@int, int2, int4) << 2);
				num2 |= (int)((int)VectorMath.SideXZ(@int, int2, int5) << 4);
				num3 = (int)NavmeshBase.LinecastShapeEdgeLookup[num2];
				if (VectorMath.SideXZ((num3 == 0) ? int3 : ((num3 == 1) ? int4 : int5), (num3 == 0) ? int4 : ((num3 == 1) ? int5 : int3), int2) != Side.Left)
				{
					goto Block_15;
				}
				if (num3 == 255)
				{
					goto Block_17;
				}
				bool flag = false;
				Connection[] connections = triangleMeshNode.connections;
				for (int i = 0; i < connections.Length; i++)
				{
					if ((int)connections[i].shapeEdge == num3)
					{
						TriangleMeshNode triangleMeshNode2 = connections[i].node as TriangleMeshNode;
						if (triangleMeshNode2 != null && triangleMeshNode2.Walkable && (filter == null || filter(triangleMeshNode2)))
						{
							Connection[] connections2 = triangleMeshNode2.connections;
							int num4 = -1;
							for (int j = 0; j < connections2.Length; j++)
							{
								if (connections2[j].node == triangleMeshNode)
								{
									num4 = (int)connections2[j].shapeEdge;
									break;
								}
							}
							if (num4 != -1)
							{
								Side side = VectorMath.SideXZ(@int, int2, triangleMeshNode2.GetVertexInGraphSpace(num4));
								Side side2 = VectorMath.SideXZ(@int, int2, triangleMeshNode2.GetVertexInGraphSpace((num4 + 1) % 3));
								flag = (side == Side.Right || side == Side.Colinear) && (side2 == Side.Left || side2 == Side.Colinear);
								if (flag)
								{
									triangleMeshNode = triangleMeshNode2;
									break;
								}
							}
						}
					}
				}
				if (!flag)
				{
					goto Block_27;
				}
			}
			Debug.LogError("Linecast was stuck in infinite loop. Breaking.");
			return true;
			Block_15:
			hit.point = end;
			hit.node = triangleMeshNode;
			TriangleMeshNode triangleMeshNode3 = graph.GetNearest(end, NavmeshBase.NNConstraintNoneXZ).node as TriangleMeshNode;
			return triangleMeshNode3 != triangleMeshNode && triangleMeshNode3 != null;
			Block_17:
			Debug.LogError("Line does not intersect node at all");
			hit.node = triangleMeshNode;
			hit.point = (hit.tangentOrigin = hit.origin);
			return true;
			Block_27:
			Vector3 vector = (Vector3)((num3 == 0) ? int3 : ((num3 == 1) ? int4 : int5));
			Vector3 vector2 = (Vector3)((num3 == 0) ? int4 : ((num3 == 1) ? int5 : int3));
			Vector3 vector3 = VectorMath.LineIntersectionPointXZ(vector, vector2, (Vector3)@int, (Vector3)int2);
			hit.point = graph.transform.Transform(vector3);
			hit.node = triangleMeshNode;
			Vector3 vector4 = graph.transform.Transform(vector);
			Vector3 vector5 = graph.transform.Transform(vector2);
			hit.tangent = vector5 - vector4;
			hit.tangentOrigin = vector4;
			return true;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		public override void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			using (GraphGizmoHelper singleFrameGizmoHelper = gizmos.GetSingleFrameGizmoHelper(this.active))
			{
				Bounds bounds = default(Bounds);
				bounds.SetMinMax(Vector3.zero, this.forcedBoundsSize);
				singleFrameGizmoHelper.builder.DrawWireCube(this.CalculateTransform(), bounds, Color.white);
			}
			if (this.tiles != null && (this.showMeshSurface || this.showMeshOutline || this.showNodeConnections))
			{
				RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
				hasher.AddHash(this.showMeshOutline ? 1 : 0);
				hasher.AddHash(this.showMeshSurface ? 1 : 0);
				hasher.AddHash(this.showNodeConnections ? 1 : 0);
				int num = 0;
				RetainedGizmos.Hasher hasher2 = hasher;
				int num2 = 0;
				for (int i = 0; i < this.tiles.Length; i++)
				{
					if (this.tiles[i] != null)
					{
						TriangleMeshNode[] nodes = this.tiles[i].nodes;
						for (int j = 0; j < nodes.Length; j++)
						{
							hasher2.HashNode(nodes[j]);
						}
						num2 += nodes.Length;
						if (num2 > 1024 || i % this.tileXCount == this.tileXCount - 1 || i == this.tiles.Length - 1)
						{
							if (!gizmos.Draw(hasher2))
							{
								using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher2))
								{
									if (this.showMeshSurface || this.showMeshOutline)
									{
										this.CreateNavmeshSurfaceVisualization(this.tiles, num, i + 1, gizmoHelper);
										NavmeshBase.CreateNavmeshOutlineVisualization(this.tiles, num, i + 1, gizmoHelper);
									}
									if (this.showNodeConnections)
									{
										for (int k = num; k <= i; k++)
										{
											if (this.tiles[k] != null)
											{
												TriangleMeshNode[] nodes2 = this.tiles[k].nodes;
												for (int l = 0; l < nodes2.Length; l++)
												{
													gizmoHelper.DrawConnections(nodes2[l]);
												}
											}
										}
									}
								}
							}
							gizmos.Draw(hasher2);
							num = i + 1;
							hasher2 = hasher;
							num2 = 0;
						}
					}
				}
			}
			if (this.active.showUnwalkableNodes)
			{
				base.DrawUnwalkableNodes(this.active.unwalkableNodeDebugSize);
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001CF50 File Offset: 0x0001B150
		private void CreateNavmeshSurfaceVisualization(NavmeshTile[] tiles, int startTile, int endTile, GraphGizmoHelper helper)
		{
			int num = 0;
			for (int i = startTile; i < endTile; i++)
			{
				if (tiles[i] != null)
				{
					num += tiles[i].nodes.Length;
				}
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(num * 3);
			Color[] array2 = ArrayPool<Color>.Claim(num * 3);
			int num2 = 0;
			for (int j = startTile; j < endTile; j++)
			{
				NavmeshTile navmeshTile = tiles[j];
				if (navmeshTile != null)
				{
					for (int k = 0; k < navmeshTile.nodes.Length; k++)
					{
						TriangleMeshNode triangleMeshNode = navmeshTile.nodes[k];
						Int3 @int;
						Int3 int2;
						Int3 int3;
						triangleMeshNode.GetVertices(out @int, out int2, out int3);
						int num3 = num2 + k * 3;
						array[num3] = (Vector3)@int;
						array[num3 + 1] = (Vector3)int2;
						array[num3 + 2] = (Vector3)int3;
						Color color = helper.NodeColor(triangleMeshNode);
						array2[num3] = (array2[num3 + 1] = (array2[num3 + 2] = color));
					}
					num2 += navmeshTile.nodes.Length * 3;
				}
			}
			if (this.showMeshSurface)
			{
				helper.DrawTriangles(array, array2, num);
			}
			if (this.showMeshOutline)
			{
				helper.DrawWireTriangles(array, array2, num);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			ArrayPool<Color>.Release(ref array2, false);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		private static void CreateNavmeshOutlineVisualization(NavmeshTile[] tiles, int startTile, int endTile, GraphGizmoHelper helper)
		{
			bool[] array = new bool[3];
			for (int i = startTile; i < endTile; i++)
			{
				NavmeshTile navmeshTile = tiles[i];
				if (navmeshTile != null)
				{
					for (int j = 0; j < navmeshTile.nodes.Length; j++)
					{
						array[0] = (array[1] = (array[2] = false));
						TriangleMeshNode triangleMeshNode = navmeshTile.nodes[j];
						for (int k = 0; k < triangleMeshNode.connections.Length; k++)
						{
							TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[k].node as TriangleMeshNode;
							if (triangleMeshNode2 != null && triangleMeshNode2.GraphIndex == triangleMeshNode.GraphIndex)
							{
								for (int l = 0; l < 3; l++)
								{
									for (int m = 0; m < 3; m++)
									{
										if (triangleMeshNode.GetVertexIndex(l) == triangleMeshNode2.GetVertexIndex((m + 1) % 3) && triangleMeshNode.GetVertexIndex((l + 1) % 3) == triangleMeshNode2.GetVertexIndex(m))
										{
											array[l] = true;
											l = 3;
											break;
										}
									}
								}
							}
						}
						Color color = helper.NodeColor(triangleMeshNode);
						for (int n = 0; n < 3; n++)
						{
							if (!array[n])
							{
								helper.builder.DrawLine((Vector3)triangleMeshNode.GetVertex(n), (Vector3)triangleMeshNode.GetVertex((n + 1) % 3), color);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			BinaryWriter writer = ctx.writer;
			if (this.tiles == null)
			{
				writer.Write(-1);
				return;
			}
			writer.Write(this.tileXCount);
			writer.Write(this.tileZCount);
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					NavmeshTile navmeshTile = this.tiles[j + i * this.tileXCount];
					if (navmeshTile == null)
					{
						throw new Exception("NULL Tile");
					}
					writer.Write(navmeshTile.x);
					writer.Write(navmeshTile.z);
					if (navmeshTile.x == j && navmeshTile.z == i)
					{
						writer.Write(navmeshTile.w);
						writer.Write(navmeshTile.d);
						writer.Write(navmeshTile.tris.Length);
						for (int k = 0; k < navmeshTile.tris.Length; k++)
						{
							writer.Write(navmeshTile.tris[k]);
						}
						writer.Write(navmeshTile.verts.Length);
						for (int l = 0; l < navmeshTile.verts.Length; l++)
						{
							ctx.SerializeInt3(navmeshTile.verts[l]);
						}
						writer.Write(navmeshTile.vertsInGraphSpace.Length);
						for (int m = 0; m < navmeshTile.vertsInGraphSpace.Length; m++)
						{
							ctx.SerializeInt3(navmeshTile.vertsInGraphSpace[m]);
						}
						writer.Write(navmeshTile.nodes.Length);
						for (int n = 0; n < navmeshTile.nodes.Length; n++)
						{
							navmeshTile.nodes[n].SerializeNode(ctx);
						}
					}
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			BinaryReader reader = ctx.reader;
			this.tileXCount = reader.ReadInt32();
			if (this.tileXCount < 0)
			{
				return;
			}
			this.tileZCount = reader.ReadInt32();
			this.transform = this.CalculateTransform();
			this.tiles = new NavmeshTile[this.tileXCount * this.tileZCount];
			TriangleMeshNode.SetNavmeshHolder((int)ctx.graphIndex, this);
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					int num = j + i * this.tileXCount;
					int num2 = reader.ReadInt32();
					if (num2 < 0)
					{
						throw new Exception("Invalid tile coordinates (x < 0)");
					}
					int num3 = reader.ReadInt32();
					if (num3 < 0)
					{
						throw new Exception("Invalid tile coordinates (z < 0)");
					}
					if (num2 != j || num3 != i)
					{
						this.tiles[num] = this.tiles[num3 * this.tileXCount + num2];
					}
					else
					{
						NavmeshTile[] array = this.tiles;
						int num4 = num;
						NavmeshTile navmeshTile = new NavmeshTile();
						navmeshTile.x = num2;
						navmeshTile.z = num3;
						navmeshTile.w = reader.ReadInt32();
						navmeshTile.d = reader.ReadInt32();
						navmeshTile.bbTree = ObjectPool<BBTree>.Claim();
						navmeshTile.graph = this;
						NavmeshTile navmeshTile2 = navmeshTile;
						array[num4] = navmeshTile;
						NavmeshTile navmeshTile3 = navmeshTile2;
						int num5 = reader.ReadInt32();
						if (num5 % 3 != 0)
						{
							throw new Exception("Corrupt data. Triangle indices count must be divisable by 3. Read " + num5.ToString());
						}
						navmeshTile3.tris = new int[num5];
						for (int k = 0; k < navmeshTile3.tris.Length; k++)
						{
							navmeshTile3.tris[k] = reader.ReadInt32();
						}
						navmeshTile3.verts = new Int3[reader.ReadInt32()];
						for (int l = 0; l < navmeshTile3.verts.Length; l++)
						{
							navmeshTile3.verts[l] = ctx.DeserializeInt3();
						}
						if (ctx.meta.version.Major >= 4)
						{
							navmeshTile3.vertsInGraphSpace = new Int3[reader.ReadInt32()];
							if (navmeshTile3.vertsInGraphSpace.Length != navmeshTile3.verts.Length)
							{
								throw new Exception("Corrupt data. Array lengths did not match");
							}
							for (int m = 0; m < navmeshTile3.verts.Length; m++)
							{
								navmeshTile3.vertsInGraphSpace[m] = ctx.DeserializeInt3();
							}
						}
						else
						{
							navmeshTile3.vertsInGraphSpace = new Int3[navmeshTile3.verts.Length];
							navmeshTile3.verts.CopyTo(navmeshTile3.vertsInGraphSpace, 0);
							this.transform.InverseTransform(navmeshTile3.vertsInGraphSpace);
						}
						int num6 = reader.ReadInt32();
						navmeshTile3.nodes = new TriangleMeshNode[num6];
						num <<= 12;
						for (int n = 0; n < navmeshTile3.nodes.Length; n++)
						{
							TriangleMeshNode triangleMeshNode = new TriangleMeshNode(this.active);
							navmeshTile3.nodes[n] = triangleMeshNode;
							triangleMeshNode.DeserializeNode(ctx);
							triangleMeshNode.v0 = navmeshTile3.tris[n * 3] | num;
							triangleMeshNode.v1 = navmeshTile3.tris[n * 3 + 1] | num;
							triangleMeshNode.v2 = navmeshTile3.tris[n * 3 + 2] | num;
							triangleMeshNode.UpdatePositionFromVertices();
						}
						navmeshTile3.bbTree.RebuildFrom(navmeshTile3.nodes);
					}
				}
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			if (ctx.meta.version < AstarSerializer.V4_1_0 && this.tiles != null)
			{
				Dictionary<TriangleMeshNode, Connection[]> conns = this.tiles.SelectMany((NavmeshTile s) => s.nodes).ToDictionary((TriangleMeshNode n) => n, (TriangleMeshNode n) => n.connections ?? new Connection[0]);
				NavmeshTile[] array = this.tiles;
				for (int i = 0; i < array.Length; i++)
				{
					NavmeshBase.CreateNodeConnections(array[i].nodes);
				}
				foreach (NavmeshTile navmeshTile in this.tiles)
				{
					this.ConnectTileWithNeighbours(navmeshTile, false);
				}
				this.GetNodes(delegate(GraphNode node)
				{
					TriangleMeshNode triNode = node as TriangleMeshNode;
					IEnumerable<Connection> enumerable = conns[triNode];
					Func<Connection, bool> <>9__4;
					Func<Connection, bool> func;
					if ((func = <>9__4) == null)
					{
						func = (<>9__4 = (Connection conn) => !triNode.ContainsConnection(conn.node));
					}
					foreach (Connection connection in enumerable.Where(func).ToList<Connection>())
					{
						triNode.AddConnection(connection.node, connection.cost, connection.shapeEdge);
					}
				});
			}
			this.transform = this.CalculateTransform();
		}

		// Token: 0x040002E4 RID: 740
		public const int VertexIndexMask = 4095;

		// Token: 0x040002E5 RID: 741
		public const int TileIndexMask = 524287;

		// Token: 0x040002E6 RID: 742
		public const int TileIndexOffset = 12;

		// Token: 0x040002E7 RID: 743
		[JsonMember]
		public Vector3 forcedBoundsSize = new Vector3(100f, 40f, 100f);

		// Token: 0x040002E8 RID: 744
		[JsonMember]
		public bool showMeshOutline = true;

		// Token: 0x040002E9 RID: 745
		[JsonMember]
		public bool showNodeConnections;

		// Token: 0x040002EA RID: 746
		[JsonMember]
		public bool showMeshSurface = true;

		// Token: 0x040002EB RID: 747
		public int tileXCount;

		// Token: 0x040002EC RID: 748
		public int tileZCount;

		// Token: 0x040002ED RID: 749
		protected NavmeshTile[] tiles;

		// Token: 0x040002EE RID: 750
		[JsonMember]
		public bool nearestSearchOnlyXZ;

		// Token: 0x040002EF RID: 751
		[JsonMember]
		public bool enableNavmeshCutting = true;

		// Token: 0x040002F0 RID: 752
		internal readonly NavmeshUpdates.NavmeshUpdateSettings navmeshUpdateData;

		// Token: 0x040002F1 RID: 753
		private bool batchTileUpdate;

		// Token: 0x040002F2 RID: 754
		private List<int> batchUpdatedTiles = new List<int>();

		// Token: 0x040002F3 RID: 755
		private List<MeshNode> batchNodesToDestroy = new List<MeshNode>();

		// Token: 0x040002F4 RID: 756
		public GraphTransform transform = new GraphTransform(Matrix4x4.identity);

		// Token: 0x040002F5 RID: 757
		public Action<NavmeshTile[]> OnRecalculatedTiles;

		// Token: 0x040002F6 RID: 758
		private static readonly NNConstraint NNConstraintDistanceXZ = new NNConstraint
		{
			distanceXZ = true
		};

		// Token: 0x040002F7 RID: 759
		private Dictionary<int, int> nodeRecyclingHashBuffer = new Dictionary<int, int>();

		// Token: 0x040002F8 RID: 760
		private static readonly NNConstraint NNConstraintNoneXZ = new NNConstraint
		{
			constrainWalkability = false,
			constrainArea = false,
			constrainTags = false,
			constrainDistance = false,
			graphMask = -1,
			distanceXZ = true
		};

		// Token: 0x040002F9 RID: 761
		private static readonly byte[] LinecastShapeEdgeLookup = new byte[64];
	}
}
