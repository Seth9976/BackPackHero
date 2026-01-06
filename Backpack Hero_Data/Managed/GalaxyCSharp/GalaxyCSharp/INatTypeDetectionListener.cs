using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200013C RID: 316
	public abstract class INatTypeDetectionListener : GalaxyTypeAwareListenerNatTypeDetection
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x000129D0 File Offset: 0x00010BD0
		internal INatTypeDetectionListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INatTypeDetectionListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			INatTypeDetectionListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000129F8 File Offset: 0x00010BF8
		public INatTypeDetectionListener()
			: this(GalaxyInstancePINVOKE.new_INatTypeDetectionListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00012A1C File Offset: 0x00010C1C
		internal static HandleRef getCPtr(INatTypeDetectionListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00012A3C File Offset: 0x00010C3C
		~INatTypeDetectionListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00012A6C File Offset: 0x00010C6C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INatTypeDetectionListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (INatTypeDetectionListener.listeners.ContainsKey(handle))
					{
						INatTypeDetectionListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C15 RID: 3093
		public abstract void OnNatTypeDetectionSuccess(NatType natType);

		// Token: 0x06000C16 RID: 3094
		public abstract void OnNatTypeDetectionFailure();

		// Token: 0x06000C17 RID: 3095 RVA: 0x00012B1C File Offset: 0x00010D1C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnNatTypeDetectionSuccess", INatTypeDetectionListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_0(INatTypeDetectionListener.SwigDirectorOnNatTypeDetectionSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnNatTypeDetectionFailure", INatTypeDetectionListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_1(INatTypeDetectionListener.SwigDirectorOnNatTypeDetectionFailure);
			}
			GalaxyInstancePINVOKE.INatTypeDetectionListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00012B90 File Offset: 0x00010D90
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INatTypeDetectionListener));
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00012BC6 File Offset: 0x00010DC6
		[MonoPInvokeCallback(typeof(INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_0))]
		private static void SwigDirectorOnNatTypeDetectionSuccess(IntPtr cPtr, int natType)
		{
			if (INatTypeDetectionListener.listeners.ContainsKey(cPtr))
			{
				INatTypeDetectionListener.listeners[cPtr].OnNatTypeDetectionSuccess((NatType)natType);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00012BE9 File Offset: 0x00010DE9
		[MonoPInvokeCallback(typeof(INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_1))]
		private static void SwigDirectorOnNatTypeDetectionFailure(IntPtr cPtr)
		{
			if (INatTypeDetectionListener.listeners.ContainsKey(cPtr))
			{
				INatTypeDetectionListener.listeners[cPtr].OnNatTypeDetectionFailure();
			}
		}

		// Token: 0x04000243 RID: 579
		private static Dictionary<IntPtr, INatTypeDetectionListener> listeners = new Dictionary<IntPtr, INatTypeDetectionListener>();

		// Token: 0x04000244 RID: 580
		private HandleRef swigCPtr;

		// Token: 0x04000245 RID: 581
		private INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_0 swigDelegate0;

		// Token: 0x04000246 RID: 582
		private INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_1 swigDelegate1;

		// Token: 0x04000247 RID: 583
		private static Type[] swigMethodTypes0 = new Type[] { typeof(NatType) };

		// Token: 0x04000248 RID: 584
		private static Type[] swigMethodTypes1 = new Type[0];

		// Token: 0x0200013D RID: 317
		// (Invoke) Token: 0x06000C1D RID: 3101
		public delegate void SwigDelegateINatTypeDetectionListener_0(IntPtr cPtr, int natType);

		// Token: 0x0200013E RID: 318
		// (Invoke) Token: 0x06000C21 RID: 3105
		public delegate void SwigDelegateINatTypeDetectionListener_1(IntPtr cPtr);
	}
}
