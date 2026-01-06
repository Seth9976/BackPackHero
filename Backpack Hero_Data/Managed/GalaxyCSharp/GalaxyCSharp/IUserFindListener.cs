using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200017D RID: 381
	public abstract class IUserFindListener : GalaxyTypeAwareListenerUserFind
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x00015D48 File Offset: 0x00013F48
		internal IUserFindListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserFindListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserFindListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00015D70 File Offset: 0x00013F70
		public IUserFindListener()
			: this(GalaxyInstancePINVOKE.new_IUserFindListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00015D94 File Offset: 0x00013F94
		internal static HandleRef getCPtr(IUserFindListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00015DB4 File Offset: 0x00013FB4
		~IUserFindListener()
		{
			this.Dispose();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00015DE4 File Offset: 0x00013FE4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserFindListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserFindListener.listeners.ContainsKey(handle))
					{
						IUserFindListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000E31 RID: 3633
		public abstract void OnUserFindSuccess(string userSpecifier, GalaxyID userID);

		// Token: 0x06000E32 RID: 3634
		public abstract void OnUserFindFailure(string userSpecifier, IUserFindListener.FailureReason failureReason);

		// Token: 0x06000E33 RID: 3635 RVA: 0x00015E94 File Offset: 0x00014094
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserFindSuccess", IUserFindListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserFindListener.SwigDelegateIUserFindListener_0(IUserFindListener.SwigDirectorOnUserFindSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnUserFindFailure", IUserFindListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IUserFindListener.SwigDelegateIUserFindListener_1(IUserFindListener.SwigDirectorOnUserFindFailure);
			}
			GalaxyInstancePINVOKE.IUserFindListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00015F08 File Offset: 0x00014108
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserFindListener));
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00015F3E File Offset: 0x0001413E
		[MonoPInvokeCallback(typeof(IUserFindListener.SwigDelegateIUserFindListener_0))]
		private static void SwigDirectorOnUserFindSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, IntPtr userID)
		{
			if (IUserFindListener.listeners.ContainsKey(cPtr))
			{
				IUserFindListener.listeners[cPtr].OnUserFindSuccess(userSpecifier, new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00015F72 File Offset: 0x00014172
		[MonoPInvokeCallback(typeof(IUserFindListener.SwigDelegateIUserFindListener_1))]
		private static void SwigDirectorOnUserFindFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, int failureReason)
		{
			if (IUserFindListener.listeners.ContainsKey(cPtr))
			{
				IUserFindListener.listeners[cPtr].OnUserFindFailure(userSpecifier, (IUserFindListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040002E3 RID: 739
		private static Dictionary<IntPtr, IUserFindListener> listeners = new Dictionary<IntPtr, IUserFindListener>();

		// Token: 0x040002E4 RID: 740
		private HandleRef swigCPtr;

		// Token: 0x040002E5 RID: 741
		private IUserFindListener.SwigDelegateIUserFindListener_0 swigDelegate0;

		// Token: 0x040002E6 RID: 742
		private IUserFindListener.SwigDelegateIUserFindListener_1 swigDelegate1;

		// Token: 0x040002E7 RID: 743
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(GalaxyID)
		};

		// Token: 0x040002E8 RID: 744
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(IUserFindListener.FailureReason)
		};

		// Token: 0x0200017E RID: 382
		// (Invoke) Token: 0x06000E39 RID: 3641
		public delegate void SwigDelegateIUserFindListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, IntPtr userID);

		// Token: 0x0200017F RID: 383
		// (Invoke) Token: 0x06000E3D RID: 3645
		public delegate void SwigDelegateIUserFindListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string userSpecifier, int failureReason);

		// Token: 0x02000180 RID: 384
		public enum FailureReason
		{
			// Token: 0x040002EA RID: 746
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040002EB RID: 747
			FAILURE_REASON_USER_NOT_FOUND,
			// Token: 0x040002EC RID: 748
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
