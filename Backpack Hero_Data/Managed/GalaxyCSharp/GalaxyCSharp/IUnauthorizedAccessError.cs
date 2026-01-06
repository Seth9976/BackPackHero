using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000179 RID: 377
	public class IUnauthorizedAccessError : IError
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0001BE75 File Offset: 0x0001A075
		internal IUnauthorizedAccessError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUnauthorizedAccessError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0001BE91 File Offset: 0x0001A091
		internal static HandleRef getCPtr(IUnauthorizedAccessError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		~IUnauthorizedAccessError()
		{
			this.Dispose();
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUnauthorizedAccessError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040002DC RID: 732
		private HandleRef swigCPtr;
	}
}
