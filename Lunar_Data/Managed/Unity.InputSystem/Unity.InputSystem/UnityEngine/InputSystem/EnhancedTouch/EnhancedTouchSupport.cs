using System;
using System.Diagnostics;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000096 RID: 150
	public static class EnhancedTouchSupport
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0003EE1B File Offset: 0x0003D01B
		public static bool enabled
		{
			get
			{
				return EnhancedTouchSupport.s_Enabled > 0;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003EE28 File Offset: 0x0003D028
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

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0003EE84 File Offset: 0x0003D084
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

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0003EEE6 File Offset: 0x0003D0E6
		internal static void Reset()
		{
			Touch.s_GlobalState.touchscreens = default(InlinedArray<Touchscreen>);
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
			EnhancedTouchSupport.s_Enabled = 0;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0003EF20 File Offset: 0x0003D120
		private static void SetUpState()
		{
			Touch.s_GlobalState.playerState.updateMask = InputUpdateType.Dynamic | InputUpdateType.Fixed | InputUpdateType.Manual;
			EnhancedTouchSupport.s_UpdateMode = InputSystem.settings.updateMode;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Added);
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0003EF94 File Offset: 0x0003D194
		internal static void TearDownState()
		{
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Removed);
			}
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0003F008 File Offset: 0x0003D208
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

		// Token: 0x06000BBC RID: 3004 RVA: 0x0003F040 File Offset: 0x0003D240
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

		// Token: 0x06000BBD RID: 3005 RVA: 0x0003F06B File Offset: 0x0003D26B
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
