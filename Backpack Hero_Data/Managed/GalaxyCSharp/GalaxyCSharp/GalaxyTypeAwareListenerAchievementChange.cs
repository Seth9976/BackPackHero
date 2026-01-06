using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000018 RID: 24
	public abstract class GalaxyTypeAwareListenerAchievementChange : IGalaxyListener
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x000032F4 File Offset: 0x000014F4
		internal GalaxyTypeAwareListenerAchievementChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAchievementChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00003310 File Offset: 0x00001510
		public GalaxyTypeAwareListenerAchievementChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerAchievementChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000332E File Offset: 0x0000152E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerAchievementChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000334C File Offset: 0x0000154C
		~GalaxyTypeAwareListenerAchievementChange()
		{
			this.Dispose();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000337C File Offset: 0x0000157C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerAchievementChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00003404 File Offset: 0x00001604
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAchievementChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400002C RID: 44
		private HandleRef swigCPtr;
	}
}
