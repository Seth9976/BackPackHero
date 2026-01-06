using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	[ExcludeFromObjectFactory]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	public class RuntimeAnimatorController : Object
	{
		// Token: 0x06000289 RID: 649 RVA: 0x000039FB File Offset: 0x00001BFB
		protected RuntimeAnimatorController()
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028A RID: 650
		public extern AnimationClip[] animationClips
		{
			[MethodImpl(4096)]
			get;
		}
	}
}
