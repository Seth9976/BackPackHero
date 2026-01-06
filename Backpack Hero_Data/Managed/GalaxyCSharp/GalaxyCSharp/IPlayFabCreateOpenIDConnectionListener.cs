using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000151 RID: 337
	public class IPlayFabCreateOpenIDConnectionListener : IDisposable
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x0001A85E File Offset: 0x00018A5E
		internal IPlayFabCreateOpenIDConnectionListener(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			IPlayFabCreateOpenIDConnectionListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0001A886 File Offset: 0x00018A86
		internal static HandleRef getCPtr(IPlayFabCreateOpenIDConnectionListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		~IPlayFabCreateOpenIDConnectionListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0001A8D4 File Offset: 0x00018AD4
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IPlayFabCreateOpenIDConnectionListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IPlayFabCreateOpenIDConnectionListener.listeners.ContainsKey(handle))
					{
						IPlayFabCreateOpenIDConnectionListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0001A97C File Offset: 0x00018B7C
		public virtual void OnPlayFabCreateOpenIDConnectionSuccess(bool connectionAlreadyExists)
		{
			GalaxyInstancePINVOKE.IPlayFabCreateOpenIDConnectionListener_OnPlayFabCreateOpenIDConnectionSuccess(this.swigCPtr, connectionAlreadyExists);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0001A99A File Offset: 0x00018B9A
		public virtual void OnPlayFabCreateOpenIDConnectionFailure(IPlayFabCreateOpenIDConnectionListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IPlayFabCreateOpenIDConnectionListener_OnPlayFabCreateOpenIDConnectionFailure(this.swigCPtr, (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x04000274 RID: 628
		private static Dictionary<IntPtr, IPlayFabCreateOpenIDConnectionListener> listeners = new Dictionary<IntPtr, IPlayFabCreateOpenIDConnectionListener>();

		// Token: 0x04000275 RID: 629
		private HandleRef swigCPtr;

		// Token: 0x04000276 RID: 630
		protected bool swigCMemOwn;

		// Token: 0x02000152 RID: 338
		public enum FailureReason
		{
			// Token: 0x04000278 RID: 632
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000279 RID: 633
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
