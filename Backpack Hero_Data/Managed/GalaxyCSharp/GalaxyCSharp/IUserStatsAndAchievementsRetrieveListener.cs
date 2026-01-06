using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000185 RID: 389
	public abstract class IUserStatsAndAchievementsRetrieveListener : GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x00016294 File Offset: 0x00014494
		internal IUserStatsAndAchievementsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserStatsAndAchievementsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserStatsAndAchievementsRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000162BC File Offset: 0x000144BC
		public IUserStatsAndAchievementsRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserStatsAndAchievementsRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000162E0 File Offset: 0x000144E0
		internal static HandleRef getCPtr(IUserStatsAndAchievementsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00016300 File Offset: 0x00014500
		~IUserStatsAndAchievementsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00016330 File Offset: 0x00014530
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserStatsAndAchievementsRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserStatsAndAchievementsRetrieveListener.listeners.ContainsKey(handle))
					{
						IUserStatsAndAchievementsRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000E59 RID: 3673
		public abstract void OnUserStatsAndAchievementsRetrieveSuccess(GalaxyID userID);

		// Token: 0x06000E5A RID: 3674
		public abstract void OnUserStatsAndAchievementsRetrieveFailure(GalaxyID userID, IUserStatsAndAchievementsRetrieveListener.FailureReason failureReason);

		// Token: 0x06000E5B RID: 3675 RVA: 0x000163E0 File Offset: 0x000145E0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsRetrieveSuccess", IUserStatsAndAchievementsRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_0(IUserStatsAndAchievementsRetrieveListener.SwigDirectorOnUserStatsAndAchievementsRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsRetrieveFailure", IUserStatsAndAchievementsRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_1(IUserStatsAndAchievementsRetrieveListener.SwigDirectorOnUserStatsAndAchievementsRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IUserStatsAndAchievementsRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00016454 File Offset: 0x00014654
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserStatsAndAchievementsRetrieveListener));
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0001648A File Offset: 0x0001468A
		[MonoPInvokeCallback(typeof(IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_0))]
		private static void SwigDirectorOnUserStatsAndAchievementsRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (IUserStatsAndAchievementsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserStatsAndAchievementsRetrieveListener.listeners[cPtr].OnUserStatsAndAchievementsRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000164BD File Offset: 0x000146BD
		[MonoPInvokeCallback(typeof(IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_1))]
		private static void SwigDirectorOnUserStatsAndAchievementsRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IUserStatsAndAchievementsRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IUserStatsAndAchievementsRetrieveListener.listeners[cPtr].OnUserStatsAndAchievementsRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IUserStatsAndAchievementsRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002F6 RID: 758
		private static Dictionary<IntPtr, IUserStatsAndAchievementsRetrieveListener> listeners = new Dictionary<IntPtr, IUserStatsAndAchievementsRetrieveListener>();

		// Token: 0x040002F7 RID: 759
		private HandleRef swigCPtr;

		// Token: 0x040002F8 RID: 760
		private IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_0 swigDelegate0;

		// Token: 0x040002F9 RID: 761
		private IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_1 swigDelegate1;

		// Token: 0x040002FA RID: 762
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040002FB RID: 763
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IUserStatsAndAchievementsRetrieveListener.FailureReason)
		};

		// Token: 0x02000186 RID: 390
		// (Invoke) Token: 0x06000E61 RID: 3681
		public delegate void SwigDelegateIUserStatsAndAchievementsRetrieveListener_0(IntPtr cPtr, IntPtr userID);

		// Token: 0x02000187 RID: 391
		// (Invoke) Token: 0x06000E65 RID: 3685
		public delegate void SwigDelegateIUserStatsAndAchievementsRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x02000188 RID: 392
		public enum FailureReason
		{
			// Token: 0x040002FD RID: 765
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002FE RID: 766
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
