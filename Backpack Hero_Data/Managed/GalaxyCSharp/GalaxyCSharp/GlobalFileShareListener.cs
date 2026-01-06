using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000075 RID: 117
	public abstract class GlobalFileShareListener : IFileShareListener
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x0000E5BD File Offset: 0x0000C7BD
		internal GlobalFileShareListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFileShareListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000E5D9 File Offset: 0x0000C7D9
		public GlobalFileShareListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFileShare.GetListenerType(), this);
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000E5FB File Offset: 0x0000C7FB
		internal static HandleRef getCPtr(GlobalFileShareListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000E61C File Offset: 0x0000C81C
		~GlobalFileShareListener()
		{
			this.Dispose();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000E64C File Offset: 0x0000C84C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFileShare.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFileShareListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008A RID: 138
		private HandleRef swigCPtr;
	}
}
