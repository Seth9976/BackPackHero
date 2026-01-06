using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004C RID: 76
	public static class MouseCaptureController
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00008850 File Offset: 0x00006A50
		public static bool IsMouseCaptured()
		{
			bool flag = !MouseCaptureController.m_IsMouseCapturedWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.IsMouseCaptured() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_IsMouseCapturedWarningEmitted = true;
			}
			return false;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008884 File Offset: 0x00006A84
		public static bool HasMouseCapture(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			return visualElement.HasPointerCapture(PointerId.mousePointerId);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000088A8 File Offset: 0x00006AA8
		public static void CaptureMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.CapturePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000088E4 File Offset: 0x00006AE4
		public static void ReleaseMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.ReleasePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008920 File Offset: 0x00006B20
		public static void ReleaseMouse()
		{
			bool flag = !MouseCaptureController.m_ReleaseMouseWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.ReleaseMouse() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_ReleaseMouseWarningEmitted = true;
			}
		}

		// Token: 0x040000D4 RID: 212
		private static bool m_IsMouseCapturedWarningEmitted;

		// Token: 0x040000D5 RID: 213
		private static bool m_ReleaseMouseWarningEmitted;
	}
}
