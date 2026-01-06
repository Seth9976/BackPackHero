using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200022B RID: 555
	[RequiredByNativeCode]
	[StructLayout(0)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x060017E5 RID: 6117 RVA: 0x00026C34 File Offset: 0x00024E34
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x0400082E RID: 2094
		internal float m_Seconds;
	}
}
