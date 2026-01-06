using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000069 RID: 105
	public abstract class GameServerGlobalUserInformationRetrieveListener : IUserInformationRetrieveListener
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x0000C194 File Offset: 0x0000A394
		internal GameServerGlobalUserInformationRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalUserInformationRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GameServerGlobalUserInformationRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		public GameServerGlobalUserInformationRetrieveListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000C1DE File Offset: 0x0000A3DE
		internal static HandleRef getCPtr(GameServerGlobalUserInformationRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		~GameServerGlobalUserInformationRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000C22C File Offset: 0x0000A42C
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalUserInformationRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GameServerGlobalUserInformationRetrieveListener.listeners.ContainsKey(handle))
					{
						GameServerGlobalUserInformationRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007D RID: 125
		private static Dictionary<IntPtr, GameServerGlobalUserInformationRetrieveListener> listeners = new Dictionary<IntPtr, GameServerGlobalUserInformationRetrieveListener>();

		// Token: 0x0400007E RID: 126
		private HandleRef swigCPtr;
	}
}
