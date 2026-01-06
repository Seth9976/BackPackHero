using System;
using System.Collections.Generic;

namespace Pathfinding.ClipperLib
{
	// Token: 0x02000004 RID: 4
	public class PolyNode
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002228 File Offset: 0x00000428
		private bool IsHoleNode()
		{
			bool flag = true;
			for (PolyNode polyNode = this.m_Parent; polyNode != null; polyNode = polyNode.m_Parent)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002258 File Offset: 0x00000458
		public int ChildCount
		{
			get
			{
				return this.m_Childs.Count;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002268 File Offset: 0x00000468
		public List<IntPoint> Contour
		{
			get
			{
				return this.m_polygon;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002270 File Offset: 0x00000470
		internal void AddChild(PolyNode Child)
		{
			int count = this.m_Childs.Count;
			this.m_Childs.Add(Child);
			Child.m_Parent = this;
			Child.m_Index = count;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A4 File Offset: 0x000004A4
		public PolyNode GetNext()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return this.GetNextSiblingUp();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D8 File Offset: 0x000004D8
		internal PolyNode GetNextSiblingUp()
		{
			if (this.m_Parent == null)
			{
				return null;
			}
			if (this.m_Index == this.m_Parent.m_Childs.Count - 1)
			{
				return this.m_Parent.GetNextSiblingUp();
			}
			return this.m_Parent.m_Childs[this.m_Index + 1];
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002334 File Offset: 0x00000534
		public List<PolyNode> Childs
		{
			get
			{
				return this.m_Childs;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000233C File Offset: 0x0000053C
		public PolyNode Parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002344 File Offset: 0x00000544
		public bool IsHole
		{
			get
			{
				return this.IsHoleNode();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002354 File Offset: 0x00000554
		public bool IsOpen { get; set; }

		// Token: 0x04000004 RID: 4
		internal PolyNode m_Parent;

		// Token: 0x04000005 RID: 5
		internal List<IntPoint> m_polygon = new List<IntPoint>();

		// Token: 0x04000006 RID: 6
		internal int m_Index;

		// Token: 0x04000007 RID: 7
		internal List<PolyNode> m_Childs = new List<PolyNode>();
	}
}
