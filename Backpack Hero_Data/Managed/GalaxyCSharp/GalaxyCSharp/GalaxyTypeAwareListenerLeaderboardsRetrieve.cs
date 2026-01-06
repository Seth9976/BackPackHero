using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000035 RID: 53
	public abstract class GalaxyTypeAwareListenerLeaderboardsRetrieve : IGalaxyListener
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x000055D8 File Offset: 0x000037D8
		internal GalaxyTypeAwareListenerLeaderboardsRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardsRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000055F4 File Offset: 0x000037F4
		public GalaxyTypeAwareListenerLeaderboardsRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardsRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00005612 File Offset: 0x00003812
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardsRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00005630 File Offset: 0x00003830
		~GalaxyTypeAwareListenerLeaderboardsRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00005660 File Offset: 0x00003860
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardsRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000056E8 File Offset: 0x000038E8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardsRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000049 RID: 73
		private HandleRef swigCPtr;
	}
}
