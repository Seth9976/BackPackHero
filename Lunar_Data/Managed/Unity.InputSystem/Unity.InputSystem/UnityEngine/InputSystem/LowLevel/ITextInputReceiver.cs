using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C8 RID: 200
	public interface ITextInputReceiver
	{
		// Token: 0x06000CD1 RID: 3281
		void OnTextInput(char character);

		// Token: 0x06000CD2 RID: 3282
		void OnIMECompositionChanged(IMECompositionString compositionString);
	}
}
