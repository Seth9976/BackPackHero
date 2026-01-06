using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000008 RID: 8
	internal class PolyTree : PolyNode
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002B78 File Offset: 0x00000D78
		public void Clear()
		{
			for (int i = 0; i < this.m_AllPolys.Count; i++)
			{
				this.m_AllPolys[i] = null;
			}
			this.m_AllPolys.Clear();
			this.m_Childs.Clear();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BBE File Offset: 0x00000DBE
		public PolyNode GetFirst()
		{
			if (this.m_Childs.Count > 0)
			{
				return this.m_Childs[0];
			}
			return null;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002BDC File Offset: 0x00000DDC
		public int Total
		{
			get
			{
				int num = this.m_AllPolys.Count;
				if (num > 0 && this.m_Childs[0] != this.m_AllPolys[0])
				{
					num--;
				}
				return num;
			}
		}

		// Token: 0x04000019 RID: 25
		internal List<PolyNode> m_AllPolys = new List<PolyNode>();
	}
}
