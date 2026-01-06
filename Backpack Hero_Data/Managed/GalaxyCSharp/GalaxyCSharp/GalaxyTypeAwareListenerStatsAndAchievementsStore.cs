using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000050 RID: 80
	public abstract class GalaxyTypeAwareListenerStatsAndAchievementsStore : IGalaxyListener
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x00007654 File Offset: 0x00005854
		internal GalaxyTypeAwareListenerStatsAndAchievementsStore(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerStatsAndAchievementsStore_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00007670 File Offset: 0x00005870
		public GalaxyTypeAwareListenerStatsAndAchievementsStore()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerStatsAndAchievementsStore(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000768E File Offset: 0x0000588E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerStatsAndAchievementsStore obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000076AC File Offset: 0x000058AC
		~GalaxyTypeAwareListenerStatsAndAchievementsStore()
		{
			this.Dispose();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000076DC File Offset: 0x000058DC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerStatsAndAchievementsStore(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00007764 File Offset: 0x00005964
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerStatsAndAchievementsStore_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000064 RID: 100
		private HandleRef swigCPtr;
	}
}
