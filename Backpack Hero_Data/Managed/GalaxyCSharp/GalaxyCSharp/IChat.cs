using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x020000B0 RID: 176
	public class IChat : IDisposable
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x00016DA0 File Offset: 0x00014FA0
		internal IChat(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00016DBC File Offset: 0x00014FBC
		internal static HandleRef getCPtr(IChat obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00016DDC File Offset: 0x00014FDC
		~IChat()
		{
			this.Dispose();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00016E0C File Offset: 0x0001500C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChat(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00016E8C File Offset: 0x0001508C
		public virtual void RequestChatRoomWithUser(GalaxyID userID, IChatRoomWithUserRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomWithUser__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IChatRoomWithUserRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00016EB5 File Offset: 0x000150B5
		public virtual void RequestChatRoomWithUser(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomWithUser__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00016ED8 File Offset: 0x000150D8
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit, ulong referenceMessageID, IChatRoomMessagesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_0(this.swigCPtr, chatRoomID, limit, referenceMessageID, IChatRoomMessagesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00016EFF File Offset: 0x000150FF
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit, ulong referenceMessageID)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_1(this.swigCPtr, chatRoomID, limit, referenceMessageID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00016F1F File Offset: 0x0001511F
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_2(this.swigCPtr, chatRoomID, limit);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00016F40 File Offset: 0x00015140
		public virtual uint SendChatRoomMessage(ulong chatRoomID, string msg, IChatRoomMessageSendListener listener)
		{
			uint num = GalaxyInstancePINVOKE.IChat_SendChatRoomMessage__SWIG_0(this.swigCPtr, chatRoomID, msg, IChatRoomMessageSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00016F74 File Offset: 0x00015174
		public virtual uint SendChatRoomMessage(ulong chatRoomID, string msg)
		{
			uint num = GalaxyInstancePINVOKE.IChat_SendChatRoomMessage__SWIG_1(this.swigCPtr, chatRoomID, msg);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00016FA0 File Offset: 0x000151A0
		public virtual uint GetChatRoomMessageByIndex(uint index, ref ulong messageID, ref ChatMessageType messageType, ref GalaxyID senderID, ref uint sendTime, out string buffer, uint bufferLength)
		{
			int num = 0;
			byte[] array = new byte[bufferLength];
			uint num3;
			try
			{
				uint num2 = GalaxyInstancePINVOKE.IChat_GetChatRoomMessageByIndex(this.swigCPtr, index, ref messageID, ref num, GalaxyID.getCPtr(senderID), ref sendTime, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				num3 = num2;
			}
			finally
			{
				messageType = (ChatMessageType)num;
				buffer = Encoding.UTF8.GetString(array);
			}
			return num3;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00017010 File Offset: 0x00015210
		public virtual uint GetChatRoomMemberCount(ulong chatRoomID)
		{
			uint num = GalaxyInstancePINVOKE.IChat_GetChatRoomMemberCount(this.swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001703C File Offset: 0x0001523C
		public virtual GalaxyID GetChatRoomMemberUserIDByIndex(ulong chatRoomID, uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IChat_GetChatRoomMemberUserIDByIndex(this.swigCPtr, chatRoomID, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID galaxyID = null;
			if (intPtr != IntPtr.Zero)
			{
				galaxyID = new GalaxyID(intPtr, true);
			}
			return galaxyID;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00017084 File Offset: 0x00015284
		public virtual uint GetChatRoomUnreadMessageCount(ulong chatRoomID)
		{
			uint num = GalaxyInstancePINVOKE.IChat_GetChatRoomUnreadMessageCount(this.swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000170AF File Offset: 0x000152AF
		public virtual void MarkChatRoomAsRead(ulong chatRoomID)
		{
			GalaxyInstancePINVOKE.IChat_MarkChatRoomAsRead(this.swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x040000E3 RID: 227
		private HandleRef swigCPtr;

		// Token: 0x040000E4 RID: 228
		protected bool swigCMemOwn;
	}
}
