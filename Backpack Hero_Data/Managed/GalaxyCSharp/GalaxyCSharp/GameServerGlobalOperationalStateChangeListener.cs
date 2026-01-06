using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000065 RID: 101
	public abstract class GameServerGlobalOperationalStateChangeListener : IOperationalStateChangeListener
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0000B269 File Offset: 0x00009469
		internal GameServerGlobalOperationalStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalOperationalStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000B285 File Offset: 0x00009485
		public GameServerGlobalOperationalStateChangeListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000B2A7 File Offset: 0x000094A7
		internal static HandleRef getCPtr(GameServerGlobalOperationalStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000B2C8 File Offset: 0x000094C8
		~GameServerGlobalOperationalStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerOperationalStateChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalOperationalStateChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000079 RID: 121
		private HandleRef swigCPtr;
	}
}
