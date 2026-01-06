using System;
using UnityEngine.Scripting;

namespace UnityEngine.Serialization
{
	// Token: 0x020002CC RID: 716
	[RequiredByNativeCode]
	[AttributeUsage(256, AllowMultiple = true, Inherited = false)]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x000305BC File Offset: 0x0002E7BC
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x000305D0 File Offset: 0x0002E7D0
		public string oldName
		{
			get
			{
				return this.m_oldName;
			}
		}

		// Token: 0x040009AE RID: 2478
		private string m_oldName;
	}
}
