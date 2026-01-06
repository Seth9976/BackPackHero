using System;
using System.Diagnostics;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000030 RID: 48
	internal static class CoreLogger
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00002A3C File Offset: 0x00000C3C
		public static void Log(object message)
		{
			Debug.unityLogger.Log("[ServicesCore]", message);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002A4E File Offset: 0x00000C4E
		public static void LogWarning(object message)
		{
			Debug.unityLogger.LogWarning("[ServicesCore]", message);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002A60 File Offset: 0x00000C60
		public static void LogError(object message)
		{
			Debug.unityLogger.LogError("[ServicesCore]", message);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002A72 File Offset: 0x00000C72
		public static void LogException(Exception exception)
		{
			Debug.unityLogger.Log(LogType.Exception, "[ServicesCore]", exception);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002A85 File Offset: 0x00000C85
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.unityLogger.Log(LogType.Assert, "[ServicesCore]", message);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002A98 File Offset: 0x00000C98
		[Conditional("ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING")]
		public static void LogVerbose(object message)
		{
			Debug.unityLogger.Log("[ServicesCore]", message);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002AAA File Offset: 0x00000CAA
		[Conditional("ENABLE_UNITY_SERVICES_CORE_TELEMETRY_LOGGING")]
		public static void LogTelemetry(object message)
		{
			Debug.unityLogger.Log("[ServicesCore]", message);
		}

		// Token: 0x04000032 RID: 50
		internal const string Tag = "[ServicesCore]";

		// Token: 0x04000033 RID: 51
		internal const string VerboseLoggingDefine = "ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING";

		// Token: 0x04000034 RID: 52
		private const string k_TelemetryLoggingDefine = "ENABLE_UNITY_SERVICES_CORE_TELEMETRY_LOGGING";
	}
}
