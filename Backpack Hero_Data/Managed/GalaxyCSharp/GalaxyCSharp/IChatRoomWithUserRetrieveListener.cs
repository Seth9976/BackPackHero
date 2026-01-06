using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000BB RID: 187
	public abstract class IChatRoomWithUserRetrieveListener : GalaxyTypeAwareListenerChatRoomWithUserRetrieve
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		internal IChatRoomWithUserRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomWithUserRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomWithUserRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		public IChatRoomWithUserRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomWithUserRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		internal static HandleRef getCPtr(IChatRoomWithUserRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0000D41C File Offset: 0x0000B61C
		~IChatRoomWithUserRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000D44C File Offset: 0x0000B64C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomWithUserRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomWithUserRetrieveListener.listeners.ContainsKey(handle))
					{
						IChatRoomWithUserRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008DB RID: 2267
		public abstract void OnChatRoomWithUserRetrieveSuccess(GalaxyID userID, ulong chatRoomID);

		// Token: 0x060008DC RID: 2268
		public abstract void OnChatRoomWithUserRetrieveFailure(GalaxyID userID, IChatRoomWithUserRetrieveListener.FailureReason failureReason);

		// Token: 0x060008DD RID: 2269 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomWithUserRetrieveSuccess", IChatRoomWithUserRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_0(IChatRoomWithUserRetrieveListener.SwigDirectorOnChatRoomWithUserRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnChatRoomWithUserRetrieveFailure", IChatRoomWithUserRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_1(IChatRoomWithUserRetrieveListener.SwigDirectorOnChatRoomWithUserRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IChatRoomWithUserRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000D570 File Offset: 0x0000B770
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomWithUserRetrieveListener));
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
		[MonoPInvokeCallback(typeof(IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_0))]
		private static void SwigDirectorOnChatRoomWithUserRetrieveSuccess(IntPtr cPtr, IntPtr userID, ulong chatRoomID)
		{
			if (IChatRoomWithUserRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomWithUserRetrieveListener.listeners[cPtr].OnChatRoomWithUserRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()), chatRoomID);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0000D5DA File Offset: 0x0000B7DA
		[MonoPInvokeCallback(typeof(IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_1))]
		private static void SwigDirectorOnChatRoomWithUserRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IChatRoomWithUserRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomWithUserRetrieveListener.listeners[cPtr].OnChatRoomWithUserRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IChatRoomWithUserRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040000FD RID: 253
		private static Dictionary<IntPtr, IChatRoomWithUserRetrieveListener> listeners = new Dictionary<IntPtr, IChatRoomWithUserRetrieveListener>();

		// Token: 0x040000FE RID: 254
		private HandleRef swigCPtr;

		// Token: 0x040000FF RID: 255
		private IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_0 swigDelegate0;

		// Token: 0x04000100 RID: 256
		private IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_1 swigDelegate1;

		// Token: 0x04000101 RID: 257
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ulong)
		};

		// Token: 0x04000102 RID: 258
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IChatRoomWithUserRetrieveListener.FailureReason)
		};

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x060008E3 RID: 2275
		public delegate void SwigDelegateIChatRoomWithUserRetrieveListener_0(IntPtr cPtr, IntPtr userID, ulong chatRoomID);

		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x060008E7 RID: 2279
		public delegate void SwigDelegateIChatRoomWithUserRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000BE RID: 190
		public enum FailureReason
		{
			// Token: 0x04000104 RID: 260
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000105 RID: 261
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000106 RID: 262
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
