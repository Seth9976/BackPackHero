using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x020000FE RID: 254
	public class IFriends : IDisposable
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00018472 File Offset: 0x00016672
		internal IFriends(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001848E File Offset: 0x0001668E
		internal static HandleRef getCPtr(IFriends obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000184AC File Offset: 0x000166AC
		~IFriends()
		{
			this.Dispose();
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000184DC File Offset: 0x000166DC
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriends(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001855C File Offset: 0x0001675C
		public virtual uint GetDefaultAvatarCriteria()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetDefaultAvatarCriteria(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00018586 File Offset: 0x00016786
		public virtual void SetDefaultAvatarCriteria(uint defaultAvatarCriteria)
		{
			GalaxyInstancePINVOKE.IFriends_SetDefaultAvatarCriteria(this.swigCPtr, defaultAvatarCriteria);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000185A4 File Offset: 0x000167A4
		public virtual void RequestUserInformation(GalaxyID userID, uint avatarCriteria, IUserInformationRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), avatarCriteria, IUserInformationRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000185CE File Offset: 0x000167CE
		public virtual void RequestUserInformation(GalaxyID userID, uint avatarCriteria)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID), avatarCriteria);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000185F2 File Offset: 0x000167F2
		public virtual void RequestUserInformation(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_2(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00018618 File Offset: 0x00016818
		public virtual bool IsUserInformationAvailable(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsUserInformationAvailable(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00018648 File Offset: 0x00016848
		public virtual string GetPersonaName()
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetPersonaName(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00018674 File Offset: 0x00016874
		public virtual void GetPersonaNameCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetPersonaNameCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000A4E RID: 2638 RVA: 0x000186CC File Offset: 0x000168CC
		public virtual PersonaState GetPersonaState()
		{
			PersonaState personaState = (PersonaState)GalaxyInstancePINVOKE.IFriends_GetPersonaState(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return personaState;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000186F8 File Offset: 0x000168F8
		public virtual string GetFriendPersonaName(GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetFriendPersonaName(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00018728 File Offset: 0x00016928
		public virtual void GetFriendPersonaNameCopy(GalaxyID userID, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetFriendPersonaNameCopy(this.swigCPtr, GalaxyID.getCPtr(userID), array, bufferLength);
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

		// Token: 0x06000A51 RID: 2641 RVA: 0x00018784 File Offset: 0x00016984
		public virtual PersonaState GetFriendPersonaState(GalaxyID userID)
		{
			PersonaState personaState = (PersonaState)GalaxyInstancePINVOKE.IFriends_GetFriendPersonaState(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return personaState;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000187B4 File Offset: 0x000169B4
		public virtual string GetFriendAvatarUrl(GalaxyID userID, AvatarType avatarType)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetFriendAvatarUrl(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000187E8 File Offset: 0x000169E8
		public virtual void GetFriendAvatarUrlCopy(GalaxyID userID, AvatarType avatarType, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetFriendAvatarUrlCopy(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType, array, bufferLength);
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

		// Token: 0x06000A54 RID: 2644 RVA: 0x00018848 File Offset: 0x00016A48
		public virtual uint GetFriendAvatarImageID(GalaxyID userID, AvatarType avatarType)
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetFriendAvatarImageID(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00018879 File Offset: 0x00016A79
		public virtual void GetFriendAvatarImageRGBA(GalaxyID userID, AvatarType avatarType, byte[] buffer, uint bufferLength)
		{
			GalaxyInstancePINVOKE.IFriends_GetFriendAvatarImageRGBA(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType, buffer, bufferLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000188A0 File Offset: 0x00016AA0
		public virtual bool IsFriendAvatarImageRGBAAvailable(GalaxyID userID, AvatarType avatarType)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsFriendAvatarImageRGBAAvailable(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000188D1 File Offset: 0x00016AD1
		public virtual void RequestFriendList(IFriendListListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendList__SWIG_0(this.swigCPtr, IFriendListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000188F4 File Offset: 0x00016AF4
		public virtual void RequestFriendList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendList__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00018914 File Offset: 0x00016B14
		public virtual bool IsFriend(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsFriend(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00018944 File Offset: 0x00016B44
		public virtual uint GetFriendCount()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetFriendCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00018970 File Offset: 0x00016B70
		public virtual GalaxyID GetFriendByIndex(uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IFriends_GetFriendByIndex(this.swigCPtr, index);
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

		// Token: 0x06000A5C RID: 2652 RVA: 0x000189B5 File Offset: 0x00016BB5
		public virtual void SendFriendInvitation(GalaxyID userID, IFriendInvitationSendListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SendFriendInvitation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IFriendInvitationSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000189DE File Offset: 0x00016BDE
		public virtual void SendFriendInvitation(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_SendFriendInvitation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00018A01 File Offset: 0x00016C01
		public virtual void RequestFriendInvitationList(IFriendInvitationListRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendInvitationList__SWIG_0(this.swigCPtr, IFriendInvitationListRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00018A24 File Offset: 0x00016C24
		public virtual void RequestFriendInvitationList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendInvitationList__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00018A41 File Offset: 0x00016C41
		public virtual void RequestSentFriendInvitationList(ISentFriendInvitationListRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestSentFriendInvitationList__SWIG_0(this.swigCPtr, ISentFriendInvitationListRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00018A64 File Offset: 0x00016C64
		public virtual void RequestSentFriendInvitationList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestSentFriendInvitationList__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00018A84 File Offset: 0x00016C84
		public virtual uint GetFriendInvitationCount()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetFriendInvitationCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00018AAE File Offset: 0x00016CAE
		public virtual void GetFriendInvitationByIndex(uint index, ref GalaxyID userID, ref uint sendTime)
		{
			GalaxyInstancePINVOKE.IFriends_GetFriendInvitationByIndex(this.swigCPtr, index, GalaxyID.getCPtr(userID), ref sendTime);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00018AD4 File Offset: 0x00016CD4
		public virtual void RespondToFriendInvitation(GalaxyID userID, bool accept, IFriendInvitationRespondToListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RespondToFriendInvitation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), accept, IFriendInvitationRespondToListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00018AFE File Offset: 0x00016CFE
		public virtual void RespondToFriendInvitation(GalaxyID userID, bool accept)
		{
			GalaxyInstancePINVOKE.IFriends_RespondToFriendInvitation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID), accept);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00018B22 File Offset: 0x00016D22
		public virtual void DeleteFriend(GalaxyID userID, IFriendDeleteListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteFriend__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IFriendDeleteListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00018B4B File Offset: 0x00016D4B
		public virtual void DeleteFriend(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteFriend__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00018B6E File Offset: 0x00016D6E
		public virtual void SetRichPresence(string key, string value, IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SetRichPresence__SWIG_0(this.swigCPtr, key, value, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00018B93 File Offset: 0x00016D93
		public virtual void SetRichPresence(string key, string value)
		{
			GalaxyInstancePINVOKE.IFriends_SetRichPresence__SWIG_1(this.swigCPtr, key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00018BB2 File Offset: 0x00016DB2
		public virtual void DeleteRichPresence(string key, IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteRichPresence__SWIG_0(this.swigCPtr, key, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00018BD6 File Offset: 0x00016DD6
		public virtual void DeleteRichPresence(string key)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteRichPresence__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public virtual void ClearRichPresence(IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_ClearRichPresence__SWIG_0(this.swigCPtr, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00018C17 File Offset: 0x00016E17
		public virtual void ClearRichPresence()
		{
			GalaxyInstancePINVOKE.IFriends_ClearRichPresence__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00018C34 File Offset: 0x00016E34
		public virtual void RequestRichPresence(GalaxyID userID, IRichPresenceRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IRichPresenceRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00018C5D File Offset: 0x00016E5D
		public virtual void RequestRichPresence(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00018C80 File Offset: 0x00016E80
		public virtual void RequestRichPresence()
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00018CA0 File Offset: 0x00016EA0
		public virtual string GetRichPresence(string key, GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresence__SWIG_0(this.swigCPtr, key, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00018CD4 File Offset: 0x00016ED4
		public virtual string GetRichPresence(string key)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresence__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00018D00 File Offset: 0x00016F00
		public virtual void GetRichPresenceCopy(string key, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceCopy__SWIG_0(this.swigCPtr, key, array, bufferLength, GalaxyID.getCPtr(userID));
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

		// Token: 0x06000A74 RID: 2676 RVA: 0x00018D60 File Offset: 0x00016F60
		public virtual void GetRichPresenceCopy(string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceCopy__SWIG_1(this.swigCPtr, key, array, bufferLength);
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

		// Token: 0x06000A75 RID: 2677 RVA: 0x00018DB8 File Offset: 0x00016FB8
		public virtual uint GetRichPresenceCount(GalaxyID userID)
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetRichPresenceCount__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00018DE8 File Offset: 0x00016FE8
		public virtual uint GetRichPresenceCount()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetRichPresenceCount__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00018E12 File Offset: 0x00017012
		public virtual void GetRichPresenceByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_GetRichPresenceByIndex__SWIG_0(this.swigCPtr, index, key, keyLength, value, valueLength, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00018E3F File Offset: 0x0001703F
		public virtual void GetRichPresenceByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			GalaxyInstancePINVOKE.IFriends_GetRichPresenceByIndex__SWIG_1(this.swigCPtr, index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00018E68 File Offset: 0x00017068
		public virtual string GetRichPresenceKeyByIndex(uint index, GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndex__SWIG_0(this.swigCPtr, index, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00018E9C File Offset: 0x0001709C
		public virtual string GetRichPresenceKeyByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndex__SWIG_1(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00018EC8 File Offset: 0x000170C8
		public virtual void GetRichPresenceKeyByIndexCopy(uint index, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndexCopy__SWIG_0(this.swigCPtr, index, array, bufferLength, GalaxyID.getCPtr(userID));
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

		// Token: 0x06000A7C RID: 2684 RVA: 0x00018F28 File Offset: 0x00017128
		public virtual void GetRichPresenceKeyByIndexCopy(uint index, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndexCopy__SWIG_1(this.swigCPtr, index, array, bufferLength);
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

		// Token: 0x06000A7D RID: 2685 RVA: 0x00018F80 File Offset: 0x00017180
		public virtual void ShowOverlayInviteDialog(string connectionString)
		{
			GalaxyInstancePINVOKE.IFriends_ShowOverlayInviteDialog(this.swigCPtr, connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00018F9E File Offset: 0x0001719E
		public virtual void SendInvitation(GalaxyID userID, string connectionString, ISendInvitationListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SendInvitation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), connectionString, ISendInvitationListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00018FC8 File Offset: 0x000171C8
		public virtual void SendInvitation(GalaxyID userID, string connectionString)
		{
			GalaxyInstancePINVOKE.IFriends_SendInvitation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID), connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00018FEC File Offset: 0x000171EC
		public virtual void FindUser(string userSpecifier, IUserFindListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_FindUser__SWIG_0(this.swigCPtr, userSpecifier, IUserFindListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00019010 File Offset: 0x00017210
		public virtual void FindUser(string userSpecifier)
		{
			GalaxyInstancePINVOKE.IFriends_FindUser__SWIG_1(this.swigCPtr, userSpecifier);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00019030 File Offset: 0x00017230
		public virtual bool IsUserInTheSameGame(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsUserInTheSameGame(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x040001B6 RID: 438
		private HandleRef swigCPtr;

		// Token: 0x040001B7 RID: 439
		protected bool swigCMemOwn;
	}
}
