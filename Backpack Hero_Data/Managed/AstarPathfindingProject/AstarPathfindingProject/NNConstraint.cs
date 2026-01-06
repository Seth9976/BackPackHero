using System;

namespace Pathfinding
{
	// Token: 0x02000012 RID: 18
	public class NNConstraint
	{
		// Token: 0x06000196 RID: 406 RVA: 0x000085E7 File Offset: 0x000067E7
		public virtual bool SuitableGraph(int graphIndex, NavGraph graph)
		{
			return this.graphMask.Contains(graphIndex);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000085F8 File Offset: 0x000067F8
		public virtual bool Suitable(GraphNode node)
		{
			return (!this.constrainWalkability || node.Walkable == this.walkable) && (!this.constrainArea || this.area < 0 || (ulong)node.Area == (ulong)((long)this.area)) && (!this.constrainTags || ((this.tags >> (int)node.Tag) & 1) != 0);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000865F File Offset: 0x0000685F
		public static NNConstraint Default
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008666 File Offset: 0x00006866
		public static NNConstraint None
		{
			get
			{
				return new NNConstraint
				{
					constrainWalkability = false,
					constrainArea = false,
					constrainTags = false,
					constrainDistance = false,
					graphMask = -1
				};
			}
		}

		// Token: 0x040000E7 RID: 231
		public GraphMask graphMask = -1;

		// Token: 0x040000E8 RID: 232
		public bool constrainArea;

		// Token: 0x040000E9 RID: 233
		public int area = -1;

		// Token: 0x040000EA RID: 234
		public bool constrainWalkability = true;

		// Token: 0x040000EB RID: 235
		public bool walkable = true;

		// Token: 0x040000EC RID: 236
		public bool distanceXZ;

		// Token: 0x040000ED RID: 237
		public bool constrainTags = true;

		// Token: 0x040000EE RID: 238
		public int tags = -1;

		// Token: 0x040000EF RID: 239
		public bool constrainDistance = true;
	}
}
