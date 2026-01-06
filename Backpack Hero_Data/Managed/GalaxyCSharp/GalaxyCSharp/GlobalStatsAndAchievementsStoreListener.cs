using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009E RID: 158
	public abstract class GlobalStatsAndAchievementsStoreListener : IStatsAndAchievementsStoreListener
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x000157CA File Offset: 0x000139CA
		internal GlobalStatsAndAchievementsStoreListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalStatsAndAchievementsStoreListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000157E6 File Offset: 0x000139E6
		public GlobalStatsAndAchievementsStoreListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerStatsAndAchievementsStore.GetListenerType(), this);
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00015808 File Offset: 0x00013A08
		internal static HandleRef getCPtr(GlobalStatsAndAchievementsStoreListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00015828 File Offset: 0x00013A28
		~GlobalStatsAndAchievementsStoreListener()
		{
			this.Dispose();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00015858 File Offset: 0x00013A58
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerStatsAndAchievementsStore.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalStatsAndAchievementsStoreListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BB RID: 187
		private HandleRef swigCPtr;
	}
}
