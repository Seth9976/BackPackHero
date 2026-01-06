using System;
using Pathfinding.Drawing;
using Pathfinding.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200005D RID: 93
	[AddComponentMenu("Pathfinding/GraphUpdateScene")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/graphupdatescene.html")]
	public class GraphUpdateScene : GraphModifier
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000FAC9 File Offset: 0x0000DCC9
		public void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (!this.firstApplied && this.applyOnStart)
			{
				this.Apply();
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
		public override void OnPostScan()
		{
			if (this.applyOnScan)
			{
				this.Apply();
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		public virtual void InvertSettings()
		{
			this.setWalkability = !this.setWalkability;
			this.penaltyDelta = -this.penaltyDelta;
			if (this.setTagInvert == 0U)
			{
				this.setTagInvert = this.setTag;
				this.setTag = 0U;
				return;
			}
			this.setTag = this.setTagInvert;
			this.setTagInvert = 0U;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FB63 File Offset: 0x0000DD63
		public void RecalcConvex()
		{
			this.convexPoints = (this.convex ? Polygon.ConvexHullXZ(this.points) : null);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FB84 File Offset: 0x0000DD84
		public Bounds GetBounds()
		{
			if (this.points == null || this.points.Length == 0)
			{
				Collider component = base.GetComponent<Collider>();
				Collider2D component2 = base.GetComponent<Collider2D>();
				Renderer component3 = base.GetComponent<Renderer>();
				Bounds bounds;
				if (component != null)
				{
					bounds = component.bounds;
				}
				else if (component2 != null)
				{
					bounds = component2.bounds;
					bounds.size = new Vector3(bounds.size.x, bounds.size.y, Mathf.Max(bounds.size.z, 1f));
				}
				else
				{
					if (!(component3 != null))
					{
						return new Bounds(Vector3.zero, Vector3.zero);
					}
					bounds = component3.bounds;
				}
				if (this.legacyMode && bounds.size.y < this.minBoundsHeight)
				{
					bounds.size = new Vector3(bounds.size.x, this.minBoundsHeight, bounds.size.z);
				}
				return bounds;
			}
			if (this.convexPoints == null)
			{
				this.RecalcConvex();
			}
			return GraphUpdateShape.GetBounds(this.convex ? this.convexPoints : this.points, (this.legacyMode && this.legacyUseWorldSpace) ? Matrix4x4.identity : base.transform.localToWorldMatrix, this.minBoundsHeight);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public virtual GraphUpdateObject GetGraphUpdate()
		{
			GraphUpdateObject graphUpdateObject;
			if (this.points == null || this.points.Length == 0)
			{
				PolygonCollider2D component = base.GetComponent<PolygonCollider2D>();
				if (component != null)
				{
					Vector2[] array = component.points;
					Vector3[] array2 = new Vector3[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						Vector2 vector = array[i] + component.offset;
						array2[i] = new Vector3(vector.x, 0f, vector.y);
					}
					Matrix4x4 matrix4x = base.transform.localToWorldMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), Vector3.one);
					GraphUpdateShape graphUpdateShape = new GraphUpdateShape(array2, this.convex, matrix4x, this.minBoundsHeight);
					graphUpdateObject = new GraphUpdateObject(this.GetBounds());
					graphUpdateObject.shape = graphUpdateShape;
				}
				else
				{
					Bounds bounds = this.GetBounds();
					if (bounds.center == Vector3.zero && bounds.size == Vector3.zero)
					{
						Debug.LogError("Cannot apply GraphUpdateScene, no points defined and no renderer or collider attached", this);
						return null;
					}
					if (bounds.size == Vector3.zero)
					{
						Debug.LogWarning("Collider bounding box was empty. Are you trying to apply the GraphUpdateScene before the collider has been enabled or initialized?", this);
					}
					graphUpdateObject = new GraphUpdateObject(bounds);
				}
			}
			else
			{
				GraphUpdateShape graphUpdateShape2;
				if (this.legacyMode && !this.legacyUseWorldSpace)
				{
					Vector3[] array3 = new Vector3[this.points.Length];
					for (int j = 0; j < this.points.Length; j++)
					{
						array3[j] = base.transform.TransformPoint(this.points[j]);
					}
					graphUpdateShape2 = new GraphUpdateShape(array3, this.convex, Matrix4x4.identity, this.minBoundsHeight);
				}
				else
				{
					graphUpdateShape2 = new GraphUpdateShape(this.points, this.convex, (this.legacyMode && this.legacyUseWorldSpace) ? Matrix4x4.identity : base.transform.localToWorldMatrix, this.minBoundsHeight);
				}
				graphUpdateObject = new GraphUpdateObject(graphUpdateShape2.GetBounds());
				graphUpdateObject.shape = graphUpdateShape2;
			}
			this.firstApplied = true;
			graphUpdateObject.modifyWalkability = this.modifyWalkability;
			graphUpdateObject.setWalkability = this.setWalkability;
			graphUpdateObject.addPenalty = this.penaltyDelta;
			graphUpdateObject.updatePhysics = this.updatePhysics;
			graphUpdateObject.updateErosion = this.updateErosion;
			graphUpdateObject.resetPenaltyOnPhysics = this.resetPenaltyOnPhysics;
			graphUpdateObject.modifyTag = this.modifyTag;
			graphUpdateObject.setTag = this.setTag;
			return graphUpdateObject;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000FF54 File Offset: 0x0000E154
		public void Apply()
		{
			if (AstarPath.active == null)
			{
				Debug.LogError("There is no AstarPath object in the scene", this);
				return;
			}
			GraphUpdateObject graphUpdate = this.GetGraphUpdate();
			if (graphUpdate != null)
			{
				AstarPath.active.UpdateGraphs(graphUpdate);
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000FF90 File Offset: 0x0000E190
		public override void DrawGizmos()
		{
			bool flag = GizmoContext.InActiveSelection(this);
			Color color = (flag ? new Color(0.8901961f, 0.23921569f, 0.08627451f, 1f) : new Color(0.8901961f, 0.23921569f, 0.08627451f, 0.9f));
			if (flag)
			{
				Color color2 = Color.Lerp(color, new Color(1f, 1f, 1f, 0.2f), 0.9f);
				Bounds bounds = this.GetBounds();
				Draw.SolidBox(bounds.center, bounds.size, color2);
				Draw.WireBox(bounds.center, bounds.size, color2);
			}
			if (this.points == null)
			{
				return;
			}
			if (this.convex)
			{
				color.a *= 0.5f;
			}
			Matrix4x4 matrix4x = ((this.legacyMode && this.legacyUseWorldSpace) ? Matrix4x4.identity : base.transform.localToWorldMatrix);
			if (this.convex)
			{
				color.r -= 0.1f;
				color.g -= 0.2f;
				color.b -= 0.1f;
			}
			using (Draw.WithMatrix(matrix4x))
			{
				if (flag || !this.convex)
				{
					Draw.Polyline(this.points, true, color);
				}
				if (this.convex)
				{
					if (this.convexPoints == null)
					{
						this.RecalcConvex();
					}
					Draw.Polyline(this.convexPoints, true, flag ? new Color(0.8901961f, 0.23921569f, 0.08627451f, 1f) : new Color(0.8901961f, 0.23921569f, 0.08627451f, 0.9f));
				}
				Vector3[] array = (this.convex ? this.convexPoints : this.points);
				if (flag && array != null && array.Length != 0)
				{
					float num = array[0].y;
					float num2 = array[0].y;
					for (int i = 0; i < array.Length; i++)
					{
						num = Mathf.Min(num, array[i].y);
						num2 = Mathf.Max(num2, array[i].y);
					}
					float num3 = Mathf.Max(this.minBoundsHeight - (num2 - num), 0f) * 0.5f;
					num -= num3;
					num2 += num3;
					using (Draw.WithColor(new Color(1f, 1f, 1f, 0.2f)))
					{
						for (int j = 0; j < array.Length; j++)
						{
							int num4 = (j + 1) % array.Length;
							Vector3 vector = array[j] + Vector3.up * (num - array[j].y);
							Vector3 vector2 = array[j] + Vector3.up * (num2 - array[j].y);
							Vector3 vector3 = array[num4] + Vector3.up * (num - array[num4].y);
							Vector3 vector4 = array[num4] + Vector3.up * (num2 - array[num4].y);
							Draw.Line(vector, vector2);
							Draw.Line(vector, vector3);
							Draw.Line(vector2, vector4);
						}
					}
				}
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001034C File Offset: 0x0000E54C
		public void DisableLegacyMode()
		{
			if (this.legacyMode)
			{
				this.legacyMode = false;
				if (this.legacyUseWorldSpace)
				{
					this.legacyUseWorldSpace = false;
					for (int i = 0; i < this.points.Length; i++)
					{
						this.points[i] = base.transform.InverseTransformPoint(this.points[i]);
					}
					this.RecalcConvex();
				}
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000103B4 File Offset: 0x0000E5B4
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			int num;
			if (migrations.TryMigrateFromLegacyFormat(out num))
			{
				if (num == 0 && this.points != null && this.points.Length != 0)
				{
					this.legacyMode = true;
				}
				if (this.setTagCompatibility != -1)
				{
					this.setTag = (uint)this.setTagCompatibility;
					this.setTagCompatibility = -1;
				}
			}
		}

		// Token: 0x040001F2 RID: 498
		public Vector3[] points;

		// Token: 0x040001F3 RID: 499
		private Vector3[] convexPoints;

		// Token: 0x040001F4 RID: 500
		public bool convex = true;

		// Token: 0x040001F5 RID: 501
		public float minBoundsHeight = 1f;

		// Token: 0x040001F6 RID: 502
		public int penaltyDelta;

		// Token: 0x040001F7 RID: 503
		public bool modifyWalkability;

		// Token: 0x040001F8 RID: 504
		public bool setWalkability;

		// Token: 0x040001F9 RID: 505
		public bool applyOnStart = true;

		// Token: 0x040001FA RID: 506
		public bool applyOnScan = true;

		// Token: 0x040001FB RID: 507
		public bool updatePhysics;

		// Token: 0x040001FC RID: 508
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x040001FD RID: 509
		public bool updateErosion = true;

		// Token: 0x040001FE RID: 510
		public bool modifyTag;

		// Token: 0x040001FF RID: 511
		public PathfindingTag setTag;

		// Token: 0x04000200 RID: 512
		[HideInInspector]
		public bool legacyMode;

		// Token: 0x04000201 RID: 513
		private PathfindingTag setTagInvert;

		// Token: 0x04000202 RID: 514
		private bool firstApplied;

		// Token: 0x04000203 RID: 515
		[SerializeField]
		[FormerlySerializedAs("useWorldSpace")]
		private bool legacyUseWorldSpace;

		// Token: 0x04000204 RID: 516
		[SerializeField]
		[FormerlySerializedAs("setTag")]
		private int setTagCompatibility = -1;
	}
}
