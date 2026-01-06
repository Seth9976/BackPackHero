using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000140 RID: 320
	public abstract class INetworkingListener : GalaxyTypeAwareListenerNetworking
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0000AD24 File Offset: 0x00008F24
		internal INetworkingListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INetworkingListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			INetworkingListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public INetworkingListener()
			: this(GalaxyInstancePINVOKE.new_INetworkingListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0000AD70 File Offset: 0x00008F70
		internal static HandleRef getCPtr(INetworkingListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0000AD90 File Offset: 0x00008F90
		~INetworkingListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INetworkingListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (INetworkingListener.listeners.ContainsKey(handle))
					{
						INetworkingListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C3B RID: 3131
		public abstract void OnP2PPacketAvailable(uint msgSize, byte channel);

		// Token: 0x06000C3C RID: 3132 RVA: 0x0000AE70 File Offset: 0x00009070
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnP2PPacketAvailable", INetworkingListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new INetworkingListener.SwigDelegateINetworkingListener_0(INetworkingListener.SwigDirectorOnP2PPacketAvailable);
			}
			GalaxyInstancePINVOKE.INetworkingListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0000AEAC File Offset: 0x000090AC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INetworkingListener));
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0000AEE2 File Offset: 0x000090E2
		[MonoPInvokeCallback(typeof(INetworkingListener.SwigDelegateINetworkingListener_0))]
		private static void SwigDirectorOnP2PPacketAvailable(IntPtr cPtr, uint msgSize, byte channel)
		{
			if (INetworkingListener.listeners.ContainsKey(cPtr))
			{
				INetworkingListener.listeners[cPtr].OnP2PPacketAvailable(msgSize, channel);
			}
		}

		// Token: 0x0400024B RID: 587
		private static Dictionary<IntPtr, INetworkingListener> listeners = new Dictionary<IntPtr, INetworkingListener>();

		// Token: 0x0400024C RID: 588
		private HandleRef swigCPtr;

		// Token: 0x0400024D RID: 589
		private INetworkingListener.SwigDelegateINetworkingListener_0 swigDelegate0;

		// Token: 0x0400024E RID: 590
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(uint),
			typeof(byte)
		};

		// Token: 0x02000141 RID: 321
		// (Invoke) Token: 0x06000C41 RID: 3137
		public delegate void SwigDelegateINetworkingListener_0(IntPtr cPtr, uint msgSize, byte channel);
	}
}
