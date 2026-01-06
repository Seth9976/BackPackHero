using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000129 RID: 297
	public abstract class ILobbyEnteredListener : GalaxyTypeAwareListenerLobbyEntered
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x00009B3C File Offset: 0x00007D3C
		internal ILobbyEnteredListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyEnteredListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyEnteredListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00009B64 File Offset: 0x00007D64
		public ILobbyEnteredListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyEnteredListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00009B88 File Offset: 0x00007D88
		internal static HandleRef getCPtr(ILobbyEnteredListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00009BA8 File Offset: 0x00007DA8
		~ILobbyEnteredListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyEnteredListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyEnteredListener.listeners.ContainsKey(handle))
					{
						ILobbyEnteredListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B6E RID: 2926
		public abstract void OnLobbyEntered(GalaxyID lobbyID, LobbyEnterResult _result);

		// Token: 0x06000B6F RID: 2927 RVA: 0x00009C88 File Offset: 0x00007E88
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyEntered", ILobbyEnteredListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyEnteredListener.SwigDelegateILobbyEnteredListener_0(ILobbyEnteredListener.SwigDirectorOnLobbyEntered);
			}
			GalaxyInstancePINVOKE.ILobbyEnteredListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00009CC4 File Offset: 0x00007EC4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyEnteredListener));
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00009CFA File Offset: 0x00007EFA
		[MonoPInvokeCallback(typeof(ILobbyEnteredListener.SwigDelegateILobbyEnteredListener_0))]
		private static void SwigDirectorOnLobbyEntered(IntPtr cPtr, IntPtr lobbyID, int _result)
		{
			if (ILobbyEnteredListener.listeners.ContainsKey(cPtr))
			{
				ILobbyEnteredListener.listeners[cPtr].OnLobbyEntered(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (LobbyEnterResult)_result);
			}
		}

		// Token: 0x04000218 RID: 536
		private static Dictionary<IntPtr, ILobbyEnteredListener> listeners = new Dictionary<IntPtr, ILobbyEnteredListener>();

		// Token: 0x04000219 RID: 537
		private HandleRef swigCPtr;

		// Token: 0x0400021A RID: 538
		private ILobbyEnteredListener.SwigDelegateILobbyEnteredListener_0 swigDelegate0;

		// Token: 0x0400021B RID: 539
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(LobbyEnterResult)
		};

		// Token: 0x0200012A RID: 298
		// (Invoke) Token: 0x06000B74 RID: 2932
		public delegate void SwigDelegateILobbyEnteredListener_0(IntPtr cPtr, IntPtr lobbyID, int _result);
	}
}
