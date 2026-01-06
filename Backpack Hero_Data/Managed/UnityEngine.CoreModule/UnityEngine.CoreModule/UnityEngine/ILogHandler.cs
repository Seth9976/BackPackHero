using System;

namespace UnityEngine
{
	// Token: 0x020001B9 RID: 441
	public interface ILogHandler
	{
		// Token: 0x0600134B RID: 4939
		void LogFormat(LogType logType, Object context, string format, params object[] args);

		// Token: 0x0600134C RID: 4940
		void LogException(Exception exception, Object context);
	}
}
