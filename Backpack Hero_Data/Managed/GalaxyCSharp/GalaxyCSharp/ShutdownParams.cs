using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200019E RID: 414
	public class ShutdownParams : IDisposable
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x0001CDA9 File Offset: 0x0001AFA9
		internal ShutdownParams(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0001CDC5 File Offset: 0x0001AFC5
		public ShutdownParams(bool _preserveStaticObjects)
			: this(GalaxyInstancePINVOKE.new_ShutdownParams(_preserveStaticObjects), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		internal static HandleRef getCPtr(ShutdownParams obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0001CE04 File Offset: 0x0001B004
		~ShutdownParams()
		{
			this.Dispose();
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0001CE34 File Offset: 0x0001B034
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ShutdownParams(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x0001CEB4 File Offset: 0x0001B0B4
		public bool preserveStaticObjects
		{
			get
			{
				bool flag = GalaxyInstancePINVOKE.ShutdownParams_preserveStaticObjects_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return flag;
			}
			set
			{
				GalaxyInstancePINVOKE.ShutdownParams_preserveStaticObjects_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x0400039C RID: 924
		private HandleRef swigCPtr;

		// Token: 0x0400039D RID: 925
		protected bool swigCMemOwn;
	}
}
