using System;
using System.Collections.Generic;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class Database<T> : ScriptableObject where T : ScriptableObject, ITagProvider
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00006B61 File Offset: 0x00004D61
		private void OnEnable()
		{
			this.built = false;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006B6A File Offset: 0x00004D6A
		public List<T> Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006B74 File Offset: 0x00004D74
		public void Add(T element)
		{
			if (this.data == null)
			{
				this.data = new List<T>();
			}
			this.data.Add(element);
			if (!this.built || !Application.isPlaying)
			{
				this.built = false;
				return;
			}
			string tagID = element.TagID;
			if (this.dictionary.ContainsKey(tagID))
			{
				Debug.LogError("Text Animator: Tag " + tagID + " is already present in the database. Skipping...");
				return;
			}
			this.dictionary.Add(tagID, element);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public void ForceBuildRefresh()
		{
			this.built = false;
			this.BuildOnce();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006C04 File Offset: 0x00004E04
		public void BuildOnce()
		{
			if (this.built)
			{
				return;
			}
			this.built = true;
			if (this.dictionary == null)
			{
				this.dictionary = new Dictionary<string, T>();
			}
			else
			{
				this.dictionary.Clear();
			}
			foreach (T t in this.data)
			{
				if (t)
				{
					string tagID = t.TagID;
					if (string.IsNullOrEmpty(tagID))
					{
						Debug.LogError("Text Animator: Tag is null or empty. Skipping...");
					}
					else if (this.dictionary.ContainsKey(tagID))
					{
						Debug.LogError("Text Animator: Tag " + tagID + " is already present in the database. Skipping...");
					}
					else
					{
						this.dictionary.Add(tagID, t);
					}
				}
			}
			this.OnBuildOnce();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006CE8 File Offset: 0x00004EE8
		protected virtual void OnBuildOnce()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006CEA File Offset: 0x00004EEA
		public bool ContainsKey(string key)
		{
			this.BuildOnce();
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x17000032 RID: 50
		public T this[string key]
		{
			get
			{
				this.BuildOnce();
				return this.dictionary[key];
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006D14 File Offset: 0x00004F14
		public void DestroyImmediate(bool databaseOnly = false)
		{
			if (!databaseOnly)
			{
				foreach (T t in this.data)
				{
					Object.DestroyImmediate(t);
				}
			}
			Object.DestroyImmediate(this);
		}

		// Token: 0x040000F4 RID: 244
		private bool built;

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		private List<T> data = new List<T>();

		// Token: 0x040000F6 RID: 246
		private Dictionary<string, T> dictionary;
	}
}
