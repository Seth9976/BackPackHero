using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200010C RID: 268
	public abstract class ILeaderboardEntriesRetrieveListener : GalaxyTypeAwareListenerLeaderboardEntriesRetrieve
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x00010978 File Offset: 0x0000EB78
		internal ILeaderboardEntriesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardEntriesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardEntriesRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000109A0 File Offset: 0x0000EBA0
		public ILeaderboardEntriesRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardEntriesRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000109C4 File Offset: 0x0000EBC4
		internal static HandleRef getCPtr(ILeaderboardEntriesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000109E4 File Offset: 0x0000EBE4
		~ILeaderboardEntriesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00010A14 File Offset: 0x0000EC14
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardEntriesRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardEntriesRetrieveListener.listeners.ContainsKey(handle))
					{
						ILeaderboardEntriesRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AD4 RID: 2772
		public abstract void OnLeaderboardEntriesRetrieveSuccess(string name, uint entryCount);

		// Token: 0x06000AD5 RID: 2773
		public abstract void OnLeaderboardEntriesRetrieveFailure(string name, ILeaderboardEntriesRetrieveListener.FailureReason failureReason);

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardEntriesRetrieveSuccess", ILeaderboardEntriesRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_0(ILeaderboardEntriesRetrieveListener.SwigDirectorOnLeaderboardEntriesRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardEntriesRetrieveFailure", ILeaderboardEntriesRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_1(ILeaderboardEntriesRetrieveListener.SwigDirectorOnLeaderboardEntriesRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardEntriesRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00010B38 File Offset: 0x0000ED38
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardEntriesRetrieveListener));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00010B6E File Offset: 0x0000ED6E
		[MonoPInvokeCallback(typeof(ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardEntriesRetrieveSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint entryCount)
		{
			if (ILeaderboardEntriesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardEntriesRetrieveListener.listeners[cPtr].OnLeaderboardEntriesRetrieveSuccess(name, entryCount);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00010B92 File Offset: 0x0000ED92
		[MonoPInvokeCallback(typeof(ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardEntriesRetrieveFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ILeaderboardEntriesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardEntriesRetrieveListener.listeners[cPtr].OnLeaderboardEntriesRetrieveFailure(name, (ILeaderboardEntriesRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001D4 RID: 468
		private static Dictionary<IntPtr, ILeaderboardEntriesRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardEntriesRetrieveListener>();

		// Token: 0x040001D5 RID: 469
		private HandleRef swigCPtr;

		// Token: 0x040001D6 RID: 470
		private ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_0 swigDelegate0;

		// Token: 0x040001D7 RID: 471
		private ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_1 swigDelegate1;

		// Token: 0x040001D8 RID: 472
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(uint)
		};

		// Token: 0x040001D9 RID: 473
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(ILeaderboardEntriesRetrieveListener.FailureReason)
		};

		// Token: 0x0200010D RID: 269
		// (Invoke) Token: 0x06000ADC RID: 2780
		public delegate void SwigDelegateILeaderboardEntriesRetrieveListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint entryCount);

		// Token: 0x0200010E RID: 270
		// (Invoke) Token: 0x06000AE0 RID: 2784
		public delegate void SwigDelegateILeaderboardEntriesRetrieveListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x0200010F RID: 271
		public enum FailureReason
		{
			// Token: 0x040001DB RID: 475
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001DC RID: 476
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x040001DD RID: 477
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
