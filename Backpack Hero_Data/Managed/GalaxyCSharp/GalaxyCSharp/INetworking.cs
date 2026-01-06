using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200013F RID: 319
	public class INetworking : IDisposable
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x0001A19D File Offset: 0x0001839D
		internal INetworking(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0001A1B9 File Offset: 0x000183B9
		internal static HandleRef getCPtr(INetworking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0001A1D8 File Offset: 0x000183D8
		~INetworking()
		{
			this.Dispose();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0001A208 File Offset: 0x00018408
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INetworking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0001A288 File Offset: 0x00018488
		public virtual bool SendP2PPacket(GalaxyID galaxyID, byte[] data, uint dataSize, P2PSendType sendType, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_SendP2PPacket__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(galaxyID), data, dataSize, (int)sendType, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0001A2C0 File Offset: 0x000184C0
		public virtual bool SendP2PPacket(GalaxyID galaxyID, byte[] data, uint dataSize, P2PSendType sendType)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_SendP2PPacket__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(galaxyID), data, dataSize, (int)sendType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001A2F4 File Offset: 0x000184F4
		public virtual bool PeekP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_PeekP2PPacket__SWIG_0(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID), channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0001A32C File Offset: 0x0001852C
		public virtual bool PeekP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_PeekP2PPacket__SWIG_1(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0001A364 File Offset: 0x00018564
		public virtual bool IsP2PPacketAvailable(ref uint outMsgSize, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_IsP2PPacketAvailable__SWIG_0(this.swigCPtr, ref outMsgSize, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0001A390 File Offset: 0x00018590
		public virtual bool IsP2PPacketAvailable(ref uint outMsgSize)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_IsP2PPacketAvailable__SWIG_1(this.swigCPtr, ref outMsgSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0001A3BC File Offset: 0x000185BC
		public virtual bool ReadP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_ReadP2PPacket__SWIG_0(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID), channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public virtual bool ReadP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_ReadP2PPacket__SWIG_1(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001A429 File Offset: 0x00018629
		public virtual void PopP2PPacket(byte channel)
		{
			GalaxyInstancePINVOKE.INetworking_PopP2PPacket__SWIG_0(this.swigCPtr, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0001A447 File Offset: 0x00018647
		public virtual void PopP2PPacket()
		{
			GalaxyInstancePINVOKE.INetworking_PopP2PPacket__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0001A464 File Offset: 0x00018664
		public virtual int GetPingWith(GalaxyID galaxyID)
		{
			int num = GalaxyInstancePINVOKE.INetworking_GetPingWith(this.swigCPtr, GalaxyID.getCPtr(galaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001A494 File Offset: 0x00018694
		public virtual void RequestNatTypeDetection()
		{
			GalaxyInstancePINVOKE.INetworking_RequestNatTypeDetection(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public virtual NatType GetNatType()
		{
			NatType natType = (NatType)GalaxyInstancePINVOKE.INetworking_GetNatType(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return natType;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0001A4E0 File Offset: 0x000186E0
		public virtual ConnectionType GetConnectionType(GalaxyID userID)
		{
			ConnectionType connectionType = (ConnectionType)GalaxyInstancePINVOKE.INetworking_GetConnectionType(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return connectionType;
		}

		// Token: 0x04000249 RID: 585
		private HandleRef swigCPtr;

		// Token: 0x0400024A RID: 586
		protected bool swigCMemOwn;
	}
}
