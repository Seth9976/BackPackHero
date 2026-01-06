using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009C RID: 156
	public abstract class GlobalSharedFileDownloadListener : ISharedFileDownloadListener
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00015305 File Offset: 0x00013505
		internal GlobalSharedFileDownloadListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSharedFileDownloadListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00015321 File Offset: 0x00013521
		public GlobalSharedFileDownloadListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSharedFileDownload.GetListenerType(), this);
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00015343 File Offset: 0x00013543
		internal static HandleRef getCPtr(GlobalSharedFileDownloadListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00015364 File Offset: 0x00013564
		~GlobalSharedFileDownloadListener()
		{
			this.Dispose();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00015394 File Offset: 0x00013594
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSharedFileDownload.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSharedFileDownloadListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B9 RID: 185
		private HandleRef swigCPtr;
	}
}
