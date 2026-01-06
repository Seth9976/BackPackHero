using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000138 RID: 312
	public abstract class ILobbyOwnerChangeListener : GalaxyTypeAwareListenerLobbyOwnerChange
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x00012664 File Offset: 0x00010864
		internal ILobbyOwnerChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyOwnerChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyOwnerChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001268C File Offset: 0x0001088C
		public ILobbyOwnerChangeListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyOwnerChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000126B0 File Offset: 0x000108B0
		internal static HandleRef getCPtr(ILobbyOwnerChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000126D0 File Offset: 0x000108D0
		~ILobbyOwnerChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00012700 File Offset: 0x00010900
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyOwnerChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyOwnerChangeListener.listeners.ContainsKey(handle))
					{
						ILobbyOwnerChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000BC8 RID: 3016
		public abstract void OnLobbyOwnerChanged(GalaxyID lobbyID, GalaxyID newOwnerID);

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000127B0 File Offset: 0x000109B0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyOwnerChanged", ILobbyOwnerChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyOwnerChangeListener.SwigDelegateILobbyOwnerChangeListener_0(ILobbyOwnerChangeListener.SwigDirectorOnLobbyOwnerChanged);
			}
			GalaxyInstancePINVOKE.ILobbyOwnerChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000127EC File Offset: 0x000109EC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyOwnerChangeListener));
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00012824 File Offset: 0x00010A24
		[MonoPInvokeCallback(typeof(ILobbyOwnerChangeListener.SwigDelegateILobbyOwnerChangeListener_0))]
		private static void SwigDirectorOnLobbyOwnerChanged(IntPtr cPtr, IntPtr lobbyID, IntPtr newOwnerID)
		{
			if (ILobbyOwnerChangeListener.listeners.ContainsKey(cPtr))
			{
				ILobbyOwnerChangeListener.listeners[cPtr].OnLobbyOwnerChanged(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(newOwnerID, false).ToUint64()));
			}
		}

		// Token: 0x0400023B RID: 571
		private static Dictionary<IntPtr, ILobbyOwnerChangeListener> listeners = new Dictionary<IntPtr, ILobbyOwnerChangeListener>();

		// Token: 0x0400023C RID: 572
		private HandleRef swigCPtr;

		// Token: 0x0400023D RID: 573
		private ILobbyOwnerChangeListener.SwigDelegateILobbyOwnerChangeListener_0 swigDelegate0;

		// Token: 0x0400023E RID: 574
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x06000BCE RID: 3022
		public delegate void SwigDelegateILobbyOwnerChangeListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr newOwnerID);
	}
}
