using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000013 RID: 19
	[NotKeyable]
	[Serializable]
	internal class AudioClipProperties : PlayableBehaviour
	{
		// Token: 0x04000086 RID: 134
		[Range(0f, 1f)]
		public float volume = 1f;
	}
}
