using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A6 RID: 166
	public abstract class IAccessTokenListener : GalaxyTypeAwareListenerAccessToken
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x00007EC0 File Offset: 0x000060C0
		internal IAccessTokenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAccessTokenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IAccessTokenListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00007EE8 File Offset: 0x000060E8
		public IAccessTokenListener()
			: this(GalaxyInstancePINVOKE.new_IAccessTokenListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00007F0C File Offset: 0x0000610C
		internal static HandleRef getCPtr(IAccessTokenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00007F2C File Offset: 0x0000612C
		~IAccessTokenListener()
		{
			this.Dispose();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00007F5C File Offset: 0x0000615C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IAccessTokenListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IAccessTokenListener.listeners.ContainsKey(handle))
					{
						IAccessTokenListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000851 RID: 2129
		public abstract void OnAccessTokenChanged();

		// Token: 0x06000852 RID: 2130 RVA: 0x0000800C File Offset: 0x0000620C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnAccessTokenChanged", IAccessTokenListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IAccessTokenListener.SwigDelegateIAccessTokenListener_0(IAccessTokenListener.SwigDirectorOnAccessTokenChanged);
			}
			GalaxyInstancePINVOKE.IAccessTokenListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00008048 File Offset: 0x00006248
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAccessTokenListener));
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0000807E File Offset: 0x0000627E
		[MonoPInvokeCallback(typeof(IAccessTokenListener.SwigDelegateIAccessTokenListener_0))]
		private static void SwigDirectorOnAccessTokenChanged(IntPtr cPtr)
		{
			if (IAccessTokenListener.listeners.ContainsKey(cPtr))
			{
				IAccessTokenListener.listeners[cPtr].OnAccessTokenChanged();
			}
		}

		// Token: 0x040000C8 RID: 200
		private static Dictionary<IntPtr, IAccessTokenListener> listeners = new Dictionary<IntPtr, IAccessTokenListener>();

		// Token: 0x040000C9 RID: 201
		private HandleRef swigCPtr;

		// Token: 0x040000CA RID: 202
		private IAccessTokenListener.SwigDelegateIAccessTokenListener_0 swigDelegate0;

		// Token: 0x040000CB RID: 203
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x020000A7 RID: 167
		// (Invoke) Token: 0x06000857 RID: 2135
		public delegate void SwigDelegateIAccessTokenListener_0(IntPtr cPtr);
	}
}
