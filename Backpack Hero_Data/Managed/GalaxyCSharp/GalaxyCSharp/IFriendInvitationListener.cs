using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000EC RID: 236
	public abstract class IFriendInvitationListener : GalaxyTypeAwareListenerFriendInvitation
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x0000EE1C File Offset: 0x0000D01C
		internal IFriendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFriendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000EE44 File Offset: 0x0000D044
		public IFriendInvitationListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000EE68 File Offset: 0x0000D068
		internal static HandleRef getCPtr(IFriendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0000EE88 File Offset: 0x0000D088
		~IFriendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFriendInvitationListener.listeners.ContainsKey(handle))
					{
						IFriendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060009E9 RID: 2537
		public abstract void OnFriendInvitationReceived(GalaxyID userID, uint sendTime);

		// Token: 0x060009EA RID: 2538 RVA: 0x0000EF68 File Offset: 0x0000D168
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFriendInvitationReceived", IFriendInvitationListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFriendInvitationListener.SwigDelegateIFriendInvitationListener_0(IFriendInvitationListener.SwigDirectorOnFriendInvitationReceived);
			}
			GalaxyInstancePINVOKE.IFriendInvitationListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationListener));
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000EFDA File Offset: 0x0000D1DA
		[MonoPInvokeCallback(typeof(IFriendInvitationListener.SwigDelegateIFriendInvitationListener_0))]
		private static void SwigDirectorOnFriendInvitationReceived(IntPtr cPtr, IntPtr userID, uint sendTime)
		{
			if (IFriendInvitationListener.listeners.ContainsKey(cPtr))
			{
				IFriendInvitationListener.listeners[cPtr].OnFriendInvitationReceived(new GalaxyID(new GalaxyID(userID, false).ToUint64()), sendTime);
			}
		}

		// Token: 0x04000188 RID: 392
		private static Dictionary<IntPtr, IFriendInvitationListener> listeners = new Dictionary<IntPtr, IFriendInvitationListener>();

		// Token: 0x04000189 RID: 393
		private HandleRef swigCPtr;

		// Token: 0x0400018A RID: 394
		private IFriendInvitationListener.SwigDelegateIFriendInvitationListener_0 swigDelegate0;

		// Token: 0x0400018B RID: 395
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(uint)
		};

		// Token: 0x020000ED RID: 237
		// (Invoke) Token: 0x060009EF RID: 2543
		public delegate void SwigDelegateIFriendInvitationListener_0(IntPtr cPtr, IntPtr userID, uint sendTime);
	}
}
