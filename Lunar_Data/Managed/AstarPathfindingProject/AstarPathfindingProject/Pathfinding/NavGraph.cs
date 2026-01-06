using System;
using Pathfinding.Drawing;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000CD RID: 205
	public abstract class NavGraph : IGraphInternals
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00021E4E File Offset: 0x0002004E
		internal bool exists
		{
			get
			{
				return this.active != null;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600067A RID: 1658
		public abstract bool isScanned { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00016F22 File Offset: 0x00015122
		public virtual bool persistent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00016F22 File Offset: 0x00015122
		public virtual bool showInInspector
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00021E5C File Offset: 0x0002005C
		public virtual Bounds bounds
		{
			get
			{
				return new Bounds(Vector3.zero, Vector3.positiveInfinity);
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00021E70 File Offset: 0x00020070
		public virtual int CountNodes()
		{
			int count = 0;
			this.GetNodes(delegate(GraphNode _)
			{
				int count2 = count;
				count = count2 + 1;
			});
			return count;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00021EA4 File Offset: 0x000200A4
		public void GetNodes(Func<GraphNode, bool> action)
		{
			bool cont = true;
			this.GetNodes(delegate(GraphNode node)
			{
				if (cont)
				{
					cont &= action(node);
				}
			});
		}

		// Token: 0x06000680 RID: 1664
		public abstract void GetNodes(Action<GraphNode> action);

		// Token: 0x06000681 RID: 1665 RVA: 0x00016F22 File Offset: 0x00015122
		public virtual bool IsInsideBounds(Vector3 point)
		{
			return true;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00021ED7 File Offset: 0x000200D7
		protected void AssertSafeToUpdateGraph()
		{
			if (!this.active.IsAnyWorkItemInProgress && !this.active.isScanning)
			{
				throw new Exception("Trying to update graphs when it is not safe to do so. Graph updates must be done inside a work item or when a graph is being scanned. See AstarPath.AddWorkItem");
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00021EFE File Offset: 0x000200FE
		protected void DirtyBounds(Bounds bounds)
		{
			this.active.DirtyBounds(bounds);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00021F0C File Offset: 0x0002010C
		public virtual void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.position = (Int3)deltaMatrix.MultiplyPoint((Vector3)node.position);
			});
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000057A6 File Offset: 0x000039A6
		public virtual float NearestNodeDistanceSqrLowerBound(Vector3 position, NNConstraint constraint = null)
		{
			return 0f;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00021F38 File Offset: 0x00020138
		public NNInfo GetNearest(Vector3 position, NNConstraint constraint = null)
		{
			float num = ((constraint == null || constraint.constrainDistance) ? this.active.maxNearestNodeDistanceSqr : float.PositiveInfinity);
			return this.GetNearest(position, constraint, num);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00021F6C File Offset: 0x0002016C
		public virtual NNInfo GetNearest(Vector3 position, NNConstraint constraint, float maxDistanceSqr)
		{
			GraphNode minNode = null;
			this.GetNodes(delegate(GraphNode node)
			{
				float sqrMagnitude = (position - (Vector3)node.position).sqrMagnitude;
				if (sqrMagnitude < maxDistanceSqr && (constraint == null || constraint.Suitable(node)))
				{
					maxDistanceSqr = sqrMagnitude;
					minNode = node;
				}
			});
			if (minNode == null)
			{
				return NNInfo.Empty;
			}
			return new NNInfo(minNode, (Vector3)minNode.position, maxDistanceSqr);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00021FDC File Offset: 0x000201DC
		[Obsolete("Use GetNearest instead")]
		public NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00021FE6 File Offset: 0x000201E6
		protected virtual void OnDestroy()
		{
			this.DestroyAllNodes();
			this.DisposeUnmanagedData();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000033F6 File Offset: 0x000015F6
		protected virtual void DisposeUnmanagedData()
		{
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00021FF4 File Offset: 0x000201F4
		protected virtual void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.Destroy();
			});
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000146B9 File Offset: 0x000128B9
		public virtual IGraphSnapshot Snapshot(Bounds bounds)
		{
			return null;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002201B File Offset: 0x0002021B
		public void Scan()
		{
			this.active.Scan(this);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00022029 File Offset: 0x00020229
		protected virtual IGraphUpdatePromise ScanInternal()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00022030 File Offset: 0x00020230
		protected virtual IGraphUpdatePromise ScanInternal(bool async)
		{
			return this.ScanInternal();
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000033F6 File Offset: 0x000015F6
		protected virtual void SerializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000033F6 File Offset: 0x000015F6
		protected virtual void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000033F6 File Offset: 0x000015F6
		protected virtual void PostDeserialization(GraphSerializationContext ctx)
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00022038 File Offset: 0x00020238
		public virtual void OnDrawGizmos(DrawingData gizmos, bool drawNodes, RedrawScope redrawScope)
		{
			if (!drawNodes)
			{
				return;
			}
			NodeHasher hasher = new NodeHasher(this.active);
			this.GetNodes(delegate(GraphNode node)
			{
				hasher.HashNode(node);
			});
			if (!gizmos.Draw(hasher, redrawScope))
			{
				using (GraphGizmoHelper gizmoHelper = GraphGizmoHelper.GetGizmoHelper(gizmos, this.active, hasher, redrawScope))
				{
					this.GetNodes(new Action<GraphNode>(gizmoHelper.DrawConnections));
				}
			}
			if (this.active.showUnwalkableNodes)
			{
				this.DrawUnwalkableNodes(gizmos, this.active.unwalkableNodeDebugSize, redrawScope);
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000220F0 File Offset: 0x000202F0
		protected void DrawUnwalkableNodes(DrawingData gizmos, float size, RedrawScope redrawScope)
		{
			DrawingData.Hasher hasher = DrawingData.Hasher.Create<NavGraph>(this);
			this.GetNodes(delegate(GraphNode node)
			{
				hasher.Add<bool>(node.Walkable);
				if (!node.Walkable)
				{
					hasher.Add<Int3>(node.position);
				}
			});
			if (!gizmos.Draw(hasher, redrawScope))
			{
				using (CommandBuilder builder = gizmos.GetBuilder(hasher, default(RedrawScope), false))
				{
					using (builder.WithColor(AstarColor.UnwalkableNode))
					{
						this.GetNodes(delegate(GraphNode node)
						{
							if (!node.Walkable)
							{
								builder.SolidBox((Vector3)node.position, new float3(size, size, size));
							}
						});
					}
				}
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x000221C8 File Offset: 0x000203C8
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x000221D0 File Offset: 0x000203D0
		string IGraphInternals.SerializedEditorSettings
		{
			get
			{
				return this.serializedEditorSettings;
			}
			set
			{
				this.serializedEditorSettings = value;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000221D9 File Offset: 0x000203D9
		void IGraphInternals.OnDestroy()
		{
			this.OnDestroy();
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000221E1 File Offset: 0x000203E1
		void IGraphInternals.DisposeUnmanagedData()
		{
			this.DisposeUnmanagedData();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000221E9 File Offset: 0x000203E9
		void IGraphInternals.DestroyAllNodes()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000221F1 File Offset: 0x000203F1
		IGraphUpdatePromise IGraphInternals.ScanInternal(bool async)
		{
			return this.ScanInternal(async);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000221FA File Offset: 0x000203FA
		void IGraphInternals.SerializeExtraInfo(GraphSerializationContext ctx)
		{
			this.SerializeExtraInfo(ctx);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00022203 File Offset: 0x00020403
		void IGraphInternals.DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			this.DeserializeExtraInfo(ctx);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0002220C File Offset: 0x0002040C
		void IGraphInternals.PostDeserialization(GraphSerializationContext ctx)
		{
			this.PostDeserialization(ctx);
		}

		// Token: 0x0400045E RID: 1118
		public AstarPath active;

		// Token: 0x0400045F RID: 1119
		[JsonMember]
		public Guid guid;

		// Token: 0x04000460 RID: 1120
		[JsonMember]
		public uint initialPenalty;

		// Token: 0x04000461 RID: 1121
		[JsonMember]
		public bool open;

		// Token: 0x04000462 RID: 1122
		public uint graphIndex;

		// Token: 0x04000463 RID: 1123
		[JsonMember]
		public string name;

		// Token: 0x04000464 RID: 1124
		[JsonMember]
		public bool drawGizmos = true;

		// Token: 0x04000465 RID: 1125
		[JsonMember]
		public bool infoScreenOpen;

		// Token: 0x04000466 RID: 1126
		[JsonMember]
		private string serializedEditorSettings;
	}
}
