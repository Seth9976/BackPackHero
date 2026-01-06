using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006B RID: 107
	public abstract class GlobalAchievementChangeListener : IAchievementChangeListener
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0000C631 File Offset: 0x0000A831
		internal GlobalAchievementChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalAchievementChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000C64D File Offset: 0x0000A84D
		public GlobalAchievementChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerAchievementChange.GetListenerType(), this);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000C66F File Offset: 0x0000A86F
		internal static HandleRef getCPtr(GlobalAchievementChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000C690 File Offset: 0x0000A890
		~GlobalAchievementChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerAchievementChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalAchievementChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000080 RID: 128
		private HandleRef swigCPtr;
	}
}
