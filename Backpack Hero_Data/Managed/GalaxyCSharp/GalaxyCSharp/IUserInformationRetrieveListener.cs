using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000181 RID: 385
	public abstract class IUserInformationRetrieveListener : GalaxyTypeAwareListenerUserInformationRetrieve
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		internal IUserInformationRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserInformationRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserInformationRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0000BF08 File Offset: 0x0000A108
		public IUserInformationRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserInformationRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0000BF2C File Offset: 0x0000A12C
		internal static HandleRef getCPtr(IUserInformationRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0000BF4C File Offset: 0x0000A14C
		~IUserInformationRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0000BF7C File Offset: 0x0000A17C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserInformationRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserInformationRetrieveListener.listeners.ContainsKey(handle))
					{
						IUserInformationRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000E45 RID: 3653
		public abstract void OnUserInformationRetrieveSuccess(GalaxyID userID);

		// Token: 0x06000E46 RID: 3654
		public abstract void OnUserInformationRetrieveFailure(GalaxyID userID, IUserInformationRetrieveListener.FailureReason failureReason);

		// Token: 0x06000E47 RID: 3655 RVA: 0x0000C02C File Offset: 0x0000A22C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserInformationRetrieveSuccess", IUserInformationRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_0(IUserInformationRetrieveListener.SwigDirectorOnUserInformationRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserInformationRetrieveFailure", IUserInformationRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_1(IUserInformationRetrieveListener.SwigDirectorOnUserInformationRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IUserInformationRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserInformationRetrieveListener));
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		[MonoPInvokeCallback(typeof(IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_0))]
		private static void SwigDirectorOnUserInformationRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IUserInformationRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserInformationRetrieveListener.listeners[cPtr].OnUserInformationRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0000C109 File Offset: 0x0000A309
		[MonoPInvokeCallback(typeof(IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_1))]
		private static void SwigDirectorOnUserInformationRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IUserInformationRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserInformationRetrieveListener.listeners[cPtr].OnUserInformationRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IUserInformationRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002ED RID: 749
		private static Dictionary<IntPtr, IUserInformationRetrieveListener> listeners = new Dictionary<IntPtr, IUserInformationRetrieveListener>();

		// Token: 0x040002EE RID: 750
		private HandleRef swigCPtr;

		// Token: 0x040002EF RID: 751
		private IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_0 swigDelegate0;

		// Token: 0x040002F0 RID: 752
		private IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_1 swigDelegate1;

		// Token: 0x040002F1 RID: 753
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040002F2 RID: 754
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IUserInformationRetrieveListener.FailureReason)
		};

		// Token: 0x02000182 RID: 386
		// (Invoke) Token: 0x06000E4D RID: 3661
		public delegate void SwigDelegateIUserInformationRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x02000183 RID: 387
		// (Invoke) Token: 0x06000E51 RID: 3665
		public delegate void SwigDelegateIUserInformationRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x02000184 RID: 388
		public enum FailureReason
		{
			// Token: 0x040002F4 RID: 756
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002F5 RID: 757
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
