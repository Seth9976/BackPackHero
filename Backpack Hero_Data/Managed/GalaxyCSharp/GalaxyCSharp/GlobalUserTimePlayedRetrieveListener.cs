using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A4 RID: 164
	public abstract class GlobalUserTimePlayedRetrieveListener : IUserTimePlayedRetrieveListener
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x00016928 File Offset: 0x00014B28
		internal GlobalUserTimePlayedRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserTimePlayedRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00016944 File Offset: 0x00014B44
		public GlobalUserTimePlayedRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserTimePlayedRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00016966 File Offset: 0x00014B66
		internal static HandleRef getCPtr(GlobalUserTimePlayedRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00016984 File Offset: 0x00014B84
		~GlobalUserTimePlayedRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000169B4 File Offset: 0x00014BB4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserTimePlayedRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserTimePlayedRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000C2 RID: 194
		private HandleRef swigCPtr;
	}
}
