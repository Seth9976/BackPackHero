using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoHumanBone")]
	public struct HumanBone
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00003C30 File Offset: 0x00001E30
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00003C48 File Offset: 0x00001E48
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00003C54 File Offset: 0x00001E54
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00003C6C File Offset: 0x00001E6C
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x04000112 RID: 274
		private string m_BoneName;

		// Token: 0x04000113 RID: 275
		private string m_HumanName;

		// Token: 0x04000114 RID: 276
		[NativeName("m_Limit")]
		public HumanLimit limit;
	}
}
