using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E4 RID: 228
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/File/ApplicationSpecificPersistentDataPath.h")]
	[NativeHeader("Runtime/Export/Application/Application.bindings.h")]
	[NativeHeader("Runtime/BaseClasses/IsPlaying.h")]
	[NativeHeader("Runtime/Application/ApplicationInfo.h")]
	[NativeHeader("Runtime/Input/InputManager.h")]
	[NativeHeader("Runtime/Utilities/Argv.h")]
	[NativeHeader("Runtime/Input/TargetFrameRate.h")]
	[NativeHeader("Runtime/Utilities/URLUtility.h")]
	[NativeHeader("Runtime/Application/AdsIdHandler.h")]
	[NativeHeader("Runtime/PreloadManager/PreloadManager.h")]
	[NativeHeader("Runtime/PreloadManager/LoadSceneOperation.h")]
	[NativeHeader("Runtime/Logging/LogSystem.h")]
	[NativeHeader("Runtime/Misc/BuildSettings.h")]
	[NativeHeader("Runtime/Misc/Player.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Network/NetworkUtility.h")]
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	public class Application
	{
		// Token: 0x060003BC RID: 956
		[FreeFunction("GetInputManager().QuitApplication")]
		[MethodImpl(4096)]
		public static extern void Quit(int exitCode);

		// Token: 0x060003BD RID: 957 RVA: 0x0000636D File Offset: 0x0000456D
		public static void Quit()
		{
			Application.Quit(0);
		}

		// Token: 0x060003BE RID: 958
		[Obsolete("CancelQuit is deprecated. Use the wantsToQuit event instead.")]
		[FreeFunction("GetInputManager().CancelQuitApplication")]
		[MethodImpl(4096)]
		public static extern void CancelQuit();

		// Token: 0x060003BF RID: 959
		[FreeFunction("Application_Bindings::Unload")]
		[MethodImpl(4096)]
		public static extern void Unload();

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003C0 RID: 960
		[Obsolete("This property is deprecated, please use LoadLevelAsync to detect if a specific scene is currently loading.")]
		public static extern bool isLoadingLevel
		{
			[FreeFunction("GetPreloadManager().IsLoadingOrQueued")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00006378 File Offset: 0x00004578
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0 for valid level indices.")]
		public static float GetStreamProgressForLevel(int levelIndex)
		{
			bool flag = levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000063AC File Offset: 0x000045AC
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0.")]
		public static float GetStreamProgressForLevel(string levelName)
		{
			return 1f;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000063C4 File Offset: 0x000045C4
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This property is deprecated and always returns 0.")]
		public static int streamedBytes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x000063D8 File Offset: 0x000045D8
		[EditorBrowsable(1)]
		[Obsolete("Application.webSecurityEnabled is no longer supported, since the Unity Web Player is no longer supported by Unity", true)]
		public static bool webSecurityEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000063EC File Offset: 0x000045EC
		public static bool CanStreamedLevelBeLoaded(int levelIndex)
		{
			return levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
		}

		// Token: 0x060003C6 RID: 966
		[FreeFunction("Application_Bindings::CanStreamedLevelBeLoaded")]
		[MethodImpl(4096)]
		public static extern bool CanStreamedLevelBeLoaded(string levelName);

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003C7 RID: 967
		public static extern bool isPlaying
		{
			[FreeFunction("IsWorldPlaying")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003C8 RID: 968
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern bool IsPlaying([NotNull("NullExceptionObject")] Object obj);

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003C9 RID: 969
		public static extern bool isFocused
		{
			[FreeFunction("IsPlayerFocused")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003CA RID: 970
		[FreeFunction("GetBuildSettings().GetBuildTags")]
		[MethodImpl(4096)]
		public static extern string[] GetBuildTags();

		// Token: 0x060003CB RID: 971
		[FreeFunction("GetBuildSettings().SetBuildTags")]
		[MethodImpl(4096)]
		public static extern void SetBuildTags(string[] buildTags);

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003CC RID: 972
		public static extern string buildGUID
		{
			[FreeFunction("Application_Bindings::GetBuildGUID")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003CD RID: 973
		// (set) Token: 0x060003CE RID: 974
		public static extern bool runInBackground
		{
			[FreeFunction("GetPlayerSettingsRunInBackground")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("SetPlayerSettingsRunInBackground")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003CF RID: 975
		[FreeFunction("GetBuildSettings().GetHasPROVersion")]
		[MethodImpl(4096)]
		public static extern bool HasProLicense();

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003D0 RID: 976
		public static extern bool isBatchMode
		{
			[FreeFunction("::IsBatchmode")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003D1 RID: 977
		internal static extern bool isTestRun
		{
			[FreeFunction("::IsTestRun")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003D2 RID: 978
		internal static extern bool isHumanControllingUs
		{
			[FreeFunction("::IsHumanControllingUs")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003D3 RID: 979
		[FreeFunction("HasARGV")]
		[MethodImpl(4096)]
		internal static extern bool HasARGV(string name);

		// Token: 0x060003D4 RID: 980
		[FreeFunction("GetFirstValueForARGV")]
		[MethodImpl(4096)]
		internal static extern string GetValueForARGV(string name);

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003D5 RID: 981
		public static extern string dataPath
		{
			[FreeFunction("GetAppDataPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003D6 RID: 982
		public static extern string streamingAssetsPath
		{
			[FreeFunction("GetStreamingAssetsPath", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003D7 RID: 983
		public static extern string persistentDataPath
		{
			[FreeFunction("GetPersistentDataPathApplicationSpecific")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003D8 RID: 984
		public static extern string temporaryCachePath
		{
			[FreeFunction("GetTemporaryCachePathApplicationSpecific")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003D9 RID: 985
		public static extern string absoluteURL
		{
			[FreeFunction("GetPlayerSettings().GetAbsoluteURL")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00006410 File Offset: 0x00004610
		[Obsolete("Application.ExternalEval is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalEval(string script)
		{
			bool flag = script.Length > 0 && script.get_Chars(script.Length - 1) != ';';
			if (flag)
			{
				script += ";";
			}
			Application.Internal_ExternalCall(script);
		}

		// Token: 0x060003DB RID: 987
		[FreeFunction("Application_Bindings::ExternalCall")]
		[MethodImpl(4096)]
		private static extern void Internal_ExternalCall(string script);

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003DC RID: 988
		public static extern string unityVersion
		{
			[FreeFunction("Application_Bindings::GetUnityVersion", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003DD RID: 989
		internal static extern int unityVersionVer
		{
			[FreeFunction("Application_Bindings::GetUnityVersionVer", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003DE RID: 990
		internal static extern int unityVersionMaj
		{
			[FreeFunction("Application_Bindings::GetUnityVersionMaj", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003DF RID: 991
		internal static extern int unityVersionMin
		{
			[FreeFunction("Application_Bindings::GetUnityVersionMin", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003E0 RID: 992
		public static extern string version
		{
			[FreeFunction("GetApplicationInfo().GetVersion")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003E1 RID: 993
		public static extern string installerName
		{
			[FreeFunction("GetApplicationInfo().GetInstallerName")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003E2 RID: 994
		public static extern string identifier
		{
			[FreeFunction("GetApplicationInfo().GetApplicationIdentifier")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003E3 RID: 995
		public static extern ApplicationInstallMode installMode
		{
			[FreeFunction("GetApplicationInfo().GetInstallMode")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003E4 RID: 996
		public static extern ApplicationSandboxType sandboxType
		{
			[FreeFunction("GetApplicationInfo().GetSandboxType")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003E5 RID: 997
		public static extern string productName
		{
			[FreeFunction("GetPlayerSettings().GetProductName")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003E6 RID: 998
		public static extern string companyName
		{
			[FreeFunction("GetPlayerSettings().GetCompanyName")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003E7 RID: 999
		public static extern string cloudProjectId
		{
			[FreeFunction("GetPlayerSettings().GetCloudProjectId")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003E8 RID: 1000
		[FreeFunction("GetAdsIdHandler().RequestAdsIdAsync")]
		[MethodImpl(4096)]
		public static extern bool RequestAdvertisingIdentifierAsync(Application.AdvertisingIdentifierCallback delegateMethod);

		// Token: 0x060003E9 RID: 1001
		[FreeFunction("OpenURL")]
		[MethodImpl(4096)]
		public static extern void OpenURL(string url);

		// Token: 0x060003EA RID: 1002 RVA: 0x00006457 File Offset: 0x00004657
		[Obsolete("Use UnityEngine.Diagnostics.Utils.ForceCrash")]
		public static void ForceCrash(int mode)
		{
			Utils.ForceCrash((ForcedCrashCategory)mode);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003EB RID: 1003
		// (set) Token: 0x060003EC RID: 1004
		public static extern int targetFrameRate
		{
			[FreeFunction("GetTargetFrameRate")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("SetTargetFrameRate")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003ED RID: 1005
		[FreeFunction("Application_Bindings::SetLogCallbackDefined")]
		[MethodImpl(4096)]
		private static extern void SetLogCallbackDefined(bool defined);

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003EE RID: 1006
		// (set) Token: 0x060003EF RID: 1007
		[Obsolete("Use SetStackTraceLogType/GetStackTraceLogType instead")]
		public static extern StackTraceLogType stackTraceLogType
		{
			[FreeFunction("Application_Bindings::GetStackTraceLogType")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Application_Bindings::SetStackTraceLogType")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003F0 RID: 1008
		[FreeFunction("GetStackTraceLogType")]
		[MethodImpl(4096)]
		public static extern StackTraceLogType GetStackTraceLogType(LogType logType);

		// Token: 0x060003F1 RID: 1009
		[FreeFunction("SetStackTraceLogType")]
		[MethodImpl(4096)]
		public static extern void SetStackTraceLogType(LogType logType, StackTraceLogType stackTraceType);

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003F2 RID: 1010
		public static extern string consoleLogPath
		{
			[FreeFunction("GetConsoleLogPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003F3 RID: 1011
		// (set) Token: 0x060003F4 RID: 1012
		public static extern ThreadPriority backgroundLoadingPriority
		{
			[FreeFunction("GetPreloadManager().GetThreadPriority")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("GetPreloadManager().SetThreadPriority")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003F5 RID: 1013
		public static extern bool genuine
		{
			[FreeFunction("IsApplicationGenuine")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003F6 RID: 1014
		public static extern bool genuineCheckAvailable
		{
			[FreeFunction("IsApplicationGenuineAvailable")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003F7 RID: 1015
		[FreeFunction("Application_Bindings::RequestUserAuthorization")]
		[MethodImpl(4096)]
		public static extern AsyncOperation RequestUserAuthorization(UserAuthorization mode);

		// Token: 0x060003F8 RID: 1016
		[FreeFunction("Application_Bindings::HasUserAuthorization")]
		[MethodImpl(4096)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003F9 RID: 1017
		internal static extern bool submitAnalytics
		{
			[FreeFunction("GetPlayerSettings().GetSubmitAnalytics")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00006464 File Offset: 0x00004664
		[Obsolete("This property is deprecated, please use SplashScreen.isFinished instead")]
		public static bool isShowingSplashScreen
		{
			get
			{
				return !SplashScreen.isFinished;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003FB RID: 1019
		public static extern RuntimePlatform platform
		{
			[FreeFunction("systeminfo::GetRuntimePlatform", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00006480 File Offset: 0x00004680
		public static bool isMobilePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				return runtimePlatform == RuntimePlatform.IPhonePlayer || runtimePlatform == RuntimePlatform.Android || (runtimePlatform - RuntimePlatform.MetroPlayerX86 <= 2 && SystemInfo.deviceType == DeviceType.Handheld);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x000064C0 File Offset: 0x000046C0
		public static bool isConsolePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.GameCoreXboxOne || platform == RuntimePlatform.GameCoreXboxSeries || platform == RuntimePlatform.PS4 || platform == RuntimePlatform.PS5 || platform == RuntimePlatform.Switch || platform == RuntimePlatform.XboxOne;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003FE RID: 1022
		public static extern SystemLanguage systemLanguage
		{
			[FreeFunction("(SystemLanguage)systeminfo::GetSystemLanguage")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003FF RID: 1023
		public static extern NetworkReachability internetReachability
		{
			[FreeFunction("GetInternetReachability")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000400 RID: 1024 RVA: 0x000064FC File Offset: 0x000046FC
		// (remove) Token: 0x06000401 RID: 1025 RVA: 0x00006530 File Offset: 0x00004730
		[field: DebuggerBrowsable(0)]
		public static event Application.LowMemoryCallback lowMemory;

		// Token: 0x06000402 RID: 1026 RVA: 0x00006564 File Offset: 0x00004764
		[RequiredByNativeCode]
		internal static void CallLowMemory()
		{
			Application.LowMemoryCallback lowMemoryCallback = Application.lowMemory;
			bool flag = lowMemoryCallback != null;
			if (flag)
			{
				lowMemoryCallback();
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000403 RID: 1027 RVA: 0x00006587 File Offset: 0x00004787
		// (remove) Token: 0x06000404 RID: 1028 RVA: 0x000065A6 File Offset: 0x000047A6
		public static event Application.LogCallback logMessageReceived
		{
			add
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandler, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandler, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000405 RID: 1029 RVA: 0x000065BE File Offset: 0x000047BE
		// (remove) Token: 0x06000406 RID: 1030 RVA: 0x000065DD File Offset: 0x000047DD
		public static event Application.LogCallback logMessageReceivedThreaded
		{
			add
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandlerThreaded, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandlerThreaded, value);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000065F8 File Offset: 0x000047F8
		[RequiredByNativeCode]
		private static void CallLogCallback(string logString, string stackTrace, LogType type, bool invokedOnMainThread)
		{
			if (invokedOnMainThread)
			{
				Application.LogCallback logCallback = Application.s_LogCallbackHandler;
				bool flag = logCallback != null;
				if (flag)
				{
					logCallback(logString, stackTrace, type);
				}
			}
			Application.LogCallback logCallback2 = Application.s_LogCallbackHandlerThreaded;
			bool flag2 = logCallback2 != null;
			if (flag2)
			{
				logCallback2(logString, stackTrace, type);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00006640 File Offset: 0x00004840
		internal static void InvokeOnAdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled)
		{
			bool flag = Application.OnAdvertisingIdentifierCallback != null;
			if (flag)
			{
				Application.OnAdvertisingIdentifierCallback(advertisingId, trackingEnabled, string.Empty);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000666C File Offset: 0x0000486C
		private static string ObjectToJSString(object o)
		{
			bool flag = o == null;
			string text;
			if (flag)
			{
				text = "null";
			}
			else
			{
				bool flag2 = o is string;
				if (flag2)
				{
					string text2 = o.ToString().Replace("\\", "\\\\");
					text2 = text2.Replace("\"", "\\\"");
					text2 = text2.Replace("\n", "\\n");
					text2 = text2.Replace("\r", "\\r");
					text2 = text2.Replace("\0", "");
					text2 = text2.Replace("\u2028", "");
					text2 = text2.Replace("\u2029", "");
					text = "\"" + text2 + "\"";
				}
				else
				{
					bool flag3 = o is int || o is short || o is uint || o is ushort || o is byte;
					if (flag3)
					{
						text = o.ToString();
					}
					else
					{
						bool flag4 = o is float;
						if (flag4)
						{
							NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
							text = ((float)o).ToString(numberFormat);
						}
						else
						{
							bool flag5 = o is double;
							if (flag5)
							{
								NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
								text = ((double)o).ToString(numberFormat2);
							}
							else
							{
								bool flag6 = o is char;
								if (flag6)
								{
									bool flag7 = (char)o == '"';
									if (flag7)
									{
										text = "\"\\\"\"";
									}
									else
									{
										text = "\"" + o.ToString() + "\"";
									}
								}
								else
								{
									bool flag8 = o is IList;
									if (flag8)
									{
										IList list = (IList)o;
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append("new Array(");
										int count = list.Count;
										for (int i = 0; i < count; i++)
										{
											bool flag9 = i != 0;
											if (flag9)
											{
												stringBuilder.Append(", ");
											}
											stringBuilder.Append(Application.ObjectToJSString(list[i]));
										}
										stringBuilder.Append(")");
										text = stringBuilder.ToString();
									}
									else
									{
										text = Application.ObjectToJSString(o.ToString());
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000068C2 File Offset: 0x00004AC2
		[Obsolete("Application.ExternalCall is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000068D4 File Offset: 0x00004AD4
		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = i != 0;
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00006954 File Offset: 0x00004B54
		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00006970 File Offset: 0x00004B70
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		public static void DontDestroyOnLoad(Object o)
		{
			bool flag = o != null;
			if (flag)
			{
				Object.DontDestroyOnLoad(o);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00006990 File Offset: 0x00004B90
		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename, int superSize)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00006990 File Offset: 0x00004B90
		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000410 RID: 1040 RVA: 0x0000699D File Offset: 0x00004B9D
		// (remove) Token: 0x06000411 RID: 1041 RVA: 0x000069A7 File Offset: 0x00004BA7
		public static event UnityAction onBeforeRender
		{
			add
			{
				BeforeRenderHelper.RegisterCallback(value);
			}
			remove
			{
				BeforeRenderHelper.UnregisterCallback(value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000412 RID: 1042 RVA: 0x000069B4 File Offset: 0x00004BB4
		// (remove) Token: 0x06000413 RID: 1043 RVA: 0x000069E8 File Offset: 0x00004BE8
		[field: DebuggerBrowsable(0)]
		public static event Action<bool> focusChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000414 RID: 1044 RVA: 0x00006A1C File Offset: 0x00004C1C
		// (remove) Token: 0x06000415 RID: 1045 RVA: 0x00006A50 File Offset: 0x00004C50
		[field: DebuggerBrowsable(0)]
		public static event Action<string> deepLinkActivated;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000416 RID: 1046 RVA: 0x00006A84 File Offset: 0x00004C84
		// (remove) Token: 0x06000417 RID: 1047 RVA: 0x00006AB8 File Offset: 0x00004CB8
		[field: DebuggerBrowsable(0)]
		public static event Func<bool> wantsToQuit;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000418 RID: 1048 RVA: 0x00006AEC File Offset: 0x00004CEC
		// (remove) Token: 0x06000419 RID: 1049 RVA: 0x00006B20 File Offset: 0x00004D20
		[field: DebuggerBrowsable(0)]
		public static event Action quitting;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600041A RID: 1050 RVA: 0x00006B54 File Offset: 0x00004D54
		// (remove) Token: 0x0600041B RID: 1051 RVA: 0x00006B88 File Offset: 0x00004D88
		[field: DebuggerBrowsable(0)]
		public static event Action unloading;

		// Token: 0x0600041C RID: 1052 RVA: 0x00006BBC File Offset: 0x00004DBC
		[RequiredByNativeCode]
		private static bool Internal_ApplicationWantsToQuit()
		{
			bool flag = Application.wantsToQuit != null;
			if (flag)
			{
				foreach (Func<bool> func in Application.wantsToQuit.GetInvocationList())
				{
					try
					{
						bool flag2 = !func.Invoke();
						if (flag2)
						{
							return false;
						}
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
				}
			}
			return true;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00006C3C File Offset: 0x00004E3C
		[RequiredByNativeCode]
		private static void Internal_ApplicationQuit()
		{
			bool flag = Application.quitting != null;
			if (flag)
			{
				Application.quitting.Invoke();
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00006C64 File Offset: 0x00004E64
		[RequiredByNativeCode]
		private static void Internal_ApplicationUnload()
		{
			bool flag = Application.unloading != null;
			if (flag)
			{
				Application.unloading.Invoke();
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00006C89 File Offset: 0x00004E89
		[RequiredByNativeCode]
		internal static void InvokeOnBeforeRender()
		{
			BeforeRenderHelper.Invoke();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00006C94 File Offset: 0x00004E94
		[RequiredByNativeCode]
		internal static void InvokeFocusChanged(bool focus)
		{
			bool flag = Application.focusChanged != null;
			if (flag)
			{
				Application.focusChanged.Invoke(focus);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00006CBC File Offset: 0x00004EBC
		[RequiredByNativeCode]
		internal static void InvokeDeepLinkActivated(string url)
		{
			bool flag = Application.deepLinkActivated != null;
			if (flag)
			{
				Application.deepLinkActivated.Invoke(url);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00006CE2 File Offset: 0x00004EE2
		[Obsolete("Application.RegisterLogCallback is deprecated. Use Application.logMessageReceived instead.")]
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, false);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00006CED File Offset: 0x00004EED
		[Obsolete("Application.RegisterLogCallbackThreaded is deprecated. Use Application.logMessageReceivedThreaded instead.")]
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, true);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00006CF8 File Offset: 0x00004EF8
		private static void RegisterLogCallback(Application.LogCallback handler, bool threaded)
		{
			bool flag = Application.s_RegisterLogCallbackDeprecated != null;
			if (flag)
			{
				Application.logMessageReceived -= Application.s_RegisterLogCallbackDeprecated;
				Application.logMessageReceivedThreaded -= Application.s_RegisterLogCallbackDeprecated;
			}
			Application.s_RegisterLogCallbackDeprecated = handler;
			bool flag2 = handler != null;
			if (flag2)
			{
				if (threaded)
				{
					Application.logMessageReceivedThreaded += handler;
				}
				else
				{
					Application.logMessageReceived += handler;
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00006D58 File Offset: 0x00004F58
		[Obsolete("Use SceneManager.sceneCountInBuildSettings")]
		public static int levelCount
		{
			get
			{
				return SceneManager.sceneCountInBuildSettings;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00006D70 File Offset: 0x00004F70
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static int loadedLevel
		{
			get
			{
				return SceneManager.GetActiveScene().buildIndex;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00006D90 File Offset: 0x00004F90
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static string loadedLevelName
		{
			get
			{
				return SceneManager.GetActiveScene().name;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00006DAF File Offset: 0x00004FAF
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Single);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00006DBA File Offset: 0x00004FBA
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Single);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00006DC5 File Offset: 0x00004FC5
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Additive);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00006DD0 File Offset: 0x00004FD0
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Additive);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00006DDC File Offset: 0x00004FDC
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00006DF8 File Offset: 0x00004FF8
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00006E14 File Offset: 0x00005014
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00006E30 File Offset: 0x00005030
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00006E4C File Offset: 0x0000504C
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(int index)
		{
			return SceneManager.UnloadScene(index);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00006E64 File Offset: 0x00005064
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(string scenePath)
		{
			return SceneManager.UnloadScene(scenePath);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00006E7C File Offset: 0x0000507C
		public static bool isEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040002F5 RID: 757
		private static Application.LogCallback s_LogCallbackHandler;

		// Token: 0x040002F6 RID: 758
		private static Application.LogCallback s_LogCallbackHandlerThreaded;

		// Token: 0x040002F7 RID: 759
		internal static Application.AdvertisingIdentifierCallback OnAdvertisingIdentifierCallback;

		// Token: 0x040002FD RID: 765
		private static volatile Application.LogCallback s_RegisterLogCallbackDeprecated;

		// Token: 0x020000E5 RID: 229
		// (Invoke) Token: 0x06000435 RID: 1077
		public delegate void AdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled, string errorMsg);

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x06000439 RID: 1081
		public delegate void LowMemoryCallback();

		// Token: 0x020000E7 RID: 231
		// (Invoke) Token: 0x0600043D RID: 1085
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
