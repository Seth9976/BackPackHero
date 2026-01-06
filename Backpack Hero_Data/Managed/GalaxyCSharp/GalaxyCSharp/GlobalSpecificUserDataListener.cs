using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009D RID: 157
	public abstract class GlobalSpecificUserDataListener : ISpecificUserDataListener
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00015434 File Offset: 0x00013634
		internal GlobalSpecificUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSpecificUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00015450 File Offset: 0x00013650
		public GlobalSpecificUserDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00015472 File Offset: 0x00013672
		internal static HandleRef getCPtr(GlobalSpecificUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00015490 File Offset: 0x00013690
		~GlobalSpecificUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000154C0 File Offset: 0x000136C0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSpecificUserDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BA RID: 186
		private HandleRef swigCPtr;
	}
}
