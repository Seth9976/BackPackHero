using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000FF RID: 255
	public class IGalaxyListener : IDisposable
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x00003076 File Offset: 0x00001276
		internal IGalaxyListener(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			IGalaxyListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000309E File Offset: 0x0000129E
		public IGalaxyListener()
			: this(GalaxyInstancePINVOKE.new_IGalaxyListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000030BC File Offset: 0x000012BC
		internal static HandleRef getCPtr(IGalaxyListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000030DC File Offset: 0x000012DC
		~IGalaxyListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000310C File Offset: 0x0000130C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IGalaxyListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IGalaxyListener.listeners.ContainsKey(handle))
					{
						IGalaxyListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x040001B8 RID: 440
		private static Dictionary<IntPtr, IGalaxyListener> listeners = new Dictionary<IntPtr, IGalaxyListener>();

		// Token: 0x040001B9 RID: 441
		private HandleRef swigCPtr;

		// Token: 0x040001BA RID: 442
		protected bool swigCMemOwn;
	}
}
