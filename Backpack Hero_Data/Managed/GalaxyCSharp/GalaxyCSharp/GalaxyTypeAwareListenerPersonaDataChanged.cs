using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000048 RID: 72
	public abstract class GalaxyTypeAwareListenerPersonaDataChanged : IGalaxyListener
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x00006CB4 File Offset: 0x00004EB4
		internal GalaxyTypeAwareListenerPersonaDataChanged(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerPersonaDataChanged_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public GalaxyTypeAwareListenerPersonaDataChanged()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerPersonaDataChanged(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00006CEE File Offset: 0x00004EEE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerPersonaDataChanged obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00006D0C File Offset: 0x00004F0C
		~GalaxyTypeAwareListenerPersonaDataChanged()
		{
			this.Dispose();
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00006D3C File Offset: 0x00004F3C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerPersonaDataChanged(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerPersonaDataChanged_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005C RID: 92
		private HandleRef swigCPtr;
	}
}
