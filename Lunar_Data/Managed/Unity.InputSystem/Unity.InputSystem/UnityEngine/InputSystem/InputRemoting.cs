using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000043 RID: 67
	public sealed class InputRemoting : IObservable<InputRemoting.Message>, IObserver<InputRemoting.Message>
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0002BAB6 File Offset: 0x00029CB6
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0002BAC3 File Offset: 0x00029CC3
		public bool sending
		{
			get
			{
				return (this.m_Flags & InputRemoting.Flags.Sending) == InputRemoting.Flags.Sending;
			}
			private set
			{
				if (value)
				{
					this.m_Flags |= InputRemoting.Flags.Sending;
					return;
				}
				this.m_Flags &= ~InputRemoting.Flags.Sending;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0002BAE6 File Offset: 0x00029CE6
		internal InputRemoting(InputManager manager, bool startSendingOnConnect = false)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			this.m_LocalManager = manager;
			if (startSendingOnConnect)
			{
				this.m_Flags |= InputRemoting.Flags.StartSendingOnConnect;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002BB14 File Offset: 0x00029D14
		public void StartSending()
		{
			if (this.sending)
			{
				return;
			}
			this.m_LocalManager.onEvent += this.SendEvent;
			this.m_LocalManager.onDeviceChange += this.SendDeviceChange;
			this.m_LocalManager.onLayoutChange += this.SendLayoutChange;
			this.sending = true;
			this.SendInitialMessages();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0002BB7C File Offset: 0x00029D7C
		public void StopSending()
		{
			if (!this.sending)
			{
				return;
			}
			this.m_LocalManager.onEvent -= this.SendEvent;
			this.m_LocalManager.onDeviceChange -= this.SendDeviceChange;
			this.m_LocalManager.onLayoutChange -= this.SendLayoutChange;
			this.sending = false;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0002BBE0 File Offset: 0x00029DE0
		void IObserver<InputRemoting.Message>.OnNext(InputRemoting.Message msg)
		{
			switch (msg.type)
			{
			case InputRemoting.MessageType.Connect:
				InputRemoting.ConnectMsg.Process(this);
				return;
			case InputRemoting.MessageType.Disconnect:
				InputRemoting.DisconnectMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewLayout:
				InputRemoting.NewLayoutMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewDevice:
				InputRemoting.NewDeviceMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewEvents:
				InputRemoting.NewEventsMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.RemoveDevice:
				InputRemoting.RemoveDeviceMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.RemoveLayout:
				break;
			case InputRemoting.MessageType.ChangeUsages:
				InputRemoting.ChangeUsageMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.StartSending:
				InputRemoting.StartSendingMsg.Process(this);
				return;
			case InputRemoting.MessageType.StopSending:
				InputRemoting.StopSendingMsg.Process(this);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0002BC67 File Offset: 0x00029E67
		void IObserver<InputRemoting.Message>.OnError(Exception error)
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0002BC69 File Offset: 0x00029E69
		void IObserver<InputRemoting.Message>.OnCompleted()
		{
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0002BC6C File Offset: 0x00029E6C
		public IDisposable Subscribe(IObserver<InputRemoting.Message> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			InputRemoting.Subscriber subscriber = new InputRemoting.Subscriber
			{
				owner = this,
				observer = observer
			};
			ArrayHelpers.Append<InputRemoting.Subscriber>(ref this.m_Subscribers, subscriber);
			return subscriber;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002BCA9 File Offset: 0x00029EA9
		private void SendInitialMessages()
		{
			this.SendAllGeneratedLayouts();
			this.SendAllDevices();
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002BCB8 File Offset: 0x00029EB8
		private void SendAllGeneratedLayouts()
		{
			foreach (KeyValuePair<InternedString, Func<InputControlLayout>> keyValuePair in this.m_LocalManager.m_Layouts.layoutBuilders)
			{
				this.SendLayout(keyValuePair.Key);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002BD20 File Offset: 0x00029F20
		private void SendLayout(string layoutName)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			InputRemoting.Message? message = InputRemoting.NewLayoutMsg.Create(this, layoutName);
			if (message != null)
			{
				this.Send(message.Value);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002BD54 File Offset: 0x00029F54
		private void SendAllDevices()
		{
			foreach (InputDevice inputDevice in this.m_LocalManager.devices)
			{
				this.SendDevice(inputDevice);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002BDB0 File Offset: 0x00029FB0
		private void SendDevice(InputDevice device)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device.remote)
			{
				return;
			}
			InputRemoting.Message message = InputRemoting.NewDeviceMsg.Create(device);
			this.Send(message);
			InputRemoting.Message message2 = InputRemoting.NewEventsMsg.CreateStateEvent(device);
			this.Send(message2);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0002BDEC File Offset: 0x00029FEC
		private void SendEvent(InputEventPtr eventPtr, InputDevice device)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device != null && device.remote)
			{
				return;
			}
			InputRemoting.Message message = InputRemoting.NewEventsMsg.Create(eventPtr.data, 1);
			this.Send(message);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002BE24 File Offset: 0x0002A024
		private void SendDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device.remote)
			{
				return;
			}
			InputRemoting.Message message;
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					switch (change)
					{
					case InputDeviceChange.UsageChanged:
						message = InputRemoting.ChangeUsageMsg.Create(device);
						break;
					case InputDeviceChange.ConfigurationChanged:
						return;
					case InputDeviceChange.SoftReset:
						message = InputRemoting.NewEventsMsg.CreateResetEvent(device, false);
						break;
					case InputDeviceChange.HardReset:
						message = InputRemoting.NewEventsMsg.CreateResetEvent(device, true);
						break;
					default:
						return;
					}
				}
				else
				{
					message = InputRemoting.RemoveDeviceMsg.Create(device);
				}
			}
			else
			{
				message = InputRemoting.NewDeviceMsg.Create(device);
			}
			this.Send(message);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0002BE9C File Offset: 0x0002A09C
		private void SendLayoutChange(string layout, InputControlLayoutChange change)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (!this.m_LocalManager.m_Layouts.IsGeneratedLayout(new InternedString(layout)))
			{
				return;
			}
			if (change != InputControlLayoutChange.Added && change != InputControlLayoutChange.Replaced)
			{
				return;
			}
			InputRemoting.Message? message = InputRemoting.NewLayoutMsg.Create(this, layout);
			if (message != null)
			{
				this.Send(message.Value);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		private void Send(InputRemoting.Message msg)
		{
			InputRemoting.Subscriber[] subscribers = this.m_Subscribers;
			for (int i = 0; i < subscribers.Length; i++)
			{
				subscribers[i].observer.OnNext(msg);
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0002BF24 File Offset: 0x0002A124
		private int FindOrCreateSenderRecord(int senderId)
		{
			if (this.m_Senders != null)
			{
				int num = this.m_Senders.Length;
				for (int i = 0; i < num; i++)
				{
					if (this.m_Senders[i].senderId == senderId)
					{
						return i;
					}
				}
			}
			InputRemoting.RemoteSender remoteSender = new InputRemoting.RemoteSender
			{
				senderId = senderId
			};
			return ArrayHelpers.Append<InputRemoting.RemoteSender>(ref this.m_Senders, remoteSender);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002BF82 File Offset: 0x0002A182
		private static InternedString BuildLayoutNamespace(int senderId)
		{
			return new InternedString(string.Format("Remote::{0}", senderId));
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0002BF9C File Offset: 0x0002A19C
		private int FindLocalDeviceId(int remoteDeviceId, int senderIndex)
		{
			InputRemoting.RemoteInputDevice[] devices = this.m_Senders[senderIndex].devices;
			if (devices != null)
			{
				int num = devices.Length;
				for (int i = 0; i < num; i++)
				{
					if (devices[i].remoteId == remoteDeviceId)
					{
						return devices[i].localId;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0002BFEC File Offset: 0x0002A1EC
		private InputDevice TryGetDeviceByRemoteId(int remoteDeviceId, int senderIndex)
		{
			int num = this.FindLocalDeviceId(remoteDeviceId, senderIndex);
			return this.m_LocalManager.TryGetDeviceById(num);
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0002C00E File Offset: 0x0002A20E
		internal InputManager manager
		{
			get
			{
				return this.m_LocalManager;
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002C018 File Offset: 0x0002A218
		public void RemoveRemoteDevices(int participantId)
		{
			int num = this.FindOrCreateSenderRecord(participantId);
			InputRemoting.RemoteInputDevice[] devices = this.m_Senders[num].devices;
			if (devices != null)
			{
				foreach (InputRemoting.RemoteInputDevice remoteInputDevice in devices)
				{
					InputDevice inputDevice = this.m_LocalManager.TryGetDeviceById(remoteInputDevice.localId);
					if (inputDevice != null)
					{
						this.m_LocalManager.RemoveDevice(inputDevice, false);
					}
				}
			}
			ArrayHelpers.EraseAt<InputRemoting.RemoteSender>(ref this.m_Senders, num);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002C090 File Offset: 0x0002A290
		private static byte[] SerializeData<TData>(TData data)
		{
			string text = JsonUtility.ToJson(data);
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0002C0B4 File Offset: 0x0002A2B4
		private static TData DeserializeData<TData>(byte[] data)
		{
			return JsonUtility.FromJson<TData>(Encoding.UTF8.GetString(data));
		}

		// Token: 0x04000227 RID: 551
		private InputRemoting.Flags m_Flags;

		// Token: 0x04000228 RID: 552
		private InputManager m_LocalManager;

		// Token: 0x04000229 RID: 553
		private InputRemoting.Subscriber[] m_Subscribers;

		// Token: 0x0400022A RID: 554
		private InputRemoting.RemoteSender[] m_Senders;

		// Token: 0x02000193 RID: 403
		public enum MessageType
		{
			// Token: 0x04000897 RID: 2199
			Connect,
			// Token: 0x04000898 RID: 2200
			Disconnect,
			// Token: 0x04000899 RID: 2201
			NewLayout,
			// Token: 0x0400089A RID: 2202
			NewDevice,
			// Token: 0x0400089B RID: 2203
			NewEvents,
			// Token: 0x0400089C RID: 2204
			RemoveDevice,
			// Token: 0x0400089D RID: 2205
			RemoveLayout,
			// Token: 0x0400089E RID: 2206
			ChangeUsages,
			// Token: 0x0400089F RID: 2207
			StartSending,
			// Token: 0x040008A0 RID: 2208
			StopSending
		}

		// Token: 0x02000194 RID: 404
		public struct Message
		{
			// Token: 0x040008A1 RID: 2209
			public int participantId;

			// Token: 0x040008A2 RID: 2210
			public InputRemoting.MessageType type;

			// Token: 0x040008A3 RID: 2211
			public byte[] data;
		}

		// Token: 0x02000195 RID: 405
		[Flags]
		private enum Flags
		{
			// Token: 0x040008A5 RID: 2213
			Sending = 1,
			// Token: 0x040008A6 RID: 2214
			StartSendingOnConnect = 2
		}

		// Token: 0x02000196 RID: 406
		[Serializable]
		internal struct RemoteSender
		{
			// Token: 0x040008A7 RID: 2215
			public int senderId;

			// Token: 0x040008A8 RID: 2216
			public InternedString[] layouts;

			// Token: 0x040008A9 RID: 2217
			public InputRemoting.RemoteInputDevice[] devices;
		}

		// Token: 0x02000197 RID: 407
		[Serializable]
		internal struct RemoteInputDevice
		{
			// Token: 0x040008AA RID: 2218
			public int remoteId;

			// Token: 0x040008AB RID: 2219
			public int localId;

			// Token: 0x040008AC RID: 2220
			public InputDeviceDescription description;
		}

		// Token: 0x02000198 RID: 408
		internal class Subscriber : IDisposable
		{
			// Token: 0x0600139A RID: 5018 RVA: 0x0005A961 File Offset: 0x00058B61
			public void Dispose()
			{
				ArrayHelpers.Erase<InputRemoting.Subscriber>(ref this.owner.m_Subscribers, this);
			}

			// Token: 0x040008AD RID: 2221
			public InputRemoting owner;

			// Token: 0x040008AE RID: 2222
			public IObserver<InputRemoting.Message> observer;
		}

		// Token: 0x02000199 RID: 409
		private static class ConnectMsg
		{
			// Token: 0x0600139C RID: 5020 RVA: 0x0005A97D File Offset: 0x00058B7D
			public static void Process(InputRemoting receiver)
			{
				if (receiver.sending)
				{
					receiver.SendInitialMessages();
					return;
				}
				if ((receiver.m_Flags & InputRemoting.Flags.StartSendingOnConnect) == InputRemoting.Flags.StartSendingOnConnect)
				{
					receiver.StartSending();
				}
			}
		}

		// Token: 0x0200019A RID: 410
		private static class StartSendingMsg
		{
			// Token: 0x0600139D RID: 5021 RVA: 0x0005A99F File Offset: 0x00058B9F
			public static void Process(InputRemoting receiver)
			{
				receiver.StartSending();
			}
		}

		// Token: 0x0200019B RID: 411
		private static class StopSendingMsg
		{
			// Token: 0x0600139E RID: 5022 RVA: 0x0005A9A7 File Offset: 0x00058BA7
			public static void Process(InputRemoting receiver)
			{
				receiver.StopSending();
			}
		}

		// Token: 0x0200019C RID: 412
		private static class DisconnectMsg
		{
			// Token: 0x0600139F RID: 5023 RVA: 0x0005A9AF File Offset: 0x00058BAF
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				Debug.Log("DisconnectMsg.Process");
				receiver.RemoveRemoteDevices(msg.participantId);
				receiver.StopSending();
			}
		}

		// Token: 0x0200019D RID: 413
		private static class NewLayoutMsg
		{
			// Token: 0x060013A0 RID: 5024 RVA: 0x0005A9D0 File Offset: 0x00058BD0
			public static InputRemoting.Message? Create(InputRemoting sender, string layoutName)
			{
				InputControlLayout inputControlLayout;
				try
				{
					inputControlLayout = sender.m_LocalManager.TryLoadControlLayout(new InternedString(layoutName));
					if (inputControlLayout == null)
					{
						Debug.Log(string.Format("Could not find layout '{0}' meant to be sent through remote connection; this should not happen", layoutName));
						return null;
					}
				}
				catch (Exception ex)
				{
					Debug.Log(string.Format("Could not load layout '{0}'; not sending to remote listeners (exception: {1})", layoutName, ex));
					return null;
				}
				InputRemoting.NewLayoutMsg.Data data = new InputRemoting.NewLayoutMsg.Data
				{
					name = layoutName,
					layoutJson = inputControlLayout.ToJson(),
					isOverride = inputControlLayout.isOverride
				};
				return new InputRemoting.Message?(new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewLayout,
					data = InputRemoting.SerializeData<InputRemoting.NewLayoutMsg.Data>(data)
				});
			}

			// Token: 0x060013A1 RID: 5025 RVA: 0x0005AA94 File Offset: 0x00058C94
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				InputRemoting.NewLayoutMsg.Data data = InputRemoting.DeserializeData<InputRemoting.NewLayoutMsg.Data>(msg.data);
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				InternedString internedString = new InternedString(data.name);
				receiver.m_LocalManager.RegisterControlLayout(data.layoutJson, data.name, data.isOverride);
				ArrayHelpers.Append<InternedString>(ref receiver.m_Senders[num].layouts, internedString);
			}

			// Token: 0x02000265 RID: 613
			[Serializable]
			public struct Data
			{
				// Token: 0x04000C78 RID: 3192
				public string name;

				// Token: 0x04000C79 RID: 3193
				public string layoutJson;

				// Token: 0x04000C7A RID: 3194
				public bool isOverride;
			}
		}

		// Token: 0x0200019E RID: 414
		private static class NewDeviceMsg
		{
			// Token: 0x060013A2 RID: 5026 RVA: 0x0005AAFC File Offset: 0x00058CFC
			public static InputRemoting.Message Create(InputDevice device)
			{
				InputRemoting.NewDeviceMsg.Data data = default(InputRemoting.NewDeviceMsg.Data);
				data.name = device.name;
				data.layout = device.layout;
				data.deviceId = device.deviceId;
				data.description = device.description;
				data.usages = device.usages.Select((InternedString x) => x.ToString()).ToArray<string>();
				InputRemoting.NewDeviceMsg.Data data2 = data;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewDevice,
					data = InputRemoting.SerializeData<InputRemoting.NewDeviceMsg.Data>(data2)
				};
			}

			// Token: 0x060013A3 RID: 5027 RVA: 0x0005ABA0 File Offset: 0x00058DA0
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				InputRemoting.NewDeviceMsg.Data data = InputRemoting.DeserializeData<InputRemoting.NewDeviceMsg.Data>(msg.data);
				InputRemoting.RemoteInputDevice[] devices = receiver.m_Senders[num].devices;
				if (devices != null)
				{
					InputRemoting.RemoteInputDevice[] array = devices;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].remoteId == data.deviceId)
						{
							Debug.LogError(string.Format("Already received device with id {0} (layout '{1}', description '{3}) from remote {2}", new object[] { data.deviceId, data.layout, msg.participantId, data.description }));
							return;
						}
					}
				}
				InputDevice inputDevice;
				try
				{
					InternedString internedString = new InternedString(data.layout);
					inputDevice = receiver.m_LocalManager.AddDevice(internedString, data.name, default(InternedString));
					inputDevice.m_ParticipantId = msg.participantId;
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("Could not create remote device '{0}' with layout '{1}' locally (exception: {2})", data.description, data.layout, ex));
					return;
				}
				inputDevice.m_Description = data.description;
				inputDevice.m_DeviceFlags |= InputDevice.DeviceFlags.Remote;
				foreach (string text in data.usages)
				{
					receiver.m_LocalManager.AddDeviceUsage(inputDevice, new InternedString(text));
				}
				InputRemoting.RemoteInputDevice remoteInputDevice = new InputRemoting.RemoteInputDevice
				{
					remoteId = data.deviceId,
					localId = inputDevice.deviceId,
					description = data.description
				};
				ArrayHelpers.Append<InputRemoting.RemoteInputDevice>(ref receiver.m_Senders[num].devices, remoteInputDevice);
			}

			// Token: 0x02000266 RID: 614
			[Serializable]
			public struct Data
			{
				// Token: 0x04000C7B RID: 3195
				public string name;

				// Token: 0x04000C7C RID: 3196
				public string layout;

				// Token: 0x04000C7D RID: 3197
				public int deviceId;

				// Token: 0x04000C7E RID: 3198
				public string[] usages;

				// Token: 0x04000C7F RID: 3199
				public InputDeviceDescription description;
			}
		}

		// Token: 0x0200019F RID: 415
		private static class NewEventsMsg
		{
			// Token: 0x060013A4 RID: 5028 RVA: 0x0005AD64 File Offset: 0x00058F64
			public unsafe static InputRemoting.Message CreateResetEvent(InputDevice device, bool isHardReset)
			{
				DeviceResetEvent deviceResetEvent = DeviceResetEvent.Create(device.deviceId, isHardReset, -1.0);
				return InputRemoting.NewEventsMsg.Create((InputEvent*)UnsafeUtility.AddressOf<DeviceResetEvent>(ref deviceResetEvent), 1);
			}

			// Token: 0x060013A5 RID: 5029 RVA: 0x0005AD94 File Offset: 0x00058F94
			public static InputRemoting.Message CreateStateEvent(InputDevice device)
			{
				InputEventPtr inputEventPtr;
				InputRemoting.Message message;
				using (StateEvent.From(device, out inputEventPtr, Allocator.Temp))
				{
					message = InputRemoting.NewEventsMsg.Create(inputEventPtr.data, 1);
				}
				return message;
			}

			// Token: 0x060013A6 RID: 5030 RVA: 0x0005ADDC File Offset: 0x00058FDC
			public unsafe static InputRemoting.Message Create(InputEvent* events, int eventCount)
			{
				uint num = 0U;
				InputEventPtr inputEventPtr = new InputEventPtr(events);
				int i = 0;
				while (i < eventCount)
				{
					num = num.AlignToMultipleOf(4U) + inputEventPtr.sizeInBytes;
					i++;
					inputEventPtr = inputEventPtr.Next();
				}
				byte[] array = new byte[num];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				UnsafeUtility.MemCpy((void*)ptr, (void*)events, (long)((ulong)num));
				array2 = null;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewEvents,
					data = array
				};
			}

			// Token: 0x060013A7 RID: 5031 RVA: 0x0005AE64 File Offset: 0x00059064
			public unsafe static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				InputManager localManager = receiver.m_LocalManager;
				byte[] array;
				byte* ptr;
				if ((array = msg.data) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				IntPtr intPtr = new IntPtr((void*)(ptr + msg.data.Length));
				int num = 0;
				InputEventPtr inputEventPtr = new InputEventPtr((InputEvent*)ptr);
				int num2 = receiver.FindOrCreateSenderRecord(msg.participantId);
				while (inputEventPtr.data < (InputEvent*)intPtr.ToPointer())
				{
					int deviceId = inputEventPtr.deviceId;
					int num3 = receiver.FindLocalDeviceId(deviceId, num2);
					inputEventPtr.deviceId = num3;
					if (num3 != 0)
					{
						localManager.QueueEvent(inputEventPtr);
					}
					num++;
					inputEventPtr = inputEventPtr.Next();
				}
				array = null;
			}
		}

		// Token: 0x020001A0 RID: 416
		private static class ChangeUsageMsg
		{
			// Token: 0x060013A8 RID: 5032 RVA: 0x0005AF0C File Offset: 0x0005910C
			public static InputRemoting.Message Create(InputDevice device)
			{
				InputRemoting.ChangeUsageMsg.Data data = default(InputRemoting.ChangeUsageMsg.Data);
				data.deviceId = device.deviceId;
				data.usages = device.usages.Select((InternedString x) => x.ToString()).ToArray<string>();
				InputRemoting.ChangeUsageMsg.Data data2 = data;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.ChangeUsages,
					data = InputRemoting.SerializeData<InputRemoting.ChangeUsageMsg.Data>(data2)
				};
			}

			// Token: 0x060013A9 RID: 5033 RVA: 0x0005AF8C File Offset: 0x0005918C
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				InputRemoting.ChangeUsageMsg.Data data = InputRemoting.DeserializeData<InputRemoting.ChangeUsageMsg.Data>(msg.data);
				InputDevice inputDevice = receiver.TryGetDeviceByRemoteId(data.deviceId, num);
				if (inputDevice != null)
				{
					foreach (InternedString internedString in inputDevice.usages)
					{
						if (!data.usages.Contains(internedString))
						{
							receiver.m_LocalManager.RemoveDeviceUsage(inputDevice, new InternedString(internedString));
						}
					}
					foreach (string text in data.usages)
					{
						InternedString internedString2 = new InternedString(text);
						if (!inputDevice.usages.Contains(internedString2))
						{
							receiver.m_LocalManager.AddDeviceUsage(inputDevice, new InternedString(text));
						}
					}
				}
			}

			// Token: 0x02000268 RID: 616
			[Serializable]
			public struct Data
			{
				// Token: 0x04000C82 RID: 3202
				public int deviceId;

				// Token: 0x04000C83 RID: 3203
				public string[] usages;
			}
		}

		// Token: 0x020001A1 RID: 417
		private static class RemoveDeviceMsg
		{
			// Token: 0x060013AA RID: 5034 RVA: 0x0005B084 File Offset: 0x00059284
			public static InputRemoting.Message Create(InputDevice device)
			{
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.RemoveDevice,
					data = BitConverter.GetBytes(device.deviceId)
				};
			}

			// Token: 0x060013AB RID: 5035 RVA: 0x0005B0B4 File Offset: 0x000592B4
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				int num2 = BitConverter.ToInt32(msg.data, 0);
				InputDevice inputDevice = receiver.TryGetDeviceByRemoteId(num2, num);
				if (inputDevice != null)
				{
					receiver.m_LocalManager.RemoveDevice(inputDevice, false);
				}
			}
		}
	}
}
