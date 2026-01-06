using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000055 RID: 85
	public static class InputExtensions
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x0002D59B File Offset: 0x0002B79B
		public static bool IsInProgress(this InputActionPhase phase)
		{
			return phase == InputActionPhase.Started || phase == InputActionPhase.Performed;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002D5A7 File Offset: 0x0002B7A7
		public static bool IsEndedOrCanceled(this TouchPhase phase)
		{
			return phase == TouchPhase.Canceled || phase == TouchPhase.Ended;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002D5B3 File Offset: 0x0002B7B3
		public static bool IsActive(this TouchPhase phase)
		{
			return phase - TouchPhase.Began <= 1 || phase == TouchPhase.Stationary;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002D5C2 File Offset: 0x0002B7C2
		public static bool IsModifierKey(this Key key)
		{
			return key - Key.LeftShift <= 7;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002D5CE File Offset: 0x0002B7CE
		public static bool IsTextInputKey(this Key key)
		{
			return key > Key.Tab && key - Key.LeftShift > 26 && key - Key.F1 > 17;
		}
	}
}
