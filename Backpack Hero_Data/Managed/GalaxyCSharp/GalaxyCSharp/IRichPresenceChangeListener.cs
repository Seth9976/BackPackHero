using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000155 RID: 341
	public abstract class IRichPresenceChangeListener : GalaxyTypeAwareListenerRichPresenceChange
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x00014048 File Offset: 0x00012248
		internal IRichPresenceChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IRichPresenceChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00014070 File Offset: 0x00012270
		public IRichPresenceChangeListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00014094 File Offset: 0x00012294
		internal static HandleRef getCPtr(IRichPresenceChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x000140B4 File Offset: 0x000122B4
		~IRichPresenceChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000140E4 File Offset: 0x000122E4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRichPresenceChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IRichPresenceChangeListener.listeners.ContainsKey(handle))
					{
						IRichPresenceChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CC0 RID: 3264
		public abstract void OnRichPresenceChangeSuccess();

		// Token: 0x06000CC1 RID: 3265
		public abstract void OnRichPresenceChangeFailure(IRichPresenceChangeListener.FailureReason failureReason);

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00014194 File Offset: 0x00012394
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnRichPresenceChangeSuccess", IRichPresenceChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_0(IRichPresenceChangeListener.SwigDirectorOnRichPresenceChangeSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnRichPresenceChangeFailure", IRichPresenceChangeListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_1(IRichPresenceChangeListener.SwigDirectorOnRichPresenceChangeFailure);
			}
			GalaxyInstancePINVOKE.IRichPresenceChangeListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00014208 File Offset: 0x00012408
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceChangeListener));
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0001423E File Offset: 0x0001243E
		[MonoPInvokeCallback(typeof(IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_0))]
		private static void SwigDirectorOnRichPresenceChangeSuccess(IntPtr cPtr)
		{
			if (IRichPresenceChangeListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceChangeListener.listeners[cPtr].OnRichPresenceChangeSuccess();
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00014260 File Offset: 0x00012460
		[MonoPInvokeCallback(typeof(IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_1))]
		private static void SwigDirectorOnRichPresenceChangeFailure(IntPtr cPtr, int failureReason)
		{
			if (IRichPresenceChangeListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceChangeListener.listeners[cPtr].OnRichPresenceChangeFailure((IRichPresenceChangeListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000280 RID: 640
		private static Dictionary<IntPtr, IRichPresenceChangeListener> listeners = new Dictionary<IntPtr, IRichPresenceChangeListener>();

		// Token: 0x04000281 RID: 641
		private HandleRef swigCPtr;

		// Token: 0x04000282 RID: 642
		private IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_0 swigDelegate0;

		// Token: 0x04000283 RID: 643
		private IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_1 swigDelegate1;

		// Token: 0x04000284 RID: 644
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x04000285 RID: 645
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IRichPresenceChangeListener.FailureReason) };

		// Token: 0x02000156 RID: 342
		// (Invoke) Token: 0x06000CC8 RID: 3272
		public delegate void SwigDelegateIRichPresenceChangeListener_0(IntPtr cPtr);

		// Token: 0x02000157 RID: 343
		// (Invoke) Token: 0x06000CCC RID: 3276
		public delegate void SwigDelegateIRichPresenceChangeListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x02000158 RID: 344
		public enum FailureReason
		{
			// Token: 0x04000287 RID: 647
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000288 RID: 648
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
