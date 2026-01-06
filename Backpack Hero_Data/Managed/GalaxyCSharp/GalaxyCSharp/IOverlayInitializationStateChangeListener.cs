using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200014A RID: 330
	public abstract class IOverlayInitializationStateChangeListener : GalaxyTypeAwareListenerOverlayInitializationStateChange
	{
		// Token: 0x06000C83 RID: 3203 RVA: 0x00013634 File Offset: 0x00011834
		internal IOverlayInitializationStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOverlayInitializationStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOverlayInitializationStateChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0001365C File Offset: 0x0001185C
		public IOverlayInitializationStateChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOverlayInitializationStateChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00013680 File Offset: 0x00011880
		internal static HandleRef getCPtr(IOverlayInitializationStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000136A0 File Offset: 0x000118A0
		~IOverlayInitializationStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000136D0 File Offset: 0x000118D0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOverlayInitializationStateChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOverlayInitializationStateChangeListener.listeners.ContainsKey(handle))
					{
						IOverlayInitializationStateChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C88 RID: 3208
		public abstract void OnOverlayStateChanged(OverlayState overlayState);

		// Token: 0x06000C89 RID: 3209 RVA: 0x00013780 File Offset: 0x00011980
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOverlayStateChanged", IOverlayInitializationStateChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOverlayInitializationStateChangeListener.SwigDelegateIOverlayInitializationStateChangeListener_0(IOverlayInitializationStateChangeListener.SwigDirectorOnOverlayStateChanged);
			}
			GalaxyInstancePINVOKE.IOverlayInitializationStateChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000137BC File Offset: 0x000119BC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOverlayInitializationStateChangeListener));
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000137F2 File Offset: 0x000119F2
		[MonoPInvokeCallback(typeof(IOverlayInitializationStateChangeListener.SwigDelegateIOverlayInitializationStateChangeListener_0))]
		private static void SwigDirectorOnOverlayStateChanged(IntPtr cPtr, int overlayState)
		{
			if (IOverlayInitializationStateChangeListener.listeners.ContainsKey(cPtr))
			{
				IOverlayInitializationStateChangeListener.listeners[cPtr].OnOverlayStateChanged((OverlayState)overlayState);
			}
		}

		// Token: 0x04000260 RID: 608
		private static Dictionary<IntPtr, IOverlayInitializationStateChangeListener> listeners = new Dictionary<IntPtr, IOverlayInitializationStateChangeListener>();

		// Token: 0x04000261 RID: 609
		private HandleRef swigCPtr;

		// Token: 0x04000262 RID: 610
		private IOverlayInitializationStateChangeListener.SwigDelegateIOverlayInitializationStateChangeListener_0 swigDelegate0;

		// Token: 0x04000263 RID: 611
		private static Type[] swigMethodTypes0 = new Type[] { typeof(OverlayState) };

		// Token: 0x0200014B RID: 331
		// (Invoke) Token: 0x06000C8E RID: 3214
		public delegate void SwigDelegateIOverlayInitializationStateChangeListener_0(IntPtr cPtr, int overlayState);
	}
}
