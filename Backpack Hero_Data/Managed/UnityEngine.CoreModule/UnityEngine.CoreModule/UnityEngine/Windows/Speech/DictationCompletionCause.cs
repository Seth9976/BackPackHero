using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000296 RID: 662
	public enum DictationCompletionCause
	{
		// Token: 0x04000944 RID: 2372
		Complete,
		// Token: 0x04000945 RID: 2373
		AudioQualityFailure,
		// Token: 0x04000946 RID: 2374
		Canceled,
		// Token: 0x04000947 RID: 2375
		TimeoutExceeded,
		// Token: 0x04000948 RID: 2376
		PauseLimitExceeded,
		// Token: 0x04000949 RID: 2377
		NetworkFailure,
		// Token: 0x0400094A RID: 2378
		MicrophoneUnavailable,
		// Token: 0x0400094B RID: 2379
		UnknownError
	}
}
