using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200015F RID: 351
	public class IRuntimeError : IError
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x0001AB2B File Offset: 0x00018D2B
		internal IRuntimeError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRuntimeError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0001AB47 File Offset: 0x00018D47
		internal static HandleRef getCPtr(IRuntimeError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0001AB68 File Offset: 0x00018D68
		~IRuntimeError()
		{
			this.Dispose();
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0001AB98 File Offset: 0x00018D98
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRuntimeError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000296 RID: 662
		private HandleRef swigCPtr;
	}
}
