using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000006 RID: 6
public class ES3AutoSaveMgr : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000010 RID: 16 RVA: 0x00002240 File Offset: 0x00000440
	public static ES3AutoSaveMgr Current
	{
		get
		{
			if (ES3AutoSaveMgr._current == null)
			{
				GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
				foreach (GameObject gameObject in rootGameObjects)
				{
					if (gameObject.name == "Easy Save 3 Manager")
					{
						return ES3AutoSaveMgr._current = gameObject.GetComponent<ES3AutoSaveMgr>();
					}
				}
				GameObject[] array = rootGameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					if ((ES3AutoSaveMgr._current = array[i].GetComponentInChildren<ES3AutoSaveMgr>()) != null)
					{
						return ES3AutoSaveMgr._current;
					}
				}
			}
			return ES3AutoSaveMgr._current;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000011 RID: 17 RVA: 0x000022CF File Offset: 0x000004CF
	public static ES3AutoSaveMgr Instance
	{
		get
		{
			return ES3AutoSaveMgr.Current;
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000022D8 File Offset: 0x000004D8
	public void Save()
	{
		if (this.autoSaves == null || this.autoSaves.Count == 0)
		{
			return;
		}
		if (this.settings.location == ES3.Location.Cache && !ES3.FileExists(this.settings))
		{
			ES3.CacheFile(this.settings);
		}
		if (this.autoSaves == null || this.autoSaves.Count == 0)
		{
			ES3.DeleteKey(this.key, this.settings);
		}
		else
		{
			List<GameObject> list = new List<GameObject>();
			foreach (ES3AutoSave es3AutoSave in this.autoSaves)
			{
				if (es3AutoSave.enabled)
				{
					list.Add(es3AutoSave.gameObject);
				}
			}
			ES3.Save<GameObject[]>(this.key, list.ToArray(), this.settings);
		}
		if (this.settings.location == ES3.Location.Cache && ES3.FileExists(this.settings))
		{
			ES3.StoreCachedFile(this.settings);
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000023E0 File Offset: 0x000005E0
	public void Load()
	{
		try
		{
			if (this.settings.location == ES3.Location.Cache && !ES3.FileExists(this.settings))
			{
				ES3.CacheFile(this.settings);
			}
		}
		catch
		{
		}
		ES3.Load<GameObject[]>(this.key, new GameObject[0], this.settings);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002440 File Offset: 0x00000640
	private void Start()
	{
		if (this.loadEvent == ES3AutoSaveMgr.LoadEvent.Start)
		{
			this.Load();
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002454 File Offset: 0x00000654
	public void Awake()
	{
		this.autoSaves = new HashSet<ES3AutoSave>();
		foreach (GameObject gameObject in base.gameObject.scene.GetRootGameObjects())
		{
			this.autoSaves.UnionWith(gameObject.GetComponentsInChildren<ES3AutoSave>(true));
		}
		ES3AutoSaveMgr._current = this;
		if (this.loadEvent == ES3AutoSaveMgr.LoadEvent.Awake)
		{
			this.Load();
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000024B9 File Offset: 0x000006B9
	private void OnApplicationQuit()
	{
		if (this.saveEvent == ES3AutoSaveMgr.SaveEvent.OnApplicationQuit)
		{
			this.Save();
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000024CA File Offset: 0x000006CA
	private void OnApplicationPause(bool paused)
	{
		if ((this.saveEvent == ES3AutoSaveMgr.SaveEvent.OnApplicationPause || (Application.isMobilePlatform && this.saveEvent == ES3AutoSaveMgr.SaveEvent.OnApplicationQuit)) && paused)
		{
			this.Save();
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000024F5 File Offset: 0x000006F5
	public static void AddAutoSave(ES3AutoSave autoSave)
	{
		if (ES3AutoSaveMgr.Current != null)
		{
			ES3AutoSaveMgr.Current.autoSaves.Add(autoSave);
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002515 File Offset: 0x00000715
	public static void RemoveAutoSave(ES3AutoSave autoSave)
	{
		if (ES3AutoSaveMgr.Current != null)
		{
			ES3AutoSaveMgr.Current.autoSaves.Remove(autoSave);
		}
	}

	// Token: 0x0400000B RID: 11
	public static ES3AutoSaveMgr _current;

	// Token: 0x0400000C RID: 12
	public string key = Guid.NewGuid().ToString();

	// Token: 0x0400000D RID: 13
	public ES3AutoSaveMgr.SaveEvent saveEvent = ES3AutoSaveMgr.SaveEvent.OnApplicationQuit;

	// Token: 0x0400000E RID: 14
	public ES3AutoSaveMgr.LoadEvent loadEvent = ES3AutoSaveMgr.LoadEvent.Awake;

	// Token: 0x0400000F RID: 15
	public ES3SerializableSettings settings = new ES3SerializableSettings("AutoSave.es3", ES3.Location.Cache);

	// Token: 0x04000010 RID: 16
	public HashSet<ES3AutoSave> autoSaves = new HashSet<ES3AutoSave>();

	// Token: 0x020000E0 RID: 224
	public enum LoadEvent
	{
		// Token: 0x0400015F RID: 351
		None,
		// Token: 0x04000160 RID: 352
		Awake,
		// Token: 0x04000161 RID: 353
		Start
	}

	// Token: 0x020000E1 RID: 225
	public enum SaveEvent
	{
		// Token: 0x04000163 RID: 355
		None,
		// Token: 0x04000164 RID: 356
		OnApplicationQuit,
		// Token: 0x04000165 RID: 357
		OnApplicationPause
	}
}
