using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000211 RID: 529
	[AttributeUsage(64, AllowMultiple = false)]
	[RequiredByNativeCode]
	public class RuntimeInitializeOnLoadMethodAttribute : PreserveAttribute
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x000255C8 File Offset: 0x000237C8
		public RuntimeInitializeOnLoadMethodAttribute()
		{
			this.loadType = RuntimeInitializeLoadType.AfterSceneLoad;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000255DA File Offset: 0x000237DA
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
		{
			this.loadType = loadType;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x000255EC File Offset: 0x000237EC
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x00025604 File Offset: 0x00023804
		public RuntimeInitializeLoadType loadType
		{
			get
			{
				return this.m_LoadType;
			}
			private set
			{
				this.m_LoadType = value;
			}
		}

		// Token: 0x040007FD RID: 2045
		private RuntimeInitializeLoadType m_LoadType;
	}
}
