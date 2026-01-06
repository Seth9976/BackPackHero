using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000021 RID: 33
	public interface ITreeNodeStream : IIntStream
	{
		// Token: 0x0600019F RID: 415
		object Get(int i);

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A0 RID: 416
		object TreeSource { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A1 RID: 417
		ITokenStream TokenStream { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A2 RID: 418
		ITreeAdaptor TreeAdaptor { get; }

		// Token: 0x1700005E RID: 94
		// (set) Token: 0x060001A3 RID: 419
		bool HasUniqueNavigationNodes { set; }

		// Token: 0x060001A4 RID: 420
		object LT(int k);

		// Token: 0x060001A5 RID: 421
		string ToString(object start, object stop);

		// Token: 0x060001A6 RID: 422
		void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);
	}
}
