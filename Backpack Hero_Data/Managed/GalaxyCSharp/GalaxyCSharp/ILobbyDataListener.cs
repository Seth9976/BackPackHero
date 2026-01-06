using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200011F RID: 287
	public abstract class ILobbyDataListener : GalaxyTypeAwareListenerLobbyData
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x00009010 File Offset: 0x00007210
		internal ILobbyDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00009038 File Offset: 0x00007238
		public ILobbyDataListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0000905C File Offset: 0x0000725C
		internal static HandleRef getCPtr(ILobbyDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0000907C File Offset: 0x0000727C
		~ILobbyDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000090AC File Offset: 0x000072AC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyDataListener.listeners.ContainsKey(handle))
					{
						ILobbyDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B38 RID: 2872
		public abstract void OnLobbyDataUpdated(GalaxyID lobbyID, GalaxyID memberID);

		// Token: 0x06000B39 RID: 2873 RVA: 0x0000915C File Offset: 0x0000735C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyDataUpdated", ILobbyDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyDataListener.SwigDelegateILobbyDataListener_0(ILobbyDataListener.SwigDirectorOnLobbyDataUpdated);
			}
			GalaxyInstancePINVOKE.ILobbyDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00009198 File Offset: 0x00007398
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataListener));
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000091D0 File Offset: 0x000073D0
		[MonoPInvokeCallback(typeof(ILobbyDataListener.SwigDelegateILobbyDataListener_0))]
		private static void SwigDirectorOnLobbyDataUpdated(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID)
		{
			if (ILobbyDataListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataListener.listeners[cPtr].OnLobbyDataUpdated(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()));
			}
		}

		// Token: 0x04000200 RID: 512
		private static Dictionary<IntPtr, ILobbyDataListener> listeners = new Dictionary<IntPtr, ILobbyDataListener>();

		// Token: 0x04000201 RID: 513
		private HandleRef swigCPtr;

		// Token: 0x04000202 RID: 514
		private ILobbyDataListener.SwigDelegateILobbyDataListener_0 swigDelegate0;

		// Token: 0x04000203 RID: 515
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};

		// Token: 0x02000120 RID: 288
		// (Invoke) Token: 0x06000B3E RID: 2878
		public delegate void SwigDelegateILobbyDataListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID);
	}
}
