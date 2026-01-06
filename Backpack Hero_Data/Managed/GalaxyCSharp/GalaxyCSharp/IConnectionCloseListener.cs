using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000D1 RID: 209
	public abstract class IConnectionCloseListener : GalaxyTypeAwareListenerConnectionClose
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		internal IConnectionCloseListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionCloseListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IConnectionCloseListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public IConnectionCloseListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionCloseListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		internal static HandleRef getCPtr(IConnectionCloseListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0000D80C File Offset: 0x0000BA0C
		~IConnectionCloseListener()
		{
			this.Dispose();
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IConnectionCloseListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IConnectionCloseListener.listeners.ContainsKey(handle))
					{
						IConnectionCloseListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600095A RID: 2394
		public abstract void OnConnectionClosed(ulong connectionID, IConnectionCloseListener.CloseReason closeReason);

		// Token: 0x0600095B RID: 2395 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnConnectionClosed", IConnectionCloseListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IConnectionCloseListener.SwigDelegateIConnectionCloseListener_0(IConnectionCloseListener.SwigDirectorOnConnectionClosed);
			}
			GalaxyInstancePINVOKE.IConnectionCloseListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0000D928 File Offset: 0x0000BB28
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionCloseListener));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0000D95E File Offset: 0x0000BB5E
		[MonoPInvokeCallback(typeof(IConnectionCloseListener.SwigDelegateIConnectionCloseListener_0))]
		private static void SwigDirectorOnConnectionClosed(IntPtr cPtr, ulong connectionID, int closeReason)
		{
			if (IConnectionCloseListener.listeners.ContainsKey(cPtr))
			{
				IConnectionCloseListener.listeners[cPtr].OnConnectionClosed(connectionID, (IConnectionCloseListener.CloseReason)closeReason);
			}
		}

		// Token: 0x04000149 RID: 329
		private static Dictionary<IntPtr, IConnectionCloseListener> listeners = new Dictionary<IntPtr, IConnectionCloseListener>();

		// Token: 0x0400014A RID: 330
		private HandleRef swigCPtr;

		// Token: 0x0400014B RID: 331
		private IConnectionCloseListener.SwigDelegateIConnectionCloseListener_0 swigDelegate0;

		// Token: 0x0400014C RID: 332
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(IConnectionCloseListener.CloseReason)
		};

		// Token: 0x020000D2 RID: 210
		// (Invoke) Token: 0x06000960 RID: 2400
		public delegate void SwigDelegateIConnectionCloseListener_0(IntPtr cPtr, ulong connectionID, int closeReason);

		// Token: 0x020000D3 RID: 211
		public enum CloseReason
		{
			// Token: 0x0400014E RID: 334
			CLOSE_REASON_UNDEFINED
		}
	}
}
