using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000C5 RID: 197
	public abstract class ICloudStorageGetFileListener : GalaxyTypeAwareListenerCloudStorageGetFile
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0001784E File Offset: 0x00015A4E
		internal ICloudStorageGetFileListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStorageGetFileListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStorageGetFileListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00017876 File Offset: 0x00015A76
		public ICloudStorageGetFileListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStorageGetFileListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001789A File Offset: 0x00015A9A
		internal static HandleRef getCPtr(ICloudStorageGetFileListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000178B8 File Offset: 0x00015AB8
		~ICloudStorageGetFileListener()
		{
			this.Dispose();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000178E8 File Offset: 0x00015AE8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorageGetFileListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStorageGetFileListener.listeners.ContainsKey(handle))
					{
						ICloudStorageGetFileListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00017998 File Offset: 0x00015B98
		public virtual void OnGetFileSuccess(string container, string name, uint fileSize, SavegameType savegameType, string savegameID)
		{
			GalaxyInstancePINVOKE.ICloudStorageGetFileListener_OnGetFileSuccess(this.swigCPtr, container, name, fileSize, (int)savegameType, savegameID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000179BC File Offset: 0x00015BBC
		public virtual void OnGetFileFailure(string container, string name, ICloudStorageGetFileListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.ICloudStorageGetFileListener_OnGetFileFailure(this.swigCPtr, container, name, (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000179DC File Offset: 0x00015BDC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGetFileSuccess", ICloudStorageGetFileListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_0(ICloudStorageGetFileListener.SwigDirectorOnGetFileSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnGetFileFailure", ICloudStorageGetFileListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_1(ICloudStorageGetFileListener.SwigDirectorOnGetFileFailure);
			}
			GalaxyInstancePINVOKE.ICloudStorageGetFileListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00017A50 File Offset: 0x00015C50
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStorageGetFileListener));
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00017A86 File Offset: 0x00015C86
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_0))]
		private static void SwigDirectorOnGetFileSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint fileSize, int savegameType, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string savegameID)
		{
			if (ICloudStorageGetFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListener.listeners[cPtr].OnGetFileSuccess(container, name, fileSize, (SavegameType)savegameType, savegameID);
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00017AAF File Offset: 0x00015CAF
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_1))]
		private static void SwigDirectorOnGetFileFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ICloudStorageGetFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListener.listeners[cPtr].OnGetFileFailure(container, name, (ICloudStorageGetFileListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400011C RID: 284
		private static Dictionary<IntPtr, ICloudStorageGetFileListener> listeners = new Dictionary<IntPtr, ICloudStorageGetFileListener>();

		// Token: 0x0400011D RID: 285
		private HandleRef swigCPtr;

		// Token: 0x0400011E RID: 286
		private ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_0 swigDelegate0;

		// Token: 0x0400011F RID: 287
		private ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_1 swigDelegate1;

		// Token: 0x04000120 RID: 288
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(uint),
			typeof(SavegameType),
			typeof(string)
		};

		// Token: 0x04000121 RID: 289
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(ICloudStorageGetFileListener.FailureReason)
		};

		// Token: 0x020000C6 RID: 198
		// (Invoke) Token: 0x06000926 RID: 2342
		public delegate void SwigDelegateICloudStorageGetFileListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint fileSize, int savegameType, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string savegameID);

		// Token: 0x020000C7 RID: 199
		// (Invoke) Token: 0x0600092A RID: 2346
		public delegate void SwigDelegateICloudStorageGetFileListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x020000C8 RID: 200
		public enum FailureReason
		{
			// Token: 0x04000123 RID: 291
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000124 RID: 292
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000125 RID: 293
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000126 RID: 294
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x04000127 RID: 295
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000128 RID: 296
			FAILURE_REASON_ABORTED,
			// Token: 0x04000129 RID: 297
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x0400012A RID: 298
			FAILURE_REASON_BUFFER_TOO_SMALL,
			// Token: 0x0400012B RID: 299
			FAILURE_REASON_WRITE_FUNC_ERROR
		}
	}
}
