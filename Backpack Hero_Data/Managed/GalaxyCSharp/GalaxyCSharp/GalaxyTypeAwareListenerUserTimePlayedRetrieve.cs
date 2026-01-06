using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000056 RID: 86
	public abstract class GalaxyTypeAwareListenerUserTimePlayedRetrieve : IGalaxyListener
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00007D8C File Offset: 0x00005F8C
		internal GalaxyTypeAwareListenerUserTimePlayedRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserTimePlayedRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public GalaxyTypeAwareListenerUserTimePlayedRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserTimePlayedRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00007DC6 File Offset: 0x00005FC6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserTimePlayedRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00007DE4 File Offset: 0x00005FE4
		~GalaxyTypeAwareListenerUserTimePlayedRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00007E14 File Offset: 0x00006014
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserTimePlayedRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00007E9C File Offset: 0x0000609C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserTimePlayedRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400006A RID: 106
		private HandleRef swigCPtr;
	}
}
