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
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0002BAF2 File Offset: 0x00029CF2
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0002BAFF File Offset: 0x00029CFF
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

		// Token: 0x0600077E RID: 1918 RVA: 0x0002BB22 File Offset: 0x00029D22
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

		// Token: 0x0600077F RID: 1919 RVA: 0x0002BB50 File Offset: 0x00029D50
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

		// Token: 0x06000780 RID: 1920 RVA: 0x0002BBB8 File Offset: 0x00029DB8
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

		// Token: 0x06000781 RID: 1921 RVA: 0x0002BC1C File Offset: 0x00029E1C
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

		// Token: 0x06000782 RID: 1922 RVA: 0x0002BCA3 File Offset: 0x00029EA3
		void IObserver<InputRemoting.Message>.OnError(Exception error)
		{
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002BCA5 File Offset: 0x00029EA5
		void IObserver<InputRemoting.Message>.OnCompleted()
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002BCA8 File Offset: 0x00029EA8
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

		// Token: 0x06000785 RID: 1925 RVA: 0x0002BCE5 File Offset: 0x00029EE5
		private void SendInitialMessages()
		{
			this.SendAllGeneratedLayouts();
			this.SendAllDevices();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002BCF4 File Offset: 0x00029EF4
		private void SendAllGeneratedLayouts()
		{
			foreach (KeyValuePair<InternedString, Func<InputControlLayout>> keyValuePair in this.m_LocalManager.m_Layouts.layoutBuilders)
			{
				this.SendLayout(keyValuePair.Key);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002BD5C File Offset: 0x00029F5C
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

		// Token: 0x06000788 RID: 1928 RVA: 0x0002BD90 File Offset: 0x00029F90
		private void SendAllDevices()
		{
			foreach (InputDevice inputDevice in this.m_LocalManager.devices)
			{
				this.SendDevice(inputDevice);
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002BDEC File Offset: 0x00029FEC
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

		// Token: 0x0600078A RID: 1930 RVA: 0x0002BE28 File Offset: 0x0002A028
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

		// Token: 0x0600078B RID: 1931 RVA: 0x0002BE60 File Offset: 0x0002A060
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

		// Token: 0x0600078C RID: 1932 RVA: 0x0002BED8 File Offset: 0x0002A0D8
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

		// Token: 0x0600078D RID: 1933 RVA: 0x0002BF30 File Offset: 0x0002A130
		private void Send(InputRemoting.Message msg)
		{
			InputRemoting.Subscriber[] subscribers = this.m_Subscribers;
			for (int i = 0; i < subscribers.Length; i++)
			{
				subscribers[i].observer.OnNext(msg);
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0002BF60 File Offset: 0x0002A160
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

		// Token: 0x0600078F RID: 1935 RVA: 0x0002BFBE File Offset: 0x0002A1BE
		private static InternedString BuildLayoutNamespace(int senderId)
		{
			return new InternedString(string.Format("Remote::{0}", senderId));
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
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

		// Token: 0x06000791 RID: 1937 RVA: 0x0002C028 File Offset: 0x0002A228
		private InputDevice TryGetDeviceByRemoteId(int remoteDeviceId, int senderIndex)
		{
			int num = this.FindLocalDeviceId(remoteDeviceId, senderIndex);
			return this.m_LocalManager.TryGetDeviceById(num);
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0002C04A File Offset: 0x0002A24A
		internal InputManager manager
		{
			get
			{
				return this.m_LocalManager;
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0002C054 File Offset: 0x0002A254
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

		// Token: 0x06000794 RID: 1940 RVA: 0x0002C0CC File Offset: 0x0002A2CC
		private static byte[] SerializeData<TData>(TData data)
		{
			string text = JsonUtility.ToJson(data);
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0002C0F0 File Offset: 0x0002A2F0
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
			// Token: 0x04000898 RID: 2200
			Connect,
			// Token: 0x04000899 RID: 2201
			Disconnect,
			// Token: 0x0400089A RID: 2202
			NewLayout,
			// Token: 0x0400089B RID: 2203
			NewDevice,
			// Token: 0x0400089C RID: 2204
			NewEvents,
			// Token: 0x0400089D RID: 2205
			RemoveDevice,
			// Token: 0x0400089E RID: 2206
			RemoveLayout,
			// Token: 0x0400089F RID: 2207
			ChangeUsages,
			// Token: 0x040008A0 RID: 2208
			StartSending,
			// Token: 0x040008A1 RID: 2209
			StopSending
		}

		// Token: 0x02000194 RID: 404
		public struct Message
		{
			// Token: 0x040008A2 RID: 2210
			public int participantId;

			// Token: 0x040008A3 RID: 2211
			public InputRemoting.MessageType type;

			// Token: 0x040008A4 RID: 2212
			public byte[] data;
		}

		// Token: 0x02000195 RID: 405
		[Flags]
		private enum Flags
		{
			// Token: 0x040008A6 RID: 2214
			Sending = 1,
			// Token: 0x040008A7 RID: 2215
			StartSendingOnConnect = 2
		}

		// Token: 0x02000196 RID: 406
		[Serializable]
		internal struct RemoteSender
		{
			// Token: 0x040008A8 RID: 2216
			public int senderId;

			// Token: 0x040008A9 RID: 2217
			public InternedString[] layouts;

			// Token: 0x040008AA RID: 2218
			public InputRemoting.RemoteInputDevice[] devices;
		}

		// Token: 0x02000197 RID: 407
		[Serializable]
		internal struct RemoteInputDevice
		{
			// Token: 0x040008AB RID: 2219
			public int remoteId;

			// Token: 0x040008AC RID: 2220
			public int localId;

			// Token: 0x040008AD RID: 2221
			public InputDeviceDescription description;
		}

		// Token: 0x02000198 RID: 408
		internal class Subscriber : IDisposable
		{
			// Token: 0x060013A1 RID: 5025 RVA: 0x0005AB75 File Offset: 0x00058D75
			public void Dispose()
			{
				ArrayHelpers.Erase<InputRemoting.Subscriber>(ref this.owner.m_Subscribers, this);
			}

			// Token: 0x040008AE RID: 2222
			public InputRemoting owner;

			// Token: 0x040008AF RID: 2223
			public IObserver<InputRemoting.Message> observer;
		}

		// Token: 0x02000199 RID: 409
		private static class ConnectMsg
		{
			// Token: 0x060013A3 RID: 5027 RVA: 0x0005AB91 File Offset: 0x00058D91
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
			// Token: 0x060013A4 RID: 5028 RVA: 0x0005ABB3 File Offset: 0x00058DB3
			public static void Process(InputRemoting receiver)
			{
				receiver.StartSending();
			}
		}

		// Token: 0x0200019B RID: 411
		private static class StopSendingMsg
		{
			// Token: 0x060013A5 RID: 5029 RVA: 0x0005ABBB File Offset: 0x00058DBB
			public static void Process(InputRemoting receiver)
			{
				receiver.StopSending();
			}
		}

		// Token: 0x0200019C RID: 412
		private static class DisconnectMsg
		{
			// Token: 0x060013A6 RID: 5030 RVA: 0x0005ABC3 File Offset: 0x00058DC3
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
			// Token: 0x060013A7 RID: 5031 RVA: 0x0005ABE4 File Offset: 0x00058DE4
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

			// Token: 0x060013A8 RID: 5032 RVA: 0x0005ACA8 File Offset: 0x00058EA8
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
				// Token: 0x04000C79 RID: 3193
				public string name;

				// Token: 0x04000C7A RID: 3194
				public string layoutJson;

				// Token: 0x04000C7B RID: 3195
				public bool isOverride;
			}
		}

		// Token: 0x0200019E RID: 414
		private static class NewDeviceMsg
		{
			// Token: 0x060013A9 RID: 5033 RVA: 0x0005AD10 File Offset: 0x00058F10
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

			// Token: 0x060013AA RID: 5034 RVA: 0x0005ADB4 File Offset: 0x00058FB4
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
				// Token: 0x04000C7C RID: 3196
				public string name;

				// Token: 0x04000C7D RID: 3197
				public string layout;

				// Token: 0x04000C7E RID: 3198
				public int deviceId;

				// Token: 0x04000C7F RID: 3199
				public string[] usages;

				// Token: 0x04000C80 RID: 3200
				public InputDeviceDescription description;
			}
		}

		// Token: 0x0200019F RID: 415
		private static class NewEventsMsg
		{
			// Token: 0x060013AB RID: 5035 RVA: 0x0005AF78 File Offset: 0x00059178
			public unsafe static InputRemoting.Message CreateResetEvent(InputDevice device, bool isHardReset)
			{
				DeviceResetEvent deviceResetEvent = DeviceResetEvent.Create(device.deviceId, isHardReset, -1.0);
				return InputRemoting.NewEventsMsg.Create((InputEvent*)UnsafeUtility.AddressOf<DeviceResetEvent>(ref deviceResetEvent), 1);
			}

			// Token: 0x060013AC RID: 5036 RVA: 0x0005AFA8 File Offset: 0x000591A8
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

			// Token: 0x060013AD RID: 5037 RVA: 0x0005AFF0 File Offset: 0x000591F0
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

			// Token: 0x060013AE RID: 5038 RVA: 0x0005B078 File Offset: 0x00059278
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
			// Token: 0x060013AF RID: 5039 RVA: 0x0005B120 File Offset: 0x00059320
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

			// Token: 0x060013B0 RID: 5040 RVA: 0x0005B1A0 File Offset: 0x000593A0
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
				// Token: 0x04000C83 RID: 3203
				public int deviceId;

				// Token: 0x04000C84 RID: 3204
				public string[] usages;
			}
		}

		// Token: 0x020001A1 RID: 417
		private static class RemoveDeviceMsg
		{
			// Token: 0x060013B1 RID: 5041 RVA: 0x0005B298 File Offset: 0x00059498
			public static InputRemoting.Message Create(InputDevice device)
			{
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.RemoveDevice,
					data = BitConverter.GetBytes(device.deviceId)
				};
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x0005B2C8 File Offset: 0x000594C8
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
