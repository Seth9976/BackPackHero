using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200013D RID: 317
	[AddComponentMenu("Pathfinding/Dynamic Grid Obstacle")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/dynamicgridobstacle.html")]
	public class DynamicGridObstacle : GraphModifier
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00033B30 File Offset: 0x00031D30
		private Bounds bounds
		{
			get
			{
				if (this.coll != null)
				{
					return this.coll.bounds;
				}
				if (this.coll2D != null)
				{
					Bounds bounds = this.coll2D.bounds;
					bounds.extents += new Vector3(0f, 0f, 10000f);
					return bounds;
				}
				return default(Bounds);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00033BA2 File Offset: 0x00031DA2
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

		// Token: 0x06000975 RID: 2421 RVA: 0x00033BCC File Offset: 0x00031DCC
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

		// Token: 0x06000976 RID: 2422 RVA: 0x00033C6A File Offset: 0x00031E6A
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

		// Token: 0x06000977 RID: 2423 RVA: 0x00033C9C File Offset: 0x00031E9C
		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.coll == null && this.coll2D == null)
			{
				Debug.LogError("No collider attached to this GameObject. The DynamicGridObstacle component requires a collider.", this);
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

		// Token: 0x06000978 RID: 2424 RVA: 0x00033E24 File Offset: 0x00032024
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

		// Token: 0x06000979 RID: 2425 RVA: 0x00033E80 File Offset: 0x00032080
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

		// Token: 0x0600097A RID: 2426 RVA: 0x00033FAA File Offset: 0x000321AA
		private static float BoundsVolume(Bounds b)
		{
			return Math.Abs(b.size.x * b.size.y * b.size.z);
		}

		// Token: 0x0400067B RID: 1659
		private Collider coll;

		// Token: 0x0400067C RID: 1660
		private Collider2D coll2D;

		// Token: 0x0400067D RID: 1661
		private Transform tr;

		// Token: 0x0400067E RID: 1662
		public float updateError = 1f;

		// Token: 0x0400067F RID: 1663
		public float checkTime = 0.2f;

		// Token: 0x04000680 RID: 1664
		private Bounds prevBounds;

		// Token: 0x04000681 RID: 1665
		private Quaternion prevRotation;

		// Token: 0x04000682 RID: 1666
		private bool prevEnabled;

		// Token: 0x04000683 RID: 1667
		private float lastCheckTime = -9999f;

		// Token: 0x04000684 RID: 1668
		private Queue<GraphUpdateObject> pendingGraphUpdates = new Queue<GraphUpdateObject>();
	}
}
