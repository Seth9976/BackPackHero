using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A8 RID: 168
	public abstract class IAchievementChangeListener : GalaxyTypeAwareListenerAchievementChange
	{
		// Token: 0x0600085A RID: 2138 RVA: 0x0000C42C File Offset: 0x0000A62C
		internal IAchievementChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAchievementChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IAchievementChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0000C454 File Offset: 0x0000A654
		public IAchievementChangeListener()
			: this(GalaxyInstancePINVOKE.new_IAchievementChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0000C478 File Offset: 0x0000A678
		internal static HandleRef getCPtr(IAchievementChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0000C498 File Offset: 0x0000A698
		~IAchievementChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IAchievementChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IAchievementChangeListener.listeners.ContainsKey(handle))
					{
						IAchievementChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600085F RID: 2143
		public abstract void OnAchievementUnlocked(string name);

		// Token: 0x06000860 RID: 2144 RVA: 0x0000C578 File Offset: 0x0000A778
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnAchievementUnlocked", IAchievementChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IAchievementChangeListener.SwigDelegateIAchievementChangeListener_0(IAchievementChangeListener.SwigDirectorOnAchievementUnlocked);
			}
			GalaxyInstancePINVOKE.IAchievementChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAchievementChangeListener));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0000C5EA File Offset: 0x0000A7EA
		[MonoPInvokeCallback(typeof(IAchievementChangeListener.SwigDelegateIAchievementChangeListener_0))]
		private static void SwigDirectorOnAchievementUnlocked(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (IAchievementChangeListener.listeners.ContainsKey(cPtr))
			{
				IAchievementChangeListener.listeners[cPtr].OnAchievementUnlocked(name);
			}
		}

		// Token: 0x040000CC RID: 204
		private static Dictionary<IntPtr, IAchievementChangeListener> listeners = new Dictionary<IntPtr, IAchievementChangeListener>();

		// Token: 0x040000CD RID: 205
		private HandleRef swigCPtr;

		// Token: 0x040000CE RID: 206
		private IAchievementChangeListener.SwigDelegateIAchievementChangeListener_0 swigDelegate0;

		// Token: 0x040000CF RID: 207
		private static Type[] swigMethodTypes0 = new Type[] { typeof(string) };

		// Token: 0x020000A9 RID: 169
		// (Invoke) Token: 0x06000865 RID: 2149
		public delegate void SwigDelegateIAchievementChangeListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);
	}
}
