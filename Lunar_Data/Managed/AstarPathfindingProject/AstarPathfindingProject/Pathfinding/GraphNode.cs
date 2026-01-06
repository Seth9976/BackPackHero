using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008E RID: 142
	public abstract class GraphNode
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00016EB2 File Offset: 0x000150B2
		public NavGraph Graph
		{
			get
			{
				return AstarData.GetGraph(this);
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00016EBA File Offset: 0x000150BA
		public void Destroy()
		{
			if (this.Destroyed)
			{
				return;
			}
			this.ClearConnections(true);
			if (AstarPath.active != null)
			{
				AstarPath.active.DestroyNode(this);
			}
			this.NodeIndex = 268435454U;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00016EEF File Offset: 0x000150EF
		public bool Destroyed
		{
			get
			{
				return this.NodeIndex == 268435454U;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00016EFE File Offset: 0x000150FE
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00016F0C File Offset: 0x0001510C
		public uint NodeIndex
		{
			get
			{
				return (uint)(this.nodeIndex & 268435455);
			}
			internal set
			{
				this.nodeIndex = (this.nodeIndex & -268435456) | (int)value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00016F22 File Offset: 0x00015122
		internal virtual int PathNodeVariants
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00016F25 File Offset: 0x00015125
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00016F36 File Offset: 0x00015136
		internal bool TemporaryFlag1
		{
			get
			{
				return (this.nodeIndex & 268435456) != 0;
			}
			set
			{
				this.nodeIndex = (this.nodeIndex & -268435457) | (value ? 268435456 : 0);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00016F56 File Offset: 0x00015156
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00016F67 File Offset: 0x00015167
		internal bool TemporaryFlag2
		{
			get
			{
				return (this.nodeIndex & 536870912) != 0;
			}
			set
			{
				this.nodeIndex = (this.nodeIndex & -536870913) | (value ? 536870912 : 0);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00016F87 File Offset: 0x00015187
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00016F8F File Offset: 0x0001518F
		public uint Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00016F98 File Offset: 0x00015198
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00016FA0 File Offset: 0x000151A0
		public uint Penalty
		{
			get
			{
				return this.penalty;
			}
			set
			{
				if (value > 16777215U)
				{
					Debug.LogWarning("Very high penalty applied. Are you sure negative values haven't underflowed?\nPenalty values this high could with long paths cause overflows and in some cases infinity loops because of that.\nPenalty value applied: " + value.ToString());
				}
				this.penalty = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00016FC7 File Offset: 0x000151C7
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00016FD4 File Offset: 0x000151D4
		public bool Walkable
		{
			get
			{
				return (this.flags & 1U) > 0U;
			}
			set
			{
				this.flags = (this.flags & 4294967294U) | (value ? 1U : 0U);
				AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00016FFD File Offset: 0x000151FD
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001700D File Offset: 0x0001520D
		internal int HierarchicalNodeIndex
		{
			[MethodImpl(256)]
			get
			{
				return (int)((this.flags & 262142U) >> 1);
			}
			[MethodImpl(256)]
			set
			{
				this.flags = (this.flags & 4294705153U) | (uint)((uint)value << 1);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00017025 File Offset: 0x00015225
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00017036 File Offset: 0x00015236
		internal bool IsHierarchicalNodeDirty
		{
			[MethodImpl(256)]
			get
			{
				return (this.flags & 262144U) > 0U;
			}
			[MethodImpl(256)]
			set
			{
				this.flags = (this.flags & 4294705151U) | ((value ? 1U : 0U) << 18);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00017055 File Offset: 0x00015255
		public uint Area
		{
			get
			{
				return AstarPath.active.hierarchicalGraph.GetConnectedComponent(this.HierarchicalNodeIndex);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001706C File Offset: 0x0001526C
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0001707D File Offset: 0x0001527D
		public uint GraphIndex
		{
			[MethodImpl(256)]
			get
			{
				return (this.flags & 4278190080U) >> 24;
			}
			set
			{
				this.flags = (this.flags & 16777215U) | (value << 24);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00017096 File Offset: 0x00015296
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000170A7 File Offset: 0x000152A7
		public uint Tag
		{
			get
			{
				return (this.flags & 16252928U) >> 19;
			}
			set
			{
				this.flags = (this.flags & 4278714367U) | ((value << 19) & 16252928U);
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000170C6 File Offset: 0x000152C6
		public void SetConnectivityDirty()
		{
			AstarPath.active.hierarchicalGraph.AddDirtyNode(this);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000170D8 File Offset: 0x000152D8
		public virtual void GetConnections(Action<GraphNode> action, int connectionFilter = 32)
		{
			this.GetConnections<Action<GraphNode>>(delegate(GraphNode node, ref Action<GraphNode> action)
			{
				action(node);
			}, ref action, connectionFilter);
		}

		// Token: 0x0600048C RID: 1164
		public abstract void GetConnections<T>(GraphNode.GetConnectionsWithData<T> action, ref T data, int connectionFilter = 32);

		// Token: 0x0600048D RID: 1165 RVA: 0x00017102 File Offset: 0x00015302
		public static void Connect(GraphNode lhs, GraphNode rhs, uint cost, OffMeshLinks.Directionality directionality = OffMeshLinks.Directionality.TwoWay)
		{
			lhs.AddPartialConnection(rhs, cost, true, directionality == OffMeshLinks.Directionality.TwoWay);
			rhs.AddPartialConnection(lhs, cost, directionality == OffMeshLinks.Directionality.TwoWay, true);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001711E File Offset: 0x0001531E
		public static void Disconnect(GraphNode lhs, GraphNode rhs)
		{
			lhs.RemovePartialConnection(rhs);
			rhs.RemovePartialConnection(lhs);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001712E File Offset: 0x0001532E
		[Obsolete("Use the static Connect method instead, or AddPartialConnection if you really need to")]
		public void AddConnection(GraphNode node, uint cost)
		{
			this.AddPartialConnection(node, cost, true, true);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001713A File Offset: 0x0001533A
		[Obsolete("Use the static Disconnect method instead, or RemovePartialConnection if you really need to")]
		public void RemoveConnection(GraphNode node)
		{
			this.RemovePartialConnection(node);
		}

		// Token: 0x06000491 RID: 1169
		public abstract void AddPartialConnection(GraphNode node, uint cost, bool isOutgoing, bool isIncoming);

		// Token: 0x06000492 RID: 1170
		public abstract void RemovePartialConnection(GraphNode node);

		// Token: 0x06000493 RID: 1171
		public abstract void ClearConnections(bool alsoReverse = true);

		// Token: 0x06000494 RID: 1172 RVA: 0x00017143 File Offset: 0x00015343
		[Obsolete("Use ContainsOutgoingConnection instead")]
		public bool ContainsConnection(GraphNode node)
		{
			return this.ContainsOutgoingConnection(node);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001714C File Offset: 0x0001534C
		public virtual bool ContainsOutgoingConnection(GraphNode node)
		{
			bool flag = false;
			this.GetConnections<bool>(delegate(GraphNode neighbour, ref bool contains)
			{
				contains |= neighbour == node;
			}, ref flag, 32);
			return flag;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00017180 File Offset: 0x00015380
		[Obsolete("Use GetPortal(GraphNode, out Vector3, out Vector3) instead")]
		public bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			Vector3 vector;
			Vector3 vector2;
			if (!backwards && this.GetPortal(other, out vector, out vector2))
			{
				if (left != null)
				{
					left.Add(vector);
					right.Add(vector2);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000171B2 File Offset: 0x000153B2
		public virtual bool GetPortal(GraphNode other, out Vector3 left, out Vector3 right)
		{
			left = Vector3.zero;
			right = Vector3.zero;
			return false;
		}

		// Token: 0x06000498 RID: 1176
		public abstract void Open(Path path, uint pathNodeIndex, uint gScore);

		// Token: 0x06000499 RID: 1177
		public abstract void OpenAtPoint(Path path, uint pathNodeIndex, Int3 position, uint gScore);

		// Token: 0x0600049A RID: 1178 RVA: 0x000171CB File Offset: 0x000153CB
		public virtual Int3 DecodeVariantPosition(uint pathNodeIndex, uint fractionAlongEdge)
		{
			return this.position;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000057A6 File Offset: 0x000039A6
		public virtual float SurfaceArea()
		{
			return 0f;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000171D3 File Offset: 0x000153D3
		public virtual Vector3 RandomPointOnSurface()
		{
			return (Vector3)this.position;
		}

		// Token: 0x0600049D RID: 1181
		public abstract Vector3 ClosestPointOnNode(Vector3 p);

		// Token: 0x0600049E RID: 1182 RVA: 0x000171E0 File Offset: 0x000153E0
		public virtual bool ContainsPoint(Int3 point)
		{
			return this.ContainsPoint((Vector3)point);
		}

		// Token: 0x0600049F RID: 1183
		public abstract bool ContainsPoint(Vector3 point);

		// Token: 0x060004A0 RID: 1184
		public abstract bool ContainsPointInGraphSpace(Int3 point);

		// Token: 0x060004A1 RID: 1185 RVA: 0x000171EE File Offset: 0x000153EE
		public virtual int GetGizmoHashCode()
		{
			return this.position.GetHashCode() ^ (int)(19U * this.Penalty) ^ (int)(41U * (this.flags & 4294443009U));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001721B File Offset: 0x0001541B
		public virtual void SerializeNode(GraphSerializationContext ctx)
		{
			ctx.writer.Write(this.Penalty);
			ctx.writer.Write(this.Flags & 4294443009U);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00017248 File Offset: 0x00015448
		public virtual void DeserializeNode(GraphSerializationContext ctx)
		{
			this.Penalty = ctx.reader.ReadUInt32();
			this.Flags = (ctx.reader.ReadUInt32() & 4294443009U) | (this.Flags & 524286U);
			this.GraphIndex = ctx.graphIndex;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void SerializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void DeserializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x04000301 RID: 769
		private int nodeIndex;

		// Token: 0x04000302 RID: 770
		protected uint flags;

		// Token: 0x04000303 RID: 771
		private uint penalty;

		// Token: 0x04000304 RID: 772
		private const uint NodeIndexMask = 268435455U;

		// Token: 0x04000305 RID: 773
		public const uint DestroyedNodeIndex = 268435454U;

		// Token: 0x04000306 RID: 774
		public const int InvalidNodeIndex = 0;

		// Token: 0x04000307 RID: 775
		private const int TemporaryFlag1Mask = 268435456;

		// Token: 0x04000308 RID: 776
		private const int TemporaryFlag2Mask = 536870912;

		// Token: 0x04000309 RID: 777
		public Int3 position;

		// Token: 0x0400030A RID: 778
		private const int FlagsWalkableOffset = 0;

		// Token: 0x0400030B RID: 779
		private const uint FlagsWalkableMask = 1U;

		// Token: 0x0400030C RID: 780
		private const int FlagsHierarchicalIndexOffset = 1;

		// Token: 0x0400030D RID: 781
		private const uint HierarchicalIndexMask = 262142U;

		// Token: 0x0400030E RID: 782
		private const int HierarchicalDirtyOffset = 18;

		// Token: 0x0400030F RID: 783
		private const uint HierarchicalDirtyMask = 262144U;

		// Token: 0x04000310 RID: 784
		private const int FlagsGraphOffset = 24;

		// Token: 0x04000311 RID: 785
		private const uint FlagsGraphMask = 4278190080U;

		// Token: 0x04000312 RID: 786
		public const uint MaxHierarchicalNodeIndex = 131071U;

		// Token: 0x04000313 RID: 787
		public const uint MaxGraphIndex = 254U;

		// Token: 0x04000314 RID: 788
		public const uint InvalidGraphIndex = 255U;

		// Token: 0x04000315 RID: 789
		private const int FlagsTagOffset = 19;

		// Token: 0x04000316 RID: 790
		public const int MaxTagIndex = 31;

		// Token: 0x04000317 RID: 791
		private const uint FlagsTagMask = 16252928U;

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x060004A8 RID: 1192
		public delegate void GetConnectionsWithData<T>(GraphNode node, ref T data);
	}
}
