using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000036 RID: 54
	public abstract class GalaxyTypeAwareListenerLobbyCreated : IGalaxyListener
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0000570C File Offset: 0x0000390C
		internal GalaxyTypeAwareListenerLobbyCreated(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyCreated_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00005728 File Offset: 0x00003928
		public GalaxyTypeAwareListenerLobbyCreated()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyCreated(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00005746 File Offset: 0x00003946
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyCreated obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00005764 File Offset: 0x00003964
		~GalaxyTypeAwareListenerLobbyCreated()
		{
			this.Dispose();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00005794 File Offset: 0x00003994
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyCreated(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000581C File Offset: 0x00003A1C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyCreated_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004A RID: 74
		private HandleRef swigCPtr;
	}
}
