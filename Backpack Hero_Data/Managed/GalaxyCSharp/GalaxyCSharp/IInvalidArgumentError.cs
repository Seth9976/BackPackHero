using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000106 RID: 262
	public class IInvalidArgumentError : IError
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00019060 File Offset: 0x00017260
		internal IInvalidArgumentError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IInvalidArgumentError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001907C File Offset: 0x0001727C
		internal static HandleRef getCPtr(IInvalidArgumentError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001909C File Offset: 0x0001729C
		~IInvalidArgumentError()
		{
			this.Dispose();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000190CC File Offset: 0x000172CC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IInvalidArgumentError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040001C7 RID: 455
		private HandleRef swigCPtr;
	}
}
