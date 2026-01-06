using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000033 RID: 51
	public abstract class GalaxyTypeAwareListenerLeaderboardRetrieve : IGalaxyListener
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x00005370 File Offset: 0x00003570
		internal GalaxyTypeAwareListenerLeaderboardRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000538C File Offset: 0x0000358C
		public GalaxyTypeAwareListenerLeaderboardRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLeaderboardRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000053AA File Offset: 0x000035AA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLeaderboardRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000053C8 File Offset: 0x000035C8
		~GalaxyTypeAwareListenerLeaderboardRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000053F8 File Offset: 0x000035F8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLeaderboardRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00005480 File Offset: 0x00003680
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLeaderboardRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000047 RID: 71
		private HandleRef swigCPtr;
	}
}
