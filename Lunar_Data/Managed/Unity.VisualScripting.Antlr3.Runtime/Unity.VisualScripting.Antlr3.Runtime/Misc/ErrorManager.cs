using System;
using System.Diagnostics;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime.Misc
{
	// Token: 0x02000008 RID: 8
	public class ErrorManager
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002730 File Offset: 0x00001730
		public static void InternalError(object error, Exception e)
		{
			StackFrame lastNonErrorManagerCodeLocation = ErrorManager.GetLastNonErrorManagerCodeLocation(e);
			string text = string.Concat(new object[] { "Exception ", e, "@", lastNonErrorManagerCodeLocation, ": ", error });
			ErrorManager.Error(text);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000277C File Offset: 0x0000177C
		public static void InternalError(object error)
		{
			StackFrame lastNonErrorManagerCodeLocation = ErrorManager.GetLastNonErrorManagerCodeLocation(new Exception());
			string text = lastNonErrorManagerCodeLocation + ": " + error;
			ErrorManager.Error(text);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027A8 File Offset: 0x000017A8
		private static StackFrame GetLastNonErrorManagerCodeLocation(Exception e)
		{
			StackTrace stackTrace = new StackTrace(e);
			int i;
			for (i = 0; i < stackTrace.FrameCount; i++)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				if (frame.ToString().IndexOf("ErrorManager") < 0)
				{
					break;
				}
			}
			return stackTrace.GetFrame(i);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027F4 File Offset: 0x000017F4
		public static void Error(object arg)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("internal error: {0} ", arg);
		}
	}
}
