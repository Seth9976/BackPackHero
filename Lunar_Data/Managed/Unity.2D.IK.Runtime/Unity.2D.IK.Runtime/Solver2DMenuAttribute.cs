using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000009 RID: 9
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class Solver2DMenuAttribute : Attribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000309A File Offset: 0x0000129A
		public string menuPath
		{
			get
			{
				return this.m_MenuPath;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030A2 File Offset: 0x000012A2
		public Solver2DMenuAttribute(string _menuPath)
		{
			this.m_MenuPath = _menuPath;
		}

		// Token: 0x04000026 RID: 38
		private string m_MenuPath;
	}
}
