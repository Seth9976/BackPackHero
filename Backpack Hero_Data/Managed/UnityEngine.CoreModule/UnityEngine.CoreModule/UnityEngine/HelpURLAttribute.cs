using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F5 RID: 501
	[AttributeUsage(4, AllowMultiple = false)]
	[UsedByNativeCode]
	public class HelpURLAttribute : Attribute
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x00023CA7 File Offset: 0x00021EA7
		public HelpURLAttribute(string url)
		{
			this.m_Url = url;
			this.m_DispatchingFieldName = "";
			this.m_Dispatcher = false;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00023CCA File Offset: 0x00021ECA
		internal HelpURLAttribute(string defaultURL, string dispatchingFieldName)
		{
			this.m_Url = defaultURL;
			this.m_DispatchingFieldName = dispatchingFieldName;
			this.m_Dispatcher = !string.IsNullOrEmpty(dispatchingFieldName);
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00023CF1 File Offset: 0x00021EF1
		public string URL
		{
			get
			{
				return this.m_Url;
			}
		}

		// Token: 0x040007D3 RID: 2003
		internal readonly string m_Url;

		// Token: 0x040007D4 RID: 2004
		internal readonly bool m_Dispatcher;

		// Token: 0x040007D5 RID: 2005
		internal readonly string m_DispatchingFieldName;
	}
}
