using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000E8 RID: 232
	public abstract class IFriendDeleteListener : GalaxyTypeAwareListenerFriendDelete
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		internal IFriendDeleteListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendDeleteListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendDeleteListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000EA64 File Offset: 0x0000CC64
		public IFriendDeleteListener()
			: this(GalaxyInstancePINVOKE.new_IFriendDeleteListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0000EA88 File Offset: 0x0000CC88
		internal static HandleRef getCPtr(IFriendDeleteListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		~IFriendDeleteListener()
		{
			this.Dispose();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendDeleteListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendDeleteListener.listeners.ContainsKey(handle))
					{
						IFriendDeleteListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009D5 RID: 2517
		public abstract void OnFriendDeleteSuccess(GalaxyID userID);

		// Token: 0x060009D6 RID: 2518
		public abstract void OnFriendDeleteFailure(GalaxyID userID, IFriendDeleteListener.FailureReason failureReason);

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000EB88 File Offset: 0x0000CD88
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendDeleteSuccess", IFriendDeleteListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendDeleteListener.SwigDelegateIFriendDeleteListener_0(IFriendDeleteListener.SwigDirectorOnFriendDeleteSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendDeleteFailure", IFriendDeleteListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendDeleteListener.SwigDelegateIFriendDeleteListener_1(IFriendDeleteListener.SwigDirectorOnFriendDeleteFailure);
			}
			GalaxyInstancePINVOKE.IFriendDeleteListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendDeleteListener));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000EC32 File Offset: 0x0000CE32
		[MonoPInvokeCallback(typeof(IFriendDeleteListener.SwigDelegateIFriendDeleteListener_0))]
		private static void SwigDirectorOnFriendDeleteSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IFriendDeleteListener.listeners.ContainsKey(cPtr))
			{
				IFriendDeleteListener.listeners[cPtr].OnFriendDeleteSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000EC65 File Offset: 0x0000CE65
		[MonoPInvokeCallback(typeof(IFriendDeleteListener.SwigDelegateIFriendDeleteListener_1))]
		private static void SwigDirectorOnFriendDeleteFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IFriendDeleteListener.listeners.ContainsKey(cPtr))
			{
				IFriendDeleteListener.listeners[cPtr].OnFriendDeleteFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendDeleteListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400017F RID: 383
		private static Dictionary<IntPtr, IFriendDeleteListener> listeners = new Dictionary<IntPtr, IFriendDeleteListener>();

		// Token: 0x04000180 RID: 384
		private HandleRef swigCPtr;

		// Token: 0x04000181 RID: 385
		private IFriendDeleteListener.SwigDelegateIFriendDeleteListener_0 swigDelegate0;

		// Token: 0x04000182 RID: 386
		private IFriendDeleteListener.SwigDelegateIFriendDeleteListener_1 swigDelegate1;

		// Token: 0x04000183 RID: 387
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x04000184 RID: 388
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendDeleteListener.FailureReason)
		};

		// Token: 0x020000E9 RID: 233
		// (Invoke) Token: 0x060009DD RID: 2525
		public delegate void SwigDelegateIFriendDeleteListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x020000EA RID: 234
		// (Invoke) Token: 0x060009E1 RID: 2529
		public delegate void SwigDelegateIFriendDeleteListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000EB RID: 235
		public enum FailureReason
		{
			// Token: 0x04000186 RID: 390
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000187 RID: 391
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
