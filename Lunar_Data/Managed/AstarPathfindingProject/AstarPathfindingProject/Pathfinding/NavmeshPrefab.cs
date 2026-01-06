using System;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.Jobs;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000073 RID: 115
	[AddComponentMenu("Pathfinding/Navmesh Prefab")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/navmeshprefab.html")]
	public class NavmeshPrefab : VersionedMonoBehaviour
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00013B34 File Offset: 0x00011D34
		protected override void Reset()
		{
			base.Reset();
			AstarPath.FindAstarPath();
			if (AstarPath.active != null && AstarPath.active.data.recastGraph != null)
			{
				RecastGraph recastGraph = AstarPath.active.data.recastGraph;
				this.bounds = new Bounds(Vector3.zero, new Vector3(recastGraph.TileWorldSizeX, recastGraph.forcedBoundsSize.y, recastGraph.TileWorldSizeZ));
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00013BA6 File Offset: 0x00011DA6
		[ContextMenu("Snap to closest tile alignment")]
		public void SnapToClosestTileAlignment()
		{
			AstarPath.FindAstarPath();
			if (AstarPath.active != null && AstarPath.active.data.recastGraph != null)
			{
				this.SnapToClosestTileAlignment(AstarPath.active.data.recastGraph);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00013BE0 File Offset: 0x00011DE0
		[ContextMenu("Apply here")]
		public void Apply()
		{
			AstarPath.FindAstarPath();
			if (AstarPath.active != null && AstarPath.active.data.recastGraph != null)
			{
				RecastGraph recastGraph = AstarPath.active.data.recastGraph;
				this.Apply(recastGraph);
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00013C28 File Offset: 0x00011E28
		public void SnapToClosestTileAlignment(RecastGraph graph)
		{
			TileLayout tileLayout = new TileLayout(graph);
			IntRect intRect;
			int num;
			float num2;
			NavmeshPrefab.SnapToGraph(tileLayout, base.transform.position, base.transform.rotation, this.bounds, out intRect, out num, out num2);
			Bounds tileBoundsInGraphSpace = tileLayout.GetTileBoundsInGraphSpace(intRect.xmin, intRect.ymin, intRect.Width, intRect.Height);
			Vector3 vector = new Vector3(tileBoundsInGraphSpace.center.x, num2, tileBoundsInGraphSpace.center.z);
			base.transform.rotation = Quaternion.Euler(graph.rotation) * Quaternion.Euler(0f, (float)(num * 90), 0f);
			base.transform.position = tileLayout.transform.Transform(vector) + base.transform.rotation * (-this.bounds.center + new Vector3(0f, this.bounds.extents.y, 0f));
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00013D38 File Offset: 0x00011F38
		public void SnapSizeToClosestTileMultiple(RecastGraph graph)
		{
			this.bounds = NavmeshPrefab.SnapSizeToClosestTileMultiple(graph, this.bounds);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00013D4C File Offset: 0x00011F4C
		private void Start()
		{
			this.startHasRun = true;
			if (this.applyOnStart && this.serializedNavmesh != null && AstarPath.active != null && AstarPath.active.data.recastGraph != null)
			{
				this.Apply(AstarPath.active.data.recastGraph);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00013DAC File Offset: 0x00011FAC
		private void OnEnable()
		{
			if (this.startHasRun && this.applyOnStart && this.serializedNavmesh != null && AstarPath.active != null && AstarPath.active.data.recastGraph != null)
			{
				this.Apply(AstarPath.active.data.recastGraph);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013E0C File Offset: 0x0001200C
		private void OnDisable()
		{
			if (this.removeTilesWhenDisabled && this.serializedNavmesh != null && AstarPath.active != null)
			{
				Vector3 pos = base.transform.position;
				Quaternion rot = base.transform.rotation;
				AstarPath.active.AddWorkItem(delegate(IWorkItemContext ctx)
				{
					RecastGraph recastGraph = AstarPath.active.data.recastGraph;
					if (recastGraph != null)
					{
						IntRect intRect;
						int num;
						float num2;
						NavmeshPrefab.SnapToGraph(new TileLayout(recastGraph), pos, rot, this.bounds, out intRect, out num, out num2);
						recastGraph.ClearTiles(intRect);
					}
				});
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013E84 File Offset: 0x00012084
		public static Bounds SnapSizeToClosestTileMultiple(RecastGraph graph, Bounds bounds)
		{
			float num = Mathf.Max((float)graph.editorTileSize * graph.cellSize, 0.001f);
			Vector2 vector = new Vector2(bounds.size.x / num, bounds.size.z / num);
			Int2 @int = new Int2(Mathf.Max(1, Mathf.RoundToInt(vector.x)), Mathf.Max(1, Mathf.RoundToInt(vector.y)));
			return new Bounds(bounds.center, new Vector3((float)@int.x * num, bounds.size.y, (float)@int.y * num));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013F28 File Offset: 0x00012128
		public static void SnapToGraph(TileLayout tileLayout, Vector3 position, Quaternion rotation, Bounds bounds, out IntRect tileRect, out int snappedRotation, out float yOffset)
		{
			Vector3 vector = tileLayout.transform.InverseTransformVector(rotation * Vector3.right);
			snappedRotation = -Mathf.RoundToInt(Mathf.Atan2(vector.z, vector.x) / 1.5707964f);
			Quaternion quaternion = Quaternion.Euler(0f, (float)(snappedRotation * 90), 0f);
			Matrix4x4 matrix4x = tileLayout.transform.inverseMatrix * Matrix4x4.TRS(position + quaternion * bounds.center, quaternion, Vector3.one);
			Vector3 vector2 = matrix4x.MultiplyPoint3x4(-bounds.extents);
			Vector3 vector3 = matrix4x.MultiplyPoint3x4(bounds.extents);
			Vector3 vector4 = Vector3.Min(vector2, vector3);
			Vector3 vector5 = Vector3.Scale(vector4, new Vector3(1f / tileLayout.TileWorldSizeX, 1f, 1f / tileLayout.TileWorldSizeZ));
			Int2 @int = new Int2(Mathf.RoundToInt(vector5.x), Mathf.RoundToInt(vector5.z));
			Vector2 vector6 = new Vector2(bounds.size.x, bounds.size.z);
			if ((snappedRotation % 2 + 2) % 2 == 1)
			{
				Memory.Swap<float>(ref vector6.x, ref vector6.y);
			}
			int num = Mathf.Max(1, Mathf.RoundToInt(vector6.x / tileLayout.TileWorldSizeX));
			int num2 = Mathf.Max(1, Mathf.RoundToInt(vector6.y / tileLayout.TileWorldSizeZ));
			tileRect = new IntRect(@int.x, @int.y, @int.x + num - 1, @int.y + num2 - 1);
			yOffset = vector4.y;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000140DC File Offset: 0x000122DC
		public void Apply(RecastGraph graph)
		{
			if (this.serializedNavmesh == null)
			{
				throw new InvalidOperationException("Cannot Apply NavmeshPrefab because no serialized data has been set");
			}
			AstarPath.active.AddWorkItem(delegate
			{
				IntRect intRect;
				int num;
				float num2;
				NavmeshPrefab.SnapToGraph(new TileLayout(graph), this.transform.position, this.transform.rotation, this.bounds, out intRect, out num, out num2);
				TileMeshes tileMeshes = TileMeshes.Deserialize(this.serializedNavmesh.bytes);
				tileMeshes.Rotate(num);
				if (tileMeshes.tileRect.Width != intRect.Width || tileMeshes.tileRect.Height != intRect.Height)
				{
					throw new Exception(string.Concat(new string[]
					{
						"NavmeshPrefab has been scanned with a different size than it is right now (or with a different graph). Expected to find ",
						intRect.Width.ToString(),
						"x",
						intRect.Height.ToString(),
						" tiles, but found ",
						tileMeshes.tileRect.Width.ToString(),
						"x",
						tileMeshes.tileRect.Height.ToString()
					}));
				}
				tileMeshes.tileRect = intRect;
				graph.ReplaceTiles(tileMeshes, num2);
			});
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001412C File Offset: 0x0001232C
		public byte[] Scan()
		{
			AstarPath.FindAstarPath();
			if (AstarPath.active == null || AstarPath.active.data.recastGraph == null)
			{
				throw new InvalidOperationException("There's no recast graph in the scene. Add one if you want to scan this navmesh prefab.");
			}
			return this.Scan(AstarPath.active.data.recastGraph);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001417C File Offset: 0x0001237C
		public byte[] Scan(RecastGraph graph)
		{
			NavmeshPrefab.SerializedOutput serializedOutput = this.ScanAsync(graph).Complete();
			byte[] data = serializedOutput.data;
			serializedOutput.Dispose();
			return data;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000141A8 File Offset: 0x000123A8
		public Promise<NavmeshPrefab.SerializedOutput> ScanAsync(RecastGraph graph)
		{
			DisposeArena disposeArena = new DisposeArena();
			TileLayout tileLayout = new TileLayout(new Bounds(base.transform.position + base.transform.rotation * this.bounds.center, this.bounds.size), base.transform.rotation, graph.cellSize, graph.editorTileSize, graph.useTiles);
			TileBuilder tileBuilder = RecastBuilder.BuildTileMeshes(graph, tileLayout, new IntRect(0, 0, tileLayout.tileCount.x - 1, tileLayout.tileCount.y - 1));
			tileBuilder.scene = base.gameObject.scene;
			Promise<TileBuilder.TileBuilderOutput> promise = tileBuilder.Schedule(disposeArena);
			NavmeshPrefab.SerializedOutput serializedOutput = new NavmeshPrefab.SerializedOutput
			{
				promise = promise,
				arena = disposeArena
			};
			return new Promise<NavmeshPrefab.SerializedOutput>(new NavmeshPrefab.SerializeJob
			{
				tileMeshesPromise = promise,
				output = serializedOutput
			}.ScheduleManaged(promise.handle), serializedOutput);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000142A0 File Offset: 0x000124A0
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			int num;
			migrations.TryMigrateFromLegacyFormat(out num);
			if (migrations.AddAndMaybeRunMigration(1, true))
			{
				this.removeTilesWhenDisabled = false;
			}
			base.OnUpgradeSerializedData(ref migrations, unityThread);
		}

		// Token: 0x0400028B RID: 651
		public TextAsset serializedNavmesh;

		// Token: 0x0400028C RID: 652
		public bool applyOnStart = true;

		// Token: 0x0400028D RID: 653
		public bool removeTilesWhenDisabled = true;

		// Token: 0x0400028E RID: 654
		public Bounds bounds = new Bounds(Vector3.zero, new Vector3(10f, 10f, 10f));

		// Token: 0x0400028F RID: 655
		private bool startHasRun;

		// Token: 0x02000074 RID: 116
		public class SerializedOutput : IProgress, IDisposable
		{
			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060003DF RID: 991 RVA: 0x00014309 File Offset: 0x00012509
			public float Progress
			{
				get
				{
					return this.promise.Progress;
				}
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x00014316 File Offset: 0x00012516
			public void Dispose()
			{
				this.promise.Dispose();
				this.arena.DisposeAll();
			}

			// Token: 0x04000290 RID: 656
			public Promise<TileBuilder.TileBuilderOutput> promise;

			// Token: 0x04000291 RID: 657
			public byte[] data;

			// Token: 0x04000292 RID: 658
			public DisposeArena arena;
		}

		// Token: 0x02000075 RID: 117
		private struct SerializeJob : IJob
		{
			// Token: 0x060003E2 RID: 994 RVA: 0x00014330 File Offset: 0x00012530
			public void Execute()
			{
				TileBuilder.TileBuilderOutput value = this.tileMeshesPromise.GetValue();
				this.output.data = value.tileMeshes.ToManaged().Serialize();
			}

			// Token: 0x04000293 RID: 659
			public Promise<TileBuilder.TileBuilderOutput> tileMeshesPromise;

			// Token: 0x04000294 RID: 660
			public NavmeshPrefab.SerializedOutput output;
		}
	}
}
