using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000175 RID: 373
	public abstract class ITelemetryEventSendListener : GalaxyTypeAwareListenerTelemetryEventSend
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x0000BB04 File Offset: 0x00009D04
		internal ITelemetryEventSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ITelemetryEventSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ITelemetryEventSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0000BB2C File Offset: 0x00009D2C
		public ITelemetryEventSendListener()
			: this(GalaxyInstancePINVOKE.new_ITelemetryEventSendListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0000BB50 File Offset: 0x00009D50
		internal static HandleRef getCPtr(ITelemetryEventSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0000BB70 File Offset: 0x00009D70
		~ITelemetryEventSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ITelemetryEventSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ITelemetryEventSendListener.listeners.ContainsKey(handle))
					{
						ITelemetryEventSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000DC3 RID: 3523
		public abstract void OnTelemetryEventSendSuccess(string eventType, uint sentEventIndex);

		// Token: 0x06000DC4 RID: 3524
		public abstract void OnTelemetryEventSendFailure(string eventType, uint sentEventIndex, ITelemetryEventSendListener.FailureReason failureReason);

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0000BC50 File Offset: 0x00009E50
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnTelemetryEventSendSuccess", ITelemetryEventSendListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_0(ITelemetryEventSendListener.SwigDirectorOnTelemetryEventSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnTelemetryEventSendFailure", ITelemetryEventSendListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_1(ITelemetryEventSendListener.SwigDirectorOnTelemetryEventSendFailure);
			}
			GalaxyInstancePINVOKE.ITelemetryEventSendListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ITelemetryEventSendListener));
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0000BCFA File Offset: 0x00009EFA
		[MonoPInvokeCallback(typeof(ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_0))]
		private static void SwigDirectorOnTelemetryEventSendSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex)
		{
			if (ITelemetryEventSendListener.listeners.ContainsKey(cPtr))
			{
				ITelemetryEventSendListener.listeners[cPtr].OnTelemetryEventSendSuccess(eventType, sentEventIndex);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0000BD1E File Offset: 0x00009F1E
		[MonoPInvokeCallback(typeof(ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_1))]
		private static void SwigDirectorOnTelemetryEventSendFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex, int failureReason)
		{
			if (ITelemetryEventSendListener.listeners.ContainsKey(cPtr))
			{
				ITelemetryEventSendListener.listeners[cPtr].OnTelemetryEventSendFailure(eventType, sentEventIndex, (ITelemetryEventSendListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002CC RID: 716
		private static Dictionary<IntPtr, ITelemetryEventSendListener> listeners = new Dictionary<IntPtr, ITelemetryEventSendListener>();

		// Token: 0x040002CD RID: 717
		private HandleRef swigCPtr;

		// Token: 0x040002CE RID: 718
		private ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_0 swigDelegate0;

		// Token: 0x040002CF RID: 719
		private ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_1 swigDelegate1;

		// Token: 0x040002D0 RID: 720
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(uint)
		};

		// Token: 0x040002D1 RID: 721
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(uint),
			typeof(ITelemetryEventSendListener.FailureReason)
		};

		// Token: 0x02000176 RID: 374
		// (Invoke) Token: 0x06000DCB RID: 3531
		public delegate void SwigDelegateITelemetryEventSendListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex);

		// Token: 0x02000177 RID: 375
		// (Invoke) Token: 0x06000DCF RID: 3535
		public delegate void SwigDelegateITelemetryEventSendListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string eventType, uint sentEventIndex, int failureReason);

		// Token: 0x02000178 RID: 376
		public enum FailureReason
		{
			// Token: 0x040002D3 RID: 723
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002D4 RID: 724
			FAILURE_REASON_CLIENT_FORBIDDEN,
			// Token: 0x040002D5 RID: 725
			FAILURE_REASON_INVALID_DATA,
			// Token: 0x040002D6 RID: 726
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x040002D7 RID: 727
			FAILURE_REASON_NO_SAMPLING_CLASS_IN_CONFIG,
			// Token: 0x040002D8 RID: 728
			FAILURE_REASON_SAMPLING_CLASS_FIELD_MISSING,
			// Token: 0x040002D9 RID: 729
			FAILURE_REASON_EVENT_SAMPLED_OUT,
			// Token: 0x040002DA RID: 730
			FAILURE_REASON_SAMPLING_RESULT_ALREADY_EXIST,
			// Token: 0x040002DB RID: 731
			FAILURE_REASON_SAMPLING_INVALID_RESULT_PATH
		}
	}
}
