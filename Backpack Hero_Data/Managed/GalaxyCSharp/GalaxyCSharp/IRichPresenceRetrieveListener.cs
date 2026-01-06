using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200015B RID: 347
	public abstract class IRichPresenceRetrieveListener : GalaxyTypeAwareListenerRichPresenceRetrieve
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x0000B398 File Offset: 0x00009598
		internal IRichPresenceRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IRichPresenceRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0000B3C0 File Offset: 0x000095C0
		public IRichPresenceRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal static HandleRef getCPtr(IRichPresenceRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0000B404 File Offset: 0x00009604
		~IRichPresenceRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0000B434 File Offset: 0x00009634
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRichPresenceRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IRichPresenceRetrieveListener.listeners.ContainsKey(handle))
					{
						IRichPresenceRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public virtual void OnRichPresenceRetrieveSuccess(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_OnRichPresenceRetrieveSuccess(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0000B507 File Offset: 0x00009707
		public virtual void OnRichPresenceRetrieveFailure(GalaxyID userID, IRichPresenceRetrieveListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_OnRichPresenceRetrieveFailure(this.swigCPtr, GalaxyID.getCPtr(userID), (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0000B52C File Offset: 0x0000972C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnRichPresenceRetrieveSuccess", IRichPresenceRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_0(IRichPresenceRetrieveListener.SwigDirectorOnRichPresenceRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnRichPresenceRetrieveFailure", IRichPresenceRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_1(IRichPresenceRetrieveListener.SwigDirectorOnRichPresenceRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0000B5A0 File Offset: 0x000097A0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceRetrieveListener));
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0000B5D6 File Offset: 0x000097D6
		[MonoPInvokeCallback(typeof(IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_0))]
		private static void SwigDirectorOnRichPresenceRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IRichPresenceRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceRetrieveListener.listeners[cPtr].OnRichPresenceRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0000B609 File Offset: 0x00009809
		[MonoPInvokeCallback(typeof(IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_1))]
		private static void SwigDirectorOnRichPresenceRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IRichPresenceRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceRetrieveListener.listeners[cPtr].OnRichPresenceRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IRichPresenceRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400028D RID: 653
		private static Dictionary<IntPtr, IRichPresenceRetrieveListener> listeners = new Dictionary<IntPtr, IRichPresenceRetrieveListener>();

		// Token: 0x0400028E RID: 654
		private HandleRef swigCPtr;

		// Token: 0x0400028F RID: 655
		private IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_0 swigDelegate0;

		// Token: 0x04000290 RID: 656
		private IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_1 swigDelegate1;

		// Token: 0x04000291 RID: 657
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x04000292 RID: 658
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IRichPresenceRetrieveListener.FailureReason)
		};

		// Token: 0x0200015C RID: 348
		// (Invoke) Token: 0x06000CEA RID: 3306
		public delegate void SwigDelegateIRichPresenceRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x0200015D RID: 349
		// (Invoke) Token: 0x06000CEE RID: 3310
		public delegate void SwigDelegateIRichPresenceRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x0200015E RID: 350
		public enum FailureReason
		{
			// Token: 0x04000294 RID: 660
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000295 RID: 661
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
