using System;

namespace Steamworks
{
	// Token: 0x02000012 RID: 18
	public static class SteamInput
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00007051 File Offset: 0x00005251
		public static bool Init(bool bExplicitlyCallRunFrame)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_Init(CSteamAPIContext.GetSteamInput(), bExplicitlyCallRunFrame);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007063 File Offset: 0x00005263
		public static bool Shutdown()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_Shutdown(CSteamAPIContext.GetSteamInput());
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007074 File Offset: 0x00005274
		public static bool SetInputActionManifestFilePath(string pchInputActionManifestAbsolutePath)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchInputActionManifestAbsolutePath))
			{
				flag = NativeMethods.ISteamInput_SetInputActionManifestFilePath(CSteamAPIContext.GetSteamInput(), utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000070B8 File Offset: 0x000052B8
		public static void RunFrame(bool bReservedValue = true)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_RunFrame(CSteamAPIContext.GetSteamInput(), bReservedValue);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000070CA File Offset: 0x000052CA
		public static bool BWaitForData(bool bWaitForever, uint unTimeout)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_BWaitForData(CSteamAPIContext.GetSteamInput(), bWaitForever, unTimeout);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000070DD File Offset: 0x000052DD
		public static bool BNewDataAvailable()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_BNewDataAvailable(CSteamAPIContext.GetSteamInput());
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000070EE File Offset: 0x000052EE
		public static int GetConnectedControllers(InputHandle_t[] handlesOut)
		{
			InteropHelp.TestIfAvailableClient();
			if (handlesOut != null && handlesOut.Length != 16)
			{
				throw new ArgumentException("handlesOut must be the same size as Constants.STEAM_INPUT_MAX_COUNT!");
			}
			return NativeMethods.ISteamInput_GetConnectedControllers(CSteamAPIContext.GetSteamInput(), handlesOut);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007115 File Offset: 0x00005315
		public static void EnableDeviceCallbacks()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_EnableDeviceCallbacks(CSteamAPIContext.GetSteamInput());
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007126 File Offset: 0x00005326
		public static void EnableActionEventCallbacks(SteamInputActionEventCallbackPointer pCallback)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_EnableActionEventCallbacks(CSteamAPIContext.GetSteamInput(), pCallback);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007138 File Offset: 0x00005338
		public static InputActionSetHandle_t GetActionSetHandle(string pszActionSetName)
		{
			InteropHelp.TestIfAvailableClient();
			InputActionSetHandle_t inputActionSetHandle_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionSetName))
			{
				inputActionSetHandle_t = (InputActionSetHandle_t)NativeMethods.ISteamInput_GetActionSetHandle(CSteamAPIContext.GetSteamInput(), utf8StringHandle);
			}
			return inputActionSetHandle_t;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007180 File Offset: 0x00005380
		public static void ActivateActionSet(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_ActivateActionSet(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007193 File Offset: 0x00005393
		public static InputActionSetHandle_t GetCurrentActionSet(InputHandle_t inputHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return (InputActionSetHandle_t)NativeMethods.ISteamInput_GetCurrentActionSet(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000071AA File Offset: 0x000053AA
		public static void ActivateActionSetLayer(InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_ActivateActionSetLayer(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetLayerHandle);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000071BD File Offset: 0x000053BD
		public static void DeactivateActionSetLayer(InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_DeactivateActionSetLayer(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetLayerHandle);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000071D0 File Offset: 0x000053D0
		public static void DeactivateAllActionSetLayers(InputHandle_t inputHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_DeactivateAllActionSetLayers(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000071E2 File Offset: 0x000053E2
		public static int GetActiveActionSetLayers(InputHandle_t inputHandle, InputActionSetHandle_t[] handlesOut)
		{
			InteropHelp.TestIfAvailableClient();
			if (handlesOut != null && handlesOut.Length != 16)
			{
				throw new ArgumentException("handlesOut must be the same size as Constants.STEAM_INPUT_MAX_ACTIVE_LAYERS!");
			}
			return NativeMethods.ISteamInput_GetActiveActionSetLayers(CSteamAPIContext.GetSteamInput(), inputHandle, handlesOut);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000720C File Offset: 0x0000540C
		public static InputDigitalActionHandle_t GetDigitalActionHandle(string pszActionName)
		{
			InteropHelp.TestIfAvailableClient();
			InputDigitalActionHandle_t inputDigitalActionHandle_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionName))
			{
				inputDigitalActionHandle_t = (InputDigitalActionHandle_t)NativeMethods.ISteamInput_GetDigitalActionHandle(CSteamAPIContext.GetSteamInput(), utf8StringHandle);
			}
			return inputDigitalActionHandle_t;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007254 File Offset: 0x00005454
		public static InputDigitalActionData_t GetDigitalActionData(InputHandle_t inputHandle, InputDigitalActionHandle_t digitalActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetDigitalActionData(CSteamAPIContext.GetSteamInput(), inputHandle, digitalActionHandle);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007267 File Offset: 0x00005467
		public static int GetDigitalActionOrigins(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, EInputActionOrigin[] originsOut)
		{
			InteropHelp.TestIfAvailableClient();
			if (originsOut != null && originsOut.Length != 8)
			{
				throw new ArgumentException("originsOut must be the same size as Constants.STEAM_INPUT_MAX_ORIGINS!");
			}
			return NativeMethods.ISteamInput_GetDigitalActionOrigins(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle, digitalActionHandle, originsOut);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007290 File Offset: 0x00005490
		public static string GetStringForDigitalActionName(InputDigitalActionHandle_t eActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForDigitalActionName(CSteamAPIContext.GetSteamInput(), eActionHandle));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000072A8 File Offset: 0x000054A8
		public static InputAnalogActionHandle_t GetAnalogActionHandle(string pszActionName)
		{
			InteropHelp.TestIfAvailableClient();
			InputAnalogActionHandle_t inputAnalogActionHandle_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionName))
			{
				inputAnalogActionHandle_t = (InputAnalogActionHandle_t)NativeMethods.ISteamInput_GetAnalogActionHandle(CSteamAPIContext.GetSteamInput(), utf8StringHandle);
			}
			return inputAnalogActionHandle_t;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000072F0 File Offset: 0x000054F0
		public static InputAnalogActionData_t GetAnalogActionData(InputHandle_t inputHandle, InputAnalogActionHandle_t analogActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetAnalogActionData(CSteamAPIContext.GetSteamInput(), inputHandle, analogActionHandle);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007303 File Offset: 0x00005503
		public static int GetAnalogActionOrigins(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputAnalogActionHandle_t analogActionHandle, EInputActionOrigin[] originsOut)
		{
			InteropHelp.TestIfAvailableClient();
			if (originsOut != null && originsOut.Length != 8)
			{
				throw new ArgumentException("originsOut must be the same size as Constants.STEAM_INPUT_MAX_ORIGINS!");
			}
			return NativeMethods.ISteamInput_GetAnalogActionOrigins(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle, analogActionHandle, originsOut);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000732C File Offset: 0x0000552C
		public static string GetGlyphPNGForActionOrigin(EInputActionOrigin eOrigin, ESteamInputGlyphSize eSize, uint unFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphPNGForActionOrigin(CSteamAPIContext.GetSteamInput(), eOrigin, eSize, unFlags));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007345 File Offset: 0x00005545
		public static string GetGlyphSVGForActionOrigin(EInputActionOrigin eOrigin, uint unFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphSVGForActionOrigin(CSteamAPIContext.GetSteamInput(), eOrigin, unFlags));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000735D File Offset: 0x0000555D
		public static string GetGlyphForActionOrigin_Legacy(EInputActionOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphForActionOrigin_Legacy(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007374 File Offset: 0x00005574
		public static string GetStringForActionOrigin(EInputActionOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForActionOrigin(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000738B File Offset: 0x0000558B
		public static string GetStringForAnalogActionName(InputAnalogActionHandle_t eActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForAnalogActionName(CSteamAPIContext.GetSteamInput(), eActionHandle));
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000073A2 File Offset: 0x000055A2
		public static void StopAnalogActionMomentum(InputHandle_t inputHandle, InputAnalogActionHandle_t eAction)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_StopAnalogActionMomentum(CSteamAPIContext.GetSteamInput(), inputHandle, eAction);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000073B5 File Offset: 0x000055B5
		public static InputMotionData_t GetMotionData(InputHandle_t inputHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetMotionData(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000073C7 File Offset: 0x000055C7
		public static void TriggerVibration(InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_TriggerVibration(CSteamAPIContext.GetSteamInput(), inputHandle, usLeftSpeed, usRightSpeed);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000073DB File Offset: 0x000055DB
		public static void TriggerVibrationExtended(InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed, ushort usLeftTriggerSpeed, ushort usRightTriggerSpeed)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_TriggerVibrationExtended(CSteamAPIContext.GetSteamInput(), inputHandle, usLeftSpeed, usRightSpeed, usLeftTriggerSpeed, usRightTriggerSpeed);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000073F2 File Offset: 0x000055F2
		public static void TriggerSimpleHapticEvent(InputHandle_t inputHandle, EControllerHapticLocation eHapticLocation, byte nIntensity, char nGainDB, byte nOtherIntensity, char nOtherGainDB)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_TriggerSimpleHapticEvent(CSteamAPIContext.GetSteamInput(), inputHandle, eHapticLocation, nIntensity, nGainDB, nOtherIntensity, nOtherGainDB);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000740B File Offset: 0x0000560B
		public static void SetLEDColor(InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_SetLEDColor(CSteamAPIContext.GetSteamInput(), inputHandle, nColorR, nColorG, nColorB, nFlags);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007422 File Offset: 0x00005622
		public static void Legacy_TriggerHapticPulse(InputHandle_t inputHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_Legacy_TriggerHapticPulse(CSteamAPIContext.GetSteamInput(), inputHandle, eTargetPad, usDurationMicroSec);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007436 File Offset: 0x00005636
		public static void Legacy_TriggerRepeatedHapticPulse(InputHandle_t inputHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_Legacy_TriggerRepeatedHapticPulse(CSteamAPIContext.GetSteamInput(), inputHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000744F File Offset: 0x0000564F
		public static bool ShowBindingPanel(InputHandle_t inputHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_ShowBindingPanel(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007461 File Offset: 0x00005661
		public static ESteamInputType GetInputTypeForHandle(InputHandle_t inputHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetInputTypeForHandle(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00007473 File Offset: 0x00005673
		public static InputHandle_t GetControllerForGamepadIndex(int nIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (InputHandle_t)NativeMethods.ISteamInput_GetControllerForGamepadIndex(CSteamAPIContext.GetSteamInput(), nIndex);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000748A File Offset: 0x0000568A
		public static int GetGamepadIndexForController(InputHandle_t ulinputHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetGamepadIndexForController(CSteamAPIContext.GetSteamInput(), ulinputHandle);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000749C File Offset: 0x0000569C
		public static string GetStringForXboxOrigin(EXboxOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForXboxOrigin(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000074B3 File Offset: 0x000056B3
		public static string GetGlyphForXboxOrigin(EXboxOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphForXboxOrigin(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000074CA File Offset: 0x000056CA
		public static EInputActionOrigin GetActionOriginFromXboxOrigin(InputHandle_t inputHandle, EXboxOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetActionOriginFromXboxOrigin(CSteamAPIContext.GetSteamInput(), inputHandle, eOrigin);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000074DD File Offset: 0x000056DD
		public static EInputActionOrigin TranslateActionOrigin(ESteamInputType eDestinationInputType, EInputActionOrigin eSourceOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_TranslateActionOrigin(CSteamAPIContext.GetSteamInput(), eDestinationInputType, eSourceOrigin);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000074F0 File Offset: 0x000056F0
		public static bool GetDeviceBindingRevision(InputHandle_t inputHandle, out int pMajor, out int pMinor)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetDeviceBindingRevision(CSteamAPIContext.GetSteamInput(), inputHandle, out pMajor, out pMinor);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007504 File Offset: 0x00005704
		public static uint GetRemotePlaySessionID(InputHandle_t inputHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetRemotePlaySessionID(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00007516 File Offset: 0x00005716
		public static ushort GetSessionInputConfigurationSettings()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetSessionInputConfigurationSettings(CSteamAPIContext.GetSteamInput());
		}
	}
}
