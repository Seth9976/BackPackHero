using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000CD RID: 205
	public abstract class ICloudStoragePutFileListener : GalaxyTypeAwareListenerCloudStoragePutFile
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00017E09 File Offset: 0x00016009
		internal ICloudStoragePutFileListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStoragePutFileListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStoragePutFileListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00017E31 File Offset: 0x00016031
		public ICloudStoragePutFileListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStoragePutFileListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00017E55 File Offset: 0x00016055
		internal static HandleRef getCPtr(ICloudStoragePutFileListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00017E74 File Offset: 0x00016074
		~ICloudStoragePutFileListener()
		{
			this.Dispose();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00017EA4 File Offset: 0x000160A4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStoragePutFileListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStoragePutFileListener.listeners.ContainsKey(handle))
					{
						ICloudStoragePutFileListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000946 RID: 2374
		public abstract void OnPutFileSuccess(string container, string name);

		// Token: 0x06000947 RID: 2375
		public abstract void OnPutFileFailure(string container, string name, ICloudStoragePutFileListener.FailureReason failureReason);

		// Token: 0x06000948 RID: 2376 RVA: 0x00017F54 File Offset: 0x00016154
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnPutFileSuccess", ICloudStoragePutFileListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_0(ICloudStoragePutFileListener.SwigDirectorOnPutFileSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnPutFileFailure", ICloudStoragePutFileListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_1(ICloudStoragePutFileListener.SwigDirectorOnPutFileFailure);
			}
			GalaxyInstancePINVOKE.ICloudStoragePutFileListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00017FC8 File Offset: 0x000161C8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStoragePutFileListener));
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00017FFE File Offset: 0x000161FE
		[MonoPInvokeCallback(typeof(ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_0))]
		private static void SwigDirectorOnPutFileSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (ICloudStoragePutFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStoragePutFileListener.listeners[cPtr].OnPutFileSuccess(container, name);
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00018022 File Offset: 0x00016222
		[MonoPInvokeCallback(typeof(ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_1))]
		private static void SwigDirectorOnPutFileFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ICloudStoragePutFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStoragePutFileListener.listeners[cPtr].OnPutFileFailure(container, name, (ICloudStoragePutFileListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400013A RID: 314
		private static Dictionary<IntPtr, ICloudStoragePutFileListener> listeners = new Dictionary<IntPtr, ICloudStoragePutFileListener>();

		// Token: 0x0400013B RID: 315
		private HandleRef swigCPtr;

		// Token: 0x0400013C RID: 316
		private ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_0 swigDelegate0;

		// Token: 0x0400013D RID: 317
		private ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_1 swigDelegate1;

		// Token: 0x0400013E RID: 318
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(string)
		};

		// Token: 0x0400013F RID: 319
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(ICloudStoragePutFileListener.FailureReason)
		};

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x0600094E RID: 2382
		public delegate void SwigDelegateICloudStoragePutFileListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x06000952 RID: 2386
		public delegate void SwigDelegateICloudStoragePutFileListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x020000D0 RID: 208
		public enum FailureReason
		{
			// Token: 0x04000141 RID: 321
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000142 RID: 322
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000143 RID: 323
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000144 RID: 324
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000145 RID: 325
			FAILURE_REASON_ABORTED,
			// Token: 0x04000146 RID: 326
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x04000147 RID: 327
			FAILURE_REASON_READ_FUNC_ERROR,
			// Token: 0x04000148 RID: 328
			FAILURE_REASON_QUOTA_EXCEEDED
		}
	}
}
