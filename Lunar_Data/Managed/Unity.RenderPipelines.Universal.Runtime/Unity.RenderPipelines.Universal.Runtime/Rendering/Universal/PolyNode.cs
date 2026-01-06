using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000009 RID: 9
	internal class PolyNode
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002C2C File Offset: 0x00000E2C
		private bool IsHoleNode()
		{
			bool flag = true;
			for (PolyNode polyNode = this.m_Parent; polyNode != null; polyNode = polyNode.m_Parent)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002C54 File Offset: 0x00000E54
		public int ChildCount
		{
			get
			{
				return this.m_Childs.Count;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C61 File Offset: 0x00000E61
		public List<IntPoint> Contour
		{
			get
			{
				return this.m_polygon;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C6C File Offset: 0x00000E6C
		internal void AddChild(PolyNode Child)
		{
			int count = this.m_Childs.Count;
			this.m_Childs.Add(Child);
			Child.m_Parent = this;
			Child.m_Index = count;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C9F File Offset: 0x00000E9F
		public PolyNode GetNext()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return this.GetNextSiblingUp();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CC4 File Offset: 0x00000EC4
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D19 File Offset: 0x00000F19
		public List<PolyNode> Childs
		{
			get
			{
				return this.m_Childs;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002D21 File Offset: 0x00000F21
		public PolyNode Parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D29 File Offset: 0x00000F29
		public bool IsHole
		{
			get
			{
				return this.IsHoleNode();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D31 File Offset: 0x00000F31
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002D39 File Offset: 0x00000F39
		public bool IsOpen { get; set; }

		// Token: 0x0400001A RID: 26
		internal PolyNode m_Parent;

		// Token: 0x0400001B RID: 27
		internal List<IntPoint> m_polygon = new List<IntPoint>();

		// Token: 0x0400001C RID: 28
		internal int m_Index;

		// Token: 0x0400001D RID: 29
		internal JoinType m_jointype;

		// Token: 0x0400001E RID: 30
		internal EndType m_endtype;

		// Token: 0x0400001F RID: 31
		internal List<PolyNode> m_Childs = new List<PolyNode>();
	}
}
