using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x0200038C RID: 908
	[Serializable]
	internal class PlayerEditorConnectionEvents
	{
		// Token: 0x06001EDD RID: 7901 RVA: 0x0003227C File Offset: 0x0003047C
		public void InvokeMessageIdSubscribers(Guid messageId, byte[] data, int playerId)
		{
			IEnumerable<PlayerEditorConnectionEvents.MessageTypeSubscribers> enumerable = Enumerable.Where<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = !Enumerable.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>(enumerable);
			if (flag)
			{
				string text = "No actions found for messageId: ";
				Guid messageId2 = messageId;
				Debug.LogError(text + messageId2.ToString());
			}
			else
			{
				MessageEventArgs messageEventArgs = new MessageEventArgs
				{
					playerId = playerId,
					data = data
				};
				foreach (PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers in enumerable)
				{
					messageTypeSubscribers.messageCallback.Invoke(messageEventArgs);
				}
			}
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00032344 File Offset: 0x00030544
		public UnityEvent<MessageEventArgs> AddAndCreate(Guid messageId)
		{
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers = Enumerable.SingleOrDefault<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = messageTypeSubscribers == null;
			if (flag)
			{
				messageTypeSubscribers = new PlayerEditorConnectionEvents.MessageTypeSubscribers
				{
					MessageTypeId = messageId,
					messageCallback = new PlayerEditorConnectionEvents.MessageEvent()
				};
				this.messageTypeSubscribers.Add(messageTypeSubscribers);
			}
			messageTypeSubscribers.subscriberCount++;
			return messageTypeSubscribers.messageCallback;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000323C4 File Offset: 0x000305C4
		public void UnregisterManagedCallback(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers = Enumerable.SingleOrDefault<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = messageTypeSubscribers == null;
			if (!flag)
			{
				messageTypeSubscribers.subscriberCount--;
				messageTypeSubscribers.messageCallback.RemoveListener(callback);
				bool flag2 = messageTypeSubscribers.subscriberCount <= 0;
				if (flag2)
				{
					this.messageTypeSubscribers.Remove(messageTypeSubscribers);
				}
			}
		}

		// Token: 0x04000A24 RID: 2596
		[SerializeField]
		public List<PlayerEditorConnectionEvents.MessageTypeSubscribers> messageTypeSubscribers = new List<PlayerEditorConnectionEvents.MessageTypeSubscribers>();

		// Token: 0x04000A25 RID: 2597
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent connectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x04000A26 RID: 2598
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent disconnectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x0200038D RID: 909
		[Serializable]
		public class MessageEvent : UnityEvent<MessageEventArgs>
		{
		}

		// Token: 0x0200038E RID: 910
		[Serializable]
		public class ConnectionChangeEvent : UnityEvent<int>
		{
		}

		// Token: 0x0200038F RID: 911
		[Serializable]
		public class MessageTypeSubscribers
		{
			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x00032478 File Offset: 0x00030678
			// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x00032495 File Offset: 0x00030695
			public Guid MessageTypeId
			{
				get
				{
					return new Guid(this.m_messageTypeId);
				}
				set
				{
					this.m_messageTypeId = value.ToString();
				}
			}

			// Token: 0x04000A27 RID: 2599
			[SerializeField]
			private string m_messageTypeId;

			// Token: 0x04000A28 RID: 2600
			public int subscriberCount = 0;

			// Token: 0x04000A29 RID: 2601
			public PlayerEditorConnectionEvents.MessageEvent messageCallback = new PlayerEditorConnectionEvents.MessageEvent();
		}
	}
}
