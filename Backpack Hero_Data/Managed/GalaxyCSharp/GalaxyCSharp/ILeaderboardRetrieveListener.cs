using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000110 RID: 272
	public abstract class ILeaderboardRetrieveListener : GalaxyTypeAwareListenerLeaderboardRetrieve
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00010D48 File Offset: 0x0000EF48
		internal ILeaderboardRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00010D70 File Offset: 0x0000EF70
		public ILeaderboardRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00010D94 File Offset: 0x0000EF94
		internal static HandleRef getCPtr(ILeaderboardRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		~ILeaderboardRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardRetrieveListener.listeners.ContainsKey(handle))
					{
						ILeaderboardRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AE8 RID: 2792
		public abstract void OnLeaderboardRetrieveSuccess(string name);

		// Token: 0x06000AE9 RID: 2793
		public abstract void OnLeaderboardRetrieveFailure(string name, ILeaderboardRetrieveListener.FailureReason failureReason);

		// Token: 0x06000AEA RID: 2794 RVA: 0x00010E94 File Offset: 0x0000F094
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardRetrieveSuccess", ILeaderboardRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_0(ILeaderboardRetrieveListener.SwigDirectorOnLeaderboardRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardRetrieveFailure", ILeaderboardRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_1(ILeaderboardRetrieveListener.SwigDirectorOnLeaderboardRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00010F08 File Offset: 0x0000F108
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardRetrieveListener));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00010F3E File Offset: 0x0000F13E
		[MonoPInvokeCallback(typeof(ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardRetrieveSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (ILeaderboardRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardRetrieveListener.listeners[cPtr].OnLeaderboardRetrieveSuccess(name);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00010F61 File Offset: 0x0000F161
		[MonoPInvokeCallback(typeof(ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardRetrieveFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ILeaderboardRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardRetrieveListener.listeners[cPtr].OnLeaderboardRetrieveFailure(name, (ILeaderboardRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001DE RID: 478
		private static Dictionary<IntPtr, ILeaderboardRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardRetrieveListener>();

		// Token: 0x040001DF RID: 479
		private HandleRef swigCPtr;

		// Token: 0x040001E0 RID: 480
		private ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_0 swigDelegate0;

		// Token: 0x040001E1 RID: 481
		private ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_1 swigDelegate1;

		// Token: 0x040001E2 RID: 482
		private static Type[] swigMethodTypes0 = new Type[] { typeof(string) };

		// Token: 0x040001E3 RID: 483
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(ILeaderboardRetrieveListener.FailureReason)
		};

		// Token: 0x02000111 RID: 273
		// (Invoke) Token: 0x06000AF0 RID: 2800
		public delegate void SwigDelegateILeaderboardRetrieveListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);

		// Token: 0x02000112 RID: 274
		// (Invoke) Token: 0x06000AF4 RID: 2804
		public delegate void SwigDelegateILeaderboardRetrieveListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x02000113 RID: 275
		public enum FailureReason
		{
			// Token: 0x040001E5 RID: 485
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001E6 RID: 486
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
