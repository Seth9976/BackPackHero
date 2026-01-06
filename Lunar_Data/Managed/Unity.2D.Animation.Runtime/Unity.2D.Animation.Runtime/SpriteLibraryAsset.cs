using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000014 RID: 20
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@latest/index.html?subfolder=/manual/SLAsset.html")]
	[MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	public class SpriteLibraryAsset : ScriptableObject
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000304B File Offset: 0x0000124B
		internal static SpriteLibraryAsset CreateAsset(List<SpriteLibCategory> categories, string assetName, long version)
		{
			SpriteLibraryAsset spriteLibraryAsset = ScriptableObject.CreateInstance<SpriteLibraryAsset>();
			spriteLibraryAsset.m_Labels = categories;
			spriteLibraryAsset.ValidateCategories();
			spriteLibraryAsset.name = assetName;
			spriteLibraryAsset.UpdateHashes();
			spriteLibraryAsset.m_ModificationHash = version;
			return spriteLibraryAsset;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003073 File Offset: 0x00001273
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000307B File Offset: 0x0000127B
		internal List<SpriteLibCategory> categories
		{
			get
			{
				return this.m_Labels;
			}
			set
			{
				this.m_Labels = value;
				this.ValidateCategories();
				this.UpdateModificationHash();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003090 File Offset: 0x00001290
		internal long modificationHash
		{
			get
			{
				return this.m_ModificationHash;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003098 File Offset: 0x00001298
		internal Sprite GetSprite(int categoryHash, int labelHash)
		{
			SpriteLibCategory spriteLibCategory = this.m_Labels.FirstOrDefault((SpriteLibCategory x) => x.hash == categoryHash);
			if (spriteLibCategory != null)
			{
				SpriteCategoryEntry spriteCategoryEntry = spriteLibCategory.categoryList.FirstOrDefault((SpriteCategoryEntry x) => x.hash == labelHash);
				if (spriteCategoryEntry != null)
				{
					return spriteCategoryEntry.sprite;
				}
			}
			return null;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030F8 File Offset: 0x000012F8
		internal Sprite GetSprite(int categoryHash, int labelHash, out bool validEntry)
		{
			SpriteLibCategory spriteLibCategory = null;
			for (int i = 0; i < this.m_Labels.Count; i++)
			{
				if (this.m_Labels[i].hash == categoryHash)
				{
					spriteLibCategory = this.m_Labels[i];
					break;
				}
			}
			if (spriteLibCategory != null)
			{
				SpriteCategoryEntry spriteCategoryEntry = null;
				for (int j = 0; j < spriteLibCategory.categoryList.Count; j++)
				{
					if (spriteLibCategory.categoryList[j].hash == labelHash)
					{
						spriteCategoryEntry = spriteLibCategory.categoryList[j];
						break;
					}
				}
				if (spriteCategoryEntry != null)
				{
					validEntry = true;
					return spriteCategoryEntry.sprite;
				}
			}
			validEntry = false;
			return null;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003190 File Offset: 0x00001390
		public Sprite GetSprite(string category, string label)
		{
			int num = SpriteLibraryUtility.GetStringHash(category);
			int num2 = SpriteLibraryUtility.GetStringHash(label);
			return this.GetSprite(num, num2);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000031BD File Offset: 0x000013BD
		public IEnumerable<string> GetCategoryNames()
		{
			return this.m_Labels.Select((SpriteLibCategory x) => x.name);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031E9 File Offset: 0x000013E9
		[Obsolete("GetCategorylabelNames has been deprecated. Please use GetCategoryLabelNames (UnityUpgradable) -> GetCategoryLabelNames(*)")]
		public IEnumerable<string> GetCategorylabelNames(string category)
		{
			return this.GetCategoryLabelNames(category);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031F4 File Offset: 0x000013F4
		public IEnumerable<string> GetCategoryLabelNames(string category)
		{
			SpriteLibCategory spriteLibCategory = this.m_Labels.FirstOrDefault((SpriteLibCategory x) => x.name == category);
			if (spriteLibCategory != null)
			{
				return spriteLibCategory.categoryList.Select((SpriteCategoryEntry x) => x.name);
			}
			return new string[0];
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000325C File Offset: 0x0000145C
		public void AddCategoryLabel(Sprite sprite, string category, string label)
		{
			category = category.Trim();
			label = ((label != null) ? label.Trim() : null);
			if (string.IsNullOrEmpty(category))
			{
				throw new ArgumentException("Cannot add empty or null Category string");
			}
			int catHash = SpriteLibraryUtility.GetStringHash(category);
			SpriteLibCategory spriteLibCategory = this.m_Labels.FirstOrDefault((SpriteLibCategory x) => x.hash == catHash);
			if (spriteLibCategory != null)
			{
				if (string.IsNullOrEmpty(label))
				{
					throw new ArgumentException("Cannot add empty or null Label string");
				}
				int labelHash = SpriteLibraryUtility.GetStringHash(label);
				SpriteCategoryEntry spriteCategoryEntry = spriteLibCategory.categoryList.FirstOrDefault((SpriteCategoryEntry y) => y.hash == labelHash);
				if (spriteCategoryEntry != null)
				{
					spriteCategoryEntry.sprite = sprite;
				}
				else
				{
					spriteCategoryEntry = new SpriteCategoryEntry
					{
						name = label,
						sprite = sprite
					};
					spriteLibCategory.categoryList.Add(spriteCategoryEntry);
				}
			}
			else
			{
				SpriteLibCategory spriteLibCategory2 = new SpriteLibCategory
				{
					categoryList = new List<SpriteCategoryEntry>(),
					name = category
				};
				if (!string.IsNullOrEmpty(label))
				{
					spriteLibCategory2.categoryList.Add(new SpriteCategoryEntry
					{
						name = label,
						sprite = sprite
					});
				}
				this.m_Labels.Add(spriteLibCategory2);
			}
			this.UpdateModificationHash();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000338C File Offset: 0x0000158C
		public void RemoveCategoryLabel(string category, string label, bool deleteCategory)
		{
			int catHash = SpriteLibraryUtility.GetStringHash(category);
			SpriteLibCategory libCategory = null;
			libCategory = this.m_Labels.FirstOrDefault((SpriteLibCategory x) => x.hash == catHash);
			if (libCategory != null)
			{
				int labelHash = SpriteLibraryUtility.GetStringHash(label);
				libCategory.categoryList.RemoveAll((SpriteCategoryEntry x) => x.hash == labelHash);
				if (deleteCategory && libCategory.categoryList.Count == 0)
				{
					this.m_Labels.RemoveAll((SpriteLibCategory x) => x.hash == libCategory.hash);
				}
				this.UpdateModificationHash();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003444 File Offset: 0x00001644
		internal void UpdateHashes()
		{
			foreach (SpriteLibCategory spriteLibCategory in this.m_Labels)
			{
				spriteLibCategory.UpdateHash();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003494 File Offset: 0x00001694
		internal void ValidateCategories()
		{
			SpriteLibraryAsset.RenameDuplicate(this.m_Labels, delegate(string originalName, string newName)
			{
				Debug.LogWarning(string.Concat(new string[] { "Category ", originalName, " renamed to ", newName, " due to hash clash" }));
			});
			for (int i = 0; i < this.m_Labels.Count; i++)
			{
				this.m_Labels[i].ValidateLabels();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034F4 File Offset: 0x000016F4
		internal static void RenameDuplicate(IEnumerable<INameHash> nameHashList, Action<string, string> onRename)
		{
			for (int i = 0; i < nameHashList.Count<INameHash>(); i++)
			{
				INameHash category = nameHashList.ElementAt(i);
				IEnumerable<INameHash> enumerable = nameHashList.Where((INameHash x) => (x.hash == category.hash || x.name == category.name) && x != category);
				int j = 0;
				for (int k = 0; k < enumerable.Count<INameHash>(); k++)
				{
					INameHash categoryClash = enumerable.ElementAt(k);
					while (j < 1000)
					{
						string name = categoryClash.name;
						name = string.Format("{0}_{1}", name, j);
						int nameHash = SpriteLibraryUtility.GetStringHash(name);
						if (nameHashList.FirstOrDefault((INameHash x) => (x.hash == nameHash || x.name == name) && x != categoryClash) == null)
						{
							onRename(categoryClash.name, name);
							categoryClash.name = name;
							break;
						}
						j++;
					}
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003630 File Offset: 0x00001830
		private void UpdateModificationHash()
		{
			long num = DateTime.Now.Ticks;
			num ^= (long)this.m_Labels.GetHashCode();
			this.m_ModificationHash = num;
		}

		// Token: 0x04000027 RID: 39
		[SerializeField]
		private List<SpriteLibCategory> m_Labels = new List<SpriteLibCategory>();

		// Token: 0x04000028 RID: 40
		[SerializeField]
		private long m_ModificationHash;
	}
}
