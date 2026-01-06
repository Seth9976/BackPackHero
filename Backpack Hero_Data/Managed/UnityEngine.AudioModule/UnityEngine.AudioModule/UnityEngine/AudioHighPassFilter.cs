using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioHighPassFilter : Behaviour
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EA RID: 234
		// (set) Token: 0x060000EB RID: 235
		public extern float cutoffFrequency
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EC RID: 236
		// (set) Token: 0x060000ED RID: 237
		public extern float highpassResonanceQ
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
