using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000102 RID: 258
	public abstract class IGameJoinRequestedListener : GalaxyTypeAwareListenerGameJoinRequested
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x000104FC File Offset: 0x0000E6FC
		internal IGameJoinRequestedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGameJoinRequestedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IGameJoinRequestedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00010524 File Offset: 0x0000E724
		public IGameJoinRequestedListener()
			: this(GalaxyInstancePINVOKE.new_IGameJoinRequestedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00010548 File Offset: 0x0000E748
		internal static HandleRef getCPtr(IGameJoinRequestedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00010568 File Offset: 0x0000E768
		~IGameJoinRequestedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00010598 File Offset: 0x0000E798
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IGameJoinRequestedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IGameJoinRequestedListener.listeners.ContainsKey(handle))
					{
						IGameJoinRequestedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A9C RID: 2716
		public abstract void OnGameJoinRequested(GalaxyID userID, string connectionString);

		// Token: 0x06000A9D RID: 2717 RVA: 0x00010648 File Offset: 0x0000E848
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGameJoinRequested", IGameJoinRequestedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IGameJoinRequestedListener.SwigDelegateIGameJoinRequestedListener_0(IGameJoinRequestedListener.SwigDirectorOnGameJoinRequested);
			}
			GalaxyInstancePINVOKE.IGameJoinRequestedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00010684 File Offset: 0x0000E884
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGameJoinRequestedListener));
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000106BA File Offset: 0x0000E8BA
		[MonoPInvokeCallback(typeof(IGameJoinRequestedListener.SwigDelegateIGameJoinRequestedListener_0))]
		private static void SwigDirectorOnGameJoinRequested(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString)
		{
			if (IGameJoinRequestedListener.listeners.ContainsKey(cPtr))
			{
				IGameJoinRequestedListener.listeners[cPtr].OnGameJoinRequested(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString);
			}
		}

		// Token: 0x040001BF RID: 447
		private static Dictionary<IntPtr, IGameJoinRequestedListener> listeners = new Dictionary<IntPtr, IGameJoinRequestedListener>();

		// Token: 0x040001C0 RID: 448
		private HandleRef swigCPtr;

		// Token: 0x040001C1 RID: 449
		private IGameJoinRequestedListener.SwigDelegateIGameJoinRequestedListener_0 swigDelegate0;

		// Token: 0x040001C2 RID: 450
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string)
		};

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x06000AA2 RID: 2722
		public delegate void SwigDelegateIGameJoinRequestedListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString);
	}
}
