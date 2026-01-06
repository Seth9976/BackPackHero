using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000DF RID: 223
	public class IError : IDisposable
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x000182CE File Offset: 0x000164CE
		internal IError(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000182EA File Offset: 0x000164EA
		internal static HandleRef getCPtr(IError obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00018308 File Offset: 0x00016508
		~IError()
		{
			this.Dispose();
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00018338 File Offset: 0x00016538
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IError(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000183B8 File Offset: 0x000165B8
		public override string ToString()
		{
			return string.Format("{0}: {1} ({2})", this.GetName(), this.GetMsg(), this.GetErrorType().ToString());
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000183F0 File Offset: 0x000165F0
		public virtual string GetName()
		{
			string text = GalaxyInstancePINVOKE.IError_GetName(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001841C File Offset: 0x0001661C
		public virtual string GetMsg()
		{
			string text = GalaxyInstancePINVOKE.IError_GetMsg(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00018448 File Offset: 0x00016648
		public virtual IError.Type GetErrorType()
		{
			IError.Type type = (IError.Type)GalaxyInstancePINVOKE.IError_GetErrorType(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return type;
		}

		// Token: 0x04000168 RID: 360
		private HandleRef swigCPtr;

		// Token: 0x04000169 RID: 361
		protected bool swigCMemOwn;

		// Token: 0x020000E0 RID: 224
		public enum Type
		{
			// Token: 0x0400016B RID: 363
			UNAUTHORIZED_ACCESS,
			// Token: 0x0400016C RID: 364
			INVALID_ARGUMENT,
			// Token: 0x0400016D RID: 365
			INVALID_STATE,
			// Token: 0x0400016E RID: 366
			RUNTIME_ERROR
		}
	}
}
