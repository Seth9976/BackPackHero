using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000148 RID: 328
	public abstract class IOtherSessionStartListener : GalaxyTypeAwareListenerOtherSessionStart
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x00013310 File Offset: 0x00011510
		internal IOtherSessionStartListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOtherSessionStartListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOtherSessionStartListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00013338 File Offset: 0x00011538
		public IOtherSessionStartListener()
			: this(GalaxyInstancePINVOKE.new_IOtherSessionStartListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0001335C File Offset: 0x0001155C
		internal static HandleRef getCPtr(IOtherSessionStartListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0001337C File Offset: 0x0001157C
		~IOtherSessionStartListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000133AC File Offset: 0x000115AC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOtherSessionStartListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOtherSessionStartListener.listeners.ContainsKey(handle))
					{
						IOtherSessionStartListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C7A RID: 3194
		public abstract void OnOtherSessionStarted();

		// Token: 0x06000C7B RID: 3195 RVA: 0x0001345C File Offset: 0x0001165C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOtherSessionStarted", IOtherSessionStartListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOtherSessionStartListener.SwigDelegateIOtherSessionStartListener_0(IOtherSessionStartListener.SwigDirectorOnOtherSessionStarted);
			}
			GalaxyInstancePINVOKE.IOtherSessionStartListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00013498 File Offset: 0x00011698
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOtherSessionStartListener));
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000134CE File Offset: 0x000116CE
		[MonoPInvokeCallback(typeof(IOtherSessionStartListener.SwigDelegateIOtherSessionStartListener_0))]
		private static void SwigDirectorOnOtherSessionStarted(IntPtr cPtr)
		{
			if (IOtherSessionStartListener.listeners.ContainsKey(cPtr))
			{
				IOtherSessionStartListener.listeners[cPtr].OnOtherSessionStarted();
			}
		}

		// Token: 0x0400025C RID: 604
		private static Dictionary<IntPtr, IOtherSessionStartListener> listeners = new Dictionary<IntPtr, IOtherSessionStartListener>();

		// Token: 0x0400025D RID: 605
		private HandleRef swigCPtr;

		// Token: 0x0400025E RID: 606
		private IOtherSessionStartListener.SwigDelegateIOtherSessionStartListener_0 swigDelegate0;

		// Token: 0x0400025F RID: 607
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x02000149 RID: 329
		// (Invoke) Token: 0x06000C80 RID: 3200
		public delegate void SwigDelegateIOtherSessionStartListener_0(IntPtr cPtr);
	}
}
