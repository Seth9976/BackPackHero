using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000DA RID: 218
	public class ICustomNetworking : IDisposable
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x000180B6 File Offset: 0x000162B6
		internal ICustomNetworking(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000180D2 File Offset: 0x000162D2
		internal static HandleRef getCPtr(ICustomNetworking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000180F0 File Offset: 0x000162F0
		~ICustomNetworking()
		{
			this.Dispose();
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00018120 File Offset: 0x00016320
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICustomNetworking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000181A0 File Offset: 0x000163A0
		public virtual void OpenConnection(string connectionString, IConnectionOpenListener listener)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_OpenConnection__SWIG_0(this.swigCPtr, connectionString, IConnectionOpenListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000181C4 File Offset: 0x000163C4
		public virtual void OpenConnection(string connectionString)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_OpenConnection__SWIG_1(this.swigCPtr, connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000181E2 File Offset: 0x000163E2
		public virtual void CloseConnection(ulong connectionID, IConnectionCloseListener listener)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_CloseConnection__SWIG_0(this.swigCPtr, connectionID, IConnectionCloseListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00018206 File Offset: 0x00016406
		public virtual void CloseConnection(ulong connectionID)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_CloseConnection__SWIG_1(this.swigCPtr, connectionID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00018224 File Offset: 0x00016424
		public virtual void SendData(ulong connectionID, byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_SendData(this.swigCPtr, connectionID, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00018244 File Offset: 0x00016444
		public virtual uint GetAvailableDataSize(ulong connectionID)
		{
			uint num = GalaxyInstancePINVOKE.ICustomNetworking_GetAvailableDataSize(this.swigCPtr, connectionID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001826F File Offset: 0x0001646F
		public virtual void PeekData(ulong connectionID, byte[] dest, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_PeekData(this.swigCPtr, connectionID, dest, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001828F File Offset: 0x0001648F
		public virtual void ReadData(ulong connectionID, byte[] dest, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_ReadData(this.swigCPtr, connectionID, dest, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000182AF File Offset: 0x000164AF
		public virtual void PopData(ulong connectionID, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_PopData(this.swigCPtr, connectionID, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0400015D RID: 349
		private HandleRef swigCPtr;

		// Token: 0x0400015E RID: 350
		protected bool swigCMemOwn;
	}
}
