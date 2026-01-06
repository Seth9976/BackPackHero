using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000034 RID: 52
	public abstract class GalaxyTypeAwareListenerLeaderboardScoreUpdate : IGalaxyListener
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x000054A4 File Offset: 0x000036A4
		internal GalaxyTypeAwareListenerLeaderboardScoreUpdate(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardScoreUpdate_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000054C0 File Offset: 0x000036C0
		public GalaxyTypeAwareListenerLeaderboardScoreUpdate()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardScoreUpdate(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000054DE File Offset: 0x000036DE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardScoreUpdate obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000054FC File Offset: 0x000036FC
		~GalaxyTypeAwareListenerLeaderboardScoreUpdate()
		{
			this.Dispose();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000552C File Offset: 0x0000372C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardScoreUpdate(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000055B4 File Offset: 0x000037B4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardScoreUpdate_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000048 RID: 72
		private HandleRef swigCPtr;
	}
}
