using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000007 RID: 7
	public interface ITreeVisitorAction
	{
		// Token: 0x0600002F RID: 47
		object Pre(object t);

		// Token: 0x06000030 RID: 48
		object Post(object t);
	}
}
