using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012B RID: 299
	internal static class ExceptionHelpers
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x0004EDB4 File Offset: 0x0004CFB4
		public static bool IsExceptionIndicatingBugInCode(this Exception exception)
		{
			return exception is NullReferenceException || exception is IndexOutOfRangeException || exception is ArgumentException;
		}
	}
}
