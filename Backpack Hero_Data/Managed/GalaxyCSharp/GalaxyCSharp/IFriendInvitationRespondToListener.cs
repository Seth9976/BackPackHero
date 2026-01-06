using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000F2 RID: 242
	public abstract class IFriendInvitationRespondToListener : GalaxyTypeAwareListenerFriendInvitationRespondTo
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0000F584 File Offset: 0x0000D784
		internal IFriendInvitationRespondToListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationRespondToListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationRespondToListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public IFriendInvitationRespondToListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationRespondToListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		internal static HandleRef getCPtr(IFriendInvitationRespondToListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		~IFriendInvitationRespondToListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000F620 File Offset: 0x0000D820
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationRespondToListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationRespondToListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationRespondToListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A0B RID: 2571
		public abstract void OnFriendInvitationRespondToSuccess(GalaxyID userID, bool accept);

		// Token: 0x06000A0C RID: 2572
		public abstract void OnFriendInvitationRespondToFailure(GalaxyID userID, IFriendInvitationRespondToListener.FailureReason failureReason);

		// Token: 0x06000A0D RID: 2573 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationRespondToSuccess", IFriendInvitationRespondToListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_0(IFriendInvitationRespondToListener.SwigDirectorOnFriendInvitationRespondToSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationRespondToFailure", IFriendInvitationRespondToListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_1(IFriendInvitationRespondToListener.SwigDirectorOnFriendInvitationRespondToFailure);
			}
			GalaxyInstancePINVOKE.IFriendInvitationRespondToListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000F744 File Offset: 0x0000D944
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationRespondToListener));
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000F77A File Offset: 0x0000D97A
		[MonoPInvokeCallback(typeof(IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_0))]
		private static void SwigDirectorOnFriendInvitationRespondToSuccess(IntPtr cPtr, IntPtr userID, bool accept)
		{
			if (IFriendInvitationRespondToListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationRespondToListener.listeners[cPtr].OnFriendInvitationRespondToSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()), accept);
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000F7AE File Offset: 0x0000D9AE
		[MonoPInvokeCallback(typeof(IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_1))]
		private static void SwigDirectorOnFriendInvitationRespondToFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IFriendInvitationRespondToListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationRespondToListener.listeners[cPtr].OnFriendInvitationRespondToFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendInvitationRespondToListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000195 RID: 405
		private static Dictionary<IntPtr, IFriendInvitationRespondToListener> listeners = new Dictionary<IntPtr, IFriendInvitationRespondToListener>();

		// Token: 0x04000196 RID: 406
		private HandleRef swigCPtr;

		// Token: 0x04000197 RID: 407
		private IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_0 swigDelegate0;

		// Token: 0x04000198 RID: 408
		private IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_1 swigDelegate1;

		// Token: 0x04000199 RID: 409
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(bool)
		};

		// Token: 0x0400019A RID: 410
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendInvitationRespondToListener.FailureReason)
		};

		// Token: 0x020000F3 RID: 243
		// (Invoke) Token: 0x06000A13 RID: 2579
		public delegate void SwigDelegateIFriendInvitationRespondToListener_0(IntPtr cPtr, IntPtr userID, bool accept);

		// Token: 0x020000F4 RID: 244
		// (Invoke) Token: 0x06000A17 RID: 2583
		public delegate void SwigDelegateIFriendInvitationRespondToListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000F5 RID: 245
		public enum FailureReason
		{
			// Token: 0x0400019C RID: 412
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400019D RID: 413
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			// Token: 0x0400019E RID: 414
			FAILURE_REASON_FRIEND_INVITATION_DOES_NOT_EXIST,
			// Token: 0x0400019F RID: 415
			FAILURE_REASON_USER_ALREADY_FRIEND,
			// Token: 0x040001A0 RID: 416
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
