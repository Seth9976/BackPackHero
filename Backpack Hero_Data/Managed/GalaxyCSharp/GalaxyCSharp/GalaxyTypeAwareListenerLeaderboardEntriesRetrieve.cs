using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000032 RID: 50
	public abstract class GalaxyTypeAwareListenerLeaderboardEntriesRetrieve : IGalaxyListener
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0000523C File Offset: 0x0000343C
		internal GalaxyTypeAwareListenerLeaderboardEntriesRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00005258 File Offset: 0x00003458
		public GalaxyTypeAwareListenerLeaderboardEntriesRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00005276 File Offset: 0x00003476
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardEntriesRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00005294 File Offset: 0x00003494
		~GalaxyTypeAwareListenerLeaderboardEntriesRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000052C4 File Offset: 0x000034C4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000534C File Offset: 0x0000354C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000046 RID: 70
		private HandleRef swigCPtr;
	}
}
