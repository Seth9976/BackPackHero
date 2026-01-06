using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200007E RID: 126
	public interface IGraphEventListener
	{
		// Token: 0x060003C2 RID: 962
		void StartListening(GraphStack stack);

		// Token: 0x060003C3 RID: 963
		void StopListening(GraphStack stack);

		// Token: 0x060003C4 RID: 964
		bool IsListening(GraphPointer pointer);
	}
}
