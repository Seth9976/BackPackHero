using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000015 RID: 21
	public interface IIntStream
	{
		// Token: 0x06000109 RID: 265
		void Consume();

		// Token: 0x0600010A RID: 266
		int LA(int i);

		// Token: 0x0600010B RID: 267
		int Mark();

		// Token: 0x0600010C RID: 268
		int Index();

		// Token: 0x0600010D RID: 269
		void Rewind(int marker);

		// Token: 0x0600010E RID: 270
		void Rewind();

		// Token: 0x0600010F RID: 271
		void Release(int marker);

		// Token: 0x06000110 RID: 272
		void Seek(int index);

		// Token: 0x06000111 RID: 273
		[Obsolete("Please use property Count instead.")]
		int Size();

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000112 RID: 274
		int Count { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000113 RID: 275
		string SourceName { get; }
	}
}
