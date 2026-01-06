using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000298 RID: 664
	public struct PhraseRecognizedEventArgs
	{
		// Token: 0x06001C78 RID: 7288 RVA: 0x0002D99A File Offset: 0x0002BB9A
		internal PhraseRecognizedEventArgs(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, DateTime phraseStartTime, TimeSpan phraseDuration)
		{
			this.text = text;
			this.confidence = confidence;
			this.semanticMeanings = semanticMeanings;
			this.phraseStartTime = phraseStartTime;
			this.phraseDuration = phraseDuration;
		}

		// Token: 0x0400094E RID: 2382
		public readonly ConfidenceLevel confidence;

		// Token: 0x0400094F RID: 2383
		public readonly SemanticMeaning[] semanticMeanings;

		// Token: 0x04000950 RID: 2384
		public readonly string text;

		// Token: 0x04000951 RID: 2385
		public readonly DateTime phraseStartTime;

		// Token: 0x04000952 RID: 2386
		public readonly TimeSpan phraseDuration;
	}
}
