using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000294 RID: 660
	public enum SpeechError
	{
		// Token: 0x04000935 RID: 2357
		NoError,
		// Token: 0x04000936 RID: 2358
		TopicLanguageNotSupported,
		// Token: 0x04000937 RID: 2359
		GrammarLanguageMismatch,
		// Token: 0x04000938 RID: 2360
		GrammarCompilationFailure,
		// Token: 0x04000939 RID: 2361
		AudioQualityFailure,
		// Token: 0x0400093A RID: 2362
		PauseLimitExceeded,
		// Token: 0x0400093B RID: 2363
		TimeoutExceeded,
		// Token: 0x0400093C RID: 2364
		NetworkFailure,
		// Token: 0x0400093D RID: 2365
		MicrophoneUnavailable,
		// Token: 0x0400093E RID: 2366
		UnknownError
	}
}
