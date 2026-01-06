using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000D6 RID: 214
	public abstract class IConnectionOpenListener : GalaxyTypeAwareListenerConnectionOpen
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x0000DE20 File Offset: 0x0000C020
		internal IConnectionOpenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionOpenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IConnectionOpenListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0000DE48 File Offset: 0x0000C048
		public IConnectionOpenListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionOpenListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0000DE6C File Offset: 0x0000C06C
		internal static HandleRef getCPtr(IConnectionOpenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0000DE8C File Offset: 0x0000C08C
		~IConnectionOpenListener()
		{
			this.Dispose();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IConnectionOpenListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IConnectionOpenListener.listeners.ContainsKey(handle))
					{
						IConnectionOpenListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000976 RID: 2422
		public abstract void OnConnectionOpenSuccess(string connectionString, ulong connectionID);

		// Token: 0x06000977 RID: 2423
		public abstract void OnConnectionOpenFailure(string connectionString, IConnectionOpenListener.FailureReason failureReason);

		// Token: 0x06000978 RID: 2424 RVA: 0x0000DF6C File Offset: 0x0000C16C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionOpenSuccess", IConnectionOpenListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IConnectionOpenListener.SwigDelegateIConnectionOpenListener_0(IConnectionOpenListener.SwigDirectorOnConnectionOpenSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnConnectionOpenFailure", IConnectionOpenListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IConnectionOpenListener.SwigDelegateIConnectionOpenListener_1(IConnectionOpenListener.SwigDirectorOnConnectionOpenFailure);
			}
			GalaxyInstancePINVOKE.IConnectionOpenListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionOpenListener));
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0000E016 File Offset: 0x0000C216
		[MonoPInvokeCallback(typeof(IConnectionOpenListener.SwigDelegateIConnectionOpenListener_0))]
		private static void SwigDirectorOnConnectionOpenSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, ulong connectionID)
		{
			if (IConnectionOpenListener.listeners.ContainsKey(cPtr))
			{
				IConnectionOpenListener.listeners[cPtr].OnConnectionOpenSuccess(connectionString, connectionID);
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000E03A File Offset: 0x0000C23A
		[MonoPInvokeCallback(typeof(IConnectionOpenListener.SwigDelegateIConnectionOpenListener_1))]
		private static void SwigDirectorOnConnectionOpenFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason)
		{
			if (IConnectionOpenListener.listeners.ContainsKey(cPtr))
			{
				IConnectionOpenListener.listeners[cPtr].OnConnectionOpenFailure(connectionString, (IConnectionOpenListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000153 RID: 339
		private static Dictionary<IntPtr, IConnectionOpenListener> listeners = new Dictionary<IntPtr, IConnectionOpenListener>();

		// Token: 0x04000154 RID: 340
		private HandleRef swigCPtr;

		// Token: 0x04000155 RID: 341
		private IConnectionOpenListener.SwigDelegateIConnectionOpenListener_0 swigDelegate0;

		// Token: 0x04000156 RID: 342
		private IConnectionOpenListener.SwigDelegateIConnectionOpenListener_1 swigDelegate1;

		// Token: 0x04000157 RID: 343
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(ulong)
		};

		// Token: 0x04000158 RID: 344
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(IConnectionOpenListener.FailureReason)
		};

		// Token: 0x020000D7 RID: 215
		// (Invoke) Token: 0x0600097E RID: 2430
		public delegate void SwigDelegateIConnectionOpenListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, ulong connectionID);

		// Token: 0x020000D8 RID: 216
		// (Invoke) Token: 0x06000982 RID: 2434
		public delegate void SwigDelegateIConnectionOpenListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason);

		// Token: 0x020000D9 RID: 217
		public enum FailureReason
		{
			// Token: 0x0400015A RID: 346
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400015B RID: 347
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x0400015C RID: 348
			FAILURE_REASON_UNAUTHORIZED
		}
	}
}
