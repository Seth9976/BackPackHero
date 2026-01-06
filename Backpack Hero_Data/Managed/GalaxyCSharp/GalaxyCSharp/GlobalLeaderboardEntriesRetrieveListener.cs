using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000080 RID: 128
	public abstract class GlobalLeaderboardEntriesRetrieveListener : ILeaderboardEntriesRetrieveListener
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00010C19 File Offset: 0x0000EE19
		internal GlobalLeaderboardEntriesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardEntriesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00010C35 File Offset: 0x0000EE35
		public GlobalLeaderboardEntriesRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardEntriesRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00010C57 File Offset: 0x0000EE57
		internal static HandleRef getCPtr(GlobalLeaderboardEntriesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00010C78 File Offset: 0x0000EE78
		~GlobalLeaderboardEntriesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardEntriesRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardEntriesRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009A RID: 154
		private HandleRef swigCPtr;
	}
}
