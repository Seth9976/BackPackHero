using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000443 RID: 1091
	public enum PlayState
	{
		// Token: 0x04000E1F RID: 3615
		Paused,
		// Token: 0x04000E20 RID: 3616
		Playing,
		// Token: 0x04000E21 RID: 3617
		[Obsolete("Delayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		Delayed
	}
}
