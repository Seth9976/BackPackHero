using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000C1 RID: 193
	public abstract class ICloudStorageDeleteFileListener : GalaxyTypeAwareListenerCloudStorageDeleteFile
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0001759F File Offset: 0x0001579F
		internal ICloudStorageDeleteFileListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStorageDeleteFileListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStorageDeleteFileListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000175C7 File Offset: 0x000157C7
		public ICloudStorageDeleteFileListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStorageDeleteFileListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000175EB File Offset: 0x000157EB
		internal static HandleRef getCPtr(ICloudStorageDeleteFileListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001760C File Offset: 0x0001580C
		~ICloudStorageDeleteFileListener()
		{
			this.Dispose();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001763C File Offset: 0x0001583C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorageDeleteFileListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStorageDeleteFileListener.listeners.ContainsKey(handle))
					{
						ICloudStorageDeleteFileListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600090A RID: 2314
		public abstract void OnDeleteFileSuccess(string container, string name);

		// Token: 0x0600090B RID: 2315
		public abstract void OnDeleteFileFailure(string container, string name, ICloudStorageDeleteFileListener.FailureReason failureReason);

		// Token: 0x0600090C RID: 2316 RVA: 0x000176EC File Offset: 0x000158EC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnDeleteFileSuccess", ICloudStorageDeleteFileListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_0(ICloudStorageDeleteFileListener.SwigDirectorOnDeleteFileSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnDeleteFileFailure", ICloudStorageDeleteFileListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_1(ICloudStorageDeleteFileListener.SwigDirectorOnDeleteFileFailure);
			}
			GalaxyInstancePINVOKE.ICloudStorageDeleteFileListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00017760 File Offset: 0x00015960
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStorageDeleteFileListener));
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00017796 File Offset: 0x00015996
		[MonoPInvokeCallback(typeof(ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_0))]
		private static void SwigDirectorOnDeleteFileSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (ICloudStorageDeleteFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageDeleteFileListener.listeners[cPtr].OnDeleteFileSuccess(container, name);
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000177BA File Offset: 0x000159BA
		[MonoPInvokeCallback(typeof(ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_1))]
		private static void SwigDirectorOnDeleteFileFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ICloudStorageDeleteFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageDeleteFileListener.listeners[cPtr].OnDeleteFileFailure(container, name, (ICloudStorageDeleteFileListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400010D RID: 269
		private static Dictionary<IntPtr, ICloudStorageDeleteFileListener> listeners = new Dictionary<IntPtr, ICloudStorageDeleteFileListener>();

		// Token: 0x0400010E RID: 270
		private HandleRef swigCPtr;

		// Token: 0x0400010F RID: 271
		private ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_0 swigDelegate0;

		// Token: 0x04000110 RID: 272
		private ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_1 swigDelegate1;

		// Token: 0x04000111 RID: 273
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(string)
		};

		// Token: 0x04000112 RID: 274
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(ICloudStorageDeleteFileListener.FailureReason)
		};

		// Token: 0x020000C2 RID: 194
		// (Invoke) Token: 0x06000912 RID: 2322
		public delegate void SwigDelegateICloudStorageDeleteFileListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);

		// Token: 0x020000C3 RID: 195
		// (Invoke) Token: 0x06000916 RID: 2326
		public delegate void SwigDelegateICloudStorageDeleteFileListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x020000C4 RID: 196
		public enum FailureReason
		{
			// Token: 0x04000114 RID: 276
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000115 RID: 277
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000116 RID: 278
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000117 RID: 279
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x04000118 RID: 280
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000119 RID: 281
			FAILURE_REASON_ABORTED,
			// Token: 0x0400011A RID: 282
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x0400011B RID: 283
			FAILURE_REASON_CONFLICT
		}
	}
}
