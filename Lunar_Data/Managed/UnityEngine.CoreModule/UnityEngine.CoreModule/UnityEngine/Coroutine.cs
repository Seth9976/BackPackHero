using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FD RID: 509
	[NativeHeader("Runtime/Mono/Coroutine.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x06001699 RID: 5785 RVA: 0x00024124 File Offset: 0x00022324
		private Coroutine()
		{
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00024130 File Offset: 0x00022330
		~Coroutine()
		{
			Coroutine.ReleaseCoroutine(this.m_Ptr);
		}

		// Token: 0x0600169B RID: 5787
		[FreeFunction("Coroutine::CleanupCoroutineGC", true)]
		[MethodImpl(4096)]
		private static extern void ReleaseCoroutine(IntPtr ptr);

		// Token: 0x040007D9 RID: 2009
		internal IntPtr m_Ptr;
	}
}
