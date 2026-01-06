using System;
using System.IO;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020000B0 RID: 176
	public class GraphSerializationContext
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x0003432B File Offset: 0x0003252B
		public GraphSerializationContext(BinaryReader reader, GraphNode[] id2NodeMapping, uint graphIndex, GraphMeta meta)
		{
			this.reader = reader;
			this.id2NodeMapping = id2NodeMapping;
			this.graphIndex = graphIndex;
			this.meta = meta;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00034350 File Offset: 0x00032550
		public GraphSerializationContext(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003435F File Offset: 0x0003255F
		public void SerializeNodeReference(GraphNode node)
		{
			this.writer.Write((node == null) ? (-1) : node.NodeIndex);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00034378 File Offset: 0x00032578
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

		// Token: 0x060007D4 RID: 2004 RVA: 0x000343D1 File Offset: 0x000325D1
		public void SerializeVector3(Vector3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00034406 File Offset: 0x00032606
		public Vector3 DeserializeVector3()
		{
			return new Vector3(this.reader.ReadSingle(), this.reader.ReadSingle(), this.reader.ReadSingle());
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0003442E File Offset: 0x0003262E
		public void SerializeInt3(Int3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00034463 File Offset: 0x00032663
		public Int3 DeserializeInt3()
		{
			return new Int3(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32());
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003448B File Offset: 0x0003268B
		public int DeserializeInt(int defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadInt32();
			}
			return defaultValue;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000344BF File Offset: 0x000326BF
		public float DeserializeFloat(float defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadSingle();
			}
			return defaultValue;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000344F4 File Offset: 0x000326F4
		public Object DeserializeUnityObject()
		{
			if (this.reader.ReadInt32() == 2147483647)
			{
				return null;
			}
			string text = this.reader.ReadString();
			string text2 = this.reader.ReadString();
			string text3 = this.reader.ReadString();
			Type type = Type.GetType(text2);
			if (type == null)
			{
				Debug.LogError("Could not find type '" + text2 + "'. Cannot deserialize Unity reference");
				return null;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				UnityReferenceHelper[] array = Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[];
				int i = 0;
				while (i < array.Length)
				{
					if (array[i].GetGUID() == text3)
					{
						if (type == typeof(GameObject))
						{
							return array[i].gameObject;
						}
						return array[i].GetComponent(type);
					}
					else
					{
						i++;
					}
				}
			}
			Object[] array2 = Resources.LoadAll(text, type);
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].name == text || array2.Length == 1)
				{
					return array2[j];
				}
			}
			return null;
		}

		// Token: 0x040004A4 RID: 1188
		private readonly GraphNode[] id2NodeMapping;

		// Token: 0x040004A5 RID: 1189
		public readonly BinaryReader reader;

		// Token: 0x040004A6 RID: 1190
		public readonly BinaryWriter writer;

		// Token: 0x040004A7 RID: 1191
		public readonly uint graphIndex;

		// Token: 0x040004A8 RID: 1192
		public readonly GraphMeta meta;
	}
}
