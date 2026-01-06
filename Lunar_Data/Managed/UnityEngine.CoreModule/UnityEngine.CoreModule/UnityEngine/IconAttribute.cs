using System;
using System.Diagnostics;

namespace UnityEngine
{
	// Token: 0x020001E6 RID: 486
	[AttributeUsage(4, Inherited = true, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class IconAttribute : Attribute
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x000234A9 File Offset: 0x000216A9
		public string path
		{
			get
			{
				return this.m_IconPath;
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00002059 File Offset: 0x00000259
		private IconAttribute()
		{
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000234B1 File Offset: 0x000216B1
		public IconAttribute(string path)
		{
			this.m_IconPath = path;
		}

		// Token: 0x040007C1 RID: 1985
		private string m_IconPath;
	}
}
