using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000118 RID: 280
	public abstract class ILeaderboardsRetrieveListener : GalaxyTypeAwareListenerLeaderboardsRetrieve
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x00011500 File Offset: 0x0000F700
		internal ILeaderboardsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardsRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00011528 File Offset: 0x0000F728
		public ILeaderboardsRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardsRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001154C File Offset: 0x0000F74C
		internal static HandleRef getCPtr(ILeaderboardsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001156C File Offset: 0x0000F76C
		~ILeaderboardsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001159C File Offset: 0x0000F79C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardsRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardsRetrieveListener.listeners.ContainsKey(handle))
					{
						ILeaderboardsRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B10 RID: 2832
		public abstract void OnLeaderboardsRetrieveSuccess();

		// Token: 0x06000B11 RID: 2833
		public abstract void OnLeaderboardsRetrieveFailure(ILeaderboardsRetrieveListener.FailureReason failureReason);

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001164C File Offset: 0x0000F84C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardsRetrieveSuccess", ILeaderboardsRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_0(ILeaderboardsRetrieveListener.SwigDirectorOnLeaderboardsRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardsRetrieveFailure", ILeaderboardsRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_1(ILeaderboardsRetrieveListener.SwigDirectorOnLeaderboardsRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardsRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000116C0 File Offset: 0x0000F8C0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardsRetrieveListener));
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000116F6 File Offset: 0x0000F8F6
		[MonoPInvokeCallback(typeof(ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardsRetrieveSuccess(IntPtr cPtr)
		{
			if (ILeaderboardsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardsRetrieveListener.listeners[cPtr].OnLeaderboardsRetrieveSuccess();
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00011718 File Offset: 0x0000F918
		[MonoPInvokeCallback(typeof(ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardsRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (ILeaderboardsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardsRetrieveListener.listeners[cPtr].OnLeaderboardsRetrieveFailure((ILeaderboardsRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001F1 RID: 497
		private static Dictionary<IntPtr, ILeaderboardsRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardsRetrieveListener>();

		// Token: 0x040001F2 RID: 498
		private HandleRef swigCPtr;

		// Token: 0x040001F3 RID: 499
		private ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_0 swigDelegate0;

		// Token: 0x040001F4 RID: 500
		private ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_1 swigDelegate1;

		// Token: 0x040001F5 RID: 501
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040001F6 RID: 502
		private static Type[] swigMethodTypes1 = new Type[] { typeof(ILeaderboardsRetrieveListener.FailureReason) };

		// Token: 0x02000119 RID: 281
		// (Invoke) Token: 0x06000B18 RID: 2840
		public delegate void SwigDelegateILeaderboardsRetrieveListener_0(IntPtr cPtr);

		// Token: 0x0200011A RID: 282
		// (Invoke) Token: 0x06000B1C RID: 2844
		public delegate void SwigDelegateILeaderboardsRetrieveListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x0200011B RID: 283
		public enum FailureReason
		{
			// Token: 0x040001F8 RID: 504
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001F9 RID: 505
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
