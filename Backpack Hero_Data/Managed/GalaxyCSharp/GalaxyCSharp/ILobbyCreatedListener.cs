using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200011D RID: 285
	public abstract class ILobbyCreatedListener : GalaxyTypeAwareListenerLobbyCreated
	{
		// Token: 0x06000B25 RID: 2853 RVA: 0x00008CC0 File Offset: 0x00006EC0
		internal ILobbyCreatedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyCreatedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyCreatedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public ILobbyCreatedListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyCreatedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00008D0C File Offset: 0x00006F0C
		internal static HandleRef getCPtr(ILobbyCreatedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00008D2C File Offset: 0x00006F2C
		~ILobbyCreatedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00008D5C File Offset: 0x00006F5C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyCreatedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyCreatedListener.listeners.ContainsKey(handle))
					{
						ILobbyCreatedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B2A RID: 2858
		public abstract void OnLobbyCreated(GalaxyID lobbyID, LobbyCreateResult _result);

		// Token: 0x06000B2B RID: 2859 RVA: 0x00008E0C File Offset: 0x0000700C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyCreated", ILobbyCreatedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyCreatedListener.SwigDelegateILobbyCreatedListener_0(ILobbyCreatedListener.SwigDirectorOnLobbyCreated);
			}
			GalaxyInstancePINVOKE.ILobbyCreatedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00008E48 File Offset: 0x00007048
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyCreatedListener));
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00008E7E File Offset: 0x0000707E
		[MonoPInvokeCallback(typeof(ILobbyCreatedListener.SwigDelegateILobbyCreatedListener_0))]
		private static void SwigDirectorOnLobbyCreated(IntPtr cPtr, IntPtr lobbyID, int _result)
		{
			if (ILobbyCreatedListener.listeners.ContainsKey(cPtr))
			{
				ILobbyCreatedListener.listeners[cPtr].OnLobbyCreated(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (LobbyCreateResult)_result);
			}
		}

		// Token: 0x040001FC RID: 508
		private static Dictionary<IntPtr, ILobbyCreatedListener> listeners = new Dictionary<IntPtr, ILobbyCreatedListener>();

		// Token: 0x040001FD RID: 509
		private HandleRef swigCPtr;

		// Token: 0x040001FE RID: 510
		private ILobbyCreatedListener.SwigDelegateILobbyCreatedListener_0 swigDelegate0;

		// Token: 0x040001FF RID: 511
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(LobbyCreateResult)
		};

		// Token: 0x0200011E RID: 286
		// (Invoke) Token: 0x06000B30 RID: 2864
		public delegate void SwigDelegateILobbyCreatedListener_0(IntPtr cPtr, IntPtr lobbyID, int _result);
	}
}
