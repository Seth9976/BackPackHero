using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x0200016E RID: 366
	public class IStats : IDisposable
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x0001AC20 File Offset: 0x00018E20
		internal IStats(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0001AC3C File Offset: 0x00018E3C
		internal static HandleRef getCPtr(IStats obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0001AC5C File Offset: 0x00018E5C
		~IStats()
		{
			this.Dispose();
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0001AC8C File Offset: 0x00018E8C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStats(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0001AD0C File Offset: 0x00018F0C
		public virtual void RequestUserStatsAndAchievements(GalaxyID userID, IUserStatsAndAchievementsRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IUserStatsAndAchievementsRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0001AD35 File Offset: 0x00018F35
		public virtual void RequestUserStatsAndAchievements(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0001AD58 File Offset: 0x00018F58
		public virtual void RequestUserStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0001AD78 File Offset: 0x00018F78
		public virtual int GetStatInt(string name, GalaxyID userID)
		{
			int num = GalaxyInstancePINVOKE.IStats_GetStatInt__SWIG_0(this.swigCPtr, name, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001ADAC File Offset: 0x00018FAC
		public virtual int GetStatInt(string name)
		{
			int num = GalaxyInstancePINVOKE.IStats_GetStatInt__SWIG_1(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		public virtual float GetStatFloat(string name, GalaxyID userID)
		{
			float num = GalaxyInstancePINVOKE.IStats_GetStatFloat__SWIG_0(this.swigCPtr, name, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001AE0C File Offset: 0x0001900C
		public virtual float GetStatFloat(string name)
		{
			float num = GalaxyInstancePINVOKE.IStats_GetStatFloat__SWIG_1(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0001AE37 File Offset: 0x00019037
		public virtual void SetStatInt(string name, int value)
		{
			GalaxyInstancePINVOKE.IStats_SetStatInt(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0001AE56 File Offset: 0x00019056
		public virtual void SetStatFloat(string name, float value)
		{
			GalaxyInstancePINVOKE.IStats_SetStatFloat(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001AE75 File Offset: 0x00019075
		public virtual void UpdateAvgRateStat(string name, float countThisSession, double sessionLength)
		{
			GalaxyInstancePINVOKE.IStats_UpdateAvgRateStat(this.swigCPtr, name, countThisSession, sessionLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0001AE95 File Offset: 0x00019095
		public virtual void GetAchievement(string name, ref bool unlocked, ref uint unlockTime, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetAchievement__SWIG_0(this.swigCPtr, name, ref unlocked, ref unlockTime, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0001AEBC File Offset: 0x000190BC
		public virtual void GetAchievement(string name, ref bool unlocked, ref uint unlockTime)
		{
			GalaxyInstancePINVOKE.IStats_GetAchievement__SWIG_1(this.swigCPtr, name, ref unlocked, ref unlockTime);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0001AEDC File Offset: 0x000190DC
		public virtual void SetAchievement(string name)
		{
			GalaxyInstancePINVOKE.IStats_SetAchievement(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0001AEFA File Offset: 0x000190FA
		public virtual void ClearAchievement(string name)
		{
			GalaxyInstancePINVOKE.IStats_ClearAchievement(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001AF18 File Offset: 0x00019118
		public virtual void StoreStatsAndAchievements(IStatsAndAchievementsStoreListener listener)
		{
			GalaxyInstancePINVOKE.IStats_StoreStatsAndAchievements__SWIG_0(this.swigCPtr, IStatsAndAchievementsStoreListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0001AF3B File Offset: 0x0001913B
		public virtual void StoreStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_StoreStatsAndAchievements__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001AF58 File Offset: 0x00019158
		public virtual void ResetStatsAndAchievements(IStatsAndAchievementsStoreListener listener)
		{
			GalaxyInstancePINVOKE.IStats_ResetStatsAndAchievements__SWIG_0(this.swigCPtr, IStatsAndAchievementsStoreListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0001AF7B File Offset: 0x0001917B
		public virtual void ResetStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_ResetStatsAndAchievements__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0001AF98 File Offset: 0x00019198
		public virtual string GetAchievementDisplayName(string name)
		{
			string text = GalaxyInstancePINVOKE.IStats_GetAchievementDisplayName(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0001AFC4 File Offset: 0x000191C4
		public virtual void GetAchievementDisplayNameCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetAchievementDisplayNameCopy(this.swigCPtr, name, array, bufferLength);
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

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001B01C File Offset: 0x0001921C
		public virtual string GetAchievementDescription(string name)
		{
			string text = GalaxyInstancePINVOKE.IStats_GetAchievementDescription(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001B048 File Offset: 0x00019248
		public virtual void GetAchievementDescriptionCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetAchievementDescriptionCopy(this.swigCPtr, name, array, bufferLength);
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

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001B0A0 File Offset: 0x000192A0
		public virtual bool IsAchievementVisible(string name)
		{
			bool flag = GalaxyInstancePINVOKE.IStats_IsAchievementVisible(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0001B0CC File Offset: 0x000192CC
		public virtual bool IsAchievementVisibleWhileLocked(string name)
		{
			bool flag = GalaxyInstancePINVOKE.IStats_IsAchievementVisibleWhileLocked(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001B0F7 File Offset: 0x000192F7
		public virtual void RequestLeaderboards(ILeaderboardsRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboards__SWIG_0(this.swigCPtr, ILeaderboardsRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0001B11A File Offset: 0x0001931A
		public virtual void RequestLeaderboards()
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboards__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0001B138 File Offset: 0x00019338
		public virtual string GetLeaderboardDisplayName(string name)
		{
			string text = GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayName(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0001B164 File Offset: 0x00019364
		public virtual void GetLeaderboardDisplayNameCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayNameCopy(this.swigCPtr, name, array, bufferLength);
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

		// Token: 0x06000D5F RID: 3423 RVA: 0x0001B1BC File Offset: 0x000193BC
		public virtual LeaderboardSortMethod GetLeaderboardSortMethod(string name)
		{
			LeaderboardSortMethod leaderboardSortMethod = (LeaderboardSortMethod)GalaxyInstancePINVOKE.IStats_GetLeaderboardSortMethod(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return leaderboardSortMethod;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0001B1E8 File Offset: 0x000193E8
		public virtual LeaderboardDisplayType GetLeaderboardDisplayType(string name)
		{
			LeaderboardDisplayType leaderboardDisplayType = (LeaderboardDisplayType)GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayType(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return leaderboardDisplayType;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0001B213 File Offset: 0x00019413
		public virtual void RequestLeaderboardEntriesGlobal(string name, uint rangeStart, uint rangeEnd, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesGlobal__SWIG_0(this.swigCPtr, name, rangeStart, rangeEnd, ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0001B23A File Offset: 0x0001943A
		public virtual void RequestLeaderboardEntriesGlobal(string name, uint rangeStart, uint rangeEnd)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesGlobal__SWIG_1(this.swigCPtr, name, rangeStart, rangeEnd);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0001B25A File Offset: 0x0001945A
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter, GalaxyID userID, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_0(this.swigCPtr, name, countBefore, countAfter, GalaxyID.getCPtr(userID), ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0001B288 File Offset: 0x00019488
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_1(this.swigCPtr, name, countBefore, countAfter, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0001B2AF File Offset: 0x000194AF
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_2(this.swigCPtr, name, countBefore, countAfter);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public virtual void RequestLeaderboardEntriesForUsers(string name, ref GalaxyID[] userArray, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesForUsers__SWIG_0(this.swigCPtr, name, Array.ConvertAll<GalaxyID, ulong>(userArray, (GalaxyID id) => id.ToUint64()), (uint)userArray.LongLength, ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0001B32C File Offset: 0x0001952C
		public virtual void RequestLeaderboardEntriesForUsers(string name, ref GalaxyID[] userArray)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesForUsers__SWIG_1(this.swigCPtr, name, Array.ConvertAll<GalaxyID, ulong>(userArray, (GalaxyID id) => id.ToUint64()), (uint)userArray.LongLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0001B381 File Offset: 0x00019581
		public virtual void GetRequestedLeaderboardEntry(uint index, ref uint rank, ref int score, ref GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetRequestedLeaderboardEntry(this.swigCPtr, index, ref rank, ref score, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0001B3A9 File Offset: 0x000195A9
		public virtual void GetRequestedLeaderboardEntryWithDetails(uint index, ref uint rank, ref int score, byte[] details, uint detailsSize, ref uint outDetailsSize, ref GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetRequestedLeaderboardEntryWithDetails(this.swigCPtr, index, ref rank, ref score, details, detailsSize, ref outDetailsSize, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0001B3D7 File Offset: 0x000195D7
		public virtual void SetLeaderboardScore(string name, int score, bool forceUpdate, ILeaderboardScoreUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_0(this.swigCPtr, name, score, forceUpdate, ILeaderboardScoreUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0001B3FE File Offset: 0x000195FE
		public virtual void SetLeaderboardScore(string name, int score, bool forceUpdate)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_1(this.swigCPtr, name, score, forceUpdate);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0001B41E File Offset: 0x0001961E
		public virtual void SetLeaderboardScore(string name, int score)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_2(this.swigCPtr, name, score);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0001B43D File Offset: 0x0001963D
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize, bool forceUpdate, ILeaderboardScoreUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_0(this.swigCPtr, name, score, details, detailsSize, forceUpdate, ILeaderboardScoreUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0001B468 File Offset: 0x00019668
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize, bool forceUpdate)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_1(this.swigCPtr, name, score, details, detailsSize, forceUpdate);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0001B48C File Offset: 0x0001968C
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_2(this.swigCPtr, name, score, details, detailsSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public virtual uint GetLeaderboardEntryCount(string name)
		{
			uint num = GalaxyInstancePINVOKE.IStats_GetLeaderboardEntryCount(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0001B4DB File Offset: 0x000196DB
		public virtual void FindLeaderboard(string name, ILeaderboardRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_FindLeaderboard__SWIG_0(this.swigCPtr, name, ILeaderboardRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0001B4FF File Offset: 0x000196FF
		public virtual void FindLeaderboard(string name)
		{
			GalaxyInstancePINVOKE.IStats_FindLeaderboard__SWIG_1(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0001B51D File Offset: 0x0001971D
		public virtual void FindOrCreateLeaderboard(string name, string displayName, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType, ILeaderboardRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_FindOrCreateLeaderboard__SWIG_0(this.swigCPtr, name, displayName, (int)sortMethod, (int)displayType, ILeaderboardRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0001B546 File Offset: 0x00019746
		public virtual void FindOrCreateLeaderboard(string name, string displayName, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType)
		{
			GalaxyInstancePINVOKE.IStats_FindOrCreateLeaderboard__SWIG_1(this.swigCPtr, name, displayName, (int)sortMethod, (int)displayType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0001B568 File Offset: 0x00019768
		public virtual void RequestUserTimePlayed(GalaxyID userID, IUserTimePlayedRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IUserTimePlayedRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0001B591 File Offset: 0x00019791
		public virtual void RequestUserTimePlayed(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0001B5B4 File Offset: 0x000197B4
		public virtual void RequestUserTimePlayed()
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0001B5D4 File Offset: 0x000197D4
		public virtual uint GetUserTimePlayed(GalaxyID userID)
		{
			uint num = GalaxyInstancePINVOKE.IStats_GetUserTimePlayed__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0001B604 File Offset: 0x00019804
		public virtual uint GetUserTimePlayed()
		{
			uint num = GalaxyInstancePINVOKE.IStats_GetUserTimePlayed__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x040002BB RID: 699
		private HandleRef swigCPtr;

		// Token: 0x040002BC RID: 700
		protected bool swigCMemOwn;
	}
}
