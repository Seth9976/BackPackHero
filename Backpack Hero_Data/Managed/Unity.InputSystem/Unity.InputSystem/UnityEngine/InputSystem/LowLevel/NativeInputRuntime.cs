using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngineInternal.Input;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EE RID: 238
	internal class NativeInputRuntime : IInputRuntime
	{
		// Token: 0x06000E01 RID: 3585 RVA: 0x00044BC0 File Offset: 0x00042DC0
		public int AllocateDeviceId()
		{
			return NativeInputSystem.AllocateDeviceId();
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00044BC7 File Offset: 0x00042DC7
		public void Update(InputUpdateType updateType)
		{
			NativeInputSystem.Update((NativeInputUpdateType)updateType);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00044BCF File Offset: 0x00042DCF
		public unsafe void QueueEvent(InputEvent* ptr)
		{
			NativeInputSystem.QueueInputEvent((IntPtr)((void*)ptr));
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00044BDC File Offset: 0x00042DDC
		public unsafe long DeviceCommand(int deviceId, InputDeviceCommand* commandPtr)
		{
			if (commandPtr == null)
			{
				throw new ArgumentNullException("commandPtr");
			}
			return NativeInputSystem.IOCTL(deviceId, commandPtr->type, new IntPtr(commandPtr->payloadPtr), commandPtr->payloadSizeInBytes);
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00044C10 File Offset: 0x00042E10
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x00044C18 File Offset: 0x00042E18
		public unsafe InputUpdateDelegate onUpdate
		{
			get
			{
				return this.m_OnUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onUpdate = delegate(NativeInputUpdateType updateType, NativeInputEventBuffer* eventBufferPtr)
					{
						InputEventBuffer inputEventBuffer = new InputEventBuffer((InputEvent*)eventBufferPtr->eventBuffer, eventBufferPtr->eventCount, eventBufferPtr->sizeInBytes, eventBufferPtr->capacityInBytes);
						try
						{
							value((InputUpdateType)updateType, ref inputEventBuffer);
						}
						catch (Exception ex)
						{
							Debug.LogException(ex);
							Debug.LogError(string.Format("{0} during event processing of {1} update; resetting event buffer", ex.GetType().Name, updateType));
							inputEventBuffer.Reset();
						}
						if (inputEventBuffer.eventCount > 0)
						{
							eventBufferPtr->eventCount = inputEventBuffer.eventCount;
							eventBufferPtr->sizeInBytes = (int)inputEventBuffer.sizeInBytes;
							eventBufferPtr->capacityInBytes = (int)inputEventBuffer.capacityInBytes;
							eventBufferPtr->eventBuffer = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(inputEventBuffer.data);
							return;
						}
						eventBufferPtr->eventCount = 0;
						eventBufferPtr->sizeInBytes = 0;
					};
				}
				else
				{
					NativeInputSystem.onUpdate = null;
				}
				this.m_OnUpdate = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00044C5F File Offset: 0x00042E5F
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x00044C68 File Offset: 0x00042E68
		public Action<InputUpdateType> onBeforeUpdate
		{
			get
			{
				return this.m_OnBeforeUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onBeforeUpdate = delegate(NativeInputUpdateType updateType)
					{
						value((InputUpdateType)updateType);
					};
				}
				else
				{
					NativeInputSystem.onBeforeUpdate = null;
				}
				this.m_OnBeforeUpdate = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00044CAF File Offset: 0x00042EAF
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00044CB8 File Offset: 0x00042EB8
		public Func<InputUpdateType, bool> onShouldRunUpdate
		{
			get
			{
				return this.m_OnShouldRunUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onShouldRunUpdate = (NativeInputUpdateType updateType) => value((InputUpdateType)updateType);
				}
				else
				{
					NativeInputSystem.onShouldRunUpdate = null;
				}
				this.m_OnShouldRunUpdate = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00044CFF File Offset: 0x00042EFF
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x00044D06 File Offset: 0x00042F06
		public Action<int, string> onDeviceDiscovered
		{
			get
			{
				return NativeInputSystem.onDeviceDiscovered;
			}
			set
			{
				NativeInputSystem.onDeviceDiscovered = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00044D0E File Offset: 0x00042F0E
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x00044D16 File Offset: 0x00042F16
		public Action onShutdown
		{
			get
			{
				return this.m_ShutdownMethod;
			}
			set
			{
				if (value == null)
				{
					Application.quitting -= this.OnShutdown;
				}
				else if (this.m_ShutdownMethod == null)
				{
					Application.quitting += this.OnShutdown;
				}
				this.m_ShutdownMethod = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00044D4E File Offset: 0x00042F4E
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x00044D56 File Offset: 0x00042F56
		public Action<bool> onPlayerFocusChanged
		{
			get
			{
				return this.m_FocusChangedMethod;
			}
			set
			{
				if (value == null)
				{
					Application.focusChanged -= this.OnFocusChanged;
				}
				else if (this.m_FocusChangedMethod == null)
				{
					Application.focusChanged += this.OnFocusChanged;
				}
				this.m_FocusChangedMethod = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00044D8E File Offset: 0x00042F8E
		public bool isPlayerFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00044D95 File Offset: 0x00042F95
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00044D9D File Offset: 0x00042F9D
		public float pollingFrequency
		{
			get
			{
				return this.m_PollingFrequency;
			}
			set
			{
				this.m_PollingFrequency = value;
				NativeInputSystem.SetPollingFrequency(value);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00044DAC File Offset: 0x00042FAC
		public double currentTime
		{
			get
			{
				return NativeInputSystem.currentTime;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00044DB3 File Offset: 0x00042FB3
		public double currentTimeForFixedUpdate
		{
			get
			{
				return (double)Time.fixedUnscaledTime + this.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00044DC2 File Offset: 0x00042FC2
		public double currentTimeOffsetToRealtimeSinceStartup
		{
			get
			{
				return NativeInputSystem.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00044DC9 File Offset: 0x00042FC9
		public float unscaledGameTime
		{
			get
			{
				return Time.unscaledTime;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00044DD0 File Offset: 0x00042FD0
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x00044DE1 File Offset: 0x00042FE1
		public bool runInBackground
		{
			get
			{
				return Application.runInBackground || this.m_RunInBackground;
			}
			set
			{
				this.m_RunInBackground = value;
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00044DEA File Offset: 0x00042FEA
		private void OnShutdown()
		{
			this.m_ShutdownMethod();
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00044DF7 File Offset: 0x00042FF7
		private bool OnWantsToShutdown()
		{
			if (!this.m_DidCallOnShutdown)
			{
				this.OnShutdown();
				this.m_DidCallOnShutdown = true;
			}
			return true;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00044E0F File Offset: 0x0004300F
		private void OnFocusChanged(bool focus)
		{
			this.m_FocusChangedMethod(focus);
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00044E1D File Offset: 0x0004301D
		public Vector2 screenSize
		{
			get
			{
				return new Vector2((float)Screen.width, (float)Screen.height);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00044E30 File Offset: 0x00043030
		public ScreenOrientation screenOrientation
		{
			get
			{
				return Screen.orientation;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00044E37 File Offset: 0x00043037
		public bool isInBatchMode
		{
			get
			{
				return Application.isBatchMode;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00044E3E File Offset: 0x0004303E
		public void RegisterAnalyticsEvent(string name, int maxPerHour, int maxPropertiesPerEvent)
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00044E40 File Offset: 0x00043040
		public void SendAnalyticsEvent(string name, object data)
		{
		}

		// Token: 0x040005AC RID: 1452
		public static readonly NativeInputRuntime instance = new NativeInputRuntime();

		// Token: 0x040005AD RID: 1453
		private bool m_RunInBackground;

		// Token: 0x040005AE RID: 1454
		private Action m_ShutdownMethod;

		// Token: 0x040005AF RID: 1455
		private InputUpdateDelegate m_OnUpdate;

		// Token: 0x040005B0 RID: 1456
		private Action<InputUpdateType> m_OnBeforeUpdate;

		// Token: 0x040005B1 RID: 1457
		private Func<InputUpdateType, bool> m_OnShouldRunUpdate;

		// Token: 0x040005B2 RID: 1458
		private float m_PollingFrequency = 60f;

		// Token: 0x040005B3 RID: 1459
		private bool m_DidCallOnShutdown;

		// Token: 0x040005B4 RID: 1460
		private Action<bool> m_FocusChangedMethod;
	}
}
