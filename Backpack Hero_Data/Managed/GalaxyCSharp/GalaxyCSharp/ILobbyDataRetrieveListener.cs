using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000121 RID: 289
	public abstract class ILobbyDataRetrieveListener : GalaxyTypeAwareListenerLobbyDataRetrieve
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x0000937C File Offset: 0x0000757C
		internal ILobbyDataRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyDataRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000093A4 File Offset: 0x000075A4
		public ILobbyDataRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000093C8 File Offset: 0x000075C8
		internal static HandleRef getCPtr(ILobbyDataRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000093E8 File Offset: 0x000075E8
		~ILobbyDataRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00009418 File Offset: 0x00007618
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyDataRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyDataRetrieveListener.listeners.ContainsKey(handle))
					{
						ILobbyDataRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B46 RID: 2886
		public abstract void OnLobbyDataRetrieveSuccess(GalaxyID lobbyID);

		// Token: 0x06000B47 RID: 2887
		public abstract void OnLobbyDataRetrieveFailure(GalaxyID lobbyID, ILobbyDataRetrieveListener.FailureReason failureReason);

		// Token: 0x06000B48 RID: 2888 RVA: 0x000094C8 File Offset: 0x000076C8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyDataRetrieveSuccess", ILobbyDataRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_0(ILobbyDataRetrieveListener.SwigDirectorOnLobbyDataRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLobbyDataRetrieveFailure", ILobbyDataRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_1(ILobbyDataRetrieveListener.SwigDirectorOnLobbyDataRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILobbyDataRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0000953C File Offset: 0x0000773C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataRetrieveListener));
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00009572 File Offset: 0x00007772
		[MonoPInvokeCallback(typeof(ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_0))]
		private static void SwigDirectorOnLobbyDataRetrieveSuccess(IntPtr cPtr, IntPtr lobbyID)
		{
			if (ILobbyDataRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataRetrieveListener.listeners[cPtr].OnLobbyDataRetrieveSuccess(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()));
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000095A5 File Offset: 0x000077A5
		[MonoPInvokeCallback(typeof(ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_1))]
		private static void SwigDirectorOnLobbyDataRetrieveFailure(IntPtr cPtr, IntPtr lobbyID, int failureReason)
		{
			if (ILobbyDataRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataRetrieveListener.listeners[cPtr].OnLobbyDataRetrieveFailure(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (ILobbyDataRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000204 RID: 516
		private static Dictionary<IntPtr, ILobbyDataRetrieveListener> listeners = new Dictionary<IntPtr, ILobbyDataRetrieveListener>();

		// Token: 0x04000205 RID: 517
		private HandleRef swigCPtr;

		// Token: 0x04000206 RID: 518
		private ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_0 swigDelegate0;

		// Token: 0x04000207 RID: 519
		private ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_1 swigDelegate1;

		// Token: 0x04000208 RID: 520
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x04000209 RID: 521
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ILobbyDataRetrieveListener.FailureReason)
		};

		// Token: 0x02000122 RID: 290
		// (Invoke) Token: 0x06000B4E RID: 2894
		public delegate void SwigDelegateILobbyDataRetrieveListener_0(IntPtr cPtr, IntPtr lobbyID);

		// Token: 0x02000123 RID: 291
		// (Invoke) Token: 0x06000B52 RID: 2898
		public delegate void SwigDelegateILobbyDataRetrieveListener_1(IntPtr cPtr, IntPtr lobbyID, int failureReason);

		// Token: 0x02000124 RID: 292
		public enum FailureReason
		{
			// Token: 0x0400020B RID: 523
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400020C RID: 524
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			// Token: 0x0400020D RID: 525
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
