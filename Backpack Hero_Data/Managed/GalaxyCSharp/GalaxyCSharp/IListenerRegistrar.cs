using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200011C RID: 284
	public class IListenerRegistrar : IDisposable
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x000194E9 File Offset: 0x000176E9
		internal IListenerRegistrar(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00019505 File Offset: 0x00017705
		internal static HandleRef getCPtr(IListenerRegistrar obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00019524 File Offset: 0x00017724
		~IListenerRegistrar()
		{
			this.Dispose();
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00019554 File Offset: 0x00017754
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IListenerRegistrar(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000195D4 File Offset: 0x000177D4
		public virtual void Register(ListenerType listenerType, IGalaxyListener listener)
		{
			GalaxyInstancePINVOKE.IListenerRegistrar_Register(this.swigCPtr, (int)listenerType, IGalaxyListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000195F8 File Offset: 0x000177F8
		public virtual void Unregister(ListenerType listenerType, IGalaxyListener listener)
		{
			GalaxyInstancePINVOKE.IListenerRegistrar_Unregister(this.swigCPtr, (int)listenerType, IGalaxyListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x040001FA RID: 506
		private HandleRef swigCPtr;

		// Token: 0x040001FB RID: 507
		protected bool swigCMemOwn;
	}
}
