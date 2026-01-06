using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000056 RID: 86
	public abstract class NavGraph : IGraphInternals
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014C4D File Offset: 0x00012E4D
		internal bool exists
		{
			get
			{
				return this.active != null;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00014C5C File Offset: 0x00012E5C
		public virtual int CountNodes()
		{
			int count = 0;
			this.GetNodes(delegate(GraphNode node)
			{
				int count2 = count;
				count = count2 + 1;
			});
			return count;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00014C90 File Offset: 0x00012E90
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

		// Token: 0x0600042A RID: 1066
		public abstract void GetNodes(Action<GraphNode> action);

		// Token: 0x0600042B RID: 1067 RVA: 0x00014CC3 File Offset: 0x00012EC3
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public void SetMatrix(Matrix4x4 m)
		{
			this.matrix = m;
			this.inverseMatrix = m.inverse;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00014CD9 File Offset: 0x00012ED9
		[Obsolete("Use RelocateNodes(Matrix4x4) instead. To keep the same behavior you can call RelocateNodes(newMatrix * oldMatrix.inverse).")]
		public void RelocateNodes(Matrix4x4 oldMatrix, Matrix4x4 newMatrix)
		{
			this.RelocateNodes(newMatrix * oldMatrix.inverse);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00014CEE File Offset: 0x00012EEE
		protected void AssertSafeToUpdateGraph()
		{
			if (!this.active.IsAnyWorkItemInProgress && !this.active.isScanning)
			{
				throw new Exception("Trying to update graphs when it is not safe to do so. Graph updates must be done inside a work item or when a graph is being scanned. See AstarPath.AddWorkItem");
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00014D18 File Offset: 0x00012F18
		public virtual void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.position = (Int3)deltaMatrix.MultiplyPoint((Vector3)node.position);
			});
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00014D44 File Offset: 0x00012F44
		public NNInfoInternal GetNearest(Vector3 position)
		{
			return this.GetNearest(position, NNConstraint.None);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00014D52 File Offset: 0x00012F52
		public NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint, null);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00014D60 File Offset: 0x00012F60
		public virtual NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			float maxDistSqr = ((constraint == null || constraint.constrainDistance) ? AstarPath.active.maxNearestNodeDistanceSqr : float.PositiveInfinity);
			float minDist = float.PositiveInfinity;
			GraphNode minNode = null;
			float minConstDist = float.PositiveInfinity;
			GraphNode minConstNode = null;
			this.GetNodes(delegate(GraphNode node)
			{
				float sqrMagnitude = (position - (Vector3)node.position).sqrMagnitude;
				if (sqrMagnitude < minDist)
				{
					minDist = sqrMagnitude;
					minNode = node;
				}
				if (sqrMagnitude < minConstDist && sqrMagnitude < maxDistSqr && (constraint == null || constraint.Suitable(node)))
				{
					minConstDist = sqrMagnitude;
					minConstNode = node;
				}
			});
			NNInfoInternal nninfoInternal = new NNInfoInternal(minNode);
			nninfoInternal.constrainedNode = minConstNode;
			if (minConstNode != null)
			{
				nninfoInternal.constClampedPosition = (Vector3)minConstNode.position;
			}
			else if (minNode != null)
			{
				nninfoInternal.constrainedNode = minNode;
				nninfoInternal.constClampedPosition = (Vector3)minNode.position;
			}
			return nninfoInternal;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00014E4B File Offset: 0x0001304B
		public virtual NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00014E55 File Offset: 0x00013055
		protected virtual void OnDestroy()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014E5D File Offset: 0x0001305D
		protected virtual void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.Destroy();
			});
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014E84 File Offset: 0x00013084
		[Obsolete("Use AstarPath.Scan instead")]
		public void ScanGraph()
		{
			this.Scan();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014E8C File Offset: 0x0001308C
		public void Scan()
		{
			this.active.Scan(this);
		}

		// Token: 0x06000437 RID: 1079
		protected abstract IEnumerable<Progress> ScanInternal();

		// Token: 0x06000438 RID: 1080 RVA: 0x00014E9A File Offset: 0x0001309A
		protected virtual void SerializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00014E9C File Offset: 0x0001309C
		protected virtual void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00014E9E File Offset: 0x0001309E
		protected virtual void PostDeserialization(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014EA0 File Offset: 0x000130A0
		protected virtual void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.guid = new Guid(ctx.reader.ReadBytes(16));
			this.initialPenalty = ctx.reader.ReadUInt32();
			this.open = ctx.reader.ReadBoolean();
			this.name = ctx.reader.ReadString();
			this.drawGizmos = ctx.reader.ReadBoolean();
			this.infoScreenOpen = ctx.reader.ReadBoolean();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014F1C File Offset: 0x0001311C
		public virtual void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
			this.GetNodes(delegate(GraphNode node)
			{
				hasher.HashNode(node);
			});
			if (!gizmos.Draw(hasher))
			{
				using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
				{
					this.GetNodes(new Action<GraphNode>(gizmoHelper.DrawConnections));
				}
			}
			if (this.active.showUnwalkableNodes)
			{
				this.DrawUnwalkableNodes(this.active.unwalkableNodeDebugSize);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014FC4 File Offset: 0x000131C4
		protected void DrawUnwalkableNodes(float size)
		{
			Gizmos.color = AstarColor.UnwalkableNode;
			this.GetNodes(delegate(GraphNode node)
			{
				if (!node.Walkable)
				{
					Gizmos.DrawCube((Vector3)node.position, Vector3.one * size);
				}
			});
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00014FFA File Offset: 0x000131FA
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00015002 File Offset: 0x00013202
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

		// Token: 0x06000440 RID: 1088 RVA: 0x0001500B File Offset: 0x0001320B
		void IGraphInternals.OnDestroy()
		{
			this.OnDestroy();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00015013 File Offset: 0x00013213
		void IGraphInternals.DestroyAllNodes()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001501B File Offset: 0x0001321B
		IEnumerable<Progress> IGraphInternals.ScanInternal()
		{
			return this.ScanInternal();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00015023 File Offset: 0x00013223
		void IGraphInternals.SerializeExtraInfo(GraphSerializationContext ctx)
		{
			this.SerializeExtraInfo(ctx);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001502C File Offset: 0x0001322C
		void IGraphInternals.DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			this.DeserializeExtraInfo(ctx);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00015035 File Offset: 0x00013235
		void IGraphInternals.PostDeserialization(GraphSerializationContext ctx)
		{
			this.PostDeserialization(ctx);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001503E File Offset: 0x0001323E
		void IGraphInternals.DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.DeserializeSettingsCompatibility(ctx);
		}

		// Token: 0x0400027B RID: 635
		public AstarPath active;

		// Token: 0x0400027C RID: 636
		[JsonMember]
		public Guid guid;

		// Token: 0x0400027D RID: 637
		[JsonMember]
		public uint initialPenalty;

		// Token: 0x0400027E RID: 638
		[JsonMember]
		public bool open;

		// Token: 0x0400027F RID: 639
		public uint graphIndex;

		// Token: 0x04000280 RID: 640
		[JsonMember]
		public string name;

		// Token: 0x04000281 RID: 641
		[JsonMember]
		public bool drawGizmos = true;

		// Token: 0x04000282 RID: 642
		[JsonMember]
		public bool infoScreenOpen;

		// Token: 0x04000283 RID: 643
		[JsonMember]
		private string serializedEditorSettings;

		// Token: 0x04000284 RID: 644
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 matrix = Matrix4x4.identity;

		// Token: 0x04000285 RID: 645
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 inverseMatrix = Matrix4x4.identity;
	}
}
