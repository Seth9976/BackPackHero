using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009B RID: 155
	public interface IBitArray
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060004C7 RID: 1223
		uint capacity { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060004C8 RID: 1224
		bool allFalse { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004C9 RID: 1225
		bool allTrue { get; }

		// Token: 0x17000099 RID: 153
		bool this[uint index] { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004CC RID: 1228
		string humanizedData { get; }

		// Token: 0x060004CD RID: 1229
		IBitArray BitAnd(IBitArray other);

		// Token: 0x060004CE RID: 1230
		IBitArray BitOr(IBitArray other);

		// Token: 0x060004CF RID: 1231
		IBitArray BitNot();
	}
}
