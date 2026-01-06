using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000168 RID: 360
	public abstract class ISharedFileDownloadListener : GalaxyTypeAwareListenerSharedFileDownload
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x00015064 File Offset: 0x00013264
		internal ISharedFileDownloadListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISharedFileDownloadListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISharedFileDownloadListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0001508C File Offset: 0x0001328C
		public ISharedFileDownloadListener()
			: this(GalaxyInstancePINVOKE.new_ISharedFileDownloadListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000150B0 File Offset: 0x000132B0
		internal static HandleRef getCPtr(ISharedFileDownloadListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000150D0 File Offset: 0x000132D0
		~ISharedFileDownloadListener()
		{
			this.Dispose();
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00015100 File Offset: 0x00013300
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISharedFileDownloadListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISharedFileDownloadListener.listeners.ContainsKey(handle))
					{
						ISharedFileDownloadListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000D22 RID: 3362
		public abstract void OnSharedFileDownloadSuccess(ulong sharedFileID, string fileName);

		// Token: 0x06000D23 RID: 3363
		public abstract void OnSharedFileDownloadFailure(ulong sharedFileID, ISharedFileDownloadListener.FailureReason failureReason);

		// Token: 0x06000D24 RID: 3364 RVA: 0x000151B0 File Offset: 0x000133B0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnSharedFileDownloadSuccess", ISharedFileDownloadListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_0(ISharedFileDownloadListener.SwigDirectorOnSharedFileDownloadSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnSharedFileDownloadFailure", ISharedFileDownloadListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_1(ISharedFileDownloadListener.SwigDirectorOnSharedFileDownloadFailure);
			}
			GalaxyInstancePINVOKE.ISharedFileDownloadListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00015224 File Offset: 0x00013424
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISharedFileDownloadListener));
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0001525A File Offset: 0x0001345A
		[MonoPInvokeCallback(typeof(ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_0))]
		private static void SwigDirectorOnSharedFileDownloadSuccess(IntPtr cPtr, ulong sharedFileID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName)
		{
			if (ISharedFileDownloadListener.listeners.ContainsKey(cPtr))
			{
				ISharedFileDownloadListener.listeners[cPtr].OnSharedFileDownloadSuccess(sharedFileID, fileName);
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0001527E File Offset: 0x0001347E
		[MonoPInvokeCallback(typeof(ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_1))]
		private static void SwigDirectorOnSharedFileDownloadFailure(IntPtr cPtr, ulong sharedFileID, int failureReason)
		{
			if (ISharedFileDownloadListener.listeners.ContainsKey(cPtr))
			{
				ISharedFileDownloadListener.listeners[cPtr].OnSharedFileDownloadFailure(sharedFileID, (ISharedFileDownloadListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002AE RID: 686
		private static Dictionary<IntPtr, ISharedFileDownloadListener> listeners = new Dictionary<IntPtr, ISharedFileDownloadListener>();

		// Token: 0x040002AF RID: 687
		private HandleRef swigCPtr;

		// Token: 0x040002B0 RID: 688
		private ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_0 swigDelegate0;

		// Token: 0x040002B1 RID: 689
		private ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_1 swigDelegate1;

		// Token: 0x040002B2 RID: 690
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(string)
		};

		// Token: 0x040002B3 RID: 691
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(ulong),
			typeof(ISharedFileDownloadListener.FailureReason)
		};

		// Token: 0x02000169 RID: 361
		// (Invoke) Token: 0x06000D2A RID: 3370
		public delegate void SwigDelegateISharedFileDownloadListener_0(IntPtr cPtr, ulong sharedFileID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName);

		// Token: 0x0200016A RID: 362
		// (Invoke) Token: 0x06000D2E RID: 3374
		public delegate void SwigDelegateISharedFileDownloadListener_1(IntPtr cPtr, ulong sharedFileID, int failureReason);

		// Token: 0x0200016B RID: 363
		public enum FailureReason
		{
			// Token: 0x040002B5 RID: 693
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002B6 RID: 694
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
