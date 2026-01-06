using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000D4 RID: 212
	public abstract class IConnectionDataListener : GalaxyTypeAwareListenerConnectionData
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		internal IConnectionDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IConnectionDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public IConnectionDataListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		internal static HandleRef getCPtr(IConnectionDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		~IConnectionDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IConnectionDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IConnectionDataListener.listeners.ContainsKey(handle))
					{
						IConnectionDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000968 RID: 2408
		public abstract void OnConnectionDataReceived(ulong connectionID, uint dataSize);

		// Token: 0x06000969 RID: 2409 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionDataReceived", IConnectionDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IConnectionDataListener.SwigDelegateIConnectionDataListener_0(IConnectionDataListener.SwigDirectorOnConnectionDataReceived);
			}
			GalaxyInstancePINVOKE.IConnectionDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000DC68 File Offset: 0x0000BE68
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionDataListener));
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000DC9E File Offset: 0x0000BE9E
		[MonoPInvokeCallback(typeof(IConnectionDataListener.SwigDelegateIConnectionDataListener_0))]
		private static void SwigDirectorOnConnectionDataReceived(IntPtr cPtr, ulong connectionID, uint dataSize)
		{
			if (IConnectionDataListener.listeners.ContainsKey(cPtr))
			{
				IConnectionDataListener.listeners[cPtr].OnConnectionDataReceived(connectionID, dataSize);
			}
		}

		// Token: 0x0400014F RID: 335
		private static Dictionary<IntPtr, IConnectionDataListener> listeners = new Dictionary<IntPtr, IConnectionDataListener>();

		// Token: 0x04000150 RID: 336
		private HandleRef swigCPtr;

		// Token: 0x04000151 RID: 337
		private IConnectionDataListener.SwigDelegateIConnectionDataListener_0 swigDelegate0;

		// Token: 0x04000152 RID: 338
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint)
		};

		// Token: 0x020000D5 RID: 213
		// (Invoke) Token: 0x0600096E RID: 2414
		public delegate void SwigDelegateIConnectionDataListener_0(IntPtr cPtr, ulong connectionID, uint dataSize);
	}
}
