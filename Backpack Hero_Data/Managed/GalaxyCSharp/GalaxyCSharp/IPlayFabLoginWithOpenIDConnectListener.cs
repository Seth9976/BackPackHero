using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000153 RID: 339
	public class IPlayFabLoginWithOpenIDConnectListener : IDisposable
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		internal IPlayFabLoginWithOpenIDConnectListener(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			IPlayFabLoginWithOpenIDConnectListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0001A9EC File Offset: 0x00018BEC
		internal static HandleRef getCPtr(IPlayFabLoginWithOpenIDConnectListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0001AA0C File Offset: 0x00018C0C
		~IPlayFabLoginWithOpenIDConnectListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IPlayFabLoginWithOpenIDConnectListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IPlayFabLoginWithOpenIDConnectListener.listeners.ContainsKey(handle))
					{
						IPlayFabLoginWithOpenIDConnectListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		public virtual void OnPlayFabLoginWithOpenIDConnectSuccess()
		{
			GalaxyInstancePINVOKE.IPlayFabLoginWithOpenIDConnectListener_OnPlayFabLoginWithOpenIDConnectSuccess(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0001AB01 File Offset: 0x00018D01
		public virtual void OnPlayFabLoginWithOpenIDConnectFailure(IPlayFabLoginWithOpenIDConnectListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IPlayFabLoginWithOpenIDConnectListener_OnPlayFabLoginWithOpenIDConnectFailure(this.swigCPtr, (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0400027A RID: 634
		private static Dictionary<IntPtr, IPlayFabLoginWithOpenIDConnectListener> listeners = new Dictionary<IntPtr, IPlayFabLoginWithOpenIDConnectListener>();

		// Token: 0x0400027B RID: 635
		private HandleRef swigCPtr;

		// Token: 0x0400027C RID: 636
		protected bool swigCMemOwn;

		// Token: 0x02000154 RID: 340
		public enum FailureReason
		{
			// Token: 0x0400027E RID: 638
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400027F RID: 639
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
