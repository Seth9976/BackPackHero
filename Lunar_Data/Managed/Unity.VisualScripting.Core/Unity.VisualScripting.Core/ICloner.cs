using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000D RID: 13
	public interface ICloner
	{
		// Token: 0x0600004A RID: 74
		bool Handles(Type type);

		// Token: 0x0600004B RID: 75
		object ConstructClone(Type type, object original);

		// Token: 0x0600004C RID: 76
		void BeforeClone(Type type, object original);

		// Token: 0x0600004D RID: 77
		void FillClone(Type type, ref object clone, object original, CloningContext context);

		// Token: 0x0600004E RID: 78
		void AfterClone(Type type, object clone);
	}
}
