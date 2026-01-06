using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000DB RID: 219
	public abstract class IEncryptedAppTicketListener : GalaxyTypeAwareListenerEncryptedAppTicket
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x000085D4 File Offset: 0x000067D4
		internal IEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IEncryptedAppTicketListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000085FC File Offset: 0x000067FC
		public IEncryptedAppTicketListener()
			: this(GalaxyInstancePINVOKE.new_IEncryptedAppTicketListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00008620 File Offset: 0x00006820
		internal static HandleRef getCPtr(IEncryptedAppTicketListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00008640 File Offset: 0x00006840
		~IEncryptedAppTicketListener()
		{
			this.Dispose();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00008670 File Offset: 0x00006870
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IEncryptedAppTicketListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IEncryptedAppTicketListener.listeners.ContainsKey(handle))
					{
						IEncryptedAppTicketListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000997 RID: 2455
		public abstract void OnEncryptedAppTicketRetrieveSuccess();

		// Token: 0x06000998 RID: 2456 RVA: 0x00008720 File Offset: 0x00006920
		public virtual void OnEncryptedAppTicketRetrieveFailure(IEncryptedAppTicketListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IEncryptedAppTicketListener_OnEncryptedAppTicketRetrieveFailure(this.swigCPtr, (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00008740 File Offset: 0x00006940
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnEncryptedAppTicketRetrieveSuccess", IEncryptedAppTicketListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_0(IEncryptedAppTicketListener.SwigDirectorOnEncryptedAppTicketRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnEncryptedAppTicketRetrieveFailure", IEncryptedAppTicketListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_1(IEncryptedAppTicketListener.SwigDirectorOnEncryptedAppTicketRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IEncryptedAppTicketListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000087B4 File Offset: 0x000069B4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IEncryptedAppTicketListener));
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000087EA File Offset: 0x000069EA
		[MonoPInvokeCallback(typeof(IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_0))]
		private static void SwigDirectorOnEncryptedAppTicketRetrieveSuccess(IntPtr cPtr)
		{
			if (IEncryptedAppTicketListener.listeners.ContainsKey(cPtr))
			{
				IEncryptedAppTicketListener.listeners[cPtr].OnEncryptedAppTicketRetrieveSuccess();
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0000880C File Offset: 0x00006A0C
		[MonoPInvokeCallback(typeof(IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_1))]
		private static void SwigDirectorOnEncryptedAppTicketRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (IEncryptedAppTicketListener.listeners.ContainsKey(cPtr))
			{
				IEncryptedAppTicketListener.listeners[cPtr].OnEncryptedAppTicketRetrieveFailure((IEncryptedAppTicketListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400015F RID: 351
		private static Dictionary<IntPtr, IEncryptedAppTicketListener> listeners = new Dictionary<IntPtr, IEncryptedAppTicketListener>();

		// Token: 0x04000160 RID: 352
		private HandleRef swigCPtr;

		// Token: 0x04000161 RID: 353
		private IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_0 swigDelegate0;

		// Token: 0x04000162 RID: 354
		private IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_1 swigDelegate1;

		// Token: 0x04000163 RID: 355
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x04000164 RID: 356
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IEncryptedAppTicketListener.FailureReason) };

		// Token: 0x020000DC RID: 220
		// (Invoke) Token: 0x0600099F RID: 2463
		public delegate void SwigDelegateIEncryptedAppTicketListener_0(IntPtr cPtr);

		// Token: 0x020000DD RID: 221
		// (Invoke) Token: 0x060009A3 RID: 2467
		public delegate void SwigDelegateIEncryptedAppTicketListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000DE RID: 222
		public enum FailureReason
		{
			// Token: 0x04000166 RID: 358
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000167 RID: 359
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
