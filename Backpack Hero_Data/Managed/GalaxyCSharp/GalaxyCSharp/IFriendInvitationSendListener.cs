using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000F6 RID: 246
	public abstract class IFriendInvitationSendListener : GalaxyTypeAwareListenerFriendInvitationSend
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		internal IFriendInvitationSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		public IFriendInvitationSendListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationSendListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000FA00 File Offset: 0x0000DC00
		internal static HandleRef getCPtr(IFriendInvitationSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000FA20 File Offset: 0x0000DC20
		~IFriendInvitationSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000FA50 File Offset: 0x0000DC50
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationSendListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A1F RID: 2591
		public abstract void OnFriendInvitationSendSuccess(GalaxyID userID);

		// Token: 0x06000A20 RID: 2592
		public abstract void OnFriendInvitationSendFailure(GalaxyID userID, IFriendInvitationSendListener.FailureReason failureReason);

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000FB00 File Offset: 0x0000DD00
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationSendSuccess", IFriendInvitationSendListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_0(IFriendInvitationSendListener.SwigDirectorOnFriendInvitationSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationSendFailure", IFriendInvitationSendListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_1(IFriendInvitationSendListener.SwigDirectorOnFriendInvitationSendFailure);
			}
			GalaxyInstancePINVOKE.IFriendInvitationSendListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000FB74 File Offset: 0x0000DD74
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationSendListener));
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0000FBAA File Offset: 0x0000DDAA
		[MonoPInvokeCallback(typeof(IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_0))]
		private static void SwigDirectorOnFriendInvitationSendSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IFriendInvitationSendListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationSendListener.listeners[cPtr].OnFriendInvitationSendSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000FBDD File Offset: 0x0000DDDD
		[MonoPInvokeCallback(typeof(IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_1))]
		private static void SwigDirectorOnFriendInvitationSendFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IFriendInvitationSendListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationSendListener.listeners[cPtr].OnFriendInvitationSendFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendInvitationSendListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001A1 RID: 417
		private static Dictionary<IntPtr, IFriendInvitationSendListener> listeners = new Dictionary<IntPtr, IFriendInvitationSendListener>();

		// Token: 0x040001A2 RID: 418
		private HandleRef swigCPtr;

		// Token: 0x040001A3 RID: 419
		private IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_0 swigDelegate0;

		// Token: 0x040001A4 RID: 420
		private IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_1 swigDelegate1;

		// Token: 0x040001A5 RID: 421
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040001A6 RID: 422
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendInvitationSendListener.FailureReason)
		};

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x06000A27 RID: 2599
		public delegate void SwigDelegateIFriendInvitationSendListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x06000A2B RID: 2603
		public delegate void SwigDelegateIFriendInvitationSendListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000F9 RID: 249
		public enum FailureReason
		{
			// Token: 0x040001A8 RID: 424
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001A9 RID: 425
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			// Token: 0x040001AA RID: 426
			FAILURE_REASON_USER_ALREADY_INVITED,
			// Token: 0x040001AB RID: 427
			FAILURE_REASON_USER_ALREADY_FRIEND,
			// Token: 0x040001AC RID: 428
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
