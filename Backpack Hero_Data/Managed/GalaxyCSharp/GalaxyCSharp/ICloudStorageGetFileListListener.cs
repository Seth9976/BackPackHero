using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000C9 RID: 201
	public abstract class ICloudStorageGetFileListListener : GalaxyTypeAwareListenerCloudStorageGetFileList
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x00017B69 File Offset: 0x00015D69
		internal ICloudStorageGetFileListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStorageGetFileListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStorageGetFileListListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00017B91 File Offset: 0x00015D91
		public ICloudStorageGetFileListListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStorageGetFileListListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00017BB5 File Offset: 0x00015DB5
		internal static HandleRef getCPtr(ICloudStorageGetFileListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00017BD4 File Offset: 0x00015DD4
		~ICloudStorageGetFileListListener()
		{
			this.Dispose();
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00017C04 File Offset: 0x00015E04
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorageGetFileListListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStorageGetFileListListener.listeners.ContainsKey(handle))
					{
						ICloudStorageGetFileListListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000932 RID: 2354
		public abstract void OnGetFileListSuccess(uint fileCount, uint quota, uint quotaUsed);

		// Token: 0x06000933 RID: 2355
		public abstract void OnGetFileListFailure(ICloudStorageGetFileListListener.FailureReason failureReason);

		// Token: 0x06000934 RID: 2356 RVA: 0x00017CB4 File Offset: 0x00015EB4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGetFileListSuccess", ICloudStorageGetFileListListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_0(ICloudStorageGetFileListListener.SwigDirectorOnGetFileListSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnGetFileListFailure", ICloudStorageGetFileListListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_1(ICloudStorageGetFileListListener.SwigDirectorOnGetFileListFailure);
			}
			GalaxyInstancePINVOKE.ICloudStorageGetFileListListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00017D28 File Offset: 0x00015F28
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStorageGetFileListListener));
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00017D5E File Offset: 0x00015F5E
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_0))]
		private static void SwigDirectorOnGetFileListSuccess(IntPtr cPtr, uint fileCount, uint quota, uint quotaUsed)
		{
			if (ICloudStorageGetFileListListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListListener.listeners[cPtr].OnGetFileListSuccess(fileCount, quota, quotaUsed);
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00017D83 File Offset: 0x00015F83
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_1))]
		private static void SwigDirectorOnGetFileListFailure(IntPtr cPtr, int failureReason)
		{
			if (ICloudStorageGetFileListListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListListener.listeners[cPtr].OnGetFileListFailure((ICloudStorageGetFileListListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400012C RID: 300
		private static Dictionary<IntPtr, ICloudStorageGetFileListListener> listeners = new Dictionary<IntPtr, ICloudStorageGetFileListListener>();

		// Token: 0x0400012D RID: 301
		private HandleRef swigCPtr;

		// Token: 0x0400012E RID: 302
		private ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_0 swigDelegate0;

		// Token: 0x0400012F RID: 303
		private ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_1 swigDelegate1;

		// Token: 0x04000130 RID: 304
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(uint),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x04000131 RID: 305
		private static Type[] swigMethodTypes1 = new Type[] { typeof(ICloudStorageGetFileListListener.FailureReason) };

		// Token: 0x020000CA RID: 202
		// (Invoke) Token: 0x0600093A RID: 2362
		public delegate void SwigDelegateICloudStorageGetFileListListener_0(IntPtr cPtr, uint fileCount, uint quota, uint quotaUsed);

		// Token: 0x020000CB RID: 203
		// (Invoke) Token: 0x0600093E RID: 2366
		public delegate void SwigDelegateICloudStorageGetFileListListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000CC RID: 204
		public enum FailureReason
		{
			// Token: 0x04000133 RID: 307
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000134 RID: 308
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000135 RID: 309
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000136 RID: 310
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x04000137 RID: 311
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000138 RID: 312
			FAILURE_REASON_ABORTED,
			// Token: 0x04000139 RID: 313
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
