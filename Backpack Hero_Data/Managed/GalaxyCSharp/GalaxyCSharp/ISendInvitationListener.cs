using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000160 RID: 352
	public abstract class ISendInvitationListener : GalaxyTypeAwareListenerSendInvitation
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x00014850 File Offset: 0x00012A50
		internal ISendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00014878 File Offset: 0x00012A78
		public ISendInvitationListener()
			: this(GalaxyInstancePINVOKE.new_ISendInvitationListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0001489C File Offset: 0x00012A9C
		internal static HandleRef getCPtr(ISendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000148BC File Offset: 0x00012ABC
		~ISendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000148EC File Offset: 0x00012AEC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISendInvitationListener.listeners.ContainsKey(handle))
					{
						ISendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CFA RID: 3322
		public abstract void OnInvitationSendSuccess(GalaxyID userID, string connectionString);

		// Token: 0x06000CFB RID: 3323
		public abstract void OnInvitationSendFailure(GalaxyID userID, string connectionString, ISendInvitationListener.FailureReason failureReason);

		// Token: 0x06000CFC RID: 3324 RVA: 0x0001499C File Offset: 0x00012B9C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnInvitationSendSuccess", ISendInvitationListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISendInvitationListener.SwigDelegateISendInvitationListener_0(ISendInvitationListener.SwigDirectorOnInvitationSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnInvitationSendFailure", ISendInvitationListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ISendInvitationListener.SwigDelegateISendInvitationListener_1(ISendInvitationListener.SwigDirectorOnInvitationSendFailure);
			}
			GalaxyInstancePINVOKE.ISendInvitationListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00014A10 File Offset: 0x00012C10
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISendInvitationListener));
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00014A46 File Offset: 0x00012C46
		[MonoPInvokeCallback(typeof(ISendInvitationListener.SwigDelegateISendInvitationListener_0))]
		private static void SwigDirectorOnInvitationSendSuccess(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString)
		{
			if (ISendInvitationListener.listeners.ContainsKey(cPtr))
			{
				ISendInvitationListener.listeners[cPtr].OnInvitationSendSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString);
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00014A7A File Offset: 0x00012C7A
		[MonoPInvokeCallback(typeof(ISendInvitationListener.SwigDelegateISendInvitationListener_1))]
		private static void SwigDirectorOnInvitationSendFailure(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason)
		{
			if (ISendInvitationListener.listeners.ContainsKey(cPtr))
			{
				ISendInvitationListener.listeners[cPtr].OnInvitationSendFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString, (ISendInvitationListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000297 RID: 663
		private static Dictionary<IntPtr, ISendInvitationListener> listeners = new Dictionary<IntPtr, ISendInvitationListener>();

		// Token: 0x04000298 RID: 664
		private HandleRef swigCPtr;

		// Token: 0x04000299 RID: 665
		private ISendInvitationListener.SwigDelegateISendInvitationListener_0 swigDelegate0;

		// Token: 0x0400029A RID: 666
		private ISendInvitationListener.SwigDelegateISendInvitationListener_1 swigDelegate1;

		// Token: 0x0400029B RID: 667
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string)
		};

		// Token: 0x0400029C RID: 668
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string),
			typeof(ISendInvitationListener.FailureReason)
		};

		// Token: 0x02000161 RID: 353
		// (Invoke) Token: 0x06000D02 RID: 3330
		public delegate void SwigDelegateISendInvitationListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString);

		// Token: 0x02000162 RID: 354
		// (Invoke) Token: 0x06000D06 RID: 3334
		public delegate void SwigDelegateISendInvitationListener_1(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason);

		// Token: 0x02000163 RID: 355
		public enum FailureReason
		{
			// Token: 0x0400029E RID: 670
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400029F RID: 671
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			// Token: 0x040002A0 RID: 672
			FAILURE_REASON_RECEIVER_DOES_NOT_ALLOW_INVITING,
			// Token: 0x040002A1 RID: 673
			FAILURE_REASON_SENDER_DOES_NOT_ALLOW_INVITING,
			// Token: 0x040002A2 RID: 674
			FAILURE_REASON_RECEIVER_BLOCKED,
			// Token: 0x040002A3 RID: 675
			FAILURE_REASON_SENDER_BLOCKED,
			// Token: 0x040002A4 RID: 676
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
