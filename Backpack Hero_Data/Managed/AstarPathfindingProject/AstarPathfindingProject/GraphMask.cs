using System;

namespace Pathfinding
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public struct GraphMask
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008F11 File Offset: 0x00007111
		public static GraphMask everything
		{
			get
			{
				return new GraphMask(-1);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008F19 File Offset: 0x00007119
		public GraphMask(int value)
		{
			this.value = value;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008F22 File Offset: 0x00007122
		public static implicit operator int(GraphMask mask)
		{
			return mask.value;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008F2A File Offset: 0x0000712A
		public static implicit operator GraphMask(int mask)
		{
			return new GraphMask(mask);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008F32 File Offset: 0x00007132
		public static GraphMask operator &(GraphMask lhs, GraphMask rhs)
		{
			return new GraphMask(lhs.value & rhs.value);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008F46 File Offset: 0x00007146
		public static GraphMask operator |(GraphMask lhs, GraphMask rhs)
		{
			return new GraphMask(lhs.value | rhs.value);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008F5A File Offset: 0x0000715A
		public static GraphMask operator ~(GraphMask lhs)
		{
			return new GraphMask(~lhs.value);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008F68 File Offset: 0x00007168
		public bool Contains(int graphIndex)
		{
			return ((this.value >> graphIndex) & 1) != 0;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008F7A File Offset: 0x0000717A
		public static GraphMask FromGraph(NavGraph graph)
		{
			return 1 << (int)graph.graphIndex;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008F8C File Offset: 0x0000718C
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008F9C File Offset: 0x0000719C
		public static GraphMask FromGraphName(string graphName)
		{
			NavGraph navGraph = AstarData.active.data.FindGraph((NavGraph g) => g.name == graphName);
			if (navGraph == null)
			{
				throw new ArgumentException("Could not find any graph with the name '" + graphName + "'");
			}
			return GraphMask.FromGraph(navGraph);
		}

		// Token: 0x04000115 RID: 277
		public int value;
	}
}
