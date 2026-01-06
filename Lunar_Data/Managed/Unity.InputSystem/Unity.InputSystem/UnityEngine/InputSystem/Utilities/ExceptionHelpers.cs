using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012B RID: 299
	internal static class ExceptionHelpers
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x0004EC0C File Offset: 0x0004CE0C
		public static bool IsExceptionIndicatingBugInCode(this Exception exception)
		{
			return exception is NullReferenceException || exception is IndexOutOfRangeException || exception is ArgumentException;
		}
	}
}
