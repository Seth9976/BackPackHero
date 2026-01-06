using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngineInternal.Input;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EE RID: 238
	internal class NativeInputRuntime : IInputRuntime
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x00044B78 File Offset: 0x00042D78
		public int AllocateDeviceId()
		{
			return NativeInputSystem.AllocateDeviceId();
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00044B7F File Offset: 0x00042D7F
		public void Update(InputUpdateType updateType)
		{
			NativeInputSystem.Update((NativeInputUpdateType)updateType);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00044B87 File Offset: 0x00042D87
		public unsafe void QueueEvent(InputEvent* ptr)
		{
			NativeInputSystem.QueueInputEvent((IntPtr)((void*)ptr));
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00044B94 File Offset: 0x00042D94
		public unsafe long DeviceCommand(int deviceId, InputDeviceCommand* commandPtr)
		{
			if (commandPtr == null)
			{
				throw new ArgumentNullException("commandPtr");
			}
			return NativeInputSystem.IOCTL(deviceId, commandPtr->type, new IntPtr(commandPtr->payloadPtr), commandPtr->payloadSizeInBytes);
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00044BC8 File Offset: 0x00042DC8
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x00044BD0 File Offset: 0x00042DD0
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

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00044C17 File Offset: 0x00042E17
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00044C20 File Offset: 0x00042E20
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

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00044C67 File Offset: 0x00042E67
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x00044C70 File Offset: 0x00042E70
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

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00044CB7 File Offset: 0x00042EB7
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x00044CBE File Offset: 0x00042EBE
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

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00044CC6 File Offset: 0x00042EC6
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00044CCE File Offset: 0x00042ECE
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00044D06 File Offset: 0x00042F06
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x00044D0E File Offset: 0x00042F0E
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00044D46 File Offset: 0x00042F46
		public bool isPlayerFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00044D4D File Offset: 0x00042F4D
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00044D55 File Offset: 0x00042F55
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

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00044D64 File Offset: 0x00042F64
		public double currentTime
		{
			get
			{
				return NativeInputSystem.currentTime;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00044D6B File Offset: 0x00042F6B
		public double currentTimeForFixedUpdate
		{
			get
			{
				return (double)Time.fixedUnscaledTime + this.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00044D7A File Offset: 0x00042F7A
		public double currentTimeOffsetToRealtimeSinceStartup
		{
			get
			{
				return NativeInputSystem.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00044D81 File Offset: 0x00042F81
		public float unscaledGameTime
		{
			get
			{
				return Time.unscaledTime;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00044D88 File Offset: 0x00042F88
		public bool runInBackground
		{
			get
			{
				return Application.runInBackground || Application.platform == RuntimePlatform.PS5;
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00044D9C File Offset: 0x00042F9C
		private void OnShutdown()
		{
			this.m_ShutdownMethod();
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00044DA9 File Offset: 0x00042FA9
		private bool OnWantsToShutdown()
		{
			if (!this.m_DidCallOnShutdown)
			{
				this.OnShutdown();
				this.m_DidCallOnShutdown = true;
			}
			return true;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00044DC1 File Offset: 0x00042FC1
		private void OnFocusChanged(bool focus)
		{
			this.m_FocusChangedMethod(focus);
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00044DCF File Offset: 0x00042FCF
		public Vector2 screenSize
		{
			get
			{
				return new Vector2((float)Screen.width, (float)Screen.height);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00044DE2 File Offset: 0x00042FE2
		public ScreenOrientation screenOrientation
		{
			get
			{
				return Screen.orientation;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00044DE9 File Offset: 0x00042FE9
		public bool isInBatchMode
		{
			get
			{
				return Application.isBatchMode;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00044DF0 File Offset: 0x00042FF0
		public void RegisterAnalyticsEvent(string name, int maxPerHour, int maxPropertiesPerEvent)
		{
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00044DF2 File Offset: 0x00042FF2
		public void SendAnalyticsEvent(string name, object data)
		{
		}

		// Token: 0x040005AC RID: 1452
		public static readonly NativeInputRuntime instance = new NativeInputRuntime();

		// Token: 0x040005AD RID: 1453
		private Action m_ShutdownMethod;

		// Token: 0x040005AE RID: 1454
		private InputUpdateDelegate m_OnUpdate;

		// Token: 0x040005AF RID: 1455
		private Action<InputUpdateType> m_OnBeforeUpdate;

		// Token: 0x040005B0 RID: 1456
		private Func<InputUpdateType, bool> m_OnShouldRunUpdate;

		// Token: 0x040005B1 RID: 1457
		private float m_PollingFrequency = 60f;

		// Token: 0x040005B2 RID: 1458
		private bool m_DidCallOnShutdown;

		// Token: 0x040005B3 RID: 1459
		private Action<bool> m_FocusChangedMethod;
	}
}
