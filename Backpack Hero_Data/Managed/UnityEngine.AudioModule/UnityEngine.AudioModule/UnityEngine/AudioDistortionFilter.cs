using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioDistortionFilter : Behaviour
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EF RID: 239
		// (set) Token: 0x060000F0 RID: 240
		public extern float distortionLevel
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
