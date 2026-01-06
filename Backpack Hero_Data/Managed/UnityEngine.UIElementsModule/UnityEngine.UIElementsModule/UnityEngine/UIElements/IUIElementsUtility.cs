using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C0 RID: 192
	internal interface IUIElementsUtility
	{
		// Token: 0x06000664 RID: 1636
		bool TakeCapture();

		// Token: 0x06000665 RID: 1637
		bool ReleaseCapture();

		// Token: 0x06000666 RID: 1638
		bool ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled);

		// Token: 0x06000667 RID: 1639
		bool CleanupRoots();

		// Token: 0x06000668 RID: 1640
		bool EndContainerGUIFromException(Exception exception);

		// Token: 0x06000669 RID: 1641
		bool MakeCurrentIMGUIContainerDirty();

		// Token: 0x0600066A RID: 1642
		void UpdateSchedulers();

		// Token: 0x0600066B RID: 1643
		void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback);
	}
}
