using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000081 RID: 129
	public abstract class GlobalLeaderboardRetrieveListener : ILeaderboardRetrieveListener
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00010FDC File Offset: 0x0000F1DC
		internal GlobalLeaderboardRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public GlobalLeaderboardRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001101A File Offset: 0x0000F21A
		internal static HandleRef getCPtr(GlobalLeaderboardRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00011038 File Offset: 0x0000F238
		~GlobalLeaderboardRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00011068 File Offset: 0x0000F268
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009B RID: 155
		private HandleRef swigCPtr;
	}
}
