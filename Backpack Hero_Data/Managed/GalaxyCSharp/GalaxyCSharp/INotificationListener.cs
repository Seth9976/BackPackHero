using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000143 RID: 323
	public abstract class INotificationListener : GalaxyTypeAwareListenerNotification
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x00012E94 File Offset: 0x00011094
		internal INotificationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INotificationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			INotificationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00012EBC File Offset: 0x000110BC
		public INotificationListener()
			: this(GalaxyInstancePINVOKE.new_INotificationListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00012EE0 File Offset: 0x000110E0
		internal static HandleRef getCPtr(INotificationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00012F00 File Offset: 0x00011100
		~INotificationListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00012F30 File Offset: 0x00011130
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INotificationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (INotificationListener.listeners.ContainsKey(handle))
					{
						INotificationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C5E RID: 3166
		public abstract void OnNotificationReceived(ulong notificationID, uint typeLength, uint contentSize);

		// Token: 0x06000C5F RID: 3167 RVA: 0x00012FE0 File Offset: 0x000111E0
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnNotificationReceived", INotificationListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new INotificationListener.SwigDelegateINotificationListener_0(INotificationListener.SwigDirectorOnNotificationReceived);
			}
			GalaxyInstancePINVOKE.INotificationListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0001301C File Offset: 0x0001121C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INotificationListener));
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00013052 File Offset: 0x00011252
		[MonoPInvokeCallback(typeof(INotificationListener.SwigDelegateINotificationListener_0))]
		private static void SwigDirectorOnNotificationReceived(IntPtr cPtr, ulong notificationID, uint typeLength, uint contentSize)
		{
			if (INotificationListener.listeners.ContainsKey(cPtr))
			{
				INotificationListener.listeners[cPtr].OnNotificationReceived(notificationID, typeLength, contentSize);
			}
		}

		// Token: 0x04000251 RID: 593
		private static Dictionary<IntPtr, INotificationListener> listeners = new Dictionary<IntPtr, INotificationListener>();

		// Token: 0x04000252 RID: 594
		private HandleRef swigCPtr;

		// Token: 0x04000253 RID: 595
		private INotificationListener.SwigDelegateINotificationListener_0 swigDelegate0;

		// Token: 0x04000254 RID: 596
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x02000144 RID: 324
		// (Invoke) Token: 0x06000C64 RID: 3172
		public delegate void SwigDelegateINotificationListener_0(IntPtr cPtr, ulong notificationID, uint typeLength, uint contentSize);
	}
}
