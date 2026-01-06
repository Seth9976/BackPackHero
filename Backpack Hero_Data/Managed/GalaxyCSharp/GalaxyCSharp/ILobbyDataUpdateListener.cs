using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000125 RID: 293
	public abstract class ILobbyDataUpdateListener : GalaxyTypeAwareListenerLobbyDataUpdate
	{
		// Token: 0x06000B55 RID: 2901 RVA: 0x0000975C File Offset: 0x0000795C
		internal ILobbyDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyDataUpdateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00009784 File Offset: 0x00007984
		public ILobbyDataUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataUpdateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000097A8 File Offset: 0x000079A8
		internal static HandleRef getCPtr(ILobbyDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000097C8 File Offset: 0x000079C8
		~ILobbyDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000097F8 File Offset: 0x000079F8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyDataUpdateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyDataUpdateListener.listeners.ContainsKey(handle))
					{
						ILobbyDataUpdateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B5A RID: 2906
		public abstract void OnLobbyDataUpdateSuccess(GalaxyID lobbyID);

		// Token: 0x06000B5B RID: 2907
		public abstract void OnLobbyDataUpdateFailure(GalaxyID lobbyID, ILobbyDataUpdateListener.FailureReason failureReason);

		// Token: 0x06000B5C RID: 2908 RVA: 0x000098A8 File Offset: 0x00007AA8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyDataUpdateSuccess", ILobbyDataUpdateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_0(ILobbyDataUpdateListener.SwigDirectorOnLobbyDataUpdateSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLobbyDataUpdateFailure", ILobbyDataUpdateListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_1(ILobbyDataUpdateListener.SwigDirectorOnLobbyDataUpdateFailure);
			}
			GalaxyInstancePINVOKE.ILobbyDataUpdateListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0000991C File Offset: 0x00007B1C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataUpdateListener));
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00009952 File Offset: 0x00007B52
		[MonoPInvokeCallback(typeof(ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_0))]
		private static void SwigDirectorOnLobbyDataUpdateSuccess(IntPtr cPtr, IntPtr lobbyID)
		{
			if (ILobbyDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataUpdateListener.listeners[cPtr].OnLobbyDataUpdateSuccess(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()));
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00009985 File Offset: 0x00007B85
		[MonoPInvokeCallback(typeof(ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_1))]
		private static void SwigDirectorOnLobbyDataUpdateFailure(IntPtr cPtr, IntPtr lobbyID, int failureReason)
		{
			if (ILobbyDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataUpdateListener.listeners[cPtr].OnLobbyDataUpdateFailure(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (ILobbyDataUpdateListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400020E RID: 526
		private static Dictionary<IntPtr, ILobbyDataUpdateListener> listeners = new Dictionary<IntPtr, ILobbyDataUpdateListener>();

		// Token: 0x0400020F RID: 527
		private HandleRef swigCPtr;

		// Token: 0x04000210 RID: 528
		private ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_0 swigDelegate0;

		// Token: 0x04000211 RID: 529
		private ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_1 swigDelegate1;

		// Token: 0x04000212 RID: 530
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x04000213 RID: 531
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ILobbyDataUpdateListener.FailureReason)
		};

		// Token: 0x02000126 RID: 294
		// (Invoke) Token: 0x06000B62 RID: 2914
		public delegate void SwigDelegateILobbyDataUpdateListener_0(IntPtr cPtr, IntPtr lobbyID);

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000B66 RID: 2918
		public delegate void SwigDelegateILobbyDataUpdateListener_1(IntPtr cPtr, IntPtr lobbyID, int failureReason);

		// Token: 0x02000128 RID: 296
		public enum FailureReason
		{
			// Token: 0x04000215 RID: 533
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000216 RID: 534
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			// Token: 0x04000217 RID: 535
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
