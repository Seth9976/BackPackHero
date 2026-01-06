using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000EE RID: 238
	public abstract class IFriendInvitationListRetrieveListener : GalaxyTypeAwareListenerFriendInvitationListRetrieve
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		internal IFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public IFriendInvitationListRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationListRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		internal static HandleRef getCPtr(IFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0000F218 File Offset: 0x0000D418
		~IFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000F248 File Offset: 0x0000D448
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009F7 RID: 2551
		public abstract void OnFriendInvitationListRetrieveSuccess();

		// Token: 0x060009F8 RID: 2552
		public abstract void OnFriendInvitationListRetrieveFailure(IFriendInvitationListRetrieveListener.FailureReason failureReason);

		// Token: 0x060009F9 RID: 2553 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationListRetrieveSuccess", IFriendInvitationListRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_0(IFriendInvitationListRetrieveListener.SwigDirectorOnFriendInvitationListRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationListRetrieveFailure", IFriendInvitationListRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_1(IFriendInvitationListRetrieveListener.SwigDirectorOnFriendInvitationListRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IFriendInvitationListRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000F36C File Offset: 0x0000D56C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationListRetrieveListener));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000F3A2 File Offset: 0x0000D5A2
		[MonoPInvokeCallback(typeof(IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_0))]
		private static void SwigDirectorOnFriendInvitationListRetrieveSuccess(IntPtr cPtr)
		{
			if (IFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationListRetrieveListener.listeners[cPtr].OnFriendInvitationListRetrieveSuccess();
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		[MonoPInvokeCallback(typeof(IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_1))]
		private static void SwigDirectorOnFriendInvitationListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (IFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationListRetrieveListener.listeners[cPtr].OnFriendInvitationListRetrieveFailure((IFriendInvitationListRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400018C RID: 396
		private static Dictionary<IntPtr, IFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, IFriendInvitationListRetrieveListener>();

		// Token: 0x0400018D RID: 397
		private HandleRef swigCPtr;

		// Token: 0x0400018E RID: 398
		private IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_0 swigDelegate0;

		// Token: 0x0400018F RID: 399
		private IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_1 swigDelegate1;

		// Token: 0x04000190 RID: 400
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x04000191 RID: 401
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IFriendInvitationListRetrieveListener.FailureReason) };

		// Token: 0x020000EF RID: 239
		// (Invoke) Token: 0x060009FF RID: 2559
		public delegate void SwigDelegateIFriendInvitationListRetrieveListener_0(IntPtr cPtr);

		// Token: 0x020000F0 RID: 240
		// (Invoke) Token: 0x06000A03 RID: 2563
		public delegate void SwigDelegateIFriendInvitationListRetrieveListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000F1 RID: 241
		public enum FailureReason
		{
			// Token: 0x04000193 RID: 403
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000194 RID: 404
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
