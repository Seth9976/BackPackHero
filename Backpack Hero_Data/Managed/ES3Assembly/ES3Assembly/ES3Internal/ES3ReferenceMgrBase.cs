using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ES3Internal
{
	// Token: 0x020000CA RID: 202
	[DisallowMultipleComponent]
	[Serializable]
	public abstract class ES3ReferenceMgrBase : MonoBehaviour
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001F198 File Offset: 0x0001D398
		public static ES3ReferenceMgrBase Current
		{
			get
			{
				if (ES3ReferenceMgrBase._current == null)
				{
					GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
					ES3ReferenceMgr es3ReferenceMgr = null;
					foreach (GameObject gameObject in rootGameObjects)
					{
						if (gameObject.name == "Easy Save 3 Manager")
						{
							es3ReferenceMgr = gameObject.GetComponent<ES3ReferenceMgr>();
						}
					}
					if (es3ReferenceMgr == null)
					{
						GameObject[] array = rootGameObjects;
						for (int i = 0; i < array.Length; i++)
						{
							if ((ES3ReferenceMgrBase._current = array[i].GetComponentInChildren<ES3ReferenceMgr>()) != null)
							{
								return ES3ReferenceMgrBase._current;
							}
						}
					}
					ES3ReferenceMgrBase.mgrs.Add(ES3ReferenceMgrBase._current = es3ReferenceMgr);
				}
				return ES3ReferenceMgrBase._current;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001F24B File Offset: 0x0001D44B
		public bool IsInitialised
		{
			get
			{
				return this.idRef.Count > 0;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001F25C File Offset: 0x0001D45C
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		public ES3RefIdDictionary refId
		{
			get
			{
				if (this._refId == null)
				{
					this._refId = new ES3RefIdDictionary();
					foreach (KeyValuePair<long, Object> keyValuePair in this.idRef)
					{
						if (keyValuePair.Value != null)
						{
							this._refId[keyValuePair.Value] = keyValuePair.Key;
						}
					}
				}
				return this._refId;
			}
			set
			{
				this._refId = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001F2F5 File Offset: 0x0001D4F5
		public ES3GlobalReferences GlobalReferences
		{
			get
			{
				return ES3GlobalReferences.Instance;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		private void Awake()
		{
			if (ES3ReferenceMgrBase._current != null && ES3ReferenceMgrBase._current != this)
			{
				ES3ReferenceMgrBase current = ES3ReferenceMgrBase._current;
				if (ES3ReferenceMgrBase.Current != null)
				{
					current.Merge(this);
					if (base.gameObject.name.Contains("Easy Save 3 Manager"))
					{
						Object.Destroy(base.gameObject);
					}
					else
					{
						Object.Destroy(this);
					}
					ES3ReferenceMgrBase._current = current;
				}
			}
			else
			{
				ES3ReferenceMgrBase._current = this;
			}
			ES3ReferenceMgrBase.mgrs.Add(this);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001F381 File Offset: 0x0001D581
		private void OnDestroy()
		{
			ES3ReferenceMgrBase.mgrs.Remove(this);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001F390 File Offset: 0x0001D590
		public void Merge(ES3ReferenceMgrBase otherMgr)
		{
			foreach (KeyValuePair<long, Object> keyValuePair in otherMgr.idRef)
			{
				this.Add(keyValuePair.Value, keyValuePair.Key);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		public long Get(Object obj)
		{
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					if (obj == null)
					{
						return -1L;
					}
					long num;
					if (!es3ReferenceMgrBase.refId.TryGetValue(obj, out num))
					{
						return -1L;
					}
					return num;
				}
			}
			return -1L;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001F474 File Offset: 0x0001D674
		internal Object Get(long id, Type type, bool suppressWarnings = false)
		{
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					if (id == -1L)
					{
						return null;
					}
					Object @object;
					if (!es3ReferenceMgrBase.idRef.TryGetValue(id, out @object))
					{
						if (this.GlobalReferences != null)
						{
							Object object2 = this.GlobalReferences.Get(id);
							if (object2 != null)
							{
								return object2;
							}
						}
						if (type != null)
						{
							ES3Debug.LogWarning(string.Concat(new string[]
							{
								"Reference for ",
								(type != null) ? type.ToString() : null,
								" with ID ",
								id.ToString(),
								" could not be found in Easy Save's reference manager. If you are loading objects dynamically (i.e. objects created at runtime), this warning is expected and can be ignored."
							}), this, 0);
						}
						else
						{
							ES3Debug.LogWarning("Reference with ID " + id.ToString() + " could not be found in Easy Save's reference manager. If you are loading objects dynamically (i.e. objects created at runtime), this warning is expected and can be ignored.", this, 0);
						}
						return null;
					}
					if (@object == null)
					{
						return null;
					}
					return @object;
				}
			}
			return null;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001F5A4 File Offset: 0x0001D7A4
		public Object Get(long id, bool suppressWarnings = false)
		{
			return this.Get(id, null, suppressWarnings);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		public ES3Prefab GetPrefab(long id, bool suppressWarnings = false)
		{
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					foreach (ES3Prefab es3Prefab in es3ReferenceMgrBase.prefabs)
					{
						if (this.prefabs != null && es3Prefab.prefabId == id)
						{
							return es3Prefab;
						}
					}
				}
			}
			if (!suppressWarnings)
			{
				ES3Debug.LogWarning("Prefab with ID " + id.ToString() + " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene.", this, 0);
			}
			return null;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001F67C File Offset: 0x0001D87C
		public long GetPrefab(ES3Prefab prefabToFind, bool suppressWarnings = false)
		{
			using (HashSet<ES3ReferenceMgrBase>.Enumerator enumerator = ES3ReferenceMgrBase.mgrs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current == null))
					{
						foreach (ES3Prefab es3Prefab in this.prefabs)
						{
							if (es3Prefab == prefabToFind)
							{
								return es3Prefab.prefabId;
							}
						}
					}
				}
			}
			if (!suppressWarnings)
			{
				ES3Debug.LogWarning("Prefab with name " + prefabToFind.name + " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene.", prefabToFind, 0);
			}
			return -1L;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001F740 File Offset: 0x0001D940
		public long Add(Object obj)
		{
			long num;
			if (this.refId.TryGetValue(obj, out num))
			{
				return num;
			}
			if (this.GlobalReferences != null)
			{
				num = this.GlobalReferences.GetOrAdd(obj);
				if (num != -1L)
				{
					this.Add(obj, num);
					return num;
				}
			}
			object @lock = this._lock;
			long num2;
			lock (@lock)
			{
				num = ES3ReferenceMgrBase.GetNewRefID();
				num2 = this.Add(obj, num);
			}
			return num2;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
		public long Add(Object obj, long id)
		{
			if (!ES3ReferenceMgrBase.CanBeSaved(obj))
			{
				return -1L;
			}
			if (id == -1L)
			{
				id = ES3ReferenceMgrBase.GetNewRefID();
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.idRef[id] = obj;
				if (obj != null)
				{
					this.refId[obj] = id;
				}
			}
			return id;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001F840 File Offset: 0x0001DA40
		public bool AddPrefab(ES3Prefab prefab)
		{
			if (!this.prefabs.Contains(prefab))
			{
				this.prefabs.Add(prefab);
				return true;
			}
			return false;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001F860 File Offset: 0x0001DA60
		public void Remove(Object obj)
		{
			Func<KeyValuePair<long, Object>, bool> <>9__0;
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					object @lock = es3ReferenceMgrBase._lock;
					lock (@lock)
					{
						es3ReferenceMgrBase.refId.Remove(obj);
						IEnumerable<KeyValuePair<long, Object>> enumerable = es3ReferenceMgrBase.idRef;
						Func<KeyValuePair<long, Object>, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (KeyValuePair<long, Object> kvp) => kvp.Value == obj);
						}
						foreach (KeyValuePair<long, Object> keyValuePair in enumerable.Where(func).ToList<KeyValuePair<long, Object>>())
						{
							es3ReferenceMgrBase.idRef.Remove(keyValuePair.Key);
						}
					}
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001F980 File Offset: 0x0001DB80
		public void Remove(long referenceID)
		{
			Func<KeyValuePair<Object, long>, bool> <>9__0;
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					object @lock = es3ReferenceMgrBase._lock;
					lock (@lock)
					{
						es3ReferenceMgrBase.idRef.Remove(referenceID);
						IEnumerable<KeyValuePair<Object, long>> refId = es3ReferenceMgrBase.refId;
						Func<KeyValuePair<Object, long>, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (KeyValuePair<Object, long> kvp) => kvp.Value == referenceID);
						}
						foreach (KeyValuePair<Object, long> keyValuePair in refId.Where(func).ToList<KeyValuePair<Object, long>>())
						{
							es3ReferenceMgrBase.refId.Remove(keyValuePair.Key);
						}
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001FAA0 File Offset: 0x0001DCA0
		public void RemoveNullOrInvalidValues()
		{
			foreach (long num in (from pair in this.idRef
				where pair.Value == null || !ES3ReferenceMgrBase.CanBeSaved(pair.Value)
				select pair.Key).ToList<long>())
			{
				this.idRef.Remove(num);
			}
			if (this.GlobalReferences != null)
			{
				this.GlobalReferences.RemoveInvalidKeys();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001FB60 File Offset: 0x0001DD60
		public void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.refId.Clear();
				this.idRef.Clear();
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
		public bool Contains(Object obj)
		{
			return this.refId.ContainsKey(obj);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001FBBE File Offset: 0x0001DDBE
		public bool Contains(long referenceID)
		{
			return this.idRef.ContainsKey(referenceID);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		public void ChangeId(long oldId, long newId)
		{
			foreach (ES3ReferenceMgrBase es3ReferenceMgrBase in ES3ReferenceMgrBase.mgrs)
			{
				if (!(es3ReferenceMgrBase == null))
				{
					es3ReferenceMgrBase.idRef.ChangeKey(oldId, newId);
					es3ReferenceMgrBase.refId = null;
				}
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001FC38 File Offset: 0x0001DE38
		internal static long GetNewRefID()
		{
			if (ES3ReferenceMgrBase.rng == null)
			{
				ES3ReferenceMgrBase.rng = new Random();
			}
			byte[] array = new byte[8];
			ES3ReferenceMgrBase.rng.NextBytes(array);
			return Math.Abs(BitConverter.ToInt64(array, 0) % long.MaxValue);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001FC7E File Offset: 0x0001DE7E
		internal static bool CanBeSaved(Object obj)
		{
			return true;
		}

		// Token: 0x0400010C RID: 268
		internal object _lock = new object();

		// Token: 0x0400010D RID: 269
		public const string referencePropertyName = "_ES3Ref";

		// Token: 0x0400010E RID: 270
		private static ES3ReferenceMgrBase _current = null;

		// Token: 0x0400010F RID: 271
		private static HashSet<ES3ReferenceMgrBase> mgrs = new HashSet<ES3ReferenceMgrBase>();

		// Token: 0x04000110 RID: 272
		private static Random rng;

		// Token: 0x04000111 RID: 273
		[HideInInspector]
		public bool openPrefabs;

		// Token: 0x04000112 RID: 274
		public List<ES3Prefab> prefabs = new List<ES3Prefab>();

		// Token: 0x04000113 RID: 275
		[SerializeField]
		public ES3IdRefDictionary idRef = new ES3IdRefDictionary();

		// Token: 0x04000114 RID: 276
		private ES3RefIdDictionary _refId;
	}
}
