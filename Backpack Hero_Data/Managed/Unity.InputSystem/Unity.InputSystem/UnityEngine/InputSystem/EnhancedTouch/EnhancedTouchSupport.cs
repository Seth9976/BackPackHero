using System;
using System.Diagnostics;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000096 RID: 150
	public static class EnhancedTouchSupport
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0003EE57 File Offset: 0x0003D057
		public static bool enabled
		{
			get
			{
				return EnhancedTouchSupport.s_Enabled > 0;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0003EE64 File Offset: 0x0003D064
		public static void Enable()
		{
			EnhancedTouchSupport.s_Enabled++;
			if (EnhancedTouchSupport.s_Enabled > 1)
			{
				return;
			}
			InputSystem.onDeviceChange += EnhancedTouchSupport.OnDeviceChange;
			InputSystem.onBeforeUpdate += Touch.BeginUpdate;
			InputSystem.onSettingsChange += EnhancedTouchSupport.OnSettingsChange;
			EnhancedTouchSupport.SetUpState();
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0003EEC0 File Offset: 0x0003D0C0
		public static void Disable()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				return;
			}
			EnhancedTouchSupport.s_Enabled--;
			if (EnhancedTouchSupport.s_Enabled > 0)
			{
				return;
			}
			InputSystem.onDeviceChange -= EnhancedTouchSupport.OnDeviceChange;
			InputSystem.onBeforeUpdate -= Touch.BeginUpdate;
			InputSystem.onSettingsChange -= EnhancedTouchSupport.OnSettingsChange;
			EnhancedTouchSupport.TearDownState();
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0003EF22 File Offset: 0x0003D122
		internal static void Reset()
		{
			Touch.s_GlobalState.touchscreens = default(InlinedArray<Touchscreen>);
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
			EnhancedTouchSupport.s_Enabled = 0;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0003EF5C File Offset: 0x0003D15C
		private static void SetUpState()
		{
			Touch.s_GlobalState.playerState.updateMask = InputUpdateType.Dynamic | InputUpdateType.Fixed | InputUpdateType.Manual;
			EnhancedTouchSupport.s_UpdateMode = InputSystem.settings.updateMode;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Added);
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
		internal static void TearDownState()
		{
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Removed);
			}
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0003F044 File Offset: 0x0003D244
		private static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					return;
				}
				Touchscreen touchscreen = device as Touchscreen;
				if (touchscreen != null)
				{
					Touch.RemoveTouchscreen(touchscreen);
				}
			}
			else
			{
				Touchscreen touchscreen2 = device as Touchscreen;
				if (touchscreen2 != null)
				{
					Touch.AddTouchscreen(touchscreen2);
					return;
				}
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0003F07C File Offset: 0x0003D27C
		private static void OnSettingsChange()
		{
			InputSettings.UpdateMode updateMode = InputSystem.settings.updateMode;
			if (EnhancedTouchSupport.s_UpdateMode == updateMode)
			{
				return;
			}
			EnhancedTouchSupport.TearDownState();
			EnhancedTouchSupport.SetUpState();
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003F0A7 File Offset: 0x0003D2A7
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void CheckEnabled()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				throw new InvalidOperationException("EnhancedTouch API is not enabled; call EnhancedTouchSupport.Enable()");
			}
		}

		// Token: 0x04000431 RID: 1073
		private static int s_Enabled;

		// Token: 0x04000432 RID: 1074
		private static InputSettings.UpdateMode s_UpdateMode;
	}
}
