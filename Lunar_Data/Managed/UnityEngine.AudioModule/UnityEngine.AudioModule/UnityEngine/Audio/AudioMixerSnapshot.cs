using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/Audio/Public/AudioMixerSnapshot.h")]
	public class AudioMixerSnapshot : Object, ISubAssetNotDuplicatable
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00003481 File Offset: 0x00001681
		internal AudioMixerSnapshot()
		{
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001BD RID: 445
		[NativeProperty]
		public extern AudioMixer audioMixer
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000362C File Offset: 0x0000182C
		public void TransitionTo(float timeToReach)
		{
			this.audioMixer.TransitionToSnapshot(this, timeToReach);
		}
	}
}
