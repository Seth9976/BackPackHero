using System;

namespace Pathfinding
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public struct GraphMask
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A521 File Offset: 0x00008721
		public static GraphMask everything
		{
			get
			{
				return new GraphMask(-1);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A529 File Offset: 0x00008729
		public GraphMask(int value)
		{
			this.value = value;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A532 File Offset: 0x00008732
		public static implicit operator int(GraphMask mask)
		{
			return mask.value;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A53A File Offset: 0x0000873A
		public static implicit operator GraphMask(int mask)
		{
			return new GraphMask(mask);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A542 File Offset: 0x00008742
		public static GraphMask operator &(GraphMask lhs, GraphMask rhs)
		{
			return new GraphMask(lhs.value & rhs.value);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A556 File Offset: 0x00008756
		public static GraphMask operator |(GraphMask lhs, GraphMask rhs)
		{
			return new GraphMask(lhs.value | rhs.value);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A56A File Offset: 0x0000876A
		public static GraphMask operator ~(GraphMask lhs)
		{
			return new GraphMask(~lhs.value);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A578 File Offset: 0x00008778
		public bool Contains(int graphIndex)
		{
			return ((this.value >> graphIndex) & 1) != 0;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A58A File Offset: 0x0000878A
		public static GraphMask FromGraph(NavGraph graph)
		{
			return 1 << (int)graph.graphIndex;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A59C File Offset: 0x0000879C
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A5A9 File Offset: 0x000087A9
		public static GraphMask FromGraphIndex(uint graphIndex)
		{
			return new GraphMask(1 << (int)graphIndex);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public static GraphMask FromGraphName(string graphName)
		{
			NavGraph navGraph = AstarPath.active.data.FindGraph((NavGraph g) => g.name == graphName);
			if (navGraph == null)
			{
				throw new ArgumentException("Could not find any graph with the name '" + graphName + "'");
			}
			return GraphMask.FromGraph(navGraph);
		}

		// Token: 0x04000170 RID: 368
		public int value;
	}
}
