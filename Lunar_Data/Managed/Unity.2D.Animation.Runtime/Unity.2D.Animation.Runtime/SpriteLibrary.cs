using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000009 RID: 9
	[DisallowMultipleComponent]
	[AddComponentMenu("2D Animation/Sprite Library")]
	[MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@latest/index.html?subfolder=/manual/SLAsset.html%23sprite-library-component")]
	public class SpriteLibrary : MonoBehaviour
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002731 File Offset: 0x00000931
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000270E File Offset: 0x0000090E
		public SpriteLibraryAsset spriteLibraryAsset
		{
			get
			{
				return this.m_SpriteLibraryAsset;
			}
			set
			{
				if (this.m_SpriteLibraryAsset != value)
				{
					this.m_SpriteLibraryAsset = value;
					this.CacheOverrides();
					this.RefreshSpriteResolvers();
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002739 File Offset: 0x00000939
		private void OnEnable()
		{
			this.CacheOverrides();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002741 File Offset: 0x00000941
		public Sprite GetSprite(string category, string label)
		{
			return this.GetSprite(SpriteLibrary.GetHashForCategoryAndEntry(category, label));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002750 File Offset: 0x00000950
		private Sprite GetSprite(int hash)
		{
			if (this.m_CategoryEntryHashCache.ContainsKey(hash))
			{
				return this.m_CategoryEntryHashCache[hash].sprite;
			}
			return null;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002774 File Offset: 0x00000974
		private void UpdateCacheOverridesIfNeeded()
		{
			if (this.m_CategoryEntryCache != null)
			{
				int previousSpriteLibraryAsset = this.m_PreviousSpriteLibraryAsset;
				SpriteLibraryAsset spriteLibraryAsset = this.m_SpriteLibraryAsset;
				int? num = ((spriteLibraryAsset != null) ? new int?(spriteLibraryAsset.GetInstanceID()) : null);
				if ((previousSpriteLibraryAsset == num.GetValueOrDefault()) & (num != null))
				{
					long previousModificationHash = this.m_PreviousModificationHash;
					SpriteLibraryAsset spriteLibraryAsset2 = this.m_SpriteLibraryAsset;
					long? num2 = ((spriteLibraryAsset2 != null) ? new long?(spriteLibraryAsset2.modificationHash) : null);
					if ((previousModificationHash == num2.GetValueOrDefault()) & (num2 != null))
					{
						return;
					}
				}
			}
			this.CacheOverrides();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002804 File Offset: 0x00000A04
		internal bool GetCategoryAndEntryNameFromHash(int hash, out string category, out string entry)
		{
			this.UpdateCacheOverridesIfNeeded();
			if (this.m_CategoryEntryHashCache.ContainsKey(hash))
			{
				category = this.m_CategoryEntryHashCache[hash].category;
				entry = this.m_CategoryEntryHashCache[hash].entry;
				return true;
			}
			category = null;
			entry = null;
			return false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002854 File Offset: 0x00000A54
		internal static int GetHashForCategoryAndEntry(string category, string entry)
		{
			return SpriteLibraryUtility.GetStringHash(category + "_" + entry);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000286C File Offset: 0x00000A6C
		internal Sprite GetSpriteFromCategoryAndEntryHash(int hash, out bool validEntry)
		{
			this.UpdateCacheOverridesIfNeeded();
			if (this.m_CategoryEntryHashCache.ContainsKey(hash))
			{
				validEntry = true;
				return this.m_CategoryEntryHashCache[hash].sprite;
			}
			validEntry = false;
			return null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000289C File Offset: 0x00000A9C
		private List<SpriteCategoryEntry> GetEntries(string category, bool addIfNotExist)
		{
			int num = this.m_Library.FindIndex((SpriteLibCategory x) => x.name == category);
			if (num < 0)
			{
				if (!addIfNotExist)
				{
					return null;
				}
				this.m_Library.Add(new SpriteLibCategory
				{
					name = category,
					categoryList = new List<SpriteCategoryEntry>()
				});
				num = this.m_Library.Count - 1;
			}
			return this.m_Library[num].categoryList;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002920 File Offset: 0x00000B20
		private SpriteCategoryEntry GetEntry(List<SpriteCategoryEntry> entries, string entry, bool addIfNotExist)
		{
			int num = entries.FindIndex((SpriteCategoryEntry x) => x.name == entry);
			if (num < 0)
			{
				if (!addIfNotExist)
				{
					return null;
				}
				entries.Add(new SpriteCategoryEntry
				{
					name = entry
				});
				num = entries.Count - 1;
			}
			return entries[num];
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002980 File Offset: 0x00000B80
		public void AddOverride(SpriteLibraryAsset spriteLib, string category, string label)
		{
			Sprite sprite = spriteLib.GetSprite(category, label);
			List<SpriteCategoryEntry> entries = this.GetEntries(category, true);
			this.GetEntry(entries, label, true).sprite = sprite;
			this.CacheOverrides();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void AddOverride(SpriteLibraryAsset spriteLib, string category)
		{
			int categoryHash = SpriteLibraryUtility.GetStringHash(category);
			SpriteLibCategory spriteLibCategory = spriteLib.categories.FirstOrDefault((SpriteLibCategory x) => x.hash == categoryHash);
			if (spriteLibCategory != null)
			{
				List<SpriteCategoryEntry> entries = this.GetEntries(category, true);
				for (int i = 0; i < spriteLibCategory.categoryList.Count; i++)
				{
					SpriteCategoryEntry spriteCategoryEntry = spriteLibCategory.categoryList[i];
					this.GetEntry(entries, spriteCategoryEntry.name, true).sprite = spriteCategoryEntry.sprite;
				}
				this.CacheOverrides();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A41 File Offset: 0x00000C41
		public void AddOverride(Sprite sprite, string category, string label)
		{
			this.GetEntry(this.GetEntries(category, true), label, true).sprite = sprite;
			this.CacheOverrides();
			this.RefreshSpriteResolvers();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A68 File Offset: 0x00000C68
		public void RemoveOverride(string category)
		{
			int num = this.m_Library.FindIndex((SpriteLibCategory x) => x.name == category);
			if (num >= 0)
			{
				this.m_Library.RemoveAt(num);
				this.CacheOverrides();
				this.RefreshSpriteResolvers();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public void RemoveOverride(string category, string label)
		{
			List<SpriteCategoryEntry> entries = this.GetEntries(category, false);
			if (entries != null)
			{
				int num = entries.FindIndex((SpriteCategoryEntry x) => x.name == label);
				if (num >= 0)
				{
					entries.RemoveAt(num);
					this.CacheOverrides();
					this.RefreshSpriteResolvers();
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B08 File Offset: 0x00000D08
		public bool HasOverride(string category, string label)
		{
			List<SpriteCategoryEntry> entries = this.GetEntries(category, false);
			return entries != null && this.GetEntry(entries, label, false) != null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B30 File Offset: 0x00000D30
		public void RefreshSpriteResolvers()
		{
			SpriteResolver[] componentsInChildren = base.GetComponentsInChildren<SpriteResolver>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ResolveSpriteToSpriteRenderer();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B5A File Offset: 0x00000D5A
		internal IEnumerable<string> categoryNames
		{
			get
			{
				this.UpdateCacheOverridesIfNeeded();
				return this.m_CategoryEntryCache.Keys;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B6D File Offset: 0x00000D6D
		internal IEnumerable<string> GetEntryNames(string category)
		{
			this.UpdateCacheOverridesIfNeeded();
			if (this.m_CategoryEntryCache.ContainsKey(category))
			{
				return this.m_CategoryEntryCache[category];
			}
			return null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B94 File Offset: 0x00000D94
		internal void CacheOverrides()
		{
			this.m_PreviousSpriteLibraryAsset = 0;
			this.m_PreviousModificationHash = 0L;
			this.m_CategoryEntryHashCache = new Dictionary<int, SpriteLibrary.CategoryEntrySprite>();
			this.m_CategoryEntryCache = new Dictionary<string, HashSet<string>>();
			if (this.m_SpriteLibraryAsset)
			{
				this.m_PreviousSpriteLibraryAsset = this.m_SpriteLibraryAsset.GetInstanceID();
				this.m_PreviousModificationHash = this.m_SpriteLibraryAsset.modificationHash;
				foreach (SpriteLibCategory spriteLibCategory in this.m_SpriteLibraryAsset.categories)
				{
					string name = spriteLibCategory.name;
					this.m_CategoryEntryCache.Add(name, new HashSet<string>());
					HashSet<string> hashSet = this.m_CategoryEntryCache[name];
					foreach (SpriteCategoryEntry spriteCategoryEntry in spriteLibCategory.categoryList)
					{
						this.m_CategoryEntryHashCache.Add(SpriteLibrary.GetHashForCategoryAndEntry(name, spriteCategoryEntry.name), new SpriteLibrary.CategoryEntrySprite
						{
							category = name,
							entry = spriteCategoryEntry.name,
							sprite = spriteCategoryEntry.sprite
						});
						hashSet.Add(spriteCategoryEntry.name);
					}
				}
			}
			foreach (SpriteLibCategory spriteLibCategory2 in this.m_Library)
			{
				string name2 = spriteLibCategory2.name;
				if (!this.m_CategoryEntryCache.ContainsKey(name2))
				{
					this.m_CategoryEntryCache.Add(name2, new HashSet<string>());
				}
				HashSet<string> hashSet2 = this.m_CategoryEntryCache[name2];
				foreach (SpriteCategoryEntry spriteCategoryEntry2 in spriteLibCategory2.categoryList)
				{
					if (!hashSet2.Contains(spriteCategoryEntry2.name))
					{
						hashSet2.Add(spriteCategoryEntry2.name);
					}
					int hashForCategoryAndEntry = SpriteLibrary.GetHashForCategoryAndEntry(name2, spriteCategoryEntry2.name);
					if (!this.m_CategoryEntryHashCache.ContainsKey(hashForCategoryAndEntry))
					{
						this.m_CategoryEntryHashCache.Add(hashForCategoryAndEntry, new SpriteLibrary.CategoryEntrySprite
						{
							category = name2,
							entry = spriteCategoryEntry2.name,
							sprite = spriteCategoryEntry2.sprite
						});
					}
					else
					{
						SpriteLibrary.CategoryEntrySprite categoryEntrySprite = this.m_CategoryEntryHashCache[hashForCategoryAndEntry];
						categoryEntrySprite.sprite = spriteCategoryEntry2.sprite;
						this.m_CategoryEntryHashCache[hashForCategoryAndEntry] = categoryEntrySprite;
					}
				}
			}
		}

		// Token: 0x04000011 RID: 17
		[SerializeField]
		private List<SpriteLibCategory> m_Library = new List<SpriteLibCategory>();

		// Token: 0x04000012 RID: 18
		[SerializeField]
		private SpriteLibraryAsset m_SpriteLibraryAsset;

		// Token: 0x04000013 RID: 19
		private Dictionary<int, SpriteLibrary.CategoryEntrySprite> m_CategoryEntryHashCache;

		// Token: 0x04000014 RID: 20
		private Dictionary<string, HashSet<string>> m_CategoryEntryCache;

		// Token: 0x04000015 RID: 21
		private int m_PreviousSpriteLibraryAsset;

		// Token: 0x04000016 RID: 22
		private long m_PreviousModificationHash;

		// Token: 0x0200000A RID: 10
		private struct CategoryEntrySprite
		{
			// Token: 0x04000017 RID: 23
			public string category;

			// Token: 0x04000018 RID: 24
			public string entry;

			// Token: 0x04000019 RID: 25
			public Sprite sprite;
		}
	}
}
