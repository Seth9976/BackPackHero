using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200014C RID: 332
	public abstract class IOverlayVisibilityChangeListener : GalaxyTypeAwareListenerOverlayVisibilityChange
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x000139A8 File Offset: 0x00011BA8
		internal IOverlayVisibilityChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOverlayVisibilityChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000139D0 File Offset: 0x00011BD0
		public IOverlayVisibilityChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOverlayVisibilityChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x000139F4 File Offset: 0x00011BF4
		internal static HandleRef getCPtr(IOverlayVisibilityChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00013A14 File Offset: 0x00011C14
		~IOverlayVisibilityChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00013A44 File Offset: 0x00011C44
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOverlayVisibilityChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOverlayVisibilityChangeListener.listeners.ContainsKey(handle))
					{
						IOverlayVisibilityChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00013AF4 File Offset: 0x00011CF4
		public virtual void OnOverlayVisibilityChanged(bool overlayVisible)
		{
			GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_OnOverlayVisibilityChanged(this.swigCPtr, overlayVisible);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00013B12 File Offset: 0x00011D12
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOverlayVisibilityChanged", IOverlayVisibilityChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOverlayVisibilityChangeListener.SwigDelegateIOverlayVisibilityChangeListener_0(IOverlayVisibilityChangeListener.SwigDirectorOnOverlayVisibilityChanged);
			}
			GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00013B4C File Offset: 0x00011D4C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOverlayVisibilityChangeListener));
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00013B82 File Offset: 0x00011D82
		[MonoPInvokeCallback(typeof(IOverlayVisibilityChangeListener.SwigDelegateIOverlayVisibilityChangeListener_0))]
		private static void SwigDirectorOnOverlayVisibilityChanged(IntPtr cPtr, bool overlayVisible)
		{
			if (IOverlayVisibilityChangeListener.listeners.ContainsKey(cPtr))
			{
				IOverlayVisibilityChangeListener.listeners[cPtr].OnOverlayVisibilityChanged(overlayVisible);
			}
		}

		// Token: 0x04000264 RID: 612
		private static Dictionary<IntPtr, IOverlayVisibilityChangeListener> listeners = new Dictionary<IntPtr, IOverlayVisibilityChangeListener>();

		// Token: 0x04000265 RID: 613
		private HandleRef swigCPtr;

		// Token: 0x04000266 RID: 614
		private IOverlayVisibilityChangeListener.SwigDelegateIOverlayVisibilityChangeListener_0 swigDelegate0;

		// Token: 0x04000267 RID: 615
		private static Type[] swigMethodTypes0 = new Type[] { typeof(bool) };

		// Token: 0x0200014D RID: 333
		// (Invoke) Token: 0x06000C9C RID: 3228
		public delegate void SwigDelegateIOverlayVisibilityChangeListener_0(IntPtr cPtr, bool overlayVisible);
	}
}
