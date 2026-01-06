using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000060 RID: 96
	public static class GraphInstances
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00007394 File Offset: 0x00005594
		public static void Instantiate(GraphReference instance)
		{
			object obj = GraphInstances.@lock;
			lock (obj)
			{
				Ensure.That("instance").IsNotNull<GraphReference>(instance);
				instance.CreateGraphData();
				instance.graph.Instantiate(instance);
				HashSet<GraphReference> hashSet;
				if (!GraphInstances.byGraph.TryGetValue(instance.graph, out hashSet))
				{
					hashSet = new HashSet<GraphReference>();
					GraphInstances.byGraph.Add(instance.graph, hashSet);
				}
				if (!hashSet.Add(instance))
				{
					Debug.LogWarning(string.Format("Attempting to add duplicate graph instance mapping:\n{0} => {1}", instance.graph, instance));
				}
				HashSet<GraphReference> hashSet2;
				if (!GraphInstances.byParent.TryGetValue(instance.parent, out hashSet2))
				{
					hashSet2 = new HashSet<GraphReference>();
					GraphInstances.byParent.Add(instance.parent, hashSet2);
				}
				if (!hashSet2.Add(instance))
				{
					Debug.LogWarning(string.Format("Attempting to add duplicate parent instance mapping:\n{0} => {1}", instance.parent.ToSafeString(), instance));
				}
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000748C File Offset: 0x0000568C
		public static void Uninstantiate(GraphReference instance)
		{
			object obj = GraphInstances.@lock;
			lock (obj)
			{
				instance.graph.Uninstantiate(instance);
				HashSet<GraphReference> hashSet;
				if (!GraphInstances.byGraph.TryGetValue(instance.graph, out hashSet))
				{
					throw new InvalidOperationException("Graph instance not found via graph.");
				}
				if (hashSet.Remove(instance))
				{
					if (hashSet.Count == 0)
					{
						GraphInstances.byGraph.Remove(instance.graph);
					}
				}
				else
				{
					Debug.LogWarning(string.Format("Could not find graph instance mapping to remove:\n{0} => {1}", instance.graph, instance));
				}
				HashSet<GraphReference> hashSet2;
				if (!GraphInstances.byParent.TryGetValue(instance.parent, out hashSet2))
				{
					throw new InvalidOperationException("Graph instance not found via parent.");
				}
				if (hashSet2.Remove(instance))
				{
					if (hashSet2.Count == 0)
					{
						GraphInstances.byParent.Remove(instance.parent);
					}
				}
				else
				{
					Debug.LogWarning(string.Format("Could not find parent instance mapping to remove:\n{0} => {1}", instance.parent.ToSafeString(), instance));
				}
				instance.FreeGraphData();
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007590 File Offset: 0x00005790
		public static HashSet<GraphReference> OfPooled(IGraph graph)
		{
			Ensure.That("graph").IsNotNull<IGraph>(graph);
			object obj = GraphInstances.@lock;
			HashSet<GraphReference> hashSet2;
			lock (obj)
			{
				HashSet<GraphReference> hashSet;
				if (GraphInstances.byGraph.TryGetValue(graph, out hashSet))
				{
					hashSet2 = hashSet.ToHashSetPooled<GraphReference>();
				}
				else
				{
					hashSet2 = HashSetPool<GraphReference>.New();
				}
			}
			return hashSet2;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000075F8 File Offset: 0x000057F8
		public static HashSet<GraphReference> ChildrenOfPooled(IGraphParent parent)
		{
			Ensure.That("parent").IsNotNull<IGraphParent>(parent);
			object obj = GraphInstances.@lock;
			HashSet<GraphReference> hashSet2;
			lock (obj)
			{
				HashSet<GraphReference> hashSet;
				if (GraphInstances.byParent.TryGetValue(parent, out hashSet))
				{
					hashSet2 = hashSet.ToHashSetPooled<GraphReference>();
				}
				else
				{
					hashSet2 = HashSetPool<GraphReference>.New();
				}
			}
			return hashSet2;
		}

		// Token: 0x040000CE RID: 206
		private static readonly object @lock = new object();

		// Token: 0x040000CF RID: 207
		private static readonly Dictionary<IGraph, HashSet<GraphReference>> byGraph = new Dictionary<IGraph, HashSet<GraphReference>>();

		// Token: 0x040000D0 RID: 208
		private static readonly Dictionary<IGraphParent, HashSet<GraphReference>> byParent = new Dictionary<IGraphParent, HashSet<GraphReference>>();
	}
}
