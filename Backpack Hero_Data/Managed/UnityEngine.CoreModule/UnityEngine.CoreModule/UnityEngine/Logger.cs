using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x020001BA RID: 442
	public class Logger : ILogger, ILogHandler
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x00008C2F File Offset: 0x00006E2F
		private Logger()
		{
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0001ACEA File Offset: 0x00018EEA
		public Logger(ILogHandler logHandler)
		{
			this.logHandler = logHandler;
			this.logEnabled = true;
			this.filterLogType = LogType.Log;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0001AD0C File Offset: 0x00018F0C
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x0001AD14 File Offset: 0x00018F14
		public ILogHandler logHandler { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0001AD1D File Offset: 0x00018F1D
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x0001AD25 File Offset: 0x00018F25
		public bool logEnabled { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0001AD2E File Offset: 0x00018F2E
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x0001AD36 File Offset: 0x00018F36
		public LogType filterLogType { get; set; }

		// Token: 0x06001355 RID: 4949 RVA: 0x0001AD40 File Offset: 0x00018F40
		public bool IsLogTypeAllowed(LogType logType)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				bool flag = logType == LogType.Exception;
				if (flag)
				{
					return true;
				}
				bool flag2 = this.filterLogType != LogType.Exception;
				if (flag2)
				{
					return logType <= this.filterLogType;
				}
			}
			return false;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0001AD8C File Offset: 0x00018F8C
		private static string GetString(object message)
		{
			bool flag = message == null;
			string text;
			if (flag)
			{
				text = "Null";
			}
			else
			{
				IFormattable formattable = message as IFormattable;
				bool flag2 = formattable != null;
				if (flag2)
				{
					text = formattable.ToString(null, CultureInfo.InvariantCulture);
				}
				else
				{
					text = message.ToString();
				}
			}
			return text;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		public void Log(LogType logType, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0001AE14 File Offset: 0x00019014
		public void Log(LogType logType, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0001AE50 File Offset: 0x00019050
		public void Log(LogType logType, string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0001AE90 File Offset: 0x00019090
		public void Log(LogType logType, string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0001AED0 File Offset: 0x000190D0
		public void Log(object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0001AF0C File Offset: 0x0001910C
		public void Log(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0001AF4C File Offset: 0x0001914C
		public void Log(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0001AF8C File Offset: 0x0001918C
		public void LogWarning(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0001AFCC File Offset: 0x000191CC
		public void LogWarning(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0001B00C File Offset: 0x0001920C
		public void LogError(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0001B04C File Offset: 0x0001924C
		public void LogError(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0001B08C File Offset: 0x0001928C
		public void LogException(Exception exception)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, null);
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0001B0B4 File Offset: 0x000192B4
		public void LogException(Exception exception, Object context)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, context);
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0001B0DC File Offset: 0x000192DC
		public void LogFormat(LogType logType, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, format, args);
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0001B108 File Offset: 0x00019308
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, format, args);
			}
		}

		// Token: 0x0400072F RID: 1839
		private const string kNoTagFormat = "{0}";

		// Token: 0x04000730 RID: 1840
		private const string kTagFormat = "{0}: {1}";
	}
}
