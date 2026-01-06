using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200012E RID: 302
	public abstract class ILobbyListListener : GalaxyTypeAwareListenerLobbyList
	{
		// Token: 0x06000B85 RID: 2949 RVA: 0x00011FA0 File Offset: 0x000101A0
		internal ILobbyListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyListListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00011FC8 File Offset: 0x000101C8
		public ILobbyListListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyListListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00011FEC File Offset: 0x000101EC
		internal static HandleRef getCPtr(ILobbyListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0001200C File Offset: 0x0001020C
		~ILobbyListListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0001203C File Offset: 0x0001023C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyListListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyListListener.listeners.ContainsKey(handle))
					{
						ILobbyListListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B8A RID: 2954
		public abstract void OnLobbyList(uint lobbyCount, LobbyListResult _result);

		// Token: 0x06000B8B RID: 2955 RVA: 0x000120EC File Offset: 0x000102EC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyList", ILobbyListListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyListListener.SwigDelegateILobbyListListener_0(ILobbyListListener.SwigDirectorOnLobbyList);
			}
			GalaxyInstancePINVOKE.ILobbyListListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00012128 File Offset: 0x00010328
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyListListener));
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0001215E File Offset: 0x0001035E
		[MonoPInvokeCallback(typeof(ILobbyListListener.SwigDelegateILobbyListListener_0))]
		private static void SwigDirectorOnLobbyList(IntPtr cPtr, uint lobbyCount, int _result)
		{
			if (ILobbyListListener.listeners.ContainsKey(cPtr))
			{
				ILobbyListListener.listeners[cPtr].OnLobbyList(lobbyCount, (LobbyListResult)_result);
			}
		}

		// Token: 0x04000225 RID: 549
		private static Dictionary<IntPtr, ILobbyListListener> listeners = new Dictionary<IntPtr, ILobbyListListener>();

		// Token: 0x04000226 RID: 550
		private HandleRef swigCPtr;

		// Token: 0x04000227 RID: 551
		private ILobbyListListener.SwigDelegateILobbyListListener_0 swigDelegate0;

		// Token: 0x04000228 RID: 552
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(uint),
			typeof(LobbyListResult)
		};

		// Token: 0x0200012F RID: 303
		// (Invoke) Token: 0x06000B90 RID: 2960
		public delegate void SwigDelegateILobbyListListener_0(IntPtr cPtr, uint lobbyCount, int _result);
	}
}
