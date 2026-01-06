using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000136 RID: 310
	internal class ForDeviceEventObservable : IObservable<InputEventPtr>
	{
		// Token: 0x06001110 RID: 4368 RVA: 0x000514F8 File Offset: 0x0004F6F8
		public ForDeviceEventObservable(IObservable<InputEventPtr> source, Type deviceType, InputDevice device)
		{
			this.m_Source = source;
			this.m_DeviceType = deviceType;
			this.m_Device = device;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00051515 File Offset: 0x0004F715
		public IDisposable Subscribe(IObserver<InputEventPtr> observer)
		{
			return this.m_Source.Subscribe(new ForDeviceEventObservable.ForDevice(this.m_DeviceType, this.m_Device, observer));
		}

		// Token: 0x040006C6 RID: 1734
		private IObservable<InputEventPtr> m_Source;

		// Token: 0x040006C7 RID: 1735
		private InputDevice m_Device;

		// Token: 0x040006C8 RID: 1736
		private Type m_DeviceType;

		// Token: 0x0200023F RID: 575
		private class ForDevice : IObserver<InputEventPtr>
		{
			// Token: 0x060015B8 RID: 5560 RVA: 0x000638F2 File Offset: 0x00061AF2
			public ForDevice(Type deviceType, InputDevice device, IObserver<InputEventPtr> observer)
			{
				this.m_Device = device;
				this.m_DeviceType = deviceType;
				this.m_Observer = observer;
			}

			// Token: 0x060015B9 RID: 5561 RVA: 0x0006390F File Offset: 0x00061B0F
			public void OnCompleted()
			{
			}

			// Token: 0x060015BA RID: 5562 RVA: 0x00063911 File Offset: 0x00061B11
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x060015BB RID: 5563 RVA: 0x0006391C File Offset: 0x00061B1C
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

			// Token: 0x04000C19 RID: 3097
			private IObserver<InputEventPtr> m_Observer;

			// Token: 0x04000C1A RID: 3098
			private InputDevice m_Device;

			// Token: 0x04000C1B RID: 3099
			private Type m_DeviceType;
		}
	}
}
