using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000058 RID: 88
	public class InputSettings : ScriptableObject
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00031A67 File Offset: 0x0002FC67
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00031A6F File Offset: 0x0002FC6F
		public InputSettings.UpdateMode updateMode
		{
			get
			{
				return this.m_UpdateMode;
			}
			set
			{
				if (this.m_UpdateMode == value)
				{
					return;
				}
				this.m_UpdateMode = value;
				this.OnChange();
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00031A88 File Offset: 0x0002FC88
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00031A90 File Offset: 0x0002FC90
		public bool compensateForScreenOrientation
		{
			get
			{
				return this.m_CompensateForScreenOrientation;
			}
			set
			{
				if (this.m_CompensateForScreenOrientation == value)
				{
					return;
				}
				this.m_CompensateForScreenOrientation = value;
				this.OnChange();
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00031AA9 File Offset: 0x0002FCA9
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00031AAC File Offset: 0x0002FCAC
		[Obsolete("filterNoiseOnCurrent is deprecated, filtering of noise is always enabled now.", false)]
		public bool filterNoiseOnCurrent
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00031AAE File Offset: 0x0002FCAE
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00031AB6 File Offset: 0x0002FCB6
		public float defaultDeadzoneMin
		{
			get
			{
				return this.m_DefaultDeadzoneMin;
			}
			set
			{
				if (this.m_DefaultDeadzoneMin == value)
				{
					return;
				}
				this.m_DefaultDeadzoneMin = value;
				this.OnChange();
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00031ACF File Offset: 0x0002FCCF
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00031AD7 File Offset: 0x0002FCD7
		public float defaultDeadzoneMax
		{
			get
			{
				return this.m_DefaultDeadzoneMax;
			}
			set
			{
				if (this.m_DefaultDeadzoneMax == value)
				{
					return;
				}
				this.m_DefaultDeadzoneMax = value;
				this.OnChange();
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00031AF0 File Offset: 0x0002FCF0
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00031AF8 File Offset: 0x0002FCF8
		public float defaultButtonPressPoint
		{
			get
			{
				return this.m_DefaultButtonPressPoint;
			}
			set
			{
				if (this.m_DefaultButtonPressPoint == value)
				{
					return;
				}
				this.m_DefaultButtonPressPoint = Mathf.Clamp(value, 0.0001f, float.MaxValue);
				this.OnChange();
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00031B20 File Offset: 0x0002FD20
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00031B28 File Offset: 0x0002FD28
		public float buttonReleaseThreshold
		{
			get
			{
				return this.m_ButtonReleaseThreshold;
			}
			set
			{
				if (this.m_ButtonReleaseThreshold == value)
				{
					return;
				}
				this.m_ButtonReleaseThreshold = value;
				this.OnChange();
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00031B41 File Offset: 0x0002FD41
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00031B49 File Offset: 0x0002FD49
		public float defaultTapTime
		{
			get
			{
				return this.m_DefaultTapTime;
			}
			set
			{
				if (this.m_DefaultTapTime == value)
				{
					return;
				}
				this.m_DefaultTapTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00031B62 File Offset: 0x0002FD62
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00031B6A File Offset: 0x0002FD6A
		public float defaultSlowTapTime
		{
			get
			{
				return this.m_DefaultSlowTapTime;
			}
			set
			{
				if (this.m_DefaultSlowTapTime == value)
				{
					return;
				}
				this.m_DefaultSlowTapTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00031B83 File Offset: 0x0002FD83
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00031B8B File Offset: 0x0002FD8B
		public float defaultHoldTime
		{
			get
			{
				return this.m_DefaultHoldTime;
			}
			set
			{
				if (this.m_DefaultHoldTime == value)
				{
					return;
				}
				this.m_DefaultHoldTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00031BA4 File Offset: 0x0002FDA4
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00031BAC File Offset: 0x0002FDAC
		public float tapRadius
		{
			get
			{
				return this.m_TapRadius;
			}
			set
			{
				if (this.m_TapRadius == value)
				{
					return;
				}
				this.m_TapRadius = value;
				this.OnChange();
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00031BC5 File Offset: 0x0002FDC5
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00031BCD File Offset: 0x0002FDCD
		public float multiTapDelayTime
		{
			get
			{
				return this.m_MultiTapDelayTime;
			}
			set
			{
				if (this.m_MultiTapDelayTime == value)
				{
					return;
				}
				this.m_MultiTapDelayTime = value;
				this.OnChange();
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00031BE6 File Offset: 0x0002FDE6
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00031BEE File Offset: 0x0002FDEE
		public InputSettings.BackgroundBehavior backgroundBehavior
		{
			get
			{
				return this.m_BackgroundBehavior;
			}
			set
			{
				if (this.m_BackgroundBehavior == value)
				{
					return;
				}
				this.m_BackgroundBehavior = value;
				this.OnChange();
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00031C07 File Offset: 0x0002FE07
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00031C0F File Offset: 0x0002FE0F
		public InputSettings.EditorInputBehaviorInPlayMode editorInputBehaviorInPlayMode
		{
			get
			{
				return this.m_EditorInputBehaviorInPlayMode;
			}
			set
			{
				if (this.m_EditorInputBehaviorInPlayMode == value)
				{
					return;
				}
				this.m_EditorInputBehaviorInPlayMode = value;
				this.OnChange();
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00031C28 File Offset: 0x0002FE28
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00031C30 File Offset: 0x0002FE30
		public int maxEventBytesPerUpdate
		{
			get
			{
				return this.m_MaxEventBytesPerUpdate;
			}
			set
			{
				if (this.m_MaxEventBytesPerUpdate == value)
				{
					return;
				}
				this.m_MaxEventBytesPerUpdate = value;
				this.OnChange();
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00031C49 File Offset: 0x0002FE49
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00031C51 File Offset: 0x0002FE51
		public int maxQueuedEventsPerUpdate
		{
			get
			{
				return this.m_MaxQueuedEventsPerUpdate;
			}
			set
			{
				if (this.m_MaxQueuedEventsPerUpdate == value)
				{
					return;
				}
				this.m_MaxQueuedEventsPerUpdate = value;
				this.OnChange();
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00031C6A File Offset: 0x0002FE6A
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x00031C78 File Offset: 0x0002FE78
		public ReadOnlyArray<string> supportedDevices
		{
			get
			{
				return new ReadOnlyArray<string>(this.m_SupportedDevices);
			}
			set
			{
				if (this.supportedDevices.Count == value.Count)
				{
					bool flag = false;
					for (int i = 0; i < this.supportedDevices.Count; i++)
					{
						if (this.m_SupportedDevices[i] != value[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return;
					}
				}
				this.m_SupportedDevices = value.ToArray();
				this.OnChange();
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00031CE9 File Offset: 0x0002FEE9
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x00031CF1 File Offset: 0x0002FEF1
		public bool disableRedundantEventsMerging
		{
			get
			{
				return this.m_DisableRedundantEventsMerging;
			}
			set
			{
				if (this.m_DisableRedundantEventsMerging == value)
				{
					return;
				}
				this.m_DisableRedundantEventsMerging = value;
				this.OnChange();
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00031D0A File Offset: 0x0002FF0A
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x00031D12 File Offset: 0x0002FF12
		public bool shortcutKeysConsumeInput
		{
			get
			{
				return this.m_ShortcutKeysConsumeInputs;
			}
			set
			{
				if (this.m_ShortcutKeysConsumeInputs == value)
				{
					return;
				}
				this.m_ShortcutKeysConsumeInputs = value;
				this.OnChange();
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00031D2C File Offset: 0x0002FF2C
		public void SetInternalFeatureFlag(string featureName, bool enabled)
		{
			if (string.IsNullOrEmpty(featureName))
			{
				throw new ArgumentNullException("featureName");
			}
			if (!(featureName == "USE_OPTIMIZED_CONTROLS"))
			{
				if (!(featureName == "USE_READ_VALUE_CACHING"))
				{
					if (!(featureName == "PARANOID_READ_VALUE_CACHING_CHECKS"))
					{
						if (this.m_FeatureFlags == null)
						{
							this.m_FeatureFlags = new HashSet<string>();
						}
						if (enabled)
						{
							this.m_FeatureFlags.Add(featureName.ToUpperInvariant());
						}
						else
						{
							this.m_FeatureFlags.Remove(featureName.ToUpperInvariant());
						}
					}
					else
					{
						InputSettings.paranoidReadValueCachingChecksEnabled = enabled;
					}
				}
				else
				{
					InputSettings.readValueCachingFeatureEnabled = enabled;
				}
			}
			else
			{
				InputSettings.optimizedControlsFeatureEnabled = enabled;
			}
			this.OnChange();
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00031DCF File Offset: 0x0002FFCF
		internal bool IsFeatureEnabled(string featureName)
		{
			return this.m_FeatureFlags != null && this.m_FeatureFlags.Contains(featureName.ToUpperInvariant());
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00031DEC File Offset: 0x0002FFEC
		internal void OnChange()
		{
			if (InputSystem.settings == this)
			{
				InputSystem.s_Manager.ApplySettings();
			}
		}

		// Token: 0x04000289 RID: 649
		[Tooltip("Determine which type of devices are used by the application. By default, this is empty meaning that all devices recognized by Unity will be used. Restricting the set of supported devices will make only those devices appear in the input system.")]
		[SerializeField]
		private string[] m_SupportedDevices;

		// Token: 0x0400028A RID: 650
		[Tooltip("Determine when Unity processes events. By default, accumulated input events are flushed out before each fixed update and before each dynamic update. This setting can be used to restrict event processing to only where the application needs it.")]
		[SerializeField]
		private InputSettings.UpdateMode m_UpdateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;

		// Token: 0x0400028B RID: 651
		[SerializeField]
		private int m_MaxEventBytesPerUpdate = 5242880;

		// Token: 0x0400028C RID: 652
		[SerializeField]
		private int m_MaxQueuedEventsPerUpdate = 1000;

		// Token: 0x0400028D RID: 653
		[SerializeField]
		private bool m_CompensateForScreenOrientation = true;

		// Token: 0x0400028E RID: 654
		[SerializeField]
		private InputSettings.BackgroundBehavior m_BackgroundBehavior;

		// Token: 0x0400028F RID: 655
		[SerializeField]
		private InputSettings.EditorInputBehaviorInPlayMode m_EditorInputBehaviorInPlayMode;

		// Token: 0x04000290 RID: 656
		[SerializeField]
		private float m_DefaultDeadzoneMin = 0.125f;

		// Token: 0x04000291 RID: 657
		[SerializeField]
		private float m_DefaultDeadzoneMax = 0.925f;

		// Token: 0x04000292 RID: 658
		[Min(0.0001f)]
		[SerializeField]
		private float m_DefaultButtonPressPoint = 0.5f;

		// Token: 0x04000293 RID: 659
		[SerializeField]
		private float m_ButtonReleaseThreshold = 0.75f;

		// Token: 0x04000294 RID: 660
		[SerializeField]
		private float m_DefaultTapTime = 0.2f;

		// Token: 0x04000295 RID: 661
		[SerializeField]
		private float m_DefaultSlowTapTime = 0.5f;

		// Token: 0x04000296 RID: 662
		[SerializeField]
		private float m_DefaultHoldTime = 0.4f;

		// Token: 0x04000297 RID: 663
		[SerializeField]
		private float m_TapRadius = 5f;

		// Token: 0x04000298 RID: 664
		[SerializeField]
		private float m_MultiTapDelayTime = 0.75f;

		// Token: 0x04000299 RID: 665
		[SerializeField]
		private bool m_DisableRedundantEventsMerging;

		// Token: 0x0400029A RID: 666
		[SerializeField]
		private bool m_ShortcutKeysConsumeInputs;

		// Token: 0x0400029B RID: 667
		[NonSerialized]
		internal HashSet<string> m_FeatureFlags;

		// Token: 0x0400029C RID: 668
		internal static bool optimizedControlsFeatureEnabled;

		// Token: 0x0400029D RID: 669
		internal static bool readValueCachingFeatureEnabled;

		// Token: 0x0400029E RID: 670
		internal static bool paranoidReadValueCachingChecksEnabled;

		// Token: 0x0400029F RID: 671
		internal const int s_OldUnsupportedFixedAndDynamicUpdateSetting = 0;

		// Token: 0x020001AB RID: 427
		public enum UpdateMode
		{
			// Token: 0x040008D6 RID: 2262
			ProcessEventsInDynamicUpdate = 1,
			// Token: 0x040008D7 RID: 2263
			ProcessEventsInFixedUpdate,
			// Token: 0x040008D8 RID: 2264
			ProcessEventsManually
		}

		// Token: 0x020001AC RID: 428
		public enum BackgroundBehavior
		{
			// Token: 0x040008DA RID: 2266
			ResetAndDisableNonBackgroundDevices,
			// Token: 0x040008DB RID: 2267
			ResetAndDisableAllDevices,
			// Token: 0x040008DC RID: 2268
			IgnoreFocus
		}

		// Token: 0x020001AD RID: 429
		public enum EditorInputBehaviorInPlayMode
		{
			// Token: 0x040008DE RID: 2270
			PointersAndKeyboardsRespectGameViewFocus,
			// Token: 0x040008DF RID: 2271
			AllDevicesRespectGameViewFocus,
			// Token: 0x040008E0 RID: 2272
			AllDeviceInputAlwaysGoesToGameView
		}
	}
}
