using System;
using System.Collections.Generic;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000299 RID: 665
	public sealed class KeywordRecognizer : PhraseRecognizer
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0002D9C2 File Offset: 0x0002BBC2
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0002D9CA File Offset: 0x0002BBCA
		public IEnumerable<string> Keywords { get; private set; }

		// Token: 0x06001C7B RID: 7291 RVA: 0x0002D9D3 File Offset: 0x0002BBD3
		public KeywordRecognizer(string[] keywords)
			: this(keywords, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0002D9E0 File Offset: 0x0002BBE0
		public KeywordRecognizer(string[] keywords, ConfidenceLevel minimumConfidence)
		{
			bool flag = keywords == null;
			if (flag)
			{
				throw new ArgumentNullException("keywords");
			}
			bool flag2 = keywords.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("At least one keyword must be specified.", "keywords");
			}
			int num = keywords.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag3 = keywords[i] == null;
				if (flag3)
				{
					throw new ArgumentNullException(string.Format("Keyword at index {0} is null.", i));
				}
			}
			this.Keywords = keywords;
			this.m_Recognizer = PhraseRecognizer.CreateFromKeywords(this, keywords, minimumConfidence);
		}
	}
}
