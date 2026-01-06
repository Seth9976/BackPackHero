using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000FA RID: 250
	public abstract class IFriendListListener : GalaxyTypeAwareListenerFriendList
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
		internal IFriendListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendListListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000FDFC File Offset: 0x0000DFFC
		public IFriendListListener()
			: this(GalaxyInstancePINVOKE.new_IFriendListListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000FE20 File Offset: 0x0000E020
		internal static HandleRef getCPtr(IFriendListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000FE40 File Offset: 0x0000E040
		~IFriendListListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000FE70 File Offset: 0x0000E070
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendListListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendListListener.listeners.ContainsKey(handle))
					{
						IFriendListListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A33 RID: 2611
		public abstract void OnFriendListRetrieveSuccess();

		// Token: 0x06000A34 RID: 2612
		public abstract void OnFriendListRetrieveFailure(IFriendListListener.FailureReason failureReason);

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000FF20 File Offset: 0x0000E120
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendListRetrieveSuccess", IFriendListListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendListListener.SwigDelegateIFriendListListener_0(IFriendListListener.SwigDirectorOnFriendListRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendListRetrieveFailure", IFriendListListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendListListener.SwigDelegateIFriendListListener_1(IFriendListListener.SwigDirectorOnFriendListRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IFriendListListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000FF94 File Offset: 0x0000E194
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendListListener));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0000FFCA File Offset: 0x0000E1CA
		[MonoPInvokeCallback(typeof(IFriendListListener.SwigDelegateIFriendListListener_0))]
		private static void SwigDirectorOnFriendListRetrieveSuccess(IntPtr cPtr)
		{
			if (IFriendListListener.listeners.ContainsKey(cPtr))
			{
				IFriendListListener.listeners[cPtr].OnFriendListRetrieveSuccess();
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		[MonoPInvokeCallback(typeof(IFriendListListener.SwigDelegateIFriendListListener_1))]
		private static void SwigDirectorOnFriendListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (IFriendListListener.listeners.ContainsKey(cPtr))
			{
				IFriendListListener.listeners[cPtr].OnFriendListRetrieveFailure((IFriendListListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001AD RID: 429
		private static Dictionary<IntPtr, IFriendListListener> listeners = new Dictionary<IntPtr, IFriendListListener>();

		// Token: 0x040001AE RID: 430
		private HandleRef swigCPtr;

		// Token: 0x040001AF RID: 431
		private IFriendListListener.SwigDelegateIFriendListListener_0 swigDelegate0;

		// Token: 0x040001B0 RID: 432
		private IFriendListListener.SwigDelegateIFriendListListener_1 swigDelegate1;

		// Token: 0x040001B1 RID: 433
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040001B2 RID: 434
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IFriendListListener.FailureReason) };

		// Token: 0x020000FB RID: 251
		// (Invoke) Token: 0x06000A3B RID: 2619
		public delegate void SwigDelegateIFriendListListener_0(IntPtr cPtr);

		// Token: 0x020000FC RID: 252
		// (Invoke) Token: 0x06000A3F RID: 2623
		public delegate void SwigDelegateIFriendListListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000FD RID: 253
		public enum FailureReason
		{
			// Token: 0x040001B4 RID: 436
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001B5 RID: 437
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
