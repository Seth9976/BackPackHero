using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000109 RID: 265
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	public class Debug
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00008719 File Offset: 0x00006919
		public static ILogger unityLogger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00008720 File Offset: 0x00006920
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
		{
			bool flag = true;
			Debug.DrawLine(start, end, color, duration, flag);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000873C File Offset: 0x0000693C
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawLine(start, end, color, num, flag);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00008760 File Offset: 0x00006960
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.DrawLine(start, end, white, num, flag);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00008787 File Offset: 0x00006987
		[FreeFunction("DebugDrawLine", IsThreadSafe = true)]
		public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine_Injected(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00008798 File Offset: 0x00006998
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
		{
			bool flag = true;
			Debug.DrawRay(start, dir, color, duration, flag);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000087B4 File Offset: 0x000069B4
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawRay(start, dir, color, num, flag);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000087D8 File Offset: 0x000069D8
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.DrawRay(start, dir, white, num, flag);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000087FF File Offset: 0x000069FF
		public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		// Token: 0x0600063A RID: 1594
		[FreeFunction("PauseEditor")]
		[MethodImpl(4096)]
		public static extern void Break();

		// Token: 0x0600063B RID: 1595
		[MethodImpl(4096)]
		public static extern void DebugBreak();

		// Token: 0x0600063C RID: 1596
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern int ExtractStackTraceNoAlloc(byte* buffer, int bufferMax, string projectFolder);

		// Token: 0x0600063D RID: 1597 RVA: 0x00008814 File Offset: 0x00006A14
		public static void Log(object message)
		{
			Debug.unityLogger.Log(LogType.Log, message);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00008824 File Offset: 0x00006A24
		public static void Log(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Log, message, context);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00008835 File Offset: 0x00006A35
		public static void LogFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, format, args);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00008846 File Offset: 0x00006A46
		public static void LogFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, context, format, args);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00008858 File Offset: 0x00006A58
		public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler debugLogHandler = Debug.unityLogger.logHandler as DebugLogHandler;
			bool flag = debugLogHandler == null;
			if (flag)
			{
				Debug.unityLogger.LogFormat(logType, context, format, args);
			}
			else
			{
				bool flag2 = Debug.unityLogger.IsLogTypeAllowed(logType);
				if (flag2)
				{
					debugLogHandler.LogFormat(logType, logOptions, context, format, args);
				}
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000088AC File Offset: 0x00006AAC
		public static void LogError(object message)
		{
			Debug.unityLogger.Log(LogType.Error, message);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000088BC File Offset: 0x00006ABC
		public static void LogError(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Error, message, context);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000088CD File Offset: 0x00006ACD
		public static void LogErrorFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, format, args);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000088DE File Offset: 0x00006ADE
		public static void LogErrorFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, context, format, args);
		}

		// Token: 0x06000646 RID: 1606
		[MethodImpl(4096)]
		public static extern void ClearDeveloperConsole();

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000647 RID: 1607
		// (set) Token: 0x06000648 RID: 1608
		public static extern bool developerConsoleVisible
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000088F0 File Offset: 0x00006AF0
		public static void LogException(Exception exception)
		{
			Debug.unityLogger.LogException(exception, null);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00008900 File Offset: 0x00006B00
		public static void LogException(Exception exception, Object context)
		{
			Debug.unityLogger.LogException(exception, context);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00008910 File Offset: 0x00006B10
		public static void LogWarning(object message)
		{
			Debug.unityLogger.Log(LogType.Warning, message);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00008920 File Offset: 0x00006B20
		public static void LogWarning(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Warning, message, context);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00008931 File Offset: 0x00006B31
		public static void LogWarningFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, format, args);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00008942 File Offset: 0x00006B42
		public static void LogWarningFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, context, format, args);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00008954 File Offset: 0x00006B54
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed");
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000897C File Offset: 0x00006B7C
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed", context);
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000089A4 File Offset: 0x00006BA4
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000089C8 File Offset: 0x00006BC8
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000089EC File Offset: 0x00006BEC
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00008A10 File Offset: 0x00006C10
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00008A34 File Offset: 0x00006C34
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00008A58 File Offset: 0x00006C58
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, Object context, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00008A7D File Offset: 0x00006C7D
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.unityLogger.Log(LogType.Assert, message);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00008A8D File Offset: 0x00006C8D
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Assert, message, context);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00008A9E File Offset: 0x00006C9E
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, format, args);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00008AAF File Offset: 0x00006CAF
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600065B RID: 1627
		[StaticAccessor("GetBuildSettings()", StaticAccessorType.Dot)]
		[NativeProperty(TargetType = TargetType.Field)]
		public static extern bool isDebugBuild
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600065C RID: 1628
		[FreeFunction("DeveloperConsole_OpenConsoleFile")]
		[MethodImpl(4096)]
		internal static extern void OpenConsoleFile();

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600065D RID: 1629
		[NativeThrows]
		internal static extern DiagnosticSwitch[] diagnosticSwitches
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00008AC4 File Offset: 0x00006CC4
		internal static DiagnosticSwitch GetDiagnosticSwitch(string name)
		{
			foreach (DiagnosticSwitch diagnosticSwitch in Debug.diagnosticSwitches)
			{
				bool flag = diagnosticSwitch.name == name;
				if (flag)
				{
					return diagnosticSwitch;
				}
			}
			throw new ArgumentException("Could not find DiagnosticSwitch named " + name);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00008B18 File Offset: 0x00006D18
		[RequiredByNativeCode]
		internal static bool CallOverridenDebugHandler(Exception exception, Object obj)
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				try
				{
					Debug.unityLogger.LogException(exception, obj);
				}
				catch (Exception ex)
				{
					Debug.s_DefaultLogger.LogError(string.Format("Invalid exception thrown from custom {0}.LogException(). Message: {1}", Debug.unityLogger.logHandler.GetType(), ex), obj);
					return false;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00008B94 File Offset: 0x00006D94
		[RequiredByNativeCode]
		internal static bool IsLoggingEnabled()
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool flag2;
			if (flag)
			{
				flag2 = Debug.unityLogger.logEnabled;
			}
			else
			{
				flag2 = Debug.s_DefaultLogger.logEnabled;
			}
			return flag2;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00008BD4 File Offset: 0x00006DD4
		[Obsolete("Assert(bool, string, params object[]) is obsolete. Use AssertFormat(bool, string, params object[]) (UnityUpgradable) -> AssertFormat(*)", true)]
		[Conditional("UNITY_ASSERTIONS")]
		[EditorBrowsable(1)]
		public static void Assert(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00008BF8 File Offset: 0x00006DF8
		[Obsolete("Debug.logger is obsolete. Please use Debug.unityLogger instead (UnityUpgradable) -> unityLogger")]
		[EditorBrowsable(1)]
		public static ILogger logger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x06000665 RID: 1637
		[MethodImpl(4096)]
		private static extern void DrawLine_Injected(ref Vector3 start, ref Vector3 end, [DefaultValue("Color.white")] ref Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest);

		// Token: 0x0400037B RID: 891
		internal static readonly ILogger s_DefaultLogger = new Logger(new DebugLogHandler());

		// Token: 0x0400037C RID: 892
		internal static ILogger s_Logger = new Logger(new DebugLogHandler());
	}
}
