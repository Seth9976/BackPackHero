using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000009 RID: 9
	[RequiredByNativeCode]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	public static class AnalyticsSessionInfo
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600006E RID: 110 RVA: 0x00002C74 File Offset: 0x00000E74
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[field: DebuggerBrowsable(0)]
		public static event AnalyticsSessionInfo.SessionStateChanged sessionStateChanged;

		// Token: 0x06000070 RID: 112 RVA: 0x00002CDC File Offset: 0x00000EDC
		[RequiredByNativeCode]
		internal static void CallSessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged)
		{
			AnalyticsSessionInfo.SessionStateChanged sessionStateChanged = AnalyticsSessionInfo.sessionStateChanged;
			bool flag = sessionStateChanged != null;
			if (flag)
			{
				sessionStateChanged(sessionState, sessionId, sessionElapsedTime, sessionChanged);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000071 RID: 113
		public static extern AnalyticsSessionState sessionState
		{
			[NativeMethod("GetPlayerSessionState")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000072 RID: 114
		public static extern long sessionId
		{
			[NativeMethod("GetPlayerSessionId")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000073 RID: 115
		public static extern long sessionCount
		{
			[NativeMethod("GetPlayerSessionCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000074 RID: 116
		public static extern long sessionElapsedTime
		{
			[NativeMethod("GetPlayerSessionElapsedTime")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000075 RID: 117
		public static extern bool sessionFirstRun
		{
			[NativeMethod("GetPlayerSessionFirstRun", false, true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000076 RID: 118
		public static extern string userId
		{
			[NativeMethod("GetUserId")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002D04 File Offset: 0x00000F04
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static string customUserId
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = null;
				}
				else
				{
					text = AnalyticsSessionInfo.customUserIdInternal;
				}
				return text;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					AnalyticsSessionInfo.customUserIdInternal = value;
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002D4C File Offset: 0x00000F4C
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002D74 File Offset: 0x00000F74
		public static string customDeviceId
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = null;
				}
				else
				{
					text = AnalyticsSessionInfo.customDeviceIdInternal;
				}
				return text;
			}
			set
			{
				bool flag = Analytics.IsInitialized();
				if (flag)
				{
					AnalyticsSessionInfo.customDeviceIdInternal = value;
				}
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600007B RID: 123 RVA: 0x00002D94 File Offset: 0x00000F94
		// (remove) Token: 0x0600007C RID: 124 RVA: 0x00002DC8 File Offset: 0x00000FC8
		[field: DebuggerBrowsable(0)]
		public static event AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged;

		// Token: 0x0600007D RID: 125 RVA: 0x00002DFC File Offset: 0x00000FFC
		[RequiredByNativeCode]
		internal static void CallIdentityTokenChanged(string token)
		{
			AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged = AnalyticsSessionInfo.identityTokenChanged;
			bool flag = identityTokenChanged != null;
			if (flag)
			{
				identityTokenChanged(token);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002E20 File Offset: 0x00001020
		public static string identityToken
		{
			get
			{
				bool flag = !Analytics.IsInitialized();
				string text;
				if (flag)
				{
					text = null;
				}
				else
				{
					text = AnalyticsSessionInfo.identityTokenInternal;
				}
				return text;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007F RID: 127
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string identityTokenInternal
		{
			[NativeMethod("GetIdentityToken")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000080 RID: 128
		// (set) Token: 0x06000081 RID: 129
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string customUserIdInternal
		{
			[NativeMethod("GetCustomUserId")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetCustomUserId")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000082 RID: 130
		// (set) Token: 0x06000083 RID: 131
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string customDeviceIdInternal
		{
			[NativeMethod("GetCustomDeviceId")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetCustomDeviceId")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000085 RID: 133
		public delegate void SessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged);

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000089 RID: 137
		public delegate void IdentityTokenChanged(string token);
	}
}
