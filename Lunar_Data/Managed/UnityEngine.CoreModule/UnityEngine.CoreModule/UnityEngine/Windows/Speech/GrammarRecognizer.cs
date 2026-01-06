using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200029A RID: 666
	public sealed class GrammarRecognizer : PhraseRecognizer
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0002DA71 File Offset: 0x0002BC71
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0002DA79 File Offset: 0x0002BC79
		public string GrammarFilePath { get; private set; }

		// Token: 0x06001C7F RID: 7295 RVA: 0x0002DA82 File Offset: 0x0002BC82
		public GrammarRecognizer(string grammarFilePath)
			: this(grammarFilePath, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0002DA90 File Offset: 0x0002BC90
		public GrammarRecognizer(string grammarFilePath, ConfidenceLevel minimumConfidence)
		{
			bool flag = grammarFilePath == null;
			if (flag)
			{
				throw new ArgumentNullException("grammarFilePath");
			}
			bool flag2 = grammarFilePath.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Grammar file path cannot be empty.");
			}
			this.GrammarFilePath = grammarFilePath;
			this.m_Recognizer = PhraseRecognizer.CreateFromGrammarFile(this, grammarFilePath, minimumConfidence);
		}
	}
}
