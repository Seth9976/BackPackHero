using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000108 RID: 264
	public abstract class IIsDlcOwnedListener : GalaxyTypeAwareListenerIsDlcOwned
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x00019248 File Offset: 0x00017448
		internal IIsDlcOwnedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IIsDlcOwnedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IIsDlcOwnedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00019270 File Offset: 0x00017470
		public IIsDlcOwnedListener()
			: this(GalaxyInstancePINVOKE.new_IIsDlcOwnedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00019294 File Offset: 0x00017494
		internal static HandleRef getCPtr(IIsDlcOwnedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000192B4 File Offset: 0x000174B4
		~IIsDlcOwnedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000192E4 File Offset: 0x000174E4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IIsDlcOwnedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IIsDlcOwnedListener.listeners.ContainsKey(handle))
					{
						IIsDlcOwnedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AC0 RID: 2752
		public abstract void OnDlcCheckSuccess(ulong producId, bool isOwned);

		// Token: 0x06000AC1 RID: 2753
		public abstract void OnDlcCheckFailure(ulong producId, IIsDlcOwnedListener.FailureReason failueReason);

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00019394 File Offset: 0x00017594
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnDlcCheckSuccess", IIsDlcOwnedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IIsDlcOwnedListener.SwigDelegateIIsDlcOwnedListener_0(IIsDlcOwnedListener.SwigDirectorOnDlcCheckSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnDlcCheckFailure", IIsDlcOwnedListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IIsDlcOwnedListener.SwigDelegateIIsDlcOwnedListener_1(IIsDlcOwnedListener.SwigDirectorOnDlcCheckFailure);
			}
			GalaxyInstancePINVOKE.IIsDlcOwnedListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00019408 File Offset: 0x00017608
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IIsDlcOwnedListener));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001943E File Offset: 0x0001763E
		[MonoPInvokeCallback(typeof(IIsDlcOwnedListener.SwigDelegateIIsDlcOwnedListener_0))]
		private static void SwigDirectorOnDlcCheckSuccess(IntPtr cPtr, ulong producId, bool isOwned)
		{
			if (IIsDlcOwnedListener.listeners.ContainsKey(cPtr))
			{
				IIsDlcOwnedListener.listeners[cPtr].OnDlcCheckSuccess(producId, isOwned);
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00019462 File Offset: 0x00017662
		[MonoPInvokeCallback(typeof(IIsDlcOwnedListener.SwigDelegateIIsDlcOwnedListener_1))]
		private static void SwigDirectorOnDlcCheckFailure(IntPtr cPtr, ulong producId, int failueReason)
		{
			if (IIsDlcOwnedListener.listeners.ContainsKey(cPtr))
			{
				IIsDlcOwnedListener.listeners[cPtr].OnDlcCheckFailure(producId, (IIsDlcOwnedListener.FailureReason)failueReason);
			}
		}

		// Token: 0x040001C9 RID: 457
		private static Dictionary<IntPtr, IIsDlcOwnedListener> listeners = new Dictionary<IntPtr, IIsDlcOwnedListener>();

		// Token: 0x040001CA RID: 458
		private HandleRef swigCPtr;

		// Token: 0x040001CB RID: 459
		private IIsDlcOwnedListener.SwigDelegateIIsDlcOwnedListener_0 swigDelegate0;

		// Token: 0x040001CC RID: 460
		private IIsDlcOwnedListener.SwigDelegateIIsDlcOwnedListener_1 swigDelegate1;

		// Token: 0x040001CD RID: 461
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(bool)
		};

		// Token: 0x040001CE RID: 462
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(ulong),
			typeof(IIsDlcOwnedListener.FailureReason)
		};

		// Token: 0x02000109 RID: 265
		// (Invoke) Token: 0x06000AC8 RID: 2760
		public delegate void SwigDelegateIIsDlcOwnedListener_0(IntPtr cPtr, ulong producId, bool isOwned);

		// Token: 0x0200010A RID: 266
		// (Invoke) Token: 0x06000ACC RID: 2764
		public delegate void SwigDelegateIIsDlcOwnedListener_1(IntPtr cPtr, ulong producId, int failueReason);

		// Token: 0x0200010B RID: 267
		public enum FailureReason
		{
			// Token: 0x040001D0 RID: 464
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001D1 RID: 465
			FAILURE_REASON_GALAXY_SERVICE_NOT_SIGNED_IN,
			// Token: 0x040001D2 RID: 466
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x040001D3 RID: 467
			FAILURE_REASON_EXTERNAL_SERVICE_FAILURE
		}
	}
}
