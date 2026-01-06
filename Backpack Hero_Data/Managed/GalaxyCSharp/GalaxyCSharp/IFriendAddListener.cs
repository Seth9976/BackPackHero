using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000E5 RID: 229
	public abstract class IFriendAddListener : GalaxyTypeAwareListenerFriendAdd
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		internal IFriendAddListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendAddListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendAddListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0000E714 File Offset: 0x0000C914
		public IFriendAddListener()
			: this(GalaxyInstancePINVOKE.new_IFriendAddListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0000E738 File Offset: 0x0000C938
		internal static HandleRef getCPtr(IFriendAddListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0000E758 File Offset: 0x0000C958
		~IFriendAddListener()
		{
			this.Dispose();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000E788 File Offset: 0x0000C988
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendAddListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendAddListener.listeners.ContainsKey(handle))
					{
						IFriendAddListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009C7 RID: 2503
		public abstract void OnFriendAdded(GalaxyID userID, IFriendAddListener.InvitationDirection invitationDirection);

		// Token: 0x060009C8 RID: 2504 RVA: 0x0000E838 File Offset: 0x0000CA38
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendAdded", IFriendAddListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendAddListener.SwigDelegateIFriendAddListener_0(IFriendAddListener.SwigDirectorOnFriendAdded);
			}
			GalaxyInstancePINVOKE.IFriendAddListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0000E874 File Offset: 0x0000CA74
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendAddListener));
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0000E8AA File Offset: 0x0000CAAA
		[MonoPInvokeCallback(typeof(IFriendAddListener.SwigDelegateIFriendAddListener_0))]
		private static void SwigDirectorOnFriendAdded(IntPtr cPtr, IntPtr userID, int invitationDirection)
		{
			if (IFriendAddListener.listeners.ContainsKey(cPtr))
			{
				IFriendAddListener.listeners[cPtr].OnFriendAdded(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IFriendAddListener.InvitationDirection)invitationDirection);
			}
		}

		// Token: 0x04000178 RID: 376
		private static Dictionary<IntPtr, IFriendAddListener> listeners = new Dictionary<IntPtr, IFriendAddListener>();

		// Token: 0x04000179 RID: 377
		private HandleRef swigCPtr;

		// Token: 0x0400017A RID: 378
		private IFriendAddListener.SwigDelegateIFriendAddListener_0 swigDelegate0;

		// Token: 0x0400017B RID: 379
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IFriendAddListener.InvitationDirection)
		};

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x060009CD RID: 2509
		public delegate void SwigDelegateIFriendAddListener_0(IntPtr cPtr, IntPtr userID, int invitationDirection);

		// Token: 0x020000E7 RID: 231
		public enum InvitationDirection
		{
			// Token: 0x0400017D RID: 381
			INVITATION_DIRECTION_INCOMING,
			// Token: 0x0400017E RID: 382
			INVITATION_DIRECTION_OUTGOING
		}
	}
}
