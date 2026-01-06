using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000AB RID: 171
	public abstract class IAuthListener : GalaxyTypeAwareListenerAuth
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x000081E4 File Offset: 0x000063E4
		internal IAuthListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAuthListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IAuthListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000820C File Offset: 0x0000640C
		public IAuthListener()
			: this(GalaxyInstancePINVOKE.new_IAuthListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00008230 File Offset: 0x00006430
		internal static HandleRef getCPtr(IAuthListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00008250 File Offset: 0x00006450
		~IAuthListener()
		{
			this.Dispose();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00008280 File Offset: 0x00006480
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IAuthListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IAuthListener.listeners.ContainsKey(handle))
					{
						IAuthListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600087B RID: 2171
		public abstract void OnAuthSuccess();

		// Token: 0x0600087C RID: 2172
		public abstract void OnAuthFailure(IAuthListener.FailureReason failureReason);

		// Token: 0x0600087D RID: 2173
		public abstract void OnAuthLost();

		// Token: 0x0600087E RID: 2174 RVA: 0x00008330 File Offset: 0x00006530
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnAuthSuccess", IAuthListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IAuthListener.SwigDelegateIAuthListener_0(IAuthListener.SwigDirectorOnAuthSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnAuthFailure", IAuthListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IAuthListener.SwigDelegateIAuthListener_1(IAuthListener.SwigDirectorOnAuthFailure);
			}
			if (this.SwigDerivedClassHasMethod("OnAuthLost", IAuthListener.swigMethodTypes2))
			{
				this.swigDelegate2 = new IAuthListener.SwigDelegateIAuthListener_2(IAuthListener.SwigDirectorOnAuthLost);
			}
			GalaxyInstancePINVOKE.IAuthListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1, this.swigDelegate2);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000083D0 File Offset: 0x000065D0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAuthListener));
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00008406 File Offset: 0x00006606
		[MonoPInvokeCallback(typeof(IAuthListener.SwigDelegateIAuthListener_0))]
		private static void SwigDirectorOnAuthSuccess(IntPtr cPtr)
		{
			if (IAuthListener.listeners.ContainsKey(cPtr))
			{
				IAuthListener.listeners[cPtr].OnAuthSuccess();
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00008428 File Offset: 0x00006628
		[MonoPInvokeCallback(typeof(IAuthListener.SwigDelegateIAuthListener_1))]
		private static void SwigDirectorOnAuthFailure(IntPtr cPtr, int failureReason)
		{
			if (IAuthListener.listeners.ContainsKey(cPtr))
			{
				IAuthListener.listeners[cPtr].OnAuthFailure((IAuthListener.FailureReason)failureReason);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0000844B File Offset: 0x0000664B
		[MonoPInvokeCallback(typeof(IAuthListener.SwigDelegateIAuthListener_2))]
		private static void SwigDirectorOnAuthLost(IntPtr cPtr)
		{
			if (IAuthListener.listeners.ContainsKey(cPtr))
			{
				IAuthListener.listeners[cPtr].OnAuthLost();
			}
		}

		// Token: 0x040000D2 RID: 210
		private static Dictionary<IntPtr, IAuthListener> listeners = new Dictionary<IntPtr, IAuthListener>();

		// Token: 0x040000D3 RID: 211
		private HandleRef swigCPtr;

		// Token: 0x040000D4 RID: 212
		private IAuthListener.SwigDelegateIAuthListener_0 swigDelegate0;

		// Token: 0x040000D5 RID: 213
		private IAuthListener.SwigDelegateIAuthListener_1 swigDelegate1;

		// Token: 0x040000D6 RID: 214
		private IAuthListener.SwigDelegateIAuthListener_2 swigDelegate2;

		// Token: 0x040000D7 RID: 215
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x040000D8 RID: 216
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IAuthListener.FailureReason) };

		// Token: 0x040000D9 RID: 217
		private static Type[] swigMethodTypes2 = new Type[0];

		// Token: 0x020000AC RID: 172
		// (Invoke) Token: 0x06000885 RID: 2181
		public delegate void SwigDelegateIAuthListener_0(IntPtr cPtr);

		// Token: 0x020000AD RID: 173
		// (Invoke) Token: 0x06000889 RID: 2185
		public delegate void SwigDelegateIAuthListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000AE RID: 174
		// (Invoke) Token: 0x0600088D RID: 2189
		public delegate void SwigDelegateIAuthListener_2(IntPtr cPtr);

		// Token: 0x020000AF RID: 175
		public enum FailureReason
		{
			// Token: 0x040000DB RID: 219
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040000DC RID: 220
			FAILURE_REASON_GALAXY_SERVICE_NOT_AVAILABLE,
			// Token: 0x040000DD RID: 221
			FAILURE_REASON_GALAXY_SERVICE_NOT_SIGNED_IN,
			// Token: 0x040000DE RID: 222
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x040000DF RID: 223
			FAILURE_REASON_NO_LICENSE,
			// Token: 0x040000E0 RID: 224
			FAILURE_REASON_INVALID_CREDENTIALS,
			// Token: 0x040000E1 RID: 225
			FAILURE_REASON_GALAXY_NOT_INITIALIZED,
			// Token: 0x040000E2 RID: 226
			FAILURE_REASON_EXTERNAL_SERVICE_FAILURE
		}
	}
}
