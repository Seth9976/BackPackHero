using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class AstarData
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A623 File Offset: 0x00008823
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000A62B File Offset: 0x0000882B
		public NavMeshGraph navmesh { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A634 File Offset: 0x00008834
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000A63C File Offset: 0x0000883C
		public GridGraph gridGraph { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A645 File Offset: 0x00008845
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000A64D File Offset: 0x0000884D
		public LayerGridGraph layerGridGraph { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A656 File Offset: 0x00008856
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000A65E File Offset: 0x0000885E
		public PointGraph pointGraph { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A667 File Offset: 0x00008867
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000A66F File Offset: 0x0000886F
		public RecastGraph recastGraph { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A678 File Offset: 0x00008878
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000A680 File Offset: 0x00008880
		public LinkGraph linkGraph { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A689 File Offset: 0x00008889
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000A691 File Offset: 0x00008891
		public Type[] graphTypes { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A69C File Offset: 0x0000889C
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000A6CA File Offset: 0x000088CA
		private byte[] data
		{
			get
			{
				byte[] array = ((this.dataString != null) ? Convert.FromBase64String(this.dataString) : null);
				if (array != null && array.Length == 0)
				{
					return null;
				}
				return array;
			}
			set
			{
				this.dataString = ((value != null) ? Convert.ToBase64String(value) : null);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A6DE File Offset: 0x000088DE
		internal AstarData(AstarPath active)
		{
			this.active = active;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A704 File Offset: 0x00008904
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A70C File Offset: 0x0000890C
		public void SetData(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A718 File Offset: 0x00008918
		public void OnEnable()
		{
			if (this.graphTypes == null)
			{
				this.FindGraphTypes();
			}
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			if (this.cacheStartup && this.file_cachedStartup != null && Application.isPlaying)
			{
				this.LoadFromCache();
				return;
			}
			this.DeserializeGraphs();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A771 File Offset: 0x00008971
		internal void LockGraphStructure(bool allowAddingGraphs = false)
		{
			this.graphStructureLocked.Add(allowAddingGraphs);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A77F File Offset: 0x0000897F
		internal void UnlockGraphStructure()
		{
			if (this.graphStructureLocked.Count == 0)
			{
				throw new InvalidOperationException();
			}
			this.graphStructureLocked.RemoveAt(this.graphStructureLocked.Count - 1);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A7AC File Offset: 0x000089AC
		private PathProcessor.GraphUpdateLock AssertSafe(bool onlyAddingGraph = false)
		{
			if (this.graphStructureLocked.Count > 0)
			{
				bool flag = true;
				for (int i = 0; i < this.graphStructureLocked.Count; i++)
				{
					flag &= this.graphStructureLocked[i];
				}
				if (!onlyAddingGraph || !flag)
				{
					throw new InvalidOperationException("Graphs cannot be added, removed or serialized while the graph structure is locked. This is the case when a graph is currently being scanned and when executing graph updates and work items.\nHowever as a special case, graphs can be added inside work items.");
				}
			}
			PathProcessor.GraphUpdateLock graphUpdateLock = this.active.PausePathfinding();
			if (!this.active.IsInsideWorkItem)
			{
				this.active.FlushWorkItems();
				this.active.pathReturnQueue.ReturnPaths(false);
			}
			return graphUpdateLock;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A834 File Offset: 0x00008A34
		public void GetNodes(Action<GraphNode> callback)
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(callback);
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A86C File Offset: 0x00008A6C
		public void UpdateShortcuts()
		{
			this.navmesh = (NavMeshGraph)this.FindGraphOfType(typeof(NavMeshGraph));
			this.gridGraph = (GridGraph)this.FindGraphOfType(typeof(GridGraph));
			this.layerGridGraph = (LayerGridGraph)this.FindGraphOfType(typeof(LayerGridGraph));
			this.pointGraph = (PointGraph)this.FindGraphOfType(typeof(PointGraph));
			this.recastGraph = (RecastGraph)this.FindGraphOfType(typeof(RecastGraph));
			this.linkGraph = (LinkGraph)this.FindGraphOfType(typeof(LinkGraph));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A91C File Offset: 0x00008B1C
		public void LoadFromCache()
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			if (this.file_cachedStartup != null)
			{
				byte[] bytes = this.file_cachedStartup.bytes;
				this.DeserializeGraphs(bytes);
				GraphModifier.TriggerEvent(GraphModifier.EventType.PostCacheLoad);
			}
			else
			{
				Debug.LogError("Can't load from cache since the cache is empty");
			}
			graphUpdateLock.Release();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A96C File Offset: 0x00008B6C
		public byte[] SerializeGraphs()
		{
			return this.SerializeGraphs(SerializeSettings.Settings);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A97C File Offset: 0x00008B7C
		public byte[] SerializeGraphs(SerializeSettings settings)
		{
			uint num;
			return this.SerializeGraphs(settings, out num);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000A994 File Offset: 0x00008B94
		public byte[] SerializeGraphs(SerializeSettings settings, out uint checksum)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			AstarSerializer astarSerializer = new AstarSerializer(this, settings, this.active.gameObject);
			astarSerializer.OpenSerialize();
			astarSerializer.SerializeGraphs(this.graphs);
			astarSerializer.SerializeExtraInfo();
			byte[] array = astarSerializer.CloseSerialize();
			checksum = astarSerializer.GetChecksum();
			graphUpdateLock.Release();
			return array;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000A9EC File Offset: 0x00008BEC
		public void DeserializeGraphs()
		{
			byte[] data = this.data;
			if (data != null)
			{
				this.DeserializeGraphs(data);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AA0C File Offset: 0x00008C0C
		public void ClearGraphs()
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			this.ClearGraphsInternal();
			graphUpdateLock.Release();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AA30 File Offset: 0x00008C30
		private void ClearGraphsInternal()
		{
			if (this.graphs == null)
			{
				return;
			}
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.active.DirtyBounds(this.graphs[i].bounds);
					((IGraphInternals)this.graphs[i]).OnDestroy();
					this.graphs[i].active = null;
				}
			}
			this.graphs = new NavGraph[0];
			this.UpdateShortcuts();
			graphUpdateLock.Release();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public void DisposeUnmanagedData()
		{
			if (this.graphs == null)
			{
				return;
			}
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					((IGraphInternals)this.graphs[i]).DisposeUnmanagedData();
				}
			}
			graphUpdateLock.Release();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AB08 File Offset: 0x00008D08
		internal void DestroyAllNodes()
		{
			if (this.graphs == null)
			{
				return;
			}
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					((IGraphInternals)this.graphs[i]).DestroyAllNodes();
				}
			}
			graphUpdateLock.Release();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AB57 File Offset: 0x00008D57
		public void OnDestroy()
		{
			this.ClearGraphsInternal();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AB60 File Offset: 0x00008D60
		public void DeserializeGraphs(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			this.ClearGraphs();
			this.DeserializeGraphsAdditive(bytes);
			graphUpdateLock.Release();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public void DeserializeGraphsAdditive(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this, this.active.gameObject);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPartAdditive(astarSerializer);
					astarSerializer.CloseDeserialize();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip).\nThe data is either corrupt or it was saved using a 3.0.x or earlier version of the system");
				}
				this.active.VerifyIntegrity();
			}
			catch (Exception ex)
			{
				Debug.LogError(new Exception("Caught exception while deserializing data.", ex));
				this.graphs = new NavGraph[0];
			}
			this.UpdateShortcuts();
			GraphModifier.TriggerEvent(GraphModifier.EventType.PostGraphLoad);
			graphUpdateLock.Release();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AC38 File Offset: 0x00008E38
		private void DeserializeGraphsPartAdditive(AstarSerializer sr)
		{
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			List<NavGraph> list = new List<NavGraph>(this.graphs);
			sr.SetGraphIndexOffset(list.Count);
			this.FindGraphTypes();
			NavGraph[] newGraphs = sr.DeserializeGraphs(this.graphTypes);
			list.AddRange(newGraphs);
			if ((long)list.Count > 255L)
			{
				throw new InvalidOperationException("Graph Count Limit Reached. You cannot have more than " + 254U.ToString() + " graphs.");
			}
			this.graphs = list.ToArray();
			int i;
			int m;
			for (i = 0; i < this.graphs.Length; i = m + 1)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(delegate(GraphNode node)
					{
						node.GraphIndex = (uint)i;
					});
				}
				m = i;
			}
			for (int j = 0; j < this.graphs.Length; j++)
			{
				for (int k = j + 1; k < this.graphs.Length; k++)
				{
					if (this.graphs[j] != null && this.graphs[k] != null && this.graphs[j].guid == this.graphs[k].guid)
					{
						Debug.LogWarning("Guid Conflict when importing graphs additively. Imported graph will get a new Guid.\nThis message is (relatively) harmless.");
						this.graphs[j].guid = Guid.NewGuid();
						break;
					}
				}
			}
			sr.PostDeserialization();
			this.active.AddWorkItem(delegate(IWorkItemContext ctx)
			{
				for (int l = 0; l < newGraphs.Length; l++)
				{
					ctx.DirtyBounds(newGraphs[l].bounds);
				}
			});
			this.active.FlushWorkItems();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public void FindGraphTypes()
		{
			if (this.graphTypes != null)
			{
				return;
			}
			List<Type> list = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			int i = 0;
			while (i < assemblies.Length)
			{
				Assembly assembly = assemblies[i];
				Type[] array = null;
				try
				{
					array = assembly.GetTypes();
				}
				catch
				{
					goto IL_008B;
				}
				goto IL_0032;
				IL_008B:
				i++;
				continue;
				IL_0032:
				foreach (Type type in array)
				{
					Type type2 = type.BaseType;
					while (type2 != null)
					{
						if (object.Equals(type2, typeof(NavGraph)))
						{
							list.Add(type);
							break;
						}
						type2 = type2.BaseType;
					}
				}
				goto IL_008B;
			}
			this.graphTypes = list.ToArray();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AEAC File Offset: 0x000090AC
		internal NavGraph CreateGraph(Type type)
		{
			NavGraph navGraph = Activator.CreateInstance(type) as NavGraph;
			navGraph.active = this.active;
			return navGraph;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AEC5 File Offset: 0x000090C5
		public T AddGraph<T>() where T : NavGraph
		{
			return this.AddGraph(typeof(T)) as T;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public NavGraph AddGraph(Type type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (object.Equals(this.graphTypes[i], type))
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"No NavGraph of type '",
					(type != null) ? type.ToString() : null,
					"' could be found, ",
					this.graphTypes.Length.ToString(),
					" graph types are avaliable"
				}));
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000AF80 File Offset: 0x00009180
		private void AddGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(true);
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == null)
				{
					this.graphs[i] = graph;
					graph.graphIndex = (uint)i;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (this.graphs != null && (long)this.graphs.Length >= 254L)
				{
					throw new Exception("Graph Count Limit Reached. You cannot have more than " + 254U.ToString() + " graphs.");
				}
				Memory.Realloc<NavGraph>(ref this.graphs, this.graphs.Length + 1);
				this.graphs[this.graphs.Length - 1] = graph;
				graph.graphIndex = (uint)(this.graphs.Length - 1);
			}
			this.UpdateShortcuts();
			graph.active = this.active;
			graphUpdateLock.Release();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B058 File Offset: 0x00009258
		public bool RemoveGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			this.active.DirtyBounds(graph.bounds);
			((IGraphInternals)graph).OnDestroy();
			graph.active = null;
			int num = Array.IndexOf<NavGraph>(this.graphs, graph);
			if (num != -1)
			{
				this.graphs[num] = null;
			}
			this.UpdateShortcuts();
			this.active.offMeshLinks.Refresh();
			graphUpdateLock.Release();
			return num != -1;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B0CC File Offset: 0x000092CC
		public static NavGraph GetGraph(GraphNode node)
		{
			if (node == null || node.Destroyed)
			{
				return null;
			}
			AstarPath astarPath = AstarPath.active;
			if (astarPath == null)
			{
				return null;
			}
			AstarData data = astarPath.data;
			if (data == null || data.graphs == null)
			{
				return null;
			}
			uint graphIndex = node.GraphIndex;
			return data.graphs[(int)graphIndex];
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B114 File Offset: 0x00009314
		public NavGraph FindGraph(Func<NavGraph, bool> predicate)
		{
			if (this.graphs != null)
			{
				for (int i = 0; i < this.graphs.Length; i++)
				{
					if (this.graphs[i] != null && predicate(this.graphs[i]))
					{
						return this.graphs[i];
					}
				}
			}
			return null;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B160 File Offset: 0x00009360
		public NavGraph FindGraphOfType(Type type)
		{
			return this.FindGraph((NavGraph graph) => object.Equals(graph.GetType(), type));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B18C File Offset: 0x0000938C
		public NavGraph FindGraphWhichInheritsFrom(Type type)
		{
			return this.FindGraph((NavGraph graph) => WindowsStoreCompatibility.GetTypeInfo(type).IsAssignableFrom(WindowsStoreCompatibility.GetTypeInfo(graph.GetType())));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public IEnumerable FindGraphsOfType(Type type)
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] != null && object.Equals(this.graphs[i].GetType(), type))
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B1CF File Offset: 0x000093CF
		public IEnumerable GetUpdateableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] is IUpdatableGraph)
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public int GetGraphIndex(NavGraph graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			int num = -1;
			if (this.graphs != null)
			{
				num = Array.IndexOf<NavGraph>(this.graphs, graph);
				if (num == -1)
				{
					Debug.LogError("Graph doesn't exist");
				}
			}
			return num;
		}

		// Token: 0x040001B6 RID: 438
		internal AstarPath active;

		// Token: 0x040001BE RID: 446
		[NonSerialized]
		public NavGraph[] graphs = new NavGraph[0];

		// Token: 0x040001BF RID: 447
		[SerializeField]
		private string dataString;

		// Token: 0x040001C0 RID: 448
		public TextAsset file_cachedStartup;

		// Token: 0x040001C1 RID: 449
		[SerializeField]
		public bool cacheStartup;

		// Token: 0x040001C2 RID: 450
		private List<bool> graphStructureLocked = new List<bool>();
	}
}
