using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000145 RID: 325
	public abstract class IOperationalStateChangeListener : GalaxyTypeAwareListenerOperationalStateChange
	{
		// Token: 0x06000C67 RID: 3175 RVA: 0x0000B064 File Offset: 0x00009264
		internal IOperationalStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOperationalStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOperationalStateChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0000B08C File Offset: 0x0000928C
		public IOperationalStateChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOperationalStateChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0000B0B0 File Offset: 0x000092B0
		internal static HandleRef getCPtr(IOperationalStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0000B0D0 File Offset: 0x000092D0
		~IOperationalStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0000B100 File Offset: 0x00009300
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOperationalStateChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOperationalStateChangeListener.listeners.ContainsKey(handle))
					{
						IOperationalStateChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C6C RID: 3180
		public abstract void OnOperationalStateChanged(uint operationalState);

		// Token: 0x06000C6D RID: 3181 RVA: 0x0000B1B0 File Offset: 0x000093B0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOperationalStateChanged", IOperationalStateChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOperationalStateChangeListener.SwigDelegateIOperationalStateChangeListener_0(IOperationalStateChangeListener.SwigDirectorOnOperationalStateChanged);
			}
			GalaxyInstancePINVOKE.IOperationalStateChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0000B1EC File Offset: 0x000093EC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOperationalStateChangeListener));
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0000B222 File Offset: 0x00009422
		[MonoPInvokeCallback(typeof(IOperationalStateChangeListener.SwigDelegateIOperationalStateChangeListener_0))]
		private static void SwigDirectorOnOperationalStateChanged(IntPtr cPtr, uint operationalState)
		{
			if (IOperationalStateChangeListener.listeners.ContainsKey(cPtr))
			{
				IOperationalStateChangeListener.listeners[cPtr].OnOperationalStateChanged(operationalState);
			}
		}

		// Token: 0x04000255 RID: 597
		private static Dictionary<IntPtr, IOperationalStateChangeListener> listeners = new Dictionary<IntPtr, IOperationalStateChangeListener>();

		// Token: 0x04000256 RID: 598
		private HandleRef swigCPtr;

		// Token: 0x04000257 RID: 599
		private IOperationalStateChangeListener.SwigDelegateIOperationalStateChangeListener_0 swigDelegate0;

		// Token: 0x04000258 RID: 600
		private static Type[] swigMethodTypes0 = new Type[] { typeof(uint) };

		// Token: 0x02000146 RID: 326
		// (Invoke) Token: 0x06000C72 RID: 3186
		public delegate void SwigDelegateIOperationalStateChangeListener_0(IntPtr cPtr, uint operationalState);

		// Token: 0x02000147 RID: 327
		public enum OperationalState
		{
			// Token: 0x0400025A RID: 602
			OPERATIONAL_STATE_SIGNED_IN = 1,
			// Token: 0x0400025B RID: 603
			OPERATIONAL_STATE_LOGGED_ON
		}
	}
}
