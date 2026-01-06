using System;
using Unity.Collections;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000091 RID: 145
	public abstract class OnScreenControl : MonoBehaviour
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0003D3B6 File Offset: 0x0003B5B6
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0003D3BE File Offset: 0x0003B5BE
		public string controlPath
		{
			get
			{
				return this.controlPathInternal;
			}
			set
			{
				this.controlPathInternal = value;
				if (base.isActiveAndEnabled)
				{
					this.SetupInputControl();
				}
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0003D3D5 File Offset: 0x0003B5D5
		public InputControl control
		{
			get
			{
				return this.m_Control;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B85 RID: 2949
		// (set) Token: 0x06000B86 RID: 2950
		protected abstract string controlPathInternal { get; set; }

		// Token: 0x06000B87 RID: 2951 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		private void SetupInputControl()
		{
			string controlPathInternal = this.controlPathInternal;
			if (string.IsNullOrEmpty(controlPathInternal))
			{
				return;
			}
			string text = InputControlPath.TryGetDeviceLayout(controlPathInternal);
			if (text == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Cannot determine device layout to use based on control path '",
					controlPathInternal,
					"' used in ",
					base.GetType().Name,
					" component"
				}), this);
				return;
			}
			InternedString internedString = new InternedString(text);
			int num = -1;
			for (int i = 0; i < OnScreenControl.s_OnScreenDevices.length; i++)
			{
				if (OnScreenControl.s_OnScreenDevices[i].device.m_Layout == internedString)
				{
					num = i;
					break;
				}
			}
			InputDevice inputDevice;
			if (num == -1)
			{
				try
				{
					inputDevice = InputSystem.AddDevice(text, null, null);
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Could not create device with layout '",
						text,
						"' used in '",
						base.GetType().Name,
						"' component"
					}));
					Debug.LogException(ex);
					return;
				}
				InputSystem.AddDeviceUsage(inputDevice, "OnScreen");
				InputEventPtr inputEventPtr;
				NativeArray<byte> nativeArray = StateEvent.From(inputDevice, out inputEventPtr, Allocator.Persistent);
				num = OnScreenControl.s_OnScreenDevices.Append(new OnScreenControl.OnScreenDeviceInfo
				{
					eventPtr = inputEventPtr,
					buffer = nativeArray,
					device = inputDevice
				});
			}
			else
			{
				inputDevice = OnScreenControl.s_OnScreenDevices[num].device;
			}
			this.m_Control = InputControlPath.TryFindControl(inputDevice, controlPathInternal, 0);
			if (this.m_Control == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Cannot find control with path '",
					controlPathInternal,
					"' on device of type '",
					text,
					"' referenced by component '",
					base.GetType().Name,
					"'"
				}), this);
				if (OnScreenControl.s_OnScreenDevices[num].firstControl == null)
				{
					OnScreenControl.s_OnScreenDevices[num].Destroy();
					OnScreenControl.s_OnScreenDevices.RemoveAt(num);
				}
				return;
			}
			this.m_InputEventPtr = OnScreenControl.s_OnScreenDevices[num].eventPtr;
			OnScreenControl.s_OnScreenDevices[num] = OnScreenControl.s_OnScreenDevices[num].AddControl(this);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0003D61C File Offset: 0x0003B81C
		protected void SendValueToControl<TValue>(TValue value) where TValue : struct
		{
			if (this.m_Control == null)
			{
				return;
			}
			InputControl<TValue> inputControl = this.m_Control as InputControl<TValue>;
			if (inputControl == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"The control path ",
					this.controlPath,
					" yields a control of type ",
					this.m_Control.GetType().Name,
					" which is not an InputControl with value type ",
					typeof(TValue).Name
				}), "value");
			}
			this.m_InputEventPtr.internalTime = InputRuntime.s_Instance.currentTime;
			inputControl.WriteValueIntoEvent(value, this.m_InputEventPtr);
			InputSystem.QueueEvent(this.m_InputEventPtr);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003D6CA File Offset: 0x0003B8CA
		protected void SentDefaultValueToControl()
		{
			if (this.m_Control == null)
			{
				return;
			}
			this.m_InputEventPtr.internalTime = InputRuntime.s_Instance.currentTime;
			this.m_Control.ResetToDefaultStateInEvent(this.m_InputEventPtr);
			InputSystem.QueueEvent(this.m_InputEventPtr);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003D707 File Offset: 0x0003B907
		protected virtual void OnEnable()
		{
			this.SetupInputControl();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0003D710 File Offset: 0x0003B910
		protected virtual void OnDisable()
		{
			if (this.m_Control == null)
			{
				return;
			}
			InputDevice device = this.m_Control.device;
			for (int i = 0; i < OnScreenControl.s_OnScreenDevices.length; i++)
			{
				if (OnScreenControl.s_OnScreenDevices[i].device == device)
				{
					OnScreenControl.OnScreenDeviceInfo onScreenDeviceInfo = OnScreenControl.s_OnScreenDevices[i].RemoveControl(this);
					if (onScreenDeviceInfo.firstControl == null)
					{
						OnScreenControl.s_OnScreenDevices[i].Destroy();
						OnScreenControl.s_OnScreenDevices.RemoveAt(i);
					}
					else
					{
						OnScreenControl.s_OnScreenDevices[i] = onScreenDeviceInfo;
						if (!this.m_Control.CheckStateIsAtDefault())
						{
							this.SentDefaultValueToControl();
						}
					}
					this.m_Control = null;
					this.m_InputEventPtr = default(InputEventPtr);
					return;
				}
			}
		}

		// Token: 0x0400041C RID: 1052
		private InputControl m_Control;

		// Token: 0x0400041D RID: 1053
		private OnScreenControl m_NextControlOnDevice;

		// Token: 0x0400041E RID: 1054
		private InputEventPtr m_InputEventPtr;

		// Token: 0x0400041F RID: 1055
		private static InlinedArray<OnScreenControl.OnScreenDeviceInfo> s_OnScreenDevices;

		// Token: 0x020001D7 RID: 471
		private struct OnScreenDeviceInfo
		{
			// Token: 0x06001423 RID: 5155 RVA: 0x0005CFEE File Offset: 0x0005B1EE
			public OnScreenControl.OnScreenDeviceInfo AddControl(OnScreenControl control)
			{
				control.m_NextControlOnDevice = this.firstControl;
				this.firstControl = control;
				return this;
			}

			// Token: 0x06001424 RID: 5156 RVA: 0x0005D00C File Offset: 0x0005B20C
			public OnScreenControl.OnScreenDeviceInfo RemoveControl(OnScreenControl control)
			{
				if (this.firstControl == control)
				{
					this.firstControl = control.m_NextControlOnDevice;
				}
				else
				{
					OnScreenControl onScreenControl = this.firstControl.m_NextControlOnDevice;
					OnScreenControl onScreenControl2 = this.firstControl;
					while (onScreenControl != null)
					{
						if (!(onScreenControl != control))
						{
							onScreenControl2.m_NextControlOnDevice = onScreenControl.m_NextControlOnDevice;
							break;
						}
						onScreenControl2 = onScreenControl;
						onScreenControl = onScreenControl.m_NextControlOnDevice;
					}
				}
				control.m_NextControlOnDevice = null;
				return this;
			}

			// Token: 0x06001425 RID: 5157 RVA: 0x0005D080 File Offset: 0x0005B280
			public void Destroy()
			{
				if (this.buffer.IsCreated)
				{
					this.buffer.Dispose();
				}
				if (this.device != null)
				{
					InputSystem.RemoveDevice(this.device);
				}
				this.device = null;
				this.buffer = default(NativeArray<byte>);
			}

			// Token: 0x0400099E RID: 2462
			public InputEventPtr eventPtr;

			// Token: 0x0400099F RID: 2463
			public NativeArray<byte> buffer;

			// Token: 0x040009A0 RID: 2464
			public InputDevice device;

			// Token: 0x040009A1 RID: 2465
			public OnScreenControl firstControl;
		}
	}
}
