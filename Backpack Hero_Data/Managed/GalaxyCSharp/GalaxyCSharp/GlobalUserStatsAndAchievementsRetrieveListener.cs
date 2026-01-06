using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A3 RID: 163
	public abstract class GlobalUserStatsAndAchievementsRetrieveListener : IUserStatsAndAchievementsRetrieveListener
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x00016548 File Offset: 0x00014748
		internal GlobalUserStatsAndAchievementsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserStatsAndAchievementsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00016564 File Offset: 0x00014764
		public GlobalUserStatsAndAchievementsRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00016586 File Offset: 0x00014786
		internal static HandleRef getCPtr(GlobalUserStatsAndAchievementsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000165A4 File Offset: 0x000147A4
		~GlobalUserStatsAndAchievementsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000165D4 File Offset: 0x000147D4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserStatsAndAchievementsRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000C1 RID: 193
		private HandleRef swigCPtr;
	}
}
