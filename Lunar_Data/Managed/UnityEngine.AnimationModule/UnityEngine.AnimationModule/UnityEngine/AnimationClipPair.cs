using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[Obsolete("This class is not used anymore. See AnimatorOverrideController.GetOverrides() and AnimatorOverrideController.ApplyOverrides()")]
	[Serializable]
	[StructLayout(0)]
	public class AnimationClipPair
	{
		// Token: 0x0400006C RID: 108
		public AnimationClip originalClip;

		// Token: 0x0400006D RID: 109
		public AnimationClip overrideClip;
	}
}
