using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x0200017A RID: 378
	public class IUser : IDisposable
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0001BF68 File Offset: 0x0001A168
		internal IUser(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0001BF84 File Offset: 0x0001A184
		internal static HandleRef getCPtr(IUser obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
		~IUser()
		{
			this.Dispose();
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUser(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0001C054 File Offset: 0x0001A254
		public virtual bool SignedIn()
		{
			bool flag = GalaxyInstancePINVOKE.IUser_SignedIn(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0001C080 File Offset: 0x0001A280
		public virtual GalaxyID GetGalaxyID()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IUser_GetGalaxyID(this.swigCPtr);
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

		// Token: 0x06000DDC RID: 3548 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		public virtual void SignInCredentials(string login, string password, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInCredentials__SWIG_0(this.swigCPtr, login, password, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0001C0E9 File Offset: 0x0001A2E9
		public virtual void SignInCredentials(string login, string password)
		{
			GalaxyInstancePINVOKE.IUser_SignInCredentials__SWIG_1(this.swigCPtr, login, password);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0001C108 File Offset: 0x0001A308
		public virtual void SignInToken(string refreshToken, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInToken__SWIG_0(this.swigCPtr, refreshToken, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0001C12C File Offset: 0x0001A32C
		public virtual void SignInToken(string refreshToken)
		{
			GalaxyInstancePINVOKE.IUser_SignInToken__SWIG_1(this.swigCPtr, refreshToken);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0001C14A File Offset: 0x0001A34A
		public virtual void SignInLauncher(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInLauncher__SWIG_0(this.swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0001C16D File Offset: 0x0001A36D
		public virtual void SignInLauncher()
		{
			GalaxyInstancePINVOKE.IUser_SignInLauncher__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0001C18A File Offset: 0x0001A38A
		public virtual void SignInSteam(byte[] steamAppTicket, uint steamAppTicketSize, string personaName, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInSteam__SWIG_0(this.swigCPtr, steamAppTicket, steamAppTicketSize, personaName, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0001C1B1 File Offset: 0x0001A3B1
		public virtual void SignInSteam(byte[] steamAppTicket, uint steamAppTicketSize, string personaName)
		{
			GalaxyInstancePINVOKE.IUser_SignInSteam__SWIG_1(this.swigCPtr, steamAppTicket, steamAppTicketSize, personaName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0001C1D1 File Offset: 0x0001A3D1
		public virtual void SignInGalaxy(bool requireOnline, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_0(this.swigCPtr, requireOnline, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0001C1F5 File Offset: 0x0001A3F5
		public virtual void SignInGalaxy(bool requireOnline)
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_1(this.swigCPtr, requireOnline);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001C213 File Offset: 0x0001A413
		public virtual void SignInGalaxy()
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0001C230 File Offset: 0x0001A430
		public virtual void SignInPS4(string ps4ClientID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInPS4__SWIG_0(this.swigCPtr, ps4ClientID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001C254 File Offset: 0x0001A454
		public virtual void SignInPS4(string ps4ClientID)
		{
			GalaxyInstancePINVOKE.IUser_SignInPS4__SWIG_1(this.swigCPtr, ps4ClientID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001C272 File Offset: 0x0001A472
		public virtual void SignInXB1(string xboxOneUserID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXB1__SWIG_0(this.swigCPtr, xboxOneUserID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0001C296 File Offset: 0x0001A496
		public virtual void SignInXB1(string xboxOneUserID)
		{
			GalaxyInstancePINVOKE.IUser_SignInXB1__SWIG_1(this.swigCPtr, xboxOneUserID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		public virtual void SignInXbox(ulong xboxID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXbox__SWIG_0(this.swigCPtr, xboxID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		public virtual void SignInXbox(ulong xboxID)
		{
			GalaxyInstancePINVOKE.IUser_SignInXbox__SWIG_1(this.swigCPtr, xboxID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001C2F6 File Offset: 0x0001A4F6
		public virtual void SignInXBLive(string token, string signature, string marketplaceID, string locale, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXBLive__SWIG_0(this.swigCPtr, token, signature, marketplaceID, locale, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001C31F File Offset: 0x0001A51F
		public virtual void SignInXBLive(string token, string signature, string marketplaceID, string locale)
		{
			GalaxyInstancePINVOKE.IUser_SignInXBLive__SWIG_1(this.swigCPtr, token, signature, marketplaceID, locale);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0001C341 File Offset: 0x0001A541
		public virtual void SignInAnonymous(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymous__SWIG_0(this.swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001C364 File Offset: 0x0001A564
		public virtual void SignInAnonymous()
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymous__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001C381 File Offset: 0x0001A581
		public virtual void SignInAnonymousTelemetry(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymousTelemetry__SWIG_0(this.swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		public virtual void SignInAnonymousTelemetry()
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymousTelemetry__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0001C3C1 File Offset: 0x0001A5C1
		public virtual void SignInServerKey(string serverKey, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInServerKey__SWIG_0(this.swigCPtr, serverKey, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0001C3E5 File Offset: 0x0001A5E5
		public virtual void SignInServerKey(string serverKey)
		{
			GalaxyInstancePINVOKE.IUser_SignInServerKey__SWIG_1(this.swigCPtr, serverKey);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0001C403 File Offset: 0x0001A603
		public virtual void SignInAuthorizationCode(string authorizationCode, string redirectURI, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAuthorizationCode__SWIG_0(this.swigCPtr, authorizationCode, redirectURI, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001C428 File Offset: 0x0001A628
		public virtual void SignInAuthorizationCode(string authorizationCode, string redirectURI)
		{
			GalaxyInstancePINVOKE.IUser_SignInAuthorizationCode__SWIG_1(this.swigCPtr, authorizationCode, redirectURI);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0001C447 File Offset: 0x0001A647
		public virtual void SignOut()
		{
			GalaxyInstancePINVOKE.IUser_SignOut(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0001C464 File Offset: 0x0001A664
		public virtual void RequestUserData(GalaxyID userID, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0001C48D File Offset: 0x0001A68D
		public virtual void RequestUserData(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		public virtual void RequestUserData()
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		public virtual bool IsUserDataAvailable(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_IsUserDataAvailable__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0001C500 File Offset: 0x0001A700
		public virtual bool IsUserDataAvailable()
		{
			bool flag = GalaxyInstancePINVOKE.IUser_IsUserDataAvailable__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0001C52C File Offset: 0x0001A72C
		public virtual string GetUserData(string key, GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IUser_GetUserData__SWIG_0(this.swigCPtr, key, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0001C560 File Offset: 0x0001A760
		public virtual string GetUserData(string key)
		{
			string text = GalaxyInstancePINVOKE.IUser_GetUserData__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0001C58C File Offset: 0x0001A78C
		public virtual void GetUserDataCopy(string key, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetUserDataCopy__SWIG_0(this.swigCPtr, key, array, bufferLength, GalaxyID.getCPtr(userID));
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

		// Token: 0x06000E00 RID: 3584 RVA: 0x0001C5EC File Offset: 0x0001A7EC
		public virtual void GetUserDataCopy(string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetUserDataCopy__SWIG_1(this.swigCPtr, key, array, bufferLength);
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

		// Token: 0x06000E01 RID: 3585 RVA: 0x0001C644 File Offset: 0x0001A844
		public virtual void SetUserData(string key, string value, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SetUserData__SWIG_0(this.swigCPtr, key, value, ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0001C669 File Offset: 0x0001A869
		public virtual void SetUserData(string key, string value)
		{
			GalaxyInstancePINVOKE.IUser_SetUserData__SWIG_1(this.swigCPtr, key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0001C688 File Offset: 0x0001A888
		public virtual uint GetUserDataCount(GalaxyID userID)
		{
			uint num = GalaxyInstancePINVOKE.IUser_GetUserDataCount__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		public virtual uint GetUserDataCount()
		{
			uint num = GalaxyInstancePINVOKE.IUser_GetUserDataCount__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
		public virtual bool GetUserDataByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength, GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_GetUserDataByIndex__SWIG_0(this.swigCPtr, index, key, keyLength, value, valueLength, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0001C720 File Offset: 0x0001A920
		public virtual bool GetUserDataByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_GetUserDataByIndex__SWIG_1(this.swigCPtr, index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0001C753 File Offset: 0x0001A953
		public virtual void DeleteUserData(string key, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_DeleteUserData__SWIG_0(this.swigCPtr, key, ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0001C777 File Offset: 0x0001A977
		public virtual void DeleteUserData(string key)
		{
			GalaxyInstancePINVOKE.IUser_DeleteUserData__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0001C798 File Offset: 0x0001A998
		public virtual bool IsLoggedOn()
		{
			bool flag = GalaxyInstancePINVOKE.IUser_IsLoggedOn(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0001C7C2 File Offset: 0x0001A9C2
		public virtual void RequestEncryptedAppTicket(byte[] data, uint dataSize, IEncryptedAppTicketListener listener)
		{
			GalaxyInstancePINVOKE.IUser_RequestEncryptedAppTicket__SWIG_0(this.swigCPtr, data, dataSize, IEncryptedAppTicketListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0001C7E7 File Offset: 0x0001A9E7
		public virtual void RequestEncryptedAppTicket(byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.IUser_RequestEncryptedAppTicket__SWIG_1(this.swigCPtr, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0001C806 File Offset: 0x0001AA06
		public virtual void GetEncryptedAppTicket(byte[] encryptedAppTicket, uint maxEncryptedAppTicketSize, ref uint currentEncryptedAppTicketSize)
		{
			GalaxyInstancePINVOKE.IUser_GetEncryptedAppTicket(this.swigCPtr, encryptedAppTicket, maxEncryptedAppTicketSize, ref currentEncryptedAppTicketSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0001C826 File Offset: 0x0001AA26
		public virtual void CreateOpenIDConnection(string secretKey, string titleID, string connectionID, bool ignoreNonce, IPlayFabCreateOpenIDConnectionListener listener)
		{
			GalaxyInstancePINVOKE.IUser_CreateOpenIDConnection__SWIG_0(this.swigCPtr, secretKey, titleID, connectionID, ignoreNonce, IPlayFabCreateOpenIDConnectionListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0001C84F File Offset: 0x0001AA4F
		public virtual void CreateOpenIDConnection(string secretKey, string titleID, string connectionID, bool ignoreNonce)
		{
			GalaxyInstancePINVOKE.IUser_CreateOpenIDConnection__SWIG_1(this.swigCPtr, secretKey, titleID, connectionID, ignoreNonce);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0001C871 File Offset: 0x0001AA71
		public virtual void CreateOpenIDConnection(string secretKey, string titleID, string connectionID)
		{
			GalaxyInstancePINVOKE.IUser_CreateOpenIDConnection__SWIG_2(this.swigCPtr, secretKey, titleID, connectionID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0001C891 File Offset: 0x0001AA91
		public virtual void LoginWithOpenIDConnect(string titleID, string connectionID, string idToken, bool createAccount, string encryptedRequest, string playerSecret, IPlayFabLoginWithOpenIDConnectListener listener)
		{
			GalaxyInstancePINVOKE.IUser_LoginWithOpenIDConnect__SWIG_0(this.swigCPtr, titleID, connectionID, idToken, createAccount, encryptedRequest, playerSecret, IPlayFabLoginWithOpenIDConnectListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0001C8BE File Offset: 0x0001AABE
		public virtual void LoginWithOpenIDConnect(string titleID, string connectionID, string idToken, bool createAccount, string encryptedRequest, string playerSecret)
		{
			GalaxyInstancePINVOKE.IUser_LoginWithOpenIDConnect__SWIG_1(this.swigCPtr, titleID, connectionID, idToken, createAccount, encryptedRequest, playerSecret);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
		public virtual void LoginWithOpenIDConnect(string titleID, string connectionID, string idToken, bool createAccount, string encryptedRequest)
		{
			GalaxyInstancePINVOKE.IUser_LoginWithOpenIDConnect__SWIG_2(this.swigCPtr, titleID, connectionID, idToken, createAccount, encryptedRequest);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0001C908 File Offset: 0x0001AB08
		public virtual void LoginWithOpenIDConnect(string titleID, string connectionID, string idToken, bool createAccount)
		{
			GalaxyInstancePINVOKE.IUser_LoginWithOpenIDConnect__SWIG_3(this.swigCPtr, titleID, connectionID, idToken, createAccount);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0001C92A File Offset: 0x0001AB2A
		public virtual void LoginWithOpenIDConnect(string titleID, string connectionID, string idToken)
		{
			GalaxyInstancePINVOKE.IUser_LoginWithOpenIDConnect__SWIG_4(this.swigCPtr, titleID, connectionID, idToken);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0001C94C File Offset: 0x0001AB4C
		public virtual ulong GetSessionID()
		{
			ulong num = GalaxyInstancePINVOKE.IUser_GetSessionID(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0001C978 File Offset: 0x0001AB78
		public virtual string GetAccessToken()
		{
			string text = GalaxyInstancePINVOKE.IUser_GetAccessToken(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		public virtual void GetAccessTokenCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetAccessTokenCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000E18 RID: 3608 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		public virtual string GetRefreshToken()
		{
			string text = GalaxyInstancePINVOKE.IUser_GetRefreshToken(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public virtual void GetRefreshTokenCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetRefreshTokenCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000E1A RID: 3610 RVA: 0x0001CA80 File Offset: 0x0001AC80
		public virtual string GetIDToken()
		{
			string text = GalaxyInstancePINVOKE.IUser_GetIDToken(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		public virtual void GetIDTokenCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetIDTokenCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000E1C RID: 3612 RVA: 0x0001CB04 File Offset: 0x0001AD04
		public virtual bool ReportInvalidAccessToken(string accessToken, string info)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_ReportInvalidAccessToken__SWIG_0(this.swigCPtr, accessToken, info);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001CB30 File Offset: 0x0001AD30
		public virtual bool ReportInvalidAccessToken(string accessToken)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_ReportInvalidAccessToken__SWIG_1(this.swigCPtr, accessToken);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x040002DD RID: 733
		private HandleRef swigCPtr;

		// Token: 0x040002DE RID: 734
		protected bool swigCMemOwn;
	}
}
