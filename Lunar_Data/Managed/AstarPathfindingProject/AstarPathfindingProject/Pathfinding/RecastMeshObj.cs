using System;
using System.Collections.Generic;
using Pathfinding.Graphs.Navmesh;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000103 RID: 259
	[AddComponentMenu("Pathfinding/Navmesh/RecastMeshObj")]
	[DisallowMultipleComponent]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/recastmeshobj.html")]
	public class RecastMeshObj : VersionedMonoBehaviour
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0002BCDC File Offset: 0x00029EDC
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0002BD1B File Offset: 0x00029F1B
		[Obsolete("Use mode and surfaceID instead")]
		public int area
		{
			get
			{
				switch (this.mode)
				{
				case RecastMeshObj.Mode.UnwalkableSurface:
					return -1;
				case RecastMeshObj.Mode.WalkableSurfaceWithSeam:
					return this.surfaceID;
				case RecastMeshObj.Mode.WalkableSurfaceWithTag:
					return this.surfaceID;
				}
				return 0;
			}
			set
			{
				if (value <= -1)
				{
					this.mode = RecastMeshObj.Mode.UnwalkableSurface;
				}
				if (value == 0)
				{
					this.mode = RecastMeshObj.Mode.WalkableSurface;
				}
				if (value > 0)
				{
					this.mode = RecastMeshObj.Mode.WalkableSurfaceWithSeam;
					this.surfaceID = value;
				}
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002BD44 File Offset: 0x00029F44
		private void OnEnable()
		{
			this.surfaceID = Mathf.Clamp(this.surfaceID, 0, 33554432);
			if (!this.treeKey.isValid)
			{
				this.treeKey = RecastMeshObj.tree.Add(this.CalculateBounds(), this);
				if (this.dynamic)
				{
					BatchedEvents.Add<RecastMeshObj>(this, BatchedEvents.Event.Custom, new Action<RecastMeshObj[], int>(RecastMeshObj.OnUpdate), 0);
				}
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002BDA8 File Offset: 0x00029FA8
		private void OnDisable()
		{
			BatchedEvents.Remove<RecastMeshObj>(this);
			Bounds bounds = RecastMeshObj.tree.Remove(this.treeKey);
			this.treeKey = default(AABBTree<RecastMeshObj>.Key);
			if (!this.dynamic)
			{
				Bounds bounds2 = this.CalculateBounds();
				bounds.Expand(0.001f);
				bounds2.Encapsulate(bounds);
				if ((bounds2.center - bounds.center).sqrMagnitude > 0.0001f || (bounds2.extents - bounds.extents).sqrMagnitude > 0.0001f)
				{
					string text = "The RecastMeshObj has been moved or resized since it was enabled. You should set dynamic to true for moving objects, or disable the component while moving it. The bounds changed from ";
					Bounds bounds3 = bounds;
					string text2 = bounds3.ToString();
					string text3 = " to ";
					bounds3 = bounds2;
					Debug.LogError(text + text2 + text3 + bounds3.ToString(), this);
				}
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002BE78 File Offset: 0x0002A078
		private static void OnUpdate(RecastMeshObj[] components, int _)
		{
			foreach (RecastMeshObj recastMeshObj in components)
			{
				if (recastMeshObj != null && recastMeshObj.transform.hasChanged)
				{
					Bounds bounds = recastMeshObj.CalculateBounds();
					if (RecastMeshObj.tree.GetBounds(recastMeshObj.treeKey) != bounds)
					{
						RecastMeshObj.tree.Move(recastMeshObj.treeKey, bounds);
					}
					recastMeshObj.transform.hasChanged = false;
				}
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		public static void GetAllInBounds(List<RecastMeshObj> buffer, Bounds bounds)
		{
			BatchedEvents.ProcessEvent<RecastMeshObj>(BatchedEvents.Event.Custom);
			if (!Application.isPlaying)
			{
				RecastMeshObj[] array = UnityCompatibility.FindObjectsByTypeSorted<RecastMeshObj>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].enabled && bounds.Intersects(array[i].CalculateBounds()))
					{
						buffer.Add(array[i]);
					}
				}
				return;
			}
			if (Time.timeSinceLevelLoad == 0f)
			{
				RecastMeshObj[] array2 = UnityCompatibility.FindObjectsByTypeUnsorted<RecastMeshObj>();
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].OnEnable();
				}
			}
			RecastMeshObj.tree.Query(bounds, buffer);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002BF74 File Offset: 0x0002A174
		public void ResolveMeshSource(out MeshFilter meshFilter, out Collider collider, out Collider2D collider2D)
		{
			meshFilter = null;
			collider = null;
			collider2D = null;
			switch (this.geometrySource)
			{
			case RecastMeshObj.GeometrySource.Auto:
			{
				MeshRenderer meshRenderer;
				if (base.TryGetComponent<MeshRenderer>(out meshRenderer) && base.TryGetComponent<MeshFilter>(out meshFilter) && meshFilter.sharedMesh != null)
				{
					return;
				}
				if (base.TryGetComponent<Collider>(out collider))
				{
					return;
				}
				base.TryGetComponent<Collider2D>(out collider2D);
				return;
			}
			case RecastMeshObj.GeometrySource.MeshFilter:
				base.TryGetComponent<MeshFilter>(out meshFilter);
				return;
			case RecastMeshObj.GeometrySource.Collider:
				if (base.TryGetComponent<Collider>(out collider))
				{
					return;
				}
				base.TryGetComponent<Collider2D>(out collider2D);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		private Bounds CalculateBounds()
		{
			MeshFilter meshFilter;
			Collider collider;
			Collider2D collider2D;
			this.ResolveMeshSource(out meshFilter, out collider, out collider2D);
			if (collider != null)
			{
				return collider.bounds;
			}
			if (collider2D != null)
			{
				return collider2D.bounds;
			}
			if (!(meshFilter != null))
			{
				Debug.LogError("Could not find an attached mesh source", this);
				return new Bounds(Vector3.zero, Vector3.one);
			}
			MeshRenderer meshRenderer;
			if (base.TryGetComponent<MeshRenderer>(out meshRenderer))
			{
				return meshRenderer.bounds;
			}
			Debug.LogError("Cannot use a MeshFilter as a geomtry source without a MeshRenderer attached to the same GameObject.", this);
			return new Bounds(Vector3.zero, Vector3.one);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002C084 File Offset: 0x0002A284
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			int num;
			if (migrations.TryMigrateFromLegacyFormat(out num))
			{
				if (num == 1)
				{
					this.area = this.surfaceID;
				}
				if (num <= 2)
				{
					this.includeInScan = RecastMeshObj.ScanInclusion.AlwaysInclude;
				}
				if (this.mode == (RecastMeshObj.Mode)0)
				{
					this.includeInScan = RecastMeshObj.ScanInclusion.AlwaysExclude;
				}
			}
		}

		// Token: 0x04000560 RID: 1376
		protected static AABBTree<RecastMeshObj> tree = new AABBTree<RecastMeshObj>();

		// Token: 0x04000561 RID: 1377
		public bool dynamic = true;

		// Token: 0x04000562 RID: 1378
		public bool solid;

		// Token: 0x04000563 RID: 1379
		public RecastMeshObj.GeometrySource geometrySource;

		// Token: 0x04000564 RID: 1380
		public RecastMeshObj.ScanInclusion includeInScan;

		// Token: 0x04000565 RID: 1381
		[FormerlySerializedAs("area")]
		public int surfaceID = 1;

		// Token: 0x04000566 RID: 1382
		public RecastMeshObj.Mode mode = RecastMeshObj.Mode.WalkableSurface;

		// Token: 0x04000567 RID: 1383
		private AABBTree<RecastMeshObj>.Key treeKey;

		// Token: 0x02000104 RID: 260
		public enum ScanInclusion
		{
			// Token: 0x04000569 RID: 1385
			Auto,
			// Token: 0x0400056A RID: 1386
			AlwaysExclude,
			// Token: 0x0400056B RID: 1387
			AlwaysInclude
		}

		// Token: 0x02000105 RID: 261
		public enum GeometrySource
		{
			// Token: 0x0400056D RID: 1389
			Auto,
			// Token: 0x0400056E RID: 1390
			MeshFilter,
			// Token: 0x0400056F RID: 1391
			Collider
		}

		// Token: 0x02000106 RID: 262
		public enum Mode
		{
			// Token: 0x04000571 RID: 1393
			UnwalkableSurface = 1,
			// Token: 0x04000572 RID: 1394
			WalkableSurface,
			// Token: 0x04000573 RID: 1395
			WalkableSurfaceWithSeam,
			// Token: 0x04000574 RID: 1396
			WalkableSurfaceWithTag
		}
	}
}
