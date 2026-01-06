using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004B RID: 75
	public abstract class GalaxyTypeAwareListenerRichPresenceRetrieve : IGalaxyListener
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00007050 File Offset: 0x00005250
		internal GalaxyTypeAwareListenerRichPresenceRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000706C File Offset: 0x0000526C
		public GalaxyTypeAwareListenerRichPresenceRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerRichPresenceRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000708A File Offset: 0x0000528A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerRichPresenceRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000070A8 File Offset: 0x000052A8
		~GalaxyTypeAwareListenerRichPresenceRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000070D8 File Offset: 0x000052D8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerRichPresenceRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00007160 File Offset: 0x00005360
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005F RID: 95
		private HandleRef swigCPtr;
	}
}
