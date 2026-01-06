using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000420 RID: 1056
	[UsedByNativeCode]
	public struct PlatformKeywordSet
	{
		// Token: 0x060024D6 RID: 9430 RVA: 0x0003E510 File Offset: 0x0003C710
		private ulong ComputeKeywordMask(BuiltinShaderDefine define)
		{
			return (ulong)(1L << (int)((define % (BuiltinShaderDefine)64) & BuiltinShaderDefine.SHADER_API_GLES30));
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0003E52C File Offset: 0x0003C72C
		public bool IsEnabled(BuiltinShaderDefine define)
		{
			return (this.m_Bits & this.ComputeKeywordMask(define)) > 0UL;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0003E550 File Offset: 0x0003C750
		public void Enable(BuiltinShaderDefine define)
		{
			this.m_Bits |= this.ComputeKeywordMask(define);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0003E567 File Offset: 0x0003C767
		public void Disable(BuiltinShaderDefine define)
		{
			this.m_Bits &= ~this.ComputeKeywordMask(define);
		}

		// Token: 0x04000DA3 RID: 3491
		private const int k_SizeInBits = 64;

		// Token: 0x04000DA4 RID: 3492
		internal ulong m_Bits;
	}
}
