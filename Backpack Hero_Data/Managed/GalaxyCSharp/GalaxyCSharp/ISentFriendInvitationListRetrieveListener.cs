using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000164 RID: 356
	public abstract class ISentFriendInvitationListRetrieveListener : GalaxyTypeAwareListenerSentFriendInvitationListRetrieve
	{
		// Token: 0x06000D09 RID: 3337 RVA: 0x00014C8C File Offset: 0x00012E8C
		internal ISentFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISentFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISentFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00014CB4 File Offset: 0x00012EB4
		public ISentFriendInvitationListRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ISentFriendInvitationListRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00014CD8 File Offset: 0x00012ED8
		internal static HandleRef getCPtr(ISentFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00014CF8 File Offset: 0x00012EF8
		~ISentFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00014D28 File Offset: 0x00012F28
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISentFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISentFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						ISentFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000D0E RID: 3342
		public abstract void OnSentFriendInvitationListRetrieveSuccess();

		// Token: 0x06000D0F RID: 3343
		public abstract void OnSentFriendInvitationListRetrieveFailure(ISentFriendInvitationListRetrieveListener.FailureReason failureReason);

		// Token: 0x06000D10 RID: 3344 RVA: 0x00014DD8 File Offset: 0x00012FD8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnSentFriendInvitationListRetrieveSuccess", ISentFriendInvitationListRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_0(ISentFriendInvitationListRetrieveListener.SwigDirectorOnSentFriendInvitationListRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnSentFriendInvitationListRetrieveFailure", ISentFriendInvitationListRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_1(ISentFriendInvitationListRetrieveListener.SwigDirectorOnSentFriendInvitationListRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ISentFriendInvitationListRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00014E4C File Offset: 0x0001304C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISentFriendInvitationListRetrieveListener));
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00014E82 File Offset: 0x00013082
		[MonoPInvokeCallback(typeof(ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_0))]
		private static void SwigDirectorOnSentFriendInvitationListRetrieveSuccess(IntPtr cPtr)
		{
			if (ISentFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ISentFriendInvitationListRetrieveListener.listeners[cPtr].OnSentFriendInvitationListRetrieveSuccess();
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00014EA4 File Offset: 0x000130A4
		[MonoPInvokeCallback(typeof(ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_1))]
		private static void SwigDirectorOnSentFriendInvitationListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (ISentFriendInvitationListRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ISentFriendInvitationListRetrieveListener.listeners[cPtr].OnSentFriendInvitationListRetrieveFailure((ISentFriendInvitationListRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002A5 RID: 677
		private static Dictionary<IntPtr, ISentFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, ISentFriendInvitationListRetrieveListener>();

		// Token: 0x040002A6 RID: 678
		private HandleRef swigCPtr;

		// Token: 0x040002A7 RID: 679
		private ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_0 swigDelegate0;

		// Token: 0x040002A8 RID: 680
		private ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_1 swigDelegate1;

		// Token: 0x040002A9 RID: 681
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040002AA RID: 682
		private static Type[] swigMethodTypes1 = new Type[] { typeof(ISentFriendInvitationListRetrieveListener.FailureReason) };

		// Token: 0x02000165 RID: 357
		// (Invoke) Token: 0x06000D16 RID: 3350
		public delegate void SwigDelegateISentFriendInvitationListRetrieveListener_0(IntPtr cPtr);

		// Token: 0x02000166 RID: 358
		// (Invoke) Token: 0x06000D1A RID: 3354
		public delegate void SwigDelegateISentFriendInvitationListRetrieveListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x02000167 RID: 359
		public enum FailureReason
		{
			// Token: 0x040002AC RID: 684
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002AD RID: 685
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
