using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000016 RID: 22
	public interface INotifiedCollectionItem
	{
		// Token: 0x0600008F RID: 143
		void BeforeAdd();

		// Token: 0x06000090 RID: 144
		void AfterAdd();

		// Token: 0x06000091 RID: 145
		void BeforeRemove();

		// Token: 0x06000092 RID: 146
		void AfterRemove();
	}
}
