using System;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Networking.PlayerConnection;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal class RemoteInputPlayerConnection : ScriptableObject, IObserver<InputRemoting.Message>, IObservable<InputRemoting.Message>
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0002C0C8 File Offset: 0x0002A2C8
		public void Bind(IEditorPlayerConnection connection, bool isConnected)
		{
			if (this.m_Connection == null)
			{
				connection.RegisterConnection(new UnityAction<int>(this.OnConnected));
				connection.RegisterDisconnection(new UnityAction<int>(this.OnDisconnected));
				connection.Register(RemoteInputPlayerConnection.kNewDeviceMsg, new UnityAction<MessageEventArgs>(this.OnNewDevice));
				connection.Register(RemoteInputPlayerConnection.kNewLayoutMsg, new UnityAction<MessageEventArgs>(this.OnNewLayout));
				connection.Register(RemoteInputPlayerConnection.kNewEventsMsg, new UnityAction<MessageEventArgs>(this.OnNewEvents));
				connection.Register(RemoteInputPlayerConnection.kRemoveDeviceMsg, new UnityAction<MessageEventArgs>(this.OnRemoveDevice));
				connection.Register(RemoteInputPlayerConnection.kChangeUsagesMsg, new UnityAction<MessageEventArgs>(this.OnChangeUsages));
				connection.Register(RemoteInputPlayerConnection.kStartSendingMsg, new UnityAction<MessageEventArgs>(this.OnStartSending));
				connection.Register(RemoteInputPlayerConnection.kStopSendingMsg, new UnityAction<MessageEventArgs>(this.OnStopSending));
				this.m_Connection = connection;
				if (isConnected)
				{
					this.OnConnected(0);
				}
				return;
			}
			if (this.m_Connection == connection)
			{
				return;
			}
			throw new InvalidOperationException("Already bound to an IEditorPlayerConnection");
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0002C1C8 File Offset: 0x0002A3C8
		public IDisposable Subscribe(IObserver<InputRemoting.Message> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			RemoteInputPlayerConnection.Subscriber subscriber = new RemoteInputPlayerConnection.Subscriber
			{
				owner = this,
				observer = observer
			};
			ArrayHelpers.Append<RemoteInputPlayerConnection.Subscriber>(ref this.m_Subscribers, subscriber);
			if (this.m_ConnectedIds != null)
			{
				foreach (int num in this.m_ConnectedIds)
				{
					observer.OnNext(new InputRemoting.Message
					{
						type = InputRemoting.MessageType.Connect,
						participantId = num
					});
				}
			}
			return subscriber;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0002C246 File Offset: 0x0002A446
		private void OnConnected(int id)
		{
			if (this.m_ConnectedIds != null && ArrayHelpers.Contains<int>(this.m_ConnectedIds, id))
			{
				return;
			}
			ArrayHelpers.Append<int>(ref this.m_ConnectedIds, id);
			this.SendToSubscribers(InputRemoting.MessageType.Connect, new MessageEventArgs
			{
				playerId = id
			});
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0002C27F File Offset: 0x0002A47F
		private void OnDisconnected(int id)
		{
			if (this.m_ConnectedIds == null || !ArrayHelpers.Contains<int>(this.m_ConnectedIds, id))
			{
				return;
			}
			ArrayHelpers.Erase<int>(ref this.m_ConnectedIds, id);
			this.SendToSubscribers(InputRemoting.MessageType.Disconnect, new MessageEventArgs
			{
				playerId = id
			});
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
		private void OnNewDevice(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.NewDevice, args);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0002C2C2 File Offset: 0x0002A4C2
		private void OnNewLayout(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.NewLayout, args);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		private void OnNewEvents(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.NewEvents, args);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002C2D6 File Offset: 0x0002A4D6
		private void OnRemoveDevice(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.RemoveDevice, args);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		private void OnChangeUsages(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.ChangeUsages, args);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0002C2EA File Offset: 0x0002A4EA
		private void OnStartSending(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.StartSending, args);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
		private void OnStopSending(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.StopSending, args);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0002C300 File Offset: 0x0002A500
		private void SendToSubscribers(InputRemoting.MessageType type, MessageEventArgs args)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			InputRemoting.Message message = new InputRemoting.Message
			{
				participantId = args.playerId,
				type = type,
				data = args.data
			};
			for (int i = 0; i < this.m_Subscribers.Length; i++)
			{
				this.m_Subscribers[i].observer.OnNext(message);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0002C368 File Offset: 0x0002A568
		void IObserver<InputRemoting.Message>.OnNext(InputRemoting.Message msg)
		{
			if (this.m_Connection == null)
			{
				return;
			}
			switch (msg.type)
			{
			case InputRemoting.MessageType.NewLayout:
				this.m_Connection.Send(RemoteInputPlayerConnection.kNewLayoutMsg, msg.data);
				return;
			case InputRemoting.MessageType.NewDevice:
				this.m_Connection.Send(RemoteInputPlayerConnection.kNewDeviceMsg, msg.data);
				return;
			case InputRemoting.MessageType.NewEvents:
				this.m_Connection.Send(RemoteInputPlayerConnection.kNewEventsMsg, msg.data);
				return;
			case InputRemoting.MessageType.RemoveDevice:
				this.m_Connection.Send(RemoteInputPlayerConnection.kRemoveDeviceMsg, msg.data);
				break;
			case InputRemoting.MessageType.RemoveLayout:
				break;
			case InputRemoting.MessageType.ChangeUsages:
				this.m_Connection.Send(RemoteInputPlayerConnection.kChangeUsagesMsg, msg.data);
				return;
			default:
				return;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0002C418 File Offset: 0x0002A618
		void IObserver<InputRemoting.Message>.OnError(Exception error)
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002C41A File Offset: 0x0002A61A
		void IObserver<InputRemoting.Message>.OnCompleted()
		{
		}

		// Token: 0x0400022B RID: 555
		public static readonly Guid kNewDeviceMsg = new Guid("fcd9651ded40425995dfa6aeb78f1f1c");

		// Token: 0x0400022C RID: 556
		public static readonly Guid kNewLayoutMsg = new Guid("fccfec2b7369466d88502a9dd38505f4");

		// Token: 0x0400022D RID: 557
		public static readonly Guid kNewEventsMsg = new Guid("53546641df1347bc8aa315278a603586");

		// Token: 0x0400022E RID: 558
		public static readonly Guid kRemoveDeviceMsg = new Guid("e5e299b2d9e44255b8990bb71af8922d");

		// Token: 0x0400022F RID: 559
		public static readonly Guid kChangeUsagesMsg = new Guid("b9fe706dfc854d7ca109a5e38d7db730");

		// Token: 0x04000230 RID: 560
		public static readonly Guid kStartSendingMsg = new Guid("0d58e99045904672b3ef34b8797d23cb");

		// Token: 0x04000231 RID: 561
		public static readonly Guid kStopSendingMsg = new Guid("548716b2534a45369ab0c9323fc8b4a8");

		// Token: 0x04000232 RID: 562
		[SerializeField]
		private IEditorPlayerConnection m_Connection;

		// Token: 0x04000233 RID: 563
		[NonSerialized]
		private RemoteInputPlayerConnection.Subscriber[] m_Subscribers;

		// Token: 0x04000234 RID: 564
		[SerializeField]
		private int[] m_ConnectedIds;

		// Token: 0x020001A2 RID: 418
		private class Subscriber : IDisposable
		{
			// Token: 0x060013AC RID: 5036 RVA: 0x0005B0F4 File Offset: 0x000592F4
			public void Dispose()
			{
				ArrayHelpers.Erase<RemoteInputPlayerConnection.Subscriber>(ref this.owner.m_Subscribers, this);
			}

			// Token: 0x040008AF RID: 2223
			public RemoteInputPlayerConnection owner;

			// Token: 0x040008B0 RID: 2224
			public IObserver<InputRemoting.Message> observer;
		}
	}
}
