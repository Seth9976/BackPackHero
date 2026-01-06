using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000093 RID: 147
	public abstract class GlobalOtherSessionStartListener : IOtherSessionStartListener
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x00013507 File Offset: 0x00011707
		internal GlobalOtherSessionStartListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOtherSessionStartListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00013523 File Offset: 0x00011723
		public GlobalOtherSessionStartListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOtherSessionStart.GetListenerType(), this);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00013545 File Offset: 0x00011745
		internal static HandleRef getCPtr(GlobalOtherSessionStartListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00013564 File Offset: 0x00011764
		~GlobalOtherSessionStartListener()
		{
			this.Dispose();
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00013594 File Offset: 0x00011794
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOtherSessionStart.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOtherSessionStartListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AD RID: 173
		private HandleRef swigCPtr;
	}
}
