using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000096 RID: 150
	public abstract class GlobalPersonaDataChangedListener : IPersonaDataChangedListener
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x00013F1B File Offset: 0x0001211B
		internal GlobalPersonaDataChangedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalPersonaDataChangedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00013F37 File Offset: 0x00012137
		public GlobalPersonaDataChangedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerPersonaDataChanged.GetListenerType(), this);
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00013F59 File Offset: 0x00012159
		internal static HandleRef getCPtr(GlobalPersonaDataChangedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00013F78 File Offset: 0x00012178
		~GlobalPersonaDataChangedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00013FA8 File Offset: 0x000121A8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerPersonaDataChanged.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalPersonaDataChangedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B1 RID: 177
		private HandleRef swigCPtr;
	}
}
