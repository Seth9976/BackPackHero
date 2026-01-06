using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DevPunksSaveGame.Interfaces;
using UnityEngine;

namespace DevPunksSaveGame
{
	// Token: 0x0200022B RID: 555
	public class ConsoleWrapper : MonoBehaviour
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x000AC754 File Offset: 0x000AA954
		public static ConsoleWrapper Instance
		{
			get
			{
				return ConsoleWrapper.GetInstance();
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000AC75B File Offset: 0x000AA95B
		public void SetSlot(string slot)
		{
			this._slotOverride = slot;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000AC764 File Offset: 0x000AA964
		public void GameHasFinishedLoading()
		{
			this._gameHasFinishedLoading = true;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000AC76D File Offset: 0x000AA96D
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static ConsoleWrapper GetInstance()
		{
			if (ConsoleWrapper._instance == null && !ConsoleWrapper._creating)
			{
				ConsoleWrapper._creating = true;
				return new GameObject("ConsoleWrapper").AddComponent<ConsoleWrapper>();
			}
			return ConsoleWrapper._instance;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000AC7A0 File Offset: 0x000AA9A0
		public int CheckForLatestSave()
		{
			int num = 0;
			DateTime dateTime = DateTime.UnixEpoch;
			for (int i = 0; i < this.NumberOfSaveSlots; i++)
			{
				if (DateTime.Compare(MetaProgressSaveManager.main.GetDateTimeFromSlot(i), dateTime) > 0)
				{
					num = i;
					dateTime = MetaProgressSaveManager.main.GetDateTimeFromSlot(i);
				}
			}
			return num;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000AC7E9 File Offset: 0x000AA9E9
		public IEnumerator StartLatestSave()
		{
			this.b_startingFromActivity = false;
			this.latestSaveSlotUsed = this.CheckForLatestSave();
			yield return new WaitUntil(() => this.latestSaveSlotUsed != -1);
			base.gameObject.GetComponent<LoadStoryGame>().LoadStoryFromActivity(this.latestSaveSlotUsed);
			yield break;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000AC7F8 File Offset: 0x000AA9F8
		public string[] GetAllSlots()
		{
			string[] array;
			try
			{
				array = this._file.GetAllSlots();
			}
			catch (Exception ex)
			{
				Debug.Log("Failed to get all slots:" + ex.Message);
				array = new string[0];
			}
			return array;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x000AC844 File Offset: 0x000AAA44
		public bool SlotExists(string slot)
		{
			slot = this.SanitizePath(slot);
			return this.GetAllSlots().Contains(slot);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000AC85C File Offset: 0x000AAA5C
		public string[] GetAllFilesInSlot(string slot, bool disableCache = false)
		{
			slot = this.SanitizePath(slot);
			if (this._overRideSlot)
			{
				slot = this._slotOverride;
			}
			Debug.Log("ConsoleWrapper.GetAllFiles");
			if (!disableCache)
			{
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, SaveFileEntry> keyValuePair in this._saveCache.ToArray())
				{
					if (keyValuePair.Key.StartsWith(slot + "::"))
					{
						list.Add(keyValuePair.Value.Path);
					}
				}
				if (list.Count > 0)
				{
					return list.ToArray();
				}
			}
			while (this._accessingFileSystem)
			{
			}
			if (this._simpleSave)
			{
				throw new NotSupportedException("Get All Files In Slot, is not supported with simplesave(Playerprefs)");
			}
			return this._file.GetAllFilesInSlot(slot);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000AC91C File Offset: 0x000AAB1C
		public void DeleteSlotAndAllWithin(string slot)
		{
			slot = this.SanitizePath(slot);
			if (this._simpleSave)
			{
				throw new NotSupportedException("Delete All Files In Slot, is not supported with simplesave(Playerprefs)");
			}
			this._file.DeleteSlotAndAllWithin(slot);
			KeyValuePair<string, SaveFileEntry>[] array = this._saveCache.ToArray();
			this._saveCache.Clear();
			foreach (KeyValuePair<string, SaveFileEntry> keyValuePair in array)
			{
				if (!keyValuePair.Key.StartsWith(slot + "::"))
				{
					this._saveCache.TryAdd(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000AC9B1 File Offset: 0x000AABB1
		public void LoadFileAsync(SaveFileEntry fileEntry, Action<EFileResult, SaveFileEntry> callback)
		{
			this._file.LoadFileAsync(fileEntry, callback);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000AC9C0 File Offset: 0x000AABC0
		public void SetSimpleSave(bool simpleSave)
		{
			this._simpleSave = simpleSave;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000AC9C9 File Offset: 0x000AABC9
		public void SetUseCompression(bool useCompression)
		{
			this._file.SetUseCompression(useCompression);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x000AC9D7 File Offset: 0x000AABD7
		public string GetSystemLanguage()
		{
			return this._file.GetSystemLanguage();
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000AC9E4 File Offset: 0x000AABE4
		private void Awake()
		{
			if (ConsoleWrapper._instance != null && ConsoleWrapper._instance != this)
			{
				Object.Destroy(base.gameObject);
				return;
			}
			ConsoleWrapper._instance = this;
			this.InitFile("defaultFolder", "defaultSave");
			this.InitAchievements();
			Object.DontDestroyOnLoad(ConsoleWrapper._instance);
			Debug.Log("ConsoleWrapper created");
			ConsoleWrapper._creating = false;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000ACA4D File Offset: 0x000AAC4D
		private void UserLoggedIn(string userId, string token)
		{
			this.EOSToken = token;
			this.EOSUserId = userId;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000ACA5D File Offset: 0x000AAC5D
		public bool IsReady
		{
			get
			{
				FileInterface file = this._file;
				return file != null && file.IsReady && ConsoleWrapper._initialized;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x000ACA7A File Offset: 0x000AAC7A
		public bool FileOperationInProgress
		{
			get
			{
				return this._accessingFileSystem;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000ACA84 File Offset: 0x000AAC84
		public bool SaveInProgress
		{
			get
			{
				return this._saveQueue != null && this._saveQueue.Count > 0;
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000ACA9E File Offset: 0x000AAC9E
		public void InitFile(string folder, string file)
		{
			bool simpleSave = this._simpleSave;
			this._file = new PCFile();
			this._saveQueue = new List<SaveFileEntry>();
			this._saveCache = new ConcurrentDictionary<string, SaveFileEntry>();
			ConsoleWrapper._initialized = this._file != null;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000ACAD6 File Offset: 0x000AACD6
		private void OnApplicationQuit()
		{
			if (this._simpleSave)
			{
				this.SavePlayerPrefs("slot", "path");
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000ACAF0 File Offset: 0x000AACF0
		public void Update()
		{
			if ((DateTime.Now - this.LastSaveTime).TotalSeconds > 3.0)
			{
				if (this.startBatchTread)
				{
					ThreadState threadState = this.batchThreadRunning.ThreadState;
					if (this.batchThreadRunning.ThreadState != ThreadState.Running)
					{
						this.batchThreadRunning = null;
						this.startBatchTread = false;
					}
				}
				if (this._saveQueue.Count > 0 && !this._accessingFileSystem)
				{
					Debug.Log("Get all files from Batch-queue and Save it");
					string slot = this._saveQueue[0].Slot;
					int num = 0;
					while (num < this._saveQueue.Count && (this._saveQueue.Count < 10 || slot == this._saveQueue[num].Slot))
					{
						num++;
					}
					Debug.Log("Found " + num.ToString() + " files to save");
					this.BatchSaveQueue(num);
					this.LastSaveTime = DateTime.Now;
				}
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000ACBF8 File Offset: 0x000AADF8
		public GenericUser GetGenericUser()
		{
			string text = "Unknown_" + Random.Range(0, 10000).ToString();
			return new GenericUser
			{
				Id = text,
				Tag = text
			};
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000ACC38 File Offset: 0x000AAE38
		private void AddReplaceInSaveCache(SaveFileEntry entry)
		{
			string text = entry.Slot + "::" + entry.Path;
			Debug.Log("AddReplaceInSaveCache:" + text);
			this._saveCache[text] = entry;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000ACC7C File Offset: 0x000AAE7C
		private void AddToSaveQueue(SaveFileEntry entry)
		{
			SaveFileEntry saveFileEntry = new SaveFileEntry(entry.Slot, entry.Path, entry.Value);
			this.AddReplaceInSaveCache(saveFileEntry);
			this._saveQueue.Add(saveFileEntry);
			this.ResetBatchSaveTimer();
			Debug.Log(string.Concat(new string[]
			{
				"AddToSaveQueueTime ",
				entry.Path,
				" ",
				entry.Slot,
				" ",
				DateTime.Now.ToString("HH:mm:ss.FFFFFF")
			}));
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000ACD0C File Offset: 0x000AAF0C
		private SaveFileEntry FindInSaveCache(string slot, string path)
		{
			return null;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000ACD1C File Offset: 0x000AAF1C
		private void SaveFileCallback(EFileResult result)
		{
			Debug.Log("SaveFileCallback:" + result.ToString());
			if (this._file.UsesCriticalSection)
			{
				this._file.ExitCriticalSection();
			}
			this._saveQueue.RemoveAt(0);
			this._lastSaveResult = result;
			this._accessingFileSystem = false;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000ACD7C File Offset: 0x000AAF7C
		private void BatchSaveFileCallback(EFileResult result)
		{
			Debug.Log("SaveFileCallback1:" + result.ToString());
			this._lastSaveResult = result;
			this._accessingFileSystem = false;
			if (this._file.UsesCriticalSection)
			{
				this._file.ExitCriticalSection();
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000ACDD0 File Offset: 0x000AAFD0
		public void BatchSaveQueue(int queueCount)
		{
			this._accessingFileSystem = true;
			this._lastSaveResult = EFileResult.Other;
			if (this._simpleSave)
			{
				if (this._file.UsesCriticalSection)
				{
					this._file.EnterCriticalSection();
				}
				for (int i = 0; i < queueCount; i++)
				{
					PlayerPrefs.SetString(this._saveQueue[i].Slot + this._saveQueue[i].Path, Convert.ToBase64String(this._saveQueue[i].Value));
				}
				for (int j = 0; j < queueCount; j++)
				{
					this._saveQueue.RemoveAt(0);
				}
				PlayerPrefs.Save();
				this._accessingFileSystem = false;
				this._lastSaveResult = EFileResult.Ok;
				if (this._file.UsesCriticalSection)
				{
					this._file.ExitCriticalSection();
				}
			}
			else
			{
				if (this._file.UsesCriticalSection)
				{
					this._file.EnterCriticalSection();
				}
				List<SaveFileEntry> saveList = new List<SaveFileEntry>();
				for (int k = 0; k < queueCount; k++)
				{
					saveList.Add(new SaveFileEntry(this._saveQueue[k].Slot, this._saveQueue[k].Path, this._saveQueue[k].Value));
				}
				for (int l = 0; l < queueCount; l++)
				{
					this._saveQueue.RemoveAt(0);
				}
				Thread thread = new Thread(delegate
				{
					this._file.BatchSave(saveList, new Action<EFileResult>(this.BatchSaveFileCallback), "title", "subtitle", "detail");
				})
				{
					Name = "Save"
				};
				thread.Start();
				this.batchThreadRunning = thread;
				this.startBatchTread = true;
			}
			if (this.BackupComplete)
			{
				Debug.Log("backup complete!");
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000ACF90 File Offset: 0x000AB190
		public void SaveFileAsyncNoSlot(string path, string value)
		{
			path = this.SanitizePath(path);
			if (!this._overRideSlot)
			{
				Debug.LogError("No slot specified using override");
				return;
			}
			this.AddToSaveQueue(new SaveFileEntry(this._slotOverride, path, Encoding.UTF8.GetBytes(value)));
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000ACFCB File Offset: 0x000AB1CB
		public void SaveFileAsyncNoSlot(string path, byte[] value)
		{
			path = this.SanitizePath(path);
			if (!this._overRideSlot)
			{
				Debug.LogError("No slot specified using override");
				return;
			}
			this.AddToSaveQueue(new SaveFileEntry(this._slotOverride, path, value));
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000ACFFC File Offset: 0x000AB1FC
		public void SaveFileAsync(string slot, string path, string value)
		{
			slot = this.SanitizePath(slot);
			path = this.SanitizePath(path);
			if (this._overRideSlot)
			{
				slot = this._slotOverride;
			}
			this.AddToSaveQueue(new SaveFileEntry(slot, path, Encoding.UTF8.GetBytes(value)));
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000AD038 File Offset: 0x000AB238
		public void SaveFileAsync(string slot, string path, byte[] value)
		{
			slot = this.SanitizePath(slot);
			path = this.SanitizePath(path);
			if (this._overRideSlot)
			{
				slot = this._slotOverride;
			}
			this.AddToSaveQueue(new SaveFileEntry(slot, path, value));
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000AD06A File Offset: 0x000AB26A
		public void SaveFile(SaveFileEntry fileEntry, Action<EFileResult> callback)
		{
			this._file.SaveFile(fileEntry, callback, "", "", "");
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000AD088 File Offset: 0x000AB288
		public ValueTuple<bool, byte[]> LoadFile(string slot, string path, bool disableCache = false)
		{
			return new ValueTuple<bool, byte[]>(false, null);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000AD09C File Offset: 0x000AB29C
		public void SavePlayerPrefs(string slot, string path)
		{
			slot = this.SanitizePath(slot);
			path = this.SanitizePath(path);
			PlayerPrefs.Save();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000AD0B8 File Offset: 0x000AB2B8
		public bool FileExists(string slot, string path, bool disableCache = false)
		{
			slot = this.SanitizePath(slot);
			path = this.SanitizePath(path);
			bool flag = this._FileExists(slot, path, disableCache);
			Debug.Log(string.Format("ConsoleWrapper.FileExists(slot: {0}, path: {1}, disableCache: {2}): {3}", new object[] { slot, path, disableCache, flag }));
			return flag;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000AD114 File Offset: 0x000AB314
		private bool _FileExists(string slot, string path, bool disableCache = false)
		{
			slot = this.SanitizePath(slot);
			path = this.SanitizePath(path);
			if (this._overRideSlot)
			{
				slot = this._slotOverride;
			}
			if (!disableCache)
			{
				if (this.FindInSaveCache(slot, path) != null)
				{
					return true;
				}
				if (this._gameHasFinishedLoading)
				{
					return false;
				}
			}
			while (this._accessingFileSystem)
			{
			}
			if (this._simpleSave)
			{
				this._accessingFileSystem = false;
				return true;
			}
			ValueTuple<EFileResult, SaveFileEntry> valueTuple = this._file.LoadFileSync(slot, path);
			EFileResult item = valueTuple.Item1;
			SaveFileEntry item2 = valueTuple.Item2;
			if (item == EFileResult.Ok && item2 != null && item2.Value != null)
			{
				this._accessingFileSystem = false;
				return true;
			}
			this._accessingFileSystem = false;
			return false;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000AD1B6 File Offset: 0x000AB3B6
		private void InitAchievements()
		{
			Debug.Log("Start achievements");
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000AD1C4 File Offset: 0x000AB3C4
		public void UnlockAchievement(string achievement, int value = 0)
		{
			Debug.Log("ConsoleWrapper.UnlockAchievement:" + achievement + "," + value.ToString());
			AchievementIds achievementIds = AchievementsConfig.GetAchievementIds(achievement);
			if (achievementIds.SteamId != achievement)
			{
				Debug.LogError("Achievement not found: " + achievement);
				return;
			}
			if (value != 0)
			{
				if (achievementIds.UnlockCount <= 0U)
				{
					Debug.LogError("Error: Tried to unlock achievement " + achievement + " with value but no unlock count set.");
					return;
				}
				if (value <= 0)
				{
					Debug.LogError("Error: Tried to unlock achievement " + achievement + " with no progress.");
					return;
				}
			}
			if (value != 0)
			{
				Debug.Log(string.Format("Success: Unlocked {0} of {1}, percentage: {2}%", value, achievementIds.UnlockCount, (long)(100 * value) / (long)((ulong)achievementIds.UnlockCount)));
			}
			else
			{
				Debug.Log("Success: Unlocked achievement: " + achievement);
			}
			AchievementInterface achievement2 = this._achievement;
			if (achievement2 == null)
			{
				return;
			}
			achievement2.UnlockAchievement(achievementIds, value);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000AD2A8 File Offset: 0x000AB4A8
		private void ResetBatchSaveTimer()
		{
			this.nextBatchSaveTime = Time.time + this.batchSaveTimeOffset;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000AD2BC File Offset: 0x000AB4BC
		private bool BatchSaveTimerHasRunOut()
		{
			return this.nextBatchSaveTime < Time.time;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000AD2CB File Offset: 0x000AB4CB
		public string SanitizePath(string path)
		{
			return path;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000AD2D0 File Offset: 0x000AB4D0
		public List<string> FindSaveSlots(string prefix, string suffix)
		{
			string[] allSlots = this.GetAllSlots();
			prefix = this.SanitizePath(prefix);
			suffix = this.SanitizePath(suffix);
			Debug.Log(string.Concat(new string[] { "Filtering slots: prefix=\"", prefix, "\" suffix=\"", suffix, "\"" }));
			foreach (string text in allSlots)
			{
				Debug.Log(string.Format("{0} - {1}", text, text.StartsWith(prefix) && text.EndsWith(suffix)));
			}
			return allSlots.Where((string slot) => slot.StartsWith(prefix) && slot.EndsWith(suffix)).ToList<string>();
		}

		// Token: 0x04000E56 RID: 3670
		private static bool _creating;

		// Token: 0x04000E57 RID: 3671
		private static bool _initialized;

		// Token: 0x04000E58 RID: 3672
		public bool BackupComplete;

		// Token: 0x04000E59 RID: 3673
		private float nextBatchSaveTime;

		// Token: 0x04000E5A RID: 3674
		private float batchSaveTimeOffset = 0.1f;

		// Token: 0x04000E5B RID: 3675
		private bool _simpleSave;

		// Token: 0x04000E5C RID: 3676
		private readonly bool _overRideSlot;

		// Token: 0x04000E5D RID: 3677
		private bool _gameHasFinishedLoading;

		// Token: 0x04000E5E RID: 3678
		private AchievementInterface _achievement;

		// Token: 0x04000E5F RID: 3679
		private FileInterface _file;

		// Token: 0x04000E60 RID: 3680
		public bool OnlineSupported;

		// Token: 0x04000E61 RID: 3681
		public string EOSToken;

		// Token: 0x04000E62 RID: 3682
		public string EOSUserId;

		// Token: 0x04000E63 RID: 3683
		private static ConsoleWrapper _instance;

		// Token: 0x04000E64 RID: 3684
		private ConcurrentDictionary<string, SaveFileEntry> _saveCache;

		// Token: 0x04000E65 RID: 3685
		private List<SaveFileEntry> _saveQueue;

		// Token: 0x04000E66 RID: 3686
		private Thread batchThreadRunning;

		// Token: 0x04000E67 RID: 3687
		private bool startBatchTread;

		// Token: 0x04000E68 RID: 3688
		public string _slotOverride = "";

		// Token: 0x04000E69 RID: 3689
		public int NumberOfSaveSlots = 3;

		// Token: 0x04000E6A RID: 3690
		public int latestSaveSlotUsed = -1;

		// Token: 0x04000E6B RID: 3691
		public bool b_startingFromActivity;

		// Token: 0x04000E6C RID: 3692
		public bool b_launchActivityOnStartGame;

		// Token: 0x04000E6D RID: 3693
		private EFileResult _lastSaveResult;

		// Token: 0x04000E6E RID: 3694
		private Action<bool, byte[]> _loadCallback;

		// Token: 0x04000E6F RID: 3695
		private volatile bool _accessingFileSystem;

		// Token: 0x04000E70 RID: 3696
		public bool networkReady;

		// Token: 0x04000E71 RID: 3697
		private int FrameCoolDown;

		// Token: 0x04000E72 RID: 3698
		private DateTime LastSaveTime = DateTime.Now;

		// Token: 0x04000E73 RID: 3699
		private const int SaveCooldown = 3;
	}
}
