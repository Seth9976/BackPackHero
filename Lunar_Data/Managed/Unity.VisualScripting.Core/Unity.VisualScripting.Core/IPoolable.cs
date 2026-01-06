using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000C2 RID: 194
	public interface IPoolable
	{
		// Token: 0x060004BE RID: 1214
		void New();

		// Token: 0x060004BF RID: 1215
		void Free();
	}
}
