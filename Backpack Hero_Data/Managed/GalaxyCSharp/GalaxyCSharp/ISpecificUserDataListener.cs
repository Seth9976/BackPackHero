using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200016C RID: 364
	public abstract class ISpecificUserDataListener : GalaxyTypeAwareListenerSpecificUserData
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x0000B7C0 File Offset: 0x000099C0
		internal ISpecificUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISpecificUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISpecificUserDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public ISpecificUserDataListener()
			: this(GalaxyInstancePINVOKE.new_ISpecificUserDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0000B80C File Offset: 0x00009A0C
		internal static HandleRef getCPtr(ISpecificUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0000B82C File Offset: 0x00009A2C
		~ISpecificUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0000B85C File Offset: 0x00009A5C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISpecificUserDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISpecificUserDataListener.listeners.ContainsKey(handle))
					{
						ISpecificUserDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000D36 RID: 3382
		public abstract void OnSpecificUserDataUpdated(GalaxyID userID);

		// Token: 0x06000D37 RID: 3383 RVA: 0x0000B90C File Offset: 0x00009B0C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnSpecificUserDataUpdated", ISpecificUserDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISpecificUserDataListener.SwigDelegateISpecificUserDataListener_0(ISpecificUserDataListener.SwigDirectorOnSpecificUserDataUpdated);
			}
			GalaxyInstancePINVOKE.ISpecificUserDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0000B948 File Offset: 0x00009B48
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISpecificUserDataListener));
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0000B97E File Offset: 0x00009B7E
		[MonoPInvokeCallback(typeof(ISpecificUserDataListener.SwigDelegateISpecificUserDataListener_0))]
		private static void SwigDirectorOnSpecificUserDataUpdated(IntPtr cPtr, IntPtr userID)
		{
			if (ISpecificUserDataListener.listeners.ContainsKey(cPtr))
			{
				ISpecificUserDataListener.listeners[cPtr].OnSpecificUserDataUpdated(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x040002B7 RID: 695
		private static Dictionary<IntPtr, ISpecificUserDataListener> listeners = new Dictionary<IntPtr, ISpecificUserDataListener>();

		// Token: 0x040002B8 RID: 696
		private HandleRef swigCPtr;

		// Token: 0x040002B9 RID: 697
		private ISpecificUserDataListener.SwigDelegateISpecificUserDataListener_0 swigDelegate0;

		// Token: 0x040002BA RID: 698
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x0200016D RID: 365
		// (Invoke) Token: 0x06000D3C RID: 3388
		public delegate void SwigDelegateISpecificUserDataListener_0(IntPtr cPtr, IntPtr userID);
	}
}
