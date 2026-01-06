using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000104 RID: 260
	public abstract class IGogServicesConnectionStateListener : GalaxyTypeAwareListenerGogServicesConnectionState
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000898C File Offset: 0x00006B8C
		internal IGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IGogServicesConnectionStateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000089B4 File Offset: 0x00006BB4
		public IGogServicesConnectionStateListener()
			: this(GalaxyInstancePINVOKE.new_IGogServicesConnectionStateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000089D8 File Offset: 0x00006BD8
		internal static HandleRef getCPtr(IGogServicesConnectionStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000089F8 File Offset: 0x00006BF8
		~IGogServicesConnectionStateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00008A28 File Offset: 0x00006C28
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IGogServicesConnectionStateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IGogServicesConnectionStateListener.listeners.ContainsKey(handle))
					{
						IGogServicesConnectionStateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AAA RID: 2730
		public abstract void OnConnectionStateChange(GogServicesConnectionState connectionState);

		// Token: 0x06000AAB RID: 2731 RVA: 0x00008AD8 File Offset: 0x00006CD8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionStateChange", IGogServicesConnectionStateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IGogServicesConnectionStateListener.SwigDelegateIGogServicesConnectionStateListener_0(IGogServicesConnectionStateListener.SwigDirectorOnConnectionStateChange);
			}
			GalaxyInstancePINVOKE.IGogServicesConnectionStateListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00008B14 File Offset: 0x00006D14
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGogServicesConnectionStateListener));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00008B4A File Offset: 0x00006D4A
		[MonoPInvokeCallback(typeof(IGogServicesConnectionStateListener.SwigDelegateIGogServicesConnectionStateListener_0))]
		private static void SwigDirectorOnConnectionStateChange(IntPtr cPtr, int connectionState)
		{
			if (IGogServicesConnectionStateListener.listeners.ContainsKey(cPtr))
			{
				IGogServicesConnectionStateListener.listeners[cPtr].OnConnectionStateChange((GogServicesConnectionState)connectionState);
			}
		}

		// Token: 0x040001C3 RID: 451
		private static Dictionary<IntPtr, IGogServicesConnectionStateListener> listeners = new Dictionary<IntPtr, IGogServicesConnectionStateListener>();

		// Token: 0x040001C4 RID: 452
		private HandleRef swigCPtr;

		// Token: 0x040001C5 RID: 453
		private IGogServicesConnectionStateListener.SwigDelegateIGogServicesConnectionStateListener_0 swigDelegate0;

		// Token: 0x040001C6 RID: 454
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GogServicesConnectionState) };

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x06000AB0 RID: 2736
		public delegate void SwigDelegateIGogServicesConnectionStateListener_0(IntPtr cPtr, int connectionState);
	}
}
