using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000388 RID: 904
	[Serializable]
	public class PlayerConnection : ScriptableObject, IEditorPlayerConnection
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x00031E08 File Offset: 0x00030008
		public static PlayerConnection instance
		{
			get
			{
				bool flag = PlayerConnection.s_Instance == null;
				PlayerConnection playerConnection;
				if (flag)
				{
					playerConnection = PlayerConnection.CreateInstance();
				}
				else
				{
					playerConnection = PlayerConnection.s_Instance;
				}
				return playerConnection;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00031E38 File Offset: 0x00030038
		public bool isConnected
		{
			get
			{
				return this.GetConnectionNativeApi().IsConnected();
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00031E58 File Offset: 0x00030058
		private static PlayerConnection CreateInstance()
		{
			PlayerConnection.s_Instance = ScriptableObject.CreateInstance<PlayerConnection>();
			PlayerConnection.s_Instance.hideFlags = HideFlags.HideAndDontSave;
			return PlayerConnection.s_Instance;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00031E88 File Offset: 0x00030088
		public void OnEnable()
		{
			bool isInitilized = this.m_IsInitilized;
			if (!isInitilized)
			{
				this.m_IsInitilized = true;
				this.GetConnectionNativeApi().Initialize();
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00031EB8 File Offset: 0x000300B8
		private IPlayerEditorConnectionNative GetConnectionNativeApi()
		{
			return PlayerConnection.connectionNative ?? new PlayerConnectionInternal();
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00031ED8 File Offset: 0x000300D8
		public void Register(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			bool flag2 = !Enumerable.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.m_PlayerEditorConnectionEvents.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag2)
			{
				this.GetConnectionNativeApi().RegisterInternal(messageId);
			}
			this.m_PlayerEditorConnectionEvents.AddAndCreate(messageId).AddListener(callback);
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00031F68 File Offset: 0x00030168
		public void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.m_PlayerEditorConnectionEvents.UnregisterManagedCallback(messageId, callback);
			bool flag = !Enumerable.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>(this.m_PlayerEditorConnectionEvents.messageTypeSubscribers, (PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag)
			{
				this.GetConnectionNativeApi().UnregisterInternal(messageId);
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00031FD0 File Offset: 0x000301D0
		public void RegisterConnection(UnityAction<int> callback)
		{
			foreach (int num in this.m_connectedPlayers)
			{
				callback(num);
			}
			this.m_PlayerEditorConnectionEvents.connectionEvent.AddListener(callback);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0003203C File Offset: 0x0003023C
		public void RegisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.AddListener(callback);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00032051 File Offset: 0x00030251
		public void UnregisterConnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.connectionEvent.RemoveListener(callback);
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00032066 File Offset: 0x00030266
		public void UnregisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.RemoveListener(callback);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0003207C File Offset: 0x0003027C
		public void Send(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			this.GetConnectionNativeApi().SendMessage(messageId, data, 0);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x000320BC File Offset: 0x000302BC
		public bool TrySend(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			return this.GetConnectionNativeApi().TrySendMessage(messageId, data, 0);
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x000320FC File Offset: 0x000302FC
		public bool BlockUntilRecvMsg(Guid messageId, int timeout)
		{
			bool msgReceived = false;
			UnityAction<MessageEventArgs> unityAction = delegate(MessageEventArgs args)
			{
				msgReceived = true;
			};
			DateTime now = DateTime.Now;
			this.Register(messageId, unityAction);
			while ((DateTime.Now - now).TotalMilliseconds < (double)timeout && !msgReceived)
			{
				this.GetConnectionNativeApi().Poll();
			}
			this.Unregister(messageId, unityAction);
			return msgReceived;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0003217E File Offset: 0x0003037E
		public void DisconnectAll()
		{
			this.GetConnectionNativeApi().DisconnectAll();
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00032190 File Offset: 0x00030390
		[RequiredByNativeCode]
		private static void MessageCallbackInternal(IntPtr data, ulong size, ulong guid, string messageId)
		{
			byte[] array = null;
			bool flag = size > 0UL;
			if (flag)
			{
				array = new byte[size];
				Marshal.Copy(data, array, 0, (int)size);
			}
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.InvokeMessageIdSubscribers(new Guid(messageId), array, (int)guid);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000321D7 File Offset: 0x000303D7
		[RequiredByNativeCode]
		private static void ConnectedCallbackInternal(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Add(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.connectionEvent.Invoke(playerId);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00032201 File Offset: 0x00030401
		[RequiredByNativeCode]
		private static void DisconnectedCallback(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Remove(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.disconnectionEvent.Invoke(playerId);
		}

		// Token: 0x04000A1C RID: 2588
		internal static IPlayerEditorConnectionNative connectionNative;

		// Token: 0x04000A1D RID: 2589
		[SerializeField]
		private PlayerEditorConnectionEvents m_PlayerEditorConnectionEvents = new PlayerEditorConnectionEvents();

		// Token: 0x04000A1E RID: 2590
		[SerializeField]
		private List<int> m_connectedPlayers = new List<int>();

		// Token: 0x04000A1F RID: 2591
		private bool m_IsInitilized;

		// Token: 0x04000A20 RID: 2592
		private static PlayerConnection s_Instance;
	}
}
