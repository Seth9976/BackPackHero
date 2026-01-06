using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200000A RID: 10
	public interface IToken
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000055 RID: 85
		// (set) Token: 0x06000056 RID: 86
		int Type { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000057 RID: 87
		// (set) Token: 0x06000058 RID: 88
		int Line { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000059 RID: 89
		// (set) Token: 0x0600005A RID: 90
		int CharPositionInLine { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91
		// (set) Token: 0x0600005C RID: 92
		int Channel { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93
		// (set) Token: 0x0600005E RID: 94
		int TokenIndex { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		string Text { get; set; }
	}
}
