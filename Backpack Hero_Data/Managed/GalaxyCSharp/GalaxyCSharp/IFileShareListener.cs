using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000E1 RID: 225
	public abstract class IFileShareListener : GalaxyTypeAwareListenerFileShare
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x0000E31C File Offset: 0x0000C51C
		internal IFileShareListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFileShareListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFileShareListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000E344 File Offset: 0x0000C544
		public IFileShareListener()
			: this(GalaxyInstancePINVOKE.new_IFileShareListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0000E368 File Offset: 0x0000C568
		internal static HandleRef getCPtr(IFileShareListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0000E388 File Offset: 0x0000C588
		~IFileShareListener()
		{
			this.Dispose();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFileShareListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFileShareListener.listeners.ContainsKey(handle))
					{
						IFileShareListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009B3 RID: 2483
		public abstract void OnFileShareSuccess(string fileName, ulong sharedFileID);

		// Token: 0x060009B4 RID: 2484
		public abstract void OnFileShareFailure(string fileName, IFileShareListener.FailureReason failureReason);

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000E468 File Offset: 0x0000C668
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFileShareSuccess", IFileShareListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFileShareListener.SwigDelegateIFileShareListener_0(IFileShareListener.SwigDirectorOnFileShareSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFileShareFailure", IFileShareListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFileShareListener.SwigDelegateIFileShareListener_1(IFileShareListener.SwigDirectorOnFileShareFailure);
			}
			GalaxyInstancePINVOKE.IFileShareListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFileShareListener));
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0000E512 File Offset: 0x0000C712
		[MonoPInvokeCallback(typeof(IFileShareListener.SwigDelegateIFileShareListener_0))]
		private static void SwigDirectorOnFileShareSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, ulong sharedFileID)
		{
			if (IFileShareListener.listeners.ContainsKey(cPtr))
			{
				IFileShareListener.listeners[cPtr].OnFileShareSuccess(fileName, sharedFileID);
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000E536 File Offset: 0x0000C736
		[MonoPInvokeCallback(typeof(IFileShareListener.SwigDelegateIFileShareListener_1))]
		private static void SwigDirectorOnFileShareFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, int failureReason)
		{
			if (IFileShareListener.listeners.ContainsKey(cPtr))
			{
				IFileShareListener.listeners[cPtr].OnFileShareFailure(fileName, (IFileShareListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400016F RID: 367
		private static Dictionary<IntPtr, IFileShareListener> listeners = new Dictionary<IntPtr, IFileShareListener>();

		// Token: 0x04000170 RID: 368
		private HandleRef swigCPtr;

		// Token: 0x04000171 RID: 369
		private IFileShareListener.SwigDelegateIFileShareListener_0 swigDelegate0;

		// Token: 0x04000172 RID: 370
		private IFileShareListener.SwigDelegateIFileShareListener_1 swigDelegate1;

		// Token: 0x04000173 RID: 371
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(ulong)
		};

		// Token: 0x04000174 RID: 372
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(IFileShareListener.FailureReason)
		};

		// Token: 0x020000E2 RID: 226
		// (Invoke) Token: 0x060009BB RID: 2491
		public delegate void SwigDelegateIFileShareListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, ulong sharedFileID);

		// Token: 0x020000E3 RID: 227
		// (Invoke) Token: 0x060009BF RID: 2495
		public delegate void SwigDelegateIFileShareListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, int failureReason);

		// Token: 0x020000E4 RID: 228
		public enum FailureReason
		{
			// Token: 0x04000176 RID: 374
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000177 RID: 375
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
