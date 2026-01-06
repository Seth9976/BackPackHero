using System;
using System.Collections.Generic;

namespace Pathfinding.ClipperLib
{
	// Token: 0x02000003 RID: 3
	public class PolyTree : PolyNode
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000214C File Offset: 0x0000034C
		~PolyTree()
		{
			this.Clear();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002188 File Offset: 0x00000388
		public void Clear()
		{
			for (int i = 0; i < this.m_AllPolys.Count; i++)
			{
				this.m_AllPolys[i] = null;
			}
			this.m_AllPolys.Clear();
			this.m_Childs.Clear();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021D4 File Offset: 0x000003D4
		public PolyNode GetFirst()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021F8 File Offset: 0x000003F8
		public int Total
		{
			get
			{
				return this.m_AllPolys.Count;
			}
		}

		// Token: 0x04000003 RID: 3
		internal List<PolyNode> m_AllPolys = new List<PolyNode>();
	}
}
