using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x0200013B RID: 315
	public class IMatchmaking : IDisposable
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x000197BC File Offset: 0x000179BC
		internal IMatchmaking(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000197D8 File Offset: 0x000179D8
		internal static HandleRef getCPtr(IMatchmaking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000197F8 File Offset: 0x000179F8
		~IMatchmaking()
		{
			this.Dispose();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00019828 File Offset: 0x00017A28
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IMatchmaking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000198A8 File Offset: 0x00017AA8
		public bool SendLobbyMessage(GalaxyID lobbyID, string msg)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(msg);
			return this.SendLobbyMessage(lobbyID, bytes, (uint)bytes.Length);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000198CC File Offset: 0x00017ACC
		public uint GetLobbyMessage(GalaxyID lobbyID, uint messageID, ref GalaxyID _senderID, out string msg, uint internalBufferLen = 1024U)
		{
			byte[] array = new byte[internalBufferLen];
			GalaxyID galaxyID = new GalaxyID();
			uint lobbyMessage = this.GetLobbyMessage(lobbyID, messageID, ref galaxyID, ref array, (uint)array.Length);
			msg = Encoding.UTF8.GetString(array, 0, (int)lobbyMessage);
			return lobbyMessage;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00019909 File Offset: 0x00017B09
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType, ILobbyCreatedListener lobbyCreatedListener, ILobbyEnteredListener lobbyEnteredListener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_0(this.swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType, ILobbyCreatedListener.getCPtr(lobbyCreatedListener), ILobbyEnteredListener.getCPtr(lobbyEnteredListener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00019939 File Offset: 0x00017B39
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType, ILobbyCreatedListener lobbyCreatedListener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_1(this.swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType, ILobbyCreatedListener.getCPtr(lobbyCreatedListener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00019962 File Offset: 0x00017B62
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_2(this.swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00019984 File Offset: 0x00017B84
		public virtual void RequestLobbyList(bool allowFullLobbies, ILobbyListListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_0(this.swigCPtr, allowFullLobbies, ILobbyListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000199A8 File Offset: 0x00017BA8
		public virtual void RequestLobbyList(bool allowFullLobbies)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_1(this.swigCPtr, allowFullLobbies);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000199C6 File Offset: 0x00017BC6
		public virtual void RequestLobbyList()
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000199E3 File Offset: 0x00017BE3
		public virtual void AddRequestLobbyListResultCountFilter(uint limit)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListResultCountFilter(this.swigCPtr, limit);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00019A01 File Offset: 0x00017C01
		public virtual void AddRequestLobbyListStringFilter(string keyToMatch, string valueToMatch, LobbyComparisonType comparisonType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListStringFilter(this.swigCPtr, keyToMatch, valueToMatch, (int)comparisonType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00019A21 File Offset: 0x00017C21
		public virtual void AddRequestLobbyListNumericalFilter(string keyToMatch, int valueToMatch, LobbyComparisonType comparisonType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListNumericalFilter(this.swigCPtr, keyToMatch, valueToMatch, (int)comparisonType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00019A41 File Offset: 0x00017C41
		public virtual void AddRequestLobbyListNearValueFilter(string keyToMatch, int valueToBeCloseTo)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListNearValueFilter(this.swigCPtr, keyToMatch, valueToBeCloseTo);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00019A60 File Offset: 0x00017C60
		public virtual GalaxyID GetLobbyByIndex(uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID galaxyID = null;
			if (intPtr != IntPtr.Zero)
			{
				galaxyID = new GalaxyID(intPtr, true);
			}
			return galaxyID;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00019AA5 File Offset: 0x00017CA5
		public virtual void JoinLobby(GalaxyID lobbyID, ILobbyEnteredListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_JoinLobby__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyEnteredListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00019ACE File Offset: 0x00017CCE
		public virtual void JoinLobby(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_JoinLobby__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00019AF1 File Offset: 0x00017CF1
		public virtual void LeaveLobby(GalaxyID lobbyID, ILobbyLeftListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_LeaveLobby__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyLeftListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00019B1A File Offset: 0x00017D1A
		public virtual void LeaveLobby(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_LeaveLobby__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00019B3D File Offset: 0x00017D3D
		public virtual void SetMaxNumLobbyMembers(GalaxyID lobbyID, uint maxNumLobbyMembers, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetMaxNumLobbyMembers__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), maxNumLobbyMembers, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00019B67 File Offset: 0x00017D67
		public virtual void SetMaxNumLobbyMembers(GalaxyID lobbyID, uint maxNumLobbyMembers)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetMaxNumLobbyMembers__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), maxNumLobbyMembers);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00019B8C File Offset: 0x00017D8C
		public virtual uint GetMaxNumLobbyMembers(GalaxyID lobbyID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetMaxNumLobbyMembers(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00019BBC File Offset: 0x00017DBC
		public virtual uint GetNumLobbyMembers(GalaxyID lobbyID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetNumLobbyMembers(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00019BEC File Offset: 0x00017DEC
		public virtual GalaxyID GetLobbyMemberByIndex(GalaxyID lobbyID, uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberByIndex(this.swigCPtr, GalaxyID.getCPtr(lobbyID), index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID galaxyID = null;
			if (intPtr != IntPtr.Zero)
			{
				galaxyID = new GalaxyID(intPtr, true);
			}
			return galaxyID;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00019C37 File Offset: 0x00017E37
		public virtual void SetLobbyType(GalaxyID lobbyID, LobbyType lobbyType, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyType__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), (int)lobbyType, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00019C61 File Offset: 0x00017E61
		public virtual void SetLobbyType(GalaxyID lobbyID, LobbyType lobbyType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyType__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), (int)lobbyType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00019C88 File Offset: 0x00017E88
		public virtual LobbyType GetLobbyType(GalaxyID lobbyID)
		{
			LobbyType lobbyType = (LobbyType)GalaxyInstancePINVOKE.IMatchmaking_GetLobbyType(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return lobbyType;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00019CB8 File Offset: 0x00017EB8
		public virtual void SetLobbyJoinable(GalaxyID lobbyID, bool joinable, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyJoinable__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), joinable, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00019CE2 File Offset: 0x00017EE2
		public virtual void SetLobbyJoinable(GalaxyID lobbyID, bool joinable)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyJoinable__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), joinable);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00019D08 File Offset: 0x00017F08
		public virtual bool IsLobbyJoinable(GalaxyID lobbyID)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_IsLobbyJoinable(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00019D38 File Offset: 0x00017F38
		public virtual void RequestLobbyData(GalaxyID lobbyID, ILobbyDataRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyDataRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00019D61 File Offset: 0x00017F61
		public virtual void RequestLobbyData(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00019D84 File Offset: 0x00017F84
		public virtual string GetLobbyData(GalaxyID lobbyID, string key)
		{
			string text = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyData(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00019DB8 File Offset: 0x00017FB8
		public virtual void GetLobbyDataCopy(GalaxyID lobbyID, string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataCopy(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00019E18 File Offset: 0x00018018
		public virtual void SetLobbyData(GalaxyID lobbyID, string key, string value, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00019E44 File Offset: 0x00018044
		public virtual void SetLobbyData(GalaxyID lobbyID, string key, string value)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00019E6C File Offset: 0x0001806C
		public virtual uint GetLobbyDataCount(GalaxyID lobbyID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataCount(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00019E9C File Offset: 0x0001809C
		public virtual bool GetLobbyDataByIndex(GalaxyID lobbyID, uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataByIndex(this.swigCPtr, GalaxyID.getCPtr(lobbyID), index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00019ED6 File Offset: 0x000180D6
		public virtual void DeleteLobbyData(GalaxyID lobbyID, string key, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00019F00 File Offset: 0x00018100
		public virtual void DeleteLobbyData(GalaxyID lobbyID, string key)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00019F24 File Offset: 0x00018124
		public virtual string GetLobbyMemberData(GalaxyID lobbyID, GalaxyID memberID, string key)
		{
			string text = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberData(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00019F5C File Offset: 0x0001815C
		public virtual void GetLobbyMemberDataCopy(GalaxyID lobbyID, GalaxyID memberID, string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataCopy(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), key, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00019FC4 File Offset: 0x000181C4
		public virtual void SetLobbyMemberData(GalaxyID lobbyID, string key, string value, ILobbyMemberDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyMemberData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value, ILobbyMemberDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00019FF0 File Offset: 0x000181F0
		public virtual void SetLobbyMemberData(GalaxyID lobbyID, string key, string value)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyMemberData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001A018 File Offset: 0x00018218
		public virtual uint GetLobbyMemberDataCount(GalaxyID lobbyID, GalaxyID memberID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataCount(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001A050 File Offset: 0x00018250
		public virtual bool GetLobbyMemberDataByIndex(GalaxyID lobbyID, GalaxyID memberID, uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataByIndex(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0001A091 File Offset: 0x00018291
		public virtual void DeleteLobbyMemberData(GalaxyID lobbyID, string key, ILobbyMemberDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyMemberData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, ILobbyMemberDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001A0BB File Offset: 0x000182BB
		public virtual void DeleteLobbyMemberData(GalaxyID lobbyID, string key)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyMemberData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001A0E0 File Offset: 0x000182E0
		public virtual GalaxyID GetLobbyOwner(GalaxyID lobbyID)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyOwner(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID galaxyID = null;
			if (intPtr != IntPtr.Zero)
			{
				galaxyID = new GalaxyID(intPtr, true);
			}
			return galaxyID;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001A12C File Offset: 0x0001832C
		public virtual bool SendLobbyMessage(GalaxyID lobbyID, byte[] data, uint dataSize)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_SendLobbyMessage(this.swigCPtr, GalaxyID.getCPtr(lobbyID), data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001A160 File Offset: 0x00018360
		public virtual uint GetLobbyMessage(GalaxyID lobbyID, uint messageID, ref GalaxyID senderID, ref byte[] msg, uint msgLength)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMessage(this.swigCPtr, GalaxyID.getCPtr(lobbyID), messageID, GalaxyID.getCPtr(senderID), msg, msgLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x04000241 RID: 577
		private HandleRef swigCPtr;

		// Token: 0x04000242 RID: 578
		protected bool swigCMemOwn;
	}
}
