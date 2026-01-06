using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200014E RID: 334
	public abstract class IPersonaDataChangedListener : GalaxyTypeAwareListenerPersonaDataChanged
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x00013CF8 File Offset: 0x00011EF8
		internal IPersonaDataChangedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IPersonaDataChangedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IPersonaDataChangedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00013D20 File Offset: 0x00011F20
		public IPersonaDataChangedListener()
			: this(GalaxyInstancePINVOKE.new_IPersonaDataChangedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00013D44 File Offset: 0x00011F44
		internal static HandleRef getCPtr(IPersonaDataChangedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00013D64 File Offset: 0x00011F64
		~IPersonaDataChangedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00013D94 File Offset: 0x00011F94
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IPersonaDataChangedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IPersonaDataChangedListener.listeners.ContainsKey(handle))
					{
						IPersonaDataChangedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CA4 RID: 3236
		public abstract void OnPersonaDataChanged(GalaxyID userID, uint personaStateChange);

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00013E44 File Offset: 0x00012044
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnPersonaDataChanged", IPersonaDataChangedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IPersonaDataChangedListener.SwigDelegateIPersonaDataChangedListener_0(IPersonaDataChangedListener.SwigDirectorOnPersonaDataChanged);
			}
			GalaxyInstancePINVOKE.IPersonaDataChangedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00013E80 File Offset: 0x00012080
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IPersonaDataChangedListener));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00013EB6 File Offset: 0x000120B6
		[MonoPInvokeCallback(typeof(IPersonaDataChangedListener.SwigDelegateIPersonaDataChangedListener_0))]
		private static void SwigDirectorOnPersonaDataChanged(IntPtr cPtr, IntPtr userID, uint personaStateChange)
		{
			if (IPersonaDataChangedListener.listeners.ContainsKey(cPtr))
			{
				IPersonaDataChangedListener.listeners[cPtr].OnPersonaDataChanged(new GalaxyID(new GalaxyID(userID, false).ToUint64()), personaStateChange);
			}
		}

		// Token: 0x04000268 RID: 616
		private static Dictionary<IntPtr, IPersonaDataChangedListener> listeners = new Dictionary<IntPtr, IPersonaDataChangedListener>();

		// Token: 0x04000269 RID: 617
		private HandleRef swigCPtr;

		// Token: 0x0400026A RID: 618
		private IPersonaDataChangedListener.SwigDelegateIPersonaDataChangedListener_0 swigDelegate0;

		// Token: 0x0400026B RID: 619
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(uint)
		};

		// Token: 0x0200014F RID: 335
		// (Invoke) Token: 0x06000CAA RID: 3242
		public delegate void SwigDelegateIPersonaDataChangedListener_0(IntPtr cPtr, IntPtr userID, uint personaStateChange);

		// Token: 0x02000150 RID: 336
		public enum PersonaStateChange
		{
			// Token: 0x0400026D RID: 621
			PERSONA_CHANGE_NONE,
			// Token: 0x0400026E RID: 622
			PERSONA_CHANGE_NAME,
			// Token: 0x0400026F RID: 623
			PERSONA_CHANGE_AVATAR,
			// Token: 0x04000270 RID: 624
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_SMALL = 4,
			// Token: 0x04000271 RID: 625
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_MEDIUM = 8,
			// Token: 0x04000272 RID: 626
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_LARGE = 16,
			// Token: 0x04000273 RID: 627
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_ANY = 28
		}
	}
}
