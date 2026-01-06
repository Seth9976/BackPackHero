using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000B7 RID: 183
	public abstract class IChatRoomMessagesRetrieveListener : GalaxyTypeAwareListenerChatRoomMessagesRetrieve
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		internal IChatRoomMessagesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessagesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomMessagesRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public IChatRoomMessagesRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessagesRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000D020 File Offset: 0x0000B220
		internal static HandleRef getCPtr(IChatRoomMessagesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000D040 File Offset: 0x0000B240
		~IChatRoomMessagesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000D070 File Offset: 0x0000B270
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomMessagesRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomMessagesRetrieveListener.listeners.ContainsKey(handle))
					{
						IChatRoomMessagesRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008C7 RID: 2247
		public abstract void OnChatRoomMessagesRetrieveSuccess(ulong chatRoomID, uint messageCount, uint longestMessageLenght);

		// Token: 0x060008C8 RID: 2248
		public abstract void OnChatRoomMessagesRetrieveFailure(ulong chatRoomID, IChatRoomMessagesRetrieveListener.FailureReason failureReason);

		// Token: 0x060008C9 RID: 2249 RVA: 0x0000D120 File Offset: 0x0000B320
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessagesRetrieveSuccess", IChatRoomMessagesRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_0(IChatRoomMessagesRetrieveListener.SwigDirectorOnChatRoomMessagesRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessagesRetrieveFailure", IChatRoomMessagesRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_1(IChatRoomMessagesRetrieveListener.SwigDirectorOnChatRoomMessagesRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IChatRoomMessagesRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000D194 File Offset: 0x0000B394
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessagesRetrieveListener));
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0000D1CA File Offset: 0x0000B3CA
		[MonoPInvokeCallback(typeof(IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_0))]
		private static void SwigDirectorOnChatRoomMessagesRetrieveSuccess(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght)
		{
			if (IChatRoomMessagesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessagesRetrieveListener.listeners[cPtr].OnChatRoomMessagesRetrieveSuccess(chatRoomID, messageCount, longestMessageLenght);
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0000D1EF File Offset: 0x0000B3EF
		[MonoPInvokeCallback(typeof(IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_1))]
		private static void SwigDirectorOnChatRoomMessagesRetrieveFailure(IntPtr cPtr, ulong chatRoomID, int failureReason)
		{
			if (IChatRoomMessagesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessagesRetrieveListener.listeners[cPtr].OnChatRoomMessagesRetrieveFailure(chatRoomID, (IChatRoomMessagesRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040000F3 RID: 243
		private static Dictionary<IntPtr, IChatRoomMessagesRetrieveListener> listeners = new Dictionary<IntPtr, IChatRoomMessagesRetrieveListener>();

		// Token: 0x040000F4 RID: 244
		private HandleRef swigCPtr;

		// Token: 0x040000F5 RID: 245
		private IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_0 swigDelegate0;

		// Token: 0x040000F6 RID: 246
		private IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_1 swigDelegate1;

		// Token: 0x040000F7 RID: 247
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x040000F8 RID: 248
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(ulong),
			typeof(IChatRoomMessagesRetrieveListener.FailureReason)
		};

		// Token: 0x020000B8 RID: 184
		// (Invoke) Token: 0x060008CF RID: 2255
		public delegate void SwigDelegateIChatRoomMessagesRetrieveListener_0(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght);

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x060008D3 RID: 2259
		public delegate void SwigDelegateIChatRoomMessagesRetrieveListener_1(IntPtr cPtr, ulong chatRoomID, int failureReason);

		// Token: 0x020000BA RID: 186
		public enum FailureReason
		{
			// Token: 0x040000FA RID: 250
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040000FB RID: 251
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x040000FC RID: 252
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
