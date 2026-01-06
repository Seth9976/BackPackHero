using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000114 RID: 276
	public abstract class ILeaderboardScoreUpdateListener : GalaxyTypeAwareListenerLeaderboardScoreUpdate
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x00011108 File Offset: 0x0000F308
		internal ILeaderboardScoreUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardScoreUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILeaderboardScoreUpdateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00011130 File Offset: 0x0000F330
		public ILeaderboardScoreUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardScoreUpdateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00011154 File Offset: 0x0000F354
		internal static HandleRef getCPtr(ILeaderboardScoreUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00011174 File Offset: 0x0000F374
		~ILeaderboardScoreUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000111A4 File Offset: 0x0000F3A4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILeaderboardScoreUpdateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILeaderboardScoreUpdateListener.listeners.ContainsKey(handle))
					{
						ILeaderboardScoreUpdateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000AFC RID: 2812
		public abstract void OnLeaderboardScoreUpdateSuccess(string name, int score, uint oldRank, uint newRank);

		// Token: 0x06000AFD RID: 2813
		public abstract void OnLeaderboardScoreUpdateFailure(string name, int score, ILeaderboardScoreUpdateListener.FailureReason failureReason);

		// Token: 0x06000AFE RID: 2814 RVA: 0x00011254 File Offset: 0x0000F454
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLeaderboardScoreUpdateSuccess", ILeaderboardScoreUpdateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_0(ILeaderboardScoreUpdateListener.SwigDirectorOnLeaderboardScoreUpdateSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLeaderboardScoreUpdateFailure", ILeaderboardScoreUpdateListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_1(ILeaderboardScoreUpdateListener.SwigDirectorOnLeaderboardScoreUpdateFailure);
			}
			GalaxyInstancePINVOKE.ILeaderboardScoreUpdateListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000112C8 File Offset: 0x0000F4C8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardScoreUpdateListener));
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000112FE File Offset: 0x0000F4FE
		[MonoPInvokeCallback(typeof(ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_0))]
		private static void SwigDirectorOnLeaderboardScoreUpdateSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, uint oldRank, uint newRank)
		{
			if (ILeaderboardScoreUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardScoreUpdateListener.listeners[cPtr].OnLeaderboardScoreUpdateSuccess(name, score, oldRank, newRank);
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00011325 File Offset: 0x0000F525
		[MonoPInvokeCallback(typeof(ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_1))]
		private static void SwigDirectorOnLeaderboardScoreUpdateFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, int failureReason)
		{
			if (ILeaderboardScoreUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILeaderboardScoreUpdateListener.listeners[cPtr].OnLeaderboardScoreUpdateFailure(name, score, (ILeaderboardScoreUpdateListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001E7 RID: 487
		private static Dictionary<IntPtr, ILeaderboardScoreUpdateListener> listeners = new Dictionary<IntPtr, ILeaderboardScoreUpdateListener>();

		// Token: 0x040001E8 RID: 488
		private HandleRef swigCPtr;

		// Token: 0x040001E9 RID: 489
		private ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_0 swigDelegate0;

		// Token: 0x040001EA RID: 490
		private ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_1 swigDelegate1;

		// Token: 0x040001EB RID: 491
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(int),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x040001EC RID: 492
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(int),
			typeof(ILeaderboardScoreUpdateListener.FailureReason)
		};

		// Token: 0x02000115 RID: 277
		// (Invoke) Token: 0x06000B04 RID: 2820
		public delegate void SwigDelegateILeaderboardScoreUpdateListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, uint oldRank, uint newRank);

		// Token: 0x02000116 RID: 278
		// (Invoke) Token: 0x06000B08 RID: 2824
		public delegate void SwigDelegateILeaderboardScoreUpdateListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int score, int failureReason);

		// Token: 0x02000117 RID: 279
		public enum FailureReason
		{
			// Token: 0x040001EE RID: 494
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040001EF RID: 495
			FAILURE_REASON_NO_IMPROVEMENT,
			// Token: 0x040001F0 RID: 496
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
