using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E7 RID: 487
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class ResourceRequest : AsyncOperation
	{
		// Token: 0x0600160C RID: 5644 RVA: 0x000234C4 File Offset: 0x000216C4
		protected virtual Object GetResult()
		{
			return Resources.Load(this.m_Path, this.m_Type);
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000234E8 File Offset: 0x000216E8
		public Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x040007C2 RID: 1986
		internal string m_Path;

		// Token: 0x040007C3 RID: 1987
		internal Type m_Type;
	}
}
