using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000055 RID: 85
	public static class InputExtensions
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0002D5D7 File Offset: 0x0002B7D7
		public static bool IsInProgress(this InputActionPhase phase)
		{
			return phase == InputActionPhase.Started || phase == InputActionPhase.Performed;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002D5E3 File Offset: 0x0002B7E3
		public static bool IsEndedOrCanceled(this TouchPhase phase)
		{
			return phase == TouchPhase.Canceled || phase == TouchPhase.Ended;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002D5EF File Offset: 0x0002B7EF
		public static bool IsActive(this TouchPhase phase)
		{
			return phase - TouchPhase.Began <= 1 || phase == TouchPhase.Stationary;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002D5FE File Offset: 0x0002B7FE
		public static bool IsModifierKey(this Key key)
		{
			return key - Key.LeftShift <= 7;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002D60A File Offset: 0x0002B80A
		public static bool IsTextInputKey(this Key key)
		{
			return key > Key.Tab && key - Key.LeftShift > 26 && key - Key.F1 > 17;
		}
	}
}
