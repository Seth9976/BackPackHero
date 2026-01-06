using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000136 RID: 310
	public abstract class ILobbyMessageListener : GalaxyTypeAwareListenerLobbyMessage
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0000A98C File Offset: 0x00008B8C
		internal ILobbyMessageListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMessageListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyMessageListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		public ILobbyMessageListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMessageListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		internal static HandleRef getCPtr(ILobbyMessageListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		~ILobbyMessageListener()
		{
			this.Dispose();
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0000AA28 File Offset: 0x00008C28
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyMessageListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyMessageListener.listeners.ContainsKey(handle))
					{
						ILobbyMessageListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000BBA RID: 3002
		public abstract void OnLobbyMessageReceived(GalaxyID lobbyID, GalaxyID senderID, uint messageID, uint messageLength);

		// Token: 0x06000BBB RID: 3003 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyMessageReceived", ILobbyMessageListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyMessageListener.SwigDelegateILobbyMessageListener_0(ILobbyMessageListener.SwigDirectorOnLobbyMessageReceived);
			}
			GalaxyInstancePINVOKE.ILobbyMessageListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0000AB14 File Offset: 0x00008D14
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMessageListener));
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0000AB4C File Offset: 0x00008D4C
		[MonoPInvokeCallback(typeof(ILobbyMessageListener.SwigDelegateILobbyMessageListener_0))]
		private static void SwigDirectorOnLobbyMessageReceived(IntPtr cPtr, IntPtr lobbyID, IntPtr senderID, uint messageID, uint messageLength)
		{
			if (ILobbyMessageListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMessageListener.listeners[cPtr].OnLobbyMessageReceived(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(senderID, false).ToUint64()), messageID, messageLength);
			}
		}

		// Token: 0x04000237 RID: 567
		private static Dictionary<IntPtr, ILobbyMessageListener> listeners = new Dictionary<IntPtr, ILobbyMessageListener>();

		// Token: 0x04000238 RID: 568
		private HandleRef swigCPtr;

		// Token: 0x04000239 RID: 569
		private ILobbyMessageListener.SwigDelegateILobbyMessageListener_0 swigDelegate0;

		// Token: 0x0400023A RID: 570
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x02000137 RID: 311
		// (Invoke) Token: 0x06000BC0 RID: 3008
		public delegate void SwigDelegateILobbyMessageListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr senderID, uint messageID, uint messageLength);
	}
}
