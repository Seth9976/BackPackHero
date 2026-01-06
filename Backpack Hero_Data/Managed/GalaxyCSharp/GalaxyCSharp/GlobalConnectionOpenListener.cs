using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000073 RID: 115
	public abstract class GlobalConnectionOpenListener : IConnectionOpenListener
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		internal GlobalConnectionOpenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalConnectionOpenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000E0DD File Offset: 0x0000C2DD
		public GlobalConnectionOpenListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerConnectionOpen.GetListenerType(), this);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000E0FF File Offset: 0x0000C2FF
		internal static HandleRef getCPtr(GlobalConnectionOpenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000E120 File Offset: 0x0000C320
		~GlobalConnectionOpenListener()
		{
			this.Dispose();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000E150 File Offset: 0x0000C350
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerConnectionOpen.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalConnectionOpenListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000088 RID: 136
		private HandleRef swigCPtr;
	}
}
