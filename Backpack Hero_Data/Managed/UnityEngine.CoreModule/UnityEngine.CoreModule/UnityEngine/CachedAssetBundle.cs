using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000EF RID: 239
	[UsedByNativeCode]
	public struct CachedAssetBundle
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00006F22 File Offset: 0x00005122
		public CachedAssetBundle(string name, Hash128 hash)
		{
			this.m_Name = name;
			this.m_Hash = hash;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00006F34 File Offset: 0x00005134
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00006F4C File Offset: 0x0000514C
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00006F58 File Offset: 0x00005158
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00006F70 File Offset: 0x00005170
		public Hash128 hash
		{
			get
			{
				return this.m_Hash;
			}
			set
			{
				this.m_Hash = value;
			}
		}

		// Token: 0x04000324 RID: 804
		private string m_Name;

		// Token: 0x04000325 RID: 805
		private Hash128 m_Hash;
	}
}
