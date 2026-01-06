using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000097 RID: 151
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_dynamic_grid_obstacle.php")]
	public class DynamicGridObstacle : GraphModifier
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0002B13C File Offset: 0x0002933C
		private Bounds bounds
		{
			get
			{
				if (this.coll != null)
				{
					return this.coll.bounds;
				}
				Bounds bounds = this.coll2D.bounds;
				bounds.extents += new Vector3(0f, 0f, 10000f);
				return bounds;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0002B196 File Offset: 0x00029396
		private bool colliderEnabled
		{
			get
			{
				if (!(this.coll != null))
				{
					return this.coll2D.enabled;
				}
				return this.coll.enabled;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0002B1C0 File Offset: 0x000293C0
		protected override void Awake()
		{
			base.Awake();
			this.coll = base.GetComponent<Collider>();
			this.coll2D = base.GetComponent<Collider2D>();
			this.tr = base.transform;
			if (this.coll == null && this.coll2D == null && Application.isPlaying)
			{
				throw new Exception("A collider or 2D collider must be attached to the GameObject(" + base.gameObject.name + ") for the DynamicGridObstacle to work");
			}
			this.prevBounds = this.bounds;
			this.prevRotation = this.tr.rotation;
			this.prevEnabled = false;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0002B25E File Offset: 0x0002945E
		public override void OnPostScan()
		{
			if (this.coll == null)
			{
				this.Awake();
			}
			if (this.coll != null)
			{
				this.prevEnabled = this.colliderEnabled;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0002B290 File Offset: 0x00029490
		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.coll == null && this.coll2D == null)
			{
				Debug.LogError("Removed collider from DynamicGridObstacle", this);
				base.enabled = false;
				return;
			}
			while (this.pendingGraphUpdates.Count > 0 && this.pendingGraphUpdates.Peek().stage != GraphUpdateStage.Pending)
			{
				this.pendingGraphUpdates.Dequeue();
			}
			if (AstarPath.active == null || AstarPath.active.isScanning || Time.realtimeSinceStartup - this.lastCheckTime < this.checkTime || !Application.isPlaying || this.pendingGraphUpdates.Count > 0)
			{
				return;
			}
			this.lastCheckTime = Time.realtimeSinceStartup;
			if (this.colliderEnabled)
			{
				Bounds bounds = this.bounds;
				Quaternion rotation = this.tr.rotation;
				Vector3 vector = this.prevBounds.min - bounds.min;
				Vector3 vector2 = this.prevBounds.max - bounds.max;
				float num = bounds.extents.magnitude * Quaternion.Angle(this.prevRotation, rotation) * 0.017453292f;
				if (vector.sqrMagnitude > this.updateError * this.updateError || vector2.sqrMagnitude > this.updateError * this.updateError || num > this.updateError || !this.prevEnabled)
				{
					this.DoUpdateGraphs();
					return;
				}
			}
			else if (this.prevEnabled)
			{
				this.DoUpdateGraphs();
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0002B418 File Offset: 0x00029618
		protected override void OnDisable()
		{
			base.OnDisable();
			if (AstarPath.active != null && Application.isPlaying)
			{
				GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.prevBounds);
				this.pendingGraphUpdates.Enqueue(graphUpdateObject);
				AstarPath.active.UpdateGraphs(graphUpdateObject);
				this.prevEnabled = false;
			}
			this.pendingGraphUpdates.Clear();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0002B474 File Offset: 0x00029674
		public void DoUpdateGraphs()
		{
			if (this.coll == null && this.coll2D == null)
			{
				return;
			}
			Physics.SyncTransforms();
			Physics2D.SyncTransforms();
			if (!this.colliderEnabled)
			{
				GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.prevBounds);
				this.pendingGraphUpdates.Enqueue(graphUpdateObject);
				AstarPath.active.UpdateGraphs(graphUpdateObject);
			}
			else
			{
				Bounds bounds = this.bounds;
				Bounds bounds2 = bounds;
				bounds2.Encapsulate(this.prevBounds);
				if (DynamicGridObstacle.BoundsVolume(bounds2) < DynamicGridObstacle.BoundsVolume(bounds) + DynamicGridObstacle.BoundsVolume(this.prevBounds))
				{
					GraphUpdateObject graphUpdateObject2 = new GraphUpdateObject(bounds2);
					this.pendingGraphUpdates.Enqueue(graphUpdateObject2);
					AstarPath.active.UpdateGraphs(graphUpdateObject2);
				}
				else
				{
					GraphUpdateObject graphUpdateObject3 = new GraphUpdateObject(this.prevBounds);
					GraphUpdateObject graphUpdateObject4 = new GraphUpdateObject(bounds);
					this.pendingGraphUpdates.Enqueue(graphUpdateObject3);
					this.pendingGraphUpdates.Enqueue(graphUpdateObject4);
					AstarPath.active.UpdateGraphs(graphUpdateObject3);
					AstarPath.active.UpdateGraphs(graphUpdateObject4);
				}
				this.prevBounds = bounds;
			}
			this.prevEnabled = this.colliderEnabled;
			this.prevRotation = this.tr.rotation;
			this.lastCheckTime = Time.realtimeSinceStartup;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0002B59E File Offset: 0x0002979E
		private static float BoundsVolume(Bounds b)
		{
			return Math.Abs(b.size.x * b.size.y * b.size.z);
		}

		// Token: 0x04000415 RID: 1045
		private Collider coll;

		// Token: 0x04000416 RID: 1046
		private Collider2D coll2D;

		// Token: 0x04000417 RID: 1047
		private Transform tr;

		// Token: 0x04000418 RID: 1048
		public float updateError = 1f;

		// Token: 0x04000419 RID: 1049
		public float checkTime = 0.2f;

		// Token: 0x0400041A RID: 1050
		private Bounds prevBounds;

		// Token: 0x0400041B RID: 1051
		private Quaternion prevRotation;

		// Token: 0x0400041C RID: 1052
		private bool prevEnabled;

		// Token: 0x0400041D RID: 1053
		private float lastCheckTime = -9999f;

		// Token: 0x0400041E RID: 1054
		private Queue<GraphUpdateObject> pendingGraphUpdates = new Queue<GraphUpdateObject>();
	}
}
