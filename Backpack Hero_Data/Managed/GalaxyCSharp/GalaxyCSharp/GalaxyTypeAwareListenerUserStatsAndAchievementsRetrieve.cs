using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000055 RID: 85
	public abstract class GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve : IGalaxyListener
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x00007C58 File Offset: 0x00005E58
		internal GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00007C74 File Offset: 0x00005E74
		public GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00007C92 File Offset: 0x00005E92
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00007CB0 File Offset: 0x00005EB0
		~GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00007D68 File Offset: 0x00005F68
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000069 RID: 105
		private HandleRef swigCPtr;
	}
}
