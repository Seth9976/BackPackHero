using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000100 RID: 256
	public abstract class IGameInvitationReceivedListener : GalaxyTypeAwareListenerGameInvitationReceived
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x0001016C File Offset: 0x0000E36C
		internal IGameInvitationReceivedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGameInvitationReceivedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IGameInvitationReceivedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00010194 File Offset: 0x0000E394
		public IGameInvitationReceivedListener()
			: this(GalaxyInstancePINVOKE.new_IGameInvitationReceivedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000101B8 File Offset: 0x0000E3B8
		internal static HandleRef getCPtr(IGameInvitationReceivedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000101D8 File Offset: 0x0000E3D8
		~IGameInvitationReceivedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00010208 File Offset: 0x0000E408
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IGameInvitationReceivedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IGameInvitationReceivedListener.listeners.ContainsKey(handle))
					{
						IGameInvitationReceivedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A8E RID: 2702
		public abstract void OnGameInvitationReceived(GalaxyID userID, string connectionString);

		// Token: 0x06000A8F RID: 2703 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGameInvitationReceived", IGameInvitationReceivedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IGameInvitationReceivedListener.SwigDelegateIGameInvitationReceivedListener_0(IGameInvitationReceivedListener.SwigDirectorOnGameInvitationReceived);
			}
			GalaxyInstancePINVOKE.IGameInvitationReceivedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000102F4 File Offset: 0x0000E4F4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGameInvitationReceivedListener));
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001032A File Offset: 0x0000E52A
		[MonoPInvokeCallback(typeof(IGameInvitationReceivedListener.SwigDelegateIGameInvitationReceivedListener_0))]
		private static void SwigDirectorOnGameInvitationReceived(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString)
		{
			if (IGameInvitationReceivedListener.listeners.ContainsKey(cPtr))
			{
				IGameInvitationReceivedListener.listeners[cPtr].OnGameInvitationReceived(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString);
			}
		}

		// Token: 0x040001BB RID: 443
		private static Dictionary<IntPtr, IGameInvitationReceivedListener> listeners = new Dictionary<IntPtr, IGameInvitationReceivedListener>();

		// Token: 0x040001BC RID: 444
		private HandleRef swigCPtr;

		// Token: 0x040001BD RID: 445
		private IGameInvitationReceivedListener.SwigDelegateIGameInvitationReceivedListener_0 swigDelegate0;

		// Token: 0x040001BE RID: 446
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string)
		};

		// Token: 0x02000101 RID: 257
		// (Invoke) Token: 0x06000A94 RID: 2708
		public delegate void SwigDelegateIGameInvitationReceivedListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString);
	}
}
