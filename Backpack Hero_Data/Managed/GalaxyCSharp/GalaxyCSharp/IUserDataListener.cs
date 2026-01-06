using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200017B RID: 379
	public abstract class IUserDataListener : GalaxyTypeAwareListenerUserData
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x00015A24 File Offset: 0x00013C24
		internal IUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00015A4C File Offset: 0x00013C4C
		public IUserDataListener()
			: this(GalaxyInstancePINVOKE.new_IUserDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00015A70 File Offset: 0x00013C70
		internal static HandleRef getCPtr(IUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00015A90 File Offset: 0x00013C90
		~IUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00015AC0 File Offset: 0x00013CC0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserDataListener.listeners.ContainsKey(handle))
					{
						IUserDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000E23 RID: 3619
		public abstract void OnUserDataUpdated();

		// Token: 0x06000E24 RID: 3620 RVA: 0x00015B70 File Offset: 0x00013D70
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserDataUpdated", IUserDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserDataListener.SwigDelegateIUserDataListener_0(IUserDataListener.SwigDirectorOnUserDataUpdated);
			}
			GalaxyInstancePINVOKE.IUserDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00015BAC File Offset: 0x00013DAC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserDataListener));
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00015BE2 File Offset: 0x00013DE2
		[MonoPInvokeCallback(typeof(IUserDataListener.SwigDelegateIUserDataListener_0))]
		private static void SwigDirectorOnUserDataUpdated(IntPtr cPtr)
		{
			if (IUserDataListener.listeners.ContainsKey(cPtr))
			{
				IUserDataListener.listeners[cPtr].OnUserDataUpdated();
			}
		}

		// Token: 0x040002DF RID: 735
		private static Dictionary<IntPtr, IUserDataListener> listeners = new Dictionary<IntPtr, IUserDataListener>();

		// Token: 0x040002E0 RID: 736
		private HandleRef swigCPtr;

		// Token: 0x040002E1 RID: 737
		private IUserDataListener.SwigDelegateIUserDataListener_0 swigDelegate0;

		// Token: 0x040002E2 RID: 738
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x0200017C RID: 380
		// (Invoke) Token: 0x06000E29 RID: 3625
		public delegate void SwigDelegateIUserDataListener_0(IntPtr cPtr);
	}
}
