using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000B1 RID: 177
	public abstract class IChatRoomMessageSendListener : GalaxyTypeAwareListenerChatRoomMessageSend
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x0000C88C File Offset: 0x0000AA8C
		internal IChatRoomMessageSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessageSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomMessageSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public IChatRoomMessageSendListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessageSendListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		internal static HandleRef getCPtr(IChatRoomMessageSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		~IChatRoomMessageSendListener()
		{
			this.Dispose();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000C928 File Offset: 0x0000AB28
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomMessageSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomMessageSendListener.listeners.ContainsKey(handle))
					{
						IChatRoomMessageSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008A5 RID: 2213
		public abstract void OnChatRoomMessageSendSuccess(ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime);

		// Token: 0x060008A6 RID: 2214
		public abstract void OnChatRoomMessageSendFailure(ulong chatRoomID, uint sentMessageIndex, IChatRoomMessageSendListener.FailureReason failureReason);

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessageSendSuccess", IChatRoomMessageSendListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_0(IChatRoomMessageSendListener.SwigDirectorOnChatRoomMessageSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessageSendFailure", IChatRoomMessageSendListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_1(IChatRoomMessageSendListener.SwigDirectorOnChatRoomMessageSendFailure);
			}
			GalaxyInstancePINVOKE.IChatRoomMessageSendListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessageSendListener));
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000CA82 File Offset: 0x0000AC82
		[MonoPInvokeCallback(typeof(IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_0))]
		private static void SwigDirectorOnChatRoomMessageSendSuccess(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime)
		{
			if (IChatRoomMessageSendListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessageSendListener.listeners[cPtr].OnChatRoomMessageSendSuccess(chatRoomID, sentMessageIndex, messageID, sendTime);
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0000CAA9 File Offset: 0x0000ACA9
		[MonoPInvokeCallback(typeof(IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_1))]
		private static void SwigDirectorOnChatRoomMessageSendFailure(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, int failureReason)
		{
			if (IChatRoomMessageSendListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessageSendListener.listeners[cPtr].OnChatRoomMessageSendFailure(chatRoomID, sentMessageIndex, (IChatRoomMessageSendListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040000E5 RID: 229
		private static Dictionary<IntPtr, IChatRoomMessageSendListener> listeners = new Dictionary<IntPtr, IChatRoomMessageSendListener>();

		// Token: 0x040000E6 RID: 230
		private HandleRef swigCPtr;

		// Token: 0x040000E7 RID: 231
		private IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_0 swigDelegate0;

		// Token: 0x040000E8 RID: 232
		private IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_1 swigDelegate1;

		// Token: 0x040000E9 RID: 233
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(ulong),
			typeof(uint)
		};

		// Token: 0x040000EA RID: 234
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(IChatRoomMessageSendListener.FailureReason)
		};

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x060008AD RID: 2221
		public delegate void SwigDelegateIChatRoomMessageSendListener_0(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime);

		// Token: 0x020000B3 RID: 179
		// (Invoke) Token: 0x060008B1 RID: 2225
		public delegate void SwigDelegateIChatRoomMessageSendListener_1(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, int failureReason);

		// Token: 0x020000B4 RID: 180
		public enum FailureReason
		{
			// Token: 0x040000EC RID: 236
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040000ED RID: 237
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x040000EE RID: 238
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
