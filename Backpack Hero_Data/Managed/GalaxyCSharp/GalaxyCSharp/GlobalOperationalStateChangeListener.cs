using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000092 RID: 146
	public abstract class GlobalOperationalStateChangeListener : IOperationalStateChangeListener
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x000131E4 File Offset: 0x000113E4
		internal GlobalOperationalStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOperationalStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00013200 File Offset: 0x00011400
		public GlobalOperationalStateChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00013222 File Offset: 0x00011422
		internal static HandleRef getCPtr(GlobalOperationalStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00013240 File Offset: 0x00011440
		~GlobalOperationalStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00013270 File Offset: 0x00011470
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOperationalStateChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AC RID: 172
		private HandleRef swigCPtr;
	}
}
