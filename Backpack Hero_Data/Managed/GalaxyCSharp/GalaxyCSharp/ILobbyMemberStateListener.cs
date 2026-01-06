using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000134 RID: 308
	public abstract class ILobbyMemberStateListener : GalaxyTypeAwareListenerLobbyMemberState
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0000A610 File Offset: 0x00008810
		internal ILobbyMemberStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMemberStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyMemberStateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0000A638 File Offset: 0x00008838
		public ILobbyMemberStateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMemberStateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0000A65C File Offset: 0x0000885C
		internal static HandleRef getCPtr(ILobbyMemberStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0000A67C File Offset: 0x0000887C
		~ILobbyMemberStateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0000A6AC File Offset: 0x000088AC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyMemberStateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyMemberStateListener.listeners.ContainsKey(handle))
					{
						ILobbyMemberStateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000BAC RID: 2988
		public abstract void OnLobbyMemberStateChanged(GalaxyID lobbyID, GalaxyID memberID, LobbyMemberStateChange memberStateChange);

		// Token: 0x06000BAD RID: 2989 RVA: 0x0000A75C File Offset: 0x0000895C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyMemberStateChanged", ILobbyMemberStateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyMemberStateListener.SwigDelegateILobbyMemberStateListener_0(ILobbyMemberStateListener.SwigDirectorOnLobbyMemberStateChanged);
			}
			GalaxyInstancePINVOKE.ILobbyMemberStateListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0000A798 File Offset: 0x00008998
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMemberStateListener));
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0000A7D0 File Offset: 0x000089D0
		[MonoPInvokeCallback(typeof(ILobbyMemberStateListener.SwigDelegateILobbyMemberStateListener_0))]
		private static void SwigDirectorOnLobbyMemberStateChanged(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int memberStateChange)
		{
			if (ILobbyMemberStateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMemberStateListener.listeners[cPtr].OnLobbyMemberStateChanged(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()), (LobbyMemberStateChange)memberStateChange);
			}
		}

		// Token: 0x04000233 RID: 563
		private static Dictionary<IntPtr, ILobbyMemberStateListener> listeners = new Dictionary<IntPtr, ILobbyMemberStateListener>();

		// Token: 0x04000234 RID: 564
		private HandleRef swigCPtr;

		// Token: 0x04000235 RID: 565
		private ILobbyMemberStateListener.SwigDelegateILobbyMemberStateListener_0 swigDelegate0;

		// Token: 0x04000236 RID: 566
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(LobbyMemberStateChange)
		};

		// Token: 0x02000135 RID: 309
		// (Invoke) Token: 0x06000BB2 RID: 2994
		public delegate void SwigDelegateILobbyMemberStateListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int memberStateChange);
	}
}
