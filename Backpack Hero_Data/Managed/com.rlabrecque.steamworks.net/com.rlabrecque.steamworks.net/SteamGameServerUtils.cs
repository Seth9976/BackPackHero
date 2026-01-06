using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200000F RID: 15
	public static class SteamGameServerUtils
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00006415 File Offset: 0x00004615
		public static uint GetSecondsSinceAppActive()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetSecondsSinceAppActive(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006426 File Offset: 0x00004626
		public static uint GetSecondsSinceComputerActive()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetSecondsSinceComputerActive(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006437 File Offset: 0x00004637
		public static EUniverse GetConnectedUniverse()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetConnectedUniverse(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006448 File Offset: 0x00004648
		public static uint GetServerRealTime()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetServerRealTime(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006459 File Offset: 0x00004659
		public static string GetIPCountry()
		{
			InteropHelp.TestIfAvailableGameServer();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamUtils_GetIPCountry(CSteamGameServerAPIContext.GetSteamUtils()));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000646F File Offset: 0x0000466F
		public static bool GetImageSize(int iImage, out uint pnWidth, out uint pnHeight)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetImageSize(CSteamGameServerAPIContext.GetSteamUtils(), iImage, out pnWidth, out pnHeight);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006483 File Offset: 0x00004683
		public static bool GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetImageRGBA(CSteamGameServerAPIContext.GetSteamUtils(), iImage, pubDest, nDestBufferSize);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006497 File Offset: 0x00004697
		public static byte GetCurrentBatteryPower()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetCurrentBatteryPower(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000064A8 File Offset: 0x000046A8
		public static AppId_t GetAppID()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (AppId_t)NativeMethods.ISteamUtils_GetAppID(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000064BE File Offset: 0x000046BE
		public static void SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamUtils_SetOverlayNotificationPosition(CSteamGameServerAPIContext.GetSteamUtils(), eNotificationPosition);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000064D0 File Offset: 0x000046D0
		public static bool IsAPICallCompleted(SteamAPICall_t hSteamAPICall, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_IsAPICallCompleted(CSteamGameServerAPIContext.GetSteamUtils(), hSteamAPICall, out pbFailed);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000064E3 File Offset: 0x000046E3
		public static ESteamAPICallFailure GetAPICallFailureReason(SteamAPICall_t hSteamAPICall)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetAPICallFailureReason(CSteamGameServerAPIContext.GetSteamUtils(), hSteamAPICall);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000064F5 File Offset: 0x000046F5
		public static bool GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetAPICallResult(CSteamGameServerAPIContext.GetSteamUtils(), hSteamAPICall, pCallback, cubCallback, iCallbackExpected, out pbFailed);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000650C File Offset: 0x0000470C
		public static uint GetIPCCallCount()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetIPCCallCount(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000651D File Offset: 0x0000471D
		public static void SetWarningMessageHook(SteamAPIWarningMessageHook_t pFunction)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamUtils_SetWarningMessageHook(CSteamGameServerAPIContext.GetSteamUtils(), pFunction);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000652F File Offset: 0x0000472F
		public static bool IsOverlayEnabled()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_IsOverlayEnabled(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006540 File Offset: 0x00004740
		public static bool BOverlayNeedsPresent()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_BOverlayNeedsPresent(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006554 File Offset: 0x00004754
		public static SteamAPICall_t CheckFileSignature(string szFileName)
		{
			InteropHelp.TestIfAvailableGameServer();
			SteamAPICall_t steamAPICall_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(szFileName))
			{
				steamAPICall_t = (SteamAPICall_t)NativeMethods.ISteamUtils_CheckFileSignature(CSteamGameServerAPIContext.GetSteamUtils(), utf8StringHandle);
			}
			return steamAPICall_t;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000659C File Offset: 0x0000479C
		public static bool ShowGamepadTextInput(EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchExistingText))
				{
					flag = NativeMethods.ISteamUtils_ShowGamepadTextInput(CSteamGameServerAPIContext.GetSteamUtils(), eInputMode, eLineInputMode, utf8StringHandle, unCharMax, utf8StringHandle2);
				}
			}
			return flag;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006600 File Offset: 0x00004800
		public static uint GetEnteredGamepadTextLength()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetEnteredGamepadTextLength(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006614 File Offset: 0x00004814
		public static bool GetEnteredGamepadTextInput(out string pchText, uint cchText)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchText);
			bool flag = NativeMethods.ISteamUtils_GetEnteredGamepadTextInput(CSteamGameServerAPIContext.GetSteamUtils(), intPtr, cchText);
			pchText = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000664F File Offset: 0x0000484F
		public static string GetSteamUILanguage()
		{
			InteropHelp.TestIfAvailableGameServer();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamUtils_GetSteamUILanguage(CSteamGameServerAPIContext.GetSteamUtils()));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006665 File Offset: 0x00004865
		public static bool IsSteamRunningInVR()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_IsSteamRunningInVR(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006676 File Offset: 0x00004876
		public static void SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamUtils_SetOverlayNotificationInset(CSteamGameServerAPIContext.GetSteamUtils(), nHorizontalInset, nVerticalInset);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006689 File Offset: 0x00004889
		public static bool IsSteamInBigPictureMode()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_IsSteamInBigPictureMode(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000669A File Offset: 0x0000489A
		public static void StartVRDashboard()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamUtils_StartVRDashboard(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000066AB File Offset: 0x000048AB
		public static bool IsVRHeadsetStreamingEnabled()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_IsVRHeadsetStreamingEnabled(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000066BC File Offset: 0x000048BC
		public static void SetVRHeadsetStreamingEnabled(bool bEnabled)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamUtils_SetVRHeadsetStreamingEnabled(CSteamGameServerAPIContext.GetSteamUtils(), bEnabled);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000066CE File Offset: 0x000048CE
		public static bool IsSteamChinaLauncher()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_IsSteamChinaLauncher(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000066DF File Offset: 0x000048DF
		public static bool InitFilterText(uint unFilterOptions = 0U)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_InitFilterText(CSteamGameServerAPIContext.GetSteamUtils(), unFilterOptions);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000066F4 File Offset: 0x000048F4
		public static int FilterText(ETextFilteringContext eContext, CSteamID sourceSteamID, string pchInputMessage, out string pchOutFilteredText, uint nByteSizeOutFilteredText)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)nByteSizeOutFilteredText);
			int num2;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchInputMessage))
			{
				int num = NativeMethods.ISteamUtils_FilterText(CSteamGameServerAPIContext.GetSteamUtils(), eContext, sourceSteamID, utf8StringHandle, intPtr, nByteSizeOutFilteredText);
				pchOutFilteredText = ((num != -1) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
				Marshal.FreeHGlobal(intPtr);
				num2 = num;
			}
			return num2;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000675C File Offset: 0x0000495C
		public static ESteamIPv6ConnectivityState GetIPv6ConnectivityState(ESteamIPv6ConnectivityProtocol eProtocol)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_GetIPv6ConnectivityState(CSteamGameServerAPIContext.GetSteamUtils(), eProtocol);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000676E File Offset: 0x0000496E
		public static bool IsSteamRunningOnSteamDeck()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_IsSteamRunningOnSteamDeck(CSteamGameServerAPIContext.GetSteamUtils());
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000677F File Offset: 0x0000497F
		public static bool ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_ShowFloatingGamepadTextInput(CSteamGameServerAPIContext.GetSteamUtils(), eKeyboardMode, nTextFieldXPosition, nTextFieldYPosition, nTextFieldWidth, nTextFieldHeight);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006796 File Offset: 0x00004996
		public static void SetGameLauncherMode(bool bLauncherMode)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamUtils_SetGameLauncherMode(CSteamGameServerAPIContext.GetSteamUtils(), bLauncherMode);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000067A8 File Offset: 0x000049A8
		public static bool DismissFloatingGamepadTextInput()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUtils_DismissFloatingGamepadTextInput(CSteamGameServerAPIContext.GetSteamUtils());
		}
	}
}
