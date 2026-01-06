using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000027 RID: 39
	public abstract class GalaxyTypeAwareListenerFriendAdd : IGalaxyListener
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00004500 File Offset: 0x00002700
		internal GalaxyTypeAwareListenerFriendAdd(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendAdd_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000451C File Offset: 0x0000271C
		public GalaxyTypeAwareListenerFriendAdd()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendAdd(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000453A File Offset: 0x0000273A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendAdd obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00004558 File Offset: 0x00002758
		~GalaxyTypeAwareListenerFriendAdd()
		{
			this.Dispose();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00004588 File Offset: 0x00002788
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendAdd(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00004610 File Offset: 0x00002810
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendAdd_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400003B RID: 59
		private HandleRef swigCPtr;
	}
}
