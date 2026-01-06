using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSampleProviderExtensions.bindings.h")]
	[StaticAccessor("AudioSampleProviderExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class AudioSampleProviderExtensionsInternal
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static float GetSpeed(this AudioSampleProvider provider)
		{
			return AudioSampleProviderExtensionsInternal.InternalGetAudioSampleProviderSpeed(provider.id);
		}

		// Token: 0x06000219 RID: 537
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern float InternalGetAudioSampleProviderSpeed(uint providerId);
	}
}
