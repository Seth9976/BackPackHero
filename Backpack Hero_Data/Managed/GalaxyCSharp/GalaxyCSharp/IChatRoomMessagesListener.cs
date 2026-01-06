using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000B5 RID: 181
	public abstract class IChatRoomMessagesListener : GalaxyTypeAwareListenerChatRoomMessages
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0000CC84 File Offset: 0x0000AE84
		internal IChatRoomMessagesListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessagesListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomMessagesListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public IChatRoomMessagesListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessagesListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		internal static HandleRef getCPtr(IChatRoomMessagesListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		~IChatRoomMessagesListener()
		{
			this.Dispose();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomMessagesListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomMessagesListener.listeners.ContainsKey(handle))
					{
						IChatRoomMessagesListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008B9 RID: 2233
		public abstract void OnChatRoomMessagesReceived(ulong chatRoomID, uint messageCount, uint longestMessageLenght);

		// Token: 0x060008BA RID: 2234 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessagesReceived", IChatRoomMessagesListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomMessagesListener.SwigDelegateIChatRoomMessagesListener_0(IChatRoomMessagesListener.SwigDirectorOnChatRoomMessagesReceived);
			}
			GalaxyInstancePINVOKE.IChatRoomMessagesListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000CE0C File Offset: 0x0000B00C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessagesListener));
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0000CE42 File Offset: 0x0000B042
		[MonoPInvokeCallback(typeof(IChatRoomMessagesListener.SwigDelegateIChatRoomMessagesListener_0))]
		private static void SwigDirectorOnChatRoomMessagesReceived(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght)
		{
			if (IChatRoomMessagesListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessagesListener.listeners[cPtr].OnChatRoomMessagesReceived(chatRoomID, messageCount, longestMessageLenght);
			}
		}

		// Token: 0x040000EF RID: 239
		private static Dictionary<IntPtr, IChatRoomMessagesListener> listeners = new Dictionary<IntPtr, IChatRoomMessagesListener>();

		// Token: 0x040000F0 RID: 240
		private HandleRef swigCPtr;

		// Token: 0x040000F1 RID: 241
		private IChatRoomMessagesListener.SwigDelegateIChatRoomMessagesListener_0 swigDelegate0;

		// Token: 0x040000F2 RID: 242
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x020000B6 RID: 182
		// (Invoke) Token: 0x060008BF RID: 2239
		public delegate void SwigDelegateIChatRoomMessagesListener_0(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght);
	}
}
