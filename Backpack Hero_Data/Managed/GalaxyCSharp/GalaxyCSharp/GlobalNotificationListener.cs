using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000091 RID: 145
	public abstract class GlobalNotificationListener : INotificationListener
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x000130B5 File Offset: 0x000112B5
		internal GlobalNotificationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalNotificationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000130D1 File Offset: 0x000112D1
		public GlobalNotificationListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerNotification.GetListenerType(), this);
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000130F3 File Offset: 0x000112F3
		internal static HandleRef getCPtr(GlobalNotificationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00013114 File Offset: 0x00011314
		~GlobalNotificationListener()
		{
			this.Dispose();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00013144 File Offset: 0x00011344
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerNotification.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalNotificationListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AB RID: 171
		private HandleRef swigCPtr;
	}
}
