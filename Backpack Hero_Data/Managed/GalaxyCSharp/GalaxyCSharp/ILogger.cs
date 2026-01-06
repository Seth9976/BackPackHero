using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200013A RID: 314
	public class ILogger : IDisposable
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001961C File Offset: 0x0001781C
		internal ILogger(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00019638 File Offset: 0x00017838
		internal static HandleRef getCPtr(ILogger obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00019658 File Offset: 0x00017858
		~ILogger()
		{
			this.Dispose();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00019688 File Offset: 0x00017888
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILogger(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00019708 File Offset: 0x00017908
		public virtual void Trace(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Trace(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00019726 File Offset: 0x00017926
		public virtual void Debug(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Debug(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00019744 File Offset: 0x00017944
		public virtual void Info(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Info(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00019762 File Offset: 0x00017962
		public virtual void Warning(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Warning(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00019780 File Offset: 0x00017980
		public virtual void Error(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Error(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0001979E File Offset: 0x0001799E
		public virtual void Fatal(string format)
		{
			GalaxyInstancePINVOKE.ILogger_Fatal(this.swigCPtr, format);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0400023F RID: 575
		private HandleRef swigCPtr;

		// Token: 0x04000240 RID: 576
		protected bool swigCMemOwn;
	}
}
