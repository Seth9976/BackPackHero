using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000056 RID: 86
	public interface IGraphEventHandler<TArgs>
	{
		// Token: 0x06000280 RID: 640
		EventHook GetHook(GraphReference reference);

		// Token: 0x06000281 RID: 641
		void Trigger(GraphReference reference, TArgs args);

		// Token: 0x06000282 RID: 642
		bool IsListening(GraphPointer pointer);
	}
}
