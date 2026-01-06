using System;
using System.Runtime.CompilerServices;

namespace Pathfinding
{
	// Token: 0x0200008D RID: 141
	public struct Connection
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00016D8F File Offset: 0x00014F8F
		public int shapeEdge
		{
			get
			{
				return (int)(this.shapeEdgeInfo & 3);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00016D99 File Offset: 0x00014F99
		public int adjacentShapeEdge
		{
			get
			{
				return (this.shapeEdgeInfo >> 2) & 3;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00016DA5 File Offset: 0x00014FA5
		public bool edgesAreIdentical
		{
			get
			{
				return (this.shapeEdgeInfo & 64) > 0;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00016DB3 File Offset: 0x00014FB3
		public bool isEdgeShared
		{
			get
			{
				return (this.shapeEdgeInfo & 15) != 15;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00016DC5 File Offset: 0x00014FC5
		public bool isOutgoing
		{
			get
			{
				return (this.shapeEdgeInfo & 32) > 0;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00016DD3 File Offset: 0x00014FD3
		public bool isIncoming
		{
			get
			{
				return (this.shapeEdgeInfo & 16) > 0;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00016DE1 File Offset: 0x00014FE1
		[MethodImpl(256)]
		public Connection(GraphNode node, uint cost, bool isOutgoing, bool isIncoming)
		{
			this.node = node;
			this.cost = cost;
			this.shapeEdgeInfo = Connection.PackShapeEdgeInfo(isOutgoing, isIncoming);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00016DFF File Offset: 0x00014FFF
		[MethodImpl(256)]
		public static byte PackShapeEdgeInfo(bool isOutgoing, bool isIncoming)
		{
			return (byte)(15 | (isIncoming ? 16 : 0) | (isOutgoing ? 32 : 0));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00016E16 File Offset: 0x00015016
		[MethodImpl(256)]
		public static byte PackShapeEdgeInfo(byte shapeEdge, byte adjacentShapeEdge, bool areEdgesIdentical, bool isOutgoing, bool isIncoming)
		{
			return (byte)((int)shapeEdge | ((int)adjacentShapeEdge << 2) | (areEdgesIdentical ? 64 : 0) | (isOutgoing ? 32 : 0) | (isIncoming ? 16 : 0));
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00016E3A File Offset: 0x0001503A
		[MethodImpl(256)]
		public Connection(GraphNode node, uint cost, byte shapeEdgeInfo)
		{
			this.node = node;
			this.cost = cost;
			this.shapeEdgeInfo = shapeEdgeInfo;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00016E51 File Offset: 0x00015051
		public override int GetHashCode()
		{
			return this.node.GetHashCode() ^ (int)this.cost;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00016E68 File Offset: 0x00015068
		public override bool Equals(object obj)
		{
			if (!(obj is Connection))
			{
				return false;
			}
			Connection connection = (Connection)obj;
			return connection.node == this.node && connection.cost == this.cost && connection.shapeEdgeInfo == this.shapeEdgeInfo;
		}

		// Token: 0x040002FA RID: 762
		public GraphNode node;

		// Token: 0x040002FB RID: 763
		public uint cost;

		// Token: 0x040002FC RID: 764
		public byte shapeEdgeInfo;

		// Token: 0x040002FD RID: 765
		public const byte NoSharedEdge = 15;

		// Token: 0x040002FE RID: 766
		public const byte IncomingConnection = 16;

		// Token: 0x040002FF RID: 767
		public const byte OutgoingConnection = 32;

		// Token: 0x04000300 RID: 768
		public const byte IdenticalEdge = 64;
	}
}
