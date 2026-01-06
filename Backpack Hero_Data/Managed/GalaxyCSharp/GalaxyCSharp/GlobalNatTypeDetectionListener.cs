using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008F RID: 143
	public abstract class GlobalNatTypeDetectionListener : INatTypeDetectionListener
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x00012C3A File Offset: 0x00010E3A
		internal GlobalNatTypeDetectionListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalNatTypeDetectionListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00012C56 File Offset: 0x00010E56
		public GlobalNatTypeDetectionListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerNatTypeDetection.GetListenerType(), this);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00012C78 File Offset: 0x00010E78
		internal static HandleRef getCPtr(GlobalNatTypeDetectionListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00012C98 File Offset: 0x00010E98
		~GlobalNatTypeDetectionListener()
		{
			this.Dispose();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerNatTypeDetection.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalNatTypeDetectionListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A9 RID: 169
		private HandleRef swigCPtr;
	}
}
