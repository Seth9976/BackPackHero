using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200012B RID: 299
	public abstract class ILobbyLeftListener : GalaxyTypeAwareListenerLobbyLeft
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00009E8C File Offset: 0x0000808C
		internal ILobbyLeftListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyLeftListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyLeftListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00009EB4 File Offset: 0x000080B4
		public ILobbyLeftListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyLeftListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00009ED8 File Offset: 0x000080D8
		internal static HandleRef getCPtr(ILobbyLeftListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00009EF8 File Offset: 0x000080F8
		~ILobbyLeftListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00009F28 File Offset: 0x00008128
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyLeftListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyLeftListener.listeners.ContainsKey(handle))
					{
						ILobbyLeftListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B7C RID: 2940
		public abstract void OnLobbyLeft(GalaxyID lobbyID, ILobbyLeftListener.LobbyLeaveReason leaveReason);

		// Token: 0x06000B7D RID: 2941 RVA: 0x00009FD8 File Offset: 0x000081D8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyLeft", ILobbyLeftListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyLeftListener.SwigDelegateILobbyLeftListener_0(ILobbyLeftListener.SwigDirectorOnLobbyLeft);
			}
			GalaxyInstancePINVOKE.ILobbyLeftListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0000A014 File Offset: 0x00008214
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyLeftListener));
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0000A04A File Offset: 0x0000824A
		[MonoPInvokeCallback(typeof(ILobbyLeftListener.SwigDelegateILobbyLeftListener_0))]
		private static void SwigDirectorOnLobbyLeft(IntPtr cPtr, IntPtr lobbyID, int leaveReason)
		{
			if (ILobbyLeftListener.listeners.ContainsKey(cPtr))
			{
				ILobbyLeftListener.listeners[cPtr].OnLobbyLeft(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (ILobbyLeftListener.LobbyLeaveReason)leaveReason);
			}
		}

		// Token: 0x0400021C RID: 540
		private static Dictionary<IntPtr, ILobbyLeftListener> listeners = new Dictionary<IntPtr, ILobbyLeftListener>();

		// Token: 0x0400021D RID: 541
		private HandleRef swigCPtr;

		// Token: 0x0400021E RID: 542
		private ILobbyLeftListener.SwigDelegateILobbyLeftListener_0 swigDelegate0;

		// Token: 0x0400021F RID: 543
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ILobbyLeftListener.LobbyLeaveReason)
		};

		// Token: 0x0200012C RID: 300
		// (Invoke) Token: 0x06000B82 RID: 2946
		public delegate void SwigDelegateILobbyLeftListener_0(IntPtr cPtr, IntPtr lobbyID, int leaveReason);

		// Token: 0x0200012D RID: 301
		public enum LobbyLeaveReason
		{
			// Token: 0x04000221 RID: 545
			LOBBY_LEAVE_REASON_UNDEFINED,
			// Token: 0x04000222 RID: 546
			LOBBY_LEAVE_REASON_USER_LEFT,
			// Token: 0x04000223 RID: 547
			LOBBY_LEAVE_REASON_LOBBY_CLOSED,
			// Token: 0x04000224 RID: 548
			LOBBY_LEAVE_REASON_CONNECTION_LOST
		}
	}
}
