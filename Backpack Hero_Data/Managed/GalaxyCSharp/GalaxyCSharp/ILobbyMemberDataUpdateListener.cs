using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000130 RID: 304
	public abstract class ILobbyMemberDataUpdateListener : GalaxyTypeAwareListenerLobbyMemberDataUpdate
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x0000A1DC File Offset: 0x000083DC
		internal ILobbyMemberDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMemberDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyMemberDataUpdateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0000A204 File Offset: 0x00008404
		public ILobbyMemberDataUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMemberDataUpdateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0000A228 File Offset: 0x00008428
		internal static HandleRef getCPtr(ILobbyMemberDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0000A248 File Offset: 0x00008448
		~ILobbyMemberDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0000A278 File Offset: 0x00008478
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyMemberDataUpdateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyMemberDataUpdateListener.listeners.ContainsKey(handle))
					{
						ILobbyMemberDataUpdateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B98 RID: 2968
		public abstract void OnLobbyMemberDataUpdateSuccess(GalaxyID lobbyID, GalaxyID memberID);

		// Token: 0x06000B99 RID: 2969
		public abstract void OnLobbyMemberDataUpdateFailure(GalaxyID lobbyID, GalaxyID memberID, ILobbyMemberDataUpdateListener.FailureReason failureReason);

		// Token: 0x06000B9A RID: 2970 RVA: 0x0000A328 File Offset: 0x00008528
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyMemberDataUpdateSuccess", ILobbyMemberDataUpdateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_0(ILobbyMemberDataUpdateListener.SwigDirectorOnLobbyMemberDataUpdateSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLobbyMemberDataUpdateFailure", ILobbyMemberDataUpdateListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_1(ILobbyMemberDataUpdateListener.SwigDirectorOnLobbyMemberDataUpdateFailure);
			}
			GalaxyInstancePINVOKE.ILobbyMemberDataUpdateListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0000A39C File Offset: 0x0000859C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMemberDataUpdateListener));
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0000A3D4 File Offset: 0x000085D4
		[MonoPInvokeCallback(typeof(ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_0))]
		private static void SwigDirectorOnLobbyMemberDataUpdateSuccess(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID)
		{
			if (ILobbyMemberDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMemberDataUpdateListener.listeners[cPtr].OnLobbyMemberDataUpdateSuccess(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()));
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0000A424 File Offset: 0x00008624
		[MonoPInvokeCallback(typeof(ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_1))]
		private static void SwigDirectorOnLobbyMemberDataUpdateFailure(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int failureReason)
		{
			if (ILobbyMemberDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMemberDataUpdateListener.listeners[cPtr].OnLobbyMemberDataUpdateFailure(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()), (ILobbyMemberDataUpdateListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000229 RID: 553
		private static Dictionary<IntPtr, ILobbyMemberDataUpdateListener> listeners = new Dictionary<IntPtr, ILobbyMemberDataUpdateListener>();

		// Token: 0x0400022A RID: 554
		private HandleRef swigCPtr;

		// Token: 0x0400022B RID: 555
		private ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_0 swigDelegate0;

		// Token: 0x0400022C RID: 556
		private ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_1 swigDelegate1;

		// Token: 0x0400022D RID: 557
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};

		// Token: 0x0400022E RID: 558
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(ILobbyMemberDataUpdateListener.FailureReason)
		};

		// Token: 0x02000131 RID: 305
		// (Invoke) Token: 0x06000BA0 RID: 2976
		public delegate void SwigDelegateILobbyMemberDataUpdateListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID);

		// Token: 0x02000132 RID: 306
		// (Invoke) Token: 0x06000BA4 RID: 2980
		public delegate void SwigDelegateILobbyMemberDataUpdateListener_1(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int failureReason);

		// Token: 0x02000133 RID: 307
		public enum FailureReason
		{
			// Token: 0x04000230 RID: 560
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000231 RID: 561
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			// Token: 0x04000232 RID: 562
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
