using System;
using System.IO;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000228 RID: 552
	public class GraphSerializationContext
	{
		// Token: 0x06000CF8 RID: 3320 RVA: 0x000524A8 File Offset: 0x000506A8
		public GraphSerializationContext(BinaryReader reader, GraphNode[] id2NodeMapping, uint graphIndex, GraphMeta meta)
		{
			this.reader = reader;
			this.id2NodeMapping = id2NodeMapping;
			this.graphIndex = graphIndex;
			this.meta = meta;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000524CD File Offset: 0x000506CD
		public GraphSerializationContext(BinaryWriter writer, bool[] persistentGraphs)
		{
			this.writer = writer;
			this.persistentGraphs = persistentGraphs;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000524E3 File Offset: 0x000506E3
		public void SerializeNodeReference(GraphNode node)
		{
			this.writer.Write((int)((node == null) ? uint.MaxValue : node.NodeIndex));
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000524FC File Offset: 0x000506FC
		public void SerializeConnections(Connection[] connections, bool serializeMetadata)
		{
			if (connections == null)
			{
				this.writer.Write(-1);
				return;
			}
			int num = 0;
			for (int i = 0; i < connections.Length; i++)
			{
				num += (this.persistentGraphs[(int)connections[i].node.GraphIndex] ? 1 : 0);
			}
			this.writer.Write(num);
			for (int j = 0; j < connections.Length; j++)
			{
				if (this.persistentGraphs[(int)connections[j].node.GraphIndex])
				{
					this.SerializeNodeReference(connections[j].node);
					this.writer.Write(connections[j].cost);
					if (serializeMetadata)
					{
						this.writer.Write(connections[j].shapeEdgeInfo);
					}
				}
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000525C4 File Offset: 0x000507C4
		public Connection[] DeserializeConnections(bool deserializeMetadata)
		{
			int num = this.reader.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num);
			for (int i = 0; i < num; i++)
			{
				GraphNode graphNode = this.DeserializeNodeReference();
				uint num2 = this.reader.ReadUInt32();
				if (deserializeMetadata)
				{
					byte b = 15;
					if (!(this.meta.version < AstarSerializer.V4_1_0))
					{
						if (this.meta.version < AstarSerializer.V4_3_68)
						{
							this.reader.ReadByte();
						}
						else
						{
							b = this.reader.ReadByte();
						}
					}
					if (this.meta.version < AstarSerializer.V4_3_85)
					{
						b &= 79;
					}
					if (this.meta.version < AstarSerializer.V4_3_87)
					{
						b |= 48;
					}
					array[i] = new Connection(graphNode, num2, b);
				}
				else
				{
					array[i] = new Connection(graphNode, num2, true, true);
				}
			}
			return array;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000526C8 File Offset: 0x000508C8
		public GraphNode DeserializeNodeReference()
		{
			int num = this.reader.ReadInt32();
			if (this.id2NodeMapping == null)
			{
				throw new Exception("Calling DeserializeNodeReference when not deserializing node references");
			}
			if (num == -1)
			{
				return null;
			}
			GraphNode graphNode = this.id2NodeMapping[num];
			if (graphNode == null)
			{
				throw new Exception("Invalid id (" + num.ToString() + ")");
			}
			return graphNode;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00052721 File Offset: 0x00050921
		public void SerializeVector3(Vector3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00052756 File Offset: 0x00050956
		public Vector3 DeserializeVector3()
		{
			return new Vector3(this.reader.ReadSingle(), this.reader.ReadSingle(), this.reader.ReadSingle());
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0005277E File Offset: 0x0005097E
		public void SerializeInt3(Int3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000527B3 File Offset: 0x000509B3
		public Int3 DeserializeInt3()
		{
			return new Int3(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32());
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x000527DB File Offset: 0x000509DB
		public int DeserializeInt(int defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadInt32();
			}
			return defaultValue;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0005280F File Offset: 0x00050A0F
		public float DeserializeFloat(float defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadSingle();
			}
			return defaultValue;
		}

		// Token: 0x04000A2E RID: 2606
		private readonly GraphNode[] id2NodeMapping;

		// Token: 0x04000A2F RID: 2607
		public readonly BinaryReader reader;

		// Token: 0x04000A30 RID: 2608
		public readonly BinaryWriter writer;

		// Token: 0x04000A31 RID: 2609
		public readonly uint graphIndex;

		// Token: 0x04000A32 RID: 2610
		public readonly GraphMeta meta;

		// Token: 0x04000A33 RID: 2611
		public bool[] persistentGraphs;
	}
}
