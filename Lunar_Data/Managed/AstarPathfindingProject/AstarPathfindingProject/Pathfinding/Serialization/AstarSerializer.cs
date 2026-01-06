using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Pathfinding.Ionic.Zip;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000229 RID: 553
	public class AstarSerializer
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x00052843 File Offset: 0x00050A43
		private static StringBuilder GetStringBuilder()
		{
			AstarSerializer._stringBuilder.Length = 0;
			return AstarSerializer._stringBuilder;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00052855 File Offset: 0x00050A55
		public AstarSerializer(AstarData data, GameObject contextRoot)
			: this(data, SerializeSettings.Settings, contextRoot)
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00052864 File Offset: 0x00050A64
		public AstarSerializer(AstarData data, SerializeSettings settings, GameObject contextRoot)
		{
			this.data = data;
			this.contextRoot = contextRoot;
			this.settings = settings;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00052893 File Offset: 0x00050A93
		public void SetGraphIndexOffset(int offset)
		{
			this.graphIndexOffset = offset;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0005289C File Offset: 0x00050A9C
		private void AddChecksum(byte[] bytes)
		{
			this.checksum = Checksum.GetChecksum(bytes, this.checksum);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000528B0 File Offset: 0x00050AB0
		private void AddEntry(string name, byte[] bytes)
		{
			this.zip.AddEntry(name, bytes);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000528C0 File Offset: 0x00050AC0
		public uint GetChecksum()
		{
			return this.checksum;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000528C8 File Offset: 0x00050AC8
		public void OpenSerialize()
		{
			this.zipStream = new MemoryStream();
			this.zip = new ZipFile();
			this.zip.AlternateEncoding = Encoding.UTF8;
			this.zip.AlternateEncodingUsage = ZipOption.Always;
			this.zip.ParallelDeflateThreshold = -1L;
			this.meta = new GraphMeta();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00052920 File Offset: 0x00050B20
		public byte[] CloseSerialize()
		{
			byte[] array = this.SerializeMeta();
			this.AddChecksum(array);
			this.AddEntry("meta.json", array);
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			foreach (ZipEntry zipEntry in this.zip.Entries)
			{
				zipEntry.AccessedTime = dateTime;
				zipEntry.CreationTime = dateTime;
				zipEntry.LastModified = dateTime;
				zipEntry.ModifiedTime = dateTime;
			}
			this.zip.Save(this.zipStream);
			this.zip.Dispose();
			array = this.zipStream.ToArray();
			this.zip = null;
			this.zipStream = null;
			return array;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000529E8 File Offset: 0x00050BE8
		public void SerializeGraphs(NavGraph[] _graphs)
		{
			if (this.graphs != null)
			{
				throw new InvalidOperationException("Cannot serialize graphs multiple times.");
			}
			this.graphs = _graphs;
			if (this.zip == null)
			{
				throw new NullReferenceException("You must not call CloseSerialize before a call to this function");
			}
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			this.persistentGraphs = new bool[this.graphs.Length];
			for (int i = 0; i < this.graphs.Length; i++)
			{
				this.persistentGraphs[i] = this.graphs[i] != null && this.graphs[i].persistent;
				if (this.persistentGraphs[i])
				{
					byte[] array = this.Serialize(this.graphs[i]);
					this.AddChecksum(array);
					this.AddEntry("graph" + i.ToString() + ".json", array);
				}
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00052ABC File Offset: 0x00050CBC
		private byte[] SerializeMeta()
		{
			if (this.graphs == null)
			{
				throw new Exception("No call to SerializeGraphs has been done");
			}
			this.meta.version = AstarPath.Version;
			this.meta.graphs = this.graphs.Length;
			this.meta.guids = new List<string>();
			this.meta.typeNames = new List<string>();
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.persistentGraphs[i])
				{
					this.meta.guids.Add(this.graphs[i].guid.ToString());
					this.meta.typeNames.Add(this.graphs[i].GetType().FullName);
				}
				else
				{
					this.meta.guids.Add(null);
					this.meta.typeNames.Add(null);
				}
			}
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			TinyJsonSerializer.Serialize(this.meta, stringBuilder);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00052BD0 File Offset: 0x00050DD0
		public byte[] Serialize(NavGraph graph)
		{
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			TinyJsonSerializer.Serialize(graph, stringBuilder);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000033F6 File Offset: 0x000015F6
		[Obsolete("Not used anymore. You can safely remove the call to this function.")]
		public void SerializeNodes()
		{
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00052BFC File Offset: 0x00050DFC
		private static int GetMaxNodeIndexInAllGraphs(NavGraph[] graphs)
		{
			int maxIndex = 0;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < graphs.Length; i++)
			{
				if (graphs[i] != null && graphs[i].persistent)
				{
					NavGraph navGraph = graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							maxIndex = Math.Max((int)node.NodeIndex, maxIndex);
							if (node.Destroyed)
							{
								Debug.LogError("Graph contains destroyed nodes. This is a bug.");
							}
						});
					}
					navGraph.GetNodes(action);
				}
			}
			return maxIndex;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00052C60 File Offset: 0x00050E60
		private static byte[] SerializeNodeIndices(NavGraph[] graphs)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(memoryStream);
			int maxNodeIndexInAllGraphs = AstarSerializer.GetMaxNodeIndexInAllGraphs(graphs);
			writer.Write(maxNodeIndexInAllGraphs);
			int maxNodeIndex2 = 0;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < graphs.Length; i++)
			{
				if (graphs[i] != null && graphs[i].persistent)
				{
					NavGraph navGraph = graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							maxNodeIndex2 = Math.Max((int)node.NodeIndex, maxNodeIndex2);
							writer.Write(node.NodeIndex);
						});
					}
					navGraph.GetNodes(action);
				}
			}
			if (maxNodeIndex2 != maxNodeIndexInAllGraphs)
			{
				throw new Exception("Some graphs are not consistent in their GetNodes calls, sequential calls give different results.");
			}
			byte[] array = memoryStream.ToArray();
			writer.Close();
			return array;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00052D0C File Offset: 0x00050F0C
		private static byte[] SerializeGraphExtraInfo(NavGraph graph, bool[] persistentGraphs)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			GraphSerializationContext graphSerializationContext = new GraphSerializationContext(binaryWriter, persistentGraphs);
			((IGraphInternals)graph).SerializeExtraInfo(graphSerializationContext);
			byte[] array = memoryStream.ToArray();
			binaryWriter.Close();
			return array;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00052D40 File Offset: 0x00050F40
		private static byte[] SerializeGraphNodeReferences(NavGraph graph, bool[] persistentGraphs)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryWriter, persistentGraphs);
			graph.GetNodes(delegate(GraphNode node)
			{
				node.SerializeReferences(ctx);
			});
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00052D8C File Offset: 0x00050F8C
		public void SerializeExtraInfo()
		{
			if (!this.settings.nodes)
			{
				return;
			}
			if (this.graphs == null)
			{
				throw new InvalidOperationException("Cannot serialize extra info with no serialized graphs (call SerializeGraphs first)");
			}
			byte[] array = AstarSerializer.SerializeNodeIndices(this.graphs);
			this.AddChecksum(array);
			this.AddEntry("graph_references.binary", array);
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null && this.graphs[i].persistent)
				{
					array = AstarSerializer.SerializeGraphExtraInfo(this.graphs[i], this.persistentGraphs);
					this.AddChecksum(array);
					this.AddEntry("graph" + i.ToString() + "_extra.binary", array);
					array = AstarSerializer.SerializeGraphNodeReferences(this.graphs[i], this.persistentGraphs);
					this.AddChecksum(array);
					this.AddEntry("graph" + i.ToString() + "_references.binary", array);
				}
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00052E7B File Offset: 0x0005107B
		private ZipEntry GetEntry(string name)
		{
			return this.zip[name];
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00052E89 File Offset: 0x00051089
		private bool ContainsEntry(string name)
		{
			return this.GetEntry(name) != null;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00052E98 File Offset: 0x00051098
		public bool OpenDeserialize(byte[] bytes)
		{
			this.zipStream = new MemoryStream();
			this.zipStream.Write(bytes, 0, bytes.Length);
			this.zipStream.Position = 0L;
			try
			{
				this.zip = ZipFile.Read(this.zipStream);
				this.zip.ParallelDeflateThreshold = -1L;
			}
			catch (Exception ex)
			{
				string text = "Caught exception when loading from zip\n";
				Exception ex2 = ex;
				Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
				this.zipStream.Dispose();
				return false;
			}
			if (this.ContainsEntry("meta.json"))
			{
				this.meta = this.DeserializeMeta(this.GetEntry("meta.json"));
			}
			else
			{
				if (!this.ContainsEntry("meta.binary"))
				{
					throw new Exception("No metadata found in serialized data.");
				}
				this.meta = this.DeserializeBinaryMeta(this.GetEntry("meta.binary"));
			}
			if (AstarSerializer.FullyDefinedVersion(this.meta.version) > AstarSerializer.FullyDefinedVersion(AstarPath.Version))
			{
				string[] array = new string[5];
				array[0] = "Trying to load data from a newer version of the A* Pathfinding Project\nCurrent version: ";
				int num = 1;
				Version version = AstarPath.Version;
				array[num] = ((version != null) ? version.ToString() : null);
				array[2] = " Data version: ";
				int num2 = 3;
				Version version2 = this.meta.version;
				array[num2] = ((version2 != null) ? version2.ToString() : null);
				array[4] = "\nThis is usually fine as the stored data is usually backwards and forwards compatible.\nHowever node data (not settings) can get corrupted between versions (even though I try my best to keep compatibility), so it is recommended to recalculate any caches (those for faster startup) and resave any files. Even if it seems to load fine, it might cause subtle bugs.\n";
				Debug.LogWarning(string.Concat(array));
			}
			return true;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00053000 File Offset: 0x00051200
		private static Version FullyDefinedVersion(Version v)
		{
			return new Version(Mathf.Max(v.Major, 0), Mathf.Max(v.Minor, 0), Mathf.Max(v.Build, 0), Mathf.Max(v.Revision, 0));
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00053037 File Offset: 0x00051237
		public void CloseDeserialize()
		{
			this.zipStream.Dispose();
			this.zip.Dispose();
			this.zip = null;
			this.zipStream = null;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00053060 File Offset: 0x00051260
		private NavGraph DeserializeGraph(int zipIndex, int graphIndex, Type[] availableGraphTypes)
		{
			Type graphType = this.meta.GetGraphType(zipIndex, availableGraphTypes);
			if (object.Equals(graphType, null))
			{
				return null;
			}
			NavGraph navGraph = this.data.CreateGraph(graphType);
			navGraph.graphIndex = (uint)graphIndex;
			string text = "graph" + zipIndex.ToString() + ".json";
			if (!this.ContainsEntry(text))
			{
				throw new FileNotFoundException(string.Concat(new string[]
				{
					"Could not find data for graph ",
					zipIndex.ToString(),
					" in zip. Entry 'graph",
					zipIndex.ToString(),
					".json' does not exist"
				}));
			}
			TinyJsonDeserializer.Deserialize(AstarSerializer.GetString(this.GetEntry(text)), graphType, navGraph, this.contextRoot);
			if (navGraph.guid.ToString() != this.meta.guids[zipIndex])
			{
				string text2 = "Guid in graph file not equal to guid defined in meta file. Have you edited the data manually?\n";
				Guid guid = navGraph.guid;
				throw new Exception(text2 + guid.ToString() + " != " + this.meta.guids[zipIndex]);
			}
			return navGraph;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00053178 File Offset: 0x00051378
		public NavGraph[] DeserializeGraphs(Type[] availableGraphTypes)
		{
			List<NavGraph> list = new List<NavGraph>();
			this.graphIndexInZip = new Dictionary<NavGraph, int>();
			for (int i = 0; i < this.meta.graphs; i++)
			{
				int num = list.Count + this.graphIndexOffset;
				NavGraph navGraph = this.DeserializeGraph(i, num, availableGraphTypes);
				if (navGraph != null)
				{
					list.Add(navGraph);
					this.graphIndexInZip[navGraph] = i;
				}
			}
			this.graphs = list.ToArray();
			this.DeserializeEditorSettingsCompatibility();
			this.DeserializeExtraInfo();
			return this.graphs;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000531FC File Offset: 0x000513FC
		private bool DeserializeExtraInfo(NavGraph graph)
		{
			ZipEntry entry = this.GetEntry("graph" + this.graphIndexInZip[graph].ToString() + "_extra.binary");
			if (entry == null)
			{
				return false;
			}
			GraphSerializationContext graphSerializationContext = new GraphSerializationContext(AstarSerializer.GetBinaryReader(entry), null, graph.graphIndex, this.meta);
			((IGraphInternals)graph).DeserializeExtraInfo(graphSerializationContext);
			return true;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0005325C File Offset: 0x0005145C
		private bool AnyDestroyedNodesInGraphs()
		{
			bool result = false;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				NavGraph navGraph = this.graphs[i];
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (node.Destroyed)
						{
							result = true;
						}
					});
				}
				navGraph.GetNodes(action);
			}
			return result;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000532BC File Offset: 0x000514BC
		private GraphNode[] DeserializeNodeReferenceMap()
		{
			ZipEntry entry = this.GetEntry("graph_references.binary");
			if (entry == null)
			{
				throw new Exception("Node references not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
			}
			BinaryReader reader = AstarSerializer.GetBinaryReader(entry);
			int num = reader.ReadInt32();
			GraphNode[] int2Node = new GraphNode[num + 1];
			try
			{
				Action<GraphNode> <>9__0;
				for (int i = 0; i < this.graphs.Length; i++)
				{
					NavGraph navGraph = this.graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							int num2 = reader.ReadInt32();
							int2Node[num2] = node;
						});
					}
					navGraph.GetNodes(action);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Some graph(s) has thrown an exception during GetNodes, or some graph(s) have deserialized more or fewer nodes than were serialized", ex);
			}
			if (reader.BaseStream.Position != reader.BaseStream.Length)
			{
				throw new Exception((reader.BaseStream.Length / 4L).ToString() + " nodes were serialized, but only data for " + (reader.BaseStream.Position / 4L).ToString() + " nodes was found. The data looks corrupt.");
			}
			reader.Close();
			return int2Node;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000533F4 File Offset: 0x000515F4
		private void DeserializeNodeReferences(NavGraph graph, GraphNode[] int2Node)
		{
			int num = this.graphIndexInZip[graph];
			ZipEntry entry = this.GetEntry("graph" + num.ToString() + "_references.binary");
			if (entry == null)
			{
				throw new Exception("Node references for graph " + num.ToString() + " not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
			}
			BinaryReader binaryReader = AstarSerializer.GetBinaryReader(entry);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryReader, int2Node, graph.graphIndex, this.meta);
			graph.GetNodes(delegate(GraphNode node)
			{
				node.DeserializeReferences(ctx);
			});
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00053480 File Offset: 0x00051680
		private void DeserializeAndRemoveOldNodeLinks(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				ctx.reader.ReadUInt64();
				GraphNode graphNode = ctx.DeserializeNodeReference();
				GraphNode graphNode2 = ctx.DeserializeNodeReference();
				GraphNode graphNode3 = ctx.DeserializeNodeReference();
				GraphNode graphNode4 = ctx.DeserializeNodeReference();
				ctx.DeserializeVector3();
				ctx.DeserializeVector3();
				ctx.reader.ReadBoolean();
				graphNode.ClearConnections(true);
				graphNode2.ClearConnections(true);
				graphNode.Walkable = false;
				graphNode2.Walkable = false;
				GraphNode.Disconnect(graphNode3, graphNode);
				GraphNode.Disconnect(graphNode4, graphNode2);
			}
			bool flag = false;
			int num2 = 0;
			while (num2 < this.graphs.Length && !flag)
			{
				if (this.graphs[num2] != null)
				{
					PointGraph pointGraph = this.graphs[num2] as PointGraph;
					if (pointGraph != null)
					{
						bool anyWalkable = false;
						int count2 = 0;
						pointGraph.GetNodes(delegate(GraphNode node)
						{
							anyWalkable |= node.Walkable;
							int count = count2;
							count2 = count + 1;
						});
						if (!anyWalkable && pointGraph.root == null && 2 * num == count2 && (count2 > 0 || pointGraph.name.Contains("used for node links")))
						{
							((IGraphInternals)this.graphs[num2]).DestroyAllNodes();
							List<NavGraph> list = new List<NavGraph>(this.graphs);
							list.RemoveAt(num2);
							this.graphs = list.ToArray();
							flag = true;
						}
						if (pointGraph.name == "PointGraph (used for node links)")
						{
							pointGraph.name = "PointGraph";
						}
					}
				}
				num2++;
			}
			if (!flag && num > 0)
			{
				Debug.LogWarning("Old off-mesh links were present in the serialized graph data. Not everything could be cleaned up properly. It is recommended that you re-scan all graphs and save the cache or graph file again. An attempt to migrate the old links was made, but a stray point graph may have been left behind.");
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0005362C File Offset: 0x0005182C
		private void DeserializeExtraInfo()
		{
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				flag |= this.DeserializeExtraInfo(this.graphs[i]);
			}
			if (!flag)
			{
				return;
			}
			if (this.AnyDestroyedNodesInGraphs())
			{
				Debug.LogError("Graph contains destroyed nodes. This is a bug.");
			}
			GraphNode[] array = this.DeserializeNodeReferenceMap();
			for (int j = 0; j < this.graphs.Length; j++)
			{
				this.DeserializeNodeReferences(this.graphs[j], array);
			}
			if (this.meta.version < AstarSerializer.V4_3_85)
			{
				ZipEntry entry = this.GetEntry("node_link2.binary");
				if (entry != null)
				{
					GraphSerializationContext graphSerializationContext = new GraphSerializationContext(AstarSerializer.GetBinaryReader(entry), array, 0U, this.meta);
					this.DeserializeAndRemoveOldNodeLinks(graphSerializationContext);
				}
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000536E4 File Offset: 0x000518E4
		public void PostDeserialization()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				GraphSerializationContext graphSerializationContext = new GraphSerializationContext(null, null, 0U, this.meta);
				((IGraphInternals)this.graphs[i]).PostDeserialization(graphSerializationContext);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00053724 File Offset: 0x00051924
		private void DeserializeEditorSettingsCompatibility()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				ZipEntry entry = this.GetEntry("graph" + this.graphIndexInZip[this.graphs[i]].ToString() + "_editor.json");
				if (entry != null)
				{
					((IGraphInternals)this.graphs[i]).SerializedEditorSettings = AstarSerializer.GetString(entry);
				}
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0005378C File Offset: 0x0005198C
		private static BinaryReader GetBinaryReader(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			return new BinaryReader(memoryStream);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000537B4 File Offset: 0x000519B4
		private static string GetString(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			StreamReader streamReader = new StreamReader(memoryStream);
			string text = streamReader.ReadToEnd();
			streamReader.Dispose();
			return text;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000537E9 File Offset: 0x000519E9
		private GraphMeta DeserializeMeta(ZipEntry entry)
		{
			return TinyJsonDeserializer.Deserialize(AstarSerializer.GetString(entry), typeof(GraphMeta), null, null) as GraphMeta;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00053808 File Offset: 0x00051A08
		private GraphMeta DeserializeBinaryMeta(ZipEntry entry)
		{
			GraphMeta graphMeta = new GraphMeta();
			BinaryReader binaryReader = AstarSerializer.GetBinaryReader(entry);
			if (binaryReader.ReadString() != "A*")
			{
				throw new Exception("Invalid magic number in saved data");
			}
			int num = binaryReader.ReadInt32();
			int num2 = binaryReader.ReadInt32();
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			if (num < 0)
			{
				graphMeta.version = new Version(0, 0);
			}
			else if (num2 < 0)
			{
				graphMeta.version = new Version(num, 0);
			}
			else if (num3 < 0)
			{
				graphMeta.version = new Version(num, num2);
			}
			else if (num4 < 0)
			{
				graphMeta.version = new Version(num, num2, num3);
			}
			else
			{
				graphMeta.version = new Version(num, num2, num3, num4);
			}
			graphMeta.graphs = binaryReader.ReadInt32();
			graphMeta.guids = new List<string>();
			int num5 = binaryReader.ReadInt32();
			for (int i = 0; i < num5; i++)
			{
				graphMeta.guids.Add(binaryReader.ReadString());
			}
			graphMeta.typeNames = new List<string>();
			num5 = binaryReader.ReadInt32();
			for (int j = 0; j < num5; j++)
			{
				graphMeta.typeNames.Add(binaryReader.ReadString());
			}
			binaryReader.Close();
			return graphMeta;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0005393C File Offset: 0x00051B3C
		public static void SaveToFile(string path, byte[] data)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				fileStream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00053978 File Offset: 0x00051B78
		public static byte[] LoadFromFile(string path)
		{
			byte[] array2;
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				byte[] array = new byte[(int)fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x04000A34 RID: 2612
		private AstarData data;

		// Token: 0x04000A35 RID: 2613
		private ZipFile zip;

		// Token: 0x04000A36 RID: 2614
		private MemoryStream zipStream;

		// Token: 0x04000A37 RID: 2615
		private GraphMeta meta;

		// Token: 0x04000A38 RID: 2616
		private SerializeSettings settings;

		// Token: 0x04000A39 RID: 2617
		private GameObject contextRoot;

		// Token: 0x04000A3A RID: 2618
		private NavGraph[] graphs;

		// Token: 0x04000A3B RID: 2619
		private bool[] persistentGraphs;

		// Token: 0x04000A3C RID: 2620
		private Dictionary<NavGraph, int> graphIndexInZip;

		// Token: 0x04000A3D RID: 2621
		private int graphIndexOffset;

		// Token: 0x04000A3E RID: 2622
		private const string binaryExt = ".binary";

		// Token: 0x04000A3F RID: 2623
		private const string jsonExt = ".json";

		// Token: 0x04000A40 RID: 2624
		private uint checksum = uint.MaxValue;

		// Token: 0x04000A41 RID: 2625
		private UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x04000A42 RID: 2626
		private static StringBuilder _stringBuilder = new StringBuilder();

		// Token: 0x04000A43 RID: 2627
		public static readonly Version V3_8_3 = new Version(3, 8, 3);

		// Token: 0x04000A44 RID: 2628
		public static readonly Version V3_9_0 = new Version(3, 9, 0);

		// Token: 0x04000A45 RID: 2629
		public static readonly Version V4_1_0 = new Version(4, 1, 0);

		// Token: 0x04000A46 RID: 2630
		public static readonly Version V4_3_2 = new Version(4, 3, 2);

		// Token: 0x04000A47 RID: 2631
		public static readonly Version V4_3_6 = new Version(4, 3, 6);

		// Token: 0x04000A48 RID: 2632
		public static readonly Version V4_3_37 = new Version(4, 3, 37);

		// Token: 0x04000A49 RID: 2633
		public static readonly Version V4_3_12 = new Version(4, 3, 12);

		// Token: 0x04000A4A RID: 2634
		public static readonly Version V4_3_68 = new Version(4, 3, 68);

		// Token: 0x04000A4B RID: 2635
		public static readonly Version V4_3_74 = new Version(4, 3, 74);

		// Token: 0x04000A4C RID: 2636
		public static readonly Version V4_3_80 = new Version(4, 3, 80);

		// Token: 0x04000A4D RID: 2637
		public static readonly Version V4_3_83 = new Version(4, 3, 83);

		// Token: 0x04000A4E RID: 2638
		public static readonly Version V4_3_85 = new Version(4, 3, 85);

		// Token: 0x04000A4F RID: 2639
		public static readonly Version V4_3_87 = new Version(4, 3, 87);
	}
}
