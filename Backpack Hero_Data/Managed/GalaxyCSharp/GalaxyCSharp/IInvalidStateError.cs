using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000107 RID: 263
	public class IInvalidStateError : IError
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x00019154 File Offset: 0x00017354
		internal IInvalidStateError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IInvalidStateError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00019170 File Offset: 0x00017370
		internal static HandleRef getCPtr(IInvalidStateError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00019190 File Offset: 0x00017390
		~IInvalidStateError()
		{
			this.Dispose();
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000191C0 File Offset: 0x000173C0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IInvalidStateError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040001C8 RID: 456
		private HandleRef swigCPtr;
	}
}
