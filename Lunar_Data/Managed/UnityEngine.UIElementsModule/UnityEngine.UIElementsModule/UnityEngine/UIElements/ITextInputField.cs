using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000182 RID: 386
	internal interface ITextInputField : IEventHandler, ITextElement
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000BFC RID: 3068
		bool hasFocus { get; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000BFD RID: 3069
		bool doubleClickSelectsWord { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000BFE RID: 3070
		bool tripleClickSelectsLine { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000BFF RID: 3071
		bool isReadOnly { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C00 RID: 3072
		bool isDelayed { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000C01 RID: 3073
		bool isPasswordField { get; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000C02 RID: 3074
		TextEditorEngine editorEngine { get; }

		// Token: 0x06000C03 RID: 3075
		void SyncTextEngine();

		// Token: 0x06000C04 RID: 3076
		bool AcceptCharacter(char c);

		// Token: 0x06000C05 RID: 3077
		string CullString(string s);

		// Token: 0x06000C06 RID: 3078
		void UpdateText(string value);

		// Token: 0x06000C07 RID: 3079
		void UpdateValueFromText();
	}
}
