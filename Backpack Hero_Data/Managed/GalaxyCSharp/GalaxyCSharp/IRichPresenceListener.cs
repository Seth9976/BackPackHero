using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000159 RID: 345
	public abstract class IRichPresenceListener : GalaxyTypeAwareListenerRichPresence
	{
		// Token: 0x06000CCF RID: 3279 RVA: 0x000143E0 File Offset: 0x000125E0
		internal IRichPresenceListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IRichPresenceListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00014408 File Offset: 0x00012608
		public IRichPresenceListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0001442C File Offset: 0x0001262C
		internal static HandleRef getCPtr(IRichPresenceListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0001444C File Offset: 0x0001264C
		~IRichPresenceListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0001447C File Offset: 0x0001267C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRichPresenceListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IRichPresenceListener.listeners.ContainsKey(handle))
					{
						IRichPresenceListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CD4 RID: 3284
		public abstract void OnRichPresenceUpdated(GalaxyID userID);

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0001452C File Offset: 0x0001272C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnRichPresenceUpdated", IRichPresenceListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IRichPresenceListener.SwigDelegateIRichPresenceListener_0(IRichPresenceListener.SwigDirectorOnRichPresenceUpdated);
			}
			GalaxyInstancePINVOKE.IRichPresenceListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00014568 File Offset: 0x00012768
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceListener));
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0001459E File Offset: 0x0001279E
		[MonoPInvokeCallback(typeof(IRichPresenceListener.SwigDelegateIRichPresenceListener_0))]
		private static void SwigDirectorOnRichPresenceUpdated(IntPtr cPtr, IntPtr userID)
		{
			if (IRichPresenceListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceListener.listeners[cPtr].OnRichPresenceUpdated(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x04000289 RID: 649
		private static Dictionary<IntPtr, IRichPresenceListener> listeners = new Dictionary<IntPtr, IRichPresenceListener>();

		// Token: 0x0400028A RID: 650
		private HandleRef swigCPtr;

		// Token: 0x0400028B RID: 651
		private IRichPresenceListener.SwigDelegateIRichPresenceListener_0 swigDelegate0;

		// Token: 0x0400028C RID: 652
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x0200015A RID: 346
		// (Invoke) Token: 0x06000CDA RID: 3290
		public delegate void SwigDelegateIRichPresenceListener_0(IntPtr cPtr, IntPtr userID);
	}
}
