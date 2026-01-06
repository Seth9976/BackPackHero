using System;
using System.Collections;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200000F RID: 15
	public interface ITree
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A4 RID: 164
		int ChildCount { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		ITree Parent { get; set; }

		// Token: 0x060000A7 RID: 167
		bool HasAncestor(int ttype);

		// Token: 0x060000A8 RID: 168
		ITree GetAncestor(int ttype);

		// Token: 0x060000A9 RID: 169
		IList GetAncestors();

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AA RID: 170
		// (set) Token: 0x060000AB RID: 171
		int ChildIndex { get; set; }

		// Token: 0x060000AC RID: 172
		void FreshenParentAndChildIndexes();

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AD RID: 173
		bool IsNil { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AE RID: 174
		int Type { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AF RID: 175
		string Text { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B0 RID: 176
		int Line { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B1 RID: 177
		int CharPositionInLine { get; }

		// Token: 0x060000B2 RID: 178
		ITree GetChild(int i);

		// Token: 0x060000B3 RID: 179
		void AddChild(ITree t);

		// Token: 0x060000B4 RID: 180
		void SetChild(int i, ITree t);

		// Token: 0x060000B5 RID: 181
		object DeleteChild(int i);

		// Token: 0x060000B6 RID: 182
		void ReplaceChildren(int startChildIndex, int stopChildIndex, object t);

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B7 RID: 183
		// (set) Token: 0x060000B8 RID: 184
		int TokenStartIndex { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B9 RID: 185
		// (set) Token: 0x060000BA RID: 186
		int TokenStopIndex { get; set; }

		// Token: 0x060000BB RID: 187
		ITree DupNode();

		// Token: 0x060000BC RID: 188
		string ToStringTree();

		// Token: 0x060000BD RID: 189
		string ToString();
	}
}
