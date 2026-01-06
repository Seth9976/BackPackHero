using System;

namespace UnityEngine
{
	// Token: 0x020001B8 RID: 440
	public interface ILogger : ILogHandler
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001337 RID: 4919
		// (set) Token: 0x06001338 RID: 4920
		ILogHandler logHandler { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001339 RID: 4921
		// (set) Token: 0x0600133A RID: 4922
		bool logEnabled { get; set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600133B RID: 4923
		// (set) Token: 0x0600133C RID: 4924
		LogType filterLogType { get; set; }

		// Token: 0x0600133D RID: 4925
		bool IsLogTypeAllowed(LogType logType);

		// Token: 0x0600133E RID: 4926
		void Log(LogType logType, object message);

		// Token: 0x0600133F RID: 4927
		void Log(LogType logType, object message, Object context);

		// Token: 0x06001340 RID: 4928
		void Log(LogType logType, string tag, object message);

		// Token: 0x06001341 RID: 4929
		void Log(LogType logType, string tag, object message, Object context);

		// Token: 0x06001342 RID: 4930
		void Log(object message);

		// Token: 0x06001343 RID: 4931
		void Log(string tag, object message);

		// Token: 0x06001344 RID: 4932
		void Log(string tag, object message, Object context);

		// Token: 0x06001345 RID: 4933
		void LogWarning(string tag, object message);

		// Token: 0x06001346 RID: 4934
		void LogWarning(string tag, object message, Object context);

		// Token: 0x06001347 RID: 4935
		void LogError(string tag, object message);

		// Token: 0x06001348 RID: 4936
		void LogError(string tag, object message, Object context);

		// Token: 0x06001349 RID: 4937
		void LogFormat(LogType logType, string format, params object[] args);

		// Token: 0x0600134A RID: 4938
		void LogException(Exception exception);
	}
}
