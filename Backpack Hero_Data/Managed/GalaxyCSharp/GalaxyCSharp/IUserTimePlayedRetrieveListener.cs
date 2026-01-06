using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000189 RID: 393
	public abstract class IUserTimePlayedRetrieveListener : GalaxyTypeAwareListenerUserTimePlayedRetrieve
	{
		// Token: 0x06000E68 RID: 3688 RVA: 0x00016674 File Offset: 0x00014874
		internal IUserTimePlayedRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserTimePlayedRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserTimePlayedRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0001669C File Offset: 0x0001489C
		public IUserTimePlayedRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserTimePlayedRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000166C0 File Offset: 0x000148C0
		internal static HandleRef getCPtr(IUserTimePlayedRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000166E0 File Offset: 0x000148E0
		~IUserTimePlayedRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00016710 File Offset: 0x00014910
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserTimePlayedRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserTimePlayedRetrieveListener.listeners.ContainsKey(handle))
					{
						IUserTimePlayedRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000E6D RID: 3693
		public abstract void OnUserTimePlayedRetrieveSuccess(GalaxyID userID);

		// Token: 0x06000E6E RID: 3694
		public abstract void OnUserTimePlayedRetrieveFailure(GalaxyID userID, IUserTimePlayedRetrieveListener.FailureReason failureReason);

		// Token: 0x06000E6F RID: 3695 RVA: 0x000167C0 File Offset: 0x000149C0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserTimePlayedRetrieveSuccess", IUserTimePlayedRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_0(IUserTimePlayedRetrieveListener.SwigDirectorOnUserTimePlayedRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserTimePlayedRetrieveFailure", IUserTimePlayedRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_1(IUserTimePlayedRetrieveListener.SwigDirectorOnUserTimePlayedRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IUserTimePlayedRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00016834 File Offset: 0x00014A34
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserTimePlayedRetrieveListener));
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0001686A File Offset: 0x00014A6A
		[MonoPInvokeCallback(typeof(IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_0))]
		private static void SwigDirectorOnUserTimePlayedRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IUserTimePlayedRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserTimePlayedRetrieveListener.listeners[cPtr].OnUserTimePlayedRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0001689D File Offset: 0x00014A9D
		[MonoPInvokeCallback(typeof(IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_1))]
		private static void SwigDirectorOnUserTimePlayedRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IUserTimePlayedRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserTimePlayedRetrieveListener.listeners[cPtr].OnUserTimePlayedRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IUserTimePlayedRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002FF RID: 767
		private static Dictionary<IntPtr, IUserTimePlayedRetrieveListener> listeners = new Dictionary<IntPtr, IUserTimePlayedRetrieveListener>();

		// Token: 0x04000300 RID: 768
		private HandleRef swigCPtr;

		// Token: 0x04000301 RID: 769
		private IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_0 swigDelegate0;

		// Token: 0x04000302 RID: 770
		private IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_1 swigDelegate1;

		// Token: 0x04000303 RID: 771
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x04000304 RID: 772
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IUserTimePlayedRetrieveListener.FailureReason)
		};

		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x06000E75 RID: 3701
		public delegate void SwigDelegateIUserTimePlayedRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x0200018B RID: 395
		// (Invoke) Token: 0x06000E79 RID: 3705
		public delegate void SwigDelegateIUserTimePlayedRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x0200018C RID: 396
		public enum FailureReason
		{
			// Token: 0x04000306 RID: 774
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000307 RID: 775
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
