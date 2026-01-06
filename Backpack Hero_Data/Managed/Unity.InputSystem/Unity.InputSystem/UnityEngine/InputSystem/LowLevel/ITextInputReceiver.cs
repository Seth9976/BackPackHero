using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C8 RID: 200
	public interface ITextInputReceiver
	{
		// Token: 0x06000CD4 RID: 3284
		void OnTextInput(char character);

		// Token: 0x06000CD5 RID: 3285
		void OnIMECompositionChanged(IMECompositionString compositionString);
	}
}
