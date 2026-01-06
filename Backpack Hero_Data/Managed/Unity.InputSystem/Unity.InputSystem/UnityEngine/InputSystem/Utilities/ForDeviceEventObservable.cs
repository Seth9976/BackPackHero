using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000136 RID: 310
	internal class ForDeviceEventObservable : IObservable<InputEventPtr>
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x0005170C File Offset: 0x0004F90C
		public ForDeviceEventObservable(IObservable<InputEventPtr> source, Type deviceType, InputDevice device)
		{
			this.m_Source = source;
			this.m_DeviceType = deviceType;
			this.m_Device = device;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00051729 File Offset: 0x0004F929
		public IDisposable Subscribe(IObserver<InputEventPtr> observer)
		{
			return this.m_Source.Subscribe(new ForDeviceEventObservable.ForDevice(this.m_DeviceType, this.m_Device, observer));
		}

		// Token: 0x040006C7 RID: 1735
		private IObservable<InputEventPtr> m_Source;

		// Token: 0x040006C8 RID: 1736
		private InputDevice m_Device;

		// Token: 0x040006C9 RID: 1737
		private Type m_DeviceType;

		// Token: 0x0200023F RID: 575
		private class ForDevice : IObserver<InputEventPtr>
		{
			// Token: 0x060015BF RID: 5567 RVA: 0x00063B06 File Offset: 0x00061D06
			public ForDevice(Type deviceType, InputDevice device, IObserver<InputEventPtr> observer)
			{
				this.m_Device = device;
				this.m_DeviceType = deviceType;
				this.m_Observer = observer;
			}

			// Token: 0x060015C0 RID: 5568 RVA: 0x00063B23 File Offset: 0x00061D23
			public void OnCompleted()
			{
			}

			// Token: 0x060015C1 RID: 5569 RVA: 0x00063B25 File Offset: 0x00061D25
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015C2 RID: 5570 RVA: 0x00063B30 File Offset: 0x00061D30
			public void OnNext(InputEventPtr value)
			{
				if (this.m_DeviceType != null)
				{
					InputDevice deviceById = InputSystem.GetDeviceById(value.deviceId);
					if (deviceById == null)
					{
						return;
					}
					if (!this.m_DeviceType.IsInstanceOfType(deviceById))
					{
						return;
					}
				}
				if (this.m_Device != null && value.deviceId != this.m_Device.deviceId)
				{
					return;
				}
				this.m_Observer.OnNext(value);
			}

			// Token: 0x04000C1A RID: 3098
			private IObserver<InputEventPtr> m_Observer;

			// Token: 0x04000C1B RID: 3099
			private InputDevice m_Device;

			// Token: 0x04000C1C RID: 3100
			private Type m_DeviceType;
		}
	}
}
