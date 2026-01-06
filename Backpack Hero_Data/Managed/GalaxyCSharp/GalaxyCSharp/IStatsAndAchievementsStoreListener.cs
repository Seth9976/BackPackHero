using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200016F RID: 367
	public abstract class IStatsAndAchievementsStoreListener : GalaxyTypeAwareListenerStatsAndAchievementsStore
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x00015560 File Offset: 0x00013760
		internal IStatsAndAchievementsStoreListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IStatsAndAchievementsStoreListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IStatsAndAchievementsStoreListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00015588 File Offset: 0x00013788
		public IStatsAndAchievementsStoreListener()
			: this(GalaxyInstancePINVOKE.new_IStatsAndAchievementsStoreListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000155AC File Offset: 0x000137AC
		internal static HandleRef getCPtr(IStatsAndAchievementsStoreListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000155CC File Offset: 0x000137CC
		~IStatsAndAchievementsStoreListener()
		{
			this.Dispose();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000155FC File Offset: 0x000137FC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStatsAndAchievementsStoreListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IStatsAndAchievementsStoreListener.listeners.ContainsKey(handle))
					{
						IStatsAndAchievementsStoreListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000D81 RID: 3457
		public abstract void OnUserStatsAndAchievementsStoreSuccess();

		// Token: 0x06000D82 RID: 3458
		public abstract void OnUserStatsAndAchievementsStoreFailure(IStatsAndAchievementsStoreListener.FailureReason failureReason);

		// Token: 0x06000D83 RID: 3459 RVA: 0x000156AC File Offset: 0x000138AC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsStoreSuccess", IStatsAndAchievementsStoreListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_0(IStatsAndAchievementsStoreListener.SwigDirectorOnUserStatsAndAchievementsStoreSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserStatsAndAchievementsStoreFailure", IStatsAndAchievementsStoreListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_1(IStatsAndAchievementsStoreListener.SwigDirectorOnUserStatsAndAchievementsStoreFailure);
			}
			GalaxyInstancePINVOKE.IStatsAndAchievementsStoreListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00015720 File Offset: 0x00013920
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IStatsAndAchievementsStoreListener));
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00015756 File Offset: 0x00013956
		[MonoPInvokeCallback(typeof(IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_0))]
		private static void SwigDirectorOnUserStatsAndAchievementsStoreSuccess(IntPtr cPtr)
		{
			if (IStatsAndAchievementsStoreListener.listeners.ContainsKey(cPtr))
			{
				IStatsAndAchievementsStoreListener.listeners[cPtr].OnUserStatsAndAchievementsStoreSuccess();
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00015778 File Offset: 0x00013978
		[MonoPInvokeCallback(typeof(IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_1))]
		private static void SwigDirectorOnUserStatsAndAchievementsStoreFailure(IntPtr cPtr, int failureReason)
		{
			if (IStatsAndAchievementsStoreListener.listeners.ContainsKey(cPtr))
			{
				IStatsAndAchievementsStoreListener.listeners[cPtr].OnUserStatsAndAchievementsStoreFailure((IStatsAndAchievementsStoreListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002BF RID: 703
		private static Dictionary<IntPtr, IStatsAndAchievementsStoreListener> listeners = new Dictionary<IntPtr, IStatsAndAchievementsStoreListener>();

		// Token: 0x040002C0 RID: 704
		private HandleRef swigCPtr;

		// Token: 0x040002C1 RID: 705
		private IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_0 swigDelegate0;

		// Token: 0x040002C2 RID: 706
		private IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_1 swigDelegate1;

		// Token: 0x040002C3 RID: 707
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040002C4 RID: 708
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IStatsAndAchievementsStoreListener.FailureReason) };

		// Token: 0x02000170 RID: 368
		// (Invoke) Token: 0x06000D89 RID: 3465
		public delegate void SwigDelegateIStatsAndAchievementsStoreListener_0(IntPtr cPtr);

		// Token: 0x02000171 RID: 369
		// (Invoke) Token: 0x06000D8D RID: 3469
		public delegate void SwigDelegateIStatsAndAchievementsStoreListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x02000172 RID: 370
		public enum FailureReason
		{
			// Token: 0x040002C6 RID: 710
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002C7 RID: 711
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
